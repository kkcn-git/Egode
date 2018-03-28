using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Egode
{
	class BrandInfo
	{
		private readonly string _id;
		private readonly string _name;
		private static List<BrandInfo> _brands;
		
		public BrandInfo(string id, string name)
		{
			_id = id;
			_name = name;
		}
		
		public string Id
		{
			get { return _id; }
		}
		
		public string Name
		{
			get { return _name; }
		}

		public override string ToString()
		{
			return _name;
		}
		
		public static List<BrandInfo> Brands
		{
			get 
			{ 
				if (null == _brands)
					_brands = new List<BrandInfo>();
				return _brands; 
			}
		}
		
		// xml: xml string downloaded from server.
		public static void InitializeBrands(string xml)
		{
			if (string.IsNullOrEmpty(xml))
				return;
		
			XmlDocument xmldoc = new XmlDocument();
			xmldoc.LoadXml(xml);

			// brands
			XmlNodeList nlBrands = xmldoc.SelectNodes(".//brand");
			if (null == nlBrands || nlBrands.Count <= 0)
				return;

			foreach (XmlNode nodeBrand in nlBrands)
			{
				string id = nodeBrand.Attributes.GetNamedItem("id").Value;
				string name = nodeBrand.Attributes.GetNamedItem("name").Value;
				BrandInfo.Brands.Add(new BrandInfo(id, name));
			}
		}
		
		public static BrandInfo GetBrand(string id)
		{
			if (null == _brands || _brands.Count <= 0)
				return null;
			
			foreach (BrandInfo b in _brands)
			{
				if (b.Id.Equals(id))
					return b;
			}
			return null;
		}
	}
}