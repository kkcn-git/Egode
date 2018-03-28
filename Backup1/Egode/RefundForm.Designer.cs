namespace Egode
{
	partial class RefundForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RefundForm));
			this.tsMain = new System.Windows.Forms.ToolStrip();
			this.tsbtnAddRefund = new System.Windows.Forms.ToolStripButton();
			this.tsbtnRefresh = new System.Windows.Forms.ToolStripButton();
			this.tsbtnSearch = new System.Windows.Forms.ToolStripButton();
			this.txtKeyword = new System.Windows.Forms.ToolStripTextBox();
			this.lvwRefunds = new System.Windows.Forms.ListView();
			this.colOperator = new System.Windows.Forms.ColumnHeader();
			this.colDate = new System.Windows.Forms.ColumnHeader();
			this.colShipmentNumber = new System.Windows.Forms.ColumnHeader();
			this.colSrc = new System.Windows.Forms.ColumnHeader();
			this.colItem = new System.Windows.Forms.ColumnHeader();
			this.colComment = new System.Windows.Forms.ColumnHeader();
			this.tsMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// tsMain
			// 
			this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnAddRefund,
            this.tsbtnRefresh,
            this.tsbtnSearch,
            this.txtKeyword});
			this.tsMain.Location = new System.Drawing.Point(0, 0);
			this.tsMain.Name = "tsMain";
			this.tsMain.Size = new System.Drawing.Size(764, 25);
			this.tsMain.TabIndex = 1;
			this.tsMain.Text = "toolStrip1";
			// 
			// tsbtnAddRefund
			// 
			this.tsbtnAddRefund.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnAddRefund.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tsbtnAddRefund.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAddRefund.Image")));
			this.tsbtnAddRefund.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnAddRefund.Name = "tsbtnAddRefund";
			this.tsbtnAddRefund.Size = new System.Drawing.Size(23, 22);
			this.tsbtnAddRefund.Text = "toolStripButton1";
			this.tsbtnAddRefund.ToolTipText = "Refund";
			this.tsbtnAddRefund.Click += new System.EventHandler(this.tsbtnAddRefund_Click);
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
			this.tsbtnRefresh.Click += new System.EventHandler(this.tsbtnRefresh_Click);
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
			// lvwRefunds
			// 
			this.lvwRefunds.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colOperator,
            this.colDate,
            this.colShipmentNumber,
            this.colSrc,
            this.colItem,
            this.colComment});
			this.lvwRefunds.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvwRefunds.FullRowSelect = true;
			this.lvwRefunds.GridLines = true;
			this.lvwRefunds.Location = new System.Drawing.Point(0, 25);
			this.lvwRefunds.Name = "lvwRefunds";
			this.lvwRefunds.Size = new System.Drawing.Size(764, 417);
			this.lvwRefunds.TabIndex = 2;
			this.lvwRefunds.UseCompatibleStateImageBehavior = false;
			this.lvwRefunds.View = System.Windows.Forms.View.Details;
			// 
			// colOperator
			// 
			this.colOperator.Text = "Operator";
			// 
			// colDate
			// 
			this.colDate.Text = "Date";
			this.colDate.Width = 130;
			// 
			// colShipmentNumber
			// 
			this.colShipmentNumber.Text = "Shipment No.";
			this.colShipmentNumber.Width = 110;
			// 
			// colSrc
			// 
			this.colSrc.Text = "Source";
			this.colSrc.Width = 160;
			// 
			// colItem
			// 
			this.colItem.Text = "Item";
			this.colItem.Width = 160;
			// 
			// colComment
			// 
			this.colComment.Text = "Comment";
			this.colComment.Width = 160;
			// 
			// RefundForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(764, 442);
			this.Controls.Add(this.lvwRefunds);
			this.Controls.Add(this.tsMain);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "RefundForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Refund History";
			this.Shown += new System.EventHandler(this.StockStatForm_Shown);
			this.tsMain.ResumeLayout(false);
			this.tsMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip tsMain;
		private System.Windows.Forms.ListView lvwRefunds;
		private System.Windows.Forms.ColumnHeader colDate;
		private System.Windows.Forms.ColumnHeader colShipmentNumber;
		private System.Windows.Forms.ColumnHeader colSrc;
		private System.Windows.Forms.ColumnHeader colItem;
		private System.Windows.Forms.ColumnHeader colComment;
		private System.Windows.Forms.ToolStripButton tsbtnAddRefund;
		private System.Windows.Forms.ToolStripButton tsbtnRefresh;
		private System.Windows.Forms.ToolStripTextBox txtKeyword;
		private System.Windows.Forms.ToolStripButton tsbtnSearch;
		private System.Windows.Forms.ColumnHeader colOperator;

	}
}