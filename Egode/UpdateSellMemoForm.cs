using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class UpdateSellMemoForm : Form
	{
		public UpdateSellMemoForm(string originalMemo, string appendMemo)
		{
			InitializeComponent();
			txtOriginalMemo.Text = originalMemo;
			txtAppendMemo.Text = appendMemo;
		}
		
		public string AppendMemo
		{
			get { return txtAppendMemo.Text; }
		}

		private void UpdateSellMemoForm_Shown(object sender, EventArgs e)
		{
			txtAppendMemo.Focus();
		}

		private void txtOriginalMemo_TextChanged(object sender, EventArgs e)
		{
			OnMemoChanged();
		}

		private void txtAppendMemo_TextChanged(object sender, EventArgs e)
		{
			OnMemoChanged();
			
			btnOK.Enabled = (txtAppendMemo.Text.Length > 0);
		}
		
		private void OnMemoChanged()
		{
			string s = string.Empty; 
			if (!string.IsNullOrEmpty(txtOriginalMemo.Text))
				s = txtOriginalMemo.Text + "\r\n";
			if (!string.IsNullOrEmpty(txtAppendMemo.Text))
				s += string.Format("[{0}@{1}]:{2}", 
					User.GetDisplayName(Settings.Operator), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
					txtAppendMemo.Text);
			txtPreview.Text = s;
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