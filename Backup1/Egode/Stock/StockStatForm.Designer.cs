namespace Egode
{
	partial class StockStatForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockStatForm));
			this.tsMain = new System.Windows.Forms.ToolStrip();
			this.tscboPeroid = new System.Windows.Forms.ToolStripComboBox();
			this.lvwDayStats = new System.Windows.Forms.ListView();
			this.colDate = new System.Windows.Forms.ColumnHeader();
			this.colAptamilPre = new System.Windows.Forms.ColumnHeader();
			this.colAptamil1 = new System.Windows.Forms.ColumnHeader();
			this.colAptamil2 = new System.Windows.Forms.ColumnHeader();
			this.colAptamil3 = new System.Windows.Forms.ColumnHeader();
			this.colAptamil4 = new System.Windows.Forms.ColumnHeader();
			this.colAptamil5 = new System.Windows.Forms.ColumnHeader();
			this.colTotal = new System.Windows.Forms.ColumnHeader();
			this.lblTotalInfo = new System.Windows.Forms.Label();
			this.tscboProductBunds = new System.Windows.Forms.ToolStripComboBox();
			this.tsMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// tsMain
			// 
			this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscboPeroid,
            this.tscboProductBunds});
			this.tsMain.Location = new System.Drawing.Point(0, 0);
			this.tsMain.Name = "tsMain";
			this.tsMain.Size = new System.Drawing.Size(812, 25);
			this.tsMain.TabIndex = 2;
			this.tsMain.Text = "toolStrip1";
			// 
			// tscboPeroid
			// 
			this.tscboPeroid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tscboPeroid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tscboPeroid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.tscboPeroid.Margin = new System.Windows.Forms.Padding(1, 0, 8, 0);
			this.tscboPeroid.Name = "tscboPeroid";
			this.tscboPeroid.Size = new System.Drawing.Size(80, 25);
			// 
			// lvwDayStats
			// 
			this.lvwDayStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwDayStats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDate,
            this.colAptamilPre,
            this.colAptamil1,
            this.colAptamil2,
            this.colAptamil3,
            this.colAptamil4,
            this.colAptamil5,
            this.colTotal});
			this.lvwDayStats.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.lvwDayStats.FullRowSelect = true;
			this.lvwDayStats.GridLines = true;
			this.lvwDayStats.Location = new System.Drawing.Point(2, 27);
			this.lvwDayStats.Name = "lvwDayStats";
			this.lvwDayStats.Size = new System.Drawing.Size(612, 318);
			this.lvwDayStats.TabIndex = 3;
			this.lvwDayStats.UseCompatibleStateImageBehavior = false;
			this.lvwDayStats.View = System.Windows.Forms.View.Details;
			// 
			// colDate
			// 
			this.colDate.Text = "Date";
			this.colDate.Width = 80;
			// 
			// colAptamilPre
			// 
			this.colAptamilPre.Text = "Aptamil Pre";
			this.colAptamilPre.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colAptamilPre.Width = 75;
			// 
			// colAptamil1
			// 
			this.colAptamil1.Text = "Aptamil 1";
			this.colAptamil1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colAptamil1.Width = 70;
			// 
			// colAptamil2
			// 
			this.colAptamil2.Text = "Aptamil 2";
			this.colAptamil2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colAptamil2.Width = 70;
			// 
			// colAptamil3
			// 
			this.colAptamil3.Text = "Aptamil 3";
			this.colAptamil3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colAptamil3.Width = 70;
			// 
			// colAptamil4
			// 
			this.colAptamil4.Text = "Aptamil 1+";
			this.colAptamil4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colAptamil4.Width = 75;
			// 
			// colAptamil5
			// 
			this.colAptamil5.Text = "Aptamil 2+";
			this.colAptamil5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colAptamil5.Width = 75;
			// 
			// colTotal
			// 
			this.colTotal.Text = "Total";
			this.colTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.colTotal.Width = 50;
			// 
			// lblTotalInfo
			// 
			this.lblTotalInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblTotalInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblTotalInfo.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTotalInfo.ForeColor = System.Drawing.Color.OrangeRed;
			this.lblTotalInfo.Location = new System.Drawing.Point(616, 27);
			this.lblTotalInfo.Name = "lblTotalInfo";
			this.lblTotalInfo.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
			this.lblTotalInfo.Size = new System.Drawing.Size(193, 318);
			this.lblTotalInfo.TabIndex = 4;
			this.lblTotalInfo.Text = "Statistics Info";
			// 
			// tscboProductBunds
			// 
			this.tscboProductBunds.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.tscboProductBunds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tscboProductBunds.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tscboProductBunds.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.tscboProductBunds.Name = "tscboProductBunds";
			this.tscboProductBunds.Size = new System.Drawing.Size(121, 25);
			// 
			// StockStatForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(812, 347);
			this.Controls.Add(this.lblTotalInfo);
			this.Controls.Add(this.lvwDayStats);
			this.Controls.Add(this.tsMain);
			this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "StockStatForm";
			this.Text = "现货统计";
			this.Shown += new System.EventHandler(this.StockStatForm_Shown);
			this.tsMain.ResumeLayout(false);
			this.tsMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip tsMain;
		private System.Windows.Forms.ListView lvwDayStats;
		private System.Windows.Forms.ColumnHeader colDate;
		private System.Windows.Forms.ColumnHeader colAptamilPre;
		private System.Windows.Forms.ColumnHeader colAptamil1;
		private System.Windows.Forms.ColumnHeader colAptamil2;
		private System.Windows.Forms.ColumnHeader colAptamil3;
		private System.Windows.Forms.ColumnHeader colAptamil4;
		private System.Windows.Forms.ColumnHeader colAptamil5;
		private System.Windows.Forms.ColumnHeader colTotal;
		private System.Windows.Forms.Label lblTotalInfo;
		private System.Windows.Forms.ToolStripComboBox tscboPeroid;
		private System.Windows.Forms.ToolStripComboBox tscboProductBunds;
	}
}