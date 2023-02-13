using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace car_park_api.Persistance.Models
{
    public sealed class CarPark
    {
        public int CarParkId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public decimal DefaultPricing { get; set; }
    }
}
