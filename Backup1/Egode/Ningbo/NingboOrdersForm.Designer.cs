namespace Egode
{
	partial class NingboOrdersForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NingboOrdersForm));
			this.lvwNingboOrders = new System.Windows.Forms.ListView();
			this.colLogisticsCompany = new System.Windows.Forms.ColumnHeader();
			this.colMailNumber = new System.Windows.Forms.ColumnHeader();
			this.colTaobaoOrderId = new System.Windows.Forms.ColumnHeader();
			this.colRecipientName = new System.Windows.Forms.ColumnHeader();
			this.colMobile = new System.Windows.Forms.ColumnHeader();
			this.colProvince = new System.Windows.Forms.ColumnHeader();
			this.colCity = new System.Windows.Forms.ColumnHeader();
			this.colDistrict = new System.Windows.Forms.ColumnHeader();
			this.colStreetAddr = new System.Windows.Forms.ColumnHeader();
			this.colProduct = new System.Windows.Forms.ColumnHeader();
			this.colCount = new System.Windows.Forms.ColumnHeader();
			this.colIdInfo = new System.Windows.Forms.ColumnHeader();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.btnExport = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnLoad = new System.Windows.Forms.Button();
			this.btnSearchTaobaoOrder = new System.Windows.Forms.Button();
			this.btnConsign = new System.Windows.Forms.Button();
			this.btnStockout = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnRemarkBillNumber = new System.Windows.Forms.Button();
			this.chkSearchByMobile = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// lvwNingboOrders
			// 
			this.lvwNingboOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwNingboOrders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLogisticsCompany,
            this.colMailNumber,
            this.colTaobaoOrderId,
            this.colRecipientName,
            this.colMobile,
            this.colProvince,
            this.colCity,
            this.colDistrict,
            this.colStreetAddr,
            this.colProduct,
            this.colCount,
            this.colIdInfo});
			this.lvwNingboOrders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.lvwNingboOrders.FullRowSelect = true;
			this.lvwNingboOrders.GridLines = true;
			this.lvwNingboOrders.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvwNingboOrders.HideSelection = false;
			this.lvwNingboOrders.Location = new System.Drawing.Point(2, 2);
			this.lvwNingboOrders.Name = "lvwNingboOrders";
			this.lvwNingboOrders.Size = new System.Drawing.Size(898, 295);
			this.lvwNingboOrders.SmallImageList = this.imageList;
			this.lvwNingboOrders.TabIndex = 0;
			this.lvwNingboOrders.UseCompatibleStateImageBehavior = false;
			this.lvwNingboOrders.View = System.Windows.Forms.View.Details;
			this.lvwNingboOrders.SelectedIndexChanged += new System.EventHandler(this.lvwNingboOrders_SelectedIndexChanged);
			this.lvwNingboOrders.DoubleClick += new System.EventHandler(this.lvwNingboOrders_DoubleClick);
			// 
			// colLogisticsCompany
			// 
			this.colLogisticsCompany.Text = "快递公司";
			this.colLogisticsCompany.Width = 85;
			// 
			// colMailNumber
			// 
			this.colMailNumber.Text = "快递单号";
			this.colMailNumber.Width = 80;
			// 
			// colTaobaoOrderId
			// 
			this.colTaobaoOrderId.Text = "网站订单编号";
			this.colTaobaoOrderId.Width = 90;
			// 
			// colRecipientName
			// 
			this.colRecipientName.Text = "收货人";
			// 
			// colMobile
			// 
			this.colMobile.Text = "手机";
			// 
			// colProvince
			// 
			this.colProvince.Text = "省份";
			// 
			// colCity
			// 
			this.colCity.Text = "市";
			// 
			// colDistrict
			// 
			this.colDistrict.Text = "区(县)";
			// 
			// colStreetAddr
			// 
			this.colStreetAddr.Text = "详细地址";
			this.colStreetAddr.Width = 90;
			// 
			// colProduct
			// 
			this.colProduct.Text = "商品";
			this.colProduct.Width = 80;
			// 
			// colCount
			// 
			this.colCount.Text = "数量";
			this.colCount.Width = 40;
			// 
			// colIdInfo
			// 
			this.colIdInfo.Text = "身份证信息";
			this.colIdInfo.Width = 140;
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "forward.ico");
			// 
			// btnExport
			// 
			this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnExport.BackColor = System.Drawing.Color.CornflowerBlue;
			this.btnExport.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnExport.ForeColor = System.Drawing.Color.White;
			this.btnExport.Location = new System.Drawing.Point(2, 303);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(136, 23);
			this.btnExport.TabIndex = 1;
			this.btnExport.Text = "导出表格...";
			this.btnExport.UseVisualStyleBackColor = false;
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(825, 303);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 7;
			this.btnClose.Text = "关闭";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnLoad
			// 
			this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnLoad.Location = new System.Drawing.Point(141, 303);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(80, 23);
			this.btnLoad.TabIndex = 2;
			this.btnLoad.Text = "Load...";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// btnSearchTaobaoOrder
			// 
			this.btnSearchTaobaoOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSearchTaobaoOrder.Enabled = false;
			this.btnSearchTaobaoOrder.Location = new System.Drawing.Point(370, 303);
			this.btnSearchTaobaoOrder.Name = "btnSearchTaobaoOrder";
			this.btnSearchTaobaoOrder.Size = new System.Drawing.Size(100, 23);
			this.btnSearchTaobaoOrder.TabIndex = 4;
			this.btnSearchTaobaoOrder.Text = "查找淘宝订单";
			this.btnSearchTaobaoOrder.UseVisualStyleBackColor = true;
			this.btnSearchTaobaoOrder.Click += new System.EventHandler(this.btnSearchTaobaoOrder_Click);
			// 
			// btnConsign
			// 
			this.btnConsign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnConsign.Enabled = false;
			this.btnConsign.ForeColor = System.Drawing.Color.RoyalBlue;
			this.btnConsign.Location = new System.Drawing.Point(476, 303);
			this.btnConsign.Name = "btnConsign";
			this.btnConsign.Size = new System.Drawing.Size(100, 23);
			this.btnConsign.TabIndex = 5;
			this.btnConsign.Text = "点发货";
			this.btnConsign.UseVisualStyleBackColor = true;
			this.btnConsign.Click += new System.EventHandler(this.btnConsign_Click);
			// 
			// btnStockout
			// 
			this.btnStockout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnStockout.Enabled = false;
			this.btnStockout.Location = new System.Drawing.Point(684, 303);
			this.btnStockout.Name = "btnStockout";
			this.btnStockout.Size = new System.Drawing.Size(100, 23);
			this.btnStockout.TabIndex = 6;
			this.btnStockout.Text = "出库";
			this.btnStockout.UseVisualStyleBackColor = true;
			this.btnStockout.Click += new System.EventHandler(this.btnStockout_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRemove.BackColor = System.Drawing.SystemColors.Control;
			this.btnRemove.Enabled = false;
			this.btnRemove.ForeColor = System.Drawing.Color.Firebrick;
			this.btnRemove.Location = new System.Drawing.Point(266, 303);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(73, 23);
			this.btnRemove.TabIndex = 3;
			this.btnRemove.Text = "删除";
			this.btnRemove.UseVisualStyleBackColor = false;
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnRemarkBillNumber
			// 
			this.btnRemarkBillNumber.Location = new System.Drawing.Point(580, 303);
			this.btnRemarkBillNumber.Name = "btnRemarkBillNumber";
			this.btnRemarkBillNumber.Size = new System.Drawing.Size(100, 23);
			this.btnRemarkBillNumber.TabIndex = 8;
			this.btnRemarkBillNumber.Text = "备注单号...";
			this.btnRemarkBillNumber.UseVisualStyleBackColor = true;
			this.btnRemarkBillNumber.Click += new System.EventHandler(this.btnRemarkBillNumber_Click);
			// 
			// chkSearchByMobile
			// 
			this.chkSearchByMobile.AutoSize = true;
			this.chkSearchByMobile.Location = new System.Drawing.Point(354, 311);
			this.chkSearchByMobile.Name = "chkSearchByMobile";
			this.chkSearchByMobile.Size = new System.Drawing.Size(15, 14);
			this.chkSearchByMobile.TabIndex = 9;
			this.chkSearchByMobile.UseVisualStyleBackColor = true;
			// 
			// NingboOrdersForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(904, 327);
			this.Controls.Add(this.chkSearchByMobile);
			this.Controls.Add(this.btnRemarkBillNumber);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnStockout);
			this.Controls.Add(this.btnConsign);
			this.Controls.Add(this.btnSearchTaobaoOrder);
			this.Controls.Add(this.btnLoad);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnExport);
			this.Controls.Add(this.lvwNingboOrders);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "NingboOrdersForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Ningbo Orders";
			this.Load += new System.EventHandler(this.NingboOrdersForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView lvwNingboOrders;
		private System.Windows.Forms.ColumnHeader colLogisticsCompany;
		private System.Windows.Forms.ColumnHeader colTaobaoOrderId;
		private System.Windows.Forms.ColumnHeader colRecipientName;
		private System.Windows.Forms.ColumnHeader colMobile;
		private System.Windows.Forms.ColumnHeader colProvince;
		private System.Windows.Forms.ColumnHeader colCity;
		private System.Windows.Forms.ColumnHeader colDistrict;
		private System.Windows.Forms.ColumnHeader colStreetAddr;
		private System.Windows.Forms.ColumnHeader colProduct;
		private System.Windows.Forms.ColumnHeader colCount;
		private System.Windows.Forms.ColumnHeader colIdInfo;
		private System.Windows.Forms.Button btnExport;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.ColumnHeader colMailNumber;
		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.Button btnSearchTaobaoOrder;
		private System.Windows.Forms.Button btnConsign;
		private System.Windows.Forms.Button btnStockout;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnRemarkBillNumber;
		private System.Windows.Forms.CheckBox chkSearchByMobile;
	}
}