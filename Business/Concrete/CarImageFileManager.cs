using Business.Abstract;
using Business.Constants;
using Business.Helpers.FileHelpers;
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

        public IResult AddImage(CarImage carImage, IFormFile imageFile)
        {
            var result = BusinessRules.Run(CheckIfCarImageLimitExceeded(1));

            UploadImage(ref carImage, imageFile);
            return _carImageService.Add(carImage);
        }

        public IDataResult<List<CarImage>> GetAllImages()
        {
            return _carImageService.GetAll();
        }

        public IDataResult<List<CarImage>> GetAllImagesByCarId(int carId)
        {
            return _carImageService.GetAllByCarId(carId);
        }

        public IResult DeleteImage(CarImage carImage)
        {
            RemoveImage(carImage.ImagePath);
            return _carImageService.Delete(carImage);
        }

        [TransactionScopeAspect]
        public IResult UpdateImage(CarImage carImage, IFormFile imageFile)
        {
            var oldCarImage = _carImageService.GetById(carImage.Id).Data;
            IResult result;
            
            result = RemoveImage(oldCarImage.ImagePath);
            if (!result.Success)
                throw new Exception(result.Message);

            result = UploadImage(ref carImage, imageFile);
            if (!result.Success)
                throw new Exception(result.Message);

            return _carImageService.Update(carImage);
        }

        private IResult UploadImage(ref CarImage carImage, IFormFile imageFile)
        {
            string filePath = CarImageFileHelper.Upload(imageFile);
            if (filePath != "")
            {
                carImage.ImagePath = filePath;
                carImage.Date = DateTime.Now;
                return new SuccessResult();
            }
            return new ErrorResult(Messages.CarImageUploadFailed);
        }

        private IResult RemoveImage(string imagePath)
        {
            return CarImageFileHelper.Delete(imagePath);
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
    }
}
