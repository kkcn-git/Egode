using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Xml;
using System.Net;
using System.Threading;
using System.IO;

namespace Egode
{
	public class Address
	{
		private string _type;		// 地址类型. 助记符.
		private string _id;			// 对于平台类型地址, 该地址对应的id.
		private DateTime _datetime;	// 登记时间.
		private string _province;
		private string _city1;
		private string _city2;
		private string _district;
		private string _streetAddress;
		private string _recipient;
		private string _mobile;
		private string _phone;
		private string _postCode;
		private string _comment;
		
		public Address(string type, string id, DateTime datetime, string provice, string city1, string city2, string district, string streetAddress, string recipient, string mobile, string phone, string postCode, string comment)
		{
			_type = type;
			_id = id;
			_datetime = datetime;
			_province = provice;
			_city1 = city1;
			_city2= city2;
			_district = district;
			_streetAddress = streetAddress;
			_recipient = recipient;
			_mobile = mobile;
			_phone = phone;
			_postCode = postCode;
			_comment = comment;
		}

		public string Type
		{
			get { return _type; }
		}
		
		public string Id
		{
			get { return _id; }
		}
		
		public DateTime Datetime
		{
			get { return _datetime; }
		}

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
		
		public string Recipient
		{
			get { return _recipient; }
		}
		
		public string Mobile
		{
			get { return _mobile; }
		}
		
		public string Phone
		{
			get { return _phone; }
		}
		
		public string PostCode
		{
			get { return _postCode; }
		}
		
		public string Comment
		{
			get { return _comment; }
		}
		
		public bool MatchKeyword(string keyword)
		{
			if (_id.ToLower().Contains(keyword.ToLower()))
				return true;
			if (_province.ToLower().Contains(keyword.ToLower()))
				return true;
			if (_city1.ToLower().Contains(keyword.ToLower()))
				return true;
			if (_city2.ToLower().Contains(keyword.ToLower()))
				return true;
			if (_district.ToLower().Contains(keyword.ToLower()))
				return true;
			if (_streetAddress.ToLower().Contains(keyword.ToLower()))
				return true;
			if (_recipient.ToLower().Contains(keyword.ToLower()))
				return true;
			if (_mobile.ToLower().Contains(keyword.ToLower()))
				return true;
			if (_phone.ToLower().Contains(keyword.ToLower()))
				return true;
			if (_postCode.ToLower().Contains(keyword.ToLower()))
				return true;
			if (_comment.ToLower().Contains(keyword.ToLower()))
				return true;

			return false;
		}
	}
	
	public class Addresses : IEnumerable<Address>
	{
		private static Addresses _instance;
		private List<Address> _addresses;
		
		public Addresses()
		{
			_addresses = new List<Address>();
		}
		
		public static Addresses Instance
		{
			get
			{
				if (null == _instance)
					_instance = new Addresses();
				return _instance;
			}
		}

		#region IEnumerable<Address> Members

		public IEnumerator<Address> GetEnumerator()
		{
			return _addresses.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _addresses.GetEnumerator();
		}

		#endregion
		
		// return count of address loaded.
		// <addresses>
		// <a tp="" id="" dt="" pvn="" c1="" c2="" d="" sa="" r="" mb="" ph="" pc="" cm="" />
		// </addresses>
		public int LoadXml(string xml)
		{
			try
			{
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(xml);
				
				XmlNode nodeAddresses = doc.SelectSingleNode(@".//addresses");
				if (null == nodeAddresses)
					return -1;
				
				_addresses.Clear();
				
				XmlNodeList nlAddresses = nodeAddresses.SelectNodes(@".//a");
				foreach (XmlNode nodeAddr in nlAddresses)
				{
					string type = nodeAddr.Attributes.GetNamedItem("tp").Value;
					string id = nodeAddr.Attributes.GetNamedItem("id").Value;
					DateTime datetime = DateTime.Parse(nodeAddr.Attributes.GetNamedItem("dt").Value);
					string province = nodeAddr.Attributes.GetNamedItem("pvn").Value;
					string city1 = nodeAddr.Attributes.GetNamedItem("c1").Value;
					string city2 = nodeAddr.Attributes.GetNamedItem("c2").Value;
					string district = nodeAddr.Attributes.GetNamedItem("d").Value;
					string streetAddr = nodeAddr.Attributes.GetNamedItem("sa").Value;
					string recipient = nodeAddr.Attributes.GetNamedItem("r").Value;
					string mobile = nodeAddr.Attributes.GetNamedItem("mb").Value;
					string phone = nodeAddr.Attributes.GetNamedItem("ph").Value;
					string postCode = nodeAddr.Attributes.GetNamedItem("pc").Value;
					string comment = nodeAddr.Attributes.GetNamedItem("cm").Value;

					Address addr = new Address(type, id, datetime, province, city1, city2, district, streetAddr, recipient, mobile, phone, postCode, comment);
					_addresses.Add(addr);
				}

				return _addresses.Count;
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
				return -1;
			}
		}
		
		public bool Exists(Address addr)
		{
			if (null == addr)
				return false;
			
			foreach (Address a in _addresses)
			{
				if (!a.Recipient.Trim().ToLower().Equals(addr.Recipient.Trim().ToLower()))
					continue;
				if (!a.Mobile.Trim().ToLower().Equals(addr.Mobile.Trim().ToLower()))
					continue;
				if (!a.Phone.Trim().ToLower().Equals(addr.Phone.Trim().ToLower()))
					continue;
				if (!a.StreetAddress.Trim().ToLower().Equals(addr.StreetAddress.Trim().ToLower()))
					continue;
				if (!a.District.Trim().ToLower().Equals(addr.District.Trim().ToLower()))
					continue;
				if (!a.City1.Trim().ToLower().Equals(addr.City1.Trim().ToLower()))
					continue;
				if (!a.City2.Trim().ToLower().Equals(addr.City2.Trim().ToLower()))
					continue;
				if (!a.Province.Trim().ToLower().Equals(addr.Province.Trim().ToLower()))
					continue;

				return true;
			}
			return false;
		}
		
		public bool Add(Address addr)
		{
			if (null == addr)
				return false;
			
			if (Exists(addr))
				return false;
			
			_addresses.Add(addr);
			return true;
		}
		
		public static string AddToServer(Address addr)
		{
			if (null == addr)
				return string.Empty;

			DateTime dt = DateTime.Now;
			string url = string.Format(Common.URL_DATA_CENTER, "newaddr");
			url += string.Format("&tp={0}&id={1}&pvn={2}&c1={3}&c2={4}&d={5}&sa={6}&r={7}&mb={8}&ph={9}&pc={10}&cm={11}", 
				addr.Type, addr.Id,  
				addr.Province, addr.City1, addr.City2, addr.District, addr.StreetAddress, 
				addr.Recipient, addr.Mobile, addr.Phone, addr.PostCode, addr.Comment);
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
			request.Method = "GET";
			request.ContentType = "text/xml";
			WebResponse response = request.GetResponse();
			StreamReader reader = new StreamReader(response.GetResponseStream());
			string result = reader.ReadToEnd();
			reader.Close();

			return result;
		}
		
		private static void RespCallback(IAsyncResult asynchronousResult)
		{
			// do nothing.
		}
	}
}