using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class ConsignForm : Form
	{
		private string _orderId;
		private bool _delayed;

		public ConsignForm(string orderId)
		{
			_orderId = orderId;
			InitializeComponent();
		}

		private void ConsignForm_Load(object sender, EventArgs e)
		{
			string url = string.Format(@"http://wuliu.taobao.com/user/consign.htm?trade_id={0}", _orderId);
			wb.Navigate(url);
		}

		private bool _once;
		private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			//Trace.WriteLine(e.Url.AbsolutePath);
			//Trace.WriteLine(e.Url.AbsoluteUri);
			
			wb.Size = new Size(pnlWb.Width, wb.Document.Body.OffsetRectangle.Bottom+660);

			if (wb.Document.Body.OuterHtml.Contains("自己联系物流"))
			{
				if (!_once)
				{
					_once = true;
					pnlWb.AutoScrollPosition = new Point(0, 600);
					wb.Document.GetElementById("logis:LeSelector").SetAttribute("selectedindex", "1");
					wb.Document.GetElementById("logis:LeText").SetAttribute("value", "DHL+中国邮政");
					
					string s = Clipboard.GetText();
					if (s.Trim().Length == 12 && (s.StartsWith("297808") || s.StartsWith("960")))
					{
						wb.Document.GetElementById("logis:other").SetAttribute("value", Clipboard.GetText());
						
						DialogResult dr = MessageBox.Show(
							this,
							string.Format("收货人地址: {0}\n单号:{1}\n\n点击Yes自动发货, 点击No进入页面手动操作.", wb.Document.GetElementById("receiverInfo").InnerText, s), 
							this.Text,
							MessageBoxButtons.YesNo, MessageBoxIcon.Question);
						if (DialogResult.Yes == dr)
						{
							HtmlElement t = wb.Document.GetElementById("logis:LeText");
							HtmlElement btn = t.Parent.NextSibling.NextSibling.NextSibling.FirstChild;
							btn.InvokeMember("click");
						}
					}
					//wb.Document.GetElementById("J_moreHiddenTbody").SetAttribute("class", "expand");
					//wb.Document.InvokeScript("S");
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

					//this.DialogResult = DialogResult.OK
				}
			}
			else if (wb.Document.Body.OuterHtml.Contains("恭喜您，操作成功"))
			{
				string url = string.Format(@"http://trade.taobao.com/trade/detail/trade_item_detail.htm?bizOrderId={0}", _orderId);
				wb.Navigate(url);
			}
			
			if (wb.Document.Body.OuterHtml.ToLower().Contains("延长收货时间</a>"))
			{
				if (!_delayed)
				{
					_delayed = true;
					Delay(10);
					Delay(7);
					wb.Refresh(WebBrowserRefreshOption.Completely);
				}
				btn3.Enabled = true;
				btn5.Enabled = true;
				btn7.Enabled = true;
				btn10.Enabled = true;
			}
		}
		
		private void Delay(int days)
		{
			DelayForm delay = new DelayForm(_orderId, days);
			delay.ShowDialog(this);
		}

		private void OnDelayButtonClick(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			int days = int.Parse((((Button)sender).Tag.ToString()));
			Delay(days);
			wb.Refresh(WebBrowserRefreshOption.Completely);
			Cursor.Current = Cursors.Default;
		}
	}
}