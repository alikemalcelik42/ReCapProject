using Core.Abstract.Entities;
using System;

namespace Entities.Concrete
{
    public class CarImage : IEntity
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string ImageFilePath { get; set; }
        public string ImageRootPath { get; set; }
        public DateTime Date { get; set; }
    }
}
