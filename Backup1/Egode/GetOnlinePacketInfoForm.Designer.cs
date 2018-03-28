namespace Egode
{
	partial class GetOnlinePacketInfoForm
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
			this.wb = new System.Windows.Forms.WebBrowser();
			this.btnOK = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.wbPacketDetails = new System.Windows.Forms.WebBrowser();
			this.SuspendLayout();
			// 
			// wb
			// 
			this.wb.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wb.Location = new System.Drawing.Point(0, 0);
			this.wb.MinimumSize = new System.Drawing.Size(20, 20);
			this.wb.Name = "wb";
			this.wb.ScriptErrorsSuppressed = true;
			this.wb.Size = new System.Drawing.Size(1034, 652);
			this.wb.TabIndex = 0;
			this.wb.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wb_DocumentCompleted);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.BackColor = System.Drawing.SystemColors.HotTrack;
			this.btnOK.Enabled = false;
			this.btnOK.ForeColor = System.Drawing.Color.White;
			this.btnOK.Location = new System.Drawing.Point(773, 657);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(245, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "获取当前页面上所有包裹单信息";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblStatus.AutoSize = true;
			this.lblStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStatus.Location = new System.Drawing.Point(3, 662);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(35, 13);
			this.lblStatus.TabIndex = 2;
			this.lblStatus.Text = "label1";
			// 
			// wbPacketDetails
			// 
			this.wbPacketDetails.Location = new System.Drawing.Point(376, 411);
			this.wbPacketDetails.MinimumSize = new System.Drawing.Size(20, 20);
			this.wbPacketDetails.Name = "wbPacketDetails";
			this.wbPacketDetails.ScriptErrorsSuppressed = true;
			this.wbPacketDetails.Size = new System.Drawing.Size(250, 250);
			this.wbPacketDetails.TabIndex = 3;
			this.wbPacketDetails.Visible = false;
			this.wbPacketDetails.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbPacketDetails_DocumentCompleted);
			// 
			// GetOnlinePacketInfoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1034, 682);
			this.Controls.Add(this.wbPacketDetails);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.wb);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.Name = "GetOnlinePacketInfoForm";
			this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 30);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "获取在线包裹单信息";
			this.Load += new System.EventHandler(this.GetOnlinePacketInfoForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.WebBrowser wb;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.WebBrowser wbPacketDetails;
	}
}