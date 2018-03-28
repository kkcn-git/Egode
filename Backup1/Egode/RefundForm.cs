using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.IO;

namespace Egode
{
	public partial class RefundForm : Form
	{
		#region class Refund
		public class Refund
		{
			private readonly string _operator;
			private readonly DateTime _date;
			private readonly string _src;
			private readonly string _shipmentNumber;
			private readonly string _item;
			private readonly string _comment;

			public Refund(string op, DateTime date, string shipmentNumber, string src, string item, string comment)
			{
				_operator = op;
				_date = date;
				_src = src;
				_shipmentNumber = shipmentNumber;
				_item = item;
				_comment = comment;
			}
			
			public string Operator
			{
				get { return _operator; }
			}
			
			public DateTime Date
			{
				get { return _date; }
			}
			
			public string Src
			{
				get { return _src; }
			}
			
			public string ShipmentNumber
			{
				get { return _shipmentNumber; }
			}
			
			public string Item
			{
				get { return _item; }
			}
			
			public string Comment
			{
				get { return _comment; }
			}
			
			public bool Match(string keyword)
			{
				if (string.IsNullOrEmpty(keyword))
					return true;
				if (_src.Contains(keyword))
					return true;
				if (_shipmentNumber.Contains(keyword))
					return true;
				if (_item.Contains(keyword))
					return true;
				if (_comment.Contains(keyword))
					return true;
				return false;
			}
		}
		#endregion

		#region class RefundListViewItem
		private class RefundListViewItem : ListViewItem
		{
			private Refund _refund;
			
			public RefundListViewItem(Refund r)
			{
				_refund = r;
				this.Text = User.GetDisplayName(r.Operator);
				this.SubItems.Add(_refund.Date.ToString("yyyy-MM-dd HH:mm:ss"));
				this.SubItems.Add(_refund.ShipmentNumber);
				this.SubItems.Add(_refund.Src);
				this.SubItems.Add(_refund.Item);
				this.SubItems.Add(_refund.Comment);
			}
			
			public Refund Refund
			{
				get { return _refund; }
			}
		}
		#endregion
		
		private List<Refund> _refunds;

		public RefundForm()
		{
			InitializeComponent();
		}

		private void StockStatForm_Shown(object sender, EventArgs e)
		{
			//HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(string.Format(MainForm.URL_DATA_CENTER, "getstockhistorysh"));
			//request.Method = "GET";
			//request.ContentType = "text/xml";
			//WebResponse response = request.GetResponse();
			//StreamReader reader = new StreamReader(response.GetResponseStream());
			////Trace.WriteLine(reader.ReadToEnd());
			////Trace.WriteLine("");

			StartDownloadRefunds();
		}
		
		private void StartDownloadRefunds()
		{
			PromptForm prompt = new PromptForm();
			prompt.Owner = this;
			prompt.Show(this);
			prompt.AddMessage("正在下载退货记录...");

			WebClient wc = new WebClient();
			wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
			wc.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wc_DownloadDataCompleted);
			wc.DownloadDataAsync(new Uri(Common.URL_REFUND), prompt);
		}

		void wc_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;
			prompt.OKEnabled = true;

			string xml = Encoding.UTF8.GetString(e.Result);
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			
			XmlNodeList nlRefunds = doc.SelectNodes(".//refund");
			if (null == nlRefunds || nlRefunds.Count <= 0)
				return;

			_refunds = new List<Refund>();

			foreach (XmlNode nodeRefund in nlRefunds)
			{
				string op = nodeRefund.Attributes.GetNamedItem("operator").Value;
				string date = nodeRefund.Attributes.GetNamedItem("date").Value;
				string shipmentNo = nodeRefund.Attributes.GetNamedItem("shipment_no").Value;
				string src = nodeRefund.Attributes.GetNamedItem("src").Value;
				string item = nodeRefund.Attributes.GetNamedItem("item").Value;
				string comment = nodeRefund.Attributes.GetNamedItem("comment").Value;
				_refunds.Add(new Refund(op, DateTime.Parse(date), shipmentNo, src, item, comment));
			}
			
			if (null == _refunds || _refunds.Count <= 0)
				return;
			
			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("下载退货记录完成: 共下载{0}条退货记录%", _refunds.Count);
			prompt.RefreshDisplay();

			lvwRefunds.Items.Clear();
			
			foreach (Refund r in _refunds)
			{
				RefundListViewItem ritem = new RefundListViewItem(r);
				lvwRefunds.Items.Add(ritem);
				//ritem.EnsureVisible();
			}
		}

		void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;
			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("正在下载退货记录...{0}%", e.ProgressPercentage);
			prompt.RefreshDisplay();
		}

		private void tsbtnAddRefund_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
		
			AddRefundForm arf = new AddRefundForm();
			if (DialogResult.OK == arf.ShowDialog(this))
			{
				string url = string.Format(Common.URL_DATA_CENTER, "refund");
				url += string.Format("&op={0}&src={1}&shp={2}&itm={3}&cmt={4}", Settings.Operator, arf.Src, arf.ShipmentNumber, arf.Item.Replace("+", "@p@"), arf.Comment);
				HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
				request.Method = "GET";
				request.ContentType = "text/xml";
				WebResponse response = request.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream());
				string result = reader.ReadToEnd();
				reader.Close();
				MessageBox.Show(result);
			}
			
			Cursor.Current = Cursors.Default;
		}

		private void tsbtnRefresh_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			StartDownloadRefunds();
			Cursor.Current = Cursors.Default;
		}

		private void tsbtnSearch_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
		
			lvwRefunds.Items.Clear();
		
			foreach (Refund r in _refunds)
			{
				if (r.Match(txtKeyword.Text.Trim()))
					lvwRefunds.Items.Add(new RefundListViewItem(r));
			}
			
			Cursor.Current = Cursors.Default;
		}

		private void txtKeyword_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
				tsbtnSearch_Click(tsbtnSearch, EventArgs.Empty);
		}
	}
}