namespace Egode
{
	partial class OrderDhlStateForm
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
			this.lvwOrders = new System.Windows.Forms.ListView();
			this.colIndex = new System.Windows.Forms.ColumnHeader();
			this.colOrderId = new System.Windows.Forms.ColumnHeader();
			this.colBuyer = new System.Windows.Forms.ColumnHeader();
			this.colShipmentNumber = new System.Windows.Forms.ColumnHeader();
			this.colStatus = new System.Windows.Forms.ColumnHeader();
			this.colDays = new System.Windows.Forms.ColumnHeader();
			this.btnLoad = new System.Windows.Forms.Button();
			this.lblInfo = new System.Windows.Forms.Label();
			this.btnReport = new System.Windows.Forms.Button();
			this.lblGreaterThan = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// lvwOrders
			// 
			this.lvwOrders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIndex,
            this.colOrderId,
            this.colBuyer,
            this.colShipmentNumber,
            this.colStatus,
            this.colDays});
			this.lvwOrders.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvwOrders.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lvwOrders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lvwOrders.FullRowSelect = true;
			this.lvwOrders.GridLines = true;
			this.lvwOrders.Location = new System.Drawing.Point(0, 0);
			this.lvwOrders.MultiSelect = false;
			this.lvwOrders.Name = "lvwOrders";
			this.lvwOrders.Size = new System.Drawing.Size(784, 416);
			this.lvwOrders.TabIndex = 0;
			this.lvwOrders.UseCompatibleStateImageBehavior = false;
			this.lvwOrders.View = System.Windows.Forms.View.Details;
			// 
			// colIndex
			// 
			this.colIndex.Text = "Index";
			this.colIndex.Width = 50;
			// 
			// colOrderId
			// 
			this.colOrderId.Text = "Order ID";
			this.colOrderId.Width = 120;
			// 
			// colBuyer
			// 
			this.colBuyer.Text = "Buyer";
			this.colBuyer.Width = 100;
			// 
			// colShipmentNumber
			// 
			this.colShipmentNumber.Text = "Shipment Number";
			this.colShipmentNumber.Width = 100;
			// 
			// colStatus
			// 
			this.colStatus.Text = "Status";
			this.colStatus.Width = 320;
			// 
			// colDays
			// 
			this.colDays.Text = "Days";
			this.colDays.Width = 80;
			// 
			// btnLoad
			// 
			this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnLoad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.btnLoad.Location = new System.Drawing.Point(3, 422);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(75, 23);
			this.btnLoad.TabIndex = 2;
			this.btnLoad.Text = "Load...";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// lblInfo
			// 
			this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblInfo.Location = new System.Drawing.Point(684, 427);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(100, 18);
			this.lblInfo.TabIndex = 3;
			this.lblInfo.Text = "0/0";
			this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnReport
			// 
			this.btnReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnReport.Location = new System.Drawing.Point(84, 422);
			this.btnReport.Name = "btnReport";
			this.btnReport.Size = new System.Drawing.Size(75, 23);
			this.btnReport.TabIndex = 4;
			this.btnReport.Text = "Report...";
			this.btnReport.UseVisualStyleBackColor = true;
			this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
			// 
			// lblGreaterThan
			// 
			this.lblGreaterThan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblGreaterThan.AutoSize = true;
			this.lblGreaterThan.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblGreaterThan.ForeColor = System.Drawing.Color.Gray;
			this.lblGreaterThan.Location = new System.Drawing.Point(165, 425);
			this.lblGreaterThan.Name = "lblGreaterThan";
			this.lblGreaterThan.Size = new System.Drawing.Size(107, 18);
			this.lblGreaterThan.TabIndex = 5;
			this.lblGreaterThan.Text = "¡Ý             days";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.numericUpDown1.Location = new System.Drawing.Point(185, 425);
			this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(43, 20);
			this.numericUpDown1.TabIndex = 6;
			this.numericUpDown1.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
			// 
			// OrderDhlStateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 446);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.lblGreaterThan);
			this.Controls.Add(this.btnReport);
			this.Controls.Add(this.lblInfo);
			this.Controls.Add(this.btnLoad);
			this.Controls.Add(this.lvwOrders);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "OrderDhlStateForm";
			this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 30);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "DHL State";
			this.Shown += new System.EventHandler(this.OrderDhlStateForm_Shown);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView lvwOrders;
		private System.Windows.Forms.ColumnHeader colOrderId;
		private System.Windows.Forms.ColumnHeader colShipmentNumber;
		private System.Windows.Forms.ColumnHeader colStatus;
		private System.Windows.Forms.ColumnHeader colBuyer;
		private System.Windows.Forms.ColumnHeader colIndex;
		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.Label lblInfo;
		private System.Windows.Forms.ColumnHeader colDays;
		private System.Windows.Forms.Button btnReport;
		private System.Windows.Forms.Label lblGreaterThan;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
	}
}