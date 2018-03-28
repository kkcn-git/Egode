namespace Egode.OuterOrder
{
	partial class OuterOrdersForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OuterOrdersForm));
			this.ts = new System.Windows.Forms.ToolStrip();
			this.tsbtnImportOrders = new System.Windows.Forms.ToolStripButton();
			this.tsbtnExport = new System.Windows.Forms.ToolStripButton();
			this.lvwOrders = new System.Windows.Forms.ListView();
			this.colTaobaoId = new System.Windows.Forms.ColumnHeader();
			this.colProduct = new System.Windows.Forms.ColumnHeader();
			this.colCount = new System.Windows.Forms.ColumnHeader();
			this.colMoney = new System.Windows.Forms.ColumnHeader();
			this.colExpressType = new System.Windows.Forms.ColumnHeader();
			this.colBillNumber = new System.Windows.Forms.ColumnHeader();
			this.colAddress = new System.Windows.Forms.ColumnHeader();
			this.colDemand = new System.Windows.Forms.ColumnHeader();
			this.imglst = new System.Windows.Forms.ImageList(this.components);
			this.ts.SuspendLayout();
			this.SuspendLayout();
			// 
			// ts
			// 
			this.ts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnImportOrders,
            this.tsbtnExport});
			this.ts.Location = new System.Drawing.Point(0, 0);
			this.ts.Name = "ts";
			this.ts.Size = new System.Drawing.Size(852, 25);
			this.ts.TabIndex = 0;
			this.ts.Text = "toolStrip1";
			// 
			// tsbtnImportOrders
			// 
			this.tsbtnImportOrders.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnImportOrders.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnImportOrders.Image")));
			this.tsbtnImportOrders.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnImportOrders.Name = "tsbtnImportOrders";
			this.tsbtnImportOrders.Size = new System.Drawing.Size(23, 22);
			this.tsbtnImportOrders.Text = "导入外部订单...";
			this.tsbtnImportOrders.Click += new System.EventHandler(this.tsbtnImportOrders_Click);
			// 
			// tsbtnExport
			// 
			this.tsbtnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnExport.Image")));
			this.tsbtnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnExport.Name = "tsbtnExport";
			this.tsbtnExport.Size = new System.Drawing.Size(23, 22);
			this.tsbtnExport.Text = "toolStripButton1";
			this.tsbtnExport.Click += new System.EventHandler(this.tsbtnExport_Click);
			// 
			// lvwOrders
			// 
			this.lvwOrders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTaobaoId,
            this.colProduct,
            this.colCount,
            this.colMoney,
            this.colExpressType,
            this.colBillNumber,
            this.colAddress,
            this.colDemand});
			this.lvwOrders.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvwOrders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.lvwOrders.FullRowSelect = true;
			this.lvwOrders.GridLines = true;
			this.lvwOrders.Location = new System.Drawing.Point(0, 25);
			this.lvwOrders.Name = "lvwOrders";
			this.lvwOrders.Size = new System.Drawing.Size(852, 308);
			this.lvwOrders.SmallImageList = this.imglst;
			this.lvwOrders.TabIndex = 1;
			this.lvwOrders.UseCompatibleStateImageBehavior = false;
			this.lvwOrders.View = System.Windows.Forms.View.Details;
			this.lvwOrders.DoubleClick += new System.EventHandler(this.lvwOrders_DoubleClick);
			// 
			// colTaobaoId
			// 
			this.colTaobaoId.Text = "淘宝账号";
			this.colTaobaoId.Width = 110;
			// 
			// colProduct
			// 
			this.colProduct.Text = "商品";
			this.colProduct.Width = 120;
			// 
			// colCount
			// 
			this.colCount.Text = "数量";
			this.colCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// colMoney
			// 
			this.colMoney.Text = "金额";
			this.colMoney.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// colExpressType
			// 
			this.colExpressType.Text = "指定快递";
			// 
			// colBillNumber
			// 
			this.colBillNumber.Text = "快递单号";
			this.colBillNumber.Width = 90;
			// 
			// colAddress
			// 
			this.colAddress.Text = "地址";
			this.colAddress.Width = 240;
			// 
			// colDemand
			// 
			this.colDemand.Text = "特殊需求";
			this.colDemand.Width = 100;
			// 
			// imglst
			// 
			this.imglst.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglst.ImageStream")));
			this.imglst.TransparentColor = System.Drawing.Color.Transparent;
			this.imglst.Images.SetKeyName(0, "checkmark.ico");
			// 
			// OuterOrdersForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(852, 333);
			this.Controls.Add(this.lvwOrders);
			this.Controls.Add(this.ts);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "OuterOrdersForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "外部订单";
			this.Shown += new System.EventHandler(this.OuterOrdersForm_Shown);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OuterOrdersForm_FormClosing);
			this.ts.ResumeLayout(false);
			this.ts.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip ts;
		private System.Windows.Forms.ToolStripButton tsbtnImportOrders;
		private System.Windows.Forms.ListView lvwOrders;
		private System.Windows.Forms.ColumnHeader colTaobaoId;
		private System.Windows.Forms.ColumnHeader colProduct;
		private System.Windows.Forms.ColumnHeader colCount;
		private System.Windows.Forms.ColumnHeader colExpressType;
		private System.Windows.Forms.ColumnHeader colAddress;
		private System.Windows.Forms.ColumnHeader colMoney;
		private System.Windows.Forms.ColumnHeader colDemand;
		private System.Windows.Forms.ImageList imglst;
		private System.Windows.Forms.ColumnHeader colBillNumber;
		private System.Windows.Forms.ToolStripButton tsbtnExport;
	}
}