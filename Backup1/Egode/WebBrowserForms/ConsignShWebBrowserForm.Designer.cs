namespace Egode.WebBrowserForms
{
	partial class ConsignShWebBrowserForm
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
			this.lblBillNumber = new System.Windows.Forms.Label();
			this.lblExpressCompany = new System.Windows.Forms.Label();
			this.txtBillNumber = new System.Windows.Forms.TextBox();
			this.btnCopy = new System.Windows.Forms.Button();
			this.lblInfo = new System.Windows.Forms.Label();
			this.btnSendManually = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblBillNumber
			// 
			this.lblBillNumber.AutoSize = true;
			this.lblBillNumber.Location = new System.Drawing.Point(3, 600);
			this.lblBillNumber.Name = "lblBillNumber";
			this.lblBillNumber.Size = new System.Drawing.Size(34, 13);
			this.lblBillNumber.TabIndex = 1;
			this.lblBillNumber.Text = "单号:";
			// 
			// lblExpressCompany
			// 
			this.lblExpressCompany.AutoSize = true;
			this.lblExpressCompany.ForeColor = System.Drawing.Color.Blue;
			this.lblExpressCompany.Location = new System.Drawing.Point(140, 600);
			this.lblExpressCompany.Name = "lblExpressCompany";
			this.lblExpressCompany.Size = new System.Drawing.Size(21, 13);
			this.lblExpressCompany.TabIndex = 2;
			this.lblExpressCompany.Text = "zto";
			// 
			// txtBillNumber
			// 
			this.txtBillNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtBillNumber.ForeColor = System.Drawing.Color.Blue;
			this.txtBillNumber.Location = new System.Drawing.Point(39, 597);
			this.txtBillNumber.Name = "txtBillNumber";
			this.txtBillNumber.Size = new System.Drawing.Size(100, 21);
			this.txtBillNumber.TabIndex = 3;
			this.txtBillNumber.Text = "000000000000";
			// 
			// btnCopy
			// 
			this.btnCopy.Location = new System.Drawing.Point(174, 593);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(75, 23);
			this.btnCopy.TabIndex = 4;
			this.btnCopy.Text = "复制单号";
			this.btnCopy.UseVisualStyleBackColor = true;
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// lblInfo
			// 
			this.lblInfo.BackColor = System.Drawing.Color.PapayaWhip;
			this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblInfo.ForeColor = System.Drawing.Color.Blue;
			this.lblInfo.Location = new System.Drawing.Point(255, 593);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.lblInfo.Size = new System.Drawing.Size(202, 22);
			this.lblInfo.TabIndex = 5;
			this.lblInfo.Text = "单号000000000000已复制到剪贴板";
			this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblInfo.Visible = false;
			// 
			// btnSendManually
			// 
			this.btnSendManually.BackColor = System.Drawing.Color.Indigo;
			this.btnSendManually.ForeColor = System.Drawing.Color.White;
			this.btnSendManually.Location = new System.Drawing.Point(734, 592);
			this.btnSendManually.Name = "btnSendManually";
			this.btnSendManually.Size = new System.Drawing.Size(75, 23);
			this.btnSendManually.TabIndex = 6;
			this.btnSendManually.Text = "一键发";
			this.btnSendManually.UseVisualStyleBackColor = false;
			this.btnSendManually.Click += new System.EventHandler(this.b_Click);
			// 
			// ConsignShWebBrowserForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(812, 616);
			this.Controls.Add(this.btnSendManually);
			this.Controls.Add(this.lblInfo);
			this.Controls.Add(this.btnCopy);
			this.Controls.Add(this.txtBillNumber);
			this.Controls.Add(this.lblBillNumber);
			this.Controls.Add(this.lblExpressCompany);
			this.KeyPreview = true;
			this.Name = "ConsignShWebBrowserForm";
			this.Text = "ConsignShWebBrowserForm";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConsignShWebBrowserForm_KeyDown);
			this.Controls.SetChildIndex(this.lblExpressCompany, 0);
			this.Controls.SetChildIndex(this.lblBillNumber, 0);
			this.Controls.SetChildIndex(this.txtBillNumber, 0);
			this.Controls.SetChildIndex(this.btnCopy, 0);
			this.Controls.SetChildIndex(this.lblInfo, 0);
			this.Controls.SetChildIndex(this.btnSendManually, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblBillNumber;
		private System.Windows.Forms.Label lblExpressCompany;
		private System.Windows.Forms.TextBox txtBillNumber;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Label lblInfo;
		private System.Windows.Forms.Button btnSendManually;

	}
}