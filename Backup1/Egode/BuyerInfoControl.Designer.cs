namespace Egode
{
	partial class BuyerInfoControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblTitle = new System.Windows.Forms.Label();
			this.lblBuyerAccount = new System.Windows.Forms.LinkLabel();
			this.lblOrderAmount = new System.Windows.Forms.Label();
			this.btnCopy = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblTitle.Location = new System.Drawing.Point(13, 4);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(58, 13);
			this.lblTitle.TabIndex = 3;
			this.lblTitle.Text = "¬Úº“’À∫≈:";
			this.lblTitle.SizeChanged += new System.EventHandler(this.lblTitle_SizeChanged);
			// 
			// lblBuyerAccount
			// 
			this.lblBuyerAccount.AutoSize = true;
			this.lblBuyerAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblBuyerAccount.Location = new System.Drawing.Point(78, 4);
			this.lblBuyerAccount.Name = "lblBuyerAccount";
			this.lblBuyerAccount.Size = new System.Drawing.Size(33, 13);
			this.lblBuyerAccount.TabIndex = 4;
			this.lblBuyerAccount.TabStop = true;
			this.lblBuyerAccount.Text = "buyer";
			this.lblBuyerAccount.Click += new System.EventHandler(this.lblBuyerAccount_Click);
			this.lblBuyerAccount.SizeChanged += new System.EventHandler(this.lblTitle_SizeChanged);
			// 
			// lblOrderAmount
			// 
			this.lblOrderAmount.AutoSize = true;
			this.lblOrderAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblOrderAmount.Location = new System.Drawing.Point(119, 4);
			this.lblOrderAmount.Name = "lblOrderAmount";
			this.lblOrderAmount.Size = new System.Drawing.Size(19, 13);
			this.lblOrderAmount.TabIndex = 5;
			this.lblOrderAmount.Text = "(1)";
			this.lblOrderAmount.SizeChanged += new System.EventHandler(this.lblTitle_SizeChanged);
			// 
			// btnCopy
			// 
			this.btnCopy.Location = new System.Drawing.Point(144, 2);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(18, 18);
			this.btnCopy.TabIndex = 6;
			this.btnCopy.UseVisualStyleBackColor = true;
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// BuyerInfoControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.btnCopy);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.lblBuyerAccount);
			this.Controls.Add(this.lblOrderAmount);
			this.Name = "BuyerInfoControl";
			this.Size = new System.Drawing.Size(201, 20);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.LinkLabel lblBuyerAccount;
		private System.Windows.Forms.Label lblOrderAmount;
		private System.Windows.Forms.Button btnCopy;

	}
}
