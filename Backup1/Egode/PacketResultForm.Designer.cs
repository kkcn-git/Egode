namespace Egode
{
	partial class PacketResultForm
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
			this.btnOK = new System.Windows.Forms.Button();
			this.txtResult = new System.Windows.Forms.TextBox();
			this.lblTips = new System.Windows.Forms.Label();
			this.btnUpdateStatus = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(239, 267);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// txtResult
			// 
			this.txtResult.BackColor = System.Drawing.Color.White;
			this.txtResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtResult.Location = new System.Drawing.Point(12, 14);
			this.txtResult.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
			this.txtResult.Multiline = true;
			this.txtResult.Name = "txtResult";
			this.txtResult.ReadOnly = true;
			this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
			this.txtResult.Size = new System.Drawing.Size(530, 216);
			this.txtResult.TabIndex = 2;
			this.txtResult.WordWrap = false;
			// 
			// lblTips
			// 
			this.lblTips.AutoSize = true;
			this.lblTips.ForeColor = System.Drawing.Color.Crimson;
			this.lblTips.Location = new System.Drawing.Point(8, 241);
			this.lblTips.Name = "lblTips";
			this.lblTips.Size = new System.Drawing.Size(390, 13);
			this.lblTips.TabIndex = 3;
			this.lblTips.Text = "核对输出文档, 确认全部正确出单后点此按钮同步订单出单状态到服务器:";
			// 
			// btnUpdateStatus
			// 
			this.btnUpdateStatus.ForeColor = System.Drawing.Color.Crimson;
			this.btnUpdateStatus.Location = new System.Drawing.Point(404, 236);
			this.btnUpdateStatus.Name = "btnUpdateStatus";
			this.btnUpdateStatus.Size = new System.Drawing.Size(75, 23);
			this.btnUpdateStatus.TabIndex = 4;
			this.btnUpdateStatus.Text = "同步";
			this.btnUpdateStatus.UseVisualStyleBackColor = true;
			this.btnUpdateStatus.Click += new System.EventHandler(this.btnUpdateStatus_Click);
			// 
			// PacketResultForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(554, 296);
			this.Controls.Add(this.btnUpdateStatus);
			this.Controls.Add(this.lblTips);
			this.Controls.Add(this.txtResult);
			this.Controls.Add(this.btnOK);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "PacketResultForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "出单完成";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TextBox txtResult;
		private System.Windows.Forms.Label lblTips;
		private System.Windows.Forms.Button btnUpdateStatus;
	}
}