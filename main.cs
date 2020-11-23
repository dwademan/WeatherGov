using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
//using System.Xml.Linq;

namespace WeatherGovApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Weather myWeather = new Weather();
            myWeather.GetXmlDocument();
            myWeather.GetWeatherData();

            Console.WriteLine("   Current Location: " + myWeather.City);
            Console.WriteLine("        Observation: " + myWeather.Observation);
            Console.WriteLine(" Current Conditions: " + myWeather.CurrentConditions);
            Console.WriteLine("   Current Tempture: {0:0.0} °F", myWeather.CurrentTempture);
            Console.WriteLine("           Humidity: " + myWeather.Humidity + " %");
            Console.WriteLine("               Wind: {0} {1:0.0} mph", myWeather.WindDirection, myWeather.WindSpeed);
            Console.WriteLine("           Pressure: {0:0.00} in", myWeather.Pressure);
            Console.WriteLine("          Dew Point: {0:0.0} °F", myWeather.DewPoint);
            Console.WriteLine("         Visibility: {0:0.00} miles", myWeather.Visibility);
            Console.WriteLine("           Icon URL: " + myWeather.IconUrl);


            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
        }
    }
}