using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWeatherProgram
{
    public class WeatherData
    {
        public string State { get; set; }
        public string City { get; set; }
        public DateTime Date { get; set; }
        public decimal HighTemp { get; set; }
        public decimal LowTemp { get; set; }
    }
}
