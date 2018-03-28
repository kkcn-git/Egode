using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using OrderLib;

namespace Egode
{
	public class PacketInfo : Packet
	{
		private readonly string _orderId;
		private string _fullAddress; // contains province, city, district, addrss and post code.
		private string _phoneNumber;
		private string _homePhoneNumber;
		private string _recipientNameEn;
		private string _provinceCityEn;
		private string _addressEn;
		private string _recipientNameCn;
		private string _provinceCityCn;
		private string _addressCn;
		private string _postCode;
		
		private string _buyerRemark;
		private string _remark;
		
		private string _productLongString; // 
		private List<SoldProductInfo> _products;
		
		private List<ExportItem> _exportItems;

		public PacketInfo(
			string orderId, 
			PacketTypes packetType, int weight, int price,
			string fullAddress, string phoneNumber, string homePhoneNumber,
			string productLongString) : base(packetType, weight, price)
		{
			_exportItems = new List<ExportItem>();
		
			_orderId = orderId;
			_fullAddress = fullAddress;
			_phoneNumber = phoneNumber;
			_homePhoneNumber = homePhoneNumber;
			_productLongString = productLongString;
		}
		
		public PacketInfo Clone()
		{
			PacketInfo pi = new PacketInfo(_orderId, base.Type, base.Weight, base.Price, _fullAddress, _phoneNumber, _homePhoneNumber, _productLongString);
			pi._recipientNameCn = _recipientNameCn;
			pi._recipientNameEn = _recipientNameEn;
			pi._provinceCityCn = _provinceCityCn;
			pi._provinceCityEn = _provinceCityEn;
			pi._addressCn = _addressCn;
			pi._addressEn = _addressEn;
			pi._postCode = _postCode;
			pi._buyerRemark = _buyerRemark;
			pi._remark = _remark;
			
			return pi;
		}
		
		// Added by KK on 2015/07/10.
		// 对于多个订单合并出单, 需要以某个订单为主订单, 通常是正在出单的订单, 然后把其他订单的商品信息添加到此包裹单信息中,
		// 即直接把其他订单的表示商品信息的productLongString添加到主订单的此字符串上即可. 后续即会自动解析出这些订单包含的所有商品信息.
		public void AppendProductLongString(string productLongString)
		{
			if (!productLongString.StartsWith("★"))
				_productLongString += "★";
			_productLongString += productLongString;
		}

		public List<SoldProductInfo> Products
		{
			get
			{
				if (null == _products)
				{
					Order o = MainForm.Instance.GetOrder(_orderId);
					if (null != o)
					{
						List<Order> l = new List<Order>();
						l.Add(o);
						int c1, c2;
						_products = ConsignShForm.GetProducts(l, out c1, out c2, true);
					}

					/*
					_products = new List<SoldProductInfo>();

					string[] itemInfos = _productLongString.Split('★');
					for (int i = 0; i < itemInfos.Length; i++)
					{
						string itemInfo = itemInfos[i];
						string[] infos = itemInfo.Split('☆');
						if (infos.Length < 3)
							continue;

						if (infos[0].Contains("加强包装费"))
							continue;
						if (infos[0].Contains("气囊加固"))
							continue;

						ProductInfo p = ProductInfo.Match(infos[0], _remark);
						if (null == p)
							continue;
						SoldProductInfo product = new SoldProductInfo(p);

						product.Count = int.Parse(infos[2]);
						product.Status = (Order.OrderStatus)Enum.Parse(typeof(Order.OrderStatus), infos[3]);
						_products.Add(product);

						for (int c = 16; c > 0; c--)
						{
							if (infos[0].Contains(string.Format("{0}盒装", c)) || infos[0].Contains(string.Format("{0}罐装", c)))
							{
								product.Count *= c;
								break;
							}
						}
					}
					*/
				}

				return _products;
			}
		}
		
		// 把拼音地址, 不包含省市, 从区开始, 分成最多3段:
		// 第1段最多不超过40个字符, 且不截断1个汉字的完整拼音;
		// 第2段最多不超过40个字符, 且不截断1个汉字的完整拼音;
		// 第3段最多不超过40个字符, 且不截断1个汉字的完整拼音;
		public string[] GetAddressEnSegments()
		{
			//_addressEn = "JiangXiShengNanChangShiXiHuQuRuZiLuDianXinDaLou#120-2-102";
			string[] segments = new string[3];
			char[] addressEnCharArray = _addressEn.ToCharArray();
			
			// segment 1. max length=40
			if (_addressEn.Length <= 40)
			{
				// segment 2必须有内容.
				for (int i = _addressEn.Length-1; i>=_addressEn.Length-10; i--)
				{
					if (!IsLowerCase(addressEnCharArray[i]) && IsLowerCase(addressEnCharArray[i-1]))
					{
						segments[0] = _addressEn.Substring(0, i);
						segments[1] = _addressEn.Substring(i, _addressEn.Length-i);
						break;
					}
				}
				if (string.IsNullOrEmpty(segments[0]))
				{
					segments[0] = _addressEn.Substring(0, _addressEn.Length-10);
					segments[1] = _addressEn.Substring(_addressEn.Length - 10, 10);
				}
				return segments;
			}
			if (!IsLowerCase(addressEnCharArray[40]) && IsLowerCase(addressEnCharArray[39])) // 正好第41个字符为非小写, 前40个字符为完整的n个汉字拼音, 直接截取.
			{
				segments[0] = _addressEn.Substring(0, 40);
			}
			else // 截取前40个字符中完整汉字拼音. 也就是最后1个非小写字母前面的部分.
			{
				for (int i = 39; i >= 0; i--)
				{
					if (!IsLowerCase(addressEnCharArray[i]) && IsLowerCase(addressEnCharArray[i-1]))
					{
						segments[0] = _addressEn.Substring(0, i);
						break;
					}
				}
			}
			
			// segment 2. max length=10
			if (_addressEn.Length <= segments[0].Length + 10)
			{
				segments[1] = _addressEn.Substring(segments[0].Length, _addressEn.Length - segments[0].Length);
				return segments;
			}
			if (!IsLowerCase(addressEnCharArray[segments[0].Length + 10]) && IsLowerCase(addressEnCharArray[segments[0].Length + 9]))
			{
				segments[1] = _addressEn.Substring(segments[0].Length, 10);
			}
			else
			{
				for (int i = segments[0].Length + 9; i >= segments[0].Length; i--)
				{
					if (!IsLowerCase(addressEnCharArray[i]) && IsLowerCase(addressEnCharArray[i-1]))
					{
						segments[1] = _addressEn.Substring(segments[0].Length, i - segments[0].Length);
						break;
					}
				}
			}
			
			// segment 3. max length=30;
			// 第3段不做拼音完整性处理. 直接截取最多30个字符.	
			if (_addressEn.Length > segments[0].Length + segments[1].Length)
				segments[2] = _addressEn.Substring(segments[0].Length+segments[1].Length, Math.Min(30, _addressEn.Length-segments[0].Length-segments[1].Length));
			
			return segments;
		}
		
		private static bool IsLowerCase(char c)
		{
			return (c >= 'a' && c <= 'z');
		}
		
		public string OrderId
		{
			get { return _orderId; }
		}
		
		public string FullAddress
		{
			get { return _fullAddress; }
			set { _fullAddress = value; }
		}

		public string PhoneNumber
		{
			get { return _phoneNumber; }
			set { _phoneNumber = value; }
		}
		
		public string HomePhoneNumber
		{
			get { return _homePhoneNumber; }
			set { _homePhoneNumber = value; }
		}

		public string RecipientNameEn
		{
			get { return _recipientNameEn;}
			set { _recipientNameEn = value; }
		}
		
		public string ProvinceCityEn
		{
			get { return _provinceCityEn; }
			set { _provinceCityEn = value; }
		}
		
		public string AddressEn
		{
			get { return _addressEn; }
			set { _addressEn = value; }
		}

		public string RecipientNameCn
		{
			get { return _recipientNameCn;}
			set { _recipientNameCn = value; }
		}
		
		public string ProvinceCityCn
		{
			get { return _provinceCityCn; }
			set { _provinceCityCn = value; }
		}
		
		public string AddressCn
		{
			get { return _addressCn; }
			set { _addressCn = value; }
		}
		
		public string PostCode
		{
			get { return _postCode; }
			set { _postCode = value; }
		}
		
		public string ProductLongString
		{
			get { return _productLongString; }
			set { _productLongString = value; }
		}
		
		public string BuyerRemark
		{
			get { return _buyerRemark; }
			set { _buyerRemark = value; }
		}
		
		public string Remark
		{
			get { return _remark; }
			set { _remark = value; }
		}
		
		public List<ExportItem> ExportItems
		{
			get { return _exportItems; }
		}
		
		public float GetTotalExportMoneyAmount()
		{
			if (null == _exportItems || _exportItems.Count <= 0)
				return 0.0f;
			
			float money = 0;
			foreach (ExportItem ei in _exportItems)
			{
				//Aptamil Milk Powder|12|118.56|7.2|8.5
				string[] fileds = ei.ExportItemString.Split('|');
				money += float.Parse(fileds[2]);
			}
			
			return money;
		}
	}

	public class ExportItem
	{
		private string _exportItemString;
		private static List<ExportItem> _exportItems;
		
		public ExportItem(string exportItemString)
		{
			_exportItemString = exportItemString;
		}
		
		public string ExportItemString
		{
			get { return _exportItemString; }
		}
		
		public static List<ExportItem> ExportItems
		{
			get
			{
				if (null == _exportItems)
				{
					_exportItems = new List<ExportItem>();
					string filename = Path.Combine(Directory.GetParent(System.Windows.Forms.Application.ExecutablePath).FullName, "export-items.txt");
					StreamReader reader = new StreamReader(filename);
					while (!reader.EndOfStream)
					{
						string s = reader.ReadLine().Trim();
						if (string.IsNullOrEmpty(s))
							continue;
						_exportItems.Add(new ExportItem(s));
					}
				}
				
				return _exportItems;
			}
		}
	}
}