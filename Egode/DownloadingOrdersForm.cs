using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class DownloadingOrdersForm : Form
	{
		public DownloadingOrdersForm()
		{
			InitializeComponent();
			
			btnOK.Enabled = false;
		}
		
		public void AddMessage(string s)
		{
			lblInfo.Text += "\n";
			lblInfo.Text += s;
		}

		public string Message
		{
			get { return lblInfo.Text; }
			set { lblInfo.Text = value; }
		}
		
		public bool OKEnabled
		{
			get { return btnOK.Enabled; }
			set { btnOK.Enabled = value; }
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}