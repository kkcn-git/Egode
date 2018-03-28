using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode.Utility
{
	public partial class InputBoxForm : Form
	{
		public InputBoxForm()
		{
			InitializeComponent();
		}
		
		public string Message
		{
			get { return txtMessage.Text; }
			set { txtMessage.Text = value; }
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