namespace Egode
{
	partial class GetLocalPacketInfoForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetLocalPacketInfoForm));
			this.lvwPdfPacketInfos = new System.Windows.Forms.ListView();
			this.colRecipientNameInPackingList = new System.Windows.Forms.ColumnHeader();
			this.colRecipientName = new System.Windows.Forms.ColumnHeader();
			this.colRecipientPhone = new System.Windows.Forms.ColumnHeader();
			this.colShipmentNumber = new System.Windows.Forms.ColumnHeader();
			this.colWeight = new System.Windows.Forms.ColumnHeader();
			this.colFilename = new System.Windows.Forms.ColumnHeader();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbtnGetLocalPdfPackets = new System.Windows.Forms.ToolStripButton();
			this.tsbtnGetOnlinPacket = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnPackingList = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lvwPdfPacketInfos
			// 
			this.lvwPdfPacketInfos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwPdfPacketInfos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colRecipientNameInPackingList,
            this.colRecipientName,
            this.colRecipientPhone,
            this.colShipmentNumber,
            this.colWeight,
            this.colFilename});
			this.lvwPdfPacketInfos.FullRowSelect = true;
			this.lvwPdfPacketInfos.GridLines = true;
			this.lvwPdfPacketInfos.Location = new System.Drawing.Point(0, 28);
			this.lvwPdfPacketInfos.Name = "lvwPdfPacketInfos";
			this.lvwPdfPacketInfos.Size = new System.Drawing.Size(769, 386);
			this.lvwPdfPacketInfos.TabIndex = 4;
			this.lvwPdfPacketInfos.UseCompatibleStateImageBehavior = false;
			this.lvwPdfPacketInfos.View = System.Windows.Forms.View.Details;
			// 
			// colRecipientNameInPackingList
			// 
			this.colRecipientNameInPackingList.Text = "收货人(发货清单中)";
			this.colRecipientNameInPackingList.Width = 120;
			// 
			// colRecipientName
			// 
			this.colRecipientName.Text = "收货人(包裹单中)";
			this.colRecipientName.Width = 130;
			// 
			// colRecipientPhone
			// 
			this.colRecipientPhone.Text = "收货人电话";
			this.colRecipientPhone.Width = 160;
			// 
			// colShipmentNumber
			// 
			this.colShipmentNumber.Text = "单号";
			this.colShipmentNumber.Width = 100;
			// 
			// colWeight
			// 
			this.colWeight.Text = "重量";
			// 
			// colFilename
			// 
			this.colFilename.Text = "文件名";
			this.colFilename.Width = 160;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(601, 420);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(682, 420);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnGetLocalPdfPackets,
            this.tsbtnGetOnlinPacket,
            this.toolStripSeparator1,
            this.tsbtnPackingList});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(769, 25);
			this.toolStrip1.TabIndex = 7;
			this.toolStrip1.Text = "Local";
			// 
			// tsbtnGetLocalPdfPackets
			// 
			this.tsbtnGetLocalPdfPackets.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnGetLocalPdfPackets.Image")));
			this.tsbtnGetLocalPdfPackets.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnGetLocalPdfPackets.Name = "tsbtnGetLocalPdfPackets";
			this.tsbtnGetLocalPdfPackets.Size = new System.Drawing.Size(96, 22);
			this.tsbtnGetLocalPdfPackets.Text = "导入本地PDF";
			this.tsbtnGetLocalPdfPackets.Click += new System.EventHandler(this.tsbtnGetLocalPdfPackets_Click);
			// 
			// tsbtnGetOnlinPacket
			// 
			this.tsbtnGetOnlinPacket.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnGetOnlinPacket.Image")));
			this.tsbtnGetOnlinPacket.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnGetOnlinPacket.Name = "tsbtnGetOnlinPacket";
			this.tsbtnGetOnlinPacket.Size = new System.Drawing.Size(111, 22);
			this.tsbtnGetOnlinPacket.Text = "导入在线包裹单";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbtnPackingList
			// 
			this.tsbtnPackingList.Enabled = false;
			this.tsbtnPackingList.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnPackingList.Image")));
			this.tsbtnPackingList.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnPackingList.Name = "tsbtnPackingList";
			this.tsbtnPackingList.Size = new System.Drawing.Size(99, 22);
			this.tsbtnPackingList.Text = "导入发货清单";
			this.tsbtnPackingList.Click += new System.EventHandler(this.tsbtnPackingList_Click);
			// 
			// GetLocalPacketInfoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(769, 449);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.lvwPdfPacketInfos);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "GetLocalPacketInfoForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "包裹单信息匹配";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView lvwPdfPacketInfos;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ColumnHeader colRecipientName;
		private System.Windows.Forms.ColumnHeader colRecipientPhone;
		private System.Windows.Forms.ColumnHeader colShipmentNumber;
		private System.Windows.Forms.ColumnHeader colWeight;
		private System.Windows.Forms.ColumnHeader colFilename;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsbtnGetLocalPdfPackets;
		private System.Windows.Forms.ToolStripButton tsbtnGetOnlinPacket;
		private System.Windows.Forms.ColumnHeader colRecipientNameInPackingList;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsbtnPackingList;
	}
}