using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class ConsignShWebBrowserForm : WebBrowserForm
	{
		private string _ytoBillNumber;

		public ConsignShWebBrowserForm(string orderId, string ytoBillNumber)
			: base(string.Format("http://wuliu.taobao.com/user/consign.htm?trade_id={0}", orderId))
		{
			_ytoBillNumber = ytoBillNumber;
			InitializeComponent();
			wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);
		}

		private bool _flag;
		void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			if (wb.Document.Body.OuterHtml.Contains("登录名：") && wb.Document.Body.OuterHtml.Contains("登录密码："))
			{
				Signin();
				return;
			}
		
			if (wb.Document.Body.OuterHtml.Contains("物流编号"))
			{
				if (_flag)
					return;
				_flag = true;

				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}

			if (!wb.Document.Body.OuterHtml.Contains("确认收货信息及交易详情"))
				return;

			HtmlElement ytoTextbox = wb.Document.GetElementById("offlineMailNoYTO");
			if (null == ytoTextbox)
				return;

			ytoTextbox.SetAttribute("value", _ytoBillNumber);

			HtmlElement ytoButton = wb.Document.GetElementById("YTO");
			if (null == ytoButton)
				return;

			ytoButton.InvokeMember("click");
		}
		
		private void Signin()
		{
			HtmlElement username = wb.Document.GetElementById("TPL_username_1");
			if (nulll == username)
				return;

			HtmlElement password = wb.Document.GetElementById("TPL_password_1");
			if (null == password)
				return;
			
			
				
		}
	}
}