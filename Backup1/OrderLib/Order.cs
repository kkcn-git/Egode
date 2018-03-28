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
using System.Text.RegularExpressions;

namespace OrderLib
{
	public enum ShippingOrigins
	{
		Deutschland = 1,
		Shanghai = 2,
		Ningbo = 4
	}

	public enum ShipmentCompanies
	{
		Unknown = 0,
		Yto = 1,
		Sf = 2,
		Post = 3,
		EMS = 4,
		Zto = 5,
		Best = 6, // 百世
		Yunda = 7
	}

	public class Order
	{
		public const string ITEM_SEPARATOR = "★";
		public const string ITEM_INFO_SEPARATOR = "☆";

		public event EventHandler OnStatusChanged;
		public event EventHandler OnTransmissionChanged; // 手动自动发生变化, 自动类型发生变化都会触发此事件.

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
		
		/*
		0. "订单编号",
		1. "买家会员名",
		2. "买家支付宝账号",
		3. "买家应付货款",
		4. "买家应付邮费",
		5. "买家支付积分",
		6. "总金额",
		7. "返点积分",
		8. "买家实际支付金额",
		9. "买家实际支付积分",
		10. "订单状态",
		11. "买家留言",
		12. "收货人姓名",
		13. "收货地址 ",
		14. "运送方式",
		15. "联系电话 ",
		16. "联系手机",
		17. "订单创建时间",
		18. "订单付款时间 ",
		19. "宝贝标题 ",
		20. "宝贝种类 ",
		21. "物流单号 ",
		22. "物流公司",
		23. "订单备注",
		24. "宝贝总数量",
		25. "店铺Id",
		26. "店铺名称",
		27. "订单关闭原因",
		28. "卖家服务费",
		29. "买家服务费",
		30. "发票抬头",
		31. "是否手机订单",
		32. "分阶段订单信息",
		33. "特权订金订单id",
		34. "是否上传合同照片",
		35. "是否上传小票",
		36. "是否代付",
		37. "定金排名",
		38. "修改后的sku",
		39. "修改后的收货地址",
		40. "异常信息",
		41. "天猫卡券抵扣",
		42. "集分宝抵扣",
		43. "是否是O2O交易"
		*/

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
		private const int PROP_INDEX_PHASE_ORDER_INFO = 32;
		private const int PROP_INDEX_EDITED_SKU = 38;
		private const int PROP_INDEX_EDITED_RECIPIENT_ADDRESS = 39;
		//private const int PROP_INDEX_EXCEPTION_INFO = 40;
		
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
		// ItemSubject不再使用. 2014/10/08
		// ExportOrderDetailListXXXXXXXXXXXXX.csv中包含了更为详细的Item信息, 并且拼接为Items保存在数据库文件中,
		// 因此, 不再使用ExportOrderListXXXXXXXXXXXX.csv中的ItemSubject信息.
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
		
		private bool _preparedNingbo = false;
		private bool _localPreparedNingbo = false;
		
		// Added by KK on 2017/10/13.
		// 是否已经提取订单信息准备打印韵达面单. 提取信息后生成表格导入韵达自己的系统打印.
		private bool _yundaPrepared = false;
		private string _yundaTrackingNumber = string.Empty;
		
		private string _alipayNumber; // retrieved from details page.
		
		// Added by KK on 2017/10/19.
		// 如何自动处理订单, Unknown代表手动处理
		private ShipmentCompanies _autoTransmissionProcessor; // 如果是自动处理, 使用哪个快递公司处理.

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
			// Added by KK on 2016/10/19. I dont konw why there is a "=" in data row.
			infoString = infoString.Replace("\",=\"", "\",\"");
			
			// Added by KK on 2017/09/24.
			// Taobao start to use "null" for empty string instead of "".
			infoString = infoString.Replace("\"null\"", "\"\"");
			infoString = infoString.Replace("\"'null\"", "\"\"");
			infoString = infoString.Replace(" null ", " ");

			if (infoString.StartsWith("=\""))
				infoString = infoString.Remove(0, 2);
			else if (infoString.StartsWith("\""))
				infoString = infoString.Remove(0, 1);
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
			//order._itemSubject = NormalizeInfoString(infos[PROP_INDEX_ITEM_SUBJECT]); // obsoleted. WILL NOT save into xml database file. 2014/10/08
			order._shipmentNumber = NormalizeInfoString(infos[PROP_INDEX_SHIPMENT_NUMBER]);
			order._shipmentCompany = NormalizeInfoString(infos[PROP_INDEX_SHIPMENT_COMPANY]);
			order._remark = NormalizeInfoString(infos[PROP_INDEX_REMARK]);
			order._itemAmount = int.Parse(infos[PROP_INDEX_ITEM_AMOUNT]);
			order._shopName = NormalizeInfoString(infos[PROP_INDEX_SHOP_NAME]);
			order._closingReason = NormalizeInfoString(infos[PROP_INDEX_CLOSING_REASON]);
			order._isPhoneOrder = ParsePhoneOrderStatus(infos[PROP_INDEX_IS_PHONE_ORDER]);
			order._editedRecipientAddress = NormalizeInfoString(infos[PROP_INDEX_EDITED_RECIPIENT_ADDRESS]);

			// Added by KK on 2016/10/18.
			if (!string.IsNullOrEmpty(order._editedRecipientAddress) && order._editedRecipientAddress.Equals("否"))
				order._editedRecipientAddress = string.Empty;

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
			//this._itemSubject = o._itemSubject; // obsoleted. WILL NOT save into xml database file. 2014/10/08
			this._shipmentNumber = o._shipmentNumber;
			this._shipmentCompany = o._shipmentCompany;
			this._remark = o._remark;
			this._itemAmount = o._itemAmount;
			this._closingReason = o._closingReason;
			this._isPhoneOrder = o._isPhoneOrder;
			this._editedRecipientAddress = o._editedRecipientAddress;
			
			this._items = o._items;
			
			this._alipayNumber = o._alipayNumber;
		}

		//public void ExtractSubject(List<SubjectInfo> subjectInfos)
		//{
		//    return;
		//    if (null == subjectInfos)
		//        return;
		//    if (string.IsNullOrEmpty(_items))
		//        return;
		//    string[] items = _items.Split(Order.ITEM_SEPARATOR.ToCharArray()[0]);
		//    if (null == items || items.Length <= 0)
		//        return;
			
		//    StringBuilder sb = new StringBuilder();
		//    foreach (string item in items)
		//    {
		//        string[] itemInfo = item.Split(Order.ITEM_INFO_SEPARATOR.ToCharArray()[0]);
		//        if (null == itemInfo || itemInfo.Length < 4)
		//            continue;
				
		//        SubjectInfo si = SubjectInfo.GetSubjectInfoById(itemInfo[0]);
		//        string subject = (null == si ? itemInfo[0] : si.Subject);

		//        sb.Append(string.Format("{1}{0}{2}{0}{3}{0}{4}", Order.ITEM_INFO_SEPARATOR, subject, itemInfo[1], itemInfo[2], itemInfo[3]));
		//        sb.Append(Order.ITEM_SEPARATOR);
		//    }
		//    if (sb.Length > 0)
		//        sb.Remove(sb.Length-1, 1);
		//    _items = sb.ToString();
		//}
		
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
				XmlElement elmOrder = doc.CreateElement("o");
				elmOrders.InsertBefore(elmOrder, null);

				elmOrder.SetAttribute("oid", order.OrderId); // order_id
				elmOrder.SetAttribute("dt", order.DealTime.ToString("yyyy-MM-dd HH:mm:ss")); // deal_time
				elmOrder.SetAttribute("pt", order.PayingTime.ToString("yyyy-MM-dd HH:mm:ss")); // paying_time
				elmOrder.SetAttribute("ba", order.BuyerAccount); // buyer_account
				elmOrder.SetAttribute("aa", order.AlipayAccount); // alipay_account
				elmOrder.SetAttribute("gm", order.GoodsMoney.ToString("0.##")); // goods_money
				elmOrder.SetAttribute("f", order.Freight.ToString("0.##")); // freight
				elmOrder.SetAttribute("tm", order.TotalMoney.ToString("0.##")); // total_money
				elmOrder.SetAttribute("pm", order.RealPayMoney.ToString("0.##")); // real_pay_money
				elmOrder.SetAttribute("s", order.Status.ToString()); // status
				elmOrder.SetAttribute("r", order.BuyerRemark); // buyer_remark
				elmOrder.SetAttribute("rn", order.RecipientName); // recipient_name
				elmOrder.SetAttribute("ra", order.RecipientAddress); // recipient_address
				elmOrder.SetAttribute("era", order.EditedRecipientAddress); // edited_recipient_address
				elmOrder.SetAttribute("p", order.PhoneNumber); // phone_number
				elmOrder.SetAttribute("m", order.MobileNumber); // mobile_number
				elmOrder.SetAttribute("sn", order.ShipmentNumber); // shipment_number
				elmOrder.SetAttribute("sc", order.ShipmentCompany); // shipment_company
				//elmOrder.SetAttribute("i", order.ItemSubject); // item_subject // obsoleted. 2014/10/08
				elmOrder.SetAttribute("ia", order.ItemAmount.ToString()); // item_amount
				elmOrder.SetAttribute("c", order.Remark); // remark
				elmOrder.SetAttribute("cr", order.ClosingReason); // closing_reason
				elmOrder.SetAttribute("po", order.IsPhoneOrder.ToString()); // is_phone_order

				elmOrder.SetAttribute("pr", order.Prepared.ToString()); // prepared
				elmOrder.SetAttribute("is", order.Items); // items
			}

			doc.Save(filename);
			return true;
		}

		public static List<Order> LoadXmlFile(string filename, bool sortByPayingTime)//, bool extractOrderSubject) // removed by KK on 2016/06/05.
		{
			if (!System.IO.File.Exists(filename))
				return null;

			XmlDocument doc = new XmlDocument();
			doc.Load(filename);
			return LoadXmlCore(doc, sortByPayingTime);//, extractOrderSubject);
		}

		public static List<Order> LoadXmlStream(string xml, bool sortByPayingTime)//, bool extractOrderSubject) // removed by KK on 2016/06/05.
		{
			if (string.IsNullOrEmpty(xml))
				return null;

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			return LoadXmlCore(doc, sortByPayingTime);//, extractOrderSubject);
		}

		public static List<Order> LoadXmlCore(XmlDocument doc, bool sortByPayingTime)//, bool extractOrderSubject) // removed by KK on 2016/06/05.
		{
			if (null == doc)
				return null;

			string xpath = ".//o";
			XPathNavigator nav = doc.CreateNavigator();
			XPathExpression exp = nav.Compile(xpath);
			exp.AddSort(sortByPayingTime ? "@pt" : "@dt", new DateTimeComparer());
			XPathNodeIterator iterator = nav.Select(exp);

			List<Order> orders = new List<Order>();

			//iterator.Current.att

			while (iterator.MoveNext())
			{
				DateTime dealTime = DateTime.Parse(iterator.Current.GetAttribute("dt", string.Empty));
				//TimeSpan ts = DateTime.Now - dealTime;
				//if (ts.TotalDays >= 30)
				//    break;

				string orderId = iterator.Current.GetAttribute("oid", string.Empty);
				string buyerAccount = iterator.Current.GetAttribute("ba", string.Empty);
				string alipayAccount = iterator.Current.GetAttribute("aa", string.Empty);
				//Trace.WriteLine(dealTime.ToString("yyyy-MM-dd HH:mm:ss"));
				Order order = new Order(orderId, buyerAccount, alipayAccount, dealTime);
				orders.Add(order);

				order._payingTime = DateTime.Parse(iterator.Current.GetAttribute("pt", string.Empty));
					order._goodsMoney = float.Parse(iterator.Current.GetAttribute("gm", string.Empty));
				order._freight = float.Parse(iterator.Current.GetAttribute("f", string.Empty));
				order._totalMoney = float.Parse(iterator.Current.GetAttribute("tm", string.Empty));
				order._realPayMoney = float.Parse(iterator.Current.GetAttribute("pm", string.Empty));
				order._orderStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), iterator.Current.GetAttribute("s", string.Empty));
				order._buyerRemark = iterator.Current.GetAttribute("r", string.Empty);
				order._recipientName = iterator.Current.GetAttribute("rn", string.Empty);
				order._recipientAddress = iterator.Current.GetAttribute("ra", string.Empty);
				order._editedRecipientAddress = iterator.Current.GetAttribute("era", string.Empty);
				order._phoneNumber = iterator.Current.GetAttribute("p", string.Empty);
				order._mobileNumber = iterator.Current.GetAttribute("m", string.Empty);
				order._shipmentNumber = iterator.Current.GetAttribute("sn", string.Empty);
				order._shipmentCompany = iterator.Current.GetAttribute("sc", string.Empty);
				//order._itemSubject = iterator.Current.GetAttribute("i", string.Empty); // obsoleted. 2014/10/08.
				order._itemAmount = int.Parse(iterator.Current.GetAttribute("ia", string.Empty));
				order._remark = iterator.Current.GetAttribute("c", string.Empty);
				order._closingReason = iterator.Current.GetAttribute("cr", string.Empty);
				order._isPhoneOrder = bool.Parse(iterator.Current.GetAttribute("po", string.Empty));

				if (null != iterator.Current.GetAttribute("pr", string.Empty))
					order._prepared = bool.Parse(iterator.Current.GetAttribute("pr", string.Empty));

				order._items = iterator.Current.GetAttribute("is", string.Empty);
				// Removed by KK on 2016/06/05.
				//if (extractOrderSubject)
				//    order.ExtractSubject(SubjectInfo.SubjectInfos);

			// Added by KK on 2016/10/18.
			if (!string.IsNullOrEmpty(order._editedRecipientAddress) && order._editedRecipientAddress.Equals("否"))
				order._editedRecipientAddress = string.Empty;
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
			originalSubject = originalSubject.Replace("德国直邮", string.Empty);
			originalSubject = originalSubject.Replace("德国直邮(8罐包邮):", string.Empty);
			originalSubject = originalSubject.Replace("德国直邮(12罐包邮):", string.Empty);
			originalSubject = originalSubject.Replace("德国直邮(10盒包邮):", string.Empty);
			originalSubject = originalSubject.Replace("德国直邮(12盒包邮):", string.Empty);
			originalSubject = originalSubject.Replace("德国直邮(14盒包邮):", string.Empty);
			originalSubject = originalSubject.Replace("【德国代购】", string.Empty);
			originalSubject = originalSubject.Replace("代购", string.Empty);
			originalSubject = originalSubject.Replace("直邮", string.Empty);

			originalSubject = originalSubject.Replace("(8罐包邮包税)", string.Empty);
			originalSubject = originalSubject.Replace("(12盒包邮会员包税)", string.Empty);
			originalSubject = originalSubject.Replace("12盒包邮", string.Empty);
			originalSubject = originalSubject.Replace("0-6月", string.Empty);
			originalSubject = originalSubject.Replace("6-10月", string.Empty);
			originalSubject = originalSubject.Replace("10-12个月 ", string.Empty);
			originalSubject = originalSubject.Replace("1岁", string.Empty);
			originalSubject = originalSubject.Replace("2岁", string.Empty);

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
			originalSubject = originalSubject.Replace("气囊", string.Empty);
			originalSubject = originalSubject.Replace("上海现货", string.Empty);
			originalSubject = originalSubject.Replace("现货", string.Empty);
			originalSubject = originalSubject.Replace("新包装", string.Empty);
			originalSubject = originalSubject.Replace("新版", string.Empty);
			originalSubject = originalSubject.Replace("14盒包邮", string.Empty);
			originalSubject = originalSubject.Replace("婴儿", string.Empty);
			originalSubject = originalSubject.Replace(":", string.Empty);
			
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

			return originalSubject.Trim();
		}

		public bool MatchKeyword(string[] keys)
		{
			if (null == keys || keys.Length <= 0)
				return false;
			
			//StringBuilder sb = new StringBuilder();
			//sb.Append(_orderId);
			//sb.Append(_buyerAccount);
			//sb.Append(_alipayAccount);
			//sb.Append(_buyerRemark);
			//sb.Append(_recipientName);
			//sb.Append(HanZiToPinYin.Convert(_recipientName).ToLower());
			//sb.Append(_recipientAddress);
			//sb.Append(HanZiToPinYin.Convert(_recipientAddress).ToLower());
			//sb.Append(_phoneNumber);
			//sb.Append(_mobileNumber);
			//sb.Append(_dealTime.ToString());
			//sb.Append(_payingTime.ToString());
			//sb.Append(SimplifyItemSubject(_items));
			//sb.Append(_shipmentNumber);
			//sb.Append(_shipmentCompany);
			//sb.Append(_remark);
			//sb.Append(_closingReason);
			//sb.Append(_editedRecipientAddress);
			//if (!string.IsNullOrEmpty(_editedRecipientAddress))
			//    sb.Append(HanZiToPinYin.Convert(_editedRecipientAddress).ToLower());
			
			//string s = sb.ToString();
			
			foreach (string key in keys)
			{
				//if (s.Contains(key))
				//    return true;
				if (_buyerAccount.Contains(key))
					return true;
				if (_recipientName.Contains(key))
					return true;
				if (HanZiToPinYin.Convert(_recipientName).ToLower().Contains(key.ToLower()))
					return true;
				if (SimplifyItemSubject(_items).Contains(key))
					return true;
				if (_remark.Contains(key))
					return true;
				if (_shipmentNumber.Contains(key))
					return true;
				if (_mobileNumber.Contains(key))
					return true;
				if (_phoneNumber.Contains(key))
					return true;
				if (_buyerRemark.Contains(key))
					return true;
				if (_recipientAddress.Contains(key))
					return true;
				if (HanZiToPinYin.Convert(_recipientAddress).ToLower().Contains(key.ToLower()))
					return true;
				if (_editedRecipientAddress.Contains(key))
					return true;
				if (!string.IsNullOrEmpty(_editedRecipientAddress) && HanZiToPinYin.Convert(_editedRecipientAddress).ToLower().Contains(key.ToLower()))
					return true;
				if (_alipayAccount.Contains(key))
					return true;
				if (_orderId.Contains(key))
					return true;
				if (_dealTime.ToString().Contains(key))
					return true;
				if (_payingTime.ToString().Contains(key))
					return true;
				if (_shipmentCompany.Contains(key))
					return true;
				if (_closingReason.Contains(key))
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
			set { _orderStatus = value; }
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
			set 
			{ 
				_editedRecipientAddress = value; 
				// Added by KK on 2016/10/18.
				if (!string.IsNullOrEmpty(_editedRecipientAddress) && _editedRecipientAddress.Equals("否"))
					_editedRecipientAddress = string.Empty;
			}
		}
		
		public bool Prepared
		{
			get { return _prepared; }
			set { _prepared = value; }
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

		public bool PreparedNingbo
		{
			get { return _preparedNingbo; }
			set { _preparedNingbo = value; }
		}
		
		public void PrepareNingbo()
		{
			_preparedNingbo = true;
			if (null != this.OnStatusChanged)
				this.OnStatusChanged(this, EventArgs.Empty);
		}

		public bool LocalPreparedNingbo
		{
			get { return _localPreparedNingbo; }
			set { _localPreparedNingbo = value; }
		}
		
		public void LocalPrepareNingbo()
		{
			_localPreparedNingbo = true;
			if (null != this.OnStatusChanged)
				this.OnStatusChanged(this, EventArgs.Empty);
		}
		
		// Added by KK on 2017/10/13.
		public bool YundaPrepared
		{
			get { return _yundaPrepared; }
		}
		
		public string YundaTrackingNumber
		{
			get { return _yundaTrackingNumber; }
		}
		
		public void PrepareYunda()
		{
			_yundaPrepared = true;
			if (null != this.OnStatusChanged)
				this.OnStatusChanged(this, EventArgs.Empty);
		}
		
		public void PrepareYunda(string yundaTrackingNumber)
		{
			PrepareYunda();
			_yundaTrackingNumber = yundaTrackingNumber;
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
		
		public string ShopName
		{
			get { return _shopName; }
		}
		
		public string GetShipmentCompany()
		{
			if (_shipmentCompany.Equals("其他") && (_shipmentNumber.StartsWith("96") || _shipmentNumber.StartsWith("297808")))
				return "DHL";
			return _shipmentCompany;
		}
		
		public string AlipayNumber
		{
			get { return _alipayNumber; }
			set { _alipayNumber = value; }
		}
		
		public ShipmentCompanies AutoTransmissionProcessor
		{
			get { return _autoTransmissionProcessor; }
			set
			{
				if (value != _autoTransmissionProcessor)
				{
					_autoTransmissionProcessor = value;
					if (null != this.OnTransmissionChanged)
						this.OnTransmissionChanged(this, EventArgs.Empty);
				}
			}
		}
	}
}