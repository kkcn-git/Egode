namespace Egode
{
	partial class RateForm
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
			this.btnGo = new System.Windows.Forms.Button();
			this.btnLoad = new System.Windows.Forms.Button();
			this.chkBuyerRated = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// lvwOrders
			// 
			this.lvwOrders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIndex,
            this.colOrderId,
            this.colBuyer,
            this.colShipmentNumber,
            this.colStatus});
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
			this.lvwOrders.DoubleClick += new System.EventHandler(this.lvwOrders_DoubleClick);
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
			this.colStatus.Width = 370;
			// 
			// btnGo
			// 
			this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnGo.Location = new System.Drawing.Point(0, 422);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(75, 23);
			this.btnGo.TabIndex = 1;
			this.btnGo.Text = "GO";
			this.btnGo.UseVisualStyleBackColor = true;
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// btnLoad
			// 
			this.btnLoad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.btnLoad.Location = new System.Drawing.Point(81, 422);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(75, 23);
			this.btnLoad.TabIndex = 2;
			this.btnLoad.Text = "Load...";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// chkBuyerRated
			// 
			this.chkBuyerRated.AutoSize = true;
			this.chkBuyerRated.Location = new System.Drawing.Point(173, 426);
			this.chkBuyerRated.Name = "chkBuyerRated";
			this.chkBuyerRated.Size = new System.Drawing.Size(74, 17);
			this.chkBuyerRated.TabIndex = 3;
			this.chkBuyerRated.Text = "对方已评";
			this.chkBuyerRated.UseVisualStyleBackColor = true;
			// 
			// RateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 446);
			this.Controls.Add(this.chkBuyerRated);
			this.Controls.Add(this.btnLoad);
			this.Controls.Add(this.btnGo);
			this.Controls.Add(this.lvwOrders);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "RateForm";
			this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 30);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "评价";
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
		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.CheckBox chkBuyerRated;
	}
}