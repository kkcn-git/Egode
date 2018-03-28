using System;
using System.Collections.Generic;
using System.Text;

namespace Dangdang
{
	// 输入: 中国湖北黄冈市黄州区湖北省黄冈市黄州区西湖二路领秀西湖九洲房地产评估所
	//		中国湖北黄冈市黄州区西湖一路 福星花园 7栋1单元701室
	// 输出: Province, city, district, address
	public class DangdangAddressParser
	{
		private string[] _provinces = new string[] { 
			"北京市", "上海市", "天津市", "重庆市", 
			"江苏省", "浙江省", "安徽省", 
			"山东省", "山西省", "广东省", "广西省", "河南省", "河北省", "湖南省", "湖北省", 
			"江西省", "福建省", "陕西省", "海南省", "四川省", "贵州省", "云南省", "宁夏省", "甘肃省", "青海省", 
			"黑龙江省", "吉林省", "辽宁省", "内蒙古省", "新疆省", "西藏省"};
	
		private string _province;
		private string _city;
		private string _district;
		private string _streetAddress;

		public DangdangAddressParser(string fullAddress)
		{
			if (fullAddress.StartsWith("中国"))
				fullAddress = fullAddress.Remove(0, 2);
			
			// get province.
			foreach (string province in _provinces)
			{
				string provinceWithoutPostfix = province.Substring(0, province.Length-1); // remove ending Province or City.
			
				if (fullAddress.StartsWith(provinceWithoutPostfix))
				{
					_province = province;
					fullAddress = fullAddress.Remove(0, provinceWithoutPostfix.Length);
					//if (fullAddress.StartsWith(province)) // 通常是直辖市, 市名会连续出现2次.
					//    fullAddress = fullAddress.Remove(0, province.Length);
					break;
				}
			}
			
			// get city.
			int index = fullAddress.IndexOf("市");
			if (index > 0)
			{
				_city = fullAddress.Substring(0, index + 1);
				fullAddress = fullAddress.Remove(0, index + 1);
			}
			else
			{
			}
			
			// get district.
			int index1 = fullAddress.IndexOf("区");
			int index2 = fullAddress.IndexOf("县");
			int index3 = fullAddress.IndexOf("市");
			if (-1 == index1)
				index1 = 99999;
			if (-1 == index2)
				index2 = 99999;
			if (-1 == index3)
				index3 = 99999;
			index = Math.Min(Math.Min(index1, index2), index3);
			_district = fullAddress.Substring(0, index+1);
			fullAddress = fullAddress.Remove(0, index + 1);
			
			// 去掉StreetAddress中可能重复出现的省市区信息.
			if (fullAddress.StartsWith(_province))
				fullAddress = fullAddress.Remove(0, _province.Length);
			if (fullAddress.StartsWith(_city))
				fullAddress = fullAddress.Remove(0, _city.Length);
			if (fullAddress.StartsWith(_district))
				fullAddress = fullAddress.Remove(0, _district.Length);
			
			_streetAddress = fullAddress;
		}
		
		public string Province
		{
			get { return _province; }
		}
		
		public string City
		{
			get { return _city; }
		}
		
		public string District
		{
			get { return _district; }
		}
		
		public string StreetAddress
		{
			get { return _streetAddress; }
		}
	}
}