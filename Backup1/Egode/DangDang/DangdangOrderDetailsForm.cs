using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Egode;

namespace Dangdang
{
	public partial class DangdangOrderDetailsForm : Form
	{
		private DangdangOrder _dangdangOrder;
	
		public DangdangOrderDetailsForm(DangdangOrder order)
		{
			_dangdangOrder = order;
			InitializeComponent();
		}

		private void pnlDetails_Paint(object sender, PaintEventArgs e)
		{
			//e.Graphics.DrawLine(
			//    new Pen(Color.FromArgb(0xff, 0xA0, 0xa0, 0xa0), 1), 
			//    lblDealTime.Left, lblDealTime.Bottom + 12,
			//    txtPayTime.Right, lblDealTime.Bottom + 12);
		}

		private void DangdangOrderDetailsForm_Load(object sender, EventArgs e)
		{
			txtOrderId.Text = _dangdangOrder.OrderId;
			txtDealTime.Text = _dangdangOrder.DealTime.ToString("yyyy/MM/dd HH:mm:ss");
			if (DateTime.MinValue != _dangdangOrder.PayTime)
				txtPayTime.Text = _dangdangOrder.PayTime.ToString("yyyy/MM/dd HH:mm:ss");
			txtPaymentId.Text = _dangdangOrder.PaymentId;
			lblOrderStatus.Text = _dangdangOrder.Status;
			switch (_dangdangOrder.Status)
			{
				case "等待配货":
					lblOrderStatus.ForeColor = Color.Blue;
					break;
				case "等待发货":
					lblOrderStatus.ForeColor = Color.OrangeRed;
					break;
				case "已送达":
					lblOrderStatus.ForeColor = Color.Green;
					break;
				case "等待到款":
				case "取消":
					lblOrderStatus.ForeColor = Color.LightGray;
					break;
			}
			
			txtRecipientName.Text = _dangdangOrder.RecipientName;
			txtPhone.Text = _dangdangOrder.Mobile;
			txtIdNumber.Text = _dangdangOrder.IdNumber;
			txtAddress.Text = _dangdangOrder.Address;
			
			txtProductName.Text = ProductInfo.GetProductByDangdangCode(_dangdangOrder.UniqueProductCode).ShortName;
			txtCount.Text = _dangdangOrder.ActualCount.ToString();
			txtTotalMoney.Text = _dangdangOrder.TotalMoney.ToString("¥0.00");
			txtFreightFee.Text = _dangdangOrder.Fee.ToString("¥0.00");
			txtTax.Text = _dangdangOrder.Tax.ToString("¥0.00");
			if (_dangdangOrder.Fee == 0)
				txtFreightFee.ForeColor = Color.LightGray;
			if (_dangdangOrder.Tax == 0)
				txtTax.ForeColor = Color.LightGray;
			
			txtShipmentCompany.Text = _dangdangOrder.ShipmentCompany;
			txtShipmentNumber.Text = _dangdangOrder.ShipmentNumber;
			if (DateTime.MinValue != _dangdangOrder.ConsigningTime)
				txtConsigningTime.Text = _dangdangOrder.ConsigningTime.ToString("yyyy/MM/dd HH:mm:ss");
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnStockoutNb_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			Stockout(OrderLib.ShippingOrigins.Ningbo);
			Cursor.Current = Cursors.Default;
		}

		private void btnStockSh_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			Stockout(OrderLib.ShippingOrigins.Shanghai);
			Cursor.Current = Cursors.Default;
		}
		
		private void Stockout(OrderLib.ShippingOrigins shippingOrigin)
		{
			if (string.IsNullOrEmpty(txtShipmentNumber.Text))
			{
				MessageBox.Show(this, this.Text, "请输入运单号.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			
			ProductInfo pi = ProductInfo.GetProductByDangdangCode(_dangdangOrder.UniqueProductCode);
			SoldProductInfo spi = new SoldProductInfo(pi);
			spi.Count = _dangdangOrder.ActualCount;
			List<SoldProductInfo> soldProductInfos = new List<SoldProductInfo>();
			soldProductInfos.Add(spi);
			
			string result = StockActionAdvForm.StockAction(
				true, soldProductInfos, 
				string.Format("dd\\{0},{1}", txtRecipientName.Text, txtPhone.Text), 
				string.Format("{0}{1}", txtShipmentCompany.Text.ToLower(), txtShipmentNumber.Text), shippingOrigin);
			MessageBox.Show(
				this,
				"Result from server: \n" + result,
				this.Text,
				MessageBoxButtons.OK, MessageBoxIcon.Information);

		}
	}
}