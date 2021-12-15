using System;

namespace Core.Entities.DTOs
{
    public class IpDetailDto
    {
        public string IpAdress { get; set; }
        public DateTime Time { get; set; }
        public int WarningCount { get; set; }
    }
}
