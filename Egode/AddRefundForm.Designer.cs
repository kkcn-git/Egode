namespace Egode
{
	partial class AddRefundForm
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
			this.pnlMain = new System.Windows.Forms.FlowLayoutPanel();
			this.lblShipmentNo = new System.Windows.Forms.Label();
			this.txtShipmentNo = new System.Windows.Forms.TextBox();
			this.lblSrcInfo = new System.Windows.Forms.Label();
			this.txtSrc = new System.Windows.Forms.TextBox();
			this.lblItem = new System.Windows.Forms.Label();
			this.txtItem = new System.Windows.Forms.TextBox();
			this.lblComment = new System.Windows.Forms.Label();
			this.txtComment = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.pnlMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.lblShipmentNo);
			this.pnlMain.Controls.Add(this.txtShipmentNo);
			this.pnlMain.Controls.Add(this.lblSrcInfo);
			this.pnlMain.Controls.Add(this.txtSrc);
			this.pnlMain.Controls.Add(this.lblItem);
			this.pnlMain.Controls.Add(this.txtItem);
			this.pnlMain.Controls.Add(this.lblComment);
			this.pnlMain.Controls.Add(this.txtComment);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(6, 6);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(327, 121);
			this.pnlMain.TabIndex = 0;
			// 
			// lblShipmentNo
			// 
			this.lblShipmentNo.AutoSize = true;
			this.lblShipmentNo.Location = new System.Drawing.Point(0, 6);
			this.lblShipmentNo.Margin = new System.Windows.Forms.Padding(0, 6, 12, 0);
			this.lblShipmentNo.Name = "lblShipmentNo";
			this.lblShipmentNo.Size = new System.Drawing.Size(47, 13);
			this.lblShipmentNo.TabIndex = 0;
			this.lblShipmentNo.Text = "运单号:";
			// 
			// txtShipmentNo
			// 
			this.pnlMain.SetFlowBreak(this.txtShipmentNo, true);
			this.txtShipmentNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtShipmentNo.Location = new System.Drawing.Point(62, 0);
			this.txtShipmentNo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 6);
			this.txtShipmentNo.Name = "txtShipmentNo";
			this.txtShipmentNo.Size = new System.Drawing.Size(260, 21);
			this.txtShipmentNo.TabIndex = 1;
			// 
			// lblSrcInfo
			// 
			this.lblSrcInfo.AutoSize = true;
			this.lblSrcInfo.Location = new System.Drawing.Point(0, 33);
			this.lblSrcInfo.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
			this.lblSrcInfo.Name = "lblSrcInfo";
			this.lblSrcInfo.Size = new System.Drawing.Size(59, 13);
			this.lblSrcInfo.TabIndex = 2;
			this.lblSrcInfo.Text = "来源信息:";
			// 
			// txtSrc
			// 
			this.pnlMain.SetFlowBreak(this.txtSrc, true);
			this.txtSrc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtSrc.Location = new System.Drawing.Point(62, 30);
			this.txtSrc.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
			this.txtSrc.Name = "txtSrc";
			this.txtSrc.Size = new System.Drawing.Size(260, 21);
			this.txtSrc.TabIndex = 3;
			// 
			// lblItem
			// 
			this.lblItem.AutoSize = true;
			this.lblItem.Location = new System.Drawing.Point(0, 63);
			this.lblItem.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
			this.lblItem.Name = "lblItem";
			this.lblItem.Size = new System.Drawing.Size(59, 13);
			this.lblItem.TabIndex = 4;
			this.lblItem.Text = "退货物品:";
			// 
			// txtItem
			// 
			this.pnlMain.SetFlowBreak(this.txtItem, true);
			this.txtItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtItem.Location = new System.Drawing.Point(62, 60);
			this.txtItem.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
			this.txtItem.Name = "txtItem";
			this.txtItem.Size = new System.Drawing.Size(260, 21);
			this.txtItem.TabIndex = 5;
			// 
			// lblComment
			// 
			this.lblComment.AutoSize = true;
			this.lblComment.Location = new System.Drawing.Point(0, 93);
			this.lblComment.Margin = new System.Windows.Forms.Padding(0, 6, 24, 0);
			this.lblComment.Name = "lblComment";
			this.lblComment.Size = new System.Drawing.Size(35, 13);
			this.lblComment.TabIndex = 6;
			this.lblComment.Text = "备注:";
			// 
			// txtComment
			// 
			this.txtComment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtComment.Location = new System.Drawing.Point(62, 90);
			this.txtComment.Name = "txtComment";
			this.txtComment.Size = new System.Drawing.Size(260, 21);
			this.txtComment.TabIndex = 7;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(172, 130);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(253, 130);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// AddRefundForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(339, 157);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.pnlMain);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddRefundForm";
			this.Padding = new System.Windows.Forms.Padding(6, 6, 6, 30);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "添加退货记录";
			this.pnlMain.ResumeLayout(false);
			this.pnlMain.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel pnlMain;
		private System.Windows.Forms.Label lblShipmentNo;
		private System.Windows.Forms.TextBox txtShipmentNo;
		private System.Windows.Forms.Label lblSrcInfo;
		private System.Windows.Forms.TextBox txtSrc;
		private System.Windows.Forms.Label lblItem;
		private System.Windows.Forms.TextBox txtItem;
		private System.Windows.Forms.Label lblComment;
		private System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
	}
}