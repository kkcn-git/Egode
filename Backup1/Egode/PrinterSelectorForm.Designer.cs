namespace Egode
{
	partial class PrinterSelectorForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrinterSelectorForm));
			this.lblYto = new System.Windows.Forms.Label();
			this.cboYtoPrinter = new System.Windows.Forms.ComboBox();
			this.lblSfNew = new System.Windows.Forms.Label();
			this.cboSfNewPrinter = new System.Windows.Forms.ComboBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.cboSfPrinter = new System.Windows.Forms.ComboBox();
			this.lblSf = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblYto
			// 
			this.lblYto.AutoSize = true;
			this.lblYto.Location = new System.Drawing.Point(13, 13);
			this.lblYto.Name = "lblYto";
			this.lblYto.Size = new System.Drawing.Size(71, 13);
			this.lblYto.TabIndex = 0;
			this.lblYto.Text = "圆通打印机:";
			// 
			// cboYtoPrinter
			// 
			this.cboYtoPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboYtoPrinter.FormattingEnabled = true;
			this.cboYtoPrinter.Location = new System.Drawing.Point(14, 31);
			this.cboYtoPrinter.Name = "cboYtoPrinter";
			this.cboYtoPrinter.Size = new System.Drawing.Size(291, 21);
			this.cboYtoPrinter.TabIndex = 1;
			// 
			// lblSfNew
			// 
			this.lblSfNew.AutoSize = true;
			this.lblSfNew.Location = new System.Drawing.Point(11, 135);
			this.lblSfNew.Name = "lblSfNew";
			this.lblSfNew.Size = new System.Drawing.Size(95, 13);
			this.lblSfNew.TabIndex = 2;
			this.lblSfNew.Text = "顺丰标签打印机:";
			// 
			// cboSfNewPrinter
			// 
			this.cboSfNewPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSfNewPrinter.FormattingEnabled = true;
			this.cboSfNewPrinter.Location = new System.Drawing.Point(11, 151);
			this.cboSfNewPrinter.Name = "cboSfNewPrinter";
			this.cboSfNewPrinter.Size = new System.Drawing.Size(291, 21);
			this.cboSfNewPrinter.TabIndex = 3;
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(227, 200);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// cboSfPrinter
			// 
			this.cboSfPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSfPrinter.FormattingEnabled = true;
			this.cboSfPrinter.Location = new System.Drawing.Point(12, 91);
			this.cboSfPrinter.Name = "cboSfPrinter";
			this.cboSfPrinter.Size = new System.Drawing.Size(291, 21);
			this.cboSfPrinter.TabIndex = 2;
			// 
			// lblSf
			// 
			this.lblSf.AutoSize = true;
			this.lblSf.Location = new System.Drawing.Point(11, 73);
			this.lblSf.Name = "lblSf";
			this.lblSf.Size = new System.Drawing.Size(71, 13);
			this.lblSf.TabIndex = 6;
			this.lblSf.Text = "顺丰打印机:";
			// 
			// PrinterSelectorForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(318, 232);
			this.Controls.Add(this.cboSfPrinter);
			this.Controls.Add(this.lblSf);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.cboSfNewPrinter);
			this.Controls.Add(this.lblSfNew);
			this.Controls.Add(this.cboYtoPrinter);
			this.Controls.Add(this.lblYto);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PrinterSelectorForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Printer";
			this.Load += new System.EventHandler(this.PrinterSelectorForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblYto;
		private System.Windows.Forms.ComboBox cboYtoPrinter;
		private System.Windows.Forms.Label lblSfNew;
		private System.Windows.Forms.ComboBox cboSfNewPrinter;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.ComboBox cboSfPrinter;
		private System.Windows.Forms.Label lblSf;
	}
}