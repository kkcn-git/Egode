using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class OrderIdControl : UserControl
	{
		public event EventHandler OnConsignSh;

		private OrderParser.Order _order;
		
		public OrderIdControl()
		{
			InitializeComponent();
		}

		public OrderParser.Order Order
		{
			get { return _order; }
			set
			{
				_order = value;
				if (null == _order)
					return;
				lblOrderId.Text = _order.OrderId;
				string url = string.Format("http://trade.taobao.com/trade/detail/trade_item_detail.htm?bizOrderId={0}", _order.OrderId);
				lblOrderId.Links.Clear();
				lblOrderId.Links.Add(new LinkLabel.Link(0, lblOrderId.Text.Length, url));
			}
		}
		
		public bool ConsignVisible
		{
			get { return btnConsign.Visible; }
			set { btnConsign.Visible = value; }
		}

		private void lblTitle_SizeChanged(object sender, EventArgs e)
		{
			int height = lblTitle.Height > lblOrderId.Height ? lblTitle.Height : lblOrderId.Height;
			lblTitle.Location = new Point(0, height - lblTitle.Height);
			lblOrderId.Location = new Point(lblTitle.Right - 3, height - lblOrderId.Height);
			lblOrderId.BringToFront();
			btnConsign.Location = new Point(lblOrderId.Right-2, (height - btnConsign.Height)+1);
			btnConsign.BringToFront();
			btnConsignSh.Location = new Point(btnConsign.Right, (height - btnConsignSh.Height) + 1);
			this.Height = height;
		}

		private void lblOrderId_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start((string)e.Link.LinkData);
		}

		private void btnConsign_Click(object sender, EventArgs e)
		{
			ConsignForm cf = new ConsignForm(_order.OrderId);
			cf.ShowDialog(this.FindForm());
		}

		private void btnConsignSh_Click(object sender, EventArgs e)
		{
			if (null != this.OnConsignSh)
				this.OnConsignSh(this, EventArgs.Empty);
		}
	}
}