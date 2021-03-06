using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Egode.OuterOrder
{
	public partial class OuterOrdersForm : Form
	{
		private string _loadedFilename;
	
		public class EgodeSimpleOrder
		{
			private string _taobaoId;
			private string _product;
			private string _count;
			private string _money;
			private string _express;
			private string _billNumber;
			private string _addr;
			private string _demand;
			
			public EgodeSimpleOrder(string taobaoId, string product, string count, string money, string express, string addr, string demand)
			{
				_taobaoId = taobaoId;
				_product = product;
				_count = count;
				_money = money;
				_express = express;
				_addr = addr;
				_demand = demand;
			}
			
			public string TaobaoId
			{
				get { return _taobaoId; }
			}
			
			public string Product
			{
				get { return _product; }
			}
			
			public string Count
			{
				get { return _count; }
			}
			
			public string Money
			{
				get { return _money; }
			}
			
			public string Express
			{
				get { return _express; }
			}
			
			public string BillNumber
			{
				get { return _billNumber; }
				set { _billNumber = value; }
			}
			
			public string Addr
			{
				get { return _addr; }
			}
			
			public string Demand
			{
				get { return _demand; }
			}
		}
	
		private class OrderListViewItem : ListViewItem
		{
			private EgodeSimpleOrder _order;
			public OrderListViewItem(EgodeSimpleOrder o)
			{
				this.UseItemStyleForSubItems = true;
				_order = o;
				float money = 0; 
				float.TryParse(o.Money, out money);
				this.Text = o.TaobaoId;
				this.SubItems.Add(o.Product);
				this.SubItems.Add(o.Count);
				this.SubItems.Add(string.Format("¥{0}", money > 0 ? money.ToString("0.00") : o.Money));
				this.SubItems.Add(o.Express);
				this.SubItems.Add(o.BillNumber);
				this.SubItems.Add(o.Addr);
				this.SubItems.Add(o.Demand);
				
				if (o.Express.Equals("圆通"))
					this.SubItems[4].ForeColor = Color.Purple;

				if (!string.IsNullOrEmpty(_order.BillNumber))
				{
					this.ImageIndex = 0;
					this.ForeColor = Color.Green;
				}
				else
				{
					this.ImageIndex = -1;
				}
			}
			
			public EgodeSimpleOrder Order
			{
				get { return _order; }
			}

			public void Update()
			{
				this.SubItems[5].Text = _order.BillNumber;
				if (!string.IsNullOrEmpty(_order.BillNumber))
				{
					this.ImageIndex = 0;
					this.ForeColor = Color.Green;
				}
				else
				{
					this.ImageIndex = -1;
				}
			}
		}
	
		public OuterOrdersForm()
		{
			InitializeComponent();
		}

		private void tsbtnImportOrders_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			
			try
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
					excel = new Egode.Excel(ofd.FileName, Excel.OledbVersions.OLEDB12);
					DataSet ds = excel.Get("Sheet1", string.Empty);
					
					// get column index of total money and demand.
					int indexTaobaoId = -1;
					int indexProduct = -1;
					int indexCount = -1;
					int indexTotalMoney = -1;
					int indexAddress = -1;
					int indexExpress = -1;
					int indexBillNumber = -1;
					int indexDemand = -1;
					for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
					{
						if (null == ds.Tables[0].Rows[0][i])
							continue; 
							
						System.Diagnostics.Trace.WriteLine(i.ToString());
						System.Diagnostics.Trace.WriteLine(ds.Tables[0].Rows[0][i].ToString());
						if (ds.Tables[0].Rows[0][i].ToString().Trim().ToLower().Equals("旺旺id"))
							indexTaobaoId = i;
						if (ds.Tables[0].Rows[0][i].ToString().Trim().Contains("产品"))
							indexProduct = i;
						if (ds.Tables[0].Rows[0][i].ToString().Trim().Equals("数量"))
							indexCount = i;
						if (ds.Tables[0].Rows[0][i].ToString().Trim().Contains("金额"))
							indexTotalMoney = i;
						if (ds.Tables[0].Rows[0][i].ToString().Trim().Equals("地址"))
							indexAddress = i;
						if (ds.Tables[0].Rows[0][i].ToString().Trim().Equals("快递"))
							indexExpress = i;
						if (ds.Tables[0].Rows[0][i].ToString().Trim().Equals("快递单号"))
							indexBillNumber = i;
						if (ds.Tables[0].Rows[0][i].ToString().Trim().Equals("特殊要求"))
							indexDemand = i;
					}
					
					lvwOrders.Items.Clear();
					for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
					{
						DataRow r = ds.Tables[0].Rows[i];
						EgodeSimpleOrder o = new EgodeSimpleOrder(
							r[indexTaobaoId].ToString().Trim(), 
							r[indexProduct].ToString().Trim(), r[indexCount].ToString().Trim(), r[indexTotalMoney].ToString().Trim(), 
							r[indexExpress].ToString().Trim(), r[indexAddress].ToString().Trim(), 
							r[indexDemand].ToString().Trim());
						if (-1 != indexBillNumber)
							o.BillNumber = r[indexBillNumber].ToString().Trim();
						OrderListViewItem item = new OrderListViewItem(o);
						lvwOrders.Items.Add(item);
					}
					
					_loadedFilename = ofd.FileName;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void tsbtnExport_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (OrderListViewItem item in lvwOrders.Items)
				{
					if (string.IsNullOrEmpty(item.Order.BillNumber))
					{
						DialogResult dr = MessageBox.Show(
							this, 
							"至少存在一个订单没有单号, 是否继续导出?", this.Text, 
							MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
						if (DialogResult.No == dr)
							return;
					}
				}
			
				string initialDirectory = Path.GetDirectoryName(_loadedFilename);
				//string filename = string.Format("{0}-r.xls", Path.GetFileNameWithoutExtension(_loadedFilename));
				string filename = string.Format("{0}.csv", Path.GetFileNameWithoutExtension(_loadedFilename));
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.OverwritePrompt = true;
				sfd.InitialDirectory = initialDirectory;
				sfd.FileName = filename;
				
				if (DialogResult.OK == sfd.ShowDialog(this))
				{
					if (System.IO.File.Exists(sfd.FileName))
						File.Delete(sfd.FileName);

					//File.Copy(Path.Combine(Directory.GetParent(Application.ExecutablePath).FullName, "template-egode-output.xls"), sfd.FileName);
					//Excel excel = new Excel(sfd.FileName);

					//for (int i = 0; i < lvwOrders.Items.Count; i++)
					//{
					//    EgodeSimpleOrder o = ((OrderListViewItem)lvwOrders.Items[i]).Order;
					//    excel.Insert(
					//        "Sheet1", 
					//        string.Format("A{0}:H{0}", i+1), 
					//        string.Format("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}'", 
					//            o.TaobaoId, o.Product, o.Count, o.Express, o.Addr, o.Money, o.Demand, o.BillNumber));
					//}
					//excel.Close();
					
					StreamWriter w = new StreamWriter(sfd.FileName, false, Encoding.UTF8, 1024);
					w.WriteLine("ID,产品,数量,快递,地址,金额,特殊要求,快递单号");
					foreach (OrderListViewItem item in lvwOrders.Items)
					{
						EgodeSimpleOrder o = item.Order;
						w.WriteLine(string.Format(
							"'{0},'{1},'{2},'{3},'{4}','{5},'{6},'{7}", 
							o.TaobaoId, o.Product, o.Count, o.Express, o.Addr, o.Money, o.Demand, o.BillNumber));
					}
					w.Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}
		
		private void lvwOrders_DoubleClick(object sender, EventArgs e)
		{
			if (lvwOrders.SelectedItems.Count <= 0)
				return;

			Cursor.Current = Cursors.WaitCursor;

			OrderListViewItem item = lvwOrders.SelectedItems[0] as OrderListViewItem;
			OuterOrderDetailsForm oodf = new OuterOrderDetailsForm(item.Order);
			if (DialogResult.OK == oodf.ShowDialog(this))
			{
				item.Order.BillNumber = oodf.BillNumber;
				item.Update();
			}

			Cursor.Current = Cursors.Default;
		}

		private void OuterOrdersForm_Shown(object sender, EventArgs e)
		{
			StartDownload();
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

		private void OuterOrdersForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult dr = MessageBox.Show(
				this, 
				"如已完成打印、出库工作, 务必导出表格后再退出.\n否则所有单号数据都将丢失!\n\n是否确定退出?", this.Text,
				MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			
			if (DialogResult.Yes == dr)
			{
				dr = MessageBox.Show(
					this,
					"再次确认! 再次确认! 再次确认!\n\n如已完成打印、出库工作, 务必导出表格后再退出.\n否则所有单号数据都将丢失!\n\n是否确定退出?", this.Text,
					MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			}
			
			if (DialogResult.No == dr)
				e.Cancel = true;
		}
	}
}