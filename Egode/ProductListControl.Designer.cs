namespace Egode
{
	partial class ProductListControl
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
			this.tblMain = new System.Windows.Forms.TableLayoutPanel();
			this.SuspendLayout();
			// 
			// tblMain
			// 
			this.tblMain.AutoSize = true;
			this.tblMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tblMain.ColumnCount = 3;
			this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblMain.Location = new System.Drawing.Point(0, 0);
			this.tblMain.Name = "tblMain";
			this.tblMain.RowCount = 1;
			this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblMain.Size = new System.Drawing.Size(0, 0);
			this.tblMain.TabIndex = 0;
			// 
			// ProductListControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.tblMain);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "ProductListControl";
			this.Size = new System.Drawing.Size(3, 3);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tblMain;
	}
}
