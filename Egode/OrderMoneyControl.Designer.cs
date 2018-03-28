namespace Egode
{
	partial class OrderMoneyControl
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
			this.lblMoney = new System.Windows.Forms.Label();
			this.lblFreight = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.BackColor = System.Drawing.Color.Transparent;
			this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblTitle.Location = new System.Drawing.Point(21, -2);
			this.lblTitle.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(35, 13);
			this.lblTitle.TabIndex = 3;
			this.lblTitle.Text = "½ð¶î:";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.lblTitle.SizeChanged += new System.EventHandler(this.lblTitle_SizeChanged);
			// 
			// lblMoney
			// 
			this.lblMoney.AutoSize = true;
			this.lblMoney.BackColor = System.Drawing.Color.Transparent;
			this.lblMoney.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMoney.ForeColor = System.Drawing.Color.OrangeRed;
			this.lblMoney.Location = new System.Drawing.Point(56, -2);
			this.lblMoney.Margin = new System.Windows.Forms.Padding(0);
			this.lblMoney.Name = "lblMoney";
			this.lblMoney.Size = new System.Drawing.Size(43, 18);
			this.lblMoney.TabIndex = 4;
			this.lblMoney.Text = "0.00";
			this.lblMoney.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.lblMoney.SizeChanged += new System.EventHandler(this.lblTitle_SizeChanged);
			// 
			// lblFreight
			// 
			this.lblFreight.AutoSize = true;
			this.lblFreight.BackColor = System.Drawing.Color.Transparent;
			this.lblFreight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblFreight.Location = new System.Drawing.Point(93, -2);
			this.lblFreight.Margin = new System.Windows.Forms.Padding(0);
			this.lblFreight.Name = "lblFreight";
			this.lblFreight.Size = new System.Drawing.Size(37, 13);
			this.lblFreight.TabIndex = 5;
			this.lblFreight.Text = "(0.00)";
			this.lblFreight.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.lblFreight.SizeChanged += new System.EventHandler(this.lblTitle_SizeChanged);
			// 
			// OrderMoneyControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.lblMoney);
			this.Controls.Add(this.lblFreight);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "OrderMoneyControl";
			this.Size = new System.Drawing.Size(150, 15);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Label lblMoney;
		private System.Windows.Forms.Label lblFreight;

	}
}
