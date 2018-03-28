namespace Egode
{
	partial class ShopSelectorForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShopSelectorForm));
			this.lblShops = new System.Windows.Forms.Label();
			this.cboShops = new System.Windows.Forms.ComboBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.lblWw = new System.Windows.Forms.Label();
			this.rdoWw = new System.Windows.Forms.RadioButton();
			this.grpContainer = new System.Windows.Forms.GroupBox();
			this.grpContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblShops
			// 
			this.lblShops.AutoSize = true;
			this.lblShops.Location = new System.Drawing.Point(15, 20);
			this.lblShops.Name = "lblShops";
			this.lblShops.Size = new System.Drawing.Size(40, 13);
			this.lblShops.TabIndex = 0;
			this.lblShops.Text = "Shops:";
			// 
			// cboShops
			// 
			this.cboShops.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboShops.FormattingEnabled = true;
			this.cboShops.Location = new System.Drawing.Point(15, 36);
			this.cboShops.Name = "cboShops";
			this.cboShops.Size = new System.Drawing.Size(160, 21);
			this.cboShops.TabIndex = 0;
			this.cboShops.SelectedIndexChanged += new System.EventHandler(this.cboShops_SelectedIndexChanged);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(130, 144);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// lblWw
			// 
			this.lblWw.AutoSize = true;
			this.lblWw.ForeColor = System.Drawing.Color.Gray;
			this.lblWw.Location = new System.Drawing.Point(15, 80);
			this.lblWw.Name = "lblWw";
			this.lblWw.Size = new System.Drawing.Size(59, 13);
			this.lblWw.TabIndex = 3;
			this.lblWw.Text = "Õ˙Õ˙’À∫≈:";
			// 
			// rdoWw
			// 
			this.rdoWw.AutoSize = true;
			this.rdoWw.Checked = true;
			this.rdoWw.ForeColor = System.Drawing.Color.Gray;
			this.rdoWw.Location = new System.Drawing.Point(15, 98);
			this.rdoWw.Name = "rdoWw";
			this.rdoWw.Size = new System.Drawing.Size(14, 13);
			this.rdoWw.TabIndex = 1;
			this.rdoWw.TabStop = true;
			this.rdoWw.UseVisualStyleBackColor = true;
			// 
			// grpContainer
			// 
			this.grpContainer.Controls.Add(this.rdoWw);
			this.grpContainer.Controls.Add(this.lblShops);
			this.grpContainer.Controls.Add(this.lblWw);
			this.grpContainer.Controls.Add(this.cboShops);
			this.grpContainer.Location = new System.Drawing.Point(12, 3);
			this.grpContainer.Name = "grpContainer";
			this.grpContainer.Size = new System.Drawing.Size(193, 130);
			this.grpContainer.TabIndex = 5;
			this.grpContainer.TabStop = false;
			// 
			// ShopSelectorForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(220, 172);
			this.Controls.Add(this.grpContainer);
			this.Controls.Add(this.btnOK);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ShopSelectorForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Shop";
			this.Load += new System.EventHandler(this.ShopSelectorForm_Load);
			this.Shown += new System.EventHandler(this.ShopSelectorForm_Shown);
			this.grpContainer.ResumeLayout(false);
			this.grpContainer.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblShops;
		private System.Windows.Forms.ComboBox cboShops;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblWw;
		private System.Windows.Forms.RadioButton rdoWw;
		private System.Windows.Forms.GroupBox grpContainer;
	}
}