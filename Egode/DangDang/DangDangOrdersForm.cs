using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Egode;

namespace Dangdang
{
	public partial class DangDangOrdersForm : Form
	{
		#region class DangdangOrderListViewItem
		private class DangdangOrderListViewItem : ListViewItem
		{
			private DangdangOrder _order;
			public DangdangOrderListViewItem(DangdangOrder o)
			{
				_order = o;
				this.Text = _order.OrderId;
				this.SubItems.Add(_order.DealTime.ToString("yyyy/MM/dd HH:mm:ss"));
				this.SubItems.Add(_order.PayTime.ToString("yyyy/MM/dd HH:mm:ss"));
				
				ProductInfo pi = ProductInfo.GetProductByDangdangCode(_order.UniqueProductCode);
				if (null != pi)
					this.SubItems.Add(ProductInfo.GetProductByDangdangCode(_order.UniqueProductCode).ShortName); //_order;-));
				else
					this.SubItems.Add(string.Empty);
					
				this.SubItems.Add(_order.ActualCount.ToString());
				this.SubItems.Add(_order.Status);
				this.SubItems.Add(_order.RecipientName);
				this.SubItems.Add(_order.IdNumber);
				this.SubItems.Add(_order.Mobile);
				this.SubItems.Add(_order.Address);
				this.SubItems.Add(_order.PaymentId);

				this.UseItemStyleForSubItems = false;
				switch (_order.Status)
				{
					case "等待配货":
						this.SubItems[5].ForeColor = Color.Blue;
						break;
					case "等待发货":
						this.SubItems[5].ForeColor = Color.OrangeRed;
						break;
					case "已送达":
						this.UseItemStyleForSubItems = true;
						this.ForeColor = Color.Green;
						break;
					case "等待到款":
					case "取消":
						this.UseItemStyleForSubItems = true;
						this.ForeColor = Color.LightGray;
						break;
				}
			}
			
			public DangdangOrder Order
			{
				get { return _order; }
			}
		}
		#endregion
		
		public DangDangOrdersForm()
		{
			InitializeComponent();
		}

		void StartDownload()
		{
			PromptForm prompt = new PromptForm();
			prompt.MaxLine = 2;
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
			prompt.OKEnabled = true;
		}
		
		private void tsbtnImportDangdangOrders_Click(object sender, EventArgs e)
		{
			if (lvwOrders.Items.Count > 0)
			{
				DialogResult dr = MessageBox.Show(this, "当前列表中的订单信息将会被清除.\n是否加载订单文件?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (DialogResult.No == dr)
					return;
			}
		
			Cursor.Current = Cursors.WaitCursor;
			
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "Excel Files(*.xlsx)|*.xlsx|Excel Files 97-2003(*.xls)|*.xls|All Files(*.*)|*.*";
			if (DialogResult.OK == ofd.ShowDialog(this))
			{
				Egode.Excel excel = null;
				try
				{
					excel = new Egode.Excel(ofd.FileName, Excel.OledbVersions.OLEDB12);
					DataSet ds = excel.Get("Sheet0", string.Empty);
					lvwOrders.Items.Clear();
					for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
					{
						DataRow row = ds.Tables[0].Rows[i];
						string orderId = row.ItemArray[1].ToString();
						string sellerId = row.ItemArray[2].ToString();
						string recipientName = row.ItemArray[3].ToString();
						string idNumber = row.ItemArray[4].ToString();
						string mobile = row.ItemArray[5].ToString();
						string address = row.ItemArray[6].ToString();
						string deliveryType = row.ItemArray[7].ToString();
						string deliveryTime = row.ItemArray[8].ToString();
						string paymentType = row.ItemArray[9].ToString();
						float totalMoney = float.Parse(row.ItemArray[10].ToString()); ///
						DateTime dealTime = DateTime.Parse(row.ItemArray[15].ToString());
						DateTime payTime = string.IsNullOrEmpty(row.ItemArray[16].ToString()) ? DateTime.MinValue : DateTime.Parse(row.ItemArray[16].ToString());
						string invoice = row.ItemArray[20].ToString();
						string status = row.ItemArray[24].ToString();
						float fee = float.Parse(row.ItemArray[26].ToString());
						float tax = float.Parse(row.ItemArray[28].ToString());
						string productCode = row.ItemArray[29].ToString();
						int count = int.Parse(row.ItemArray[30].ToString());
						float price = float.Parse(row.ItemArray[31].ToString());
						string paymentId = row.ItemArray[32].ToString();
						string device = row.ItemArray[33].ToString();
						DateTime consigningTime = string.IsNullOrEmpty(row.ItemArray[17].ToString()) ? DateTime.MinValue : DateTime.Parse(row.ItemArray[17].ToString());
						string shipmentCompany = row.ItemArray[21].ToString();
						string shipmentNumber = row.ItemArray[22].ToString();
						
						DangdangOrder o = new DangdangOrder(
							orderId, sellerId, 
							recipientName, idNumber, mobile, address, 
							deliveryType, deliveryTime, paymentType, totalMoney, 
							dealTime, payTime, invoice, status, fee, tax, productCode, count, price,
							paymentId,
							device,
							consigningTime, shipmentCompany, shipmentNumber);
						lvwOrders.Items.Add(new DangdangOrderListViewItem(o));
					}
				}
				finally
				{
					if (null != excel && excel.Opened)
					{
						excel.Close();
					}
				}
			}
			
			Cursor.Current = Cursors.Default;
		}

		private void tsbtnExportYydy_Click(object sender, EventArgs e)
		{
			// Obsoleted code.
			//if (lvwOrders.SelectedItems.Count <= 0)
			//{
			//    MessageBox.Show(this, "请选择需要导出的订单先.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//    return;
			//}

			//Cursor.Current = Cursors.WaitCursor;
			
			//try
			//{
			//    SaveFileDialog sfd = new SaveFileDialog();
			//    sfd.OverwritePrompt = true;
			//    sfd.Filter = "Excel Files(*.xls)|*.xls|All Files(*.*)|*.*";
			//    sfd.FileName = string.Format("重庆订单{0}.xls", DateTime.Now.ToString("yyyyMMdd"));
			//    if (DialogResult.OK == sfd.ShowDialog(this))
			//    {
			//        string templateFilename = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "重庆订单模板.xls");
			//        File.Copy(templateFilename, sfd.FileName, true);
			//        Egode.Excel excel = new Egode.Excel(sfd.FileName);
					
			//        try
			//        {
			//            foreach (DangdangOrderListViewItem item in lvwOrders.SelectedItems)
			//            {
			//                // obsoleted.
			//                //object[] args = new object[]{
			//                //    DateTime.Now.ToString("yyyy.MM.dd"), 
			//                //    ProductInfo.GetYydyProductName(item.Order.UniqueProductCode), item.Order.ActualCount.ToString(), 
			//                //    string.Empty, string.Empty, string.Empty, string.Empty, 
			//                //    item.Order.RecipientName, item.Order.Mobile, item.Order.IdNumber, item.Order.Address, 
			//                //    string.Empty, item.Order.OrderId};
			//                DangdangAddressParser ai = new DangdangAddressParser(item.Order.Address);

			//                object[] args = new object[]{
			//                    string.Empty, 
			//                    DateTime.Now.ToString("yyyy.MM.dd"), 
			//                    item.Order.RecipientName, item.Order.Mobile, 
			//                    ai.Province, ai.City, ai.District, ai.StreetAddress, 
			//                    item.Order.IdNumber, item.Order.OrderId, 
			//                    ProductInfo.GetYydyProductName(item.Order.UniqueProductCode), item.Order.ActualCount.ToString(), 
			//                    "98.00", "0.00", (98f*item.Order.ActualCount).ToString("0.00"), 
			//                    "EMS"};
			//                excel.Insert("Sheet1", string.Empty, args);
			//            }
						
			//            MessageBox.Show(this, "导出成功.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			//        }
			//        catch (Exception ex)
			//        {
			//            MessageBox.Show(ex.ToString());
			//        }
			//        finally
			//        {
			//            if (null != excel && excel.Opened)
			//                excel.Close();
			//        }
			//    }
			//}
			//finally
			//{
			//    Cursor.Current = Cursors.Default;
			//}
		}

		private void tsbtnExportHigo_Click(object sender, EventArgs e)
		{
			if (lvwOrders.SelectedItems.Count <= 0)
			{
				MessageBox.Show(this, "请选择需要导出的订单先.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			Cursor.Current = Cursors.WaitCursor;

			try
			{
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Filter = "Excel Files(*.xls)|*.xls|All Files(*.*)|*.*";
				sfd.FileName = string.Format("buy{0}-dd.xls", DateTime.Now.ToString("yyyyMMdd"));
				sfd.OverwritePrompt = true;
				if (DialogResult.OK == sfd.ShowDialog(this))
				{
					string templateFilename = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "template-higo.xls");
					File.Copy(templateFilename, sfd.FileName);
					Egode.Excel excel = new Egode.Excel(sfd.FileName, Excel.OledbVersions.OLEDB40);

					foreach (DangdangOrderListViewItem item in lvwOrders.SelectedItems)
					{
						DangdangAddressParser ai = new DangdangAddressParser(item.Order.Address);
						//Trace.WriteLine(item.Order.Address);
						//Trace.WriteLine(string.Format("{0}, {1}, {2}, {3}", ai.Province, ai.City, ai.District, ai.StreetAddress));
						//Trace.WriteLine(string.Empty);
						//continue;
					
						object[] args = new object[]{ 
							"SAL06", "NBBLK", "邮政国内小包", item.Order.OrderId, 
							string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
							"否", 
							string.Empty, string.Empty,
							string.Empty, 
							item.Order.RecipientName, item.Order.Mobile, 
							string.Empty, 
							ai.Province, ai.City, ai.District, ai.StreetAddress, 
							ProductInfo.GetProductByDangdangCode(item.Order.UniqueProductCode).NingboId, 
							string.Empty, string.Empty, 
							item.Order.ActualCount, 
							string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
							"否", 
							string.Empty, string.Empty,
							string.Format("{0},{1}", item.Order.RecipientName, item.Order.IdNumber), "支付宝", string.Empty,
							string.Empty, string.Empty, string.Empty, string.Empty, string.Empty};

						excel.Insert("合作代发订单导入模板", string.Empty, args);
					}

					excel.Close();
				}
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void tsbtnExportYt_Click(object sender, EventArgs e)
		{
			// obsoleted code.
			//if (lvwOrders.SelectedItems.Count <= 0)
			//{
			//    MessageBox.Show(this, "请选择需要导出的订单先.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//    return;
			//}

			//Cursor.Current = Cursors.WaitCursor;

			//try
			//{
			//    SaveFileDialog sfd = new SaveFileDialog();
			//    sfd.Filter = "Excel Files(*.xls)|*.xls|All Files(*.*)|*.*";
			//    sfd.FileName = string.Format("洋驼订单{0}.xls", DateTime.Now.ToString("yyyyMMdd"));
			//    sfd.OverwritePrompt = true;
			//    if (DialogResult.OK == sfd.ShowDialog(this))
			//    {
			//        string templateFilename = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "template-yt.xls");
			//        File.Copy(templateFilename, sfd.FileName, true);
			//        Egode.Excel excel = new Egode.Excel(sfd.FileName);
			//        Random rndOrderRandomPostfix = new Random(Environment.TickCount);

			//        foreach (DangdangOrderListViewItem item in lvwOrders.SelectedItems)
			//        {
			//            DangdangAddressParser ai = new DangdangAddressParser(item.Order.Address);
			//            string address = item.Order.Address;
			//            if (address.StartsWith("中国"))
			//                address = address.Remove(0, 2);
			//            object[] args = new object[]{ 
			//                string.Format("{0}", item.Order.ActualCount), 
			//                "德国e购",
			//                item.Order.OrderId + rndOrderRandomPostfix.Next(999).ToString("000"),
			//                item.Order.RecipientName, item.Order.Mobile, "无留言", 
			//                string.Format("{0}{1}{2}{3}", ai.Province, ai.City.Equals(ai.Province)?string.Empty:ai.City, ai.District, ai.StreetAddress), 
			//                ProductInfo.GetYtCode(item.Order.UniqueProductCode), item.Order.RecipientName, item.Order.IdNumber};

			//            excel.Insert("sheet1", string.Empty, args);
			//        }

			//        excel.Close();
			//    }
			//}
			//finally
			//{
			//    Cursor.Current = Cursors.Default;
			//}
		}

		private void lvwOrders_DoubleClick(object sender, EventArgs e)
		{
			if (lvwOrders.SelectedItems.Count <= 0)
				return;
		
			Cursor.Current = Cursors.WaitCursor;
			
			new DangdangOrderDetailsForm(((DangdangOrderListViewItem)lvwOrders.SelectedItems[0]).Order).ShowDialog(this);
			
			Cursor.Current = Cursors.Default;
		}

		private void DangDangOrdersForm_Shown(object sender, EventArgs e)
		{
			StartDownload();
		}
	}
}