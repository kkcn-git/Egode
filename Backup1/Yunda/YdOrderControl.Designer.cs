namespace Yunda
{
	partial class YdOrderControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.flpnl = new System.Windows.Forms.FlowLayoutPanel();
			this.txtOrderIdAndProducts = new System.Windows.Forms.TextBox();
			this.txtRecipientName = new System.Windows.Forms.TextBox();
			this.txtRecipientPhone = new System.Windows.Forms.TextBox();
			this.txtRecipientMobile = new System.Windows.Forms.TextBox();
			this.txtRecipientFullAddr = new System.Windows.Forms.TextBox();
			this.flpnl.SuspendLayout();
			this.SuspendLayout();
			// 
			// flpnl
			// 
			this.flpnl.Controls.Add(this.txtOrderIdAndProducts);
			this.flpnl.Controls.Add(this.txtRecipientName);
			this.flpnl.Controls.Add(this.txtRecipientPhone);
			this.flpnl.Controls.Add(this.txtRecipientMobile);
			this.flpnl.Controls.Add(this.txtRecipientFullAddr);
			this.flpnl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flpnl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.flpnl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.flpnl.Location = new System.Drawing.Point(0, 0);
			this.flpnl.Name = "flpnl";
			this.flpnl.Size = new System.Drawing.Size(660, 26);
			this.flpnl.TabIndex = 0;
			this.flpnl.WrapContents = false;
			// 
			// txtOrderIdAndProducts
			// 
			this.txtOrderIdAndProducts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
			this.txtOrderIdAndProducts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtOrderIdAndProducts.Location = new System.Drawing.Point(1, 3);
			this.txtOrderIdAndProducts.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
			this.txtOrderIdAndProducts.Multiline = true;
			this.txtOrderIdAndProducts.Name = "txtOrderIdAndProducts";
			this.txtOrderIdAndProducts.Size = new System.Drawing.Size(220, 20);
			this.txtOrderIdAndProducts.TabIndex = 0;
			// 
			// txtRecipientName
			// 
			this.txtRecipientName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
			this.txtRecipientName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtRecipientName.Location = new System.Drawing.Point(223, 3);
			this.txtRecipientName.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
			this.txtRecipientName.Multiline = true;
			this.txtRecipientName.Name = "txtRecipientName";
			this.txtRecipientName.Size = new System.Drawing.Size(100, 21);
			this.txtRecipientName.TabIndex = 1;
			// 
			// txtRecipientPhone
			// 
			this.txtRecipientPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
			this.txtRecipientPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtRecipientPhone.Location = new System.Drawing.Point(325, 3);
			this.txtRecipientPhone.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
			this.txtRecipientPhone.Multiline = true;
			this.txtRecipientPhone.Name = "txtRecipientPhone";
			this.txtRecipientPhone.Size = new System.Drawing.Size(100, 21);
			this.txtRecipientPhone.TabIndex = 2;
			// 
			// txtRecipientMobile
			// 
			this.txtRecipientMobile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
			this.txtRecipientMobile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtRecipientMobile.Location = new System.Drawing.Point(427, 3);
			this.txtRecipientMobile.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
			this.txtRecipientMobile.Multiline = true;
			this.txtRecipientMobile.Name = "txtRecipientMobile";
			this.txtRecipientMobile.Size = new System.Drawing.Size(100, 21);
			this.txtRecipientMobile.TabIndex = 3;
			// 
			// txtRecipientFullAddr
			// 
			this.txtRecipientFullAddr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtRecipientFullAddr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
			this.txtRecipientFullAddr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtRecipientFullAddr.Location = new System.Drawing.Point(529, 3);
			this.txtRecipientFullAddr.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
			this.txtRecipientFullAddr.Multiline = true;
			this.txtRecipientFullAddr.Name = "txtRecipientFullAddr";
			this.txtRecipientFullAddr.Size = new System.Drawing.Size(130, 21);
			this.txtRecipientFullAddr.TabIndex = 4;
			// 
			// YdOrderControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.flpnl);
			this.Name = "YdOrderControl";
			this.Size = new System.Drawing.Size(660, 26);
			this.SizeChanged += new System.EventHandler(this.YdOrderControl_SizeChanged);
			this.flpnl.ResumeLayout(false);
			this.flpnl.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flpnl;
		private System.Windows.Forms.TextBox txtOrderIdAndProducts;
		private System.Windows.Forms.TextBox txtRecipientName;
		private System.Windows.Forms.TextBox txtRecipientPhone;
		private System.Windows.Forms.TextBox txtRecipientMobile;
		private System.Windows.Forms.TextBox txtRecipientFullAddr;
	}
}
