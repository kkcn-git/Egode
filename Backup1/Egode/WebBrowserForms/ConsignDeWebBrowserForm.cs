using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using OrderLib;

namespace Egode.WebBrowserForms
{
	public partial class ConsignDeWebBrowserForm : AutoSigninWebBrowserForm
	{
		private static ConsignDeWebBrowserForm _instance;
	
		private Order _order;
		// Modified by KK on 2015/12/24.
		//private string _shipmentNumber;
		//private bool _isExpress;
		//private bool _isPostNL;
		private PdfPacketInfoEx _packetInfo;
		private bool _delayed;
		
		private Timer _tmrClose;

		public ConsignDeWebBrowserForm() : base(string.Empty)
		{
			InitializeComponent();
			
			_tmrClose = new Timer();
			_tmrClose.Interval = 1000;
			_tmrClose.Tick += new EventHandler(_tmrClose_Tick);
			_tmrClose.Start();
		}

		void _tmrClose_Tick(object sender, EventArgs e)
		{
			if (null == wb)
				return;
			if (null == wb.Document)
				return;
			if (null == wb.Document.Body)
				return;
			if (GetRemainingDays() >= Settings.Instance.LogisticsDays)
			{
				_tmrClose.Stop();
				this.Close();
			}
		}
		
		public static ConsignDeWebBrowserForm Instance
		{
			get
			{
				if (null == _instance)
					_instance = new ConsignDeWebBrowserForm();
				return _instance;
			}
		}
		
		public Order Order
		{
			get { return _order; }
			set { _order = value; }
		}
		
		public PdfPacketInfoEx PacketInfo
		{
			get { return _packetInfo; }
			set { _packetInfo = value; }
		}
		
		//public string ShipmentNumber
		//{
		//    get { return _shipmentNumber; }
		//    set { _shipmentNumber = value; }
		//}
		
		//public bool IsExpress
		//{
		//    get { return _isExpress; }
		//    set { _isExpress = value; }
		//}
		
		//public bool IsPostNL
		//{
		//    get { return _isPostNL; }
		//    set { _isPostNL = value; }
		//}

		private void ConsignForm_Load(object sender, EventArgs e)
		{
			//string url = string.Format(@"http://wuliu.taobao.com/user/consign.htm?trade_id={0}", _order);
			//wb.Navigate(url);
			this.AutoScroll = true;	
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			
			if (null != _order)
			{
				_once = false;
				_delayed = false;
				this.Navigate(string.Format(@"http://wuliu.taobao.com/user/consign.htm?trade_id={0}", _order.OrderId));
				_tmrClose.Start();
			}
		}

		private bool _once;
		protected override void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
		{
			base.OnDocumentCompleted(e);
			Trace.WriteLine(e.Url.AbsolutePath);
			Trace.WriteLine(e.Url.AbsoluteUri);
			Trace.WriteLine("----");
			
			if (!this.SignedIn)
				return;

			//wb.Size = new Size(pnlWb.Width, wb.Document.Body.OffsetRectangle.Bottom+660);

			if (wb.Document.Body.OuterHtml.Contains("自己联系物流"))
			{
				if (!_once)
				{
					_once = true;
					this.AutoScrollPosition = new Point(0, 600);
					HtmlElement elmLeSelector = wb.Document.GetElementById("logis:LeSelector");//.SetAttribute("selectedindex", "5");

					switch (_packetInfo.Type)
					{
						case PacketTypes.Time24_DHL:
							for (int i = 0; i < elmLeSelector.Children.Count; i++)
							{
								HtmlElement option = elmLeSelector.Children[i];
								if (null == option)
									continue;
								if (string.IsNullOrEmpty(option.InnerText))
									continue;
								if (option.InnerText.Trim().Equals("DHL"))
								{
									elmLeSelector.SetAttribute("selectedindex", i.ToString());
									break;
								}
							}
							break;
							
						case PacketTypes.Time24_PostNL:
							for (int i = 0; i < elmLeSelector.Children.Count; i++)
							{
								HtmlElement option = elmLeSelector.Children[i];
								if (null == option)
									continue;
								if (string.IsNullOrEmpty(option.InnerText))
									continue;
								if (option.InnerText.Trim().Equals("POSTNL"))
								{
									elmLeSelector.SetAttribute("selectedindex", i.ToString());
									break;
								}
							}
							break;
							
						case PacketTypes.Time24_MilkExpress:
						{
							HtmlElement billNumberTextBox = wb.Document.GetElementById("offlineMailNoEMS");
							if (null == billNumberTextBox)
								return;
							Application.DoEvents();
							billNumberTextBox.SetAttribute("value", _packetInfo.ShipmentNumber);

							HtmlElement button = wb.Document.GetElementById("EMS");
							if (null == button)
								return;
							button.InvokeMember("click");
							Application.DoEvents();

							//break;
							return;
						}
					}

					if (_packetInfo.ShipmentNumber.Trim().Length >= 10)
					{
						System.Threading.Thread.Sleep(1000);
						Application.DoEvents();
						wb.Document.GetElementById("logis:other").SetAttribute("value", _packetInfo.ShipmentNumber);
						
						//DialogResult dr = MessageBox.Show(
						//    this,
						//    string.Format("收货人地址: {0}\n单号:{1}\n\n点击Yes自动发货, 点击No进入页面手动操作.", wb.Document.GetElementById("receiverInfo").InnerText, s), 
						//    this.Text,
						//    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
						//if (DialogResult.Yes == dr)
						//{
							// Modified by KK on 2016/01/06.
							// I dont know why all NextSiblings are null.
							//HtmlElement t = wb.Document.GetElementById("logis:LeText");
							//HtmlElement btn = t.Parent.NextSibling.NextSibling.NextSibling.FirstChild;
							//btn.InvokeMember("click");
							foreach (HtmlElement button in wb.Document.GetElementsByTagName("button"))
							{
								if (null == button.GetAttribute("className"))
									continue;
								if (string.IsNullOrEmpty(button.GetAttribute("className")))
									continue;
								System.Diagnostics.Trace.WriteLine(button.GetAttribute("className"));
								if (button.GetAttribute("className").Equals("confirm logis:otherConfirm small-btn"))
								{
									//button.InvokeMember("click");
									Application.DoEvents();
									break;
								}
							}
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
			else if (wb.Document.Body.OuterHtml.Contains("恭喜您，操作成功") || (wb.Document.Body.OuterHtml.Contains("物流编号")&&wb.Document.Body.OuterHtml.Contains("发货方式")&&wb.Document.Body.OuterHtml.Contains("运单号码")))
			{
				_order.Consign();
				string url = string.Format(@"http://trade.taobao.com/trade/detail/trade_item_detail.htm?bizOrderId={0}", _order.OrderId);
				wb.Navigate(url);
			}
			
			if (_once && (wb.Document.Body.OuterHtml.ToLower().Contains("延长收货时间")))// || wb.Document.Body.OuterHtml.ToLower().Contains("延长确认时间")))
			{
				int remainingDays = GetRemainingDays();
				if (remainingDays > 0) // -1 means not correct page or getting remaining days failed.
				{
					while (remainingDays < Settings.Instance.LogisticsDays)
					{
						if (Settings.Instance.LogisticsDays - remainingDays >= 10)
						{
							Delay(10);
							remainingDays += 10;
						}
						else if (Settings.Instance.LogisticsDays - remainingDays >= 7)
						{
							Delay(7);
							remainingDays += 7;
						}
							 // Removed by KK on 2015/11/13. If the default days is 14...
						else //if (Settings.Instance.LogisticsDays - remainingDays >= 3)
						{
							Delay(3);
							remainingDays += 3;
						}
					}
					wb.Refresh(WebBrowserRefreshOption.Completely);
				}
				//if (!_delayed)
				//{
				//    _delayed = true; 
				//    if (_isExpress)
				//    {
				//        Delay(3);
				//    }
				//    else
				//    {
				//        string[] delays = Settings.Instance.LogisticsDelay.Split(',');
				//        foreach (string delay in delays)
				//            Delay(int.Parse(delay));
				//    }
				//    wb.Refresh(WebBrowserRefreshOption.Completely);
				//}
				btn3.Enabled = true;
				btn5.Enabled = true;
				btn7.Enabled = true;
				btn10.Enabled = true;
			}
		}
		
		private int GetRemainingDays()
		{
			System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"(\d{1,})天(\d{1,})小时(\d{1,})分(\d{1,})秒");
			System.Text.RegularExpressions.Match m = r.Match(wb.Document.Body.OuterHtml);
			if (!m.Success)
				return -1;
			return int.Parse(m.Groups[1].Value);
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