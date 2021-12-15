using Core.Entities.Abstract;
using System;

namespace Entities.DTOs
{
    public class RentalDetailDto : IDto
    {
        public string CarBrandName { get; set; }
        public string CarColorName { get; set; }
        public int CarModelYear { get; set; }
        public decimal CarDailyPrice { get; set; }
        public string CarDescription { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerCompanyName { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
