using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class OrdersForm : Form
	{
		public OrdersForm(List<OrderParser.Order> orders)
		{
			InitializeComponent();
			
			if (null == orders)
				return;
			
			foreach (OrderParser.Order o in orders)
				pnlOrders.Controls.Add(new OrderDetailsControl(o, -1));
		}
		
		public string Prompt
		{
			get { return lblPrompt.Text; }
			set { lblPrompt.Text = value; }
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}