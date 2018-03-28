using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using Egode.WebBrowserForms;
using System.Net;
using System.Net.Sockets;
using OrderLib;

namespace Egode
{
	public partial class NingboOrdersForm : Form
	{
		public delegate void OnSearchEventHandler(string taobaoOrderId);
		public event OnSearchEventHandler OnSearch;
		public event OnSearchEventHandler OnSearchMobile;

		public event EventHandler OnNingboOrderRemoved;

		#region NingboOrderListViewItem
		private class NingboOrderListViewItem : ListViewItem
		{
			private NingboOrder _ningboOrder;
			private int _productIndex;
			
			// productIndex: �����NingboOrder�а������product, �˲���ָ��ʹ�õڼ���product.
			// productIndex��У��, ���ں���Χ��.
			public NingboOrderListViewItem(NingboOrder o, int productIndex, Color backColor)
			{
				_ningboOrder = o;
				_productIndex = productIndex;
				
				this.BackColor = backColor;
				this.Text = o.LogisticsCompany;
				this.SubItems.Add(o.MailNumber);
				this.SubItems.Add(o.TaobaoOrderId);
				this.SubItems.Add(o.RecipientName);
				this.SubItems.Add(o.Mobile);
				this.SubItems.Add(o.Province);
				this.SubItems.Add(o.City);
				this.SubItems.Add(o.District);
				this.SubItems.Add(o.StreetAddr);
				
				if (null != o.SoldProducts && o.SoldProducts.Count > productIndex)
				{
					this.SubItems.Add(o.SoldProducts[productIndex].ShortName); // product
					this.SubItems.Add(o.SoldProducts[productIndex].Count.ToString());
				}
				else
				{
					this.SubItems.Add(string.Empty);
					this.SubItems.Add(string.Empty);
				}
				
				this.SubItems.Add(o.IdInfo);
			}
			
			public NingboOrder NingboOrder
			{
				get { return _ningboOrder; }
			}
		}
		#endregion
	
		public NingboOrdersForm()
		{
			InitializeComponent();
		}

		private void NingboOrdersForm_Load(object sender, EventArgs e)
		{
			// ����1��ningboorder�а������product, �Զ�����ʾ, �⼸������ͬ��ɫ������.
			// Ϊ�˱������ڶ������Ƕ�product, ����2�ֱ���ɫ, ����ʹ��.
			bool backColorTag=false;
		
			foreach (NingboOrder no in NingboOrder.Orders)
			{
				Color backColor = lvwNingboOrders.BackColor;
				if (no.SoldProducts.Count > 1)
				{
					backColor = backColorTag ? Color.PowderBlue : Color.AntiqueWhite;
					backColorTag = !backColorTag;
				}
				
				for (int i = 0; i < no.SoldProducts.Count; i++)
				{
					NingboOrderListViewItem item = new NingboOrderListViewItem(no, i, backColor);
					lvwNingboOrders.Items.Add(item);
				}
			}
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			
			try
			{
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Filter = "Excel Files(*.xls)|*.xls|All Files(*.*)|*.*";
				sfd.FileName = string.Format("buy{0}.xls", DateTime.Now.ToString("yyyyMMdd"));
				sfd.OverwritePrompt = true;
				if (DialogResult.OK == sfd.ShowDialog(this))
				{
					string templateFilename = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "template-higo-20151228.xls");
					File.Copy(templateFilename, sfd.FileName);
					Excel excel = new Excel(sfd.FileName, Excel.OledbVersions.OLEDB40);
					
					foreach (NingboOrder o in NingboOrder.Orders)
					{
						for (int i = 0; i < o.SoldProducts.Count; i++)
						{
							//object[] args = new object[]{ 
							//    "SAL06", "NBBLK", o.LogisticsCompany, o.TaobaoOrderId, 
							//    string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
							//    "��", 
							//    string.Empty, string.Empty,
							//    string.Empty, 
							//    o.RecipientName, o.Mobile, 
							//    string.Empty, 
							//    o.Province, o.City, o.District, o.StreetAddr, 
							//    o.SoldProducts[i].NingboId, 
							//    string.Empty, string.Empty, 
							//    o.SoldProducts[i].Count, 
							//    string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
							//    "��", 
							//    string.Empty, string.Empty,
							//    o.IdInfo, 
							//    string.Format("֧����,{0}", o.AlipayNumber),
							//    o.SoldProducts.Count > 1 ? string.Format("�ϲ�����:{0}", o.RecipientName) : string.Empty,
							//    string.Empty, string.Empty, string.Empty, string.Empty, string.Empty};
							//excel.Insert("����������������ģ��", string.Empty, args);
							
							// ����ʵ��֧�������㵥�ۡ��˷�.
							Order taobaoOrder = MainForm.Instance.GetOrder(o.TaobaoOrderId);
							System.Diagnostics.Trace.Assert(null != taobaoOrder);
							
							// v20151228.
							object[] args = new object[]{ 
							    "SAL06", "NBBLK", o.LogisticsCompany, o.TaobaoOrderId, 
							    string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
							    "��", 
							    string.Empty, // �˷�
							    string.Empty,
							    o.RecipientName, // �ͻ�
							    o.RecipientName, o.Mobile, 
							    string.Empty, // �ʱ�
							    o.Province, o.City, o.District, o.StreetAddr, 
							    o.SoldProducts[i].NingboId, 
							    string.Empty, string.Empty, 
							    o.SoldProducts[i].Count, 
							    "��", //��λ
							    "0.00", //����
							    "00.00", // �ܼ�
							    string.Empty, string.Empty, string.Empty,
							    "��", 
							    string.Empty, string.Empty,
							    o.IdInfo, 
							    string.Empty,// string.Format("֧����,{0}", o.AlipayNumber),
							    o.SoldProducts.Count > 1 ? string.Format("�ϲ�����:{0}", o.RecipientName) : string.Empty,
							    string.Empty, string.Empty, string.Empty,
							    "֧����", // ֧����ʽ
							    o.AlipayNumber, // ֧����ˮ��
							    taobaoOrder.TotalMoney.ToString("0.00")}; // ֧�����
						
							excel.Insert("��������ģ��", string.Empty, args);
						}
					}
					
					excel.Close();
					
					DialogResult dr = MessageBox.Show(this, "�������ݳɹ�.\nȷ�������������Yesͬ������״̬��������, ������No�˳�.", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
					if (DialogResult.Yes == dr)
						UpdateStatusToServer(NingboOrder.Orders);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			
			try
			{
				OpenFileDialog ofd = new OpenFileDialog();
				ofd.Filter = "Excel Files(*.xls)|*.xls|All Files(*.*)|*.*";
				if (DialogResult.OK == ofd.ShowDialog(this))
				{
					Excel excel = new Excel(ofd.FileName, true, Excel.OledbVersions.OLEDB40);
					try
					{
						List<string> tableNames = excel.GetTableNames();
						if (null == tableNames || tableNames.Count <= 0)
						{
							MessageBox.Show(this, "Excel�ļ���δ�ҵ��κα�.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}
						
						Ningbo.NingboTableColumnSelectorForm colSelectorForm = new Egode.Ningbo.NingboTableColumnSelectorForm(excel);
						if (DialogResult.OK == colSelectorForm.ShowDialog(this))
						{
							DataSet ds = excel.Get(tableNames[0], string.Empty);
							for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
							{
								DataRow row = ds.Tables[0].Rows[i];
								if (string.IsNullOrEmpty(row[colSelectorForm.ColumnInfo.LogisticsCompany].ToString()) && string.IsNullOrEmpty(row[colSelectorForm.ColumnInfo.MailNumber].ToString()))
									continue;

								int count = int.Parse(row.ItemArray[colSelectorForm.ColumnInfo.Count].ToString());

								List<SoldProductInfo> products = new List<SoldProductInfo>();
								ProductInfo pi = null;
								SoldProductInfo spi = null;
								if (colSelectorForm.ColumnInfo.ProductNingboCode >= 0)
								{
									pi = ProductInfo.GetProductByNingboCode(row.ItemArray[colSelectorForm.ColumnInfo.ProductNingboCode].ToString());
									if (null != pi)
									{
										spi = new SoldProductInfo(pi);
										spi.Count = count;
										products.Add(spi);
									}
								}

								//NingboOrder no = new NingboOrder(
								//    row.ItemArray[2].ToString().Trim(), row.ItemArray[3].ToString().Trim(),
								//    row.ItemArray[4].ToString().Trim(), row.ItemArray[15].ToString().Trim(),
								//    row.ItemArray[16].ToString().Trim(), row.ItemArray[18].ToString().Trim(), row.ItemArray[19].ToString().Trim(),
								//    row.ItemArray[20].ToString().Trim(), row.ItemArray[21].ToString().Trim(),
								//    products, row.ItemArray[35].ToString().Trim());
								NingboOrder no = new NingboOrder(
									row.ItemArray[colSelectorForm.ColumnInfo.LogisticsCompany].ToString().Trim(), row.ItemArray[colSelectorForm.ColumnInfo.MailNumber].ToString().Trim(),
									row.ItemArray[colSelectorForm.ColumnInfo.OrderId].ToString().Trim(), row.ItemArray[colSelectorForm.ColumnInfo.RecipientName].ToString().Trim(),
									row.ItemArray[colSelectorForm.ColumnInfo.Mobile].ToString().Trim(), row.ItemArray[colSelectorForm.ColumnInfo.Province].ToString().Trim(), row.ItemArray[colSelectorForm.ColumnInfo.City].ToString().Trim(),
									row.ItemArray[colSelectorForm.ColumnInfo.District].ToString().Trim(), row.ItemArray[colSelectorForm.ColumnInfo.StreetAddr].ToString().Trim(),
									products, string.Empty, string.Empty); // last arg is alipay number.
								NingboOrderListViewItem item = new NingboOrderListViewItem(no, 0, lvwNingboOrders.BackColor);
								lvwNingboOrders.Items.Add(item);
								
								if (null == spi)
								{
									if (colSelectorForm.ColumnInfo.ProductNingboCode >= 0)
										item.SubItems[9].Text = row.ItemArray[colSelectorForm.ColumnInfo.ProductNingboCode].ToString();
									if (colSelectorForm.ColumnInfo.Count >= 0)
										item.SubItems[10].Text = row.ItemArray[colSelectorForm.ColumnInfo.Count].ToString();
								}
							}
						}
						
						/* Removed by KK on 2016/07/15.
						// Get column index for shipment number return from higo.
						int logisticsCompanyColIndex = -1;
						int shipmentNumberColIndex = -1;
						int taobaoOrderIdColIndex = -1;
						int recipientNameColIndex = -1;
						int mobileColIndex = -1;
						int provinceColIndex = -1;
						int cityColIndex = -1;
						int districtColIndex = -1;
						int streetAddrColIndex = -1;
						int productNingboCodeColIndex = -1;
						int productCountColIndex = -1;
						for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
						{
							System.Diagnostics.Trace.WriteLine(string.Format("column {0}: {1}", i, ds.Tables[0].Rows[0].ItemArray[i]));
							if (ds.Tables[0].Rows[0].ItemArray[i].ToString().Contains("��ݹ�˾"))
								logisticsCompanyColIndex = i;
							if (ds.Tables[0].Rows[0].ItemArray[i].ToString().Contains("�˵���"))
								shipmentNumberColIndex = i;
							if (ds.Tables[0].Rows[0].ItemArray[i].ToString().Contains("��վ�������"))
								taobaoOrderIdColIndex = i;
							if (ds.Tables[0].Rows[0].ItemArray[i].ToString().Contains("�ջ���"))
								recipientNameColIndex = i;
							if (ds.Tables[0].Rows[0].ItemArray[i].ToString().Contains("�ֻ�"))
								mobileColIndex = i;
							if (ds.Tables[0].Rows[0].ItemArray[i].ToString().Contains("ʡ��"))
								provinceColIndex = i;
							if (ds.Tables[0].Rows[0].ItemArray[i].ToString().Contains("��"))
								cityColIndex = i;
							if (ds.Tables[0].Rows[0].ItemArray[i].ToString().Contains("��"))
								districtColIndex = i;
							if (ds.Tables[0].Rows[0].ItemArray[i].ToString().Contains("��ϸ��Ϣ"))
								streetAddrColIndex = i;
							if (ds.Tables[0].Rows[0].ItemArray[i].ToString().Contains("��Ʒ����"))
								productNingboCodeColIndex = i;
							if (ds.Tables[0].Rows[0].ItemArray[i].ToString().Trim().Equals("����"))
								productCountColIndex = i;
						}
						
						int idInfoColIndex = -1;
						if (ds.Tables[0].Rows.Count >= 2)
						{
							Regex r = new Regex(@"\d{17}[\d|x|X]");
							for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
							{
								if (r.Match(ds.Tables[0].Rows[1].ItemArray[i].ToString()).Success)
								{
									idInfoColIndex = i;
									break;
								}
							}
						}

						if (-1 == shipmentNumberColIndex)
						{
							//MessageBox.Show(this, "û���ҵ�\"�˵���\"��, �����ļ����ݻ���ϵKK~", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							MessageBox.Show(this, "û���ҵ�\"�˵���\"��, Ĭ��ʹ�õ�4��(D��)��Ϊ�˵���, ������ȷ����������!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							shipmentNumberColIndex = 3;
							//return;
						}
						if (-1 == taobaoOrderIdColIndex)
						{
							MessageBox.Show(this, "û���ҵ�\"��վ�������\"��, Ĭ��ʹ�õ�5��(E��)��Ϊ�������, ������ȷ����������!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							taobaoOrderIdColIndex = 4;
							//return;
						}
						
						for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
						{
							DataRow row = ds.Tables[0].Rows[i];

							ProductInfo pi = ProductInfo.GetProductByNingboCode(row.ItemArray[productNingboCodeColIndex].ToString());
							int count = int.Parse(row.ItemArray[productCountColIndex].ToString());
							SoldProductInfo spi = new SoldProductInfo(pi);
							spi.Count = count;
							List<SoldProductInfo> products = new List<SoldProductInfo>();
							products.Add(spi);

							//NingboOrder no = new NingboOrder(
							//    row.ItemArray[2].ToString().Trim(), row.ItemArray[3].ToString().Trim(),
							//    row.ItemArray[4].ToString().Trim(), row.ItemArray[15].ToString().Trim(),
							//    row.ItemArray[16].ToString().Trim(), row.ItemArray[18].ToString().Trim(), row.ItemArray[19].ToString().Trim(),
							//    row.ItemArray[20].ToString().Trim(), row.ItemArray[21].ToString().Trim(),
							//    products, row.ItemArray[35].ToString().Trim());
							NingboOrder no = new NingboOrder(
								row.ItemArray[logisticsCompanyColIndex].ToString().Trim(), row.ItemArray[shipmentNumberColIndex].ToString().Trim(),
								row.ItemArray[taobaoOrderIdColIndex].ToString().Trim(), row.ItemArray[recipientNameColIndex].ToString().Trim(),
								row.ItemArray[mobileColIndex].ToString().Trim(), row.ItemArray[provinceColIndex].ToString().Trim(), row.ItemArray[19].ToString().Trim(),
								row.ItemArray[cityColIndex].ToString().Trim(), row.ItemArray[districtColIndex].ToString().Trim(),
								products, row.ItemArray[idInfoColIndex].ToString().Trim(), string.Empty); // last arg is alipay number.
							NingboOrderListViewItem item = new NingboOrderListViewItem(no, 0, lvwNingboOrders.BackColor);
							lvwNingboOrders.Items.Add(item);
						}
						*/
					}
					catch (Exception ex)
					{
						MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK);
					}
					finally
					{
						excel.Close();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void lvwNingboOrders_DoubleClick(object sender, EventArgs e)
		{
			btnSearchTaobaoOrder_Click(btnSearchTaobaoOrder, EventArgs.Empty);
		}

		private void btnSearchTaobaoOrder_Click(object sender, EventArgs e)
		{
			if (lvwNingboOrders.SelectedItems.Count <= 0)
				return;
			System.Diagnostics.Trace.Assert(null != this.OnSearch);

			Cursor = Cursors.WaitCursor;
			
			// Check if all selected row have same taobao order Id.
			for (int i = 0; i < lvwNingboOrders.SelectedItems.Count; i++)
			{
				for (int j = 0; j < i; j++)
				{
					NingboOrderListViewItem itemi = lvwNingboOrders.SelectedItems[i] as NingboOrderListViewItem;
					NingboOrderListViewItem itemj = lvwNingboOrders.SelectedItems[j] as NingboOrderListViewItem;
					if (!itemi.NingboOrder.TaobaoOrderId.Equals(itemj.NingboOrder.TaobaoOrderId))
					{
						MessageBox.Show(this, "ѡ���˲�ͬ���Ա�����, �޷��������������.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
				}
			}
			
			NingboOrderListViewItem selectedItem = ((NingboOrderListViewItem)lvwNingboOrders.SelectedItems[0]);
			if (chkSearchByMobile.Checked)
				this.OnSearchMobile(selectedItem.NingboOrder.Mobile);
			else
				this.OnSearch(selectedItem.NingboOrder.TaobaoOrderId);
			Cursor = Cursors.Default;
		}

		private void btnConsign_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			
			// Search order first.
			btnSearchTaobaoOrder_Click(btnSearchTaobaoOrder, EventArgs.Empty);
			
			//// Check if there are other orders with same order id.
			//foreach (NingboOrderListViewItem item in lvwNingboOrders.Items)
			//{
			//    if (item.Selected)
			//        continue;
			//    if (item.NingboOrder.TaobaoOrderId.Equals(((NingboOrderListViewItem)lvwNingboOrders.SelectedItems[0]).NingboOrder.TaobaoOrderId))
			//    {
					
			//    }
			//}

			int cUnavailableOrders = 0;

			foreach (NingboOrderListViewItem item in lvwNingboOrders.SelectedItems)
			{
				Order order = MainForm.Instance.GetOrder(item.NingboOrder.TaobaoOrderId);
				if (null == order)
				{
					cUnavailableOrders++;
					continue;
				}
				if (order.Status == Order.OrderStatus.Sent)
				{
					DialogResult dr1 = MessageBox.Show(
						this,
						"�˶����Ѿ��Ƿ���״̬, ����������.\n�Ƿ���Ҫ��ע����?", this.Text,
						MessageBoxButtons.YesNo, MessageBoxIcon.Information);
					if (DialogResult.Yes == dr1)
						btnRemarkBillNumber_Click(btnRemarkBillNumber, EventArgs.Empty);
				}
				if (order.Status != Order.OrderStatus.Paid || order.Status == Order.OrderStatus.PartialSent)
					continue;
			
				if (item.NingboOrder.LogisticsCompany.Equals("��������С��"))
					ConsignShWebBrowserForm.Instance.ShipmentCompany = OrderLib.ShipmentCompanies.Post;
				if (item.NingboOrder.LogisticsCompany.Equals("EMS"))
					ConsignShWebBrowserForm.Instance.ShipmentCompany = OrderLib.ShipmentCompanies.EMS;
				if (item.NingboOrder.LogisticsCompany.Equals("��ͨ�ٵ�"))
					ConsignShWebBrowserForm.Instance.ShipmentCompany = OrderLib.ShipmentCompanies.Zto;

				ConsignShWebBrowserForm.Instance.OrderId = item.NingboOrder.TaobaoOrderId;
				ConsignShWebBrowserForm.Instance.BillNumber = item.NingboOrder.MailNumber;
				ConsignShWebBrowserForm.Instance.AutoSubmit = true;
				DialogResult dr = ConsignShWebBrowserForm.Instance.ShowDialog(this);
				if (DialogResult.OK == dr)
				{
					order.Consign();
					MessageBox.Show(this, "��������ɹ�", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
					//// ����Ǽ�.
					//try
					//{
					//    string url = string.Format(Common.URL_DATA_CENTER, "stocknb");
					//    url += string.Format(
					//        "&productids={0}&counts={1}&dest={2}&op={3}&comment={4}{5}",
					//        item.NingboOrder.SoldProducts[0].Id, item.NingboOrder.SoldProducts[0].Count,
					//        (ShopProfile.Current.Shop == ShopEnum.Egode ? string.Empty : (ShopProfile.Current.ShortName + "\\")) + string.Format("{0} [{1}]", order.BuyerAccount, order.RecipientName),
					//        Settings.Operator,
					//        GetLogisticsShortName(item.NingboOrder.LogisticsCompany), item.NingboOrder.MailNumber);
					//    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
					//    request.Method = "GET";
					//    request.ContentType = "text/xml";
					//    WebResponse response = request.GetResponse();
					//    StreamReader reader = new StreamReader(response.GetResponseStream());
					//    string result = reader.ReadToEnd();
					//    reader.Close();
					//    //Trace.WriteLine(result);

					//    MessageBox.Show(
					//        this,
					//        "Result from server: \n" + result,
					//        this.Text,
					//        MessageBoxButtons.OK, MessageBoxIcon.Information);
					//}
					//catch (Exception ex)
					//{
					//    MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					//}
				}
				else if (DialogResult.Yes == dr)
				{
					order.Consign();
					MessageBox.Show(this, "�˶����Ѿ��Ƿ���״̬, ����������.\n�Ƿ���Ҫ��ע����?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
				}
			}

			//if (DialogResult.Retry == dr) // retry.
			//    dr = ConsignShWebBrowserForm.Instance.ShowDialog(this);

			//if (DialogResult.OK != dr)
			//    break;
			//o.Consign();
			
			Cursor.Current = Cursors.Default;
		}
		
		private void btnRemarkBillNumber_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
		
			if (lvwNingboOrders.SelectedItems.Count <= 0)
				return;
			
			// Search order first.
			btnSearchTaobaoOrder_Click(btnSearchTaobaoOrder, EventArgs.Empty);

			NingboOrder ningboOrder = ((NingboOrderListViewItem)lvwNingboOrders.SelectedItems[0]).NingboOrder;
			Clipboard.SetText(ningboOrder.MailNumber);
			Order taobaoOrder = MainForm.Instance.GetOrder(ningboOrder.TaobaoOrderId);
			if (null == taobaoOrder)
				return;
			
			UpdateSellMemoForm usmf = new UpdateSellMemoForm(taobaoOrder.Remark, string.Format("stockout: [{0}]{1}", GetLogisticsShortName(ningboOrder.LogisticsCompany), ningboOrder.MailNumber));
			DialogResult dr = usmf.ShowDialog(this.FindForm());
			if (DialogResult.Cancel == dr)
				return;
		
			WebBrowserForms.UpdateSellMemoWebBrowserForm usmbf = new UpdateSellMemoWebBrowserForm(taobaoOrder, usmf.AppendMemo, true);
			usmbf.ShowDialog(this.FindForm());
			
			Cursor.Current = Cursors.Default;
		}

		private string GetLogisticsShortName(string logisticsCompany)
		{
			switch (logisticsCompany)
			{
				case "��������С��":
					return "post";
				case "EMS":
					return "ems";
				case "��ͨ�ٵ�":
					return "zto";
			}
			return string.Empty;
		}

		private void lvwNingboOrders_SelectedIndexChanged(object sender, EventArgs e)
		{
			btnSearchTaobaoOrder.Enabled = lvwNingboOrders.SelectedItems.Count > 0;
			btnConsign.Enabled = lvwNingboOrders.SelectedItems.Count > 0;
			btnRemarkBillNumber.Enabled = lvwNingboOrders.SelectedItems.Count > 0;
			btnStockout.Enabled = lvwNingboOrders.SelectedItems.Count > 0;
			btnRemove.Enabled = lvwNingboOrders.SelectedItems.Count > 0;
			
			foreach (ListViewItem item in lvwNingboOrders.Items)
				item.ImageIndex = -1;
			if (lvwNingboOrders.SelectedItems.Count	> 0)
			{
				foreach (ListViewItem item in lvwNingboOrders.SelectedItems)
					item.ImageIndex = 0;
			}
		}

		private void btnStockout_Click(object sender, EventArgs e)
		{
			int cSucceeded = 0, cFailed = 0;
		
			foreach (NingboOrderListViewItem item in lvwNingboOrders.SelectedItems)
			{
				Order order = MainForm.Instance.GetOrder(item.NingboOrder.TaobaoOrderId);
				try
				{
					// �л����ֶ�����.
					if (null == item.NingboOrder.SoldProducts || item.NingboOrder.SoldProducts.Count <= 0)
					{
						StockActionAdvForm saaf = new StockActionAdvForm(true, null);
						saaf.FromToPart1 = "eur8";//(ShopProfile.Current.Shop == ShopEnum.Egode ? string.Empty : ShopProfile.Current.ShortName);
						saaf.FromToPart2 = string.Format("{0} [{1},{2}]", (null == order ? string.Empty: order.BuyerAccount), item.NingboOrder.RecipientName, item.NingboOrder.Mobile);
						saaf.Comment = item.NingboOrder.LogisticsCompany.Trim() + item.NingboOrder.MailNumber.Trim();
						if (DialogResult.OK == saaf.ShowDialog(this))
						{
							// ����Ǽ�.
							try
							{
								Application.DoEvents();
								Cursor.Current = Cursors.WaitCursor;
								
								string result = StockActionAdvForm.StockAction(true, saaf.SelectedProductInfos, saaf.FromToFull, saaf.Comment, ShippingOrigins.Ningbo);
								if (result.StartsWith("Succeeded"))
									cSucceeded++;
								else
									cFailed++;
								MessageBox.Show(
									this,
									"Result from server: \n" + result,
									this.Text,
									MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
							catch (Exception ex)
							{
								MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							}
							finally
							{
								Cursor.Current = Cursors.Default;
							}
						}
						continue;
					}

					string url = string.Format(Common.URL_DATA_CENTER, "stocknb");
					url += string.Format(
						"&productids={0}&counts=-{1}&dest={2}&op={3}&comment={4}{5}",
						item.NingboOrder.SoldProducts[0].Id, item.NingboOrder.SoldProducts[0].Count,
						(ShopProfile.Current.Shop == ShopEnum.Egode ? string.Empty : (ShopProfile.Current.ShortName + "\\")) + string.Format("{0} [{1},{2}]", order.BuyerAccount, item.NingboOrder.RecipientName, item.NingboOrder.Mobile),
						Settings.Operator,
						GetLogisticsShortName(item.NingboOrder.LogisticsCompany), item.NingboOrder.MailNumber);
					HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
					request.Method = "GET";
					request.ContentType = "text/xml";
					WebResponse response = request.GetResponse();
					StreamReader reader = new StreamReader(response.GetResponseStream());
					string result1 = reader.ReadToEnd();
					reader.Close();
					//Trace.WriteLine(result1);

					if (result1.StartsWith("Succeeded"))
						cSucceeded++;
					else
						cFailed++;

					MessageBox.Show(
						this,
						"Result from server: \n" + result1,
						string.Format("{0} [{1}\\{2}]", this.Text, cSucceeded+cFailed, lvwNingboOrders.SelectedItems.Count),
						MessageBoxButtons.OK, result1.StartsWith("Succeeded") ? MessageBoxIcon.Information : MessageBoxIcon.Exclamation);
					
					// Added by KK on 2015/12/16.
					if (result1.StartsWith("Succeeded"))
						item.ForeColor = Color.DarkGreen;
					else
						item.ForeColor = Color.Red;
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			
			MessageBox.Show(
				this,
				string.Format("�����������.\n�ɹ�: {0}\nʧ��: {1}", cSucceeded, cFailed),
				this.Text,
				MessageBoxButtons.OK, cFailed == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Exclamation);
		}

		private void UpdateStatusToServer(List<NingboOrder> ningboOrders)
		{
			StringBuilder sbOrderIds = new StringBuilder();

			foreach (NingboOrder o in ningboOrders)
			{
				//if (o.LocalPrepared)
					sbOrderIds.Append(o.TaobaoOrderId + ",");
					
					if (!string.IsNullOrEmpty(o.LinkedTaobaoOrderIds))
					{
						string s = o.LinkedTaobaoOrderIds;
						if (s.StartsWith(","))
							s = s.Remove(0, 1);
						if (s.EndsWith(","))
							s = s.Remove(s.Length - 1, 1);
						sbOrderIds.Append(o.LinkedTaobaoOrderIds);
						sbOrderIds.Append(",");
					}
			}

			if (sbOrderIds.ToString().EndsWith(","))
				sbOrderIds.Remove(sbOrderIds.Length - 1, 1);

			try
			{
				string url = string.Format(Common.URL_DATA_CENTER, "preordersnb");
				url += string.Format("&shop={0}&orderids={1}&op={2}", ShopProfile.Current.Account, sbOrderIds.ToString(), Settings.Operator);
				HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
				request.Method = "GET";
				request.ContentType = "text/xml";
				WebResponse response = request.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream());
				string result = reader.ReadToEnd();
				reader.Close();
				//Trace.WriteLine(result);

				if (result.StartsWith("Succeeded"))
				{
					foreach (NingboOrder o in ningboOrders)
					{
						Order taobaoOrder = MainForm.Instance.GetOrder(o.TaobaoOrderId);
						if (null == taobaoOrder)
							continue;
						if (taobaoOrder.LocalPreparedNingbo)
							taobaoOrder.PrepareNingbo();
					}
				}

				MessageBox.Show(
					this,
					"Result from server: \n" + result,
					this.Text,
					MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
		
			try
			{
				DialogResult dr = MessageBox.Show(this, "ȷ��Ҫɾ��ѡ�еĳ�����¼?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (DialogResult.No == dr)
					return;
				
				foreach (NingboOrderListViewItem item in lvwNingboOrders.SelectedItems)
				{
					lvwNingboOrders.Items.Remove(item);
					NingboOrder.Orders.Remove(item.NingboOrder);					
				}
				
				if (null != this.OnNingboOrderRemoved)
					this.OnNingboOrderRemoved(this, EventArgs.Empty);
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
	}
}