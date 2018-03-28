namespace Egode
{
	partial class OrderIdControl
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
			this.lblTitle = new System.Windows.Forms.Label();
			this.lblOrderId = new System.Windows.Forms.LinkLabel();
			this.btnConsign = new System.Windows.Forms.Button();
			this.btnConsignSh = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblTitle.Location = new System.Drawing.Point(-2, 4);
			this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(59, 13);
			this.lblTitle.TabIndex = 2;
			this.lblTitle.Text = "¶©µ¥±àºÅ:";
			this.lblTitle.SizeChanged += new System.EventHandler(this.lblTitle_SizeChanged);
			// 
			// lblOrderId
			// 
			this.lblOrderId.AutoSize = true;
			this.lblOrderId.Location = new System.Drawing.Point(57, 4);
			this.lblOrderId.Margin = new System.Windows.Forms.Padding(0);
			this.lblOrderId.Name = "lblOrderId";
			this.lblOrderId.Size = new System.Drawing.Size(97, 13);
			this.lblOrderId.TabIndex = 3;
			this.lblOrderId.TabStop = true;
			this.lblOrderId.Text = "000000000000000";
			this.lblOrderId.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblOrderId_LinkClicked);
			this.lblOrderId.SizeChanged += new System.EventHandler(this.lblTitle_SizeChanged);
			// 
			// btnConsign
			// 
			this.btnConsign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.btnConsign.Location = new System.Drawing.Point(152, 0);
			this.btnConsign.Margin = new System.Windows.Forms.Padding(1);
			this.btnConsign.Name = "btnConsign";
			this.btnConsign.Size = new System.Drawing.Size(16, 15);
			this.btnConsign.TabIndex = 4;
			this.btnConsign.UseVisualStyleBackColor = false;
			this.btnConsign.Click += new System.EventHandler(this.btnConsign_Click);
			// 
			// btnConsignSh
			// 
			this.btnConsignSh.BackColor = System.Drawing.Color.Red;
			this.btnConsignSh.Location = new System.Drawing.Point(174, 0);
			this.btnConsignSh.Margin = new System.Windows.Forms.Padding(1);
			this.btnConsignSh.Name = "btnConsignSh";
			this.btnConsignSh.Size = new System.Drawing.Size(16, 15);
			this.btnConsignSh.TabIndex = 5;
			this.btnConsignSh.UseVisualStyleBackColor = false;
			this.btnConsignSh.Click += new System.EventHandler(this.btnConsignSh_Click);
			// 
			// OrderIdControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.btnConsignSh);
			this.Controls.Add(this.btnConsign);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.lblOrderId);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "OrderIdControl";
			this.Size = new System.Drawing.Size(194, 20);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.LinkLabel lblOrderId;
		private System.Windows.Forms.Button btnConsign;
		private System.Windows.Forms.Button btnConsignSh;

	}
}
