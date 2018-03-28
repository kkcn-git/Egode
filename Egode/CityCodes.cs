using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Egode
{
	class CityCodes
	{
		private class CityCodeInfo
		{
			public string _city;
			public string _code;
			
			public CityCodeInfo(string city, string code)
			{
				_city = city;
				_code = code;
			}
		}
	
		private static List<CityCodeInfo> _cityCodes;
	
		public static string GetCityCode(string city)
		{
			if (string.IsNullOrEmpty(city))
				return string.Empty;
			
			foreach (CityCodeInfo cci in _cityCodes)
			{
				if (cci._city.Equals(city.Trim()))
					return cci._code;
				
				if (cci._city.Length != city.Length)
					continue;
				
				if (cci._city.StartsWith(city) || city.StartsWith(cci._city))
					return cci._code;
			}
			return string.Empty;
		}
		
		static CityCodes()
		{
			_cityCodes = new List<CityCodeInfo>();
		
			string path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "city-code.txt");
			StreamReader reader = new StreamReader(path);
			while (!reader.EndOfStream)
			{
				string s = reader.ReadLine();
				//System.Diagnostics.Trace.WriteLine(s);
				string[] info =  s.Split(new char[]{' '});
				if (info.Length < 2)
					continue;
				
				string city = info[0].Trim();
				string code = info[1].Trim();
				if (code.StartsWith("0") && code.Length > 3)
					code = code.Remove(0, 1);
				
				_cityCodes.Add(new CityCodeInfo(city, code));
			}
			reader.Close();
		}
	}
}