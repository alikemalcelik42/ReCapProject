using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : ICarDal
    {
        public void Add(Car entity)
        {
            using(CarRentalContext context = new CarRentalContext())
            {
                var addCar = context.Entry(entity);
                addCar.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(Car entity)
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var deleteCar = context.Entry(entity);
                deleteCar.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public Car Get(Expression<Func<Car, bool>> filter = null)
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                return filter is null
                    ? context.Set<Car>().SingleOrDefault()
                    : context.Set<Car>().SingleOrDefault(filter);
            }
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                return filter is null
                    ? context.Set<Car>().ToList()
                    : context.Set<Car>().Where(filter).ToList();
            }
        }

        public void Update(Car entity)
        {
            using(CarRentalContext context = new CarRentalContext())
            {
                var updateCar = context.Entry(entity);
                updateCar.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
