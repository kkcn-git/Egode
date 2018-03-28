namespace Egode
{
	partial class PacketInfoForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PacketInfoForm));
			this.lvwPdfPacketInfos = new System.Windows.Forms.ListView();
			this.colIndex = new System.Windows.Forms.ColumnHeader();
			this.colRecipientNameInPackingList = new System.Windows.Forms.ColumnHeader();
			this.colRecipientName = new System.Windows.Forms.ColumnHeader();
			this.colRecipientPhone = new System.Windows.Forms.ColumnHeader();
			this.colShipmentNumber = new System.Windows.Forms.ColumnHeader();
			this.colWeight = new System.Windows.Forms.ColumnHeader();
			this.colStatus = new System.Windows.Forms.ColumnHeader();
			this.colAddress = new System.Windows.Forms.ColumnHeader();
			this.colFilename = new System.Windows.Forms.ColumnHeader();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbtnGetLocalPdfPackets = new System.Windows.Forms.ToolStripButton();
			this.tsbtnGetOnlinPacket = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnPackingList = new System.Windows.Forms.ToolStripButton();
			this.pnlDetails = new System.Windows.Forms.FlowLayoutPanel();
			this.lblRecipientNameInPackingList = new System.Windows.Forms.Label();
			this.txtRecipientNameInPackingList = new System.Windows.Forms.TextBox();
			this.btnSearchRecipientNameFromPackingList = new System.Windows.Forms.Button();
			this.lblRecipientName = new System.Windows.Forms.Label();
			this.txtRecipientName = new System.Windows.Forms.TextBox();
			this.btnSearchRecipientName = new System.Windows.Forms.Button();
			this.lblReciverPhone = new System.Windows.Forms.Label();
			this.txtReceiverPhone = new System.Windows.Forms.TextBox();
			this.btnSearchReceiverPhone = new System.Windows.Forms.Button();
			this.lblShipmentNumber = new System.Windows.Forms.Label();
			this.txtShipmentNumber = new System.Windows.Forms.TextBox();
			this.btnSearchShipmentNumber = new System.Windows.Forms.Button();
			this.lblAddress = new System.Windows.Forms.Label();
			this.txtAddress = new System.Windows.Forms.TextBox();
			this.btnSearchAddress = new System.Windows.Forms.Button();
			this.lblPdfFilename = new System.Windows.Forms.Label();
			this.lnklblFilename = new System.Windows.Forms.LinkLabel();
			this.lblClipboardPrompt = new System.Windows.Forms.Label();
			this.colPacketType = new System.Windows.Forms.ColumnHeader();
			this.toolStrip1.SuspendLayout();
			this.pnlDetails.SuspendLayout();
			this.SuspendLayout();
			// 
			// lvwPdfPacketInfos
			// 
			this.lvwPdfPacketInfos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwPdfPacketInfos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIndex,
            this.colPacketType,
            this.colRecipientNameInPackingList,
            this.colRecipientName,
            this.colRecipientPhone,
            this.colShipmentNumber,
            this.colWeight,
            this.colStatus,
            this.colAddress,
            this.colFilename});
			this.lvwPdfPacketInfos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.lvwPdfPacketInfos.FullRowSelect = true;
			this.lvwPdfPacketInfos.GridLines = true;
			this.lvwPdfPacketInfos.HideSelection = false;
			this.lvwPdfPacketInfos.Location = new System.Drawing.Point(0, 28);
			this.lvwPdfPacketInfos.MultiSelect = false;
			this.lvwPdfPacketInfos.Name = "lvwPdfPacketInfos";
			this.lvwPdfPacketInfos.Size = new System.Drawing.Size(769, 321);
			this.lvwPdfPacketInfos.TabIndex = 4;
			this.lvwPdfPacketInfos.UseCompatibleStateImageBehavior = false;
			this.lvwPdfPacketInfos.View = System.Windows.Forms.View.Details;
			this.lvwPdfPacketInfos.SelectedIndexChanged += new System.EventHandler(this.lvwPdfPacketInfos_SelectedIndexChanged);
			this.lvwPdfPacketInfos.DoubleClick += new System.EventHandler(this.lvwPdfPacketInfos_DoubleClick);
			// 
			// colIndex
			// 
			this.colIndex.Text = "Index";
			this.colIndex.Width = 40;
			// 
			// colRecipientNameInPackingList
			// 
			this.colRecipientNameInPackingList.Text = "收货人(发货清单中)";
			this.colRecipientNameInPackingList.Width = 120;
			// 
			// colRecipientName
			// 
			this.colRecipientName.Text = "收货人(包裹单中)";
			this.colRecipientName.Width = 105;
			// 
			// colRecipientPhone
			// 
			this.colRecipientPhone.Text = "收货人电话";
			this.colRecipientPhone.Width = 80;
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
			// colStatus
			// 
			this.colStatus.Text = "状态";
			this.colStatus.Width = 80;
			// 
			// colAddress
			// 
			this.colAddress.Text = "地址";
			// 
			// colFilename
			// 
			this.colFilename.Text = "文件名";
			this.colFilename.Width = 160;
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
			this.tsbtnGetOnlinPacket.Click += new System.EventHandler(this.tsbtnGetOnlinPacket_Click);
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
			// pnlDetails
			// 
			this.pnlDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlDetails.BackColor = System.Drawing.Color.White;
			this.pnlDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlDetails.Controls.Add(this.lblRecipientNameInPackingList);
			this.pnlDetails.Controls.Add(this.txtRecipientNameInPackingList);
			this.pnlDetails.Controls.Add(this.btnSearchRecipientNameFromPackingList);
			this.pnlDetails.Controls.Add(this.lblRecipientName);
			this.pnlDetails.Controls.Add(this.txtRecipientName);
			this.pnlDetails.Controls.Add(this.btnSearchRecipientName);
			this.pnlDetails.Controls.Add(this.lblReciverPhone);
			this.pnlDetails.Controls.Add(this.txtReceiverPhone);
			this.pnlDetails.Controls.Add(this.btnSearchReceiverPhone);
			this.pnlDetails.Controls.Add(this.lblShipmentNumber);
			this.pnlDetails.Controls.Add(this.txtShipmentNumber);
			this.pnlDetails.Controls.Add(this.btnSearchShipmentNumber);
			this.pnlDetails.Controls.Add(this.lblAddress);
			this.pnlDetails.Controls.Add(this.txtAddress);
			this.pnlDetails.Controls.Add(this.btnSearchAddress);
			this.pnlDetails.Controls.Add(this.lblPdfFilename);
			this.pnlDetails.Controls.Add(this.lnklblFilename);
			this.pnlDetails.Location = new System.Drawing.Point(0, 355);
			this.pnlDetails.Name = "pnlDetails";
			this.pnlDetails.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
			this.pnlDetails.Size = new System.Drawing.Size(769, 109);
			this.pnlDetails.TabIndex = 8;
			// 
			// lblRecipientNameInPackingList
			// 
			this.lblRecipientNameInPackingList.AutoSize = true;
			this.lblRecipientNameInPackingList.Location = new System.Drawing.Point(3, 8);
			this.lblRecipientNameInPackingList.Margin = new System.Windows.Forms.Padding(3, 6, 0, 0);
			this.lblRecipientNameInPackingList.Name = "lblRecipientNameInPackingList";
			this.lblRecipientNameInPackingList.Size = new System.Drawing.Size(115, 13);
			this.lblRecipientNameInPackingList.TabIndex = 0;
			this.lblRecipientNameInPackingList.Text = "收货人(发货清单中):";
			// 
			// txtRecipientNameInPackingList
			// 
			this.txtRecipientNameInPackingList.BackColor = System.Drawing.Color.LightBlue;
			this.txtRecipientNameInPackingList.Location = new System.Drawing.Point(118, 5);
			this.txtRecipientNameInPackingList.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.txtRecipientNameInPackingList.Name = "txtRecipientNameInPackingList";
			this.txtRecipientNameInPackingList.ReadOnly = true;
			this.txtRecipientNameInPackingList.Size = new System.Drawing.Size(120, 21);
			this.txtRecipientNameInPackingList.TabIndex = 1;
			// 
			// btnSearchRecipientNameFromPackingList
			// 
			this.pnlDetails.SetFlowBreak(this.btnSearchRecipientNameFromPackingList, true);
			this.btnSearchRecipientNameFromPackingList.Location = new System.Drawing.Point(238, 5);
			this.btnSearchRecipientNameFromPackingList.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.btnSearchRecipientNameFromPackingList.Name = "btnSearchRecipientNameFromPackingList";
			this.btnSearchRecipientNameFromPackingList.Size = new System.Drawing.Size(26, 20);
			this.btnSearchRecipientNameFromPackingList.TabIndex = 2;
			this.btnSearchRecipientNameFromPackingList.UseVisualStyleBackColor = true;
			this.btnSearchRecipientNameFromPackingList.Click += new System.EventHandler(this.btnSearchRecipientNameFromPackingList_Click);
			// 
			// lblRecipientName
			// 
			this.lblRecipientName.AutoSize = true;
			this.lblRecipientName.Location = new System.Drawing.Point(3, 35);
			this.lblRecipientName.Margin = new System.Windows.Forms.Padding(3, 6, 0, 0);
			this.lblRecipientName.Name = "lblRecipientName";
			this.lblRecipientName.Size = new System.Drawing.Size(115, 13);
			this.lblRecipientName.TabIndex = 3;
			this.lblRecipientName.Text = "收货人(包裹单信息):";
			// 
			// txtRecipientName
			// 
			this.txtRecipientName.Location = new System.Drawing.Point(118, 32);
			this.txtRecipientName.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.txtRecipientName.Name = "txtRecipientName";
			this.txtRecipientName.ReadOnly = true;
			this.txtRecipientName.Size = new System.Drawing.Size(120, 21);
			this.txtRecipientName.TabIndex = 4;
			// 
			// btnSearchRecipientName
			// 
			this.btnSearchRecipientName.Location = new System.Drawing.Point(238, 32);
			this.btnSearchRecipientName.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.btnSearchRecipientName.Name = "btnSearchRecipientName";
			this.btnSearchRecipientName.Size = new System.Drawing.Size(26, 20);
			this.btnSearchRecipientName.TabIndex = 5;
			this.btnSearchRecipientName.UseVisualStyleBackColor = true;
			this.btnSearchRecipientName.Click += new System.EventHandler(this.btnSearchRecipientName_Click);
			// 
			// lblReciverPhone
			// 
			this.lblReciverPhone.AutoSize = true;
			this.lblReciverPhone.Location = new System.Drawing.Point(299, 35);
			this.lblReciverPhone.Margin = new System.Windows.Forms.Padding(32, 6, 0, 0);
			this.lblReciverPhone.Name = "lblReciverPhone";
			this.lblReciverPhone.Size = new System.Drawing.Size(71, 13);
			this.lblReciverPhone.TabIndex = 6;
			this.lblReciverPhone.Text = "收货人电话:";
			// 
			// txtReceiverPhone
			// 
			this.txtReceiverPhone.Location = new System.Drawing.Point(370, 32);
			this.txtReceiverPhone.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.txtReceiverPhone.Name = "txtReceiverPhone";
			this.txtReceiverPhone.ReadOnly = true;
			this.txtReceiverPhone.Size = new System.Drawing.Size(120, 21);
			this.txtReceiverPhone.TabIndex = 7;
			// 
			// btnSearchReceiverPhone
			// 
			this.btnSearchReceiverPhone.Location = new System.Drawing.Point(490, 32);
			this.btnSearchReceiverPhone.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.btnSearchReceiverPhone.Name = "btnSearchReceiverPhone";
			this.btnSearchReceiverPhone.Size = new System.Drawing.Size(26, 20);
			this.btnSearchReceiverPhone.TabIndex = 8;
			this.btnSearchReceiverPhone.UseVisualStyleBackColor = true;
			this.btnSearchReceiverPhone.Click += new System.EventHandler(this.btnSearchReceiverPhone_Click);
			// 
			// lblShipmentNumber
			// 
			this.lblShipmentNumber.AutoSize = true;
			this.lblShipmentNumber.Location = new System.Drawing.Point(551, 35);
			this.lblShipmentNumber.Margin = new System.Windows.Forms.Padding(32, 6, 0, 0);
			this.lblShipmentNumber.Name = "lblShipmentNumber";
			this.lblShipmentNumber.Size = new System.Drawing.Size(35, 13);
			this.lblShipmentNumber.TabIndex = 9;
			this.lblShipmentNumber.Text = "单号:";
			this.lblShipmentNumber.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// txtShipmentNumber
			// 
			this.txtShipmentNumber.Location = new System.Drawing.Point(586, 32);
			this.txtShipmentNumber.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.txtShipmentNumber.Name = "txtShipmentNumber";
			this.txtShipmentNumber.ReadOnly = true;
			this.txtShipmentNumber.Size = new System.Drawing.Size(120, 21);
			this.txtShipmentNumber.TabIndex = 10;
			// 
			// btnSearchShipmentNumber
			// 
			this.pnlDetails.SetFlowBreak(this.btnSearchShipmentNumber, true);
			this.btnSearchShipmentNumber.Location = new System.Drawing.Point(706, 32);
			this.btnSearchShipmentNumber.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.btnSearchShipmentNumber.Name = "btnSearchShipmentNumber";
			this.btnSearchShipmentNumber.Size = new System.Drawing.Size(26, 20);
			this.btnSearchShipmentNumber.TabIndex = 11;
			this.btnSearchShipmentNumber.UseVisualStyleBackColor = true;
			this.btnSearchShipmentNumber.Click += new System.EventHandler(this.btnSearchShipmentNumber_Click);
			// 
			// lblAddress
			// 
			this.lblAddress.AutoSize = true;
			this.lblAddress.Location = new System.Drawing.Point(3, 62);
			this.lblAddress.Margin = new System.Windows.Forms.Padding(3, 6, 5, 0);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(107, 13);
			this.lblAddress.TabIndex = 16;
			this.lblAddress.Text = "包裹单中地址信息:";
			// 
			// txtAddress
			// 
			this.txtAddress.Location = new System.Drawing.Point(118, 59);
			this.txtAddress.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new System.Drawing.Size(588, 21);
			this.txtAddress.TabIndex = 17;
			// 
			// btnSearchAddress
			// 
			this.pnlDetails.SetFlowBreak(this.btnSearchAddress, true);
			this.btnSearchAddress.Location = new System.Drawing.Point(706, 59);
			this.btnSearchAddress.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.btnSearchAddress.Name = "btnSearchAddress";
			this.btnSearchAddress.Size = new System.Drawing.Size(26, 20);
			this.btnSearchAddress.TabIndex = 18;
			this.btnSearchAddress.UseVisualStyleBackColor = true;
			this.btnSearchAddress.Click += new System.EventHandler(this.btnSearchAddress_Click);
			// 
			// lblPdfFilename
			// 
			this.lblPdfFilename.AutoSize = true;
			this.lblPdfFilename.Location = new System.Drawing.Point(3, 89);
			this.lblPdfFilename.Margin = new System.Windows.Forms.Padding(3, 6, 0, 0);
			this.lblPdfFilename.Name = "lblPdfFilename";
			this.lblPdfFilename.Size = new System.Drawing.Size(66, 13);
			this.lblPdfFilename.TabIndex = 12;
			this.lblPdfFilename.Text = "PDF文件名:";
			// 
			// lnklblFilename
			// 
			this.lnklblFilename.AutoSize = true;
			this.lnklblFilename.Location = new System.Drawing.Point(69, 89);
			this.lnklblFilename.Margin = new System.Windows.Forms.Padding(0, 6, 3, 0);
			this.lnklblFilename.Name = "lnklblFilename";
			this.lnklblFilename.Size = new System.Drawing.Size(63, 13);
			this.lnklblFilename.TabIndex = 15;
			this.lnklblFilename.TabStop = true;
			this.lnklblFilename.Text = "<filename>";
			this.lnklblFilename.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnklblFilename_LinkClicked);
			// 
			// lblClipboardPrompt
			// 
			this.lblClipboardPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblClipboardPrompt.AutoSize = true;
			this.lblClipboardPrompt.BackColor = System.Drawing.Color.Bisque;
			this.lblClipboardPrompt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblClipboardPrompt.Location = new System.Drawing.Point(632, 366);
			this.lblClipboardPrompt.Margin = new System.Windows.Forms.Padding(500, 6, 3, 0);
			this.lblClipboardPrompt.Name = "lblClipboardPrompt";
			this.lblClipboardPrompt.Padding = new System.Windows.Forms.Padding(6, 1, 6, 1);
			this.lblClipboardPrompt.Size = new System.Drawing.Size(49, 17);
			this.lblClipboardPrompt.TabIndex = 16;
			this.lblClipboardPrompt.Text = "label1";
			this.lblClipboardPrompt.Visible = false;
			this.lblClipboardPrompt.SizeChanged += new System.EventHandler(this.lblClipboardPrompt_SizeChanged);
			// 
			// colPacketType
			// 
			this.colPacketType.Text = "包裹种类";
			// 
			// PacketInfoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(769, 467);
			this.Controls.Add(this.lblClipboardPrompt);
			this.Controls.Add(this.pnlDetails);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.lvwPdfPacketInfos);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "PacketInfoForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "包裹单信息匹配";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.pnlDetails.ResumeLayout(false);
			this.pnlDetails.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView lvwPdfPacketInfos;
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
		private System.Windows.Forms.ColumnHeader colIndex;
		private System.Windows.Forms.FlowLayoutPanel pnlDetails;
		private System.Windows.Forms.Label lblRecipientNameInPackingList;
		private System.Windows.Forms.TextBox txtRecipientNameInPackingList;
		private System.Windows.Forms.Button btnSearchRecipientNameFromPackingList;
		private System.Windows.Forms.Label lblRecipientName;
		private System.Windows.Forms.TextBox txtRecipientName;
		private System.Windows.Forms.Button btnSearchRecipientName;
		private System.Windows.Forms.Label lblReciverPhone;
		private System.Windows.Forms.TextBox txtReceiverPhone;
		private System.Windows.Forms.Button btnSearchReceiverPhone;
		private System.Windows.Forms.Label lblShipmentNumber;
		private System.Windows.Forms.TextBox txtShipmentNumber;
		private System.Windows.Forms.Button btnSearchShipmentNumber;
		private System.Windows.Forms.Label lblPdfFilename;
		private System.Windows.Forms.LinkLabel lnklblFilename;
		private System.Windows.Forms.Label lblClipboardPrompt;
		private System.Windows.Forms.ColumnHeader colStatus;
		private System.Windows.Forms.ColumnHeader colAddress;
		private System.Windows.Forms.Label lblAddress;
		private System.Windows.Forms.TextBox txtAddress;
		private System.Windows.Forms.Button btnSearchAddress;
		private System.Windows.Forms.ColumnHeader colPacketType;
	}
}