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
	public partial class UpdateSellMemoWebBrowserForm : AutoSigninWebBrowserForm
	{
		private string _memo;
		private bool _append;
		private bool _updated;
	
		public UpdateSellMemoWebBrowserForm(Order o, string memo, bool append) : base(string.Format(Common.URL_UPDATE_SELL_MEMO, ShopProfile.Current.SellerId, o.OrderId))
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
				string originalMemo = string.Empty;
				if (!string.IsNullOrEmpty(memoText.InnerText))
					originalMemo = memoText.InnerText.Replace("身份证", "身fen证").Replace("银行", "银hang");
				
				memoText.InnerText = string.Format(
					"{0}[{1}@{2}]: {3}",
					_append ? originalMemo + (string.IsNullOrEmpty(originalMemo) ? string.Empty : "\n") : string.Empty,
					User.GetDisplayName(Settings.Operator),
					DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
					_memo.Replace("身份证", "身fen证").Replace("银行", "银hang"));
				
				HtmlElementCollection buttons = wb.Document.GetElementsByTagName("button");
				foreach (HtmlElement button in buttons)
				{
					if (button.InnerText.Equals("确 定"))
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