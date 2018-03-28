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
		private readonly string _receiverPhone;
		private readonly string _shipmentNumber;
		private readonly float _weight;
		private readonly string _status; // just available for online postnl packet info.
		private readonly string _addr;
	
		public PdfPacketInfo(PacketTypes type, string recipientName, string receiverPhone, string shipmentNumber, float weight, string status, string addr)
		{
			_type = type;
			_recipientName = recipientName;
			_receiverPhone = receiverPhone;
			_shipmentNumber = shipmentNumber;
			_weight = weight;
			_status = status;
			_addr = addr;
		}
		
		public PacketTypes Type
		{
			get { return _type; }
		}
		
		public string RecipientName
		{
			get { return _recipientName; }
		}
		
		public string ReceiverPhone
		{
			get { return _receiverPhone; }
		}
		
		public string ShipmentNumber
		{
			get { return _shipmentNumber; }
		}
		
		public float Weight
		{
			get { return _weight; }
		}
		
		public string Status
		{
			get { return _status; }
		}
		
		public string Address
		{
			get { return _addr; }
		}
		
		public static PdfPacketInfo GetPdfPacketInfo(string filename)
		{
			if (!File.Exists(filename))
				return null;

			try
			{
				Egode.PdfParser parser = new Egode.PdfParser(filename);
				string s = parser.GetText();
				parser.Close();
				//Trace.WriteLine(s);
				
				// DHL超市-postNL.
				// 收件人中文地址单\n收件人：程燕(cheng yan)\n收件地址：江苏省苏州市工业园区金鸡湖大\n道1355号二期e301\n邮编：215000\n收件人电话：+8615312197535\n包裹重量：10.0KG\n包裹唯一号：DE141107P0010910\nCHN
				Regex r = new Regex("收件人中文地址单");
				Match m = r.Match(s);
				if (m.Success)
				{
					string recipientName=string.Empty, recipientPhone=string.Empty, shipmentNumber=string.Empty;
					float weight;

					r = new Regex(@"收件人：(\w*)");
					m = r.Match(s);
					if (m.Success)
					{
						recipientName = m.Groups[1].Value;

						r = new Regex(@"收件人电话：\+*(\d+)");
						m = r.Match(s);
						if (m.Success)
						{
							recipientPhone = m.Groups[1].Value;

							r = new Regex(@"包裹唯一号：(.*)\s");
							m = r.Match(s);
							if (m.Success)
							{
								shipmentNumber = m.Groups[1].Value;

								r = new Regex(@"包裹唯一号：(.*)\s");
								m = r.Match(s);
								if (m.Success)
								{
									shipmentNumber = m.Groups[1].Value;

									r = new Regex(@"包裹重量：(.*)KG\s");
									m = r.Match(s);
									if (m.Success)
									{
										weight = float.Parse(m.Groups[1].Value);
								
										return new PdfPacketInfo(PacketTypes.Time24_PostNL, recipientName, recipientPhone, shipmentNumber, weight, string.Empty, string.Empty);
									}
								}
							}
						}
					}
				}
				
				// DHL超市.
				r = new Regex(@"Ref. No.:\s\d{12}\s(.*)\sEntgelt Bezahlt\sPort Payé");
				m = r.Match(s);
				if (m.Success) 
				{
					string packetInfo = m.Groups[1].Value; // format: FamilyName GivenName123456    4-16,5
					r = new Regex(@"(\w* \D*)(\d{6})\s*(\d*-\d*),(\d*)");
					m = r.Match(packetInfo);
					if (m.Success)
						return new PdfPacketInfo(PacketTypes.Supermarket, m.Groups[1].Value, string.Empty, "297808" + m.Groups[2].Value, float.Parse(m.Groups[4].Value), string.Empty, string.Empty);
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
						float weight = float.Parse(m.Groups[2].Value.Replace(",", "."));
						string receiverPhone = string.Empty;

						r = new Regex("Receiver phone:\n(.*)\n");
						m = r.Match(s);
						if (m.Success)
							receiverPhone = m.Groups[1].Value;
						
						return new PdfPacketInfo(PacketTypes.Rainbow, recipientName, receiverPhone, shipmentNumber, weight, string.Empty, string.Empty);
					}
				}
				
				// DHL Express.
				r = new Regex(@"WAYBILL (\d{2} \d{4} \d{4})");
				m = r.Match(s);
				if (m.Success)
				{
					string shipmentNumber = m.Groups[1].Value;
					shipmentNumber = shipmentNumber.Replace(" ", string.Empty);

					string recipientName = string.Empty;
					r = new Regex(@"To\s(.*)\s");
					m = r.Match(s);
					if (m.Success)
						recipientName = m.Groups[1].Value;

					string receiverPhone = string.Empty;
					r = new Regex(@"Contact:\s.*\s(\d*)\s");
					m = r.Match(s);
					if (m.Success)
						receiverPhone = m.Groups[1].Value;

					float weight = 0;
					r = new Regex(@"Total Weight: (\d*) kg");
					m = r.Match(s);
					if (m.Success)
						weight = float.Parse(m.Groups[1].Value);
					
					return new PdfPacketInfo(PacketTypes.Express, recipientName, receiverPhone, shipmentNumber, weight, string.Empty, string.Empty);
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
			}

			return null;
		}

		public static List<PdfPacketInfoEx> GetPdfPacketInfos(string folder, bool includeSubdir)
		{
			string[] pdfFiles = Directory.GetFiles(folder, "*.pdf", includeSubdir ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
			if (null == pdfFiles || pdfFiles.Length <= 0)
				return null;

			List<PdfPacketInfoEx> pdfPackets = new List<PdfPacketInfoEx>();
			foreach (string pdfFile in pdfFiles)
			{
				PdfPacketInfoEx ppi = PdfPacketInfoEx.GetPdfPacketInfoEx(pdfFile);
				if (null == ppi)
					continue;
				pdfPackets.Add(ppi);
				Trace.WriteLine(string.Format("{0},{1},{2},{3}", ppi.Type, ppi.RecipientName, ppi.ShipmentNumber, ppi.Weight));
			}

			return pdfPackets;
		}
	}
	
	public class PdfPacketInfoEx : PdfPacketInfo
	{
		public event EventHandler OnDataChanged;

		private readonly string _filename;
		private string _matchedRecipientName; // in chinese.
		private bool _updated; // 匹配到并且成功替换了excel中对应值. (即使匹配到, 替换过程可能出错)
		
		public PdfPacketInfoEx(string filename, PacketTypes type, string recipientName, string receiverPhone, string shipmentNumber, float weight, string status, string addr) : base(type, recipientName, receiverPhone, shipmentNumber, weight, status, addr)
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
			set 
			{ 
				_matchedRecipientName = value; 
				if (null != this.OnDataChanged)
					this.OnDataChanged(this, EventArgs.Empty);
			}
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
			
			return new PdfPacketInfoEx(filename, ppi.Type, ppi.RecipientName, ppi.ReceiverPhone, ppi.ShipmentNumber, ppi.Weight, ppi.Status, ppi.Address);
		}
		
		// 根据给定的名字在给定的集合中查找对应值.
		// recipientName可以是中文, 也可以是拼音名字, 最终都转化为拼音进行匹配.
		// 匹配时, 删除拼音名字中的所有空格, 忽略大小写.
		public static PdfPacketInfoEx GetItemByRecipientName(string recipientName, List<PdfPacketInfoEx> pdfPackets, bool ignoreMatched)
		{
			if (string.IsNullOrEmpty(recipientName))
				return null;
			if (null == pdfPackets || pdfPackets.Count <= 0)
				return null;
			
			foreach (PdfPacketInfoEx ppi in pdfPackets)
			{
				if (ignoreMatched && !string.IsNullOrEmpty(ppi.MatchedRecipientName))
					continue;

				string pinyin1 = HanZiToPinYin.Convert(recipientName).Replace(" ", string.Empty).Trim().ToLower();
				string pinyin2 = HanZiToPinYin.Convert(ppi.RecipientName).Replace(" ", string.Empty).Trim().ToLower();
				
				if (pinyin1.Equals(pinyin2))
					return ppi;
			}
			return null;
		}
	}
}