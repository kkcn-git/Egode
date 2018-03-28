namespace Egode
{
	partial class PacketsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PacketsForm));
			this.tsMain = new System.Windows.Forms.ToolStrip();
			this.tsbtnGo = new System.Windows.Forms.ToolStripButton();
			this.tsbtnGoBaishi = new System.Windows.Forms.ToolStripButton();
			this.pnlPackets = new System.Windows.Forms.FlowLayoutPanel();
			this.rtb = new System.Windows.Forms.RichTextBox();
			this.tsbtnGoEms = new System.Windows.Forms.ToolStripButton();
			this.tsMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// tsMain
			// 
			this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnGo,
            this.tsbtnGoBaishi,
            this.tsbtnGoEms});
			this.tsMain.Location = new System.Drawing.Point(0, 0);
			this.tsMain.Name = "tsMain";
			this.tsMain.Size = new System.Drawing.Size(654, 25);
			this.tsMain.TabIndex = 0;
			this.tsMain.Text = "toolStrip1";
			// 
			// tsbtnGo
			// 
			this.tsbtnGo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbtnGo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tsbtnGo.ForeColor = System.Drawing.Color.DodgerBlue;
			this.tsbtnGo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnGo.Image")));
			this.tsbtnGo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnGo.Name = "tsbtnGo";
			this.tsbtnGo.Size = new System.Drawing.Size(28, 22);
			this.tsbtnGo.Text = "Go";
			this.tsbtnGo.Visible = false;
			this.tsbtnGo.Click += new System.EventHandler(this.tsbtnGo_Click);
			// 
			// tsbtnGoBaishi
			// 
			this.tsbtnGoBaishi.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbtnGoBaishi.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tsbtnGoBaishi.ForeColor = System.Drawing.Color.DodgerBlue;
			this.tsbtnGoBaishi.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnGoBaishi.Image")));
			this.tsbtnGoBaishi.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnGoBaishi.Name = "tsbtnGoBaishi";
			this.tsbtnGoBaishi.Size = new System.Drawing.Size(52, 22);
			this.tsbtnGoBaishi.Text = "Go°×Ê¯";
			this.tsbtnGoBaishi.Click += new System.EventHandler(this.tsbtnGoBaishi_Click);
			// 
			// pnlPackets
			// 
			this.pnlPackets.AutoScroll = true;
			this.pnlPackets.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlPackets.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.pnlPackets.Location = new System.Drawing.Point(0, 25);
			this.pnlPackets.Name = "pnlPackets";
			this.pnlPackets.Size = new System.Drawing.Size(654, 421);
			this.pnlPackets.TabIndex = 1;
			this.pnlPackets.WrapContents = false;
			// 
			// rtb
			// 
			this.rtb.Location = new System.Drawing.Point(220, -31);
			this.rtb.Name = "rtb";
			this.rtb.Size = new System.Drawing.Size(100, 96);
			this.rtb.TabIndex = 2;
			this.rtb.Text = "";
			this.rtb.Visible = false;
			// 
			// tsbtnGoEms
			// 
			this.tsbtnGoEms.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbtnGoEms.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tsbtnGoEms.ForeColor = System.Drawing.Color.DodgerBlue;
			this.tsbtnGoEms.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnGoEms.Image")));
			this.tsbtnGoEms.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnGoEms.Name = "tsbtnGoEms";
			this.tsbtnGoEms.Size = new System.Drawing.Size(52, 22);
			this.tsbtnGoEms.Text = "Go EMS";
			this.tsbtnGoEms.Click += new System.EventHandler(this.tsbtnGoEms_Click);
			// 
			// PacketsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(654, 446);
			this.Controls.Add(this.rtb);
			this.Controls.Add(this.pnlPackets);
			this.Controls.Add(this.tsMain);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "PacketsForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "³öµ¥";
			this.Load += new System.EventHandler(this.PacketsForm_Load);
			this.tsMain.ResumeLayout(false);
			this.tsMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip tsMain;
		private System.Windows.Forms.FlowLayoutPanel pnlPackets;
		private System.Windows.Forms.ToolStripButton tsbtnGo;
		private System.Windows.Forms.RichTextBox rtb;
		private System.Windows.Forms.ToolStripButton tsbtnGoBaishi;
		private System.Windows.Forms.ToolStripButton tsbtnGoEms;
	}
}