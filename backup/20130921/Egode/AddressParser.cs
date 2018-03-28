using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Egode
{
	public class AddressParser
	{
		private string _fullAddress;
		private string _province;
		private string _city1;
		private string _city2;
		private string _district;
		private string _streetAddress;
		
		public string Province
		{
			get { return _province; }
		}
		
		public string City1
		{
			get { return _city1; }
		}
		
		public string City2
		{
			get { return _city2; }
		}
		
		public string District
		{
			get { return _district; }
		}
		
		public string StreetAddress
		{
			get { return _streetAddress; }
		}

		// fullAddress: like: 浙江省 温州市 乐清市 浙江省乐清市虹桥镇邬家桥村龙华路22号(000000)
		public AddressParser(string fullAddress)
		{
			_fullAddress = fullAddress;
		
			// remove post code.
			string postCode = string.Empty;
			Regex regex = new Regex(@"(\(\d{6}\))|(,\d{6}$)");
			Match m = regex.Match(fullAddress);
			//Trace.Assert(m.Success);
			if (m.Success)
				fullAddress = fullAddress.Replace(m.Value, string.Empty);

			// Analyse address. Get information for original address.
			// Get address details.
			string[] addressInfos = fullAddress.Split(' ');
			_province = string.Empty;
			_city1 = string.Empty;
			_city2 = string.Empty;
			foreach (string s in addressInfos)
			{
				string ss = s.Trim();

				if (ss.EndsWith("省") || ss.EndsWith("自治区") || ss.Equals("北京") || ss.Equals("上海") || ss.Equals("天津") || ss.Equals("重庆"))
					_province = ss;
				if (ss.Length <= 6 && (ss.EndsWith("市") || ss.EndsWith("县") || ss.EndsWith("自治州")) && !ss.StartsWith(_province))
				{
					if (string.IsNullOrEmpty(_city1))
						_city1 = ss;
					else
						_city2 = ss;
				}
			}
			if (!_province.EndsWith("省") && !_province.EndsWith("市") && !_province.EndsWith("自治区")) // 直辖市or自治区.
				_province += "市";
			
			// Get _district;
			_streetAddress = RemoveStartingProvinceCity(fullAddress, _province, _city1, _city2);
			string[] streetAddressInfos = _streetAddress.Split(' '); // 此处如果还有空格分隔, 则认为第1个字符串是区. 但是如果买家在街道地址中也输入了空格, 可能出错.
			_district = string.Empty;
			if (streetAddressInfos.Length >= 2 && (streetAddressInfos[0].EndsWith("区") || streetAddressInfos[0].EndsWith("镇")))
				_district = streetAddressInfos[0];
			if (!string.IsNullOrEmpty(_district))
				_streetAddress = _streetAddress.Replace(_district, string.Empty).Trim();

			// 至此, 得到了去掉了前面的省市区的地址, 也即是买家在淘宝上手动输入的部分. 
			// 需要去除买家可能重复输入的省市区信息.
			if (_streetAddress.StartsWith(_province)) // remove starting _province.
			{
				_streetAddress = _streetAddress.Substring(_province.Length, _streetAddress.Length - _province.Length);
				
				// 如果是直辖市, 可能会在字符串首留下个"市".
				if (_streetAddress.StartsWith("市"))
					_streetAddress = _streetAddress.Substring(1, _streetAddress.Length - 1);
			}
			
			if (_streetAddress.StartsWith(_city1)) // remove starting _city1.
				_streetAddress = _streetAddress.Substring(_city1.Length, _streetAddress.Length - _city1.Length);

			if (_streetAddress.StartsWith(_city2)) // remove starting _city2.
				_streetAddress = _streetAddress.Substring(_city2.Length, _streetAddress.Length - _city2.Length);

			if (_streetAddress.StartsWith(_district)) // remove starting _district.
				_streetAddress = _streetAddress.Substring(_district.Length, _streetAddress.Length - _district.Length);
		}
		
		private string RemoveStartingProvinceCity(string address, string province, string city1, string city2)
		{
			address = address.Trim();

			// 去掉尾巴上的"省"、"市".
			string shortProvince= province;
			if (province.EndsWith("省") || province.EndsWith("市"))
				shortProvince = province.Substring(0, province.Length - 1); 
			
			while (true)
			{
				if (!string.IsNullOrEmpty(shortProvince) && address.StartsWith(shortProvince))
				{
					address = address.Substring(shortProvince.Length, address.Length - shortProvince.Length).Trim();
				
					if (address.StartsWith("省") || address.StartsWith("市"))
						address = address.Substring(1, address.Length - 1).Trim();
				}
				else if (!string.IsNullOrEmpty(city1) && address.StartsWith(city1))
				{
					address = address.Substring(city1.Length, address.Length - city1.Length).Trim();
				}
				else if (!string.IsNullOrEmpty(city2) && address.StartsWith(city2))
				{
					address = address.Substring(city2.Length, address.Length - city2.Length).Trim();
				}
				else
				{
					break;
				}
			}

			return address;
		}
	}
}