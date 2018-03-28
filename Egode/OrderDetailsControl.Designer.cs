namespace Egode
{
	partial class OrderDetailsControl
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
			this.cboPackets = new System.Windows.Forms.ComboBox();
			this.btnAddPacket = new System.Windows.Forms.Button();
			this.btnMarkPrepared = new System.Windows.Forms.Button();
			this.wb = new System.Windows.Forms.WebBrowser();
			this.chkSelected = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// cboPackets
			// 
			this.cboPackets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPackets.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboPackets.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.cboPackets.FormattingEnabled = true;
			this.cboPackets.ItemHeight = 13;
			this.cboPackets.Location = new System.Drawing.Point(270, 21);
			this.cboPackets.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
			this.cboPackets.Name = "cboPackets";
			this.cboPackets.Size = new System.Drawing.Size(150, 21);
			this.cboPackets.TabIndex = 3;
			this.cboPackets.SelectedIndexChanged += new System.EventHandler(this.cboPackets_SelectedIndexChanged);
			// 
			// btnAddPacket
			// 
			this.btnAddPacket.FlatAppearance.BorderSize = 0;
			this.btnAddPacket.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAddPacket.Location = new System.Drawing.Point(422, 21);
			this.btnAddPacket.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
			this.btnAddPacket.Name = "btnAddPacket";
			this.btnAddPacket.Size = new System.Drawing.Size(30, 21);
			this.btnAddPacket.TabIndex = 4;
			this.btnAddPacket.Text = "+\r\n";
			this.btnAddPacket.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnAddPacket.UseVisualStyleBackColor = true;
			this.btnAddPacket.Click += new System.EventHandler(this.btnAddPacket_Click);
			// 
			// btnMarkPrepared
			// 
			this.btnMarkPrepared.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnMarkPrepared.Location = new System.Drawing.Point(453, 21);
			this.btnMarkPrepared.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
			this.btnMarkPrepared.Name = "btnMarkPrepared";
			this.btnMarkPrepared.Size = new System.Drawing.Size(75, 21);
			this.btnMarkPrepared.TabIndex = 5;
			this.btnMarkPrepared.Text = "±ê¼Ç³öµ¥";
			this.btnMarkPrepared.UseVisualStyleBackColor = true;
			this.btnMarkPrepared.Click += new System.EventHandler(this.btnMarkPrepared_Click);
			// 
			// wb
			// 
			this.wb.Location = new System.Drawing.Point(1, 1);
			this.wb.MinimumSize = new System.Drawing.Size(20, 20);
			this.wb.Name = "wb";
			this.wb.Size = new System.Drawing.Size(251, 129);
			this.wb.TabIndex = 1;
			this.wb.SizeChanged += new System.EventHandler(this.wb_SizeChanged);
			// 
			// chkSelected
			// 
			this.chkSelected.AutoSize = true;
			this.chkSelected.BackColor = System.Drawing.Color.Transparent;
			this.chkSelected.Location = new System.Drawing.Point(270, 109);
			this.chkSelected.Name = "chkSelected";
			this.chkSelected.Size = new System.Drawing.Size(15, 14);
			this.chkSelected.TabIndex = 6;
			this.chkSelected.UseVisualStyleBackColor = false;
			// 
			// OrderDetailsControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.chkSelected);
			this.Controls.Add(this.wb);
			this.Controls.Add(this.cboPackets);
			this.Controls.Add(this.btnAddPacket);
			this.Controls.Add(this.btnMarkPrepared);
			this.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "OrderDetailsControl";
			this.Padding = new System.Windows.Forms.Padding(1, 1, 1, 30);
			this.Size = new System.Drawing.Size(549, 149);
			this.SizeChanged += new System.EventHandler(this.OrderDetailsControl_SizeChanged);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OrderDetailsControlMouseDown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox cboPackets;
		private System.Windows.Forms.Button btnAddPacket;
		private System.Windows.Forms.Button btnMarkPrepared;
		private System.Windows.Forms.WebBrowser wb;
		private System.Windows.Forms.CheckBox chkSelected;
	}
}
