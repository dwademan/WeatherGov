using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
//using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace WeatherGovApp
{
	class Weather
	{
		private string _weatherurl = @"http://w1.weather.gov/xml/current_obs/KEWR.xml";
		XmlDocument _xmldocument = new XmlDocument();
		XmlElement root;

		private string _city;
		private string _observation;
		private string _currentconditions;
		private float _currenttempture;
		private int _humidity;
		private string _winddirection;
		private float _windspeed;
		private float _pressure;
		private float _dewpoint;
		private float _visibility;
		private string _iconurl;

		public string City
		{
			get { return _city; }
			set { _city = value; }
		}

		public string Observation
		{
			get { return _observation; }
			set { _observation = value; }
		}

		public string CurrentConditions
		{
			get { return _currentconditions; }
			set { _currentconditions = value; }
		}

		public float CurrentTempture
		{
			get { return _currenttempture; }
			set { _currenttempture = value; }
		}

		public int Humidity
		{
			get { return _humidity; }
			set { _humidity = value; }
		}

		public string WindDirection
		{
			get { return _winddirection; }
			set { _winddirection = value; }
		}

		public float WindSpeed
		{
			get { return _windspeed; }
			set { _windspeed = value; }
		}

		public float Pressure
		{
			get { return _pressure; }
			set { _pressure = value; }
		}

		public float DewPoint
		{
			get { return _dewpoint; }
			set { _dewpoint = value; }
		}

		public float Visibility
		{
			get { return _visibility; }
			set { _visibility = value; }
		}

		public string IconUrl
		{
			get { return _iconurl; }
			set { IconUrl = value; }
		}

		public void GetXmlDocument()
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("Reading " + _weatherurl + " ...");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine();

			try
			{
				var request = WebRequest.Create(_weatherurl) as HttpWebRequest;
				request.Method = "GET";
				request.ContentType = "text/xml";
				request.UseDefaultCredentials = true;
				request.KeepAlive = false;
				request.UserAgent = "Weather";

				var response = request.GetResponse();

				Stream receiveStream = response.GetResponseStream();
				StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

				var result = readStream.ReadToEnd();
				_xmldocument.LoadXml(result);
				root = _xmldocument.DocumentElement;
			}
			catch (Exception errorMessage)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(errorMessage.Message);
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine();
				throw;
			}
		}

		public void GetWeatherData()
		{
			//string tempString;
			string[] tempStrings;

			XmlNodeList CityNodes = root.GetElementsByTagName("location");
			tempStrings = Regex.Split(CityNodes.Item(CityNodes.Count - 1).InnerXml, @",");
			_city = ((tempStrings[0] + ", " + tempStrings[2]).Replace("  ", " ")).Trim();

			XmlNodeList ObservationNodes = root.GetElementsByTagName("observation_time");
			_observation = ObservationNodes.Item(ObservationNodes.Count - 1).InnerXml;

			XmlNodeList CurrentConditionsNodes = root.GetElementsByTagName("weather");
			_currentconditions = CurrentConditionsNodes.Item(CurrentConditionsNodes.Count - 1).InnerXml.Replace("  ", " ").Trim();

			XmlNodeList CurrentTemptureNodes = root.GetElementsByTagName("temp_f");
			_currenttempture = float.Parse(CurrentTemptureNodes.Item(CurrentTemptureNodes.Count - 1).InnerXml);

			XmlNodeList HumidityNodes = root.GetElementsByTagName("relative_humidity");
			_humidity = int.Parse(HumidityNodes.Item(HumidityNodes.Count - 1).InnerXml);

			XmlNodeList WindDirectionNodes = root.GetElementsByTagName("wind_dir");
			_winddirection = WindDirectionNodes.Item(WindDirectionNodes.Count - 1).InnerXml;

			XmlNodeList WindSpeedNodes = root.GetElementsByTagName("wind_mph");
			_windspeed = float.Parse(WindSpeedNodes.Item(WindSpeedNodes.Count - 1).InnerXml);

			XmlNodeList PressureNodes = root.GetElementsByTagName("pressure_in");
			_pressure = float.Parse(PressureNodes.Item(PressureNodes.Count - 1).InnerXml);

			XmlNodeList DewpointNodes = root.GetElementsByTagName("dewpoint_f");
			_dewpoint = float.Parse(DewpointNodes.Item(DewpointNodes.Count - 1).InnerXml);

			XmlNodeList VisibilityNodes = root.GetElementsByTagName("visibility_mi");
			_visibility = float.Parse(VisibilityNodes.Item(VisibilityNodes.Count - 1).InnerXml);
			
			Array.Clear(tempStrings, 0, tempStrings.Count());
			XmlNodeList IconUrlBaseNodes = root.GetElementsByTagName("icon_url_base");
			XmlNodeList IconUrlNameNodes = root.GetElementsByTagName("icon_url_name");
			tempStrings[0] = IconUrlBaseNodes.Item(IconUrlBaseNodes.Count - 1).InnerXml;
			tempStrings[1] = IconUrlNameNodes.Item(IconUrlNameNodes.Count - 1).InnerXml;
			_iconurl = tempStrings[0] + tempStrings[1];
		}

		public void ShowValueArray(string []myArray)
		{
			Console.WriteLine();
			Console.WriteLine("----------------------------------------");
			int X = 0;
			foreach (string item in myArray)
			{
				X += 1;
				Console.WriteLine(X + ": " + item);
			}
			Console.WriteLine("----------------------------------------");
			Console.WriteLine();

		}




	}
}