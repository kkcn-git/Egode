namespace Egode
{
	partial class ConsignForm
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
			this.pnlButtons = new System.Windows.Forms.FlowLayoutPanel();
			this.btn3 = new System.Windows.Forms.Button();
			this.btn5 = new System.Windows.Forms.Button();
			this.btn7 = new System.Windows.Forms.Button();
			this.btn10 = new System.Windows.Forms.Button();
			this.pnlWb = new System.Windows.Forms.Panel();
			this.wb = new System.Windows.Forms.WebBrowser();
			this.pnlButtons.SuspendLayout();
			this.pnlWb.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlButtons
			// 
			this.pnlButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlButtons.Controls.Add(this.btn3);
			this.pnlButtons.Controls.Add(this.btn5);
			this.pnlButtons.Controls.Add(this.btn7);
			this.pnlButtons.Controls.Add(this.btn10);
			this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlButtons.Location = new System.Drawing.Point(0, 0);
			this.pnlButtons.Name = "pnlButtons";
			this.pnlButtons.Size = new System.Drawing.Size(1130, 28);
			this.pnlButtons.TabIndex = 1;
			// 
			// btn3
			// 
			this.btn3.Enabled = false;
			this.btn3.Location = new System.Drawing.Point(1, 1);
			this.btn3.Margin = new System.Windows.Forms.Padding(1, 1, 1, 3);
			this.btn3.Name = "btn3";
			this.btn3.Size = new System.Drawing.Size(75, 23);
			this.btn3.TabIndex = 0;
			this.btn3.Tag = "3";
			this.btn3.Text = "延长3天";
			this.btn3.UseVisualStyleBackColor = true;
			this.btn3.Click += new System.EventHandler(this.OnDelayButtonClick);
			// 
			// btn5
			// 
			this.btn5.Enabled = false;
			this.btn5.Location = new System.Drawing.Point(78, 1);
			this.btn5.Margin = new System.Windows.Forms.Padding(1, 1, 1, 3);
			this.btn5.Name = "btn5";
			this.btn5.Size = new System.Drawing.Size(75, 23);
			this.btn5.TabIndex = 1;
			this.btn5.Tag = "5";
			this.btn5.Text = "延长5天";
			this.btn5.UseVisualStyleBackColor = true;
			this.btn5.Click += new System.EventHandler(this.OnDelayButtonClick);
			// 
			// btn7
			// 
			this.btn7.Enabled = false;
			this.btn7.Location = new System.Drawing.Point(155, 1);
			this.btn7.Margin = new System.Windows.Forms.Padding(1, 1, 1, 3);
			this.btn7.Name = "btn7";
			this.btn7.Size = new System.Drawing.Size(75, 23);
			this.btn7.TabIndex = 2;
			this.btn7.Tag = "7";
			this.btn7.Text = "延长7天";
			this.btn7.UseVisualStyleBackColor = true;
			this.btn7.Click += new System.EventHandler(this.OnDelayButtonClick);
			// 
			// btn10
			// 
			this.btn10.Enabled = false;
			this.btn10.Location = new System.Drawing.Point(232, 1);
			this.btn10.Margin = new System.Windows.Forms.Padding(1, 1, 1, 3);
			this.btn10.Name = "btn10";
			this.btn10.Size = new System.Drawing.Size(75, 23);
			this.btn10.TabIndex = 3;
			this.btn10.Tag = "10";
			this.btn10.Text = "延长10天";
			this.btn10.UseVisualStyleBackColor = true;
			this.btn10.Click += new System.EventHandler(this.OnDelayButtonClick);
			// 
			// pnlWb
			// 
			this.pnlWb.AutoScroll = true;
			this.pnlWb.Controls.Add(this.wb);
			this.pnlWb.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlWb.Location = new System.Drawing.Point(0, 28);
			this.pnlWb.Name = "pnlWb";
			this.pnlWb.Size = new System.Drawing.Size(1130, 658);
			this.pnlWb.TabIndex = 2;
			// 
			// wb
			// 
			this.wb.Location = new System.Drawing.Point(0, 0);
			this.wb.MinimumSize = new System.Drawing.Size(20, 20);
			this.wb.Name = "wb";
			this.wb.Size = new System.Drawing.Size(1079, 658);
			this.wb.TabIndex = 3;
			this.wb.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wb_DocumentCompleted);
			// 
			// ConsignForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1130, 686);
			this.Controls.Add(this.pnlWb);
			this.Controls.Add(this.pnlButtons);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ConsignForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "ConsignForm";
			this.Load += new System.EventHandler(this.ConsignForm_Load);
			this.pnlButtons.ResumeLayout(false);
			this.pnlWb.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel pnlButtons;
		private System.Windows.Forms.Button btn3;
		private System.Windows.Forms.Button btn5;
		private System.Windows.Forms.Button btn7;
		private System.Windows.Forms.Button btn10;
		private System.Windows.Forms.Panel pnlWb;
		private System.Windows.Forms.WebBrowser wb;

	}
}