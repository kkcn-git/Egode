using System;
using System.Collections.Generic;
using System.Text;
using OrderLib;
using System.Xml;

namespace Egode
{
	public class Blacklist
	{
		private readonly string _op;
		private readonly DateTime _date;
		private readonly string _red;
		private readonly string _blue;
		private readonly string _comment;
		
		private static List<Blacklist> _blacklists;
		
		public Blacklist(string op, DateTime date, string red, string blue, string comment)
		{
			_op = op;
			_date = date;
			_red = red;
			_blue = blue;
			_comment = comment;
		}
		
		public string Op
		{
			get { return _op; }
		}
		
		public DateTime Date
		{
			get { return _date; }
		}

		public string Red
		{
			get { return _red; }
		}

		public string Blue
		{
			get { return _blue; }
		}

		public string Comment
		{
			get { return _comment; }
		}
		
		public static List<Blacklist> Blacklists
		{
			get 
			{
				if (null == _blacklists)
					_blacklists = new List<Blacklist>();
				return _blacklists;
			}
		}
		
		public static Blacklist MatchRed(Order order)
		{
			if (null == _blacklists)
				return null;
			foreach (Blacklist b in _blacklists)
			{
				if (string.IsNullOrEmpty(b.Red))
					continue;
				string[] kws = b.Red.Replace("��", ",").Replace(", ", ",").Replace(" ,", ",").Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string kw in kws)
				{
					if (order.BuyerAccount.ToLower().Contains(kw.ToLower().Trim()))
						return b;
					else if (order.RecipientName.ToLower().Contains(kw.ToLower().Trim()))
						return b;
					else if (order.RecipientAddress.ToLower().Contains(kw.ToLower().Trim()))
						return b;
					else if (order.EditedRecipientAddress.ToLower().Contains(kw.ToLower().Trim()))
						return b;
					else if (order.PhoneNumber.ToLower().Contains(kw.ToLower().Trim()))
						return b;
					else if (order.MobileNumber.ToLower().Contains(kw.ToLower().Trim()))
						return b;
					else if (order.BuyerRemark.ToLower().Contains(kw.ToLower().Trim()))
						return b;
					else if (order.Remark.ToLower().Contains(kw.ToLower().Trim()))
						return b;
				}
			}
			return null;
		}

		public static Blacklist MatchBlue(Order order, out float matchPercentage)
		{
			matchPercentage = 0f;
			if (null == _blacklists)
				return null;
			foreach (Blacklist b in _blacklists)
			{
				if (string.IsNullOrEmpty(b.Blue))
					continue;
				if (b.Blue.Contains("������"))
					System.Diagnostics.Trace.WriteLine("");
				string[] kws = b.Blue.Replace("��", ",").Replace(", ", ",").Replace(" ,", ",").Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
				int c = 0;
				foreach (string kw in kws)
				{
					if (order.BuyerAccount.ToLower().Contains(kw.ToLower().Trim()))
						c++;
					else if (order.RecipientName.ToLower().Contains(kw.ToLower().Trim()))
						c++;
					else if (order.RecipientAddress.ToLower().Contains(kw.ToLower().Trim()))
						c++;
					else if (order.EditedRecipientAddress.ToLower().Contains(kw.ToLower().Trim()))
						c++;
					else if (order.PhoneNumber.ToLower().Contains(kw.ToLower().Trim()))
						c++;
					else if (order.MobileNumber.ToLower().Contains(kw.ToLower().Trim()))
						c++;
					else if (order.BuyerRemark.ToLower().Contains(kw.ToLower().Trim()))
						c++;
					else if (order.Remark.ToLower().Contains(kw.ToLower().Trim()))
						c++;
				}

				int keywordCount = GetKeywordCount(b.Blue.Replace("��", ",").Replace(", ", ",").Replace(" ,", ","));
				if (c >= Math.Min(2, keywordCount))
				{
					matchPercentage = (float)c / (float)keywordCount;
					return b;
				}
			}
			return null;
		}

		private static int GetKeywordCount(string keyword)
		{
			string[] subkws = keyword.Split(',');
			int c = 0;
			foreach	(string subkw in subkws)
			{
				if (!string.IsNullOrEmpty(subkw.Trim()))
					c++;
			}
			return c;
		}
		
		public static int InitializeBlacklist(string xml)
		{
			if (string.IsNullOrEmpty(xml))
				return -1;

			XmlDocument xmldoc = new XmlDocument();
			xmldoc.LoadXml(xml);
			XmlNodeList nlBlacklist = xmldoc.SelectNodes(".//black");
			if (null == nlBlacklist || nlBlacklist.Count <= 0)
				return -1;

			_blacklists = new List<Blacklist>();

			foreach (XmlNode nodeBlacklist in nlBlacklist)
			{
				string op = nodeBlacklist.Attributes.GetNamedItem("op").Value;
				DateTime date = DateTime.Parse(nodeBlacklist.Attributes.GetNamedItem("time").Value);
				string red = nodeBlacklist.Attributes.GetNamedItem("red").Value;
				string blue = nodeBlacklist.Attributes.GetNamedItem("blue").Value;
				string comment = nodeBlacklist.Attributes.GetNamedItem("comment").Value;
				Blacklist.Blacklists.Add(new Blacklist(op, date, red, blue, comment));
			}
			
			return _blacklists.Count;
		}
		
		// temp code.
		public static void Import()
		{
			XmlDocument xmldoc = new XmlDocument();
			xmldoc.Load(@"E:\taobao\dev\black.xml");
			XmlNode nodeBlacks = xmldoc.SelectSingleNode(".//blacks");
			XmlNode nodeFirst = nodeBlacks.FirstChild;
			
			System.IO.StreamReader r = new System.IO.StreamReader(@"J:\=eur8=\ħ���곤\export_black_20180108.csv", Encoding.UTF8);
			r.ReadLine();
			while (!r.EndOfStream)
			{
				string s = r.ReadLine().Trim();
				if (string.IsNullOrEmpty(s))
					continue;
				string[] info = s.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
				if (info.Length <= 0)
					continue;
				
				XmlElement nodeBlack = xmldoc.CreateElement("black");
				nodeBlacks.InsertBefore(nodeBlack, nodeFirst);
				nodeBlack.SetAttribute("op", "kk");
				nodeBlack.SetAttribute("time", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
				nodeBlack.SetAttribute("red", info[0]);
				nodeBlack.SetAttribute("blue", string.Empty);
				nodeBlack.SetAttribute("comment", info.Length >= 2 ? info[1] : string.Empty);
			}
			r.Close();
			
			xmldoc.Save(@"e:\taobao\dev\blacktest.xml");
		}
		
		// temp code.
		public static void Import2()
		{
			XmlDocument xmldoc = new XmlDocument();
			xmldoc.Load(@"E:\taobao\ħ���곤\black.xml");
			XmlNode nodeBlacks = xmldoc.SelectSingleNode(".//blacks");
			XmlNode nodeFirst = nodeBlacks.FirstChild;

			int c = 0;
			foreach (string filename in System.IO.Directory.GetFiles(@"E:\taobao\ħ���곤", "*.html"))
			{
				System.Diagnostics.Trace.WriteLine(string.Format(">>>>{0}", filename));
				System.IO.StreamReader r = new System.IO.StreamReader(filename);
				string html = r.ReadToEnd();
				r.Close();
				
	//			System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("<input type=\"hidden\" id='matchBuyerNick0' value='[*]'>");
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(
					"id='matchBuyerNick\\d*' value='(.*)'.*\\s" +
					"*.*id='matchBuyerAddress\\d*' value='(.*)'.*\\s" + 
					"*.*id='matchBuyerMobile\\d*' value='(.*)'.*\\s" + 
					"*.*id='matchBuyerPhone\\d*' value='(.*)'.*\\s" + 
					"*.*id='matchBuyerAlipayNo\\d*' value='(.*)'.*\\s" + 
					"*.*id='matchBuyerRemarks\\d*' value='(.*)'.*\\s");
				System.Text.RegularExpressions.MatchCollection matches = regex.Matches(html);
				
				foreach (System.Text.RegularExpressions.Match m in matches)
				{
					if (m.Success)
					{
//						System.Diagnostics.Trace.WriteLine(string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}", ++c, m.Groups[1].Value, m.Groups[2].Value, m.Groups[3].Value, m.Groups[4].Value, m.Groups[5].Value, m.Groups[6].Value));
						XmlElement nodeBlack = xmldoc.CreateElement("black");
						nodeBlacks.InsertBefore(nodeBlack, nodeFirst);
						nodeBlack.SetAttribute("op", "kk");
						nodeBlack.SetAttribute("time", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
						
						string comment = m.Groups[6].Value;
						// buyer id may be contained in comment. Pick it and put into red keywords.
						string buyerId = string.Empty;
						System.Text.RegularExpressions.Regex regexBuyerId = new System.Text.RegularExpressions.Regex("(id)|(�������): (.*),");
						System.Text.RegularExpressions.Match matchBuyerId = regexBuyerId.Match(comment);
						if (matchBuyerId.Success)
						{
							buyerId = matchBuyerId.Groups[1].Value;
							comment = comment.Replace(string.Format("id: {0}", buyerId), string.Empty);
							comment = comment.Replace(string.Format("�������: {0}", buyerId), string.Empty);
							comment = comment.Trim();
							if (comment.EndsWith(",") || comment.EndsWith("��"))
								comment = comment.Substring(0, comment.Length - 1);
						}

						string red = m.Groups[3].Value; // mobile.
						if (!string.IsNullOrEmpty(buyerId))
							red += "," + buyerId;
						if (!string.IsNullOrEmpty(m.Groups[4].Value)) // phone.
							red += ","+m.Groups[4].Value;
						if (!string.IsNullOrEmpty(m.Groups[5].Value)) // alipay.
							red += ","+m.Groups[5].Value;
					
						string blue = string.Empty;
						if (!string.IsNullOrEmpty(m.Groups[1].Value))
							blue = m.Groups[1].Value;
						else
							blue = string.Format("{0},{1}", m.Groups[1].Value, m.Groups[2].Value);
						
						nodeBlack.SetAttribute("red", red);
						nodeBlack.SetAttribute("blue", blue);
						nodeBlack.SetAttribute("comment", comment);
						System.Diagnostics.Trace.WriteLine(string.Format("red={0}, blue={1}, comment={2}", red, blue, comment));
					}
				}
				xmldoc.Save(@"E:\taobao\ħ���곤\black_.xml");
			}
		}
	}
	
	public class WarningKeyword
	{
		// ÿ��keyword�ĸ�ʽ: k1,k2,k3...
		// ����k���ӹؼ���, Ҳ����������Ҫ�Ƚ�ƥ�����С��λ
		private static List<string> _keywords;

		// s��ʽ: k1,k2,k3...
		// ����k���ӹؼ���, Ҳ����������Ҫ�Ƚ�ƥ�����С��λ
		public static void AddKeywordRecord(string s)
		{
			if (null == _keywords)
				_keywords = new List<string>();
			_keywords.Add(s);
		}
		
		// �ø�������ƥ��ؼ���.
		// ƥ�����:
		// ����ؼ���ֻ����1���ӹؼ���, ��ƥ�����ӹؼ��ּ�ƥ��;
		// ����ؼ��ְ���2����2�������ӹؼ���, ��ƥ��2������Ϊƥ��.
		// ƥ��ɹ��򷵻������ؼ���.
		public static string Match(Order order, out float matchPercentage)
		{
			matchPercentage = 0f;
			
			if (null == order)
				return string.Empty;
			
			foreach (string kw in _keywords)
			{
				string[] subkws = kw.Split(',');
				int c = 0;
				foreach	(string subkw in subkws)
				{
					if (string.IsNullOrEmpty(subkw.Trim()))
						continue;

					if (order.BuyerAccount.ToLower().Contains(subkw.ToLower().Trim()))
						c++;
					else if (order.RecipientName.ToLower().Contains(subkw.ToLower().Trim()))
						c++;
					else if (order.RecipientAddress.ToLower().Contains(subkw.ToLower().Trim()))
						c++;
					else if (order.EditedRecipientAddress.ToLower().Contains(subkw.ToLower().Trim()))
						c++;
					else if (order.PhoneNumber.ToLower().Contains(subkw.ToLower().Trim()))
						c++;
					else if (order.MobileNumber.ToLower().Contains(subkw.ToLower().Trim()))
						c++;
					else if (order.BuyerRemark.ToLower().Contains(subkw.ToLower().Trim()))
						c++;
					else if (order.Remark.ToLower().Contains(subkw.ToLower().Trim()))
						c++;
				}
				
				int subkeywordCount = GetSubKeywordCount(kw);
				if (c >= Math.Min(2, subkeywordCount))
				{
					matchPercentage = (float)c / (float)subkeywordCount;
					return kw;
				}
			}
			
			return string.Empty;
		}
		
		private static int GetSubKeywordCount(string keyword)
		{
			string[] subkws = keyword.Split(',');
			int c = 0;
			foreach	(string subkw in subkws)
			{
				if (!string.IsNullOrEmpty(subkw.Trim()))
					c++;
			}
			return c;
		}
	}
}