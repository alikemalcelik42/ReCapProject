using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IDataResult<List<CarImage>> GetAll();
        IDataResult<CarImage> GetById(int id);
        IDataResult<List<CarImage>> GetAllByCarId(int carId);
        IResult Add(CarImage carImage, IFormFile imageFile);
        IResult Update(CarImage carImage);
        IResult UpdateWithFile(CarImage carImage, IFormFile imageFile);
        IResult Delete(CarImage carImage);
    }
}
