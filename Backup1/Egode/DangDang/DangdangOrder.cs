using System;
using System.Collections.Generic;
using System.Text;

namespace Dangdang
{
	public class DangdangOrder
	{
		private readonly string _orderId;
		private readonly string _sellerId;
		private readonly string _recipientName;
		private readonly string _idNumber;
		private readonly string _mobile;
		private readonly string _address;
		private readonly string _deliveryType;
		private readonly string _deliveryTime;
		private readonly string _paymentType;
		private readonly float _totalMoney;
		private readonly DateTime _dealTime;
		private readonly DateTime _payTime;
		private readonly string _invoice;
		private readonly string _status;
		private readonly float _fee;
		private readonly float _tax;
		private readonly string _productCode;
		private readonly int _count;
		private readonly float _price;
		private readonly string _paymentId;
		private readonly string _device;
		
		private readonly DateTime _consigningTime;
		private readonly string _shipmentCompany;
		private readonly string _shipmentNumber;
		
		public DangdangOrder(
			string orderId, string sellerId, 
			string recipientName, string idNumber, string mobile, string address, 
			string deliveryType, string deliveryTime, 
			string paymentType, float totalMoney, 
			DateTime dealTime, DateTime payTime,
			string invoice, string status, float fee, float tax, 
			string productCode, int count, float price, 
			string paymentId, 
			string device,
			DateTime consigningTime, string shipmentCompany, string shipmentNumber)
		{
			_orderId = orderId;
			_sellerId = sellerId;
			_recipientName = recipientName;
			_idNumber = idNumber;
			_mobile = mobile;
			_address = address;
			_deliveryType = deliveryType;
			_deliveryTime = deliveryTime;
			_paymentType = paymentType;
			_totalMoney = totalMoney;
			_dealTime = dealTime;
			_payTime = payTime;
			_invoice = invoice;
			_status = status;
			_fee = fee;
			_productCode = productCode;
			_tax = tax;
			_count = count;
			_price = price;
			_paymentId = paymentId;
			_device = device;
			
			_consigningTime = consigningTime;
			_shipmentCompany = shipmentCompany;
			_shipmentNumber = shipmentNumber;
		}
		
		public string OrderId
		{
			get { return _orderId; }
		}
		
		public string SellerId
		{
			get { return _sellerId; }
		}
		
		public string RecipientName
		{
			get { return _recipientName; }	
		}
		
		public string IdNumber
		{
			get { return _idNumber; }
		}
		
		public string Mobile
		{
			get { return _mobile; }
		}
		
		public string Address
		{
			get { return _address; }
		}
		
		public string DeliveryType
		{
			get { return _deliveryType; }
		}
		
		public string DeliveryTime
		{
			get { return _deliveryTime; }
		}
		
		public string PaymentType
		{
			get { return _paymentType; }
		}
		
		public float TotalMoney
		{
			get { return _totalMoney; }
		}
		
		public DateTime DealTime
		{
			get { return _dealTime; }
		}
		
		public DateTime PayTime
		{
			get { return _payTime; }
		}
		
		public string Invoice
		{
			get { return _invoice; }
		}
		
		public string Status
		{
			get { return _status; }
		}
		
		public float Fee
		{
			get { return _fee; }
		}
		
		public float Tax
		{
			get { return _tax; }
		}
		
		// comment by KK on 2015/12/24.:
		// 当当系统中的code, 每个商品链接对应不同的code.
		// 对于同1个商品, 不同链接, 例如1盒装、3盒装等, 此code不同.
		// 因此, 此code不完全对应实际商品.
		// 但是, 此code的前面部分(StartWith), 总是以对应实际商品的唯1ID开始.
		// 例如, 相同奶粉的1盒装和3盒装的链接, 其code的前面部分完全相同.
		public string ProductCode
		{
			get { return _productCode; }
		}

		// 接上述注释
		// UniqueProductCode即为从上述ProductCode中解析出对应实际商品的唯一ID.
		// ----------------
		// 2015/12/24规范:
		// 除了uniqueCode, 其后可能附加有数量信息, 格式为: APT-001-000-x3
		// 其中, APT-001-000是unique code, "-"是分隔符, "x"表示后面是数量, 3是数量, 也可能2位数, 暂不考虑3位数.
		// ----------------
		public string UniqueProductCode
		{
			get
			{
				for (int i = 32; i > 0; i--)
				{
					if (_productCode.EndsWith(string.Format("-x{0}", i)))
						return _productCode.Replace(string.Format("-x{0}", i), string.Empty);
				}
				return _productCode;
			}
		}

		// comment by KK on 2015/12/24.
		// 此数量仅代表客户拍下的件数, 对于组合装, 例如3盒装, 客户拍下1件, 
		// 则此count=1, 实际发货数量应为3
		public int Count
		{
			get { return _count; }
		}
		
		// 接上述描述, 此处根据productCode计算实际数量.
		public int ActualCount
		{
			get
			{
				for (int i = 32; i > 0; i--)
				{
					if (_productCode.EndsWith(string.Format("-x{0}", i)))
						return i*_count;
				}
				return _count;
			}
		}
		
		public float Price
		{
			get { return _price; }
		}
		
		public string PaymentId
		{
			get { return _paymentId; }
		}
		
		public string Device
		{
			get { return _device; }
		}
		
		public DateTime ConsigningTime
		{
			get { return _consigningTime; }
		}
		
		public string ShipmentCompany
		{
			get { return _shipmentCompany; }
		}
		
		public string ShipmentNumber
		{
			get { return _shipmentNumber; }
		}
	}
}