using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Secure;
using Core.Aspects.Autofac.Transaction;
using Core.CrossCuttingConcerns.Logging.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        IFileService _fileService;

        public CarImageManager(ICarImageDal carImageDal, IFileService fileService)
        {
            _carImageDal = carImageDal;
            _fileService = fileService;
        }

        [SecuredOperation("carimage.add,admin")]
        [CacheRemoveAspect("ICarImageService.Get")]
        [LogAspect(typeof(FileLogger))]
        [TransactionScopeAspect]
        public IResult Add(CarImage carImage, IFormFile imageFile)
        {
            var result = BusinessRules.Run(CheckCarImageLimitExceeded(carImage.CarId));

            if(result != null)
                return result;

            var fileResult =_fileService.Add(imageFile);
            carImage.ImageFilePath = fileResult.Data.FilePath;
            carImage.ImageRootPath = fileResult.Data.RootPath;

            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }

        [SecuredOperation("carimage.delete,admin")]
        [CacheRemoveAspect("ICarImageService.Get")]
        [LogAspect(typeof(FileLogger))]
        public IResult Delete(CarImage carImage)
        {
            _fileService.Delete(carImage.ImageFilePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.CarImageDeleted);
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), Messages.CarImagesListed);
        }

        public IDataResult<List<CarImage>> GetAllByCarId(int carId)
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(ci => ci.CarId == carId),
                Messages.CarImagesListed);
        }

        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(ci => ci.Id == id), Messages.CarImagesListed);
        }

        [SecuredOperation("carimage.update,admin")]
        [CacheRemoveAspect("ICarImageService.Get")]
        [LogAspect(typeof(FileLogger))]
        [TransactionScopeAspect]
        public IResult Update(CarImage carImage)
        {
            var result = GetById(carImage.Id);
            carImage.ImageFilePath = result.Data.ImageFilePath;
            carImage.ImageRootPath = result.Data.ImageRootPath;

            _carImageDal.Update(carImage);

            return new SuccessResult(Messages.CarImageUpdated);
        }

        [SecuredOperation("carimage.update,admin")]
        [CacheRemoveAspect("ICarImageService.Get")]
        [LogAspect(typeof(FileLogger))]
        [TransactionScopeAspect]
        public IResult UpdateWithFile(CarImage carImage, IFormFile imageFile)
        {
            string oldFilePath = GetById(carImage.Id).Data.ImageFilePath;
            var result = _fileService.Update(oldFilePath, imageFile);
            carImage.ImageFilePath = result.Data.FilePath;
            carImage.ImageRootPath = result.Data.RootPath;

            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }

        private IResult CheckCarImageLimitExceeded(int carId)
        {
            var result = GetAllByCarId(carId);

            if (result.Data.Count >= 5)
                return new ErrorResult(Messages.CarImageLimitExceeded);
            return new SuccessResult();
        }
    }
}
