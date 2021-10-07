using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarImageFileService
    {
        IResult AddImage(CarImage carImage, IFormFile imageFile);
        IDataResult<List<CarImage>> GetAllImages();
        IDataResult<List<CarImage>> GetAllImagesByCarId(int carId);
        IResult DeleteImage(CarImage carImage);
        IResult UpdateImage(CarImage carImage, IFormFile imageFile);
    }
}
