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

            var carDetails = carManager.GetCarDetails();

            foreach(var carDetail in carDetails)
            {
                Console.WriteLine($"{carDetail.BrandName} / {carDetail.ColorName}");
            }
        }
    }
}
