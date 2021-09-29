using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelpers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult AddImage(CarImage carImage, IFormFile imageFile)
        {
            var result = UploadImage(ref carImage, imageFile);

            if (!result.Success)
                return result;

            return Add(carImage);
        }

        public IResult DeleteImage(CarImage carImage)
        {
            var result = CarImageFileHelper.Delete(carImage.ImagePath);
            
            if(!result.Success)
                return result;

            return Delete(carImage);
        }

        public IResult UpdateImage(CarImage carImage, IFormFile imageFile)
        {
            var oldCarImage = GetById(carImage.Id).Data;

            var firstResult = CarImageFileHelper.Delete(oldCarImage.ImagePath);
            if (!firstResult.Success)
                return firstResult;

            var secondResult = UploadImage(ref carImage, imageFile);
            if (!secondResult.Success)
                return secondResult;

            return Update(carImage);
        }

        public IResult Add(CarImage carImage)
        {
            IResult result = BusinessRules.Run(CheckIfCarImageLimitExceeded(carImage.CarId));

            if (result != null)
                return result;

            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }

        public IResult Delete(CarImage carImage)
        {
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.CarImageDeleted);
        }

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

        public IResult Update(CarImage carImage)
        {
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }

        private IResult CheckIfCarImageLimitExceeded(int carId)
        {
            var result = GetAllByCarId(carId);
            if(result.Data.Count >= 5)
            {
                return new ErrorResult(Messages.CarImageLimitExceeded);
            }
            return new SuccessResult();
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
    }
}
