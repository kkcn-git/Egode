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
		private string _html;

		public OrderDetailsPageWebBrowserForm(string orderId)
			: base(string.Format(@"http://trade.taobao.com/trade/detail/trade_item_detail.htm?bizOrderId={0}", orderId))
		{
			InitializeComponent();
		}

		protected override void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
		{
			base.OnDocumentCompleted(e);

			//Trace.WriteLine(e.Url.AbsolutePath);
			//Trace.WriteLine(e.Url.AbsoluteUri);

			if (this.SignedIn)
			{
				_html = wb.Document.Body.OuterHtml.Trim().ToLower();
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		public string Html
		{
			get { return _html; }
		}
	}
}