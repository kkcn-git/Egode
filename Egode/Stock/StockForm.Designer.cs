namespace Egode
{
	partial class StockForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockForm));
			this.tsMain = new System.Windows.Forms.ToolStrip();
			this.tsbtnStockout = new System.Windows.Forms.ToolStripButton();
			this.tsbtnStockin = new System.Windows.Forms.ToolStripButton();
			this.tsbtnDeleteRecord = new System.Windows.Forms.ToolStripButton();
			this.tsbtnRefresh = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnSearch = new System.Windows.Forms.ToolStripButton();
			this.txtKeyword = new System.Windows.Forms.ToolStripTextBox();
			this.tsbtnExport = new System.Windows.Forms.ToolStripButton();
			this.tcMain = new System.Windows.Forms.TabControl();
			this.tpStock = new System.Windows.Forms.TabPage();
			this.lvwStock = new System.Windows.Forms.ListView();
			this.colBrand = new System.Windows.Forms.ColumnHeader();
			this.colProduct1 = new System.Windows.Forms.ColumnHeader();
			this.colCount1 = new System.Windows.Forms.ColumnHeader();
			this.colComment1 = new System.Windows.Forms.ColumnHeader();
			this.tpHistory = new System.Windows.Forms.TabPage();
			this.lvwHistory = new System.Windows.Forms.ListView();
			this.colOperator = new System.Windows.Forms.ColumnHeader();
			this.colDateTime = new System.Windows.Forms.ColumnHeader();
			this.colProduct = new System.Windows.Forms.ColumnHeader();
			this.colCount = new System.Windows.Forms.ColumnHeader();
			this.colFromTo = new System.Windows.Forms.ColumnHeader();
			this.colComment = new System.Windows.Forms.ColumnHeader();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.tsMain.SuspendLayout();
			this.tcMain.SuspendLayout();
			this.tpStock.SuspendLayout();
			this.tpHistory.SuspendLayout();
			this.SuspendLayout();
			// 
			// tsMain
			// 
			this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnStockout,
            this.tsbtnStockin,
            this.tsbtnDeleteRecord,
            this.tsbtnRefresh,
            this.toolStripSeparator1,
            this.tsbtnSearch,
            this.txtKeyword,
            this.tsbtnExport,
            this.toolStripButton1});
			this.tsMain.Location = new System.Drawing.Point(0, 0);
			this.tsMain.Name = "tsMain";
			this.tsMain.Size = new System.Drawing.Size(664, 25);
			this.tsMain.TabIndex = 0;
			this.tsMain.Text = "toolStrip1";
			// 
			// tsbtnStockout
			// 
			this.tsbtnStockout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnStockout.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnStockout.Image")));
			this.tsbtnStockout.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnStockout.Name = "tsbtnStockout";
			this.tsbtnStockout.Size = new System.Drawing.Size(23, 22);
			this.tsbtnStockout.Text = "toolStripButton1";
			this.tsbtnStockout.ToolTipText = "Stock Out";
			this.tsbtnStockout.Click += new System.EventHandler(this.tsbtnStockout_Click);
			// 
			// tsbtnStockin
			// 
			this.tsbtnStockin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnStockin.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnStockin.Image")));
			this.tsbtnStockin.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnStockin.Name = "tsbtnStockin";
			this.tsbtnStockin.Size = new System.Drawing.Size(23, 22);
			this.tsbtnStockin.Text = "toolStripButton1";
			this.tsbtnStockin.ToolTipText = "Stock In";
			this.tsbtnStockin.Click += new System.EventHandler(this.tsbtnStockin_Click);
			// 
			// tsbtnDeleteRecord
			// 
			this.tsbtnDeleteRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnDeleteRecord.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDeleteRecord.Image")));
			this.tsbtnDeleteRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnDeleteRecord.Name = "tsbtnDeleteRecord";
			this.tsbtnDeleteRecord.Size = new System.Drawing.Size(23, 22);
			this.tsbtnDeleteRecord.Text = "toolStripButton1";
			this.tsbtnDeleteRecord.Click += new System.EventHandler(this.tsbtnDeleteRecord_Click);
			// 
			// tsbtnRefresh
			// 
			this.tsbtnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRefresh.Image")));
			this.tsbtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnRefresh.Name = "tsbtnRefresh";
			this.tsbtnRefresh.Size = new System.Drawing.Size(23, 22);
			this.tsbtnRefresh.Text = "toolStripButton1";
			this.tsbtnRefresh.ToolTipText = "Refresh";
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
			this.tsbtnSearch.ToolTipText = "Search";
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
			// tsbtnExport
			// 
			this.tsbtnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnExport.Image")));
			this.tsbtnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnExport.Name = "tsbtnExport";
			this.tsbtnExport.Size = new System.Drawing.Size(23, 22);
			this.tsbtnExport.Text = "Export";
			this.tsbtnExport.Click += new System.EventHandler(this.tsbtnExport_Click);
			// 
			// tcMain
			// 
			this.tcMain.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tcMain.Controls.Add(this.tpStock);
			this.tcMain.Controls.Add(this.tpHistory);
			this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcMain.Location = new System.Drawing.Point(0, 25);
			this.tcMain.Name = "tcMain";
			this.tcMain.SelectedIndex = 0;
			this.tcMain.Size = new System.Drawing.Size(664, 669);
			this.tcMain.TabIndex = 1;
			this.tcMain.SelectedIndexChanged += new System.EventHandler(this.tcMain_SelectedIndexChanged);
			// 
			// tpStock
			// 
			this.tpStock.Controls.Add(this.lvwStock);
			this.tpStock.Location = new System.Drawing.Point(4, 4);
			this.tpStock.Name = "tpStock";
			this.tpStock.Padding = new System.Windows.Forms.Padding(3);
			this.tpStock.Size = new System.Drawing.Size(656, 643);
			this.tpStock.TabIndex = 0;
			this.tpStock.Text = "Stock";
			this.tpStock.UseVisualStyleBackColor = true;
			// 
			// lvwStock
			// 
			this.lvwStock.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colBrand,
            this.colProduct1,
            this.colCount1,
            this.colComment1});
			this.lvwStock.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvwStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.lvwStock.FullRowSelect = true;
			this.lvwStock.GridLines = true;
			this.lvwStock.Location = new System.Drawing.Point(3, 3);
			this.lvwStock.Name = "lvwStock";
			this.lvwStock.Size = new System.Drawing.Size(650, 637);
			this.lvwStock.TabIndex = 0;
			this.lvwStock.UseCompatibleStateImageBehavior = false;
			this.lvwStock.View = System.Windows.Forms.View.Details;
			// 
			// colBrand
			// 
			this.colBrand.Text = "Brand";
			this.colBrand.Width = 100;
			// 
			// colProduct1
			// 
			this.colProduct1.Text = "Product";
			this.colProduct1.Width = 200;
			// 
			// colCount1
			// 
			this.colCount1.Text = "Count";
			this.colCount1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// colComment1
			// 
			this.colComment1.Text = "Comment";
			// 
			// tpHistory
			// 
			this.tpHistory.Controls.Add(this.lvwHistory);
			this.tpHistory.Location = new System.Drawing.Point(4, 4);
			this.tpHistory.Name = "tpHistory";
			this.tpHistory.Padding = new System.Windows.Forms.Padding(3);
			this.tpHistory.Size = new System.Drawing.Size(656, 643);
			this.tpHistory.TabIndex = 1;
			this.tpHistory.Text = "History";
			this.tpHistory.UseVisualStyleBackColor = true;
			// 
			// lvwHistory
			// 
			this.lvwHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colOperator,
            this.colDateTime,
            this.colProduct,
            this.colCount,
            this.colFromTo,
            this.colComment});
			this.lvwHistory.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvwHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.lvwHistory.FullRowSelect = true;
			this.lvwHistory.GridLines = true;
			this.lvwHistory.Location = new System.Drawing.Point(3, 3);
			this.lvwHistory.MultiSelect = false;
			this.lvwHistory.Name = "lvwHistory";
			this.lvwHistory.Size = new System.Drawing.Size(650, 637);
			this.lvwHistory.TabIndex = 0;
			this.lvwHistory.UseCompatibleStateImageBehavior = false;
			this.lvwHistory.View = System.Windows.Forms.View.Details;
			this.lvwHistory.SelectedIndexChanged += new System.EventHandler(this.lvwHistory_SelectedIndexChanged);
			// 
			// colOperator
			// 
			this.colOperator.Text = "Operator";
			// 
			// colDateTime
			// 
			this.colDateTime.Text = "Date";
			this.colDateTime.Width = 120;
			// 
			// colProduct
			// 
			this.colProduct.Text = "Product";
			this.colProduct.Width = 160;
			// 
			// colCount
			// 
			this.colCount.Text = "Count";
			this.colCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colCount.Width = 45;
			// 
			// colFromTo
			// 
			this.colFromTo.Text = "From/To";
			this.colFromTo.Width = 100;
			// 
			// colComment
			// 
			this.colComment.Text = "Comment";
			this.colComment.Width = 160;
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton1.Text = "B";
			this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// StockForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(664, 694);
			this.Controls.Add(this.tcMain);
			this.Controls.Add(this.tsMain);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "StockForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Stock - Shanghai";
			this.Load += new System.EventHandler(this.StockForm_Load);
			this.Shown += new System.EventHandler(this.StockForm_Shown);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StockForm_KeyDown);
			this.tsMain.ResumeLayout(false);
			this.tsMain.PerformLayout();
			this.tcMain.ResumeLayout(false);
			this.tpStock.ResumeLayout(false);
			this.tpHistory.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip tsMain;
		private System.Windows.Forms.TabControl tcMain;
		private System.Windows.Forms.TabPage tpStock;
		private System.Windows.Forms.TabPage tpHistory;
		private System.Windows.Forms.ListView lvwHistory;
		private System.Windows.Forms.ColumnHeader colOperator;
		private System.Windows.Forms.ColumnHeader colDateTime;
		private System.Windows.Forms.ColumnHeader colProduct;
		private System.Windows.Forms.ColumnHeader colCount;
		private System.Windows.Forms.ColumnHeader colFromTo;
		private System.Windows.Forms.ColumnHeader colComment;
		private System.Windows.Forms.ToolStripButton tsbtnStockout;
		private System.Windows.Forms.ToolStripButton tsbtnStockin;
		private System.Windows.Forms.ToolStripButton tsbtnRefresh;
		private System.Windows.Forms.ListView lvwStock;
		private System.Windows.Forms.ColumnHeader colBrand;
		private System.Windows.Forms.ColumnHeader colProduct1;
		private System.Windows.Forms.ColumnHeader colCount1;
		private System.Windows.Forms.ColumnHeader colComment1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripTextBox txtKeyword;
		private System.Windows.Forms.ToolStripButton tsbtnSearch;
		private System.Windows.Forms.ToolStripButton tsbtnExport;
		private System.Windows.Forms.ToolStripButton tsbtnDeleteRecord;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
	}
}