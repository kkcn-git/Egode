namespace Egode
{
	partial class StockActionAdvForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockActionAdvForm));
			this.lblProducts = new System.Windows.Forms.Label();
			this.pnlProductList = new System.Windows.Forms.FlowLayoutPanel();
			this.tsAddProduct = new System.Windows.Forms.ToolStrip();
			this.tsbtnAddProduct = new System.Windows.Forms.ToolStripButton();
			this.lblFromTo = new System.Windows.Forms.Label();
			this.cboFromToPart1 = new System.Windows.Forms.ComboBox();
			this.lblSeparator = new System.Windows.Forms.Label();
			this.txtFromToPart2 = new System.Windows.Forms.TextBox();
			this.lblComment = new System.Windows.Forms.Label();
			this.txtComment = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.pnlProductList.SuspendLayout();
			this.tsAddProduct.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblProducts
			// 
			this.lblProducts.AutoSize = true;
			this.lblProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblProducts.Location = new System.Drawing.Point(6, 6);
			this.lblProducts.Margin = new System.Windows.Forms.Padding(3, 8, 1, 0);
			this.lblProducts.Name = "lblProducts";
			this.lblProducts.Size = new System.Drawing.Size(59, 17);
			this.lblProducts.TabIndex = 4;
			this.lblProducts.Text = "出库商品:";
			// 
			// pnlProductList
			// 
			this.pnlProductList.AutoScroll = true;
			this.pnlProductList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.pnlProductList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlProductList.Controls.Add(this.tsAddProduct);
			this.pnlProductList.Location = new System.Drawing.Point(9, 24);
			this.pnlProductList.Name = "pnlProductList";
			this.pnlProductList.Size = new System.Drawing.Size(453, 170);
			this.pnlProductList.TabIndex = 21;
			this.pnlProductList.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlProductList_Paint);
			// 
			// tsAddProduct
			// 
			this.tsAddProduct.AutoSize = false;
			this.tsAddProduct.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsAddProduct.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnAddProduct});
			this.tsAddProduct.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.tsAddProduct.Location = new System.Drawing.Point(418, 0);
			this.tsAddProduct.Margin = new System.Windows.Forms.Padding(418, 0, 2, 0);
			this.tsAddProduct.Name = "tsAddProduct";
			this.tsAddProduct.Size = new System.Drawing.Size(26, 24);
			this.tsAddProduct.TabIndex = 0;
			this.tsAddProduct.Text = "toolStrip1";
			// 
			// tsbtnAddProduct
			// 
			this.tsbtnAddProduct.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnAddProduct.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAddProduct.Image")));
			this.tsbtnAddProduct.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnAddProduct.Name = "tsbtnAddProduct";
			this.tsbtnAddProduct.Size = new System.Drawing.Size(23, 20);
			this.tsbtnAddProduct.Text = "Add Product";
			this.tsbtnAddProduct.Click += new System.EventHandler(this.tsbtnAddProduct_Click);
			// 
			// lblFromTo
			// 
			this.lblFromTo.AutoSize = true;
			this.lblFromTo.Location = new System.Drawing.Point(6, 213);
			this.lblFromTo.Name = "lblFromTo";
			this.lblFromTo.Size = new System.Drawing.Size(112, 17);
			this.lblFromTo.TabIndex = 22;
			this.lblFromTo.Text = "出库目标/入库来源:";
			// 
			// cboFromToPart1
			// 
			this.cboFromToPart1.DropDownWidth = 180;
			this.cboFromToPart1.FormattingEnabled = true;
			this.cboFromToPart1.Location = new System.Drawing.Point(9, 231);
			this.cboFromToPart1.Name = "cboFromToPart1";
			this.cboFromToPart1.Size = new System.Drawing.Size(152, 25);
			this.cboFromToPart1.TabIndex = 23;
			// 
			// lblSeparator
			// 
			this.lblSeparator.AutoSize = true;
			this.lblSeparator.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblSeparator.ForeColor = System.Drawing.Color.Black;
			this.lblSeparator.Location = new System.Drawing.Point(167, 234);
			this.lblSeparator.Name = "lblSeparator";
			this.lblSeparator.Size = new System.Drawing.Size(17, 22);
			this.lblSeparator.TabIndex = 24;
			this.lblSeparator.Text = "\\";
			// 
			// txtFromToPart2
			// 
			this.txtFromToPart2.ImeMode = System.Windows.Forms.ImeMode.On;
			this.txtFromToPart2.Location = new System.Drawing.Point(190, 233);
			this.txtFromToPart2.Name = "txtFromToPart2";
			this.txtFromToPart2.Size = new System.Drawing.Size(272, 23);
			this.txtFromToPart2.TabIndex = 25;
			// 
			// lblComment
			// 
			this.lblComment.AutoSize = true;
			this.lblComment.Location = new System.Drawing.Point(6, 278);
			this.lblComment.Name = "lblComment";
			this.lblComment.Size = new System.Drawing.Size(35, 17);
			this.lblComment.TabIndex = 26;
			this.lblComment.Text = "说明:";
			// 
			// txtComment
			// 
			this.txtComment.ImeMode = System.Windows.Forms.ImeMode.On;
			this.txtComment.Location = new System.Drawing.Point(9, 296);
			this.txtComment.Multiline = true;
			this.txtComment.Name = "txtComment";
			this.txtComment.Size = new System.Drawing.Size(453, 60);
			this.txtComment.TabIndex = 27;
			// 
			// btnOK
			// 
			this.btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.btnOK.Location = new System.Drawing.Point(306, 377);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 28;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.btnCancel.Location = new System.Drawing.Point(387, 377);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 29;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// StockActionAdvForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(474, 408);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.txtComment);
			this.Controls.Add(this.lblComment);
			this.Controls.Add(this.txtFromToPart2);
			this.Controls.Add(this.lblSeparator);
			this.Controls.Add(this.cboFromToPart1);
			this.Controls.Add(this.lblFromTo);
			this.Controls.Add(this.pnlProductList);
			this.Controls.Add(this.lblProducts);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "StockActionAdvForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "出库";
			this.Load += new System.EventHandler(this.StockActionAdvForm_Load);
			this.Shown += new System.EventHandler(this.StockActionAdvForm_Shown);
			this.pnlProductList.ResumeLayout(false);
			this.tsAddProduct.ResumeLayout(false);
			this.tsAddProduct.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblProducts;
		private System.Windows.Forms.FlowLayoutPanel pnlProductList;
		private System.Windows.Forms.ToolStrip tsAddProduct;
		private System.Windows.Forms.ToolStripButton tsbtnAddProduct;
		private System.Windows.Forms.Label lblFromTo;
		private System.Windows.Forms.ComboBox cboFromToPart1;
		private System.Windows.Forms.Label lblSeparator;
		private System.Windows.Forms.TextBox txtFromToPart2;
		private System.Windows.Forms.Label lblComment;
		private System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
	}
}