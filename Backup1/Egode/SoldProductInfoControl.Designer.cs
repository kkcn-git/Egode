namespace Egode
{
	partial class SoldProductInfoControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoldProductInfoControl));
			this.pnl = new System.Windows.Forms.FlowLayoutPanel();
			this.cboProducts = new System.Windows.Forms.ComboBox();
			this.lblX = new System.Windows.Forms.Label();
			this.nudCount = new System.Windows.Forms.NumericUpDown();
			this.ts = new System.Windows.Forms.ToolStrip();
			this.tsbtnRemove = new System.Windows.Forms.ToolStripButton();
			this.pnl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
			this.ts.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnl
			// 
			this.pnl.Controls.Add(this.cboProducts);
			this.pnl.Controls.Add(this.lblX);
			this.pnl.Controls.Add(this.nudCount);
			this.pnl.Controls.Add(this.ts);
			this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnl.Location = new System.Drawing.Point(0, 0);
			this.pnl.Name = "pnl";
			this.pnl.Size = new System.Drawing.Size(240, 26);
			this.pnl.TabIndex = 0;
			// 
			// cboProducts
			// 
			this.cboProducts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.cboProducts.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.cboProducts.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboProducts.DropDownWidth = 260;
			this.cboProducts.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.cboProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.cboProducts.FormattingEnabled = true;
			this.cboProducts.Location = new System.Drawing.Point(0, 0);
			this.cboProducts.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.cboProducts.Name = "cboProducts";
			this.cboProducts.Size = new System.Drawing.Size(130, 24);
			this.cboProducts.TabIndex = 0;
			this.cboProducts.SelectedIndexChanged += new System.EventHandler(this.cboProducts_SelectedIndexChanged);
			this.cboProducts.TextChanged += new System.EventHandler(this.cboProducts_TextChanged);
			// 
			// lblX
			// 
			this.lblX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblX.AutoSize = true;
			this.lblX.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblX.ForeColor = System.Drawing.Color.Gray;
			this.lblX.Location = new System.Drawing.Point(134, 3);
			this.lblX.Margin = new System.Windows.Forms.Padding(1, 3, 1, 0);
			this.lblX.Name = "lblX";
			this.lblX.Size = new System.Drawing.Size(16, 17);
			this.lblX.TabIndex = 1;
			this.lblX.Text = "X";
			// 
			// nudCount
			// 
			this.nudCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.nudCount.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.nudCount.ForeColor = System.Drawing.Color.Red;
			this.nudCount.Location = new System.Drawing.Point(154, 0);
			this.nudCount.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.nudCount.Maximum = new decimal(new int[] {
			1000000,
			0,
			0,
			0});
			this.nudCount.Name = "nudCount";
			this.nudCount.Size = new System.Drawing.Size(48, 23);
			this.nudCount.TabIndex = 2;
			this.nudCount.ValueChanged += new System.EventHandler(this.nudCount_ValueChanged);
			// 
			// ts
			// 
			this.ts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ts.AutoSize = false;
			this.ts.Dock = System.Windows.Forms.DockStyle.None;
			this.ts.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.ts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsbtnRemove});
			this.ts.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.ts.Location = new System.Drawing.Point(205, 1);
			this.ts.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.ts.Name = "ts";
			this.ts.Size = new System.Drawing.Size(28, 24);
			this.ts.TabIndex = 3;
			this.ts.Text = "toolStrip1";
			// 
			// tsbtnRemove
			// 
			this.tsbtnRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnRemove.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRemove.Image")));
			this.tsbtnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnRemove.Name = "tsbtnRemove";
			this.tsbtnRemove.Size = new System.Drawing.Size(23, 20);
			this.tsbtnRemove.Text = "Remove";
			this.tsbtnRemove.Click += new System.EventHandler(this.tsbtnRemove_Click);
			// 
			// SoldProductInfoControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnl);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.Name = "SoldProductInfoControl";
			this.Size = new System.Drawing.Size(240, 26);
			this.SizeChanged += new System.EventHandler(this.SoldProductInfoControl_SizeChanged);
			this.Enter += new System.EventHandler(this.SoldProductInfoControl_Enter);
			this.pnl.ResumeLayout(false);
			this.pnl.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
			this.ts.ResumeLayout(false);
			this.ts.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel pnl;
		private System.Windows.Forms.ComboBox cboProducts;
		private System.Windows.Forms.Label lblX;
		private System.Windows.Forms.NumericUpDown nudCount;
		private System.Windows.Forms.ToolStrip ts;
		private System.Windows.Forms.ToolStripButton tsbtnRemove;
	}
}
