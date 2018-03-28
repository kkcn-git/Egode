using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Sgml;
using System.Net;
using System.IO;
using System.Threading;
using Egode.WebBrowserForms;
using OrderLib;

namespace Egode
{
	public partial class RateForm : Form
	{
		private string[] _ignoreBuyers = new string[] { "wutao4742512" };
	
		private class OrderListViewItem : ListViewItem
		{
			private Order _order;
			public Order Order
			{
				get { return _order; }
			}
			
			public OrderListViewItem(int index, Order order, bool buyerRated)
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
				else if (!order.ShipmentNumber.StartsWith("297808") && !order.ShipmentNumber.StartsWith("960") && !order.ShipmentNumber.StartsWith("5328"))
				{
					this.SubItems.Add("Not DHL packet");
					this.ForeColor = Color.Green;
				}
				else
				{
					this.SubItems.Add("Retrieving...");
					Application.DoEvents();
					string city = string.Empty;
					this.SubItems[this.SubItems.Count - 1].Text = buyerRated ? "buyer rated" : GetPacketRecentStatus(order.ShipmentNumber, out city);

					bool delivered = this.SubItems[this.SubItems.Count - 1].Text.ToLower().Contains("successfully");
					bool deliveredInGermany = (delivered && city.ToLower().Equals("germany"));

					if (buyerRated || (delivered && !deliveredInGermany))
						this.ForeColor = Color.Green;
					if (deliveredInGermany)
					{
						System.Diagnostics.Trace.WriteLine(this.Order.BuyerAccount);
						this.ForeColor = Color.Purple;
					}
				}
			}

			private string GetPacketRecentStatus(string shipmentNumber, out string recentCity)
			{
				recentCity = string.Empty;
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

				XmlNodeList nlShipmentDetailsTd = doc.SelectNodes(".//td[@class='']");
				if (null != nlShipmentDetailsTd && nlShipmentDetailsTd.Count > 0)
				{
					string datetime = nlShipmentDetailsTd[nlShipmentDetailsTd.Count - 3].InnerText.Trim();
					string city = nlShipmentDetailsTd[nlShipmentDetailsTd.Count - 2].InnerText.Trim();
					string status = nlShipmentDetailsTd[nlShipmentDetailsTd.Count - 1].InnerText.Trim();
					recentCity = city;
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
	
		public RateForm(List<Order> orders)
		{
			_orders = orders;
			InitializeComponent();
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

		private void lvwOrders_DoubleClick(object sender, EventArgs e)
		{
			Rate(((OrderListViewItem)lvwOrders.SelectedItems[0]).Order);
		}
		
		private void Rate(Order o)
		{
			if (!chkBuyerRated.Checked)
			{
				foreach (string buyer in _ignoreBuyers)
				{
					if (buyer.ToLower().Trim().Equals(o.BuyerAccount.ToLower().Trim()))
						return;
				}
			}

			RateBuyerWebBrowserForm.Instance.TradeId = o.OrderId;
			RateBuyerWebBrowserForm.Instance.ShowDialog(this);
		}

		private void btnGo_Click(object sender, EventArgs e)
		{
			int c = 0;
		
			foreach (OrderListViewItem item in lvwOrders.Items)
			{
				if (item.ForeColor == Color.Green)
				{
					Rate(item.Order);
					c++;
				}
			}

			MessageBox.Show(
				this, 
				string.Format("自动评价完成.\n共评价{0}个订单.", c), 
				this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

			foreach (Order o in _orders)
			{
				if (o.ShipmentNumber.StartsWith("DE") || o.ShipmentNumber.StartsWith("4008") || o.ShipmentNumber.StartsWith("3STIFD"))
					continue;
				if (o.ShipmentCompany.Contains("POSTNL"))
					continue;
			
				OrderListViewItem lvi = new OrderListViewItem(lvwOrders.Items.Count + 1, o, chkBuyerRated.Checked);
				lvwOrders.Items.Add(lvi);
				lvi.EnsureVisible();
			}

			MessageBox.Show(this, "Done.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

			Cursor.Current = Cursors.Default;
		}
	}
}