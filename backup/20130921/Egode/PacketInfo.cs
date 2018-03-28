using System;
using System.Collections.Generic;
using System.Text;

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
		
		private string _productInfo;

		public PacketInfo(
			string orderId, 
			PacketTypes packetType, int weight, int price,
			string fullAddress, string phoneNumber, string homePhoneNumber,
			string productInfo) : base(packetType, weight, price)
		{
			_orderId = orderId;
			_fullAddress = fullAddress;
			_phoneNumber = phoneNumber;
			_homePhoneNumber = homePhoneNumber;
			_productInfo = productInfo;
		}
		
		public PacketInfo Clone()
		{
			PacketInfo pi = new PacketInfo(_orderId, base.Type, base.Weight, base.Price, _fullAddress, _phoneNumber, _homePhoneNumber, _productInfo);
			pi._recipientNameCn = _recipientNameCn;
			pi._recipientNameEn = _recipientNameEn;
			pi._provinceCityCn = _provinceCityCn;
			pi._provinceCityEn = _provinceCityEn;
			pi._addressCn = _addressCn;
			pi._addressEn = _addressEn;
			pi._postCode = _postCode;
			
			return pi;
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
		
		public string ProductInfo
		{
			get { return _productInfo; }
			set { _productInfo = value; }
		}
	}
}