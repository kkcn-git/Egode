using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class AddressSelectorForm : Form
	{
		#region class AddressListViewItem
		private class AddressListViewItem : ListViewItem
		{
			private Address _addr;
		
			public AddressListViewItem(Address addr)
			{
				if (null == addr)
					return;
				
				_addr = addr;
				
				this.Text = _addr.Type;
				this.SubItems.Add(_addr.Id);
				this.SubItems.Add(_addr.Recipient);
				this.SubItems.Add(_addr.Mobile);
				this.SubItems.Add(_addr.Phone);
				this.SubItems.Add(_addr.Province);
				this.SubItems.Add(_addr.City1);
				this.SubItems.Add(_addr.City2);
				this.SubItems.Add(_addr.District);
				this.SubItems.Add(_addr.StreetAddress);
				this.SubItems.Add(_addr.PostCode);
				this.SubItems.Add(_addr.Comment);
				this.SubItems.Add(_addr.Datetime.ToString("yyyy/MM/dd HH:mm:ss"));
			}
			
			public Address Address
			{
				get { return _addr; }
			}
		}
		#endregion
		
		private Address _selectedAddress;
		
		public AddressSelectorForm()
		{
			InitializeComponent();
		}
		
		public Address SelectedAddress
		{
			get { return _selectedAddress; }
		}

		private void AddressSelectorForm_Load(object sender, EventArgs e)
		{
			foreach (Address a in Addresses.Instance)
			{
				AddressListViewItem item = new AddressListViewItem(a);
				lvwAddresses.Items.Add(item);
			}
		}

		private void AddressSelectorForm_Shown(object sender, EventArgs e)
		{
			txtSearch.Focus();
		}

		private void lvwAddresses_SelectedIndexChanged(object sender, EventArgs e)
		{
			btnOK.Enabled = (lvwAddresses.SelectedItems.Count > 0);
		}

		private void lvwAddresses_DoubleClick(object sender, EventArgs e)
		{
			if (lvwAddresses.SelectedItems.Count > 0)
				btnOK_Click(lvwAddresses, EventArgs.Empty);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			_selectedAddress = ((AddressListViewItem)lvwAddresses.SelectedItems[0]).Address;

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			if (txtSearch.Text.Trim().Length <= 0)
				return;
		
			Cursor.Current = Cursors.WaitCursor;
			
			bool isAndSearch = txtSearch.Text.ToLower().Contains(" and ");
			string[] keywords = null;
			if (isAndSearch)
				keywords = txtSearch.Text.ToLower().Split(new string[]{" and "}, StringSplitOptions.RemoveEmptyEntries);
			else
				keywords = txtSearch.Text.ToLower().Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);

			lvwAddresses.Items.Clear();

			foreach (Address a in Addresses.Instance)
			{
				if (isAndSearch)
				{
					bool matchAll = true;
					foreach (string k in keywords)
					{
						if (!a.MatchKeyword(k))
						{
							matchAll = false;
							break;
						}
					}

					if (matchAll)
					{
						AddressListViewItem item = new AddressListViewItem(a);
						lvwAddresses.Items.Add(item);
					}
				}
				else
				{
					foreach (string k in keywords)
					{
						if (a.MatchKeyword(k))
						{
							AddressListViewItem item = new AddressListViewItem(a);
							lvwAddresses.Items.Add(item);
							break;
						}
					}
				}
			}
			
			Cursor.Current = Cursors.Default;
		}

		private void txtSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
				btnSearch_Click(txtSearch, EventArgs.Empty);
		}
	}
}