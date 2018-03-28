namespace Egode
{
	partial class PreparationQueryForm
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
			this.lblOrderId = new System.Windows.Forms.Label();
			this.txtOrderId = new System.Windows.Forms.TextBox();
			this.btnQuery = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblOrderId
			// 
			this.lblOrderId.AutoSize = true;
			this.lblOrderId.Location = new System.Drawing.Point(12, 9);
			this.lblOrderId.Name = "lblOrderId";
			this.lblOrderId.Size = new System.Drawing.Size(59, 13);
			this.lblOrderId.TabIndex = 0;
			this.lblOrderId.Text = "订单编号:";
			// 
			// txtOrderId
			// 
			this.txtOrderId.Location = new System.Drawing.Point(15, 25);
			this.txtOrderId.Name = "txtOrderId";
			this.txtOrderId.Size = new System.Drawing.Size(160, 21);
			this.txtOrderId.TabIndex = 1;
			this.txtOrderId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOrderId_KeyPress);
			this.txtOrderId.Enter += new System.EventHandler(this.txtOrderId_Enter);
			// 
			// btnQuery
			// 
			this.btnQuery.BackColor = System.Drawing.SystemColors.Highlight;
			this.btnQuery.ForeColor = System.Drawing.Color.White;
			this.btnQuery.Location = new System.Drawing.Point(177, 25);
			this.btnQuery.Name = "btnQuery";
			this.btnQuery.Size = new System.Drawing.Size(75, 21);
			this.btnQuery.TabIndex = 2;
			this.btnQuery.Text = "GO";
			this.btnQuery.UseVisualStyleBackColor = false;
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			// 
			// PreparationQueryForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(266, 62);
			this.Controls.Add(this.btnQuery);
			this.Controls.Add(this.txtOrderId);
			this.Controls.Add(this.lblOrderId);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.Name = "PreparationQueryForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "出单状态查询";
			this.Load += new System.EventHandler(this.PreparationQueryForm_Load);
			this.Shown += new System.EventHandler(this.PreparationQueryForm_Shown);
			this.Activated += new System.EventHandler(this.PreparationQueryForm_Activated);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblOrderId;
		private System.Windows.Forms.TextBox txtOrderId;
		private System.Windows.Forms.Button btnQuery;
	}
}