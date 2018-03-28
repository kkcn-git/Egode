namespace Egode
{
	partial class StockActionForm
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
			this.lblBrands = new System.Windows.Forms.Label();
			this.cboBrands = new System.Windows.Forms.ComboBox();
			this.lblProduct = new System.Windows.Forms.Label();
			this.cboProducts = new System.Windows.Forms.ComboBox();
			this.lblCount = new System.Windows.Forms.Label();
			this.nudCount = new System.Windows.Forms.NumericUpDown();
			this.lblFromTo = new System.Windows.Forms.Label();
			this.txtFromTo = new System.Windows.Forms.TextBox();
			this.lblComment = new System.Windows.Forms.Label();
			this.txtComment = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.pnlMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.lblBrands);
			this.pnlMain.Controls.Add(this.cboBrands);
			this.pnlMain.Controls.Add(this.lblProduct);
			this.pnlMain.Controls.Add(this.cboProducts);
			this.pnlMain.Controls.Add(this.lblCount);
			this.pnlMain.Controls.Add(this.nudCount);
			this.pnlMain.Controls.Add(this.lblFromTo);
			this.pnlMain.Controls.Add(this.txtFromTo);
			this.pnlMain.Controls.Add(this.lblComment);
			this.pnlMain.Controls.Add(this.txtComment);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(6, 6);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(332, 179);
			this.pnlMain.TabIndex = 0;
			// 
			// lblBrands
			// 
			this.lblBrands.AutoSize = true;
			this.lblBrands.Location = new System.Drawing.Point(3, 6);
			this.lblBrands.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
			this.lblBrands.Name = "lblBrands";
			this.lblBrands.Size = new System.Drawing.Size(39, 13);
			this.lblBrands.TabIndex = 0;
			this.lblBrands.Text = "Brand:";
			// 
			// cboBrands
			// 
			this.cboBrands.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pnlMain.SetFlowBreak(this.cboBrands, true);
			this.cboBrands.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.cboBrands.FormattingEnabled = true;
			this.cboBrands.Location = new System.Drawing.Point(65, 3);
			this.cboBrands.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
			this.cboBrands.Name = "cboBrands";
			this.cboBrands.Size = new System.Drawing.Size(260, 21);
			this.cboBrands.TabIndex = 1;
			this.cboBrands.SelectedIndexChanged += new System.EventHandler(this.cboBrands_SelectedIndexChanged);
			// 
			// lblProduct
			// 
			this.lblProduct.AutoSize = true;
			this.lblProduct.Location = new System.Drawing.Point(3, 33);
			this.lblProduct.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
			this.lblProduct.Name = "lblProduct";
			this.lblProduct.Size = new System.Drawing.Size(48, 13);
			this.lblProduct.TabIndex = 2;
			this.lblProduct.Text = "Product:";
			// 
			// cboProducts
			// 
			this.cboProducts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pnlMain.SetFlowBreak(this.cboProducts, true);
			this.cboProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.cboProducts.FormattingEnabled = true;
			this.cboProducts.Location = new System.Drawing.Point(65, 30);
			this.cboProducts.Margin = new System.Windows.Forms.Padding(11, 3, 3, 3);
			this.cboProducts.Name = "cboProducts";
			this.cboProducts.Size = new System.Drawing.Size(260, 21);
			this.cboProducts.TabIndex = 3;
			// 
			// lblCount
			// 
			this.lblCount.AutoSize = true;
			this.lblCount.Location = new System.Drawing.Point(3, 60);
			this.lblCount.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
			this.lblCount.Name = "lblCount";
			this.lblCount.Size = new System.Drawing.Size(40, 13);
			this.lblCount.TabIndex = 4;
			this.lblCount.Text = "Count:";
			// 
			// nudCount
			// 
			this.pnlMain.SetFlowBreak(this.nudCount, true);
			this.nudCount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.nudCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.nudCount.Location = new System.Drawing.Point(65, 57);
			this.nudCount.Margin = new System.Windows.Forms.Padding(19, 3, 3, 3);
			this.nudCount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.nudCount.Name = "nudCount";
			this.nudCount.Size = new System.Drawing.Size(80, 23);
			this.nudCount.TabIndex = 5;
			this.nudCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// lblFromTo
			// 
			this.lblFromTo.AutoSize = true;
			this.lblFromTo.Location = new System.Drawing.Point(3, 89);
			this.lblFromTo.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
			this.lblFromTo.Name = "lblFromTo";
			this.lblFromTo.Size = new System.Drawing.Size(51, 13);
			this.lblFromTo.TabIndex = 6;
			this.lblFromTo.Text = "From/To:";
			// 
			// txtFromTo
			// 
			this.pnlMain.SetFlowBreak(this.txtFromTo, true);
			this.txtFromTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtFromTo.Location = new System.Drawing.Point(65, 86);
			this.txtFromTo.Margin = new System.Windows.Forms.Padding(8, 3, 3, 3);
			this.txtFromTo.Name = "txtFromTo";
			this.txtFromTo.Size = new System.Drawing.Size(260, 21);
			this.txtFromTo.TabIndex = 7;
			// 
			// lblComment
			// 
			this.lblComment.AutoSize = true;
			this.lblComment.Location = new System.Drawing.Point(3, 116);
			this.lblComment.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
			this.lblComment.Name = "lblComment";
			this.lblComment.Size = new System.Drawing.Size(56, 13);
			this.lblComment.TabIndex = 8;
			this.lblComment.Text = "Comment:";
			// 
			// txtComment
			// 
			this.txtComment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtComment.Location = new System.Drawing.Point(65, 113);
			this.txtComment.Multiline = true;
			this.txtComment.Name = "txtComment";
			this.txtComment.Size = new System.Drawing.Size(260, 60);
			this.txtComment.TabIndex = 9;
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(175, 187);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(256, 187);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// StockActionForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(344, 215);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.pnlMain);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "StockActionForm";
			this.Padding = new System.Windows.Forms.Padding(6, 6, 6, 30);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Stock Operation";
			this.Load += new System.EventHandler(this.StockActionForm_Load);
			this.pnlMain.ResumeLayout(false);
			this.pnlMain.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel pnlMain;
		private System.Windows.Forms.Label lblBrands;
		private System.Windows.Forms.ComboBox cboBrands;
		private System.Windows.Forms.Label lblProduct;
		private System.Windows.Forms.ComboBox cboProducts;
		private System.Windows.Forms.Label lblCount;
		private System.Windows.Forms.NumericUpDown nudCount;
		private System.Windows.Forms.Label lblFromTo;
		private System.Windows.Forms.TextBox txtFromTo;
		private System.Windows.Forms.Label lblComment;
		private System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
	}
}