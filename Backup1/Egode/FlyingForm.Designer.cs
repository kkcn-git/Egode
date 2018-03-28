namespace Egode
{
	partial class FlyingForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlyingForm));
			this.pic = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
			this.SuspendLayout();
			// 
			// pic
			// 
			this.pic.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pic.Image = ((System.Drawing.Image)(resources.GetObject("pic.Image")));
			this.pic.Location = new System.Drawing.Point(0, 0);
			this.pic.Name = "pic";
			this.pic.Size = new System.Drawing.Size(16, 16);
			this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pic.TabIndex = 0;
			this.pic.TabStop = false;
			// 
			// NbFlyingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(16, 16);
			this.Controls.Add(this.pic);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "NbFlyingForm";
			this.Text = "NbFlyingForm";
			this.TopMost = true;
			this.Shown += new System.EventHandler(this.NbFlyingForm_Shown);
			((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pic;
	}
}