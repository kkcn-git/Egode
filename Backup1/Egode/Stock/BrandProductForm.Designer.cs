namespace Egode.Stock
{
	partial class BrandProductForm
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
			this.lblBrand = new System.Windows.Forms.Label();
			this.cboBrands = new System.Windows.Forms.ComboBox();
			this.lblFreeIds = new System.Windows.Forms.Label();
			this.txtFreeIds = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// lblBrand
			// 
			this.lblBrand.AutoSize = true;
			this.lblBrand.Location = new System.Drawing.Point(22, 14);
			this.lblBrand.Name = "lblBrand";
			this.lblBrand.Size = new System.Drawing.Size(43, 13);
			this.lblBrand.TabIndex = 0;
			this.lblBrand.Text = "Brands:";
			// 
			// cboBrands
			// 
			this.cboBrands.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboBrands.FormattingEnabled = true;
			this.cboBrands.Location = new System.Drawing.Point(22, 30);
			this.cboBrands.Name = "cboBrands";
			this.cboBrands.Size = new System.Drawing.Size(220, 21);
			this.cboBrands.TabIndex = 1;
			this.cboBrands.SelectedIndexChanged += new System.EventHandler(this.cboBrands_SelectedIndexChanged);
			// 
			// lblFreeIds
			// 
			this.lblFreeIds.AutoSize = true;
			this.lblFreeIds.Location = new System.Drawing.Point(22, 88);
			this.lblFreeIds.Name = "lblFreeIds";
			this.lblFreeIds.Size = new System.Drawing.Size(48, 13);
			this.lblFreeIds.TabIndex = 2;
			this.lblFreeIds.Text = "Free Ids:";
			// 
			// txtFreeIds
			// 
			this.txtFreeIds.Location = new System.Drawing.Point(25, 104);
			this.txtFreeIds.Multiline = true;
			this.txtFreeIds.Name = "txtFreeIds";
			this.txtFreeIds.Size = new System.Drawing.Size(217, 133);
			this.txtFreeIds.TabIndex = 3;
			// 
			// BrandProductForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(268, 261);
			this.Controls.Add(this.txtFreeIds);
			this.Controls.Add(this.lblFreeIds);
			this.Controls.Add(this.cboBrands);
			this.Controls.Add(this.lblBrand);
			this.Name = "BrandProductForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "BrandProductForm";
			this.Load += new System.EventHandler(this.BrandProductForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblBrand;
		private System.Windows.Forms.ComboBox cboBrands;
		private System.Windows.Forms.Label lblFreeIds;
		private System.Windows.Forms.TextBox txtFreeIds;
	}
}