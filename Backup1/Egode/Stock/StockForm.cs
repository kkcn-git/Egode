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
	public partial class StockForm : Form
	{
		#region class StockHistoryRecordListViewItem
		private class StockHistoryRecordListViewItem : ListViewItem
		{
			private StockHistoryRecord _record;
			
			public StockHistoryRecordListViewItem(StockHistoryRecord record)
			{
				_record = record;
				this.Text = User.GetDisplayName(_record.Op);
				this.SubItems.Add(_record.DateTime.ToString("yyyy-MM-dd HH:mm:ss"));
				this.SubItems.Add(ProductInfo.GetProductInfo(_record.ProductId).Name);
				this.SubItems.Add(_record.Count.ToString());
				this.SubItems.Add(_record.FromTo);
				this.SubItems.Add(_record.Comment);
				
				if (_record.Count < 0)
					this.ForeColor = Color.FromArgb(0xff, 0xff, 0x40, 0x40);
				else if (_record.Count > 0)
					this.ForeColor = Color.DarkGreen;
			}

			public StockHistoryRecord Record
			{
				get { return _record; }
			}
		}
		#endregion
		
		private readonly OrderLib.ShippingOrigins _stockLocation;
		private List<StockHistoryRecord> _history;
		private ComboBox _cboStockTypes;
		private ComboBox _cboProducts;
		
		public StockForm(OrderLib.ShippingOrigins stockLocation)
		{
			_stockLocation = stockLocation;
			InitializeComponent();
			//tcMain.SelectTab(1);
		}

		private void StockForm_Load(object sender, EventArgs e)
		{
			_cboStockTypes = new ComboBox();
			_cboStockTypes.DropDownStyle = ComboBoxStyle.DropDownList;
			_cboStockTypes.Items.Add("Show only stockin");
			_cboStockTypes.Items.Add("Show only stockout");
			_cboStockTypes.Items.Add("Show both");
			_cboStockTypes.SelectedIndex = 2;
			_cboStockTypes.SelectedIndexChanged += new EventHandler(cboStockTypes_SelectedIndexChanged);
			tsMain.Items.Add(new ToolStripControlHost(_cboStockTypes));

			_cboProducts = new ComboBox();
			_cboProducts.DropDownStyle = ComboBoxStyle.DropDownList;
			_cboProducts.DropDownWidth = 260;
			_cboProducts.Items.Add(new ProductInfo("0", "0", string.Empty, "0", "0", "Show All", 0.0f, "0", "Show All", string.Empty, false));
			_cboProducts.SelectedIndex = 0;
			tsMain.Items.Add(new ToolStripSeparator());
			tsMain.Items.Add(new ToolStripControlHost(_cboProducts));
			
			switch (_stockLocation)
			{
				case OrderLib.ShippingOrigins.Shanghai:
					this.Text = "Stock - Shanghai";
					break;
				case OrderLib.ShippingOrigins.Ningbo:
					this.Text = "Stock - Ningbo";
					break;
			}
			
			tcMain_SelectedIndexChanged(tcMain, EventArgs.Empty);
		}

		void _cboProducts_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshHistoryList(_history);
		}

		void cboStockTypes_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshHistoryList(_history);
		}
		
		private void StockForm_Shown(object sender, EventArgs e)
		{
			StartDownload();
		}

		void StartDownload()
		{
			PromptForm prompt = new PromptForm();
			prompt.MaxLine = 3;
			prompt.Owner = this;
			prompt.Show(this);

			StartDownloadProductInfos(prompt);
		}
		
		void StartDownloadProductInfos(PromptForm prompt)
		{
			prompt.AddMessage("正在下载产品信息...0%");
			WebClient wc = new WebClient();
			wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wcProductInfo_DownloadProgressChanged);
			wc.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wcProductInfo_DownloadDataCompleted);
			wc.DownloadDataAsync(new Uri(Common.URL_PRODUCTS), prompt);
		}

		void wcProductInfo_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;
			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("正在下载产品信息...{0}%", e.ProgressPercentage);
			prompt.RefreshDisplay();
		}

		void wcProductInfo_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;

			string xml = Encoding.UTF8.GetString(e.Result);
			BrandInfo.InitializeBrands(xml);
			ProductInfo.InitializeProducts(xml);			

			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("成功下载{0}个产品信息.", ProductInfo.Products.Count);
			prompt.RefreshDisplay();
			
			StartDownloadStockHistory(prompt);
		}
		
		void StartDownloadStockHistory(PromptForm prompt)
		{
			string dbfilename = string.Empty;
			switch (_stockLocation)
			{
				case OrderLib.ShippingOrigins.Shanghai:
					dbfilename = Common.URL_STOCK_SH;
					break;
				case OrderLib.ShippingOrigins.Ningbo:
					dbfilename = Common.URL_STOCK_NINGBO;
					break;
			}
		
			prompt.AddMessage("正在下载出入库记录...0%");
			WebClient wcDownloadStockHistory = new WebClient();
			wcDownloadStockHistory.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wcDownloadStockHistory_DownloadProgressChanged);
			wcDownloadStockHistory.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wcDownloadStockHistory_DownloadDataCompleted);
			wcDownloadStockHistory.DownloadDataAsync(new Uri(dbfilename), prompt);
		}

		void wcDownloadStockHistory_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;
			prompt.OKEnabled = true;

			string xml = Encoding.UTF8.GetString(e.Result);
			_history = GetStockHistory(xml);
			RefreshHistoryList(_history);
			RefreshStockList(_history);
			
			//int c = 0;
			//foreach (StockHistoryRecord r in _history)
			//{
			//    if (r.DateTime < new DateTime(2014, 02, 10))
			//        continue;
			//    if (!r.FromTo.Equals("德诺"))
			//        continue;
			//    if (!r.ProductId.Equals("001-0005"))
			//        continue;
			//    c += r.Count;
			//}
			//System.Diagnostics.Trace.WriteLine(c);
		}

		void wcDownloadStockHistory_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;
			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("正在下载出入库数据...{0}%", e.ProgressPercentage);
			prompt.RefreshDisplay();
		}
		
		private List<StockHistoryRecord> GetStockHistory(string xml)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			
			XmlNodeList nlStockout = doc.SelectNodes(".//action");
			List<StockHistoryRecord> history = new List<StockHistoryRecord>();
			
			foreach (XmlNode nodeStockout in nlStockout)
			{
				string op = nodeStockout.Attributes.GetNamedItem("operator").Value;
				DateTime date = DateTime.Parse(nodeStockout.Attributes.GetNamedItem("date").Value);
				string productId = nodeStockout.Attributes.GetNamedItem("product").Value;
				int count = int.Parse(nodeStockout.Attributes.GetNamedItem("amount").Value);
				string fromto = nodeStockout.Attributes.GetNamedItem("from_to").Value;
				string comment = nodeStockout.Attributes.GetNamedItem("comment").Value;

				StockHistoryRecord record = new StockHistoryRecord(op, date, productId, count, fromto, comment);
				history.Add(record);
			}
			
			return history;
		}
		
		void RefreshHistoryList(List<StockHistoryRecord> history)
		{
			Cursor.Current = Cursors.WaitCursor;
			
			lvwHistory.Items.Clear();

			if (null == history || history.Count <= 0)
				return;
			
			foreach (StockHistoryRecord r in history)
			{
				if (r.Count < 0 && _cboStockTypes.SelectedIndex == 0)
					continue;
				if (r.Count > 0 && _cboStockTypes.SelectedIndex == 1)
					continue;
				if (null != _cboProducts && _cboProducts.Items.Count > 1)
				{
					ProductInfo filterProduct = (ProductInfo)_cboProducts.SelectedItem;
					if (!r.ProductId.Equals(filterProduct.Id) && !filterProduct.Id.Equals("0"))
						continue;
				}
				if (!string.IsNullOrEmpty(txtKeyword.Text.Trim()) && !r.Match(txtKeyword.Text.Trim()))
					continue;

				StockHistoryRecordListViewItem lvi = new StockHistoryRecordListViewItem(r);
				lvwHistory.Items.Add(lvi);
			}
			
			Cursor.Current = Cursors.Default;
		}
		
		void RefreshStockList(List<StockHistoryRecord> history)
		{
			foreach (BrandInfo b in BrandInfo.Brands)
			{
				foreach (ProductInfo p in ProductInfo.Products)
				{
					if (!p.BrandId.Equals(b.Id))
						continue;

					// Fill combox of product filter on history page.
					_cboProducts.Items.Add(p);
					//

					ListViewItem lvi = lvwStock.Items.Add(b.Name);
					lvi.Tag = p;
					lvi.SubItems.Add(p.Name);
					int c = GetProductStockCount(p.Id, _history);
					lvi.SubItems.Add(c.ToString());
					lvi.SubItems.Add(c < 0 ? "-_-|||" : string.Empty);
					if (c <= 0)
						lvi.ForeColor = Color.FromArgb(0xff, 0xff, 0x40, 0x40);
				}
			}

			//foreach (BrandInfo b in BrandInfo.Brands)
			//{
			//    foreach (ProductInfo p in ProductInfo.Products)
			//        _cboProducts.Items.Add(p);
			//}
			_cboProducts.SelectedIndexChanged += new EventHandler(_cboProducts_SelectedIndexChanged);
		}
		
		private int GetProductStockCount(string productId, List<StockHistoryRecord> history)
		{
			if (string.IsNullOrEmpty(productId))
				return 0;
			if (null == history || history.Count <= 0)
				return 0;
			
			int c = 0;
			foreach (StockHistoryRecord r in history)
			{
				if (!r.ProductId.Equals(productId))
					continue;
				c += r.Count;
			}
			return c;
		}

		private void tsbtnStockout_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			
			// Removed by KK on 2015/12/26.
			// Replaced by new stockout form.
			//string brand = string.Empty, product = string.Empty;
			//if (lvwStock.SelectedItems.Count > 0)
			//{
			//    brand = ((ProductInfo)lvwStock.SelectedItems[0].Tag).BrandId;
			//    product = ((ProductInfo)lvwStock.SelectedItems[0].Tag).Id;
			//}
			
			//StockActionForm saf = new StockActionForm(true, brand, product);
			//if (DialogResult.OK == saf.ShowDialog(this))
			//    StockAction(saf.Product.Id, -1*saf.Count, saf.FromTo, Settings.Operator, saf.Comment);
			
			List<SoldProductInfo> selectedProductInfos = new List<SoldProductInfo>();
			foreach (ListViewItem lvi in lvwStock.SelectedItems)
			{
				SoldProductInfo spi = new SoldProductInfo(lvi.Tag as ProductInfo);
				spi.Count = 0;
				selectedProductInfos.Add(spi);
			}

			StockActionAdvForm saaf = new StockActionAdvForm(true, selectedProductInfos);
			if (DialogResult.OK == saaf.ShowDialog(this))
			{
				// 出库登记.
				try
				{
					StockAction(true, saaf.SelectedProductInfos, saaf.FromToFull, saaf.Comment);
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
						
			Cursor.Current = Cursors.Default;
		}

		private void tsbtnStockin_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;

			// Removed by KK on 2015/12/26.
			// Replaced by new stockout form.
			//string brand = string.Empty, product = string.Empty;
			//if (lvwStock.SelectedItems.Count > 0)
			//{
			//    brand = ((ProductInfo)lvwStock.SelectedItems[0].Tag).BrandId;
			//    product = ((ProductInfo)lvwStock.SelectedItems[0].Tag).Id;
			//}

			//StockActionForm saf = new StockActionForm(false, brand, product);
			//if (DialogResult.OK == saf.ShowDialog(this))
			//    StockAction(saf.Product.Id, saf.Count, saf.FromTo, Settings.Operator, saf.Comment);

			List<SoldProductInfo> selectedProductInfos = new List<SoldProductInfo>();
			foreach (ListViewItem lvi in lvwStock.SelectedItems)
			{
				SoldProductInfo spi = new SoldProductInfo(lvi.Tag as ProductInfo);
				spi.Count = 0;
				selectedProductInfos.Add(spi);
			}

			StockActionAdvForm saaf = new StockActionAdvForm(false, selectedProductInfos);
			if (DialogResult.OK == saaf.ShowDialog(this))
			{
				// 入库登记.
				try
				{
					StockAction(false, saaf.SelectedProductInfos, saaf.FromToFull, saaf.Comment);
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}

			Cursor.Current = Cursors.Default;
		}

		// Removed by KK on 2015/12/26.
		// Obsoleted.
		//private void StockAction(string productId, int count, string fromto, string op, string comment)
		//{
		//    string cmd = string.Empty;
		//    switch (_stockLocation)
		//    {
		//        case OrderLib.ShippingOrigins.Shanghai:
		//            cmd = "stock";
		//            break;
		//        case OrderLib.ShippingOrigins.Ningbo:
		//            cmd = "stocknb";
		//            break;
		//    }

		//    DateTime dt = DateTime.Now;
		//    string url = string.Format(Common.URL_DATA_CENTER, cmd);
		//    url += string.Format("&productids={0}&counts={1}&dest={2}&op={3}&comment={4}&date={5}", productId, count.ToString(), fromto, Settings.Operator, comment, dt.ToString("yyyy-MM-dd HH:mm:ss"));
		//    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
		//    request.Method = "GET";
		//    request.ContentType = "text/xml";
		//    WebResponse response = request.GetResponse();
		//    StreamReader reader = new StreamReader(response.GetResponseStream());
		//    string result = reader.ReadToEnd();
		//    reader.Close();
		//    //Trace.WriteLine(result);

		//    if (result.StartsWith("Succeeded"))
		//    {
		//        StockHistoryRecord r = new StockHistoryRecord(Settings.Operator, dt, productId, count, fromto, comment);
		//        StockHistoryRecordListViewItem lvi = new StockHistoryRecordListViewItem(r);
		//        lvwHistory.Items.Insert(0, lvi);
		//        lvwHistory.SelectedItems.Clear();
		//        lvi.Selected = true;
		//        lvi.EnsureVisible();
		//        lvwHistory.Focus();
		//    }

		//    MessageBox.Show(
		//        this,
		//        "Result from server: \n" + result,
		//        this.Text,
		//        MessageBoxButtons.OK, MessageBoxIcon.Information);
		//}
		private void StockAction(bool stockout, List<SoldProductInfo> stockProductInfos, string fromto, string comment)
		{
			Application.DoEvents();
			Cursor.Current = Cursors.WaitCursor;
			
			string result = StockActionAdvForm.StockAction(stockout, stockProductInfos, fromto, comment, _stockLocation);
			
			if (result.StartsWith("Succeeded"))
			{
				lvwHistory.SelectedItems.Clear();
				foreach (SoldProductInfo spi in stockProductInfos)
				{
					StockHistoryRecord r = new StockHistoryRecord(Settings.Operator, DateTime.Now, spi.Id, (stockout ? -1 : 1) * spi.Count, fromto, comment);
					StockHistoryRecordListViewItem lvi = new StockHistoryRecordListViewItem(r);
					lvwHistory.Items.Insert(0, lvi);
					lvi.Selected = true;
					lvi.EnsureVisible();
				}
				lvwHistory.Focus();
			}

			MessageBox.Show(
				this,
				"Result from server: \n" + result,
				this.Text,
				MessageBoxButtons.OK, MessageBoxIcon.Information);
			
			Cursor.Current = Cursors.Default;
		}

		private void tsbtnSearch_Click(object sender, EventArgs e)
		{
			RefreshHistoryList(_history);
			tcMain.SelectTab(1);
		}

		private void txtKeyword_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
				tsbtnSearch_Click(tsbtnSearch, EventArgs.Empty);
		}

		private void StockForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.O && e.Control)
				tsbtnStockout_Click(tsbtnStockout, EventArgs.Empty);
			else if (e.KeyCode == Keys.I && e.Control)
				tsbtnStockin_Click(tsbtnStockin, EventArgs.Empty);
		}

		private void tsbtnExport_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
		
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "csv files(*.csv)|*.csv|All Files(*.*)|*.*";
			if (DialogResult.OK != sfd.ShowDialog(this))
				return;
			
			string filename = sfd.FileName;
			if (0 == sfd.FilterIndex && !filename.ToLower().EndsWith(".csv"))
				filename += ".csv";

			StreamWriter writer = new StreamWriter(filename, false, Encoding.UTF8);
			//writer.WriteLine("\"Date\",\"Product\",\"Count\",\"FromTo\",\"Comment\"");
			writer.WriteLine("Date,Product,Count,FromTo,Comment");

			foreach (StockHistoryRecordListViewItem item in lvwHistory.Items)
				//writer.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"", 
				writer.WriteLine(string.Format("{0},{1},{2},{3},{4}", 
					item.Record.DateTime.ToString("yyyy/MM/dd HH:mm:ss"), 
					ProductInfo.GetProductInfo(item.Record.ProductId).Name.Replace(",", "，"), 
					item.Record.Count, item.Record.FromTo.Replace(",", "，"), item.Record.Comment.Replace(",", "，")));

			writer.Close();
			Cursor.Current = Cursors.Default;
		}

		private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
		{
			tsbtnDeleteRecord.Enabled = (tcMain.SelectedTab.Equals(tpHistory) && lvwHistory.SelectedItems.Count > 0);
		}

		private void lvwHistory_SelectedIndexChanged(object sender, EventArgs e)
		{
			tsbtnDeleteRecord.Enabled = (tcMain.SelectedTab.Equals(tpHistory) && lvwHistory.SelectedItems.Count > 0);
		}

		private void tsbtnDeleteRecord_Click(object sender, EventArgs e)
		{
			if (lvwHistory.SelectedItems.Count <= 0)
			{
				MessageBox.Show(this, "Select a record first.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			
			DialogResult dr = MessageBox.Show(
				this, 
				"删除出库记录操作不可逆转, 如误删除, 被删除数据不可恢复!\n是否确定要删除选中的出入库记录?", this.Text, 
				MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
			if (DialogResult.Yes != dr)
				return;
			
			Cursor.Current = Cursors.WaitCursor;
			
			string cmd = string.Empty;
			switch (_stockLocation)
			{
				case OrderLib.ShippingOrigins.Shanghai:
					cmd = "delstockrec";
					break;
				case OrderLib.ShippingOrigins.Ningbo:
					cmd = "delstockrecnb";
					break;
			}
			
			StockHistoryRecord r = ((StockHistoryRecordListViewItem)lvwHistory.SelectedItems[0]).Record;

			string url = string.Format(Common.URL_DATA_CENTER, cmd);
			url += string.Format("&date={0}&product={1}&amount={2}", r.DateTime.ToString("yyyy-MM-dd HH:mm:ss"), r.ProductId, r.Count);
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
			request.Method = "GET";
			request.ContentType = "text/xml";
			WebResponse response = request.GetResponse();
			StreamReader reader = new StreamReader(response.GetResponseStream());
			string result = reader.ReadToEnd();
			reader.Close();
			//Trace.WriteLine(result);

			if (result.StartsWith("Succeeded"))
				lvwHistory.Items.Remove(lvwHistory.SelectedItems[0]);

			MessageBox.Show(
				this,
				"Result from server: \n" + result,
				this.Text,
				MessageBoxButtons.OK, 
				result.StartsWith("Succeeded") ? MessageBoxIcon.Information : MessageBoxIcon.Exclamation);
			
			Cursor.Current = Cursors.Default;
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			new Stock.BrandProductForm().ShowDialog(this);
		}
	}
}