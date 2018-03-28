using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OrderParser;
using System.Xml;
using Sgml;
using System.Net;
using System.IO;
using System.Threading;

namespace Egode
{
	public partial class CommentBuyerForm : Form
	{
		private class OrderListViewItem : ListViewItem
		{
			private Order _order;
			public Order Order
			{
				get { return _order; }
			}
			
			public OrderListViewItem(int index, Order order)
			{
				_order = order;
				this.Text = index.ToString("00");
				this.SubItems.Add(order.OrderId);
				this.SubItems.Add(order.BuyerAccount);
				this.SubItems.Add(order.ShipmentNumber);
				
				if (string.IsNullOrEmpty(order.ShipmentNumber))
				{
					this.SubItems.Add("The order not sent");
					this.ForeColor = Color.LightGray;
				}
				else if (!order.ShipmentNumber.StartsWith("297808") && !order.ShipmentNumber.StartsWith("960"))
				{
					this.SubItems.Add("Not DHL packet");
					this.ForeColor = Color.LightGray;
				}
				else
				{
					this.SubItems.Add("Retrieving...");
					Application.DoEvents();
					this.SubItems[this.SubItems.Count - 1].Text = GetPacketRecentStatus(order.ShipmentNumber);
					
					if (this.SubItems[this.SubItems.Count - 1].Text.ToLower().Contains("successfully"))
						this.ForeColor = Color.Green;
				}
			}

			private string GetPacketRecentStatus(string shipmentNumber)
			{
				string url = string.Format(
					"http://nolp.dhl.de/nextt-online-public/set_identcodes.do?lang=en&idc={0}&rfn=&extendedSearch=true",
					shipmentNumber);
				//wb.Navigate(url);

				string html = string.Empty;

				try
				{
					HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
					HttpWebResponse response = (HttpWebResponse)request.GetResponse();
					Stream s = response.GetResponseStream();
					StreamReader sr = new StreamReader(s);
					html = sr.ReadToEnd();
					sr.Close();
					s.Close();
				}
				catch (WebException)
				{
					return "Cannot connect to DHL website.";
				}

				if (html.Contains("Unfortunately"))
				{
					return "没有找到对应单号";
				}

				XmlDocument doc = ConvertHtmlToXml(html);
				//System.Diagnostics.Trace.WriteLine(doc.OuterXml);

				//XmlNode nodeUpuCode = doc.SelectSingleNode(".//td[text()='UPU code / matchcode']");
				//if (null != nodeUpuCode)
				//    _upuCode = nodeUpuCode.NextSibling.InnerText;

				XmlNodeList nlLocation = doc.SelectNodes(".//td[@class='location']");
				if (null != nlLocation && nlLocation.Count > 0)
				{
					XmlNode nodeLocation = nlLocation[nlLocation.Count - 1];
					string datetime = nodeLocation.PreviousSibling.InnerText.Trim();
					string city = nodeLocation.InnerText.Trim();
					string status = nodeLocation.NextSibling.InnerText.Trim();
					return string.Format("{0}: {1}", datetime, status);
				}
				
				return "Unknown status";
			}
			
			public static XmlDocument ConvertHtmlToXml(string html)
			{
				Sgml.SgmlReader sgmlReader = new Sgml.SgmlReader();
				sgmlReader.DocType = "HTML";
				sgmlReader.WhitespaceHandling = System.Xml.WhitespaceHandling.All;
				sgmlReader.CaseFolding = Sgml.CaseFolding.ToLower;
				sgmlReader.InputStream = new System.IO.StringReader(html);

				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.PreserveWhitespace = false;
				xmlDoc.XmlResolver = null;
				xmlDoc.Load(sgmlReader);

				return xmlDoc;
			}
		}
	
		List<Order> _orders;
	
		public CommentBuyerForm(List<Order> orders)
		{
			_orders = orders;
			InitializeComponent();
		}

		protected override void OnShown(EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
		
			base.OnShown(e);
			
			foreach (Order o in _orders)
			{
				OrderListViewItem lvi = new OrderListViewItem(lvwOrders.Items.Count + 1, o);
				lvwOrders.Items.Add(lvi);
				lvi.EnsureVisible();
			}
			
			Cursor.Current = Cursors.Default;
		}

		private void lvwOrders_DoubleClick(object sender, EventArgs e)
		{
			string orderId = ((OrderListViewItem)lvwOrders.SelectedItems[0]).Order.OrderId;
			string url = string.Format(@"http://rate.taobao.com/remark_buyer.jhtml?tradeID={0}", orderId);
			WebBrowserForm wbf = new WebBrowserForm(url);
			wbf.WindowState = FormWindowState.Maximized;
			wbf.ShowDialog(this);
		}
	}
}