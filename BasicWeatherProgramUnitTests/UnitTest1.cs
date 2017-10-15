using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BasicWeatherProgram;
using System.Linq;

namespace BasicWeatherProgramUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestWeatherService_KnownRandomTempsAverageAsExpected()
        {
            // arrange
            WeatherData[] weatherDataPointsForMonth = GenerateTestData5Cities_3Dates_RandomTemps();
            WeatherService ws = new WeatherService();

            // act
            IEnumerable<CityAveragedWeatherData> WeatherServiceResults = ws.AggregateWeatherData(weatherDataPointsForMonth);

            List<CityAveragedWeatherData> weatherServiceResultsList = new List<CityAveragedWeatherData>(WeatherServiceResults);

            decimal denverAvgHigh = Math.Round(weatherServiceResultsList.Find(s => s.State == "CO" && s.City == "Denver").AverageHighTemp, 4);
            decimal denverAvgLow = Math.Round(weatherServiceResultsList.Find(s => s.State == "CO" && s.City == "Denver").AverageLowTemp, 4);
            decimal oaklandAvgHigh = Math.Round(weatherServiceResultsList.Find(s => s.State == "CA" && s.City == "Oakland").AverageHighTemp, 4);
            decimal oakalandAvgLow = Math.Round(weatherServiceResultsList.Find(s => s.State == "CA" && s.City == "Oakland").AverageLowTemp, 4);
            decimal portlandMaAvgHigh = Math.Round(weatherServiceResultsList.Find(s => s.State == "MA" && s.City == "Portland").AverageHighTemp, 4);
            decimal portlandMaAvgLow = Math.Round(weatherServiceResultsList.Find(s => s.State == "MA" && s.City == "Portland").AverageLowTemp, 4);
            decimal portlandOrAvgHigh = Math.Round(weatherServiceResultsList.Find(s => s.State == "OR" && s.City == "Portland").AverageHighTemp, 4);
            decimal portlandOrAvgLow = Math.Round(weatherServiceResultsList.Find(s => s.State == "OR" && s.City == "Portland").AverageLowTemp, 4);
            decimal atlantaGaAvgHigh = Math.Round(weatherServiceResultsList.Find(s => s.State == "GA" && s.City == "Atlanta").AverageHighTemp, 4);
            decimal atlantaGaAvgLow = Math.Round(weatherServiceResultsList.Find(s => s.State == "GA" && s.City == "Atlanta").AverageLowTemp, 4);
            
            // assert
            decimal expectedDenverAverageHigh = 59.3333M;
            decimal expectedDenverAverageLow = 34M;
            Assert.AreEqual(expectedDenverAverageHigh, denverAvgHigh, "The average high for Denver. CO was not the value expected.");
            Assert.AreEqual(expectedDenverAverageLow, denverAvgLow, "The average low for Denver. CO was not the value expected.");

            decimal expectedOaklandAverageHigh = 77.6667M;
            decimal expectedOaklandAverageLow = 51.6667M;
            Assert.AreEqual(expectedOaklandAverageHigh, oaklandAvgHigh, "The average high for Oakland, CA was not the value expected.");
            Assert.AreEqual(expectedOaklandAverageLow, oakalandAvgLow, "The average low for Oakland, CA was not the value expected.");

            decimal expectedPortlandMaAverageHigh = 43.6667M;
            decimal expectedPortlandMaAverageLow = 23.6667M;
            Assert.AreEqual(expectedPortlandMaAverageHigh, portlandMaAvgHigh, "The average high for Portland, MA was not the value expected.");
            Assert.AreEqual(expectedPortlandMaAverageLow, portlandMaAvgLow, "The average low for Portland, MA was not the value expected.");

            decimal expectedPortlandOrAverageHigh = 53.3333M;
            decimal expectedPortlandOrAverageLow = 30M;
            Assert.AreEqual(expectedPortlandOrAverageHigh, portlandOrAvgHigh, "The average high for Portland, OR was not the value expected.");
            Assert.AreEqual(expectedPortlandOrAverageLow, portlandOrAvgLow, "The average low for Portland, OR was not the value expected.");

            decimal expectedAtlantaGaAverageHigh = 76.3333M;
            decimal expectedAtlantaGaAverageLow = 55.3333M;
            Assert.AreEqual(expectedAtlantaGaAverageHigh, atlantaGaAvgHigh, "The average high for Atlanta, GA was not the value expected.");
            Assert.AreEqual(expectedAtlantaGaAverageLow, atlantaGaAvgLow, "The average low for Atlanta, GA was not the value expected.");
        }

        [TestMethod]
        public void TestWeatherService_NullArrayOfWeatherDataObjects()
        {
            // arrange
            WeatherData[] weatherDataPointsForMonth = null;
            WeatherService ws = new WeatherService();

            // act
            IEnumerable<CityAveragedWeatherData> WeatherServiceResults = ws.AggregateWeatherData(weatherDataPointsForMonth);

            // assert
            Assert.IsNull(WeatherServiceResults, "The AggregateWeatherData should have returned null, but did not.");
        }

        [TestMethod]
        public void TestWeatherService_EmptyArrayOfWeatherDataObjects()
        {
            // arrange
            WeatherData[] weatherDataPointsForMonth = new WeatherData[] {};
            WeatherService ws = new WeatherService();

            // act
            IEnumerable<CityAveragedWeatherData> WeatherServiceResults = ws.AggregateWeatherData(weatherDataPointsForMonth);

            // assert
            Assert.AreEqual(0, WeatherServiceResults.Count(), "The WeatherServiceResults should have been empty, but was not.");
        }

        [TestMethod]
        public void TestWeatherService_SingleWeatherDataObject()
        {
            // arrange
            WeatherData[] weatherDataPointsForMonth = GenerateTestDataOneCity_OneDate();
            WeatherService ws = new WeatherService();

            // act
            IEnumerable<CityAveragedWeatherData> WeatherServiceResults = ws.AggregateWeatherData(weatherDataPointsForMonth);

            List<CityAveragedWeatherData> weatherServiceResultsList = new List<CityAveragedWeatherData>(WeatherServiceResults);

            decimal denverAvgHigh = Math.Round(weatherServiceResultsList.Find(s => s.State == "CO" && s.City == "Denver").AverageHighTemp, 4);
            decimal denverAvgLow = Math.Round(weatherServiceResultsList.Find(s => s.State == "CO" && s.City == "Denver").AverageLowTemp, 4);

            // assert
            decimal expectedDenverAverageHigh = 80M;
            decimal expectedDenverAverageLow = 45M;
            Assert.AreEqual(expectedDenverAverageHigh, denverAvgHigh, "The average high for Denver. CO was not the value expected.");
            Assert.AreEqual(expectedDenverAverageLow, denverAvgLow, "The average low for Denver. CO was not the value expected.");
        }

        [TestMethod]
        public void TestWeatherService_SingleCityMissingOneLowTemp()
        {
            // arrange
            WeatherData[] weatherDataPoints = GenerateTestDataOneCity_FiveDates_OneMissingLow();
            WeatherService ws = new WeatherService();

            // act
            IEnumerable<CityAveragedWeatherData> WeatherServiceResults = ws.AggregateWeatherData(weatherDataPoints);

            List<CityAveragedWeatherData> weatherServiceResultsList = new List<CityAveragedWeatherData>(WeatherServiceResults);

            decimal denverAvgHigh = Math.Round(weatherServiceResultsList.Find(s => s.State == "CO" && s.City == "Denver").AverageHighTemp, 4);
            decimal denverAvgLow = Math.Round(weatherServiceResultsList.Find(s => s.State == "CO" && s.City == "Denver").AverageLowTemp, 4);

            // assert
            decimal expectedDenverAverageHigh = 80M;
            decimal expectedDenverAverageLow = 48M;     // Empty data element is ignored: 5 data points => (L + L + L + L)/5 = average
            Assert.AreEqual(expectedDenverAverageHigh, denverAvgHigh, "The average high for Denver. CO was not the value expected.");
            Assert.AreEqual(expectedDenverAverageLow, denverAvgLow, "The average low for Denver. CO was not the value expected.");
        }

        [TestMethod]
        public void TestWeatherService_SingleCityMissingOneHighTemp()
        {
            // arrange
            WeatherData[] weatherDataPoints = GenerateTestDataOneCity_FiveDates_OneMissingHigh();
            WeatherService ws = new WeatherService();

            // act
            IEnumerable<CityAveragedWeatherData> WeatherServiceResults = ws.AggregateWeatherData(weatherDataPoints);

            List<CityAveragedWeatherData> weatherServiceResultsList = new List<CityAveragedWeatherData>(WeatherServiceResults);

            decimal denverAvgHigh = Math.Round(weatherServiceResultsList.Find(s => s.State == "CO" && s.City == "Denver").AverageHighTemp, 4);
            decimal denverAvgLow = Math.Round(weatherServiceResultsList.Find(s => s.State == "CO" && s.City == "Denver").AverageLowTemp, 4);

            // assert
            decimal expectedDenverAverageHigh = 64M;     // Empty data element is ignored: 5 data points => (H + H + H + H)/5
            decimal expectedDenverAverageLow = 60M;
            Assert.AreEqual(expectedDenverAverageHigh, denverAvgHigh, "The average high for Denver. CO was not the value expected.");
            Assert.AreEqual(expectedDenverAverageLow, denverAvgLow, "The average low for Denver. CO was not the value expected.");
        }

        [TestMethod]
        public void TestWeatherService_SingleCityMissingOneHighTempAndOneLowTemp()
        {
            // arrange
            WeatherData[] weatherDataPoints = GenerateTestDataOneCity_FiveDates_OneMissingHighOneMissingLow();
            WeatherService ws = new WeatherService();

            // act
            IEnumerable<CityAveragedWeatherData> WeatherServiceResults = ws.AggregateWeatherData(weatherDataPoints);

            List<CityAveragedWeatherData> weatherServiceResultsList = new List<CityAveragedWeatherData>(WeatherServiceResults);

            decimal denverAvgHigh = Math.Round(weatherServiceResultsList.Find(s => s.State == "CO" && s.City == "Denver").AverageHighTemp, 4);
            decimal denverAvgLow = Math.Round(weatherServiceResultsList.Find(s => s.State == "CO" && s.City == "Denver").AverageLowTemp, 4);

            // assert
            decimal expectedDenverAverageHigh = 64M;     // Empty data element is ignored: 5 data points => (H + H + H + H)/5
            decimal expectedDenverAverageLow = 48M;
            Assert.AreEqual(expectedDenverAverageHigh, denverAvgHigh, "The average high for Denver. CO was not the value expected.");
            Assert.AreEqual(expectedDenverAverageLow, denverAvgLow, "The average low for Denver. CO was not the value expected.");
        }



        #region Private supporting components


        // Denver - 45, 80 : 24, 41 : 33, 57                Denver :    Avg Low: 34M        Avg High: 59.3333M
        // Oakland - 60, 85 : 44, 72 : 51, 76               Oakland:    Avg Low: 51.6667M   Avg High: 77.6666M
        // Portland, MA - 37, 66 : 22, 38 : 12, 27          Port, MA    Avg Low: 23.6666M   Avg High: 43.6666M
        // Portland, OR  -  17, 53 : 32, 38 : 41, 69        Port, OR    Avg Low: 30M        Avg High: 53.3333M
        // Atlanta, GA  -  66, 79 : 38, 59 : 62, 91         Atlanta     Avg Low: 55.3333M   Avg High: 76.3333M
        private WeatherData[] GenerateTestData5Cities_3Dates_RandomTemps()
        {
            WeatherData[] weatherDataArray = new WeatherData[15];
            weatherDataArray[0] = new WeatherData() { City = "Denver", State = "CO", Date = new DateTime(2017, 6, 1), LowTemp = 45, HighTemp = 80 };
            weatherDataArray[1] = new WeatherData() { City = "Oakland", State = "CA", Date = new DateTime(2017, 6, 1), LowTemp = 60, HighTemp = 85 };
            weatherDataArray[2] = new WeatherData() { City = "Portland", State = "MA", Date = new DateTime(2017, 5, 15), LowTemp = 37, HighTemp = 66 };
            weatherDataArray[3] = new WeatherData() { City = "Portland", State = "OR", Date = new DateTime(2017, 2, 1), LowTemp = 17, HighTemp = 53 };
            weatherDataArray[4] = new WeatherData() { City = "Atlanta", State = "GA", Date = new DateTime(2017, 4, 1), LowTemp = 66, HighTemp = 79 };
            weatherDataArray[5] = new WeatherData() { City = "Denver", State = "CO", Date = new DateTime(2017, 2, 22), LowTemp = 24, HighTemp = 41 };
            weatherDataArray[6] = new WeatherData() { City = "Oakland", State = "CA", Date = new DateTime(2017, 1, 1), LowTemp = 44, HighTemp = 72 };
            weatherDataArray[7] = new WeatherData() { City = "Portland", State = "MA", Date = new DateTime(2017, 3, 12), LowTemp = 22, HighTemp = 38 };
            weatherDataArray[8] = new WeatherData() { City = "Portland", State = "OR", Date = new DateTime(2017, 1, 23), LowTemp = 32, HighTemp = 38 };
            weatherDataArray[9] = new WeatherData() { City = "Atlanta", State = "GA", Date = new DateTime(2017, 3, 23), LowTemp = 38, HighTemp = 59 };
            weatherDataArray[10] = new WeatherData() { City = "Denver", State = "CO", Date = new DateTime(2017, 4, 12), LowTemp = 33, HighTemp = 57 };
            weatherDataArray[11] = new WeatherData() { City = "Oakland", State = "CA", Date = new DateTime(2017, 3, 18), LowTemp = 51, HighTemp = 76 };
            weatherDataArray[12] = new WeatherData() { City = "Portland", State = "MA", Date = new DateTime(2017, 1, 17), LowTemp = 12, HighTemp = 27 };
            weatherDataArray[13] = new WeatherData() { City = "Portland", State = "OR", Date = new DateTime(2017, 4, 30), LowTemp = 41, HighTemp = 69 };
            weatherDataArray[14] = new WeatherData() { City = "Atlanta", State = "GA", Date = new DateTime(2017, 6, 5), LowTemp = 62, HighTemp = 91 };
            return weatherDataArray;
        }

        // Denver - 45, 80     Avg Low: 45M       Avg High: 80M
        private WeatherData[] GenerateTestDataOneCity_OneDate()
        {
            WeatherData[] weatherDataArray = new WeatherData[1];
            weatherDataArray[0] = new WeatherData() { City = "Denver", State = "CO", Date = new DateTime(2017, 6, 1), LowTemp = 45, HighTemp = 80 };
            return weatherDataArray;
        }

        // Denver - 60, 80 : 60, 80 : ,80 : 60, 80, : 60, 80    Avg Low: 48M      Avg High: 80M
        private WeatherData[] GenerateTestDataOneCity_FiveDates_OneMissingLow()
        {
            WeatherData[] weatherDataArray = new WeatherData[5];
            weatherDataArray[0] = new WeatherData() { City = "Denver", State = "CO", Date = new DateTime(2017, 6, 1), LowTemp = 60, HighTemp = 80 };
            weatherDataArray[1] = new WeatherData() { City = "Denver", State = "CO", Date = new DateTime(2017, 6, 2), LowTemp = 60, HighTemp = 80 };
            weatherDataArray[2] = new WeatherData() { City = "Denver", State = "CO", Date = new DateTime(2017, 6, 3), HighTemp = 80 };
            weatherDataArray[3] = new WeatherData() { City = "Denver", State = "CO", Date = new DateTime(2017, 6, 4), LowTemp = 60, HighTemp = 80 };
            weatherDataArray[4] = new WeatherData() { City = "Denver", State = "CO", Date = new DateTime(2017, 6, 5), LowTemp = 60, HighTemp = 80 };
            return weatherDataArray;
        }

        // Denver - 60, 80 : 60, 80 : 60, : 60, 80, : 60, 80    Avg Low:  60M     Avg High: 64M
        private WeatherData[] GenerateTestDataOneCity_FiveDates_OneMissingHigh()
        {
            WeatherData[] weatherDataArray = new WeatherData[5];
            weatherDataArray[0] = new WeatherData() { City = "Denver", State = "CO", Date = new DateTime(2017, 6, 1), LowTemp = 60, HighTemp = 80 };
            weatherDataArray[1] = new WeatherData() { City = "Denver", State = "CO", Date = new DateTime(2017, 6, 2), LowTemp = 60, HighTemp = 80 };
            weatherDataArray[2] = new WeatherData() { City = "Denver", State = "CO", Date = new DateTime(2017, 6, 3), LowTemp = 60 };
            weatherDataArray[3] = new WeatherData() { City = "Denver", State = "CO", Date = new DateTime(2017, 6, 4), LowTemp = 60, HighTemp = 80 };
            weatherDataArray[4] = new WeatherData() { City = "Denver", State = "CO", Date = new DateTime(2017, 6, 5), LowTemp = 60, HighTemp = 80 };
            return weatherDataArray;
        }

        // Denver - 60, 80 : 60, 80 :  , : 60, 80, : 60, 80    Avg Low: 48M      Avg High: 64M
        private WeatherData[] GenerateTestDataOneCity_FiveDates_OneMissingHighOneMissingLow()
        {
            WeatherData[] weatherDataArray = new WeatherData[5];
            weatherDataArray[0] = new WeatherData() { City = "Denver", State = "CO", Date = new DateTime(2017, 6, 1), LowTemp = 60, HighTemp = 80 };
            weatherDataArray[1] = new WeatherData() { City = "Denver", State = "CO", Date = new DateTime(2017, 6, 2), LowTemp = 60, HighTemp = 80 };
            weatherDataArray[2] = new WeatherData() { City = "Denver", State = "CO", Date = new DateTime(2017, 6, 3)};
            weatherDataArray[3] = new WeatherData() { City = "Denver", State = "CO", Date = new DateTime(2017, 6, 4), LowTemp = 60, HighTemp = 80 };
            weatherDataArray[4] = new WeatherData() { City = "Denver", State = "CO", Date = new DateTime(2017, 6, 5), LowTemp = 60, HighTemp = 80 };
            return weatherDataArray;
        }


        #endregion Private supporting components
    }
}
