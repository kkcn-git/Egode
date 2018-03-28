using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class ShopSelectorForm : Form
	{
		public ShopSelectorForm()
		{
			InitializeComponent();
		}

		private void ShopSelectorForm_Load(object sender, EventArgs e)
		{
			foreach (ShopProfile sp in ShopProfile.Shops)
				cboShops.Items.Add(sp);
			cboShops.SelectedIndex = 0;
		}

		private void ShopSelectorForm_Shown(object sender, EventArgs e)
		{
			cboShops.Focus();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			ShopProfile.Switch(((ShopProfile)cboShops.SelectedItem).Shop);
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void cboShops_SelectedIndexChanged(object sender, EventArgs e)
		{
			ShopProfile sp = (ShopProfile)cboShops.SelectedItem;
			User u = User.GetUser(Settings.Operator);
			if (null == u)
			{
				rdoWw.Text = "<Failed to get operator>";
				rdoWw.ForeColor = Color.Red;
				return;
			}
			
			string ww = u.GetWW(cboShops.SelectedIndex);
			if (!string.IsNullOrEmpty(ww))
				sp.SubAccount = ww;;

			sp.Pw = u.GetWWPW(cboShops.SelectedIndex);
			rdoWw.Text = sp.Account + (string.IsNullOrEmpty(ww) ? string.Empty : ":"+sp.SubAccount);
			rdoWw.ForeColor = Color.FromArgb(0xff, 0x80, 0x80, 0x80);
		}
	}
}