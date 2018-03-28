using System;
using System.Collections.Generic;
using System.Text;

namespace Egode.Ningbo
{
	public class NingboTableColumnInfo
	{
		// all following property is colomn index.
		private int _orderId;
		private int _logisticsCompany;
		private int _mailNumber;
		private int _recipientName;
		private int _mobile;
		private int _province;
		private int _city;
		private int _district;
		private int _streetAddr;
		private int _productNingboCode;
		private int _count;
		
		public int OrderId
		{
			get { return _orderId; }
			set { _orderId = value; }
		}
		
		public int LogisticsCompany
		{
			get { return _logisticsCompany; }
			set { _logisticsCompany = value; }
		}
		
		public int MailNumber
		{
			get { return _mailNumber; }
			set { _mailNumber = value; }
		}
		
		public int RecipientName
		{
			get { return _recipientName; }
			set { _recipientName = value; }
		}
		
		public int Mobile
		{
			get { return _mobile; }
			set { _mobile = value; }
		}
		
		public int Province
		{
			get { return _province; }
			set { _province = value; }
		}
		
		public int City
		{
			get { return _city; }
			set { _city = value; }
		}
		
		public int District
		{
			get { return _district; }
			set { _district = value; }
		}
		
		public int StreetAddr
		{
			get { return _streetAddr; }
			set { _streetAddr = value; }
		}
		
		public int ProductNingboCode
		{
			get { return _productNingboCode; }
			set { _productNingboCode = value; }
		}
		
		public int Count
		{
			get { return _count; }
			set { _count = value; }
		}
	}
}