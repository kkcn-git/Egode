using System;
using System.Collections.Generic;
using System.Text;

namespace Egode
{
	public class NingboOrder
	{
		private static List<NingboOrder> _orders;
	
		private string _logisticsCompany;
		private string _mailNumber;
		private string _taobaoOrderId;
		private string _recipientName;
		private string _mobile;
		private string _province;
		private string _city;
		private string _district;
		private string _streetAddr;
		private List<SoldProductInfo> _soldProducts;
		private string _idInfo;
		private string _alipayNumber;
		private string _linkedTaobaoOrderIds; // 合并出单时, 其他关联订单id, 以半角逗号分隔. 可以为空.
		
		public NingboOrder(
			string logisticsCompany, string mailNumber, string taobaoOrderId, 
			string recipientName, string mobile, 
			string province, string city, string district, string streetAddr,
			List<SoldProductInfo> soldProducts,
			string idInfo, string alipayNumber)
		{
			_logisticsCompany = logisticsCompany;
			_mailNumber = mailNumber.Trim().Replace("-", string.Empty);
			_taobaoOrderId = taobaoOrderId;
			_recipientName = recipientName;
			_mobile = mobile;
			_province = province;
			_city = city;
			_district = district;
			_streetAddr = streetAddr;
			_soldProducts = soldProducts;
			_idInfo = idInfo;
			_alipayNumber = alipayNumber;
		}
		
		public static List<NingboOrder> Orders
		{
			get
			{
				if (null == _orders)
					_orders = new List<NingboOrder>();
				return _orders;
			}
		}
		
		public string LogisticsCompany
		{
			get { return _logisticsCompany; }
		}
		
		public string MailNumber
		{
			get { return _mailNumber; }
		}
		
		public string TaobaoOrderId
		{
			get { return _taobaoOrderId; }
		}
		
		public string LinkedTaobaoOrderIds
		{
			get { return _linkedTaobaoOrderIds; }
			set { _linkedTaobaoOrderIds = value; }
		}
		
		public string RecipientName
		{
			get { return _recipientName; }
		}
		
		public string Mobile
		{
			get { return _mobile; }
		}
		
		public string Province
		{
			get { return _province; }
		}
		
		public string City
		{
			get { return _city; }
		}
		
		public string District
		{
			get { return _district; }
		}
		
		public string StreetAddr
		{
			get { return _streetAddr; }
		}
		
		public List<SoldProductInfo> SoldProducts
		{
			get { return _soldProducts; }
		}
		
		public string IdInfo
		{
			get { return _idInfo; }
		}
		
		public string AlipayNumber
		{
			get { return _alipayNumber; }
		}
	}
}