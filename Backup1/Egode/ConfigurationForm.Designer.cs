namespace Egode
{
	partial class ConfigurationForm
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
			this.tc = new System.Windows.Forms.TabControl();
			this.tpShipment = new System.Windows.Forms.TabPage();
			this.flpnlShipment = new System.Windows.Forms.FlowLayoutPanel();
			this.gbDefaultRules = new System.Windows.Forms.GroupBox();
			this.rdoAutoSelectShipment = new System.Windows.Forms.RadioButton();
			this.lblAutoSelectShipment = new System.Windows.Forms.Label();
			this.rdoAlwaysSameShipment = new System.Windows.Forms.RadioButton();
			this.cboShipmentCompanies = new System.Windows.Forms.ComboBox();
			this.lblShipmentTips = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tc.SuspendLayout();
			this.tpShipment.SuspendLayout();
			this.flpnlShipment.SuspendLayout();
			this.gbDefaultRules.SuspendLayout();
			this.SuspendLayout();
			// 
			// tc
			// 
			this.tc.Controls.Add(this.tpShipment);
			this.tc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tc.Location = new System.Drawing.Point(0, 0);
			this.tc.Name = "tc";
			this.tc.SelectedIndex = 0;
			this.tc.Size = new System.Drawing.Size(352, 306);
			this.tc.TabIndex = 0;
			// 
			// tpShipment
			// 
			this.tpShipment.Controls.Add(this.flpnlShipment);
			this.tpShipment.Location = new System.Drawing.Point(4, 22);
			this.tpShipment.Name = "tpShipment";
			this.tpShipment.Padding = new System.Windows.Forms.Padding(3);
			this.tpShipment.Size = new System.Drawing.Size(344, 280);
			this.tpShipment.TabIndex = 0;
			this.tpShipment.Text = "快递";
			this.tpShipment.UseVisualStyleBackColor = true;
			// 
			// flpnlShipment
			// 
			this.flpnlShipment.Controls.Add(this.gbDefaultRules);
			this.flpnlShipment.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flpnlShipment.Location = new System.Drawing.Point(3, 3);
			this.flpnlShipment.Name = "flpnlShipment";
			this.flpnlShipment.Size = new System.Drawing.Size(338, 274);
			this.flpnlShipment.TabIndex = 0;
			// 
			// gbDefaultRules
			// 
			this.gbDefaultRules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gbDefaultRules.Controls.Add(this.lblShipmentTips);
			this.gbDefaultRules.Controls.Add(this.cboShipmentCompanies);
			this.gbDefaultRules.Controls.Add(this.rdoAlwaysSameShipment);
			this.gbDefaultRules.Controls.Add(this.lblAutoSelectShipment);
			this.gbDefaultRules.Controls.Add(this.rdoAutoSelectShipment);
			this.gbDefaultRules.Location = new System.Drawing.Point(12, 12);
			this.gbDefaultRules.Margin = new System.Windows.Forms.Padding(12, 12, 3, 3);
			this.gbDefaultRules.Name = "gbDefaultRules";
			this.gbDefaultRules.Size = new System.Drawing.Size(315, 250);
			this.gbDefaultRules.TabIndex = 0;
			this.gbDefaultRules.TabStop = false;
			this.gbDefaultRules.Text = "首选快递规则";
			// 
			// rdoAutoSelectShipment
			// 
			this.rdoAutoSelectShipment.AutoSize = true;
			this.rdoAutoSelectShipment.Location = new System.Drawing.Point(32, 32);
			this.rdoAutoSelectShipment.Name = "rdoAutoSelectShipment";
			this.rdoAutoSelectShipment.Size = new System.Drawing.Size(145, 17);
			this.rdoAutoSelectShipment.TabIndex = 0;
			this.rdoAutoSelectShipment.TabStop = true;
			this.rdoAutoSelectShipment.Text = "根据地区自动选择快递";
			this.rdoAutoSelectShipment.UseVisualStyleBackColor = true;
			// 
			// lblAutoSelectShipment
			// 
			this.lblAutoSelectShipment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblAutoSelectShipment.ForeColor = System.Drawing.Color.Gray;
			this.lblAutoSelectShipment.Location = new System.Drawing.Point(48, 52);
			this.lblAutoSelectShipment.Name = "lblAutoSelectShipment";
			this.lblAutoSelectShipment.Size = new System.Drawing.Size(245, 34);
			this.lblAutoSelectShipment.TabIndex = 1;
			this.lblAutoSelectShipment.Text = "江浙沪自动选择中通, 其他地区自动选择韵达.";
			// 
			// rdoAlwaysSameShipment
			// 
			this.rdoAlwaysSameShipment.AutoSize = true;
			this.rdoAlwaysSameShipment.Location = new System.Drawing.Point(32, 99);
			this.rdoAlwaysSameShipment.Name = "rdoAlwaysSameShipment";
			this.rdoAlwaysSameShipment.Size = new System.Drawing.Size(133, 17);
			this.rdoAlwaysSameShipment.TabIndex = 2;
			this.rdoAlwaysSameShipment.TabStop = true;
			this.rdoAlwaysSameShipment.Text = "总是自动选择xx快递";
			this.rdoAlwaysSameShipment.UseVisualStyleBackColor = true;
			this.rdoAlwaysSameShipment.CheckedChanged += new System.EventHandler(this.rdoAlwaysSameShipment_CheckedChanged);
			// 
			// cboShipmentCompanies
			// 
			this.cboShipmentCompanies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboShipmentCompanies.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.cboShipmentCompanies.FormattingEnabled = true;
			this.cboShipmentCompanies.Location = new System.Drawing.Point(50, 121);
			this.cboShipmentCompanies.Name = "cboShipmentCompanies";
			this.cboShipmentCompanies.Size = new System.Drawing.Size(182, 21);
			this.cboShipmentCompanies.TabIndex = 1;
			// 
			// lblShipmentTips
			// 
			this.lblShipmentTips.ForeColor = System.Drawing.Color.Green;
			this.lblShipmentTips.Location = new System.Drawing.Point(29, 182);
			this.lblShipmentTips.Name = "lblShipmentTips";
			this.lblShipmentTips.Size = new System.Drawing.Size(264, 46);
			this.lblShipmentTips.TabIndex = 3;
			this.lblShipmentTips.Text = "注意: 无论选择上述哪种规则, 如果买家留言或者客服备注有指定快递公司, 都以指定快递公司为优先.";
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(199, 312);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(277, 312);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// ConfigurationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(352, 336);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.tc);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "ConfigurationForm";
			this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 30);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Config";
			this.Load += new System.EventHandler(this.ConfigurationForm_Load);
			this.tc.ResumeLayout(false);
			this.tpShipment.ResumeLayout(false);
			this.flpnlShipment.ResumeLayout(false);
			this.gbDefaultRules.ResumeLayout(false);
			this.gbDefaultRules.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tc;
		private System.Windows.Forms.TabPage tpShipment;
		private System.Windows.Forms.FlowLayoutPanel flpnlShipment;
		private System.Windows.Forms.GroupBox gbDefaultRules;
		private System.Windows.Forms.RadioButton rdoAutoSelectShipment;
		private System.Windows.Forms.Label lblAutoSelectShipment;
		private System.Windows.Forms.RadioButton rdoAlwaysSameShipment;
		private System.Windows.Forms.ComboBox cboShipmentCompanies;
		private System.Windows.Forms.Label lblShipmentTips;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
	}
}