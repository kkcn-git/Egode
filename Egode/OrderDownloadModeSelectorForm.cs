using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class OrderDownloadModeSelectorForm : Form
	{
		public OrderDownloadModeSelectorForm()
		{
			InitializeComponent();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
		
			if (rdoFull.Checked)
				Settings.Instance.OrderDownloadMode = Settings.OrderDownloadModes.Full;
			else
				Settings.Instance.OrderDownloadMode = Settings.OrderDownloadModes.Speed;
		
			this.DialogResult = DialogResult.OK;
			this.Close();
			
			Cursor.Current = Cursors.Default;
		}
	}
}