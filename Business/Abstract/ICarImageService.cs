using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IDataResult<List<CarImage>> GetAll();
        IDataResult<CarImage> GetById(int id);
        IDataResult<List<CarImage>> GetAllByCarId(int carId);
        IResult AddImage(CarImage carImage, IFormFile imageFile);
        IResult DeleteImage(CarImage carImage);
        IResult UpdateImage(CarImage carImage, IFormFile imageFile);
        IResult Add(CarImage carImage);
        IResult Update(CarImage carImage);
        IResult Delete(CarImage carImage);
    }
}
