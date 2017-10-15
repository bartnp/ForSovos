using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWeatherProgram
{
    public class WeatherService
    {
        public IEnumerable<CityAveragedWeatherData> AggregateWeatherData(WeatherData[] inputData)
        {
            IEnumerable<CityAveragedWeatherData> cityAveraged = null;

            if (inputData != null)
            {
                cityAveraged = inputData
                                .GroupBy(cs => new { cs.State, cs.City })
                                .Select(csr => new CityAveragedWeatherData
                                {
                                    State = csr.Key.State,
                                    City = csr.Key.City,
                                    AverageHighTemp = System.Convert.ToDecimal(csr.Average(id => id.HighTemp)),
                                    AverageLowTemp = System.Convert.ToDecimal(csr.Average(id => id.LowTemp))
                                });
            }

            return cityAveraged;
        }
    }
}
