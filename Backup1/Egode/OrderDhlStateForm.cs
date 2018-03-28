using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using System.IO;
using System.Threading;
using Egode.WebBrowserForms;
using System.Globalization;
using OrderLib;

namespace Egode
{
	public partial class OrderDhlStateForm : Form
	{
		private class DhlOrder
		{
			private readonly string _orderId;
			private readonly string _buyerAccount;
			private readonly string _shipmentNumber;
			private DateTime _payingTime;
			private DateTime _pdfTime;
			private DateTime _pickedupTime;
			private DateTime _resendTime;
			
			private static List<DhlOrder> _orders;
			
			public DhlOrder(string orderId, string buyerAccount, string shipmentNumber)
			{
				_orderId = orderId;
				_buyerAccount = buyerAccount;
				_shipmentNumber = shipmentNumber;
				_pdfTime = DateTime.MinValue;
				_pickedupTime = DateTime.MinValue;
				_resendTime = DateTime.MinValue;
			}
			
			public static List<DhlOrder> DhlOrders
			{
				get
				{
					if (null == _orders)
						_orders = new List<DhlOrder>();
					return _orders;
				}
			}
			
			public static DhlOrder GetItem(string orderId)
			{
				if (null == _orders)
					return null;
				foreach (DhlOrder o in _orders)
				{
					if (o.OrderId.Equals(orderId))
						return o;
				}
				return null;
			}
			
			public string OrderId
			{
				get { return _orderId; }
			}
			
			public string BuyerAccount
			{
				get { return _buyerAccount; }
			}
			
			public string ShipmentNumber
			{
				get { return _shipmentNumber; }
			}
			
			public DateTime PayingTime
			{
				get { return _payingTime; }
				set { _payingTime = value; }
			}
			
			public DateTime PickedupTime
			{
				get { return _pickedupTime; }
				set { _pickedupTime = value; }
			}
			
			public bool Pickedup
			{
				get { return _pickedupTime > DateTime.MinValue; }
			}
			
			public DateTime PdfTime
			{
				get { return _pdfTime; }
				set { _pdfTime = value; }
			}
			
			public DateTime ResendTime
			{
				get { return _resendTime; }
				set { _resendTime = value; }
			}
		}
	
		private class OrderListViewItem : ListViewItem
		{
			private const int INDEX_STATUS = 4;
			private const int INDEX_DAYS = 5;
		
			private static Queue<OrderListViewItem> _queue;
		
			private ListView _owner;
			private Order _order;
			public Order Order
			{
				get { return _order; }
			}
			
			private ListView Owner
			{
				get { return (null == this.ListView ? _owner : this.ListView); }
			}
			
			public OrderListViewItem(int index, Order order, ListView owner)
			{
				_owner = owner;
				_order = order;
				this.Text = index.ToString("000");
				this.SubItems.Add(order.OrderId);
				this.SubItems.Add(order.BuyerAccount);
				this.SubItems.Add(order.ShipmentNumber);
				this.SubItems.Add(string.Empty);
				this.SubItems.Add(string.Empty);
				this.ForeColor = Color.LightGray;
				
				if (string.IsNullOrEmpty(order.ShipmentNumber))
				{
					this.SubItems[INDEX_STATUS].Text = "The order not sent";
				}
				else if (!order.ShipmentNumber.StartsWith("297808") && !order.ShipmentNumber.StartsWith("960"))
				{
					this.SubItems[INDEX_STATUS].Text = "Not DHL packet";
					this.ForeColor = Color.Green;
				}
				else
				{
					bool retrieved = false;
					foreach (OrderListViewItem item in owner.Items)
					{
						if (order.ShipmentNumber.Equals(item.Order.ShipmentNumber))
						{
							this.SubItems[INDEX_STATUS].Text = "----";
							this.SubItems[INDEX_DAYS].Text = "----";
							retrieved = true;
							break;
						}
					} 
				
					if (!retrieved)
					{
						QueueItem(this);
						
						if (owner.Items.Count <= 0)
							Next();
					}
				}
			}

			private static void QueueItem(OrderListViewItem item)
			{
				if (null == _queue)
					_queue = new Queue<OrderListViewItem>();
				_queue.Enqueue(item);
				item.SubItems[INDEX_STATUS].Text = "Waiting...";
			}

			private static void Next()
			{
				if (null == _queue || _queue.Count <= 0)
					return;
				OrderListViewItem item = _queue.Dequeue();
				if (null == item)
					return;
				item.StartGetRecentStatus();
			}

			private void StartGetRecentStatus()
			{
				if (this.Order.Status == Order.OrderStatus.Closed)
				{
					new Thread(new ThreadStart(delegate(){
						this.SubItems[INDEX_STATUS].Text = "Order closed";
						string url = string.Format(Common.URL_DATA_CENTER, "closedhlinfo");
						url = string.Format("{0}&oid={1}", url, this.Order.OrderId);
						HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
						request.Method = "GET";
						request.ContentType = "text/xml";
						WebResponse response = request.GetResponse();
						StreamReader reader = new StreamReader(response.GetResponseStream());
						string result = reader.ReadToEnd();
						reader.Close();
						Next();
					})).Start();
				}
				else
				{
					this.Owner.Invoke(new MethodInvoker(delegate(){
						this.SubItems[INDEX_STATUS].Text = "Retrieving DHL status...";
						this.ForeColor = Color.Blue;
					}));

					string url = string.Format(
						"http://nolp.dhl.de/nextt-online-public/set_identcodes.do?lang=en&idc={0}&rfn=&extendedSearch=true",
						this.Order.ShipmentNumber);

					string html = string.Empty;

					int retry = 0;
					while (retry++ < 3)
					{
						try
						{
							HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
							request.BeginGetResponse(responseCallback, request);
							break;
						}
						catch (WebException)
						{
							this.Owner.Invoke(new MethodInvoker(delegate()
							{
								this.SubItems[INDEX_STATUS].Text = "Cannot connect to DHL website. retry=" + retry.ToString();
							}));
						}
					}
				}
			}
			
			private void responseCallback(IAsyncResult ar)
			{
				string html = string.Empty;

				try
				{
					HttpWebRequest request = ar.AsyncState as HttpWebRequest;
					HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar);
					Stream s = response.GetResponseStream();
					StreamReader sr = new StreamReader(s);
					html = sr.ReadToEnd();
					sr.Close();
					s.Close();
				}
				catch (WebException)
				{
					this.ListView.Invoke(new MethodInvoker(delegate(){
						this.SubItems[INDEX_STATUS].Text = "Cannot connect to DHL website.";}));
				}

				if (html.Contains("Unfortunately"))
				{
					this.ListView.Invoke(new MethodInvoker(delegate(){
						this.SubItems[INDEX_STATUS].Text = "没有找到对应单号";}));
				}

				XmlDocument doc = Common.ConvertHtmlToXml(html);
				//System.Diagnostics.Trace.WriteLine(doc.OuterXml);

				//XmlNode nodeUpuCode = doc.SelectSingleNode(".//td[text()='UPU code / matchcode']");
				//if (null != nodeUpuCode)
				//    _upuCode = nodeUpuCode.NextSibling.InnerText;

				this.ListView.Invoke(new MethodInvoker(delegate()
				{
					this.ForeColor = Color.FromArgb(0xff, 0x40, 0x40, 0x40);
				}));

				XmlNodeList nlShipmentDetailsTd = doc.SelectNodes(".//td[@class='']");
				if (null != nlShipmentDetailsTd && nlShipmentDetailsTd.Count > 0)
				{
					// get and display recent status.
					string datetime = nlShipmentDetailsTd[nlShipmentDetailsTd.Count - 3].InnerText.Trim();
					string city = nlShipmentDetailsTd[nlShipmentDetailsTd.Count - 2].InnerText.Trim();
					string status = nlShipmentDetailsTd[nlShipmentDetailsTd.Count - 1].InnerText.Trim();
					string recentStatus = string.Format("{0}: {1}", datetime, status);

					this.ListView.Invoke(new MethodInvoker(delegate(){
						this.SubItems[INDEX_STATUS].Text = recentStatus;}));

					Regex r = new Regex(@"(\d{2}\.\d{2}\.\d{4} \d{2}:\d{2})");
					Match m = r.Match(recentStatus);
					if (m.Success)
					{
						IFormatProvider culture = new System.Globalization.CultureInfo("de-DE");
						DateTime dt = DateTime.Parse(m.Value, culture);
						TimeSpan ts = DateTime.Now - dt;
						this.ListView.Invoke(new MethodInvoker(delegate(){
							this.SubItems[INDEX_DAYS].Text = string.Format("{0} days", ts.TotalDays.ToString("0.00"));}));
					}

					if (this.SubItems[INDEX_STATUS].Text.Contains("The instruction data for this shipment have been provided by the sender to DHL electronically"))
					{
						this.ListView.Invoke(new MethodInvoker(delegate(){
							this.ForeColor = Color.Red;}));
					}
					
					// get info of pdf/packet creation.
					string pdfTime = nlShipmentDetailsTd[0].InnerText.Trim();
					string pickupTime = string.Empty;
					//string city = nlShipmentDetailsTd[nlShipmentDetailsTd.Count - 2].InnerText.Trim();
					//string status = nlShipmentDetailsTd[nlShipmentDetailsTd.Count - 1].InnerText.Trim();
					if (nlShipmentDetailsTd.Count > 3)
						pickupTime = nlShipmentDetailsTd[3].InnerText.Trim();
					
					// update dhl info for this order.
					string url = string.Format(Common.URL_DATA_CENTER, "updatedhlinfo");
					url = string.Format(
						"{0}&oid={1}&ba={2}&sn={3}&payt={4}&pdft={5}&pickupt={6}", 
						url, 
						this.Order.OrderId, 
						this.Order.BuyerAccount.Trim(),
						this.Order.ShipmentNumber,
						this.Order.PayingTime.ToString("yyyy-MM-dd HH:mm"),
						ParseDeTime(pdfTime).ToString("yyyy-MM-dd HH:mm"),
						ParseDeTime(pickupTime).ToString("yyyy-MM-dd HH:mm"));
					HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
					request.Method = "GET";
					request.ContentType = "text/xml";
					WebResponse response = request.GetResponse();
					StreamReader reader = new StreamReader(response.GetResponseStream());
					string result = reader.ReadToEnd();
					reader.Close();
				}
				else
				{
					this.ListView.Invoke(new MethodInvoker(delegate(){
						this.SubItems[INDEX_STATUS].Text = "No information for this shipment number.";}));
				}
				
				Next();
			}

			// s: Tue, 11.02.2014 17:29 h
			private static DateTime ParseDeTime(string s)
			{
				if (string.IsNullOrEmpty(s))
					return DateTime.MinValue;
				
				string dt = s.Remove(0, 5);
				dt = dt.Remove(dt.Length-2, 2);
				return DateTime.Parse(dt, new CultureInfo("de-De"));
			}
		}
	
		List<Order> _orders;
	
		public OrderDhlStateForm()
		{
			InitializeComponent();
		}

		private void OrderDhlStateForm_Shown(object sender, EventArgs e)
		{
			StartDownload();
		}

		void StartDownload()
		{
			PromptForm prompt = new PromptForm();
			prompt.MaxLine = 2;
			prompt.Owner = this;
			prompt.Show(this);

			StartDownloadDhlOrders(prompt);
		}

		void StartDownloadDhlOrders(PromptForm prompt)
		{
			prompt.AddMessage("正在下载订单DHL数据...0%");
			WebClient wc = new WebClient();
			wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wcDhlOrders_DownloadProgressChanged);
			wc.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wcDhlOrders_DownloadDataCompleted);
			wc.DownloadDataAsync(new Uri(Common.URL_DHL), prompt);
		}

		void wcDhlOrders_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;
			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("正在下载订单DHL数据...{0}%", e.ProgressPercentage);
			prompt.RefreshDisplay();
		}

		void wcDhlOrders_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;

			string xml = Encoding.UTF8.GetString(e.Result);
			XmlDocument xmldoc = new XmlDocument();
			xmldoc.LoadXml(xml);
			XmlNodeList nodeDhlOrders = xmldoc.SelectNodes(".//order");
			if (null == nodeDhlOrders || nodeDhlOrders.Count <= 0)
				return;

			foreach (XmlNode nodeDhlOrder in nodeDhlOrders)
			{
				string orderId = nodeDhlOrder.Attributes.GetNamedItem("order_id").Value;
				string buyer = nodeDhlOrder.Attributes.GetNamedItem("buyer").Value;
				string shipmentNumber = nodeDhlOrder.Attributes.GetNamedItem("shipment_number").Value;
				string payingTime = nodeDhlOrder.Attributes.GetNamedItem("paying_time").Value;
				string pdfTime = nodeDhlOrder.Attributes.GetNamedItem("pdf_time").Value;
				string pickupTime = nodeDhlOrder.Attributes.GetNamedItem("pickup_time").Value;
				string resendTime = nodeDhlOrder.Attributes.GetNamedItem("resend_time").Value;
				DhlOrder o = new DhlOrder(orderId, buyer, shipmentNumber);

				DateTime dt = DateTime.MinValue;
				if (DateTime.TryParse(payingTime, out dt))
					o.PayingTime = dt;
				if (DateTime.TryParse(pdfTime, out dt))
					o.PdfTime = dt;
				if (DateTime.TryParse(pickupTime, out dt))
					o.PickedupTime = dt;
				if (DateTime.TryParse(resendTime, out dt))
					o.ResendTime = dt;
				DhlOrder.DhlOrders.Add(o);
			}

			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("成功下载{0}个订单的DHL数据.", DhlOrder.DhlOrders.Count);
			prompt.RefreshDisplay();
			prompt.OKEnabled = true;
		}
		
		private List<Order> AnalyseCsv(string csvFile)
		{
			System.IO.StreamReader reader = new System.IO.StreamReader(csvFile, Encoding.Default);
			if (!reader.EndOfStream)
				reader.ReadLine(); // first line is head.

			List<Order> orders = new List<Order>();
			
			while (!reader.EndOfStream)
			{
				Order o = Order.Parse(reader.ReadLine());
				if (null == o)
					continue;
				orders.Add(o);				
			}

			reader.Close();
			return orders;
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;

			// Get folder for google downloads.
			string documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string user = Directory.GetParent(documents).FullName;
			string downloads = Path.Combine(user, "downloads");

			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "Taobao exported file (*.csv)|*.csv|Parsed orders file (*.xml)|*.xml";
			ofd.FilterIndex = 0;
			ofd.Multiselect = false;
			ofd.ShowReadOnly = false;

			if (Directory.Exists(downloads))
				ofd.InitialDirectory = downloads;

			if (DialogResult.OK == ofd.ShowDialog(this))
			{
				if (Path.GetExtension(ofd.FileName).ToLower().Equals(".xml"))
					_orders = Order.LoadXmlFile(ofd.FileName, false);
				else if (Path.GetExtension(ofd.FileName).ToLower().Equals(".csv"))
					_orders = AnalyseCsv(ofd.FileName);
			}

			if (null == _orders)
				return;

			int total = 0;
			foreach (Order o in _orders)
			{
				if (!o.ShipmentCompany.Equals("其他"))
					continue;
				if (!o.ShipmentNumber.StartsWith("297808") && !o.ShipmentNumber.StartsWith("960"))
					continue;
				DhlOrder dhlorder = DhlOrder.GetItem(o.OrderId);
				if (null != dhlorder && dhlorder.Pickedup)
					continue;
				total++;
			}

			lblInfo.Text = string.Format("{0}/{1}", 0+1, total);
			foreach (Order o in _orders)
			{
				if (!o.ShipmentCompany.Equals("其他"))
					continue;
				if (!o.ShipmentNumber.StartsWith("297808") && !o.ShipmentNumber.StartsWith("960"))
					continue;
				DhlOrder dhlorder = DhlOrder.GetItem(o.OrderId);
				if (null != dhlorder && dhlorder.Pickedup)
					continue;
				OrderListViewItem lvi = new OrderListViewItem(lvwOrders.Items.Count + 1, o, lvwOrders);
				lvwOrders.Items.Add(lvi);
			}

			Cursor.Current = Cursors.Default;
		}
		
		private void btnReport_Click(object sender, EventArgs e)
		{

		}

	}
}