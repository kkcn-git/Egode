namespace Egode
{
	partial class AddressSelectorForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddressSelectorForm));
			this.lvwAddresses = new System.Windows.Forms.ListView();
			this.colType = new System.Windows.Forms.ColumnHeader();
			this.colId = new System.Windows.Forms.ColumnHeader();
			this.colRecipient = new System.Windows.Forms.ColumnHeader();
			this.colMobile = new System.Windows.Forms.ColumnHeader();
			this.colPhone = new System.Windows.Forms.ColumnHeader();
			this.colProvince = new System.Windows.Forms.ColumnHeader();
			this.colCity1 = new System.Windows.Forms.ColumnHeader();
			this.colCity2 = new System.Windows.Forms.ColumnHeader();
			this.colDistrict = new System.Windows.Forms.ColumnHeader();
			this.colStreetAddr = new System.Windows.Forms.ColumnHeader();
			this.colPostCode = new System.Windows.Forms.ColumnHeader();
			this.colComment = new System.Windows.Forms.ColumnHeader();
			this.colDateTime = new System.Windows.Forms.ColumnHeader();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.btnSearch = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lvwAddresses
			// 
			this.lvwAddresses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colType,
            this.colId,
            this.colRecipient,
            this.colMobile,
            this.colPhone,
            this.colProvince,
            this.colCity1,
            this.colCity2,
            this.colDistrict,
            this.colStreetAddr,
            this.colPostCode,
            this.colComment,
            this.colDateTime});
			this.lvwAddresses.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvwAddresses.FullRowSelect = true;
			this.lvwAddresses.GridLines = true;
			this.lvwAddresses.Location = new System.Drawing.Point(2, 2);
			this.lvwAddresses.MultiSelect = false;
			this.lvwAddresses.Name = "lvwAddresses";
			this.lvwAddresses.Size = new System.Drawing.Size(708, 301);
			this.lvwAddresses.TabIndex = 0;
			this.lvwAddresses.UseCompatibleStateImageBehavior = false;
			this.lvwAddresses.View = System.Windows.Forms.View.Details;
			this.lvwAddresses.SelectedIndexChanged += new System.EventHandler(this.lvwAddresses_SelectedIndexChanged);
			this.lvwAddresses.DoubleClick += new System.EventHandler(this.lvwAddresses_DoubleClick);
			// 
			// colType
			// 
			this.colType.Text = "类型";
			this.colType.Width = 40;
			// 
			// colId
			// 
			this.colId.Text = "id";
			// 
			// colRecipient
			// 
			this.colRecipient.Text = "收件人";
			// 
			// colMobile
			// 
			this.colMobile.Text = "手机";
			this.colMobile.Width = 90;
			// 
			// colPhone
			// 
			this.colPhone.Text = "电话";
			// 
			// colProvince
			// 
			this.colProvince.Text = "省";
			// 
			// colCity1
			// 
			this.colCity1.Text = "市";
			// 
			// colCity2
			// 
			this.colCity2.Text = "县";
			// 
			// colDistrict
			// 
			this.colDistrict.Text = "区";
			// 
			// colStreetAddr
			// 
			this.colStreetAddr.Text = "街道地址";
			this.colStreetAddr.Width = 135;
			// 
			// colPostCode
			// 
			this.colPostCode.Text = "邮编";
			// 
			// colComment
			// 
			this.colComment.Text = "说明";
			// 
			// colDateTime
			// 
			this.colDateTime.Text = "创建时间";
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Enabled = false;
			this.btnOK.Location = new System.Drawing.Point(554, 309);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(635, 309);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// txtSearch
			// 
			this.txtSearch.Location = new System.Drawing.Point(2, 309);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(160, 21);
			this.txtSearch.TabIndex = 1;
			this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
			// 
			// btnSearch
			// 
			this.btnSearch.Location = new System.Drawing.Point(165, 309);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(48, 22);
			this.btnSearch.TabIndex = 2;
			this.btnSearch.Text = "Search";
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// AddressSelectorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(712, 333);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.txtSearch);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.lvwAddresses);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AddressSelectorForm";
			this.Padding = new System.Windows.Forms.Padding(2, 2, 2, 30);
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "地址库";
			this.Load += new System.EventHandler(this.AddressSelectorForm_Load);
			this.Shown += new System.EventHandler(this.AddressSelectorForm_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView lvwAddresses;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ColumnHeader colRecipient;
		private System.Windows.Forms.ColumnHeader colMobile;
		private System.Windows.Forms.ColumnHeader colPhone;
		private System.Windows.Forms.ColumnHeader colProvince;
		private System.Windows.Forms.ColumnHeader colCity1;
		private System.Windows.Forms.ColumnHeader colCity2;
		private System.Windows.Forms.ColumnHeader colDistrict;
		private System.Windows.Forms.ColumnHeader colPostCode;
		private System.Windows.Forms.ColumnHeader colComment;
		private System.Windows.Forms.ColumnHeader colType;
		private System.Windows.Forms.ColumnHeader colId;
		private System.Windows.Forms.ColumnHeader colStreetAddr;
		private System.Windows.Forms.ColumnHeader colDateTime;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.Button btnSearch;
	}
}