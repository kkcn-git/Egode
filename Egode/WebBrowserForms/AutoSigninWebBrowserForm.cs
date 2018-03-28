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
		private Timer _tmr;
	
		public AutoSigninWebBrowserForm() : this(string.Empty)
		{
			InitializeComponent();
		}

		public AutoSigninWebBrowserForm(string url) : base(url)
		{
			InitializeComponent();
		}

		protected override void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
		{
			base.OnDocumentCompleted(e);

			if (wb.Document.Body.OuterHtml.Contains("为了您的账户安全，请输入验证码。"))
			{
				MessageBox.Show(
					this, 
					string.Format("需要输入验证码, 程序无能为力-_-!, 请在此窗口中手动登录淘宝.\n你的登录账号是: {0}", ShopProfile.Current.Account + (string.IsNullOrEmpty(ShopProfile.Current.SubAccount) ? string.Empty : (":"+ShopProfile.Current.SubAccount))), 
					this.Text,
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			if (wb.Document.Body.OuterHtml.Contains("TPL_username") && wb.Document.Body.OuterHtml.Contains("TPL_password"))
			{
				// Added by KK on 2016/07/26.
				if (null != wb.Document && null != wb.Document.Window)
				{
					wb.Document.Window.ScrollTo(wb.Document.Body.ScrollRectangle.Width-wb.Size.Width, 210);

					if (null == _tmr)
					{
						_tmr = new Timer();
						_tmr.Interval = 50;
						_tmr.Tick += _tmr_Tick;
						_tmr.Start();
					}
				}
			
				HtmlElement u = wb.Document.GetElementById("TPL_username");
				if (null == u)
					return;

				HtmlElement p = wb.Document.GetElementById("TPL_password");
				if (null == p)
					return;

				HtmlElement s = wb.Document.GetElementById("J_Submit");
				if (null == s)
					return;

				u.SetAttribute("value", ShopProfile.Current.Account + (string.IsNullOrEmpty(ShopProfile.Current.SubAccount) ? string.Empty : (":"+ShopProfile.Current.SubAccount)));
				p.SetAttribute("value", ShopProfile.Current.Pw);
				s.InvokeMember("click");
				return;
			}
			
			if (wb.Document.Body.OuterHtml.Contains("使用其他账户登录"))
			{
				wb.Navigate(@"https://login.taobao.com/member/login.jhtml?enup=false");
				return;
			}

			if (wb.Document.Body.OuterHtml.Contains("当前订单状态"))
				_signedIn = true;

			if (wb.Document.Body.OuterHtml.Contains("已卖出的宝贝") && wb.Document.Body.OuterHtml.Contains("出售中的宝贝"))
				_signedIn = true;

			if (wb.Document.Body.OuterHtml.Contains("您的位置：") && wb.Document.Body.OuterHtml.ToLower().Contains("我的淘宝</a><span>&gt;"))
				_signedIn = true;

			// just for page of order addr info.
			if (wb.Document.Body.OuterHtml.Contains("splitStr") && wb.Document.Body.OuterHtml.ToLower().Contains("mobilephone"))
				_signedIn = true;
		}

		private bool _cursorPositionSet =  false;
		void _tmr_Tick(object sender, EventArgs e)
		{
			HtmlElement u = wb.Document.GetElementById("TPL_username");
			if (null == u)
				return;
			
			_tmr.Stop();

			if (_cursorPositionSet)
			{
				if (SystemInformation.MouseButtonsSwapped)
				    KmRobot.Win32.ClickRightMouse();
				else
				    KmRobot.Win32.ClickLeftMouse();
			}
			else
			{
				Cursor.Position = new Point(this.Left + 200, this.Bottom - 110);
				_cursorPositionSet = true;
				_tmr.Interval = 500;
				_tmr.Start();
			}
		}
		
		public bool SignedIn
		{
			get { return _signedIn; }
		}
	}
}