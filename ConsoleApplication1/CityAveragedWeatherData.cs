using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWeatherProgram
{
    public class CityAveragedWeatherData
    {
            public string State { get; set; }
            public string City { get; set; }
            public decimal AverageHighTemp { get; set; }
            public decimal AverageLowTemp { get; set; }
    }
}
