using Business.Abstract;
using Business.Constants;
using Business.Helpers.FileHelpers;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Secure;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarImageFileManager : ICarImageFileService
    {
        ICarImageService _carImageService;

        public CarImageFileManager(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }

        // [SecuredOperation("carimagefile.add,admin")]
        [CacheRemoveAspect("ICarImageFileService.Get")]
        public IResult AddImage(CarImage carImage, IFormFile imageFile)
        {
            var result = BusinessRules.Run(CheckIfCarImageLimitExceeded(carImage.CarId));

            if (result != null)
                return result;

            CarImageFileHelper.Upload(imageFile, ref carImage);
            return _carImageService.Add(carImage);
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetAllImages()
        {
            return _carImageService.GetAll();
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetAllImagesByCarId(int carId)
        {
            var result = GetCarImageByCarIdByCheckIfCarImageNotExists(carId);
            return result;
        }

        // [SecuredOperation("carimagefile.delete,admin")]
        [CacheRemoveAspect("ICarImageFileService.Get")]
        public IResult DeleteImage(CarImage carImage)
        {
            CarImageFileHelper.Delete(CarImageFileHelper.GetFileNameByUrl(carImage.ImagePath));
            return _carImageService.Delete(carImage);
        }

        [TransactionScopeAspect]
        //[SecuredOperation("carimagefile.update,admin")]
        [CacheRemoveAspect("ICarImageFileService.Get")]
        public IResult UpdateImage(CarImage carImage, IFormFile imageFile)
        {
            var oldCarImage = _carImageService.GetById(carImage.Id).Data;
            
            var deleteResult = CarImageFileHelper.Delete(CarImageFileHelper.GetFileNameByUrl(oldCarImage.ImagePath));
            if (!deleteResult.Success)
                throw new Exception(deleteResult.Message);

            var uploadResult = CarImageFileHelper.Upload(imageFile, ref carImage);
            if (!uploadResult.Success)
                throw new Exception(uploadResult.Message);

            return _carImageService.Update(carImage);
        }

        private IResult CheckIfCarImageLimitExceeded(int carId)
        {
            var result = _carImageService.GetAllByCarId(carId);
            if (result.Data.Count >= 5)
            {
                return new ErrorResult(Messages.CarImageLimitExceeded);
            }
            return new SuccessResult();
        }

        private IDataResult<List<CarImage>> GetCarImageByCarIdByCheckIfCarImageNotExists(int carId)
        {
            var carImages = _carImageService.GetAllByCarId(carId);
            if(carImages.Success == true && carImages.Data.Count == 0)
            {
                return new SuccessDataResult<List<CarImage>>(new List<CarImage>()
                {
                    new CarImage()
                    {
                        Id = 0,
                        CarId = carId,
                        ImagePath = CarImageFileHelper.GetDefaultCarImagePath(),
                        Date = DateTime.Now
                    }
                });
            }
            return carImages;
        }
    }
}
