using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.IO;

namespace Egode
{
	public partial class OrderDetailsPageForm : Form
	{
		private string _orderId;
		private string _html;
		private static bool _loggedIn;

		public OrderDetailsPageForm(string orderId)
		{
			_orderId = orderId;
			InitializeComponent();
		}

		private void OrderDetailsPageForm_Load(object sender, EventArgs e)
		{
			string url = string.Format(@"http://trade.taobao.com/trade/detail/trade_item_detail.htm?bizOrderId={0}", _orderId);
			wb.Navigate(url);
		}

		private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			//Trace.WriteLine(e.Url.AbsolutePath);
			//Trace.WriteLine(e.Url.AbsoluteUri);

			if (wb.Document.Body.OuterHtml.Contains("µ±Ç°¶©µ¥×´Ì¬"))
			//if (e.Url.AbsolutePath.ToLower().EndsWith("connection.html")) // login succeeded!
			{
				//// Get cookie.
				//_cookie = new System.Net.CookieContainer();
				//string[] cookieInfos = wb.Document.Cookie.Split(';');
				//foreach (string cookieInfo in cookieInfos)
				//{
				//    string[] cookieNameValue = cookieInfo.Split('=');
				//    Cookie ck = new Cookie(cookieNameValue[0].Trim().ToString(), cookieNameValue[1].Trim().ToString());
				//    ck.Domain = "taobao.com";
				//    _cookie.Add(ck);
				//}
				
				//HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://trade.taobao.com/trade/itemlist/list_sold_items.htm");
				//request.CookieContainer = _cookie;
				//HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				//Stream stream = response.GetResponseStream();
				//StreamReader sr = new StreamReader(stream);
				//Trace.WriteLine(sr.ReadToEnd());
				//stream.Close();
				//sr.Close();

				_loggedIn = true;
				_html = wb.Document.Body.OuterHtml.Trim().ToLower();
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}
		
		public static bool LoggedIn
		{
			get { return _loggedIn; }
		}
		
		public string Html
		{
			get { return _html; }
		}
	}
}