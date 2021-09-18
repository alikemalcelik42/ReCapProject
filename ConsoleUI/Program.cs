using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
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
            CarManager carManager = new CarManager(new EfCarDal());

            List<Car> cars = carManager.GetAllByDailyPrice(250, 300);
            foreach(Car _car in cars)
            {
                Console.WriteLine($"Id: {_car.Id}");
                Console.WriteLine($"BrandId: {_car.BrandId}");
                Console.WriteLine($"ColorId: {_car.ColorId}");
                Console.WriteLine($"ModelYear: {_car.ModelYear}");
                Console.WriteLine($"DailyPrice: {_car.DailyPrice}");
                Console.WriteLine($"Description: {_car.Description}");
                Console.WriteLine();
            }
        }
    }
}
