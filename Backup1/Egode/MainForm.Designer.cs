namespace Egode
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.tsMain = new System.Windows.Forms.ToolStrip();
			this.tsbtnDownloadData = new System.Windows.Forms.ToolStripButton();
			this.tsbtnImportOrders = new System.Windows.Forms.ToolStripButton();
			this.tsbtnConfig = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnYundaOrders = new System.Windows.Forms.ToolStripButton();
			this.tsbtnImportYundaOrders = new System.Windows.Forms.ToolStripButton();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.miRunZto = new System.Windows.Forms.ToolStripMenuItem();
			this.miRunYunda = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnGeneratePackets = new System.Windows.Forms.ToolStripButton();
			this.tsbtnAutoFillShipmentNumber = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnNingboOrders = new System.Windows.Forms.ToolStripButton();
			this.tsbtnAutoCommentBuyer = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnSearch = new System.Windows.Forms.ToolStripButton();
			this.txtKeyword = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.btnShowAll = new System.Windows.Forms.ToolStripButton();
			this.tsbtnAnalyseOrders = new System.Windows.Forms.ToolStripButton();
			this.tsbtnManualPrint = new System.Windows.Forms.ToolStripButton();
			this.statusMain = new System.Windows.Forms.StatusStrip();
			this.tsslblInfo = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslblNingboOrders = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslblOrders = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslblLastModifiedDate = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlOrders = new System.Windows.Forms.FlowLayoutPanel();
			this.tsMain.SuspendLayout();
			this.statusMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// tsMain
			// 
			this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsbtnDownloadData,
			this.tsbtnImportOrders,
			this.tsbtnConfig,
			this.toolStripSeparator4,
			this.tsbtnYundaOrders,
			this.tsbtnImportYundaOrders,
			this.toolStripDropDownButton1,
			this.toolStripSeparator5,
			this.tsbtnGeneratePackets,
			this.tsbtnAutoFillShipmentNumber,
			this.toolStripSeparator3,
			this.tsbtnNingboOrders,
			this.tsbtnAutoCommentBuyer,
			this.toolStripSeparator1,
			this.tsbtnSearch,
			this.txtKeyword,
			this.toolStripSeparator2,
			this.btnShowAll,
			this.tsbtnAnalyseOrders,
			this.tsbtnManualPrint});
			this.tsMain.Location = new System.Drawing.Point(0, 0);
			this.tsMain.Name = "tsMain";
			this.tsMain.Size = new System.Drawing.Size(1152, 25);
			this.tsMain.TabIndex = 0;
			this.tsMain.Text = "toolStrip1";
			// 
			// tsbtnDownloadData
			// 
			this.tsbtnDownloadData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnDownloadData.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDownloadData.Image")));
			this.tsbtnDownloadData.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnDownloadData.Name = "tsbtnDownloadData";
			this.tsbtnDownloadData.Size = new System.Drawing.Size(23, 22);
			this.tsbtnDownloadData.Text = "下载订单";
			this.tsbtnDownloadData.ToolTipText = "下载订单";
			this.tsbtnDownloadData.Visible = false;
			this.tsbtnDownloadData.Click += new System.EventHandler(this.tsbtnDownloadData_Click);
			// 
			// tsbtnImportOrders
			// 
			this.tsbtnImportOrders.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnImportOrders.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnImportOrders.Image")));
			this.tsbtnImportOrders.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnImportOrders.Name = "tsbtnImportOrders";
			this.tsbtnImportOrders.Size = new System.Drawing.Size(23, 22);
			this.tsbtnImportOrders.Text = "Import orders from file";
			this.tsbtnImportOrders.Visible = false;
			this.tsbtnImportOrders.Click += new System.EventHandler(this.tsbtnImportOrders_Click);
			// 
			// tsbtnConfig
			// 
			this.tsbtnConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnConfig.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnConfig.Image")));
			this.tsbtnConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnConfig.Name = "tsbtnConfig";
			this.tsbtnConfig.Size = new System.Drawing.Size(23, 22);
			this.tsbtnConfig.Text = "Config";
			this.tsbtnConfig.Click += new System.EventHandler(this.tsbtnConfig_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbtnYundaOrders
			// 
			this.tsbtnYundaOrders.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnYundaOrders.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnYundaOrders.Image")));
			this.tsbtnYundaOrders.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnYundaOrders.Name = "tsbtnYundaOrders";
			this.tsbtnYundaOrders.Size = new System.Drawing.Size(23, 22);
			this.tsbtnYundaOrders.Text = "toolStripButton1";
			this.tsbtnYundaOrders.Click += new System.EventHandler(this.tsbtnYundaOrders_Click);
			// 
			// tsbtnImportYundaOrders
			// 
			this.tsbtnImportYundaOrders.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnImportYundaOrders.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnImportYundaOrders.Image")));
			this.tsbtnImportYundaOrders.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnImportYundaOrders.Name = "tsbtnImportYundaOrders";
			this.tsbtnImportYundaOrders.Size = new System.Drawing.Size(23, 22);
			this.tsbtnImportYundaOrders.Text = "导入韵达订单";
			this.tsbtnImportYundaOrders.Click += new System.EventHandler(this.tsbtnImportYundaOrders_Click);
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.miRunZto,
			this.miRunYunda});
			this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
			this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
			// 
			// miRunZto
			// 
			this.miRunZto.Image = ((System.Drawing.Image)(resources.GetObject("miRunZto.Image")));
			this.miRunZto.Name = "miRunZto";
			this.miRunZto.Size = new System.Drawing.Size(152, 22);
			this.miRunZto.Text = "中通GO";
			this.miRunZto.Click += new System.EventHandler(this.MiRunZtoClick);
			// 
			// miRunYunda
			// 
			this.miRunYunda.Image = ((System.Drawing.Image)(resources.GetObject("miRunYunda.Image")));
			this.miRunYunda.Name = "miRunYunda";
			this.miRunYunda.Size = new System.Drawing.Size(152, 22);
			this.miRunYunda.Text = "韵达GO";
			this.miRunYunda.Click += new System.EventHandler(this.MiRunYundaClick);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbtnGeneratePackets
			// 
			this.tsbtnGeneratePackets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnGeneratePackets.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnGeneratePackets.Image")));
			this.tsbtnGeneratePackets.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnGeneratePackets.Name = "tsbtnGeneratePackets";
			this.tsbtnGeneratePackets.Size = new System.Drawing.Size(23, 22);
			this.tsbtnGeneratePackets.Text = "出单";
			this.tsbtnGeneratePackets.Click += new System.EventHandler(this.tsbtnGeneratePackets_Click);
			// 
			// tsbtnAutoFillShipmentNumber
			// 
			this.tsbtnAutoFillShipmentNumber.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnAutoFillShipmentNumber.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAutoFillShipmentNumber.Image")));
			this.tsbtnAutoFillShipmentNumber.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnAutoFillShipmentNumber.Name = "tsbtnAutoFillShipmentNumber";
			this.tsbtnAutoFillShipmentNumber.Size = new System.Drawing.Size(23, 22);
			this.tsbtnAutoFillShipmentNumber.ToolTipText = "自动填入单号";
			this.tsbtnAutoFillShipmentNumber.Click += new System.EventHandler(this.tsbtnAutoFillShipmentNumber_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbtnNingboOrders
			// 
			this.tsbtnNingboOrders.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnNingboOrders.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnNingboOrders.Image")));
			this.tsbtnNingboOrders.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnNingboOrders.Name = "tsbtnNingboOrders";
			this.tsbtnNingboOrders.Size = new System.Drawing.Size(23, 22);
			this.tsbtnNingboOrders.Text = "Ningbo Orders";
			this.tsbtnNingboOrders.Click += new System.EventHandler(this.tsbtnNingboOrders_Click);
			// 
			// tsbtnAutoCommentBuyer
			// 
			this.tsbtnAutoCommentBuyer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnAutoCommentBuyer.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAutoCommentBuyer.Image")));
			this.tsbtnAutoCommentBuyer.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnAutoCommentBuyer.Name = "tsbtnAutoCommentBuyer";
			this.tsbtnAutoCommentBuyer.Size = new System.Drawing.Size(23, 22);
			this.tsbtnAutoCommentBuyer.Text = "评价";
			this.tsbtnAutoCommentBuyer.Visible = false;
			this.tsbtnAutoCommentBuyer.Click += new System.EventHandler(this.tsbtnAutoCommentBuyer_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbtnSearch
			// 
			this.tsbtnSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tsbtnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnSearch.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSearch.Image")));
			this.tsbtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnSearch.Name = "tsbtnSearch";
			this.tsbtnSearch.Size = new System.Drawing.Size(23, 22);
			this.tsbtnSearch.Text = "toolStripButton1";
			this.tsbtnSearch.Click += new System.EventHandler(this.tsbtnSearch_Click);
			// 
			// txtKeyword
			// 
			this.txtKeyword.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.txtKeyword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtKeyword.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
			this.txtKeyword.Name = "txtKeyword";
			this.txtKeyword.Size = new System.Drawing.Size(160, 21);
			this.txtKeyword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKeyword_KeyDown);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// btnShowAll
			// 
			this.btnShowAll.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.btnShowAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnShowAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnShowAll.ForeColor = System.Drawing.Color.Blue;
			this.btnShowAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnShowAll.Name = "btnShowAll";
			this.btnShowAll.Size = new System.Drawing.Size(58, 22);
			this.btnShowAll.Text = "Show All";
			this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
			// 
			// tsbtnAnalyseOrders
			// 
			this.tsbtnAnalyseOrders.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnAnalyseOrders.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAnalyseOrders.Image")));
			this.tsbtnAnalyseOrders.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnAnalyseOrders.Name = "tsbtnAnalyseOrders";
			this.tsbtnAnalyseOrders.Size = new System.Drawing.Size(23, 22);
			this.tsbtnAnalyseOrders.Text = "toolStripButton1";
			this.tsbtnAnalyseOrders.Click += new System.EventHandler(this.tsbtnAnalyseOrders_Click);
			// 
			// tsbtnManualPrint
			// 
			this.tsbtnManualPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnManualPrint.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnManualPrint.Image")));
			this.tsbtnManualPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnManualPrint.Name = "tsbtnManualPrint";
			this.tsbtnManualPrint.Size = new System.Drawing.Size(23, 22);
			this.tsbtnManualPrint.Text = "toolStripButton1";
			// 
			// statusMain
			// 
			this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsslblInfo,
			this.tsslblNingboOrders,
			this.tsslblOrders,
			this.tsslblLastModifiedDate});
			this.statusMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.statusMain.Location = new System.Drawing.Point(0, 672);
			this.statusMain.Name = "statusMain";
			this.statusMain.Size = new System.Drawing.Size(1152, 22);
			this.statusMain.SizingGrip = false;
			this.statusMain.TabIndex = 2;
			// 
			// tsslblInfo
			// 
			this.tsslblInfo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tsslblInfo.Image = ((System.Drawing.Image)(resources.GetObject("tsslblInfo.Image")));
			this.tsslblInfo.Name = "tsslblInfo";
			this.tsslblInfo.Size = new System.Drawing.Size(97, 17);
			this.tsslblInfo.Text = "0 packet added";
			this.tsslblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.tsslblInfo.DoubleClick += new System.EventHandler(this.tsslblInfo_DoubleClick);
			// 
			// tsslblNingboOrders
			// 
			this.tsslblNingboOrders.Image = ((System.Drawing.Image)(resources.GetObject("tsslblNingboOrders.Image")));
			this.tsslblNingboOrders.Margin = new System.Windows.Forms.Padding(120, 3, 0, 2);
			this.tsslblNingboOrders.Name = "tsslblNingboOrders";
			this.tsslblNingboOrders.Size = new System.Drawing.Size(99, 17);
			this.tsslblNingboOrders.Text = "0 Ningbo orders";
			this.tsslblNingboOrders.Click += new System.EventHandler(this.tsslblNingboOrders_Click);
			// 
			// tsslblOrders
			// 
			this.tsslblOrders.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.tsslblOrders.Margin = new System.Windows.Forms.Padding(100, 3, 0, 2);
			this.tsslblOrders.Name = "tsslblOrders";
			this.tsslblOrders.Size = new System.Drawing.Size(47, 17);
			this.tsslblOrders.Text = "0 orders";
			this.tsslblOrders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tsslblLastModifiedDate
			// 
			this.tsslblLastModifiedDate.Margin = new System.Windows.Forms.Padding(60, 3, 0, 2);
			this.tsslblLastModifiedDate.Name = "tsslblLastModifiedDate";
			this.tsslblLastModifiedDate.Size = new System.Drawing.Size(177, 17);
			this.tsslblLastModifiedDate.Text = "last modified: 0000/00/00 00:00:00";
			// 
			// pnlOrders
			// 
			this.pnlOrders.AutoScroll = true;
			this.pnlOrders.BackColor = System.Drawing.Color.White;
			this.pnlOrders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlOrders.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlOrders.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.pnlOrders.Location = new System.Drawing.Point(0, 25);
			this.pnlOrders.Name = "pnlOrders";
			this.pnlOrders.Padding = new System.Windows.Forms.Padding(3);
			this.pnlOrders.Size = new System.Drawing.Size(1152, 647);
			this.pnlOrders.TabIndex = 3;
			this.pnlOrders.WrapContents = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1152, 694);
			this.Controls.Add(this.pnlOrders);
			this.Controls.Add(this.statusMain);
			this.Controls.Add(this.tsMain);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "订单管理";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.tsMain.ResumeLayout(false);
			this.tsMain.PerformLayout();
			this.statusMain.ResumeLayout(false);
			this.statusMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip tsMain;
		private System.Windows.Forms.ToolStripButton tsbtnDownloadData;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.StatusStrip statusMain;
		private System.Windows.Forms.FlowLayoutPanel pnlOrders;
		private System.Windows.Forms.ToolStripButton tsbtnSearch;
		private System.Windows.Forms.ToolStripTextBox txtKeyword;
		private System.Windows.Forms.ToolStripButton btnShowAll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripStatusLabel tsslblInfo;
		private System.Windows.Forms.ToolStripButton tsbtnGeneratePackets;
		private System.Windows.Forms.ToolStripStatusLabel tsslblOrders;
		private System.Windows.Forms.ToolStripButton tsbtnAutoFillShipmentNumber;
		private System.Windows.Forms.ToolStripButton tsbtnImportOrders;
		private System.Windows.Forms.ToolStripButton tsbtnAutoCommentBuyer;
		private System.Windows.Forms.ToolStripStatusLabel tsslblLastModifiedDate;
		private System.Windows.Forms.ToolStripStatusLabel tsslblNingboOrders;
		private System.Windows.Forms.ToolStripButton tsbtnNingboOrders;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton tsbtnAnalyseOrders;
		private System.Windows.Forms.ToolStripButton tsbtnManualPrint;
		private System.Windows.Forms.ToolStripButton tsbtnConfig;
		private System.Windows.Forms.ToolStripButton tsbtnYundaOrders;
		private System.Windows.Forms.ToolStripButton tsbtnImportYundaOrders;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
		private System.Windows.Forms.ToolStripMenuItem miRunZto;
		private System.Windows.Forms.ToolStripMenuItem miRunYunda;
	}
}

