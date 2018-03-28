using System;
using System.Collections.Generic;
using System.Text;

namespace Egode
{
	public enum PacketTypes
	{
		Unknown = 0,
		Rainbow = 1, // �ʺ����
		Supermarket = 2, // DHL����
		Dealworthier = 3, // Dealworthier
		Ouhua = 4, // ŷ������
		Express = 5, // DHL Express
		Hanslord = 6,
		Time24_PostNL = 7, 
		Time24_MilkExpress = 8, // 2015/12/23.
		Time24_DHL = 9, // 2015/12/23
	}

	public class Packet
	{
		private PacketTypes _type;
		private int _weight; // g.
		private int _price;
		
		public Packet(PacketTypes type, int weight, int price)
		{
			_type = type;
			_weight = weight;
			_price = price;
		}
		
		public PacketTypes Type
		{
			get { return _type;	}
			set { _type = value; }
		}
		
		public int Weight
		{
			get { return _weight; }
			set { _weight = value; }
		}
		
		public int Price
		{
			get { return _price; }
			set { _price = value; }
		}

		public override string ToString()
		{
			string s = string.Empty;
			
			if (_type == PacketTypes.Rainbow)
				s += "(�ʺ�) ";
			else if (_type == PacketTypes.Supermarket)
				s += "(����) ";
			else if (_type == PacketTypes.Dealworthier)
				s += "(DW) ";
			else if (_type == PacketTypes.Ouhua)
				s += "(ŷ��Eco) ";
			else if (_type == PacketTypes.Hanslord)
				s += "(Hanslord) ";
			else if (_type == PacketTypes.Time24_PostNL)
				s += "(Time24-PostNL) ";
			
			s += ((float)((float)_weight/1000)).ToString("0.0") + "kg";
			return s;
		}
		
		// Removed by KK on 2015/12/24.
		// Obsoleted.
		//public static string GetPacketTypePrefix(PacketTypes t)
		//{
		//    switch (t)
		//    {
		//        case PacketTypes.Time24_PostNL:
		//            return "N";
		//        case PacketTypes.Express:
		//            return "E";
		//        default:
		//            return "P"; // packet
		//    }
		//}

		public static string GetPacketTypeDesc(PacketTypes pt)
		{
			switch (pt)
			{
				case PacketTypes.Time24_DHL:
					return "Time24-DHL";
				case PacketTypes.Time24_PostNL:
					return "Time24-PostNL";
				case PacketTypes.Time24_MilkExpress:
					return "Time24-EMS";
				case PacketTypes.Unknown:
					return "δ֪";
			}
			return "δ֪";
		}
	}
}