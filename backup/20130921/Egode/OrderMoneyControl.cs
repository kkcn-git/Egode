using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class OrderMoneyControl : UserControl
	{
		public OrderMoneyControl()
		{
			InitializeComponent();
		}
		
		public float Money
		{
			get 
			{
				if (null == lblMoney || null == lblMoney.Tag)
					return 0;
				return (float)lblMoney.Tag; 
			}
			set
			{
				lblMoney.Text = value.ToString("0.00");
				lblMoney.Tag = value;
			}
		}
		
		public float Freight
		{
			get 
			{
				if (null == lblFreight || null == lblFreight.Tag)
					return 0;
				return (float)lblFreight.Tag; 
			}
			set
			{
				lblFreight.Text = string.Format("({0:0.00})", value);
				lblFreight.Tag = value;
			}
		}

		private void lblTitle_SizeChanged(object sender, EventArgs e)
		{
			int height = lblTitle.Height > lblMoney.Height ? lblTitle.Height : lblMoney.Height;
			height = height > lblFreight.Height ? height : lblFreight.Height;
			
			lblTitle.Location = new Point(0, height - lblTitle.Height);
			lblMoney.Location = new Point(lblTitle.Right - 3, height - lblMoney.Height + 1);
			lblFreight.Location = new Point(lblMoney.Right - 5, height - lblFreight.Height);
			lblMoney.BringToFront();
			lblFreight.BringToFront();
			this.Height = height;
		}
	}
}
