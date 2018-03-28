using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;

namespace Egode
{
	public class PdfPacketInfo
	{
		private readonly PacketTypes _type;
		private readonly string _recipientName;
		private readonly string _shipmentNumber;
		private readonly int _weight;
	
		public PdfPacketInfo(PacketTypes type, string recipientName, string shipmentNumber, int weight)
		{
			_type = type;
			_recipientName = recipientName;
			_shipmentNumber = shipmentNumber;
			_weight = weight;
		}
		
		public PacketTypes Type
		{
			get { return _type; }
		}
		
		public string RecipientName
		{
			get { return _recipientName; }
		}
		
		public string ShipmentNumber
		{
			get { return _shipmentNumber; }
		}
		
		public int Weight
		{
			get { return _weight; }
		}
		
		public static PdfPacketInfo GetPdfPacketInfo(string filename)
		{
			if (!File.Exists(filename))
				return null;

			Egode.PdfParser parser = new Egode.PdfParser(filename);
			string s = parser.GetText();
			parser.Close();
			//Trace.WriteLine(s);
			
			// DHL超市.
			Regex r = new Regex(@"Ref. No.:\s\d{12}\s(.*)\sEntgelt Bezahlt\sPort Payé");
			Match m = r.Match(s);
			if (m.Success) 
			{
				string packetInfo = m.Groups[1].Value; // format: FamilyName GivenName123456    4-16,5
				r = new Regex(@"(\w* \D*)(\d{6})\s*(\d*-\d*),(\d*)");
				m = r.Match(packetInfo);
				if (m.Success)
					return new PdfPacketInfo(PacketTypes.Supermarket, m.Groups[1].Value, "297808" + m.Groups[2].Value, int.Parse(m.Groups[4].Value));
			}
			
			// 彩虹.
			r = new Regex(@"DEUTSCHLAND\s*To\s(\w* \w*)\s");
			m = r.Match(s);
			if (m.Success) // 
			{
				//foreach (Group g in m.Groups)
				//    Trace.WriteLine("Group:" + g.Value);
				
				string recipientName = m.Groups[1].Value;
				
				r = new Regex(@"Shipment No.*: (\d{12})\s(\d*,\d*)\s*kg");
				m = r.Match(s);
				if (m.Success)
				{
					string shipmentNumber = m.Groups[1].Value;
					int weight = (int)float.Parse(m.Groups[2].Value.Replace(",", "."));
					return new PdfPacketInfo(PacketTypes.Rainbow, recipientName, shipmentNumber, weight);
				}
			}

			return null;
		}
	}
	
	public class PdfPacketInfoEx : PdfPacketInfo
	{
		private readonly string _filename;
		private string _matchedRecipientName; // in chinese.
		private bool _updated; // 匹配到并且成功替换了excel中对应值. (即使匹配到, 替换过程可能出错)
		
		public PdfPacketInfoEx(string filename, PacketTypes type, string recipientName, string shipmentNumber, int weight) : base(type, recipientName, shipmentNumber, weight)
		{
			_filename = filename;
		}
		
		public string Filename
		{
			get { return _filename; }
		}
		
		public string MatchedRecipientName
		{
			get { return _matchedRecipientName; }
			set { _matchedRecipientName = value; }
		}
		
		public bool Updated
		{
			get { return _updated; }
			set { _updated = value; }
		}

		public static PdfPacketInfoEx GetPdfPacketInfoEx(string filename)
		{
			PdfPacketInfo ppi = PdfPacketInfo.GetPdfPacketInfo(filename);
			if (null == ppi)
				return null;
			
			return new PdfPacketInfoEx(filename, ppi.Type, ppi.RecipientName, ppi.ShipmentNumber, ppi.Weight);
		}
		
		// 根据给定的拼音名字在给定的集合中查找对应值.
		// 匹配时, 删除拼音名字中的所有空格, 忽略大小写.
		public static PdfPacketInfoEx GetItem(string recipientPinyinName, List<PdfPacketInfoEx> pdfPackets, bool ignoreMatched)
		{
			if (string.IsNullOrEmpty(recipientPinyinName))
				return null;
			if (null == pdfPackets || pdfPackets.Count <= 0)
				return null;
			
			foreach (PdfPacketInfoEx ppi in pdfPackets)
			{
				if (ignoreMatched && !string.IsNullOrEmpty(ppi.MatchedRecipientName))
					continue;
			
				if (recipientPinyinName.Replace(" ", string.Empty).Trim().ToLower().Equals(ppi.RecipientName.Replace(" ", string.Empty).Trim().ToLower()))
					return ppi;
			}
			return null;
		}
	}
}