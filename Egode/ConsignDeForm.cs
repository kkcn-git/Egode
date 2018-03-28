using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class ConsignDeForm : WebBrowserForms.AutoSigninWebBrowserForm
	{
		private OrderParser.Order _order;
		private string _shipmentNumber;
		private bool _isExpress;
		private bool _delayed;

		public ConsignDeForm(OrderParser.Order order, string shipmentNumber, bool isExpress) : base(string.Format(@"http://wuliu.taobao.com/user/consign.htm?trade_id={0}", order.OrderId))
		{
			_order = order;
			_shipmentNumber = shipmentNumber;
			isExpress = isExpress;
			InitializeComponent();
		}

		private void ConsignForm_Load(object sender, EventArgs e)
		{
			//string url = string.Format(@"http://wuliu.taobao.com/user/consign.htm?trade_id={0}", _order);
			//wb.Navigate(url);
			this.AutoScroll = true;	
		}

		private bool _once;
		protected override void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
		{
			base.OnDocumentCompleted(e);
			//Trace.WriteLine(e.Url.AbsolutePath);
			//Trace.WriteLine(e.Url.AbsoluteUri);
			
			if (!this.SignedIn)
				return;

			//wb.Size = new Size(pnlWb.Width, wb.Document.Body.OffsetRectangle.Bottom+660);

			//if (wb.Document.Body.OuterHtml.Contains("��¼����") && wb.Document.Body.OuterHtml.Contains("��¼���룺"))
			//{
			//    Signin();
			//    return;
			//}

			if (wb.Document.Body.OuterHtml.Contains("�Լ���ϵ����"))
			{
				if (!_once)
				{
					_once = true;
					this.AutoScrollPosition = new Point(0, 600);
					HtmlElement elmLeSelector = wb.Document.GetElementById("logis:LeSelector");//.SetAttribute("selectedindex", "5");
					for (int i = 0; i < elmLeSelector.Children.Count; i++)
					{
						HtmlElement option = elmLeSelector.Children[i];
						if (null == option)
							continue;
						if (string.IsNullOrEmpty(option.InnerText))
							continue;
						if (option.InnerText.Trim().Equals("����"))
						{
							elmLeSelector.SetAttribute("selectedindex", i.ToString());
							break;
						}
					}
					
					wb.Document.GetElementById("logis:LeText").SetAttribute("value", _isExpress ? "DHL" : "DHL+�й�����");

					if (_shipmentNumber.Trim().Length >= 10)
					{
						System.Threading.Thread.Sleep(1000);
						Application.DoEvents();
						wb.Document.GetElementById("logis:other").SetAttribute("value", _shipmentNumber);
						
						//DialogResult dr = MessageBox.Show(
						//    this,
						//    string.Format("�ջ��˵�ַ: {0}\n����:{1}\n\n���Yes�Զ�����, ���No����ҳ���ֶ�����.", wb.Document.GetElementById("receiverInfo").InnerText, s), 
						//    this.Text,
						//    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
						//if (DialogResult.Yes == dr)
						//{
							HtmlElement t = wb.Document.GetElementById("logis:LeText");
							HtmlElement btn = t.Parent.NextSibling.NextSibling.NextSibling.FirstChild;
							btn.InvokeMember("click");
						//}
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
			else if (wb.Document.Body.OuterHtml.Contains("��ϲ���������ɹ�"))
			{
				_order.Consign();
				string url = string.Format(@"http://trade.taobao.com/trade/detail/trade_item_detail.htm?bizOrderId={0}", _order.OrderId);
				wb.Navigate(url);
			}
			
			if (wb.Document.Body.OuterHtml.ToLower().Contains("�ӳ��ջ�ʱ��</a>"))
			{
				if (!_delayed)
				{
					_delayed = true;
					if (_isExpress)
					{
						Delay(3);
					}
					else
					{
						string[] delays = Settings.Instance.LogisticsDelay.Split(',');
						foreach (string delay in delays)
							Delay(int.Parse(delay));
					}
					wb.Refresh(WebBrowserRefreshOption.Completely);
				}
				btn3.Enabled = true;
				btn5.Enabled = true;
				btn7.Enabled = true;
				btn10.Enabled = true;
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

			u.SetAttribute("value", "�¹�e��");
			p.SetAttribute("value", "ta0ba01g0d1");
			p.InvokeMember("click");
		}

		private void Delay(int days)
		{
			DelayForm delay = new DelayForm(_order.OrderId, days);
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