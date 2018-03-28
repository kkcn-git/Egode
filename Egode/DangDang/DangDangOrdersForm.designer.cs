namespace Dangdang
{
	partial class DangDangOrdersForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DangDangOrdersForm));
			this.ts = new System.Windows.Forms.ToolStrip();
			this.tsbtnImportDangdangOrders = new System.Windows.Forms.ToolStripButton();
			this.tsbtnExportHigo = new System.Windows.Forms.ToolStripButton();
			this.tsbtnExportYt = new System.Windows.Forms.ToolStripButton();
			this.tsbtnExportYydy = new System.Windows.Forms.ToolStripButton();
			this.lvwOrders = new System.Windows.Forms.ListView();
			this.colOrderId = new System.Windows.Forms.ColumnHeader();
			this.colDealTime = new System.Windows.Forms.ColumnHeader();
			this.colPayTime = new System.Windows.Forms.ColumnHeader();
			this.colProduct = new System.Windows.Forms.ColumnHeader();
			this.colCount = new System.Windows.Forms.ColumnHeader();
			this.colOrderStatus = new System.Windows.Forms.ColumnHeader();
			this.colRecipientName = new System.Windows.Forms.ColumnHeader();
			this.colIdNumber = new System.Windows.Forms.ColumnHeader();
			this.colMobile = new System.Windows.Forms.ColumnHeader();
			this.colAddress = new System.Windows.Forms.ColumnHeader();
			this.colPaymentId = new System.Windows.Forms.ColumnHeader();
			this.ts.SuspendLayout();
			this.SuspendLayout();
			// 
			// ts
			// 
			this.ts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnImportDangdangOrders,
            this.tsbtnExportHigo,
            this.tsbtnExportYt,
            this.tsbtnExportYydy});
			this.ts.Location = new System.Drawing.Point(0, 0);
			this.ts.Name = "ts";
			this.ts.Size = new System.Drawing.Size(844, 25);
			this.ts.TabIndex = 0;
			this.ts.Text = "toolStrip1";
			// 
			// tsbtnImportDangdangOrders
			// 
			this.tsbtnImportDangdangOrders.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnImportDangdangOrders.Image")));
			this.tsbtnImportDangdangOrders.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnImportDangdangOrders.Name = "tsbtnImportDangdangOrders";
			this.tsbtnImportDangdangOrders.Size = new System.Drawing.Size(111, 22);
			this.tsbtnImportDangdangOrders.Text = "导入当当订单...";
			this.tsbtnImportDangdangOrders.Click += new System.EventHandler(this.tsbtnImportDangdangOrders_Click);
			// 
			// tsbtnExportHigo
			// 
			this.tsbtnExportHigo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnExportHigo.Image")));
			this.tsbtnExportHigo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnExportHigo.Name = "tsbtnExportHigo";
			this.tsbtnExportHigo.Size = new System.Drawing.Size(111, 22);
			this.tsbtnExportHigo.Text = "导出海狗表格...";
			this.tsbtnExportHigo.ToolTipText = "Export for Higo";
			this.tsbtnExportHigo.Click += new System.EventHandler(this.tsbtnExportHigo_Click);
			// 
			// tsbtnExportYt
			// 
			this.tsbtnExportYt.Enabled = false;
			this.tsbtnExportYt.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnExportYt.Image")));
			this.tsbtnExportYt.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnExportYt.Name = "tsbtnExportYt";
			this.tsbtnExportYt.Size = new System.Drawing.Size(111, 22);
			this.tsbtnExportYt.Text = "导出洋驼表格...";
			this.tsbtnExportYt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.tsbtnExportYt.Click += new System.EventHandler(this.tsbtnExportYt_Click);
			// 
			// tsbtnExportYydy
			// 
			this.tsbtnExportYydy.Enabled = false;
			this.tsbtnExportYydy.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnExportYydy.Image")));
			this.tsbtnExportYydy.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnExportYydy.Name = "tsbtnExportYydy";
			this.tsbtnExportYydy.Size = new System.Drawing.Size(135, 22);
			this.tsbtnExportYydy.Text = "导出羊羊得意表格...";
			this.tsbtnExportYydy.Click += new System.EventHandler(this.tsbtnExportYydy_Click);
			// 
			// lvwOrders
			// 
			this.lvwOrders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colOrderId,
            this.colDealTime,
            this.colPayTime,
            this.colProduct,
            this.colCount,
            this.colOrderStatus,
            this.colRecipientName,
            this.colIdNumber,
            this.colMobile,
            this.colAddress,
            this.colPaymentId});
			this.lvwOrders.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvwOrders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.lvwOrders.FullRowSelect = true;
			this.lvwOrders.GridLines = true;
			this.lvwOrders.Location = new System.Drawing.Point(0, 25);
			this.lvwOrders.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.lvwOrders.Name = "lvwOrders";
			this.lvwOrders.Size = new System.Drawing.Size(844, 397);
			this.lvwOrders.TabIndex = 1;
			this.lvwOrders.UseCompatibleStateImageBehavior = false;
			this.lvwOrders.View = System.Windows.Forms.View.Details;
			this.lvwOrders.DoubleClick += new System.EventHandler(this.lvwOrders_DoubleClick);
			// 
			// colOrderId
			// 
			this.colOrderId.Text = "订单号";
			this.colOrderId.Width = 80;
			// 
			// colDealTime
			// 
			this.colDealTime.Text = "下单时间";
			this.colDealTime.Width = 120;
			// 
			// colPayTime
			// 
			this.colPayTime.Text = "付款时间";
			this.colPayTime.Width = 120;
			// 
			// colProduct
			// 
			this.colProduct.Text = "商品";
			this.colProduct.Width = 120;
			// 
			// colCount
			// 
			this.colCount.Text = "数量";
			// 
			// colOrderStatus
			// 
			this.colOrderStatus.Text = "订单状态";
			// 
			// colRecipientName
			// 
			this.colRecipientName.Text = "收货人";
			// 
			// colIdNumber
			// 
			this.colIdNumber.Text = "身份证";
			this.colIdNumber.Width = 120;
			// 
			// colMobile
			// 
			this.colMobile.Text = "联系电话";
			this.colMobile.Width = 80;
			// 
			// colAddress
			// 
			this.colAddress.Text = "送货地址";
			// 
			// colPaymentId
			// 
			this.colPaymentId.Text = "支付单号";
			// 
			// DangDangOrdersForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(844, 422);
			this.Controls.Add(this.lvwOrders);
			this.Controls.Add(this.ts);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "DangDangOrdersForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "当当临时出单系统";
			this.Shown += new System.EventHandler(this.DangDangOrdersForm_Shown);
			this.ts.ResumeLayout(false);
			this.ts.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip ts;
		private System.Windows.Forms.ToolStripButton tsbtnImportDangdangOrders;
		private System.Windows.Forms.ListView lvwOrders;
		private System.Windows.Forms.ColumnHeader colOrderId;
		private System.Windows.Forms.ColumnHeader colRecipientName;
		private System.Windows.Forms.ColumnHeader colIdNumber;
		private System.Windows.Forms.ColumnHeader colMobile;
		private System.Windows.Forms.ColumnHeader colAddress;
		private System.Windows.Forms.ColumnHeader colProduct;
		private System.Windows.Forms.ColumnHeader colCount;
		private System.Windows.Forms.ColumnHeader colPaymentId;
		private System.Windows.Forms.ColumnHeader colDealTime;
		private System.Windows.Forms.ColumnHeader colPayTime;
		private System.Windows.Forms.ColumnHeader colOrderStatus;
		private System.Windows.Forms.ToolStripButton tsbtnExportYydy;
		private System.Windows.Forms.ToolStripButton tsbtnExportHigo;
		private System.Windows.Forms.ToolStripButton tsbtnExportYt;
	}
}

