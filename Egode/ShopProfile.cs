using System;
using System.Collections.Generic;
using System.Text;

namespace Egode
{
	public enum ShopEnum
	{
		Egode = 0,
		EgoGermany = 1,
		First = 2
	}

	class ShopProfile
	{
		private ShopEnum _shop;
		private string _account;
		private string _subAccount;
		private string _sellerId;
		private string _pw;
		private string _displayName;
		private string _displayNameOnBill;
		private string _shortName;
		private string _senderName; // sender name displayed on shipment bill.
		private string _senderPhone; // sender phone displayed on shipment bill.
		private string _ad; // some ad information displayed on shipment bill.
		private string _ordersDbFilename;
		private string _fullOrdersDbFilename;
		
		private static List<ShopProfile> _shops;
		private static ShopProfile _current;
		
		private ShopProfile(
			ShopEnum shop, string account, string sellerId, 
			string displayName, string displayNameOnBill, string shortName, 
			string senderName, string senderPhone, string ad, 
			string ordersDbFilename, string fullOrdersDbFilename)
		{
			_shop = shop;
			_account = account;
			_sellerId = sellerId;
			_displayName = displayName;
			_displayNameOnBill = displayNameOnBill;
			_shortName = shortName;
			_senderName = senderName;
			_senderPhone = senderPhone;
			_ad = ad;
			_ordersDbFilename = ordersDbFilename;
			_fullOrdersDbFilename = fullOrdersDbFilename;
		}
		
		public ShopEnum Shop
		{
			get { return _shop; }
		}
		
		public string Account
		{
			get { return _account; }
		}
		
		public string SubAccount
		{
			get { return _subAccount; }
			set { _subAccount = value; }
		}
		
		public string SellerId
		{
			get { return _sellerId; }
		}
		
		public string Pw
		{
			get { return _pw; }
			set { _pw = value; }
		}
		
		public string DisplayName
		{
			get { return _displayName; }
		}
		
		public string DisplayNameOnBill
		{
			get { return _displayNameOnBill; }
		}
		
		public string ShortName
		{
			get { return _shortName; }
		}
		
		public string SenderName
		{
			get { return _senderName; }
		}
		
		public string SenderPhone
		{
			get { return _senderPhone; }
		}
		
		public string Ad
		{
			get { return _ad; }
		}
		
		public string OrdersDbFilename
		{
			get { return _ordersDbFilename; }
		}
		
		public string FullOrdersDbFilename
		{
			get { return _fullOrdersDbFilename; }
		}

		public static ShopProfile Current
		{
			get { return _current; }
		}
		
		public override string ToString()
		{
			return _displayName;
		}
		
		public static List<ShopProfile> Shops
		{
			get
			{
				if (null == _shops)
				{
					_shops = new List<ShopProfile>();
					//_shops.Add(new ShopProfile(ShopEnum.EgoGermany, "败遍欧洲", "1029290077", "buy欧洲", "buy欧洲", "eur8", "http://eur8.taobao.com", "18964913317", "orders-eur8-c.xml", "orders-eur8-c.xml"));
					_shops.Add(new ShopProfile(ShopEnum.EgoGermany, "败遍欧洲", "1029290077", "buy欧洲", "bu**洲", "eur8", "顾双双", "18821213226", "https://eur8.taobao.com", "orders-eur8-c.xml", "orders-eur8-c.xml"));

					/* fucking code
					//_shops.Add(new ShopProfile(ShopEnum.Egode, "德国e购", "德国e购", "德 国 e 购", "Egode", "http://egode.taobao.com", "18964913317", "orders-c.xml", "orders-full-c.xml"));
					//_shops.Add(new ShopProfile(ShopEnum.Egode, "德国e购", "787602526", "德国e购", "德 国 e 购", "egode", string.Empty, "18964913317", "orders-c.xml", "orders-full-c.xml"));
					//_shops.Add(new ShopProfile(ShopEnum.First, "德国壹号", "德国1号", "德 国 1 号", "1st", "http://deguo1st.taobao.com", "18964913317", "orders-deguo1st-c.xml", "orders-deguo1st-c.xml"));
					*/
				}

				return _shops;
			}
		}
		
		public static void Switch(ShopEnum shop)
		{
			foreach (ShopProfile sp in Shops)
			{
				if (sp.Shop == shop)
				{
					_current = sp;
					break;
				}
			}
		}
	}
}