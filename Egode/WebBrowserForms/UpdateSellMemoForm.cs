using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OrderLib;

namespace Egode.WebBrowserForms
{
	public partial class UpdateSellMemoForm : AutoSigninWebBrowserForm
	{
		private string _memo;
		private bool _append;
		private bool _updated;
	
		public UpdateSellMemoForm(Order o, string memo, bool append) : base(string.Format(Common.URL_UPDATE_SELL_MEMO, o.OrderId))
		{
			_memo = memo;
			_append = append;
			InitializeComponent();
		}

		protected override void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
		{
			base.OnDocumentCompleted(e);
			if (!this.SignedIn)
				return;
			if (_updated)
				return;

			HtmlElement memoText = wb.Document.GetElementById("memo");
			if (null != memoText)
			{
				memoText.InnerText = string.Format(
					"{0}[{1}@{2}]: {3}", 
					_append ? memoText.InnerText+"\n" : string.Empty,
					User.GetDisplayName(Settings.Operator),
					DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
					_memo);
				
				HtmlElementCollection buttons = wb.Document.GetElementsByTagName("button");
				foreach (HtmlElement button in buttons)
				{
					if (button.InnerText.Equals("È· ¶¨"))
					{
						button.InvokeMember("click");
						break;
					}
				}
				
				_updated = true;
			}
		}
	}
}