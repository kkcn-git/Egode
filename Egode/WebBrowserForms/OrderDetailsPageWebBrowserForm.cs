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

namespace Egode.WebBrowserForms
{
	public partial class OrderDetailsPageWebBrowserForm : AutoSigninWebBrowserForm
	{
		private static OrderDetailsPageWebBrowserForm _instance;
		private string _orderId;
		private string _html;

		public OrderDetailsPageWebBrowserForm(string orderId)
			: base(string.Format(@"http://trade.taobao.com/trade/detail/trade_item_detail.htm?bizOrderId={0}", orderId))
		{
			InitializeComponent();
		}

		public OrderDetailsPageWebBrowserForm() : base(string.Empty)
		{
			InitializeComponent();
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			if (!string.IsNullOrEmpty(_orderId))
				this.Navigate(string.Format(@"http://trade.taobao.com/trade/detail/trade_item_detail.htm?bizOrderId={0}", _orderId));
		}

		protected override void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
		{
			base.OnDocumentCompleted(e);

			Trace.WriteLine(e.Url.AbsolutePath);
			Trace.WriteLine(e.Url.AbsoluteUri);
			Trace.WriteLine("");

			if (this.SignedIn)
			{
				if (e.Url.AbsoluteUri.StartsWith("http://aq.taobao.com"))
					return;
			
				// 程序启动后第1次登录淘宝, 可能会进入seller_admin页面, 而不是订单详情页面.
				// 重新加载订单详情页面.
				if (e.Url.AbsolutePath.EndsWith("seller_admin.htm"))
				{
					this.wb.Navigate(this._url);
					return;
				}
				
				if (e.Url.AbsoluteUri.ToLower().StartsWith("http://aq.taobao.com"))
					return;

				_html = wb.Document.Body.OuterHtml.Trim().ToLower();
				
				// Added by KK on 2016/12/12.
				if (_html.Contains("对不起，系统繁忙，请提交验证码后继续。") && _html.Contains("请输入您在下图中看到的内容："))
					return;
				
				this.DialogResult = DialogResult.OK;

				if (Settings.Instance.AutoCloseAfterSignin)
					this.Close();
			}
		}
		
		public static OrderDetailsPageWebBrowserForm Instance
		{
			get 
			{
				if (null == _instance)
					_instance = new OrderDetailsPageWebBrowserForm(string.Empty);
				return _instance;
			}
		}

		public string Html
		{
			get { return _html; }
		}
		
		public string OrderId
		{
			get { return _orderId; }
			set { _orderId = value; }
		}
		
		public static string GetAlipayNumber(string detailsPageHtml)
		{
			if (string.IsNullOrEmpty(detailsPageHtml))
				return string.Empty;
			detailsPageHtml = detailsPageHtml.Replace("\"", string.Empty);
			//"<span class="alilay-num">2015120121001001250246665752</span>"
			string start = "<span class=alilay-num>";
			int i = detailsPageHtml.ToLower().IndexOf(start);
			if (i < 0)
				return string.Empty;
			
			string end = "</span>";
			int j = detailsPageHtml.ToLower().IndexOf(end, i);
			if (j < 0)
				return string.Empty;
			
			return detailsPageHtml.Substring(i + start.Length, j - i - start.Length);
		}

		public static string GetOrderDetailsHtml(string orderId, Form ownerForm)
		{
			if (string.IsNullOrEmpty(orderId))
				return string.Empty;

			//WebBrowserForms.OrderDetailsPageWebBrowserForm odpbf = new Egode.WebBrowserForms.OrderDetailsPageWebBrowserForm(orderId);
			//odpbf.ShowDialog(this);
			//return odpbf.Html;
			WebBrowserForms.OrderDetailsPageWebBrowserForm.Instance.OrderId = orderId;
			WebBrowserForms.OrderDetailsPageWebBrowserForm.Instance.ShowDialog(ownerForm);
			// Modified by KK on 2017/11/01.
			//return WebBrowserForms.OrderDetailsPageWebBrowserForm.Instance.Html;
			//return System.Text.RegularExpressions.Regex.Unescape(@WebBrowserForms.OrderDetailsPageWebBrowserForm.Instance.Html);
			
			// 2017/11/13.
			string s = WebBrowserForms.OrderDetailsPageWebBrowserForm.Instance.Html;
			try
			{
				s = System.Text.RegularExpressions.Regex.Unescape(@WebBrowserForms.OrderDetailsPageWebBrowserForm.Instance.Html);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
			}
			return s;
		}
	}
}