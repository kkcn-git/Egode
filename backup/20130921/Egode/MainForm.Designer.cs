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
			this.tsbtnGeneratePackets = new System.Windows.Forms.ToolStripButton();
			this.tsbtnAutoFillShipmentNumber = new System.Windows.Forms.ToolStripButton();
			this.tsbtnAutoCommentBuyer = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnSearch = new System.Windows.Forms.ToolStripButton();
			this.txtKeyword = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.btnShowAll = new System.Windows.Forms.ToolStripButton();
			this.statusMain = new System.Windows.Forms.StatusStrip();
			this.tsslblInfo = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslblOrders = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.tsbtnGeneratePackets,
            this.tsbtnAutoFillShipmentNumber,
            this.tsbtnAutoCommentBuyer,
            this.toolStripSeparator1,
            this.tsbtnSearch,
            this.txtKeyword,
            this.toolStripSeparator2,
            this.btnShowAll});
			this.tsMain.Location = new System.Drawing.Point(0, 0);
			this.tsMain.Name = "tsMain";
			this.tsMain.Size = new System.Drawing.Size(1064, 27);
			this.tsMain.TabIndex = 0;
			this.tsMain.Text = "toolStrip1";
			// 
			// tsbtnDownloadData
			// 
			this.tsbtnDownloadData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnDownloadData.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDownloadData.Image")));
			this.tsbtnDownloadData.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnDownloadData.Name = "tsbtnDownloadData";
			this.tsbtnDownloadData.Size = new System.Drawing.Size(23, 24);
			this.tsbtnDownloadData.Text = "下载订单";
			this.tsbtnDownloadData.ToolTipText = "下载订单";
			this.tsbtnDownloadData.Click += new System.EventHandler(this.tsbtnDownloadData_Click);
			// 
			// tsbtnImportOrders
			// 
			this.tsbtnImportOrders.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnImportOrders.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnImportOrders.Image")));
			this.tsbtnImportOrders.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnImportOrders.Name = "tsbtnImportOrders";
			this.tsbtnImportOrders.Size = new System.Drawing.Size(23, 24);
			this.tsbtnImportOrders.Text = "Import orders from file";
			this.tsbtnImportOrders.Visible = false;
			this.tsbtnImportOrders.Click += new System.EventHandler(this.tsbtnImportOrders_Click);
			// 
			// tsbtnGeneratePackets
			// 
			this.tsbtnGeneratePackets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnGeneratePackets.Enabled = false;
			this.tsbtnGeneratePackets.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnGeneratePackets.Image")));
			this.tsbtnGeneratePackets.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnGeneratePackets.Name = "tsbtnGeneratePackets";
			this.tsbtnGeneratePackets.Size = new System.Drawing.Size(23, 24);
			this.tsbtnGeneratePackets.Text = "出单";
			this.tsbtnGeneratePackets.Click += new System.EventHandler(this.tsbtnGeneratePackets_Click);
			// 
			// tsbtnAutoFillShipmentNumber
			// 
			this.tsbtnAutoFillShipmentNumber.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnAutoFillShipmentNumber.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAutoFillShipmentNumber.Image")));
			this.tsbtnAutoFillShipmentNumber.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnAutoFillShipmentNumber.Name = "tsbtnAutoFillShipmentNumber";
			this.tsbtnAutoFillShipmentNumber.Size = new System.Drawing.Size(23, 24);
			this.tsbtnAutoFillShipmentNumber.ToolTipText = "自动填入单号";
			this.tsbtnAutoFillShipmentNumber.Click += new System.EventHandler(this.tsbtnAutoFillShipmentNumber_Click);
			// 
			// tsbtnAutoCommentBuyer
			// 
			this.tsbtnAutoCommentBuyer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnAutoCommentBuyer.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAutoCommentBuyer.Image")));
			this.tsbtnAutoCommentBuyer.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnAutoCommentBuyer.Name = "tsbtnAutoCommentBuyer";
			this.tsbtnAutoCommentBuyer.Size = new System.Drawing.Size(23, 24);
			this.tsbtnAutoCommentBuyer.Text = "评价";
			this.tsbtnAutoCommentBuyer.Visible = false;
			this.tsbtnAutoCommentBuyer.Click += new System.EventHandler(this.tsbtnAutoCommentBuyer_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
			// 
			// tsbtnSearch
			// 
			this.tsbtnSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tsbtnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnSearch.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSearch.Image")));
			this.tsbtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnSearch.Name = "tsbtnSearch";
			this.tsbtnSearch.Size = new System.Drawing.Size(23, 24);
			this.tsbtnSearch.Text = "toolStripButton1";
			this.tsbtnSearch.Click += new System.EventHandler(this.tsbtnSearch_Click);
			// 
			// txtKeyword
			// 
			this.txtKeyword.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.txtKeyword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtKeyword.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
			this.txtKeyword.Name = "txtKeyword";
			this.txtKeyword.Size = new System.Drawing.Size(160, 23);
			this.txtKeyword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKeyword_KeyDown);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
			// 
			// btnShowAll
			// 
			this.btnShowAll.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.btnShowAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnShowAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnShowAll.ForeColor = System.Drawing.Color.Blue;
			this.btnShowAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnShowAll.Name = "btnShowAll";
			this.btnShowAll.Size = new System.Drawing.Size(58, 24);
			this.btnShowAll.Text = "Show All";
			this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
			// 
			// statusMain
			// 
			this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslblInfo,
            this.tsslblOrders});
			this.statusMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.statusMain.Location = new System.Drawing.Point(0, 740);
			this.statusMain.Name = "statusMain";
			this.statusMain.Size = new System.Drawing.Size(1064, 22);
			this.statusMain.SizingGrip = false;
			this.statusMain.TabIndex = 2;
			// 
			// tsslblInfo
			// 
			this.tsslblInfo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tsslblInfo.Name = "tsslblInfo";
			this.tsslblInfo.Size = new System.Drawing.Size(81, 17);
			this.tsslblInfo.Text = "0 packet added";
			this.tsslblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tsslblOrders
			// 
			this.tsslblOrders.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.tsslblOrders.Margin = new System.Windows.Forms.Padding(260, 3, 0, 2);
			this.tsslblOrders.Name = "tsslblOrders";
			this.tsslblOrders.Size = new System.Drawing.Size(49, 17);
			this.tsslblOrders.Text = "0 orders";
			this.tsslblOrders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnlOrders
			// 
			this.pnlOrders.AutoScroll = true;
			this.pnlOrders.BackColor = System.Drawing.Color.White;
			this.pnlOrders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlOrders.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlOrders.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.pnlOrders.Location = new System.Drawing.Point(0, 27);
			this.pnlOrders.Name = "pnlOrders";
			this.pnlOrders.Padding = new System.Windows.Forms.Padding(3);
			this.pnlOrders.Size = new System.Drawing.Size(1064, 713);
			this.pnlOrders.TabIndex = 3;
			this.pnlOrders.WrapContents = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1064, 762);
			this.Controls.Add(this.pnlOrders);
			this.Controls.Add(this.statusMain);
			this.Controls.Add(this.tsMain);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "德国e购";
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
	}
}

