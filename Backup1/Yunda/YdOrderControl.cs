using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Yunda
{
	public partial class YdOrderControl : UserControl
	{
		private Yunda.YdOrder _ydOrder;
	
		public YdOrderControl(Yunda.YdOrder ydOrder)
		{
			_ydOrder = ydOrder;
		
			InitializeComponent();
			
			if (null == _ydOrder)
				return;
			txtOrderIdAndProducts.Text = "¶©µ¥ºÅ:" + _ydOrder.OrderId + Environment.NewLine + _ydOrder.ProductsInfo;
			txtRecipientName.Text = _ydOrder.RecipientName;
			txtRecipientPhone.Text = _ydOrder.RecipientPhone;
			txtRecipientMobile.Text = _ydOrder.RecipientMobile;
			txtRecipientFullAddr.Text = _ydOrder.RecipientFullAddress;
			
 			txtOrderIdAndProducts.Height = (int)Graphics.FromHwnd(this.Handle).MeasureString(txtOrderIdAndProducts.Text, txtOrderIdAndProducts.Font, txtOrderIdAndProducts.Width).Height+4;
			if (txtOrderIdAndProducts.Height < txtRecipientName.Height)
				txtOrderIdAndProducts.Height = txtRecipientName.Height;
			
			this.Height = txtOrderIdAndProducts.Bottom + txtOrderIdAndProducts.Margin.Bottom;
			txtRecipientName.Height = this.Height - txtRecipientName.Margin.Top - txtRecipientName.Margin.Bottom;
			txtRecipientPhone.Height = this.Height - txtRecipientPhone.Margin.Top - txtRecipientPhone.Margin.Bottom;
			txtRecipientMobile.Height = this.Height - txtRecipientMobile.Margin.Top - txtRecipientMobile.Margin.Bottom;
			txtRecipientFullAddr.Height = this.Height - txtRecipientFullAddr.Margin.Top - txtRecipientFullAddr.Margin.Bottom;
		}

		private void YdOrderControl_SizeChanged(object sender, EventArgs e)
		{
			txtRecipientFullAddr.Width = this.Width - txtRecipientFullAddr.Margin.Right - txtRecipientFullAddr.Left;
		}
	}
}
