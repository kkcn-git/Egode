using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OrderLib;

namespace Egode
{
	public partial class OrdersForm : Form
	{
		private List<Order> _selectedOrders;

		public OrdersForm(List<Order> orders, Order defaultOrder)
		{
			InitializeComponent();
			
			if (null == orders)
				return;
			
			foreach (Order o in orders)
			{
				OrderDetailsControl odc = new OrderDetailsControl(o, orders.Count);
				odc.Selectable = true;
				odc.Selected = (o.OrderId.Equals(defaultOrder.OrderId));
				pnlOrders.Controls.Add(odc);
				odc.Width = pnlOrders.Width - 26;
				if (!string.IsNullOrEmpty(o.EditedRecipientAddress))
					odc.RefreshFullEditedAddress();
			}
		}
		
		public List<Order> SelectedOrders
		{
			get { return _selectedOrders; }
		}
		
		public string Prompt
		{
			get { return lblPrompt.Text; }
			set { lblPrompt.Text = value; }
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			_selectedOrders = new List<Order>();

			foreach (OrderDetailsControl odc in pnlOrders.Controls)
			{
				if (odc.Selected)
					_selectedOrders.Add(odc.Order);
			}
			
			if (0 == _selectedOrders.Count)
			{
				MessageBox.Show(this, "������Ҫѡ��1������ִ�з�������.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			
			if (_selectedOrders.Count > 1)
			{
				//DialogResult dr = MessageBox.Show(this, "ѡ���˶������, �Ƿ�ϲ�����?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				//if (DialogResult.Yes != dr)
				//    return;
				
				// Check if all selected orders belong to the same buyer and addresses are the same.
				foreach (Order o in _selectedOrders)
				{
					if (o.Status != Order.OrderStatus.Paid)
					{
						MessageBox.Show(this, "ѡ�еĶ����а�����<����Ѹ���, �ȴ����ҷ���>����, �޷��ϲ�����.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
				
					if(!chkSameAddr.Checked)
					{
						string addr = (string.IsNullOrEmpty(o.EditedRecipientAddress) ? o.RecipientAddress : o.EditedRecipientAddress);

						foreach (Order o1 in _selectedOrders)
						{
							if (!o.BuyerAccount.Equals(o1.BuyerAccount))
							{
								MessageBox.Show(this, "ѡ��Ĳ���������ͬ1�����, �޷��ϲ�����.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								return;
							}

							string addr1 = (string.IsNullOrEmpty(o1.EditedRecipientAddress) ? o1.RecipientAddress : o1.EditedRecipientAddress);

							if (!addr.Equals(addr1) && !addr.StartsWith(addr1) && !addr1.StartsWith(addr))
							{
								MessageBox.Show(this, "����Ҳ�ͬ�������ջ���ַ��ͬ, �޷��ϲ�����.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								return;
							}
						}
					}
				}
			}

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void lblPrompt_OnLocationSizeChanged(object sender, EventArgs e)
		{
			lblPrompt2.Location = new Point(lblPrompt.Right + 2, lblPrompt.Top);
		}
	}
}