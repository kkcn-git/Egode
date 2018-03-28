using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class BuyerInfoControl : UserControl
	{
		public event EventHandler OnBuyerClick;

		public BuyerInfoControl()
		{
			InitializeComponent();
		}
		
		public string BuyerAccount
		{
			get { return lblBuyerAccount.Text; }
			set { lblBuyerAccount.Text = value; }
		}
		
		public int OrderAmount
		{
			get 
			{ 
				if (null == lblOrderAmount || null == lblOrderAmount.Tag)
					return 0;
				return (int)lblOrderAmount.Tag; 
			}
			set 
			{
				lblOrderAmount.Text = string.Format("({0})", value);
				lblOrderAmount.Tag = value;
				
				if (value > 1)
				{
					lblOrderAmount.ForeColor = Color.BlueViolet;
					lblOrderAmount.Font = new Font(lblOrderAmount.Font, FontStyle.Bold);
				}
			}
		}

		private void lblTitle_SizeChanged(object sender, EventArgs e)
		{
			int height = lblTitle.Height > lblBuyerAccount.Height ? lblTitle.Height : lblBuyerAccount.Height;
			height = height > lblOrderAmount.Height ? height : lblOrderAmount.Height;
			height = height > btnCopy.Height ? height : btnCopy.Height;
			
			lblTitle.Location = new Point(0, height - lblTitle.Height);
			lblBuyerAccount.Location = new Point(lblTitle.Right - 3, height - lblBuyerAccount.Height);
			lblOrderAmount.Location = new Point(lblBuyerAccount.Right - 5, height - lblOrderAmount.Height);
			btnCopy.Location = new Point(lblOrderAmount.Right, height - btnCopy.Height);
			lblBuyerAccount.BringToFront();
			lblOrderAmount.BringToFront();
			this.Height = height;
		}

		private void lblBuyerAccount_Click(object sender, EventArgs e)
		{
			if (null != this.OnBuyerClick)
				this.OnBuyerClick(this, EventArgs.Empty);
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			try
			{
				Clipboard.SetText(lblBuyerAccount.Text);
				ClipboardPromptForm cpf = new ClipboardPromptForm("¬Úº“’À∫≈", lblBuyerAccount.Text);
				cpf.Show(this.FindForm());
				cpf.Location = new Point(
					this.FindForm().Location.X + (this.FindForm().Width - cpf.Width) / 2, 
					this.FindForm().Location.Y + (this.FindForm().Height - cpf.Height) / 2);
			}
			catch 
			{
				ClipboardPromptForm cpf = new ClipboardPromptForm("∏¥÷∆’À∫≈ ß∞‹:", lblBuyerAccount.Text);
				cpf.Show(this.FindForm());
				cpf.Location = new Point(
					this.FindForm().Location.X + (this.FindForm().Width - cpf.Width) / 2,
					this.FindForm().Location.Y + (this.FindForm().Height - cpf.Height) / 2);
			}
		}
	}
}