namespace Yunda
{
	partial class YdOrderListForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YdOrderListForm));
			this.ts = new System.Windows.Forms.ToolStrip();
			this.tsbtnExportYdExcel = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tslblSenderName = new System.Windows.Forms.ToolStripLabel();
			this.tstxtSenderName = new System.Windows.Forms.ToolStripTextBox();
			this.tslblSenderMobile = new System.Windows.Forms.ToolStripLabel();
			this.tstxtSenderMobile = new System.Windows.Forms.ToolStripTextBox();
			this.tslblSenderAddr = new System.Windows.Forms.ToolStripLabel();
			this.tstxtSenderAddr = new System.Windows.Forms.ToolStripTextBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tslblOrderId = new System.Windows.Forms.ToolStripLabel();
			this.tslblRecipientName = new System.Windows.Forms.ToolStripLabel();
			this.tslblRecipientPhone = new System.Windows.Forms.ToolStripLabel();
			this.tslblRecipientMobile = new System.Windows.Forms.ToolStripLabel();
			this.tslblRecipientFullAddr = new System.Windows.Forms.ToolStripLabel();
			this.flpnl = new System.Windows.Forms.FlowLayoutPanel();
			this.ts.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// ts
			// 
			this.ts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnExportYdExcel,
            this.toolStripSeparator1,
            this.tslblSenderName,
            this.tstxtSenderName,
            this.tslblSenderMobile,
            this.tstxtSenderMobile,
            this.tslblSenderAddr,
            this.tstxtSenderAddr});
			this.ts.Location = new System.Drawing.Point(0, 0);
			this.ts.Name = "ts";
			this.ts.Size = new System.Drawing.Size(752, 25);
			this.ts.TabIndex = 1;
			this.ts.Text = "toolStrip1";
			// 
			// tsbtnExportYdExcel
			// 
			this.tsbtnExportYdExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbtnExportYdExcel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tsbtnExportYdExcel.ForeColor = System.Drawing.Color.DodgerBlue;
			this.tsbtnExportYdExcel.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnExportYdExcel.Image")));
			this.tsbtnExportYdExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnExportYdExcel.Name = "tsbtnExportYdExcel";
			this.tsbtnExportYdExcel.Size = new System.Drawing.Size(37, 22);
			this.tsbtnExportYdExcel.Text = "导出";
			this.tsbtnExportYdExcel.Click += new System.EventHandler(this.tsbtnExportYdExcel_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tslblSenderName
			// 
			this.tslblSenderName.Name = "tslblSenderName";
			this.tslblSenderName.Size = new System.Drawing.Size(71, 22);
			this.tslblSenderName.Text = "发件人姓名:";
			this.tslblSenderName.Visible = false;
			// 
			// tstxtSenderName
			// 
			this.tstxtSenderName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.tstxtSenderName.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.tstxtSenderName.Name = "tstxtSenderName";
			this.tstxtSenderName.Size = new System.Drawing.Size(60, 25);
			this.tstxtSenderName.Text = "顾双双";
			this.tstxtSenderName.Visible = false;
			// 
			// tslblSenderMobile
			// 
			this.tslblSenderMobile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.tslblSenderMobile.Margin = new System.Windows.Forms.Padding(16, 1, 0, 2);
			this.tslblSenderMobile.Name = "tslblSenderMobile";
			this.tslblSenderMobile.Size = new System.Drawing.Size(71, 22);
			this.tslblSenderMobile.Text = "发件人手机:";
			this.tslblSenderMobile.Visible = false;
			// 
			// tstxtSenderMobile
			// 
			this.tstxtSenderMobile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.tstxtSenderMobile.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.tstxtSenderMobile.Name = "tstxtSenderMobile";
			this.tstxtSenderMobile.Size = new System.Drawing.Size(80, 25);
			this.tstxtSenderMobile.Text = "18964913317";
			this.tstxtSenderMobile.Visible = false;
			// 
			// tslblSenderAddr
			// 
			this.tslblSenderAddr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.tslblSenderAddr.Margin = new System.Windows.Forms.Padding(16, 1, 0, 2);
			this.tslblSenderAddr.Name = "tslblSenderAddr";
			this.tslblSenderAddr.Size = new System.Drawing.Size(95, 22);
			this.tslblSenderAddr.Text = "发件人详细地址:";
			this.tslblSenderAddr.Visible = false;
			// 
			// tstxtSenderAddr
			// 
			this.tstxtSenderAddr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.tstxtSenderAddr.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.tstxtSenderAddr.Name = "tstxtSenderAddr";
			this.tstxtSenderAddr.Size = new System.Drawing.Size(100, 25);
			this.tstxtSenderAddr.Text = "银春路678号";
			this.tstxtSenderAddr.Visible = false;
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblOrderId,
            this.tslblRecipientName,
            this.tslblRecipientPhone,
            this.tslblRecipientMobile,
            this.tslblRecipientFullAddr});
			this.toolStrip1.Location = new System.Drawing.Point(0, 25);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(752, 25);
			this.toolStrip1.TabIndex = 3;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tslblOrderId
			// 
			this.tslblOrderId.AutoSize = false;
			this.tslblOrderId.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.tslblOrderId.Name = "tslblOrderId";
			this.tslblOrderId.Size = new System.Drawing.Size(220, 22);
			this.tslblOrderId.Text = "订单号";
			this.tslblOrderId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tslblRecipientName
			// 
			this.tslblRecipientName.AutoSize = false;
			this.tslblRecipientName.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.tslblRecipientName.Name = "tslblRecipientName";
			this.tslblRecipientName.Size = new System.Drawing.Size(100, 22);
			this.tslblRecipientName.Text = "收件人姓名";
			this.tslblRecipientName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tslblRecipientPhone
			// 
			this.tslblRecipientPhone.AutoSize = false;
			this.tslblRecipientPhone.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.tslblRecipientPhone.Name = "tslblRecipientPhone";
			this.tslblRecipientPhone.Size = new System.Drawing.Size(100, 22);
			this.tslblRecipientPhone.Text = "收件人电话";
			this.tslblRecipientPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tslblRecipientMobile
			// 
			this.tslblRecipientMobile.AutoSize = false;
			this.tslblRecipientMobile.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.tslblRecipientMobile.Name = "tslblRecipientMobile";
			this.tslblRecipientMobile.Size = new System.Drawing.Size(100, 22);
			this.tslblRecipientMobile.Text = "收件人手机";
			this.tslblRecipientMobile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tslblRecipientFullAddr
			// 
			this.tslblRecipientFullAddr.AutoSize = false;
			this.tslblRecipientFullAddr.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.tslblRecipientFullAddr.Name = "tslblRecipientFullAddr";
			this.tslblRecipientFullAddr.Size = new System.Drawing.Size(91, 22);
			this.tslblRecipientFullAddr.Text = "收件人详细地址";
			this.tslblRecipientFullAddr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// flpnl
			// 
			this.flpnl.AutoScroll = true;
			this.flpnl.BackColor = System.Drawing.SystemColors.Window;
			this.flpnl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flpnl.Location = new System.Drawing.Point(0, 50);
			this.flpnl.Name = "flpnl";
			this.flpnl.Size = new System.Drawing.Size(752, 246);
			this.flpnl.TabIndex = 4;
			this.flpnl.SizeChanged += new System.EventHandler(this.flpnl_SizeChanged);
			// 
			// YdOrderListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(752, 296);
			this.Controls.Add(this.flpnl);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.ts);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "YdOrderListForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "韵达订单";
			this.Load += new System.EventHandler(this.YdOrderListForm_Load);
			this.ts.ResumeLayout(false);
			this.ts.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip ts;
		private System.Windows.Forms.ToolStripButton tsbtnExportYdExcel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel tslblSenderName;
		private System.Windows.Forms.ToolStripTextBox tstxtSenderName;
		private System.Windows.Forms.ToolStripLabel tslblSenderMobile;
		private System.Windows.Forms.ToolStripTextBox tstxtSenderMobile;
		private System.Windows.Forms.ToolStripLabel tslblSenderAddr;
		private System.Windows.Forms.ToolStripTextBox tstxtSenderAddr;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripLabel tslblOrderId;
		private System.Windows.Forms.ToolStripLabel tslblRecipientName;
		private System.Windows.Forms.ToolStripLabel tslblRecipientPhone;
		private System.Windows.Forms.ToolStripLabel tslblRecipientMobile;
		private System.Windows.Forms.ToolStripLabel tslblRecipientFullAddr;
		private System.Windows.Forms.FlowLayoutPanel flpnl;
	}
}