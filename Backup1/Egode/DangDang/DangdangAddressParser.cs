using System;
using System.Collections.Generic;
using System.Text;

namespace Dangdang
{
	// ����: �й������Ƹ��л���������ʡ�Ƹ��л�����������·�����������޷��ز�������
	//		�й������Ƹ��л���������һ· ���ǻ�԰ 7��1��Ԫ701��
	// ���: Province, city, district, address
	public class DangdangAddressParser
	{
		private string[] _provinces = new string[] { 
			"������", "�Ϻ���", "�����", "������", 
			"����ʡ", "�㽭ʡ", "����ʡ", 
			"ɽ��ʡ", "ɽ��ʡ", "�㶫ʡ", "����ʡ", "����ʡ", "�ӱ�ʡ", "����ʡ", "����ʡ", 
			"����ʡ", "����ʡ", "����ʡ", "����ʡ", "�Ĵ�ʡ", "����ʡ", "����ʡ", "����ʡ", "����ʡ", "�ຣʡ", 
			"������ʡ", "����ʡ", "����ʡ", "���ɹ�ʡ", "�½�ʡ", "����ʡ"};
	
		private string _province;
		private string _city;
		private string _district;
		private string _streetAddress;

		public DangdangAddressParser(string fullAddress)
		{
			if (fullAddress.StartsWith("�й�"))
				fullAddress = fullAddress.Remove(0, 2);
			
			// get province.
			foreach (string province in _provinces)
			{
				string provinceWithoutPostfix = province.Substring(0, province.Length-1); // remove ending Province or City.
			
				if (fullAddress.StartsWith(provinceWithoutPostfix))
				{
					_province = province;
					fullAddress = fullAddress.Remove(0, provinceWithoutPostfix.Length);
					//if (fullAddress.StartsWith(province)) // ͨ����ֱϽ��, ��������������2��.
					//    fullAddress = fullAddress.Remove(0, province.Length);
					break;
				}
			}
			
			// get city.
			int index = fullAddress.IndexOf("��");
			if (index > 0)
			{
				_city = fullAddress.Substring(0, index + 1);
				fullAddress = fullAddress.Remove(0, index + 1);
			}
			else
			{
			}
			
			// get district.
			int index1 = fullAddress.IndexOf("��");
			int index2 = fullAddress.IndexOf("��");
			int index3 = fullAddress.IndexOf("��");
			if (-1 == index1)
				index1 = 99999;
			if (-1 == index2)
				index2 = 99999;
			if (-1 == index3)
				index3 = 99999;
			index = Math.Min(Math.Min(index1, index2), index3);
			_district = fullAddress.Substring(0, index+1);
			fullAddress = fullAddress.Remove(0, index + 1);
			
			// ȥ��StreetAddress�п����ظ����ֵ�ʡ������Ϣ.
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