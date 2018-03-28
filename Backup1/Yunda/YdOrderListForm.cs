using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Yunda
{
	public partial class YdOrderListForm : Form
	{
		#region class YdOrderListViewItem
		private class YdOrderListViewItem : ListViewItem
		{
			private YdOrder _ydOrder;
		
			public YdOrderListViewItem(YdOrder ydOrder)
			{
				_ydOrder = ydOrder;

				this.Text = string.Format("{0}\n{1}", _ydOrder.OrderId, _ydOrder.ProductsInfo);
				this.SubItems.Add(_ydOrder.RecipientName);
				this.SubItems.Add(_ydOrder.RecipientPhone);
				this.SubItems.Add(_ydOrder.RecipientMobile);
				this.SubItems.Add(_ydOrder.RecipientFullAddress);
			}
			
			public YdOrder YdOrder
			{
				get { return _ydOrder; }
			}
		}
		#endregion
	
		private List<YdOrder> _ydOrders;
	
		public YdOrderListForm(List<YdOrder> ydOrders)
		{
			_ydOrders = ydOrders;
			InitializeComponent();
		}

		private void YdOrderListForm_Load(object sender, EventArgs e)
		{
			if (null != _ydOrders && _ydOrders.Count >= 0)
			{
				this.Text += string.Format(" [共{0}个韵达订单]", _ydOrders.Count);
			
				foreach (YdOrder o in _ydOrders)
				{
					YdOrderControl oc = new YdOrderControl(o);
					oc.Margin = new Padding(1);
					oc.Width = flpnl.Width - oc.Margin.Left - oc.Margin.Right;
					flpnl.Controls.Add(oc);
				}
			}	
		}

		private void tsbtnExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void tsbtnExportYdExcel_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			

			SaveFileDialog sfd = new SaveFileDialog();
			sfd.FileName = string.Format("yd_orders_{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmss"));
			sfd.Filter = "Excel Files (*.xls)|*.xls|All Files (*.*)|*.*";
			sfd.OverwritePrompt = true;
			if (DialogResult.OK == sfd.ShowDialog(this))
				YdOrder.ExportYundaOrders(_ydOrders, Path.Combine(Directory.GetParent(Application.ExecutablePath).FullName, "template_dr.xls"), sfd.FileName);
			
			Cursor.Current = Cursors.Default;
		}

		private void flpnl_SizeChanged(object sender, EventArgs e)
		{
			foreach (Control c in flpnl.Controls)
			{
				c.Width = flpnl.Width - c.Margin.Left - c.Margin.Right;
			}
		}
	}
}