using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Sgml;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Egode
{
	public class Common
	{
		public const string URL_ROOT = "http://120.77.71.205/";//"http://ff.egode.cn/";
		public const string URL_ROOT_DB = "http://120.77.71.205/db/";//"http://ff.egode.cn/db/";
		//public const string URL_ROOT = "http://localhost:14204/CloudEgode/";
		//public const string URL_ROOT_DB = "http://localhost:14204/CloudEgode/db/";
	
		public const string URL_USERS = URL_ROOT_DB + "users.xml";
		//public const string URL_ORDERS = URL_ROOT_DB + "orders-c.xml";
		public const string URL_PRODUCTS = URL_ROOT_DB + "products.xml";
		public const string URL_DISTRIBUTORS = URL_ROOT_DB + "distributors.xml";
		public const string URL_BLACKLIST = URL_ROOT_DB + "black.xml";
		public const string URL_STOCK_SH = URL_ROOT_DB + "stock-sh.xml";
		public const string URL_STOCK_NINGBO = URL_ROOT_DB + "stock-ningbo.xml";
		public const string URL_REFUND = URL_ROOT_DB + "refund.xml";
		public const string URL_DHL = URL_ROOT_DB + "dhl.xml";
		public const string URL_WARNING_KEYWARDS = URL_ROOT_DB + "warningkeywords.txt";
		public const string URL_DB_LOG = URL_ROOT_DB + "dblog.txt";
		public const string URL_SUBJECTS = URL_ROOT_DB + "subjects.txt";
		public const string URL_PREPARE_HISTORY = URL_ROOT_DB + "prepare-history.xml";
		public const string URL_PREPARE_HISTORY_NINGBO = URL_ROOT_DB + "prepare-history-nb.xml";
		public const string URL_ADDRESSES = URL_ROOT_DB + "addrs.xml";
		public const string URL_DATA_CENTER = URL_ROOT + "datacenter.aspx?cmd={0}";
		//public const string URL_UPDATE_SELL_MEMO = "http://trade.taobao.com/trade/memo/update_sell_memo.htm?biz_order_id={0}&seller_id=787602526&return_url=http%3A%2F%2Ftrade.taobao.com%2Ftrade%2Fdetail%2Ftrade_item_detail.htm%3Fbiz_order_id%3D{0}";
		public const string URL_UPDATE_SELL_MEMO = "https://trade.taobao.com/trade/memo/update_sell_memo.htm?seller_id={0}&biz_order_id={1}";

		//public const string URL_ORDERS = "http://localhost:14204/CloudEgode/db/orders.xml";
		//public const string URL_PRODUCTS = "http://localhost:14204/CloudEgode/db/products.xml";
		//public const string URL_BLACKLIST = "http://localhost:14204/CloudEgode/db/black.xml";
		//public const string URL_DATA_CENTER = "http://localhost:14204/CloudEgode/datacenter.aspx?cmd={0}";

		public static XmlDocument ConvertHtmlToXml(string html)
		{
			Sgml.SgmlReader sgmlReader = new Sgml.SgmlReader();
			sgmlReader.DocType = "HTML";
			sgmlReader.WhitespaceHandling = System.Xml.WhitespaceHandling.All;
			sgmlReader.CaseFolding = Sgml.CaseFolding.ToLower;
			sgmlReader.InputStream = new System.IO.StringReader(html);

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.PreserveWhitespace = false;
			xmlDoc.XmlResolver = null;
			xmlDoc.Load(sgmlReader);

			return xmlDoc;
		}

		public static bool IsServerDbReady(out string recentLog)
		{
			recentLog = string.Empty;
			WebClient wc = new WebClient();
			byte[] buf = wc.DownloadData(new Uri(Common.URL_DB_LOG));
			string s = Encoding.Default.GetString(buf);
			string[] logs = s.Split(new char[] { '\r', '\n' });
			for (int i = logs.Length - 1; i >= 0; i--)
			{
				if (!string.IsNullOrEmpty(logs[i]))
				{
					recentLog = logs[i];
					break;
				}
			}
			return s.Trim().ToLower().EndsWith("ready");
		}
	}
}