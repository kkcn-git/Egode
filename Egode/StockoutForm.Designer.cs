namespace Egode
{
	partial class StockoutForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockoutForm));
			this.lblProducts = new System.Windows.Forms.Label();
			this.pnlProductList = new System.Windows.Forms.FlowLayoutPanel();
			this.tsAddProduct = new System.Windows.Forms.ToolStrip();
			this.tsbtnAddProduct = new System.Windows.Forms.ToolStripButton();
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
			this.pnlProductList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.pnlProductList.Controls.Add(this.tsAddProduct);
			this.pnlProductList.Location = new System.Drawing.Point(9, 24);
			this.pnlProductList.Name = "pnlProductList";
			this.pnlProductList.Size = new System.Drawing.Size(320, 70);
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
			this.tsAddProduct.Location = new System.Drawing.Point(207, 0);
			this.tsAddProduct.Margin = new System.Windows.Forms.Padding(207, 0, 0, 0);
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
			// StockoutForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(597, 572);
			this.Controls.Add(this.lblProducts);
			this.Controls.Add(this.pnlProductList);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "StockoutForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "出库";
			this.Load += new System.EventHandler(this.StockoutForm_Load);
			this.Shown += new System.EventHandler(this.StockoutForm_Shown);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StockoutForm_KeyPress);
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
	}
}