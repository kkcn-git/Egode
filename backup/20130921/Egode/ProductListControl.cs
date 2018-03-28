using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class ProductListControl : UserControl
	{
		public ProductListControl()
		{
			InitializeComponent();
		}
		
		public void AddProduct(string title, float price, int amount, OrderParser.Order.OrderStatus orderStatus)
		{
			Label lblTitle = new Label();
			lblTitle.Text = title;
			lblTitle.AutoSize = true;
			lblTitle.Margin = new Padding(0, 0, 0, 0);
			lblTitle.Padding = new Padding(0, 0, 0, 0);
			lblTitle.ForeColor = Color.RoyalBlue;
			lblTitle.Font = this.Font;
			
			Label lblStatus = new Label();
			lblStatus.AutoSize = true;
			lblStatus.Margin = new Padding(0, 0, 0, 0);
			lblStatus.Padding = new Padding(0, 0, 0, 0);
			lblStatus.Font = this.Font;
			
			Panel pnlTitle = new Panel();
			pnlTitle.AutoSize = true;
			pnlTitle.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			pnlTitle.Margin = new Padding(0, 0, 16, 0);
			pnlTitle.BackColor = Color.Transparent;
			pnlTitle.Controls.AddRange(new Control[]{lblTitle, lblStatus});
			//lblStatus.BringToFront();
			lblTitle.Location = new Point(0, 0);
			lblStatus.Location = new Point(lblTitle.Right - 6, 0);
			
			Label lblPrice = new Label();
			lblPrice.Text = price.ToString("0.00");
			lblPrice.AutoSize = true;
			lblPrice.Margin = new Padding(0, 0, 16, 0);
			lblPrice.ForeColor = Color.OrangeRed;
			lblPrice.Font = this.Font;
			
			Label lblAmount = new Label();
			lblAmount.Text = amount.ToString();
			lblAmount.AutoSize = true;
			lblAmount.Margin = new Padding(0, 0, 6, 0);
			lblAmount.ForeColor = Color.FromArgb(0x60, 0x60, 0x60);
			lblAmount.Font = new Font(this.Font, FontStyle.Bold);
			
			tblMain.Controls.AddRange(new Control[]{pnlTitle, lblPrice, lblAmount});
			
			switch (orderStatus)
			{
				case OrderParser.Order.OrderStatus.Deal:
					lblStatus.Text += " (未付款)";
					lblStatus.ForeColor = Color.Gray;
					break;

				case OrderParser.Order.OrderStatus.Paid:
					lblStatus.Text += " (已付款)";
					lblStatus.ForeColor = Color.OrangeRed;
					break;

				case OrderParser.Order.OrderStatus.Sent:
					lblStatus.Text += " (已发货)";
					lblStatus.ForeColor = Color.Purple;
					break;

				case OrderParser.Order.OrderStatus.Succeeded:
					lblStatus.Text += " (交易成功)";
					lblStatus.ForeColor = Color.DarkGreen;
					break;

				case OrderParser.Order.OrderStatus.Closed:
					lblTitle.ForeColor = Color.Gray;
					lblStatus.ForeColor = Color.Gray;
					lblPrice.ForeColor = Color.Gray;
					lblAmount.ForeColor = Color.Gray;
					lblStatus.Text += " (已取消)";
					break;
			}
		}
	}
}