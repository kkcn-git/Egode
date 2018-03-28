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

		// fullAddress: like: �㽭ʡ ������ ������ �㽭ʡ�����к����������Ŵ�����·22��(000000)
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

				if (ss.EndsWith("ʡ") || ss.EndsWith("������") || ss.Equals("����") || ss.Equals("�Ϻ�") || ss.Equals("���") || ss.Equals("����"))
					_province = ss;
				if (ss.Length <= 6 && (ss.EndsWith("��") || ss.EndsWith("��") || ss.EndsWith("������")) && !ss.StartsWith(_province))
				{
					if (string.IsNullOrEmpty(_city1))
						_city1 = ss;
					else
						_city2 = ss;
				}
			}
			if (!_province.EndsWith("ʡ") && !_province.EndsWith("��") && !_province.EndsWith("������")) // ֱϽ��or������.
				_province += "��";
			
			// Get _district;
			_streetAddress = RemoveStartingProvinceCity(fullAddress, _province, _city1, _city2);
			string[] streetAddressInfos = _streetAddress.Split(' '); // �˴�������пո�ָ�, ����Ϊ��1���ַ�������. �����������ڽֵ���ַ��Ҳ�����˿ո�, ���ܳ���.
			_district = string.Empty;
			if (streetAddressInfos.Length >= 2 && (streetAddressInfos[0].EndsWith("��") || streetAddressInfos[0].EndsWith("��")))
				_district = streetAddressInfos[0];
			if (!string.IsNullOrEmpty(_district))
				_streetAddress = _streetAddress.Replace(_district, string.Empty).Trim();

			// ����, �õ���ȥ����ǰ���ʡ�����ĵ�ַ, Ҳ����������Ա����ֶ�����Ĳ���. 
			// ��Ҫȥ����ҿ����ظ������ʡ������Ϣ.
			if (_streetAddress.StartsWith(_province)) // remove starting _province.
			{
				_streetAddress = _streetAddress.Substring(_province.Length, _streetAddress.Length - _province.Length);
				
				// �����ֱϽ��, ���ܻ����ַ��������¸�"��".
				if (_streetAddress.StartsWith("��"))
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

			// ȥ��β���ϵ�"ʡ"��"��".
			string shortProvince= province;
			if (province.EndsWith("ʡ") || province.EndsWith("��"))
				shortProvince = province.Substring(0, province.Length - 1); 
			
			while (true)
			{
				if (!string.IsNullOrEmpty(shortProvince) && address.StartsWith(shortProvince))
				{
					address = address.Substring(shortProvince.Length, address.Length - shortProvince.Length).Trim();
				
					if (address.StartsWith("ʡ") || address.StartsWith("��"))
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