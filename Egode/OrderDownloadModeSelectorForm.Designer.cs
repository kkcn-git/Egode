namespace Egode
{
	partial class OrderDownloadModeSelectorForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderDownloadModeSelectorForm));
			this.grp = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.rdoFull = new System.Windows.Forms.RadioButton();
			this.rdoSpeed = new System.Windows.Forms.RadioButton();
			this.btnOK = new System.Windows.Forms.Button();
			this.grp.SuspendLayout();
			this.SuspendLayout();
			// 
			// grp
			// 
			this.grp.Controls.Add(this.label2);
			this.grp.Controls.Add(this.label1);
			this.grp.Controls.Add(this.rdoFull);
			this.grp.Controls.Add(this.rdoSpeed);
			this.grp.Location = new System.Drawing.Point(9, 4);
			this.grp.Name = "grp";
			this.grp.Size = new System.Drawing.Size(292, 175);
			this.grp.TabIndex = 0;
			this.grp.TabStop = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.Gray;
			this.label2.Location = new System.Drawing.Point(32, 115);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(207, 39);
			this.label2.TabIndex = 3;
			this.label2.Text = "下载所有历史订单.\r\n下载数据量及耗时大幅增加.\r\n完整模式订单仅在每天22:00之后刷新.\r\n";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.Gray;
			this.label1.Location = new System.Drawing.Point(32, 44);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(215, 26);
			this.label1.TabIndex = 2;
			this.label1.Text = "只下载最近180天左右订单.\r\n可能影响对于自助单是否老客户的判断.";
			// 
			// rdoFull
			// 
			this.rdoFull.AutoSize = true;
			this.rdoFull.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rdoFull.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.rdoFull.Location = new System.Drawing.Point(15, 90);
			this.rdoFull.Name = "rdoFull";
			this.rdoFull.Size = new System.Drawing.Size(90, 22);
			this.rdoFull.TabIndex = 1;
			this.rdoFull.TabStop = true;
			this.rdoFull.Text = "完整模式";
			this.rdoFull.UseVisualStyleBackColor = true;
			// 
			// rdoSpeed
			// 
			this.rdoSpeed.AutoSize = true;
			this.rdoSpeed.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rdoSpeed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.rdoSpeed.Location = new System.Drawing.Point(15, 19);
			this.rdoSpeed.Name = "rdoSpeed";
			this.rdoSpeed.Size = new System.Drawing.Size(90, 22);
			this.rdoSpeed.TabIndex = 0;
			this.rdoSpeed.TabStop = true;
			this.rdoSpeed.Text = "高速模式";
			this.rdoSpeed.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(226, 189);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// OrderDownloadModeSelectorForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(309, 217);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.grp);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OrderDownloadModeSelectorForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "订单下载模式";
			this.grp.ResumeLayout(false);
			this.grp.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox grp;
		private System.Windows.Forms.RadioButton rdoFull;
		private System.Windows.Forms.RadioButton rdoSpeed;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnOK;
	}
}