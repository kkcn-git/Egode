using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode.WebBrowserForms
{
	public partial class ConsignShWebBrowserForm : AutoSigninWebBrowserForm
	{
		private string _billNumber;

		public ConsignShWebBrowserForm(string orderId, string billNumber)
			: base(string.Format("http://wuliu.taobao.com/user/consign.htm?trade_id={0}", orderId))
		{
			_billNumber = billNumber;
			InitializeComponent();
		}

		private bool _flag;
		protected override void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
		{
			base.OnDocumentCompleted(e);

			if (!this.SignedIn)
				return;

			if (wb.Document.Body.OuterHtml.Contains("物流编号"))
			{
				if (_flag)
					return;
				_flag = true;

				this.DialogResult = DialogResult.OK;
				this.Close();
				return;
			}

			if (wb.Document.Body.OuterHtml.Contains("啊哦，系统升级中，小二在紧急处理"))
			{
				this.DialogResult = DialogResult.Retry;
				return;
			}

			if (!wb.Document.Body.OuterHtml.Contains("确认收货信息及交易详情"))
				return;

			if (_billNumber.Length == 12)
			{
				HtmlElement sfTextbox = wb.Document.GetElementById("offlineMailNoSF");
				if (null == sfTextbox)
					return;

				sfTextbox.SetAttribute("value", _billNumber);

				HtmlElement sfButton = wb.Document.GetElementById("SF");
				if (null == sfButton)
					return;

				sfButton.InvokeMember("click");
			}
			else // the length of yto bill number is 10.
			{
				HtmlElement ytoTextbox = wb.Document.GetElementById("offlineMailNoYTO");
				if (null == ytoTextbox)
					return;

				ytoTextbox.SetAttribute("value", _billNumber);

				HtmlElement ytoButton = wb.Document.GetElementById("YTO");
				if (null == ytoButton)
					return;

				ytoButton.InvokeMember("click");
			}
		}
	}
}