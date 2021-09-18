using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car>()
            {
            new Car() { Id = 1, BrandId = 1, ColorId = 1, ModelYear = 2019, DailyPrice = 200, Description = "2020 MODEL YENİ PASSAT FULL HATASIZ BOYASIZ 25.000KM" },
            new Car() { Id = 2, BrandId = 2, ColorId = 2, ModelYear = 2020, DailyPrice = 400, Description = "BAYİ-SIFIRDAN FARKSIZ AMG KAPALI GARAJDA MUHAFAZA EDİLMEKTEDİR." },
            new Car() { Id = 3, BrandId = 2, ColorId = 2, ModelYear = 2014, DailyPrice = 300, Description = "2014 MODEL 75 BİN KMDE SERVİS BAKIMLI SIFIR AYARINDA CAM TAVAN" },
        }; 
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car deleteCar = _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars.Remove(deleteCar);
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Car Get(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car updateCar = _cars.SingleOrDefault(c => c.Id == car.Id);
            updateCar.BrandId = car.BrandId;
            updateCar.ColorId = car.ColorId;
            updateCar.ModelYear = car.ModelYear;
            updateCar.DailyPrice = car.DailyPrice;
            updateCar.Description = car.Description;
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }
    }
}
