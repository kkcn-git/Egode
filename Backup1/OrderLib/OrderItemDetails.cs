using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OrderLib
{
	public class OrderItemDetails
	{
		private readonly string _orderId;
		private readonly string _subject;
		private readonly float _price;
		private readonly int _amount;
		private readonly string _code;
		//private readonly string _prop;
		private Order.OrderStatus _status;
		private string _skuCode; // Added by KK on 2016/06/05.
		
		//private string _subjectId; // index for subject in subject list.

		//public OrderItemDetails(string orderId, string item, float price, int amount, string prop, Order.OrderStatus status)
		public OrderItemDetails(string orderId, string subject, float price, int amount, string code, Order.OrderStatus status, string skuCode)
		{
			_orderId = orderId;
			_price = price;
			_amount = amount;
			_code = code;
			//_prop = prop;
			_status = status;

			_subject = subject;
			// Removed by KK on 2016/06/05.
			//_subjectId = SubjectInfo.GetSubjectId(_subject);
			
			_skuCode = skuCode;
		}
		
		public static OrderItemDetails Parse(string infoString)
		{
			// Added by KK on 2017/09/24.
			// Taobao start to use "null" for empty string instead of "".
			infoString = infoString.Replace("\"null\"", "\"\"");
			infoString = infoString.Replace("\"'null\"", "\"\"");

			int start = infoString.IndexOf("\"");
			int end = infoString.IndexOf("\"", start + 1);
			string orderId = infoString.Substring(start + 1, end - start - 1);
			
			start = infoString.IndexOf("\"", end + 1);
			end = infoString.IndexOf("\"", start + 1);
			string subject = infoString.Substring(start + 1, end - start - 1);

			start = infoString.IndexOf("\"", end + 1);
			end = infoString.IndexOf("\"", start + 1);
			float price = float.Parse(infoString.Substring(start + 1, end - start - 1));

			start = infoString.IndexOf("\"", end + 1);
			end = infoString.IndexOf("\"", start + 1);
			int amount = int.Parse(infoString.Substring(start + 1, end - start - 1));

			start = infoString.IndexOf("\"", end + 1);
			end = infoString.IndexOf("\"", start + 1);
			string code = infoString.Substring(start + 1, end - start - 1);
			
			start = infoString.IndexOf("\"", end + 1);
			end = infoString.IndexOf("\"", start + 1);
			string prop = infoString.Substring(start + 1, end - start - 1);
			
			start = infoString.IndexOf("\"", end + 1);
			end = infoString.IndexOf("\"", start + 1);
			start = infoString.IndexOf("\"", end + 1);
			end = infoString.IndexOf("\"", start + 1);

			start = infoString.IndexOf("\"", end + 1);
			end = infoString.IndexOf("\"", start + 1);
			string status = infoString.Substring(start + 1, end - start - 1);
			
			// Added by KK on 2016/06/05.
			// sku code.
			start = infoString.IndexOf("\"", end + 1);
			end = infoString.IndexOf("\"", start + 1);
			string skuCode = infoString.Substring(start + 1, end - start - 1);
			
			return new OrderItemDetails(
				orderId, 
				string.IsNullOrEmpty(prop) ? subject : string.Format("{0} ({1})", subject, prop), 
				price, amount, code, Order.ParseOrderStatus(status), skuCode);
		}
		
		public static List<OrderItemDetails> GetOrderItemDetailsList(string orderId, List<OrderItemDetails> orders)
		{
			List<OrderItemDetails> list = new List<OrderItemDetails>();
			foreach (OrderItemDetails otd in orders)
			{
				if (otd.OrderId.Equals(orderId))
					list.Add(otd);
			}
			
			return list;
		}
		
		// include subject, price, amount and status.
		public static string GetOrderItemDetailsString(List<OrderItemDetails> orderDetailsList)
		{
			if (null == orderDetailsList || orderDetailsList.Count <= 0)
				return string.Empty;
			
			StringBuilder sb = new StringBuilder();
			
			foreach (OrderItemDetails otd in orderDetailsList)
			{
				// Modified on 2014/10/08.
				//sb.Append(string.Format("{0}¡î{1}¡î{2}¡î{3}", otd.Subject, otd.Price, otd.Amount, otd.Status));
				//sb.Append("¡ï");
				//sb.Append(string.Format("{1}{0}{2}{0}{3}{0}{4}", Order.ITEM_INFO_SEPARATOR, otd.Subject, otd.Price, otd.Amount, otd.Status)); 
				sb.Append(string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}", Order.ITEM_INFO_SEPARATOR, otd.Subject, otd.Price, otd.Amount, otd.Status, otd.SkuCode)); // sku code was added by KK on 2016/06/06.
				sb.Append(Order.ITEM_SEPARATOR);
			}
			
			sb.Remove(sb.Length-1, 1);
			return sb.ToString();
		}
		
		public string OrderId
		{
			get { return _orderId; }
		}
		
		public string Subject
		{
			get { return _subject; }
		}
		
		//public string SubjectId
		//{
		//    get { return _subjectId; }
		//}
		
		public float Price
		{
			get { return _price; }
		}
		
		public int Amount
		{
			get { return _amount; }
		}
		
		public string Code
		{
			get { return _code; }
		}
		
		//public string Prop
		//{
		//    get { return _prop; }
		//}
		
		public Order.OrderStatus Status
		{
			get { return _status; }
		}
		
		public string SkuCode
		{
			get { return _skuCode; }
		}
	}
}