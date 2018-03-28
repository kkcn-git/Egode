using System;
using System.Collections.Generic;
using System.Text;

namespace Egode
{
	public class ProductInfo
	{
		private string _id;
		private string _brandId;
		private string _name;
		private string _shortName;
		private string _keywords;
		
		private static List<ProductInfo> _productInfos;
		
		public ProductInfo(string id, string brandId, string name, string shortName, string keywords)
		{
			_id = id;
			_brandId = brandId;
			_name = name;
			_shortName = shortName;
			_keywords = keywords;
		}
		
		public string Id
		{
			get { return _id; }
		}
		
		public string BrandId
		{
			get { return _brandId; }
		}
		
		public string Name
		{
			get { return _name; }
		}
		
		public string ShortName
		{
			get { return _shortName; }
		}
		
		public string Keywords
		{
			get { return _keywords; }
		}
		
		public static List<ProductInfo> ProductInfos
		{
			get 
			{
				if (null == _productInfos)
					_productInfos = new List<ProductInfo>();
				return _productInfos;
			}
		}
		
		public static ProductInfo GetProductInfo(string id)
		{
			if (null == _productInfos)
				return null;
			
			foreach (ProductInfo pi in _productInfos)
			{
				if (pi.Id.Equals(id))
					return pi;
			}
			
			return null;
		}
		
		public static ProductInfo Match(string productTitle)
		{
			if (null == _productInfos)
				return null;
			
			foreach (ProductInfo pi in _productInfos)
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
				
				if (bingo)
					return pi;
			}
			
			return null;
		}
	}

	public class SoldProductInfo : ProductInfo
	{
		private int _count;
		private string _comment;
		
		public SoldProductInfo(string id, string brandId, string name, string shortName, string keywords, int count) : base(id, brandId, name, shortName, keywords)
		{
			_count = count;
		}
		
		public int Count
		{
			get { return _count; }
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
