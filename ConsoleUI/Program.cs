using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new InMemoryCarDal());

            Car car = new Car()
            {
                Id = 4,
                BrandId = 3,
                ColorId = 1,
                ModelYear = 2015,
                DailyPrice = 350,
                Description = "Çok Temiz 316i Comfort Borusan Bakımlı"
            };

            carManager.Add(car);
            car.DailyPrice = 375;
            carManager.Update(car);

            List<Car> cars = carManager.GetAll();

            foreach(Car _car in cars)
            {
                Console.WriteLine(_car.Id);
                Console.WriteLine(_car.BrandId);
                Console.WriteLine(_car.ColorId);
                Console.WriteLine(_car.ModelYear);
                Console.WriteLine(_car.DailyPrice);
                Console.WriteLine(_car.Description);
                Console.WriteLine();
            }
        }
    }
}
