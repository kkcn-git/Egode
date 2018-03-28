namespace Egode
{
	partial class UpdateSellMemoForm
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
			this.lblOriginalMemo = new System.Windows.Forms.Label();
			this.txtOriginalMemo = new System.Windows.Forms.TextBox();
			this.lblAppendMemo = new System.Windows.Forms.Label();
			this.txtAppendMemo = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtPreview = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.pnlMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.lblOriginalMemo);
			this.pnlMain.Controls.Add(this.txtOriginalMemo);
			this.pnlMain.Controls.Add(this.lblAppendMemo);
			this.pnlMain.Controls.Add(this.txtAppendMemo);
			this.pnlMain.Controls.Add(this.label1);
			this.pnlMain.Controls.Add(this.txtPreview);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(2, 2);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(468, 373);
			this.pnlMain.TabIndex = 0;
			// 
			// lblOriginalMemo
			// 
			this.lblOriginalMemo.AutoSize = true;
			this.pnlMain.SetFlowBreak(this.lblOriginalMemo, true);
			this.lblOriginalMemo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblOriginalMemo.Location = new System.Drawing.Point(12, 12);
			this.lblOriginalMemo.Margin = new System.Windows.Forms.Padding(12, 12, 3, 0);
			this.lblOriginalMemo.Name = "lblOriginalMemo";
			this.lblOriginalMemo.Size = new System.Drawing.Size(47, 13);
			this.lblOriginalMemo.TabIndex = 0;
			this.lblOriginalMemo.Text = "原备注:";
			// 
			// txtOriginalMemo
			// 
			this.txtOriginalMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlMain.SetFlowBreak(this.txtOriginalMemo, true);
			this.txtOriginalMemo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtOriginalMemo.Location = new System.Drawing.Point(14, 27);
			this.txtOriginalMemo.Margin = new System.Windows.Forms.Padding(14, 2, 3, 3);
			this.txtOriginalMemo.Multiline = true;
			this.txtOriginalMemo.Name = "txtOriginalMemo";
			this.txtOriginalMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtOriginalMemo.Size = new System.Drawing.Size(439, 100);
			this.txtOriginalMemo.TabIndex = 1;
			this.txtOriginalMemo.TextChanged += new System.EventHandler(this.txtOriginalMemo_TextChanged);
			// 
			// lblAppendMemo
			// 
			this.lblAppendMemo.AutoSize = true;
			this.pnlMain.SetFlowBreak(this.lblAppendMemo, true);
			this.lblAppendMemo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblAppendMemo.Location = new System.Drawing.Point(12, 142);
			this.lblAppendMemo.Margin = new System.Windows.Forms.Padding(12, 12, 3, 0);
			this.lblAppendMemo.Name = "lblAppendMemo";
			this.lblAppendMemo.Size = new System.Drawing.Size(59, 13);
			this.lblAppendMemo.TabIndex = 2;
			this.lblAppendMemo.Text = "追加备注:";
			// 
			// txtAppendMemo
			// 
			this.txtAppendMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlMain.SetFlowBreak(this.txtAppendMemo, true);
			this.txtAppendMemo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtAppendMemo.Location = new System.Drawing.Point(14, 157);
			this.txtAppendMemo.Margin = new System.Windows.Forms.Padding(14, 2, 3, 3);
			this.txtAppendMemo.Multiline = true;
			this.txtAppendMemo.Name = "txtAppendMemo";
			this.txtAppendMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtAppendMemo.Size = new System.Drawing.Size(439, 60);
			this.txtAppendMemo.TabIndex = 3;
			this.txtAppendMemo.TextChanged += new System.EventHandler(this.txtAppendMemo_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.pnlMain.SetFlowBreak(this.label1, true);
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.label1.Location = new System.Drawing.Point(12, 232);
			this.label1.Margin = new System.Windows.Forms.Padding(12, 12, 3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "预览:";
			// 
			// txtPreview
			// 
			this.txtPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtPreview.BackColor = System.Drawing.SystemColors.Control;
			this.txtPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlMain.SetFlowBreak(this.txtPreview, true);
			this.txtPreview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtPreview.Location = new System.Drawing.Point(14, 247);
			this.txtPreview.Margin = new System.Windows.Forms.Padding(14, 2, 3, 3);
			this.txtPreview.Multiline = true;
			this.txtPreview.Name = "txtPreview";
			this.txtPreview.ReadOnly = true;
			this.txtPreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtPreview.Size = new System.Drawing.Size(439, 120);
			this.txtPreview.TabIndex = 5;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Enabled = false;
			this.btnOK.Location = new System.Drawing.Point(299, 381);
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
			this.btnCancel.Location = new System.Drawing.Point(380, 381);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// UpdateSellMemoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(472, 411);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.pnlMain);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "UpdateSellMemoForm";
			this.Padding = new System.Windows.Forms.Padding(2, 2, 2, 36);
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "更新备注";
			this.Shown += new System.EventHandler(this.UpdateSellMemoForm_Shown);
			this.pnlMain.ResumeLayout(false);
			this.pnlMain.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel pnlMain;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblOriginalMemo;
		private System.Windows.Forms.TextBox txtOriginalMemo;
		private System.Windows.Forms.Label lblAppendMemo;
		private System.Windows.Forms.TextBox txtAppendMemo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtPreview;
	}
}