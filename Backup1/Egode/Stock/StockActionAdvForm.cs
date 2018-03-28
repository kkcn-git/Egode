using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Resources;
using Egode.WebBrowserForms;
using OrderLib;

namespace Egode
{
	public partial class StockActionAdvForm : Form
	{
		private bool _stockout; // 是否出库. 否则入库.

		public StockActionAdvForm(bool stockout, List<SoldProductInfo> defaultSelectedProductInfos)
		{
			_stockout = stockout;
			InitializeComponent();
			
			if (null != defaultSelectedProductInfos)
			{
				foreach (SoldProductInfo spi in defaultSelectedProductInfos)
				{
					SoldProductInfoControl spic = new SoldProductInfoControl(spi, false);
					spic.OnRemove += new EventHandler(spic_OnRemove);
					spic.OnProductChanged += new EventHandler(spic_OnProductChanged);
					pnlProductList.Controls.Add(spic);
					pnlProductList.Controls.SetChildIndex(spic, pnlProductList.Controls.IndexOf(tsAddProduct));
					spic.Margin = new Padding(3, 2, 3, 0);
					spic.Width = pnlProductList.Width - tsAddProduct.Margin.Right - spic.Margin.Left - 7;
				}
			}
			
			this.Text = (stockout ? "出库" : "入库");
			pnlProductList.BackColor = (stockout ? Color.FromArgb(255, 255, 192, 192) : Color.LightGreen);
			lblProducts.Text = (stockout ? "出库商品" : "入库商品");
			lblFromTo.Text = (stockout ? "出库目标" : "入库来源");
		}
		
		public string FromToPart1
		{
			get { return cboFromToPart1.Text; }
			set { cboFromToPart1.Text = value; }
		}
		
		public string FromToPart2
		{
			get { return txtFromToPart2.Text; }
			set { txtFromToPart2.Text = value; }
		}
		
		public string FromToFull
		{
			get
			{
				if (!string.IsNullOrEmpty(this.FromToPart1) && !string.IsNullOrEmpty(this.FromToPart2))
					return string.Format("{0}\\{1}", this.FromToPart1, this.FromToPart2);
				else if (!string.IsNullOrEmpty(this.FromToPart1))
					return this.FromToPart1;
				else //if (!string.IsNullOrEmpty(this.FromToPart2))
					return this.FromToPart2;
			}
		}
		
		public string Comment
		{
			get { return txtComment.Text; }
			set { txtComment.Text = value; }
		}
		
		public List<SoldProductInfo> SelectedProductInfos
		{
			get
			{
				List<SoldProductInfo> selectedProductInfos = new List<SoldProductInfo>();
				foreach (Control c in pnlProductList.Controls)
				{
					if (!(c is SoldProductInfoControl))
						continue;
					SoldProductInfoControl spic = c as SoldProductInfoControl;
					//if (spic.Count == 0)
					//    continue;
					if (spic.SelectedProductInfo.Id.Equals("0"))
						continue;
					SoldProductInfo spi = new SoldProductInfo(spic.SelectedProductInfo);
					spi.Count = spic.Count;
					selectedProductInfos.Add(spi);
				}
				return selectedProductInfos;
			}
		}

		private void StockActionAdvForm_Load(object sender, EventArgs e)
		{
			//_soldProductInfos = GetProducts(_orders, out cMustSendFromDe, out cBonded);
			
			//foreach (SoldProductInfo spi in _soldProductInfos)
			//{
			//    SoldProductInfoControl spic = new SoldProductInfoControl(spi, false);
			//    spic.OnRemove += new EventHandler(spic_OnRemove);
			//    spic.OnProductChanged += new EventHandler(spic_OnProductChanged);
			//    spic.OnCountChanged += new EventHandler(spic_OnCountChanged);
			//    pnlProductList.Controls.Add(spic);
			//    pnlProductList.Controls.SetChildIndex(spic, pnlProductList.Controls.IndexOf(tsAddProduct));
			//    spic.Margin = new Padding(3, 2, 3, 0);
			//}

			cboFromToPart1.Items.Add("俞卫芳13773717403\\吴敏18118627103");
			cboFromToPart1.Items.Add("俞卫芳13773717403\\徐芳群15162796016");
			cboFromToPart1.Items.Add("俞卫芳13773717403");
			cboFromToPart1.Items.Add("海狗");
			cboFromToPart1.Items.Add("晴蔷薇");
		}

		private void StockActionAdvForm_Shown(object sender, EventArgs e)
		{
		}

		private void pnlProductList_Paint(object sender, PaintEventArgs e)
		{
			ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, pnlProductList.Width, pnlProductList.Height), Color.LightGray, ButtonBorderStyle.Solid);
		}

		void spic_OnProductChanged(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			
			SoldProductInfoControl spicSender = sender as SoldProductInfoControl;
			foreach (Control c in pnlProductList.Controls)
			{
				if (!(c is SoldProductInfoControl))
					continue;
				if (c.Equals(sender))
					continue;
				SoldProductInfoControl spic = c as SoldProductInfoControl;
				if (spic.SelectedProductInfo.Id.Equals(spicSender.SelectedProductInfo.Id))
				{
					MessageBox.Show(this, "此商品已存在于列表中, 请不要重复选择.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					spicSender.SetSelectedProduct("0");
					return;
				}
			}
						
			Cursor.Current = Cursors.Default;
		}

		void spic_OnRemove(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show(this, "确定要删除吗?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (DialogResult.No == dr)
				return;
			
			Cursor.Current = Cursors.WaitCursor;
			pnlProductList.Controls.Remove(sender as SoldProductInfoControl);
			Cursor.Current = Cursors.Default;
		}
		
		private void tsbtnAddProduct_Click(object sender, EventArgs e)
		{
			SoldProductInfoControl spic = new SoldProductInfoControl(null, false);
			spic.OnRemove += new EventHandler(spic_OnRemove);
			spic.OnProductChanged += new EventHandler(spic_OnProductChanged);
			pnlProductList.Controls.Add(spic);
			pnlProductList.Controls.SetChildIndex(spic, pnlProductList.Controls.IndexOf(tsAddProduct));
			spic.Margin = new Padding(3, 2, 3, 0);
			spic.Width = pnlProductList.Width - tsAddProduct.Margin.Right - spic.Margin.Left - 7;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			foreach (SoldProductInfo spi in this.SelectedProductInfos)
			{
				if (spi.Count <= 0)
				{
					DialogResult dr = MessageBox.Show(
						this,
						_stockout ? "至少存在1个出库产品数量为0.\n是否按数量0出库?" : "至少存在1个入库产品数量为0.\n是否按数量0入库?", 
						this.Text,
						MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
					if (DialogResult.Yes != dr)
						return;
					break;
				}
			}
			
			if (string.IsNullOrEmpty(this.FromToFull))
			{
				MessageBox.Show(
					this,
					_stockout ? "出库目标不能为空." : "入库来源不能为空.", this.Text,
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			this.DialogResult = DialogResult.OK;
			this.Close();
			Application.DoEvents();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		public static string StockAction(bool stockout, List<SoldProductInfo> stockProductInfos, string fromto, string comment, OrderLib.ShippingOrigins stockLocation)
		{
			string cmd = string.Empty;
			switch (stockLocation)
			{
				case OrderLib.ShippingOrigins.Shanghai:
					cmd = "stock";
					break;
				case OrderLib.ShippingOrigins.Ningbo:
					cmd = "stocknb";
					break;
			}

			string ids = string.Empty;
			string counts = string.Empty;
			foreach (SoldProductInfo spi in stockProductInfos)
			{
				ids += spi.Id + ",";
				counts += (stockout ? "-" : string.Empty) + spi.Count.ToString() + ",";
			}

			DateTime dt = DateTime.Now;
			string url = string.Format(Common.URL_DATA_CENTER, cmd);
			url += string.Format("&productids={0}&counts={1}&dest={2}&op={3}&comment={4}&date={5}", ids, counts, fromto, Settings.Operator, comment, dt.ToString("yyyy-MM-dd HH:mm:ss"));
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
			request.Method = "GET";
			request.ContentType = "text/xml";
			WebResponse response = request.GetResponse();
			StreamReader reader = new StreamReader(response.GetResponseStream());
			string result = reader.ReadToEnd();
			reader.Close();
			//Trace.WriteLine(result);

			return result;
		}
	}
}