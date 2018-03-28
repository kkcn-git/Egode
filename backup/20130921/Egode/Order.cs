using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace OrderParser
{
	public enum ShippingOrigins
	{
		Deutschland = 1,
		Shanghai = 2
	}

	public class Order
	{
		public event EventHandler OnStatusChanged;

		private class DateTimeComparer : IComparer
		{
			#region IComparer Members

			public int Compare(object x, object y)
			{
				DateTime xdt = DateTime.Parse(x.ToString());
				DateTime ydt = DateTime.Parse(y.ToString());
				return -1 * xdt.CompareTo(ydt);
			}

			#endregion
		}
	
		public enum OrderStatus
		{
			Closed = 0,
			Deal = 1,
			Paid = 2,
			Sent = 3,
			Succeeded = 4,
			PartialSent = 5
		}
	
		private const int PROP_INDEX_ORDER_ID = 0;
		private const int PROP_INDEX_BUYER_ACCOUNT = 1;
		private const int PROP_INDEX_ALIPAY_ACCOUNT = 2;
		private const int PROP_INDEX_GOODS_MONEY = 3;
		private const int PROP_INDEX_FREIGHT = 4;
		private const int PROP_INDEX_PAY_POINTS = 5;
		private const int PROP_INDEX_TOTAL_MONEY = 6;
		private const int PROP_INDEX_REWARD_POINTS = 7;
		private const int PROP_INDEX_REAL_PAY_MONEY = 8;
		private const int PROP_INDEX_REAL_PAY_POINTS = 9;
		private const int PROP_INDEX_ORDER_STATUS = 10;
		private const int PROP_INDEX_BUYER_REMARK = 11;
		private const int PROP_INDEX_RECIPIENT_NAME = 12;
		private const int PROP_INDEX_RECIPIENT_ADDRESS = 13;
		private const int PROP_INDEX_LOGISTICS_TYPE = 14;
		private const int PROP_INDEX_PHONE_NUMBER = 15;
		private const int PROP_INDEX_MOBILE_NUMBER = 16;
		private const int PROP_INDEX_DEAL_TIME = 17;
		private const int PROP_INDEX_PAYING_TIME = 18;
		private const int PROP_INDEX_ITEM_SUBJECT = 19;
		private const int PROP_INDEX_ITEM_CATEGORY = 20;
		private const int PROP_INDEX_SHIPMENT_NUMBER = 21;
		private const int PROP_INDEX_SHIPMENT_COMPANY = 22;
		private const int PROP_INDEX_REMARK = 23;
		private const int PROP_INDEX_ITEM_AMOUNT = 24;
		private const int PROP_INDEX_SHOP_ID = 25;
		private const int PROP_INDEX_SHOP_NAME = 26;
		private const int PROP_INDEX_CLOSING_REASON = 27;
		private const int PROP_INDEX_SELLER_SERVICE_FEE = 28;
		private const int PROP_INDEX_BUYER_SERVICE_FEE = 29;
		private const int PROP_INDEX_INVOICE_TITLE = 30;
		private const int PROP_INDEX_IS_PHONE_ORDER = 31;
		private const int PROP_INDEX_OTHER_ORDER_INFO = 32;
		private const int PROP_INDEX_EARNEST_MONEY = 33;
		private const int PROP_INDEX_EDITED_SKU = 34;
		private const int PROP_INDEX_EDITED_RECIPIENT_ADDRESS = 35;
		private const int PROP_INDEX_EXCEPTION_INFO = 36;
		
		private readonly string _orderId;
		private readonly string _buyerAccount;
		private string _alipayAccount;
		private float _goodsMoney;
		private float _freight;
		private float _payPoints;
		private float _totalMoney;
		private float _rewardPoints;
		private float _realPayMoney;
		private float _realPayPoints;
		private OrderStatus _orderStatus;
		private string _buyerRemark;
		private string _recipientName;
		private string _recipientAddress;
		private string _phoneNumber;
		private string _mobileNumber;
		private readonly DateTime _dealTime;
		private DateTime _payingTime;
		private string _itemSubject;
		private string _itemCategory;
		private string _shipmentNumber;
		private string _shipmentCompany;
		private string _remark;
		private int _itemAmount;
		private string _shopId;
		private string _shopName;
		private string _closingReason;
		private float _sellerServiceFee;
		private float _buyerServiceFee;
		private string _invoiceTitle;
		private bool _isPhoneOrder;
		private string _otherOrderInfo;
		private float _earnestMoney;
		private string _editedSku;
		private string _editedRecipientAddress;
		private string _exceptionInfo;
		
		private bool _prepared = false;
		private bool _localPrepared = false; // just available for client program.
		private bool _visible; // just available for client program.
		private string _items;
		
		public Order(string orderId, string buyerAccount, string alipayAccount, DateTime dealTime)
		{
			_orderId = orderId;
			_buyerAccount = buyerAccount;
			_alipayAccount = alipayAccount;
			_dealTime = dealTime;
		}
		
		// foramt of infoString: "1234567890000","xyz","xyz","xyz","","","","","","",.......
		public static Order Parse(string infoString)
		{
			if (infoString.StartsWith("\""))
				infoString = infoString.Substring(1, infoString.Length - 1);
			if (infoString.EndsWith("\""))
				infoString = infoString.Substring(0, infoString.Length - 1) + " ";
		
			List<string> infos = Split(infoString, "\",\"");
			
			if (infos.Count < 36) // invalid input string.
				return null;

			Order order = new Order(
				infos[PROP_INDEX_ORDER_ID], 
				infos[PROP_INDEX_BUYER_ACCOUNT], infos[PROP_INDEX_ALIPAY_ACCOUNT], 
				DateTime.Parse(infos[PROP_INDEX_DEAL_TIME]));

			order._alipayAccount = NormalizeInfoString(infos[PROP_INDEX_ALIPAY_ACCOUNT]);
			order._goodsMoney = float.Parse(infos[PROP_INDEX_GOODS_MONEY]);
			order._freight = float.Parse(infos[PROP_INDEX_FREIGHT]);
			order._totalMoney = float.Parse(infos[PROP_INDEX_TOTAL_MONEY]);
			order._realPayMoney = float.Parse(infos[PROP_INDEX_REAL_PAY_MONEY]);
			order._orderStatus = ParseOrderStatus(infos[PROP_INDEX_ORDER_STATUS]);
			order._buyerRemark = NormalizeInfoString(infos[PROP_INDEX_BUYER_REMARK]);
			order._recipientName = NormalizeInfoString(infos[PROP_INDEX_RECIPIENT_NAME]);
			order._recipientAddress = NormalizeInfoString(infos[PROP_INDEX_RECIPIENT_ADDRESS]);
			order._phoneNumber = NormalizeInfoString(infos[PROP_INDEX_PHONE_NUMBER]);
			order._mobileNumber = NormalizeInfoString(infos[PROP_INDEX_MOBILE_NUMBER]);
			order._payingTime = string.IsNullOrEmpty(infos[PROP_INDEX_PAYING_TIME]) ? DateTime.MinValue : DateTime.Parse(infos[PROP_INDEX_PAYING_TIME]);
			order._itemSubject = NormalizeInfoString(infos[PROP_INDEX_ITEM_SUBJECT]);
			order._shipmentNumber = NormalizeInfoString(infos[PROP_INDEX_SHIPMENT_NUMBER]);
			order._shipmentCompany = NormalizeInfoString(infos[PROP_INDEX_SHIPMENT_COMPANY]);
			order._remark = NormalizeInfoString(infos[PROP_INDEX_REMARK]);
			order._itemAmount = int.Parse(infos[PROP_INDEX_ITEM_AMOUNT]);
			order._closingReason = NormalizeInfoString(infos[PROP_INDEX_CLOSING_REASON]);
			order._isPhoneOrder = ParsePhoneOrderStatus(infos[PROP_INDEX_IS_PHONE_ORDER]);
			order._editedRecipientAddress = NormalizeInfoString(infos[PROP_INDEX_EDITED_RECIPIENT_ADDRESS]);

			return order;
		}
		
		// Update order information using sepcified order.
		public void Update(Order o)
		{
			this._goodsMoney = o._goodsMoney;
			this._freight = o._freight;
			this._totalMoney = o._totalMoney;
			this._realPayMoney = o._realPayMoney;
			this._orderStatus = o._orderStatus;
			this._buyerRemark = o._buyerRemark;
			this._recipientName = o._recipientName;
			this._recipientAddress = o._recipientAddress;
			this._phoneNumber = o._phoneNumber;
			this._mobileNumber = o._mobileNumber;
			this._payingTime = o._payingTime;
			this._itemSubject = o._itemSubject;
			this._shipmentNumber = o._shipmentNumber;
			this._shipmentCompany = o._shipmentCompany;
			this._remark = o._remark;
			this._itemAmount = o._itemAmount;
			this._closingReason = o._closingReason;
			this._isPhoneOrder = o._isPhoneOrder;
			this._editedRecipientAddress = o._editedRecipientAddress;
			
			this._items = o._items;
		}
		
		private static string NormalizeInfoString(string s)
		{
			s = s.Trim();
		
			if (s.StartsWith("'"))
				s = s.Substring(1, s.Length - 1);
			
			if (s.EndsWith("'"))
				s = s.Substring(0, s.Length - 1);
			
			return s;
		}
		
		public static OrderStatus ParseOrderStatus(string status)
		{
			switch (status)
			{
				case "等待买家付款":
					return OrderStatus.Deal;
				
				case "买家已付款，等待卖家发货":
					return OrderStatus.Paid;

				case "卖家已发货，等待买家确认":
					return OrderStatus.Sent;

				case "卖家部分发货":
					return OrderStatus.PartialSent;

				case "交易成功":
					return OrderStatus.Succeeded;
				
				case "交易关闭":
					return OrderStatus.Closed;
			}

			return OrderStatus.Deal;
		}
		
		private static bool ParsePhoneOrderStatus(string s)
		{
			return s.Equals("手机订单");
		}
		
		static List<string> Split(string s, string separator)
		{
			int start = 0;
			List<string> result = new List<string>();

			while (s.IndexOf(separator, start) >= 0)
			{
				int end = s.IndexOf(separator, start);
				if (end < 0)
					end = s.Length - 1;
				result.Add(s.Substring(start, end-start));
				start = end + separator.Length;
			}
			
			if (start < s.Length)
				result.Add(s.Substring(start, s.Length - start));
			
			return result;
		}
		
		public static bool SaveXml(string filename, List<Order> orders)
		{
			if (null == orders || orders.Count <= 0)
				return false;
		
			XmlDocument doc = new XmlDocument();
			XmlElement elmOrders = doc.CreateElement("orders");
			doc.InsertBefore(elmOrders, null);
			elmOrders.SetAttribute("last_modified", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

			foreach (Order order in orders)
			{
				XmlElement elmOrder = doc.CreateElement("order");
				elmOrders.InsertBefore(elmOrder, null);
				
				elmOrder.SetAttribute("order_id", order.OrderId);
				elmOrder.SetAttribute("deal_time", order.DealTime.ToString("yyyy-MM-dd HH:mm:ss"));
				elmOrder.SetAttribute("paying_time", order.PayingTime.ToString("yyyy-MM-dd HH:mm:ss"));
				elmOrder.SetAttribute("buyer_account", order.BuyerAccount);
				elmOrder.SetAttribute("alipay_account", order.AlipayAccount);
				elmOrder.SetAttribute("goods_money", order.GoodsMoney.ToString("0.00"));
				elmOrder.SetAttribute("freight", order.Freight.ToString("0.00"));
				elmOrder.SetAttribute("total_money", order.TotalMoney.ToString("0.00"));
				elmOrder.SetAttribute("real_pay_money", order.RealPayMoney.ToString("0.00"));
				elmOrder.SetAttribute("status", order.Status.ToString());
				elmOrder.SetAttribute("buyer_remark", order.BuyerRemark);
				elmOrder.SetAttribute("recipient_name", order.RecipientName);
				elmOrder.SetAttribute("recipient_address", order.RecipientAddress);
				elmOrder.SetAttribute("edited_recipient_address", order.EditedRecipientAddress);
				elmOrder.SetAttribute("phone_number", order.PhoneNumber);
				elmOrder.SetAttribute("mobile_number", order.MobileNumber);
				elmOrder.SetAttribute("shipment_number", order.ShipmentNumber);
				elmOrder.SetAttribute("shipment_company", order.ShipmentCompany);
				elmOrder.SetAttribute("item_subject", order.ItemSubject);
				elmOrder.SetAttribute("item_amount", order.ItemAmount.ToString());
				elmOrder.SetAttribute("remark", order.Remark);
				elmOrder.SetAttribute("closing_reason", order.ClosingReason);
				elmOrder.SetAttribute("is_phone_order", order.IsPhoneOrder.ToString());

				elmOrder.SetAttribute("prepared", order.Prepared.ToString());
				elmOrder.SetAttribute("items", order.Items);
			}
			
			doc.Save(filename);
			return true;
		}

		public static List<Order> LoadXmlFile(string filename, bool sortByPayingTime)
		{
			if (!System.IO.File.Exists(filename))
				return null;

			XmlDocument doc = new XmlDocument();
			doc.Load(filename);
			return LoadXmlCore(doc, sortByPayingTime);
		}

		public static List<Order> LoadXmlStream(string xml, bool sortByPayingTime)
		{
			if (string.IsNullOrEmpty(xml))
				return null;

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			return LoadXmlCore(doc, sortByPayingTime);
		}

		public static List<Order> LoadXmlCore(XmlDocument doc, bool sortByPayingTime)
		{
			if (null == doc)
				return null;

			string xpath = ".//order";
			XPathNavigator nav = doc.CreateNavigator();
			XPathExpression exp = nav.Compile(xpath);
			exp.AddSort(sortByPayingTime ? "@paying_time" : "@deal_time", new DateTimeComparer());
			XPathNodeIterator iterator = nav.Select(exp);

			List<Order> orders = new List<Order>();

			//iterator.Current.att

			while (iterator.MoveNext())
			{
				string orderId = iterator.Current.GetAttribute("order_id", string.Empty);
				DateTime dealTime = DateTime.Parse(iterator.Current.GetAttribute("deal_time", string.Empty));
				string buyerAccount = iterator.Current.GetAttribute("buyer_account", string.Empty);
				string alipayAccount = iterator.Current.GetAttribute("alipay_account", string.Empty);
				Order order = new Order(orderId, buyerAccount, alipayAccount, dealTime);
				orders.Add(order);

				order._payingTime = DateTime.Parse(iterator.Current.GetAttribute("paying_time", string.Empty));
				order._goodsMoney = float.Parse(iterator.Current.GetAttribute("goods_money", string.Empty));
				order._freight = float.Parse(iterator.Current.GetAttribute("freight", string.Empty));
				order._totalMoney = float.Parse(iterator.Current.GetAttribute("total_money", string.Empty));
				order._realPayMoney = float.Parse(iterator.Current.GetAttribute("real_pay_money", string.Empty));
				order._orderStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), iterator.Current.GetAttribute("status", string.Empty));
				order._buyerRemark = iterator.Current.GetAttribute("buyer_remark", string.Empty);
				order._recipientName = iterator.Current.GetAttribute("recipient_name", string.Empty);
				order._recipientAddress = iterator.Current.GetAttribute("recipient_address", string.Empty);
				order._editedRecipientAddress = iterator.Current.GetAttribute("edited_recipient_address", string.Empty);
				order._phoneNumber = iterator.Current.GetAttribute("phone_number", string.Empty);
				order._mobileNumber = iterator.Current.GetAttribute("mobile_number", string.Empty);
				order._shipmentNumber = iterator.Current.GetAttribute("shipment_number", string.Empty);
				order._shipmentCompany = iterator.Current.GetAttribute("shipment_company", string.Empty);
				order._itemSubject = iterator.Current.GetAttribute("item_subject", string.Empty);
				order._itemAmount = int.Parse(iterator.Current.GetAttribute("item_amount", string.Empty));
				order._remark = iterator.Current.GetAttribute("remark", string.Empty);
				order._closingReason = iterator.Current.GetAttribute("closing_reason", string.Empty);
				order._isPhoneOrder = bool.Parse(iterator.Current.GetAttribute("is_phone_order", string.Empty));

				if (null != iterator.Current.GetAttribute("prepared", string.Empty))
					order._prepared = bool.Parse(iterator.Current.GetAttribute("prepared", string.Empty));

				order._items = iterator.Current.GetAttribute("items", string.Empty);
			}

			return orders;
		}

		public static Order GetOrderByOrderId(string orderId, List<Order> orders)
		{
			if (null == orders || orders.Count <= 0)
				return null;

			foreach (Order o in orders)
			{
				if (o.OrderId.Equals(orderId))
					return o;
			}

			return null;
		}

		public static List<Order> GetOrdersByBuyerAccount(string buyerAccount, List<Order> orders)
		{
			if (null == orders || orders.Count <= 0)
				return null;

			List<Order> result = new List<Order>();
			
			foreach (Order o in orders)
			{
				if (o.BuyerAccount.Equals(buyerAccount))
					result.Add(o);
			}

			return result;
		}

		public static string SimplifyItemSubject(string originalSubject)
		{
			return SimplifyItemSubject(originalSubject, false);
		}

		public static string SimplifyItemSubject(string originalSubject, bool simpliest)
		{
			originalSubject = originalSubject.Replace("德国直邮(8罐包邮):", string.Empty);
			originalSubject = originalSubject.Replace("德国直邮(12罐包邮):", string.Empty);
			originalSubject = originalSubject.Replace("德国直邮(10盒包邮):", string.Empty);
			originalSubject = originalSubject.Replace("德国直邮(12盒包邮):", string.Empty);
			originalSubject = originalSubject.Replace("德国直邮(14盒包邮):", string.Empty);
			originalSubject = originalSubject.Replace("【德国代购】", string.Empty);
			originalSubject = originalSubject.Replace("德国代购 ", string.Empty);
			originalSubject = originalSubject.Replace("德国代购", string.Empty);
			originalSubject = originalSubject.Replace("【德国直邮】", string.Empty);
			originalSubject = originalSubject.Replace("德国直邮 ", string.Empty);
			originalSubject = originalSubject.Replace("德国直邮:", string.Empty);
			originalSubject = originalSubject.Replace("德国直邮", string.Empty);
			originalSubject = originalSubject.Replace("8罐包邮", string.Empty);
			originalSubject = originalSubject.Replace("12罐包邮", string.Empty);
			originalSubject = originalSubject.Replace("10盒包邮", string.Empty);
			originalSubject = originalSubject.Replace("12盒包邮", string.Empty);
			originalSubject = originalSubject.Replace("14盒包邮", string.Empty);
			originalSubject = originalSubject.Replace("会员包税", string.Empty);
			originalSubject = originalSubject.Replace("()", string.Empty);
			originalSubject = originalSubject.Replace("送气囊", string.Empty);
			originalSubject = originalSubject.Replace("上海现货", string.Empty);
			originalSubject = originalSubject.Replace("现货", string.Empty);
			originalSubject = originalSubject.Replace("新包装", string.Empty);
			originalSubject = originalSubject.Replace("新版", string.Empty);
			originalSubject = originalSubject.Replace("14盒包邮", string.Empty);
			originalSubject = originalSubject.Replace("婴儿", string.Empty);
			
			if (simpliest)
			{
				originalSubject = originalSubject.Replace("1岁奶粉", string.Empty);
				originalSubject = originalSubject.Replace("2岁奶粉", string.Empty);
				originalSubject = originalSubject.Replace("奶粉", string.Empty);
				originalSubject = originalSubject.Replace("0-6个月", string.Empty);
				originalSubject = originalSubject.Replace("6-10个月", string.Empty);
				originalSubject = originalSubject.Replace("10-12个月", string.Empty);
				originalSubject = originalSubject.Replace("爱他美Aptamil", "Apatamil");

				originalSubject = originalSubject.Replace("喜宝HiPP BIO", "HiPP");
				originalSubject = originalSubject.Replace("有机", string.Empty);

				originalSubject = originalSubject.Replace("雀巢Nestle", "Nestle");

				originalSubject = originalSubject.Replace("奶粉", string.Empty);
			}

			return originalSubject;
		}

		public bool MatchKeyword(string[] keys)
		{
			if (null == keys || keys.Length <= 0)
				return false;
			
			foreach (string key in keys)
			{
				if (_orderId.Contains(key))
					return true;
				if (_buyerAccount.Contains(key))
					return true;
				if (_alipayAccount.Contains(key))
					return true;
				if (_buyerRemark.Contains(key))
					return true;
				if (_recipientName.Contains(key))
					return true;
				if (HanZiToPinYin.Convert(_recipientName).ToLower().Contains(key.ToLower()))
					return true;
				if (_recipientAddress.Contains(key))
					return true;
				if (HanZiToPinYin.Convert(_recipientAddress).ToLower().Contains(key.ToLower()))
					return true;
				if (_phoneNumber.Contains(key))
					return true;
				if (_mobileNumber.Contains(key))
					return true;
				if (_dealTime.ToString().Contains(key))
					return true;
				if (_payingTime.ToString().Contains(key))
					return true;
				if (SimplifyItemSubject(_items).Contains(key))
					return true;
				if (_shipmentNumber.Contains(key))
					return true;
				if (_shipmentCompany.Contains(key))
					return true;
				if (_remark.Contains(key))
					return true;
				if (_closingReason.Contains(key))
					return true;
				if (_editedRecipientAddress.Contains(key))
					return true;
				if (!string.IsNullOrEmpty(_editedRecipientAddress) && HanZiToPinYin.Convert(_editedRecipientAddress).ToLower().Contains(key.ToLower()))
					return true;
			}
			
			return false;
		}
		
		public string GetFullAddress()
		{
			StringBuilder addr = new StringBuilder();
			if (!string.IsNullOrEmpty(this.RecipientName))
				addr.Append(this.RecipientName + ", ");
			if (!string.IsNullOrEmpty(this.MobileNumber))
				addr.Append(this.MobileNumber + ", ");
			if (string.IsNullOrEmpty(this.MobileNumber) && !string.IsNullOrEmpty(this.PhoneNumber))
				addr.Append(this.PhoneNumber + ", ");
			if (!string.IsNullOrEmpty(this.RecipientAddress))
				addr.Append(this.RecipientAddress);
			return addr.ToString();
		}

		//public string GetFullEditedAddress()
		//{
		//    if (string.IsNullOrEmpty(this.EditedRecipientAddress.Trim())) // no edited address.
		//        return string.Empty;
			
		//    try
		//    {
		//        string url = string.Format("http://trade.taobao.com/trade/detail/trade_item_detail.htm?bizOrderId={0}", this.OrderId);
		//        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
		//        request.Method = "GET";
		//        request.ContentType = "text/html";
		//        WebResponse response = request.GetResponse();
		//        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
		//        string html = reader.ReadToEnd().Trim();
		//        reader.Close();
				
		//        Regex r = new Regex("<th>新收货地址：</th>.*<td>(.*)</td>");
		//        Match m = r.Match(html);
		//        if (m.Success)
		//        {
		//            string s = m.Value;
		//        }
		//    }
		//    catch (Exception ex)
		//    {
		//        Trace.WriteLine(ex);
		//    }
		//    return string.Empty;
		//}

		// 单位: 克(g)
		public int GetNetWeight()
		{
			int weight = 0;
			string[] itemInfos = _items.Split('★');
			for (int i = 0; i < itemInfos.Length; i++)
			{
				string itemInfo = itemInfos[i];
				string[] infos = itemInfo.Split('☆');
				if (infos.Length < 3)
					continue;

				string productName = infos[0];
				int amount = int.Parse(infos[2]);
				weight += GetProductNetWeight(productName) * amount;
			}

			return weight;
		}

		// product: product subject on taobao.
		// unit: gram(g)
		public static int GetProductNetWeight(string product)
		{
			if (string.IsNullOrEmpty(product))
				return 0;
			
			product = product.ToLower();
			
			if (product.Contains("400g"))
				return 400;
			if (product.Contains("500g"))
				return 500;
			if (product.Contains("550g"))
				return 550;
			if (product.Contains("600g"))
				return 600;
			if (product.Contains("800g"))
				return 800;
			
			if (product.Contains("爱他美") || product.Contains("aptamil"))
			{
				if (product.Contains("pre") || product.Contains("1段") || product.Contains("2段") || product.Contains("3段"))
					return 800;
				if (product.Contains("1+") || product.Contains("2+"))
					return 600;
			}

			return 0;
		}
		
		public bool GetFirstAvailableProduct(out string product, out int amount)
		{
			product = string.Empty;
			amount = 0;
		
			string[] itemInfos = _items.Split('★');
			for (int i = 0; i < itemInfos.Length; i++)
			{
				string itemInfo = itemInfos[i];
				string[] infos = itemInfo.Split('☆');
				if (infos.Length < 3)
					continue;

				string productName = infos[0];
				int productAmount = int.Parse(infos[2]);
				if (productName.Contains("爱他美") || productName.Contains("喜宝"))
				{
					product = SimplifyItemSubject(productName, true);
					amount = productAmount;
					return true;
				}
			}

			return false;
		}

		public string OrderId
		{
			get { return _orderId; }
		}
		
		public string BuyerAccount
		{
			get { return _buyerAccount; }
		}
		
		public string AlipayAccount
		{
			get { return _alipayAccount; }
		}
		
		public float GoodsMoney
		{
			get { return _goodsMoney; }
		}
		
		public float Freight
		{
			get { return _freight; }
		}
		
		public float TotalMoney
		{
			get { return _totalMoney; }
		}
		
		public float RealPayMoney
		{
			get { return _realPayMoney; }
		}
		
		public OrderStatus Status
		{
			get { return _orderStatus; }
		}
		
		public string BuyerRemark
		{
			get { return _buyerRemark; }
		}
		
		public string RecipientName
		{
			get { return _recipientName; }
		}
		
		public string RecipientAddress
		{
			get { return _recipientAddress; }
		}
		
		public string PhoneNumber
		{
			get { return _phoneNumber; }
		}
		
		public string MobileNumber
		{
			get { return _mobileNumber; }
		}
		
		public DateTime DealTime
		{
			get { return _dealTime; }
		}
		
		public DateTime PayingTime
		{
			get { return _payingTime; }
		}
		
		public String ItemSubject
		{
			get { return _itemSubject; }
		}
		
		public string ShipmentNumber
		{
			get 
			{ 
				if (_shipmentNumber.StartsWith("No:"))
					return _shipmentNumber.Replace("No:", string.Empty);
				else
					return _shipmentNumber;
			}
		}
		
		public string ShipmentCompany
		{
			get { return _shipmentCompany; }
		}
		
		public string Remark
		{
			get { return _remark; }
		}
		
		public int ItemAmount
		{
			get { return _itemAmount; }
		}
		
		public string ClosingReason
		{
			get { return _closingReason; }
		}
		
		public bool IsPhoneOrder
		{
			get { return _isPhoneOrder; }
		}
		
		public string EditedRecipientAddress
		{
			get { return _editedRecipientAddress; }
			set { _editedRecipientAddress = value; }
		}
		
		public bool Prepared
		{
			get { return _prepared; }
		}
		
		public void Prepare()
		{
			_prepared = true;
			if (null != this.OnStatusChanged)
				this.OnStatusChanged(this, EventArgs.Empty);
		}
		
		public bool LocalPrepared
		{
			get { return _localPrepared; }
			set { _localPrepared = value; }
		}
		
		public void LocalPrepare()
		{
			_localPrepared = true;
			if (null != this.OnStatusChanged)
				this.OnStatusChanged(this, EventArgs.Empty);
		}
		
		public void Consign()
		{
			_orderStatus = OrderStatus.Sent;
			if (null != this.OnStatusChanged)
				this.OnStatusChanged(this, EventArgs.Empty);
		}
		
		public bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}
		
		public string Items
		{
			get { return _items; }
			set { _items = value; }
		}
	}
}