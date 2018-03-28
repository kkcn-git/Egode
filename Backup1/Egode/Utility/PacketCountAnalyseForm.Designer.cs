namespace Egode.Utility
{
	partial class PacketCountAnalyseForm
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
			this.txtSource = new System.Windows.Forms.TextBox();
			this.lblResult = new System.Windows.Forms.Label();
			this.txtResult = new System.Windows.Forms.TextBox();
			this.btnGo = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtSource
			// 
			this.txtSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSource.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSource.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtSource.Location = new System.Drawing.Point(0, 0);
			this.txtSource.Multiline = true;
			this.txtSource.Name = "txtSource";
			this.txtSource.Size = new System.Drawing.Size(619, 260);
			this.txtSource.TabIndex = 0;
			// 
			// lblResult
			// 
			this.lblResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblResult.AutoSize = true;
			this.lblResult.Location = new System.Drawing.Point(0, 270);
			this.lblResult.Name = "lblResult";
			this.lblResult.Size = new System.Drawing.Size(41, 13);
			this.lblResult.TabIndex = 1;
			this.lblResult.Text = "Result:";
			// 
			// txtResult
			// 
			this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtResult.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtResult.Location = new System.Drawing.Point(40, 265);
			this.txtResult.Name = "txtResult";
			this.txtResult.Size = new System.Drawing.Size(542, 29);
			this.txtResult.TabIndex = 3;
			// 
			// btnGo
			// 
			this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGo.Location = new System.Drawing.Point(585, 265);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(34, 29);
			this.btnGo.TabIndex = 4;
			this.btnGo.Text = "Go";
			this.btnGo.UseVisualStyleBackColor = true;
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// PacketCountAnalyseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(624, 299);
			this.Controls.Add(this.btnGo);
			this.Controls.Add(this.txtResult);
			this.Controls.Add(this.lblResult);
			this.Controls.Add(this.txtSource);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "PacketCountAnalyseForm";
			this.Text = "PacketCountAnalyseForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtSource;
		private System.Windows.Forms.Label lblResult;
		private System.Windows.Forms.TextBox txtResult;
		private System.Windows.Forms.Button btnGo;
	}
}