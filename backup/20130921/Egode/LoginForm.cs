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
	public partial class LoginForm : Form
	{
		private System.Net.CookieContainer _cookie;
		private static bool _loggedIn;

		public LoginForm()
		{
			InitializeComponent();
		}

		private void LoginForm_Load(object sender, EventArgs e)
		{
			wb.Navigate(@"http://i.taobao.com/my_taobao.htm");
		}

		private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			if (wb.Document.Body.OuterHtml.Contains("登录名：") && wb.Document.Body.OuterHtml.Contains("登录密码："))
			{
				Signin();
				return;
			}

			//Trace.WriteLine(e.Url.AbsolutePath);
			//Trace.WriteLine(e.Url.AbsoluteUri);

			if (wb.Document.Body.OuterHtml.Contains("收货地址管理"))
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
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void Signin()
		{
			HtmlElement u = wb.Document.GetElementById("TPL_username_1");
			if (null == u)
				return;

			HtmlElement p = wb.Document.GetElementById("TPL_password_1");
			if (null == p)
				return;

			HtmlElement s = wb.Document.GetElementById("J_SubmitStatic");
			if (null == p)
				return;

			u.SetAttribute("value", "德国e购");
			p.SetAttribute("value", "ta0ba01g0d1");
			p.InvokeMember("click");
		}

		public static bool LoggedIn
		{
			get { return _loggedIn; }
		}
		
		public CookieContainer Cookie
		{
			get { return _cookie; }
		}
	}
}