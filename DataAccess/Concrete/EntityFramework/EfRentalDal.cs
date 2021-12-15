using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EntityRepositoryBase<Rental, CarRentalContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var result = from r in context.Rentals
                             join cu in context.Customers
                             on r.CustomerId equals cu.Id
                             join u in context.Users
                             on cu.UserId equals u.Id
                             join ca in context.Cars
                             on r.CarId equals ca.Id
                             join co in context.Colors
                             on ca.ColorId equals co.Id
                             join b in context.Brands
                             on ca.BrandId equals b.Id
                             select new RentalDetailDto()
                             {
                                 CarBrandName = b.Name,
                                 CarColorName = co.Name,
                                 CarModelYear = ca.ModelYear,
                                 CarDailyPrice = ca.DailyPrice,
                                 CarDescription = ca.Description,
                                 CustomerFirstName = u.FirstName,
                                 CustomerLastName = u.LastName,
                                 CustomerEmail = u.Email,
                                 CustomerCompanyName = cu.CompanyName,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate
                             };

                return result.ToList();
            }
        }
    }
}
