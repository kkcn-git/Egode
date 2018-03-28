using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using OrderLib;

namespace Egode
{
	public class ProductInfo
	{
		private string _id;
		private string _brandId;
		private string _skuCode; // Added by KK on 2016/06/06.
		private string _ningboId;
		private string _dangdangCode;
		private string _name;
		float _price;
		string _specification;
		private string _shortName;
		private string _keywords;
		private bool _conflict; // 是否存在子型号.

		private static List<ProductInfo> _products;

		public ProductInfo(string id, string brandId, string skuCode, string ningboId, string dangdangCode, string name, float price, string specification, string shortName, string keywords, bool conflict)
		{
			_id = id;
			_brandId = brandId;
			_skuCode = skuCode;
			_ningboId = ningboId;
			_dangdangCode = dangdangCode;
			_name = name;
			_price = price;
			_specification = specification;
			_shortName = shortName;
			_keywords = keywords;
			_conflict = conflict;
		}
		
		public string Id
		{
			get { return _id; }
		}
		
		public string BrandId
		{
			get { return _brandId; }
		}
		
		public string SkuCode
		{
			get { return _skuCode; }
		}
		
		public string NingboId
		{
			get { return _ningboId; }
		}
		
		public string DangdangCode
		{
			get { return _dangdangCode; }
		}
		
		public string Name
		{
			get { return _name; }
		}
		
		public float Price
		{
			get { return _price; }
		}
		
		public string Specification
		{
			get { return _specification; }
		}
		
		// Get weight from specification.
		// If the specification is weight, the format of string of specificationi must be: 800g.
		public float Weight
		{
			get
			{
				if (string.IsNullOrEmpty(_specification))
					return 0.0f;
					
				string w = _specification.Trim();
				if (!w.EndsWith("g"))
					return 0.0f;
				
				w = w.Remove(w.Length - 1, 1);
				float f = 0.0f;
				float.TryParse(w, out f);
				return f;
			}
		}
		
		public string ShortName
		{
			get { return _shortName; }
		}
		
		public string Keywords
		{
			get { return _keywords; }
		}

		public bool Conflict
		{
			get { return _conflict; }
		}

		public override string ToString()
		{
			return _name;
		}
		
		// xml: xml string downloaded from server.
		public static void InitializeProducts(string xml)
		{
			if (string.IsNullOrEmpty(xml))
				return;

			XmlDocument xmldoc = new XmlDocument();
			xmldoc.LoadXml(xml);

			// products.
			XmlNodeList nlProducts = xmldoc.SelectNodes(".//product");
			if (null == nlProducts || nlProducts.Count <= 0)
				return;

			foreach (XmlNode nodeProduct in nlProducts)
			{
				//try
				//{
					string id = nodeProduct.Attributes.GetNamedItem("id").Value;
					string brand = nodeProduct.Attributes.GetNamedItem("brand").Value;
					string skuCode = nodeProduct.Attributes.GetNamedItem("sku_code").Value;
					string ningboId = nodeProduct.Attributes.GetNamedItem("nb_code").Value;
					string dangdangCode = nodeProduct.Attributes.GetNamedItem("dd_code").Value;
					string name = nodeProduct.Attributes.GetNamedItem("name").Value;
					string specification = nodeProduct.Attributes.GetNamedItem("specification").Value;
					string shortname = nodeProduct.Attributes.GetNamedItem("shortname").Value;
					string keywords = nodeProduct.Attributes.GetNamedItem("keywords").Value;
					bool conflict = bool.Parse(nodeProduct.Attributes.GetNamedItem("conflict").Value);
					float price = 0;
					float.TryParse(nodeProduct.Attributes.GetNamedItem("price").Value, out price);
					ProductInfo.Products.Add(new ProductInfo(id, brand, skuCode, ningboId, dangdangCode, name, price, specification, shortname, keywords, conflict));
				//}
				//catch (Exception ex)
				//{
				//    System.Diagnostics.Trace.WriteLine(ex);
				//}
			}
		}
		
		public static List<ProductInfo> Products
		{
			get 
			{
				if (null == _products)
					_products = new List<ProductInfo>();
				return _products;
			}
		}
		
		public static ProductInfo GetProductInfo(string id)
		{
			if (null == _products)
				return null;
			
			foreach (ProductInfo pi in _products)
			{
				if (pi.Id.Equals(id))
					return pi;
			}
			
			return null;
		}

		public static ProductInfo GetProductByNingboCode(string ningboId)
		{
			if (string.IsNullOrEmpty(ningboId))
				return null;
			if (null == _products)
				return null;

			foreach (ProductInfo pi in _products)
			{
				if (pi.NingboId.Equals(ningboId))
					return pi;
			}
			return null;
		}

		public static ProductInfo GetProductByDangdangCode(string dangdangCode)
		{
			if (string.IsNullOrEmpty(dangdangCode))
				return null;
			if (null == _products)
				return null;

			foreach (ProductInfo pi in _products)
			{
				if (pi.DangdangCode.Equals(dangdangCode))
					return pi;
			}
			return null;
		}
		
		// Added by KK on 2016/06/06.
		public static ProductInfo GetProductBySkuCode(string skuCode)
		{
			if (string.IsNullOrEmpty(skuCode))
				return null;
			if (null == _products)
				return null;

			foreach (ProductInfo pi in _products)
			{
				if (pi.SkuCode.Equals(skuCode))
					return pi;
			}
			return null;
		}

		public static ProductInfo Match(string productTitle, string extraInfo)
		{
			if (null == _products)
				return null;

			int matchedKeywordCount = 0;
			ProductInfo matchedProduct = null; 
			
			foreach (ProductInfo pi in _products)
			{
				if (string.IsNullOrEmpty(pi.Keywords))
					continue;

				bool bingo = true;
				string[] keywords = pi.Keywords.Split(',');
				foreach (string kw in keywords)
				{
					if (!productTitle.Trim().ToLower().Contains(kw.Trim().ToLower()))
					{
						bingo = false;
						break;
					}
				}
				
				if (bingo & keywords.Length > matchedKeywordCount) // 找出匹配关键字最多的product.
				{
					matchedKeywordCount = keywords.Length;
					matchedProduct = pi;
				}
			}

			return matchedProduct;
		}
	}

	public class SoldProductInfo : ProductInfo
	{
		private int _count;
		private string _comment;
		private string _taobaoCode; // code in taobao system.
		private Order.OrderStatus _status;
		
		public SoldProductInfo(string id, string brandId, string skuCode, string ningboId, string dangdangCode, string name, float price, string specification, string shortName, string keywords, bool conflict, int count, Order.OrderStatus status, string taobaoCode) : base(id, brandId, skuCode, ningboId, dangdangCode, name, price, specification, shortName, keywords, conflict)
		{
			_count = count;
			_status = status;
			_taobaoCode = taobaoCode;
		}
		
		public SoldProductInfo(ProductInfo pi) : base(pi.Id, pi.BrandId, pi.SkuCode, pi.NingboId, pi.DangdangCode, pi.Name, pi.Price, pi.Specification, pi.ShortName, pi.Keywords, pi.Conflict)
		{
		}
		
		public int Count
		{
			get { return _count; }
			set { _count = value; }
		}
		
		public Order.OrderStatus Status
		{
			get { return _status; }
			set { _status = value; }
		}
		
		public string TaobaoCode
		{
			get { return _taobaoCode; }
		}
		
		public void AddCount(int c)
		{
			_count += c;
		}
		
		public string Comment
		{
			get { return _comment; }
			set { _comment = value; }
		}

		//public static string GetProductId(string productTitle)
		//{
		//    if (productTitle.Contains("爱他美"))
		//    {
		//        if (productTitle.ToLower().Contains("pre"))
		//            return "001-0001";
		//        else if (productTitle.Contains("1段"))
		//            return "001-0002";
		//        else if (productTitle.Contains("2段"))
		//            return "001-0003";
		//        else if (productTitle.Contains("3段"))
		//            return "001-0004";
		//        else if (productTitle.Contains("1+"))
		//            return "001-0005";
		//        else if (productTitle.Contains("2+"))
		//            return "001-0006";
		//    }
			
		//    if (productTitle.Contains("喜宝"))
		//    {
		//        if (productTitle.Contains("益生菌"))
		//        {
		//            if (productTitle.ToLower().Contains("pre"))
		//                return "002-0006";
		//            else if (productTitle.Contains("1段"))
		//                return "002-0007";
		//            else if (productTitle.Contains("2段"))
		//                return "002-0008";
		//            else if (productTitle.Contains("3段"))
		//                return "002-0009";
		//            else if (productTitle.Contains("4段"))
		//                return "002-0010";
		//            else if (productTitle.Contains("2+"))
		//                return "002-0011";
		//        }

		//        if (productTitle.ToLower().Contains("pre"))
		//            return "002-0001";
		//        if (productTitle.Contains("1段"))
		//            return "002-0002";
		//        if (productTitle.Contains("2段"))
		//            return "002-0003";
		//        if (productTitle.Contains("3段"))
		//            return "002-0004";
		//        if (productTitle.Contains("4段"))
		//            return "002-0005";

		//        if (productTitle.Contains("#2920"))
		//            return "002-0012";
		//        if (productTitle.Contains("#2830"))
		//            return "002-0014";
		//        if (productTitle.Contains("#2890"))
		//            return "002-0015";
		//        if (productTitle.Contains("#3461"))
		//            return "002-0018";
		//        if (productTitle.Contains("#3511"))
		//            return "002-0020";
		//        if (productTitle.Contains("#2951"))
		//            return "002-0022";
		//        if (productTitle.Contains("#3171"))
		//            return "002-0023";
		//        if (productTitle.Contains("#3501"))
		//            return "002-0024";
		//        if (productTitle.Contains("#3451"))
		//            return "002-0025";
		//        if (productTitle.Contains("#3555"))
		//            return "002-0026";
		//        if (productTitle.Contains("#3471"))
		//            return "002-0027";
		//        if (productTitle.Contains("#5430"))
		//            return "002-0029";
		//        if (productTitle.Contains("#3551"))
		//            return "002-0031";
		//        if (productTitle.Contains("#3441"))
		//            return "002-0033";
		//        if (productTitle.Contains("#6105"))
		//            return "002-0034";
		//        if (productTitle.Contains("#6010"))
		//            return "002-0035";
		//        if (productTitle.Contains("#6020"))
		//            return "002-0036";
		//    }

		//    if (productTitle.Contains("D-Fluoretten 500"))
		//        return "900-0001";
		//    if (productTitle.Contains("Zymafluor") && productTitle.Contains("D500"))
		//        return "900-0017";
		//    if (productTitle.Contains("Vigantoletten") && productTitle.Contains("500") && productTitle.Contains("50天"))
		//        return "900-0018";
		//    if (productTitle.Contains("Vigantoletten") && productTitle.Contains("500") && productTitle.Contains("90天"))
		//        return "900-0020";
		//    if (productTitle.Contains("Vigantoletten") && productTitle.Contains("1000"))
		//        return "900-0002";
			

		//    return string.Empty;
		//}
		
		//public static string GetProductDesc(string id)
		//{
		//    switch (id)
		//    {
		//        case "001-0001":
		//            return "Aptamil Pre";
		//            break;
		//        case "001-0002":
		//            return "Aptamil 1";
		//            break;
		//        case "001-0003":
		//            return "Aptamil 2";
		//            break;
		//        case "001-0004":
		//            return "Aptamil 3";
		//            break;
		//        case "001-0005":
		//            return "Aptamil 1+";
		//            break;
		//        case "001-0006":
		//            return "Aptamil 2+";
		//            break;
		//        case "002-0001":
		//            return "HiPP Pre";
		//            break;
		//        case "002-0002":
		//            return "HiPP 1";
		//            break;
		//        case "002-0003":
		//            return "HiPP 2";
		//            break;
		//        case "002-0004":
		//            return "HiPP 3";
		//            break;
		//        case "002-0005":
		//            return "HiPP 4/12+";
		//            break;
		//        case "002-0006":
		//            return "HiPP益生菌 Pre";
		//            break;
		//        case "002-0007":
		//            return "HiPP益生菌 1";
		//            break;
		//        case "002-0008":
		//            return "HiPP益生菌 2";
		//            break;
		//        case "002-0009":
		//            return "HiPP益生菌 3";
		//            break;
		//        case "002-0010":
		//            return "HiPP益生菌 4/12+";
		//            break;
		//        case "002-0011":
		//            return "HiPP益生菌 2+";
		//            break;
		//    }
			
		//    return string.Empty;
		//}
	}
}
