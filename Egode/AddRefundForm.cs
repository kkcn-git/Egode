using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class AddRefundForm : Form
	{
		public AddRefundForm()
		{
			InitializeComponent();
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
		
		public string ShipmentNumber
		{
			get { return txtShipmentNo.Text; }
		}
		
		public string Src
		{
			get { return txtSrc.Text; }
		}
		
		public string Item
		{
			get { return txtItem.Text; }
		}
		
		public string Comment
		{
			get { return txtComment.Text; }
		}
	}
}