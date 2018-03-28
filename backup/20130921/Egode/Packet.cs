using System;
using System.Collections.Generic;
using System.Text;

namespace Egode
{
	public enum PacketTypes
	{
		Unknown = 0,
		Rainbow = 1, // 彩虹国际
		Supermarket = 2, // DHL超市
		Dealworthier = 3, // Dealworthier
		Ouhua = 4 // 欧华国际
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
				s += "(彩虹) ";
			else if (_type == PacketTypes.Supermarket)
				s += "(超市) ";
			else if (_type == PacketTypes.Dealworthier)
				s += "(DW) ";
			else if (_type == PacketTypes.Ouhua)
				s += "(欧华) ";
			
			s += ((float)((float)_weight/1000)).ToString("0.0") + "kg";
			return s;
		}
	}
}