using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Egode
{
	class Distributor
	{
		private string _id;
		private string _name;
		private string _ad;
		private string _tel;
		
		private static List<Distributor> _distributors;
		
		public Distributor(string id, string name, string ad, string tel)
		{
			_id = id;
			_name = name;
			_ad = ad;
			_tel = tel;
		}
		
		public string Id
		{
			get { return _id; }
		}
		
		public string Name
		{
			get { return _name; }
		}
		
		public string Ad
		{
			get { return _ad; }
		}
		
		public string Tel
		{
			get { return _tel; }
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (string.IsNullOrEmpty(_id))
				sb.Append("<Пе>");
			else
				sb.Append(_id);
			sb.Append("\\");
			
			if (string.IsNullOrEmpty(_name))
				sb.Append("<Пе>");
			else
				sb.Append(_name);
			sb.Append("\\");

			if (string.IsNullOrEmpty(_ad))
				sb.Append("<Пе>");
			else
				sb.Append(_ad);
			sb.Append("\\");

			if (string.IsNullOrEmpty(_tel))
				sb.Append("<Пе>");
			else
				sb.Append(_tel);
			
			return sb.ToString();
		}
		
		public static List<Distributor> Distributors
		{
			get
			{
				if (null == _distributors)
					_distributors = new List<Distributor>();
				return _distributors;
			}
		}

		// xml: xml string downloaded from server.
		public static void InitializeDistributors(string xml)
		{
			if (string.IsNullOrEmpty(xml))
				return;

			XmlDocument xmldoc = new XmlDocument();
			xmldoc.LoadXml(xml);

			// distributors.
			XmlNodeList nlDistributors = xmldoc.SelectNodes(".//distributor");
			if (null == nlDistributors || nlDistributors.Count <= 0)
				return;

			foreach (XmlNode nodeDistributor in nlDistributors)
			{
				string id = nodeDistributor.Attributes.GetNamedItem("id").Value;
				string name = nodeDistributor.Attributes.GetNamedItem("name").Value;
				string ad = nodeDistributor.Attributes.GetNamedItem("ad").Value;
				string tel = nodeDistributor.Attributes.GetNamedItem("tel").Value;
				Distributor.Distributors.Add(new Distributor(id, name, ad, tel));
			}
		}
		
		public static Distributor Match(string id)
		{
			foreach (Distributor d in Distributors)
			{
				if (d.Id.ToLower().Trim().Equals(id.ToLower().Trim()))
					return d;
			}
			return null;
		}
	}
}