using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class PrinterSelectorForm : Form
	{
		public PrinterSelectorForm()
		{
			InitializeComponent();
		}

		private void PrinterSelectorForm_Load(object sender, EventArgs e)
		{
			foreach (string s in PrinterSettings.InstalledPrinters)
			{
				cboYtoPrinter.Items.Add(s);
				cboSfPrinter.Items.Add(s);
				cboSfNewPrinter.Items.Add(s);
			}
			
			cboYtoPrinter.SelectedItem = Settings.Instance.YtoPrinter;
			cboSfPrinter.SelectedItem = Settings.Instance.SfPrinter;
			cboSfNewPrinter.SelectedItem = Settings.Instance.SfNewPrinter;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Settings.Instance.YtoPrinter = cboYtoPrinter.SelectedItem.ToString();
			Settings.Instance.SfPrinter = cboSfPrinter.SelectedItem.ToString();
			Settings.Instance.SfNewPrinter = cboSfNewPrinter.SelectedItem.ToString();
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}