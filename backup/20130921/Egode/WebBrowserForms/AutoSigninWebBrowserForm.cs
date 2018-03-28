using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Egode.WebBrowserForms;

namespace Egode.WebBrowserForms
{
	public partial class AutoSigninWebBrowserForm : WebBrowserForm
	{
		private bool _signedIn;
	
		public AutoSigninWebBrowserForm(string url) : base(url)
		{
			InitializeComponent();
		}

		protected override void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
		{
			base.OnDocumentCompleted(e);

			if (wb.Document.Body.OuterHtml.Contains("登录名：") && wb.Document.Body.OuterHtml.Contains("登录密码："))
			{
				HtmlElement u = wb.Document.GetElementById("TPL_username_1");
				if (null == u)
					return;

				HtmlElement p = wb.Document.GetElementById("TPL_password_1");
				if (null == p)
					return;

				HtmlElement s = wb.Document.GetElementById("J_SubmitStatic");
				if (null == s)
					return;

				u.SetAttribute("value", "德国e购");
				p.SetAttribute("value", "ta0ba01g0d1");
				s.InvokeMember("click");
				return;
			}

			if (wb.Document.Body.OuterHtml.Contains("使用其他账户登录"))
			{
				HtmlElement a = wb.Document.GetElementById("J_OtherAccountV");
				wb.Navigate(a.GetAttribute("href"));
				return;
			}

			if (wb.Document.Body.OuterHtml.Contains("当前订单状态"))
				_signedIn = true;

			if (wb.Document.Body.OuterHtml.Contains("已卖出的宝贝") && wb.Document.Body.OuterHtml.Contains("出售中的宝贝"))
				_signedIn = true;

			if (wb.Document.Body.OuterHtml.Contains("您的位置：") && wb.Document.Body.OuterHtml.ToLower().Contains("我的淘宝</a><span>&gt;"))
				_signedIn = true;
		}
		
		public bool SignedIn
		{
			get { return _signedIn; }
		}
	}
}