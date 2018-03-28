namespace Egode
{
	partial class ConsignNingboForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsignNingboForm));
			this.lblLogistics = new System.Windows.Forms.Label();
			this.lblProducts = new System.Windows.Forms.Label();
			this.lblAddress = new System.Windows.Forms.Label();
			this.pnlAddress = new System.Windows.Forms.FlowLayoutPanel();
			this.lblRecipientName = new System.Windows.Forms.Label();
			this.txtRecipientName = new System.Windows.Forms.TextBox();
			this.lblIdNumber = new System.Windows.Forms.Label();
			this.txtIdInfo = new System.Windows.Forms.TextBox();
			this.txtProvince = new System.Windows.Forms.TextBox();
			this.lblProvince = new System.Windows.Forms.Label();
			this.txtCity1 = new System.Windows.Forms.TextBox();
			this.lblCity1 = new System.Windows.Forms.Label();
			this.txtCity2 = new System.Windows.Forms.TextBox();
			this.lblCity2 = new System.Windows.Forms.Label();
			this.txtDistrict = new System.Windows.Forms.TextBox();
			this.lblDistrict = new System.Windows.Forms.Label();
			this.txtStreetAddress = new System.Windows.Forms.TextBox();
			this.lblMobile = new System.Windows.Forms.Label();
			this.txtMobile = new System.Windows.Forms.TextBox();
			this.lblRemark = new System.Windows.Forms.Label();
			this.txtRemark = new System.Windows.Forms.TextBox();
			this.lblBuyerRemark = new System.Windows.Forms.Label();
			this.txtBuyerRemark = new System.Windows.Forms.TextBox();
			this.pnlPrint = new System.Windows.Forms.FlowLayoutPanel();
			this.pnlProductList = new System.Windows.Forms.FlowLayoutPanel();
			this.tsAddProduct = new System.Windows.Forms.ToolStrip();
			this.tsbtnAddProduct = new System.Windows.Forms.ToolStripButton();
			this.lblWarning = new System.Windows.Forms.Label();
			this.lblPrint = new System.Windows.Forms.Label();
			this.lblMoney = new System.Windows.Forms.Label();
			this.pnlMoney = new System.Windows.Forms.Panel();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnNingboList = new System.Windows.Forms.Button();
			this.cboLogistics = new System.Windows.Forms.ComboBox();
			this.picNb = new System.Windows.Forms.PictureBox();
			this.txtProducts = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.pnlAddress.SuspendLayout();
			this.pnlPrint.SuspendLayout();
			this.pnlProductList.SuspendLayout();
			this.tsAddProduct.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picNb)).BeginInit();
			this.SuspendLayout();
			// 
			// lblLogistics
			// 
			this.lblLogistics.AutoSize = true;
			this.lblLogistics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblLogistics.Location = new System.Drawing.Point(2, 303);
			this.lblLogistics.Name = "lblLogistics";
			this.lblLogistics.Size = new System.Drawing.Size(59, 17);
			this.lblLogistics.TabIndex = 2;
			this.lblLogistics.Text = "快递公司:";
			// 
			// lblProducts
			// 
			this.lblProducts.AutoSize = true;
			this.lblProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblProducts.Location = new System.Drawing.Point(3, 140);
			this.lblProducts.Margin = new System.Windows.Forms.Padding(3, 6, 0, 0);
			this.lblProducts.Name = "lblProducts";
			this.lblProducts.Size = new System.Drawing.Size(35, 17);
			this.lblProducts.TabIndex = 4;
			this.lblProducts.Text = "商品:";
			// 
			// lblAddress
			// 
			this.lblAddress.AutoSize = true;
			this.lblAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblAddress.Location = new System.Drawing.Point(3, 10);
			this.lblAddress.Margin = new System.Windows.Forms.Padding(3, 10, 0, 0);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(35, 17);
			this.lblAddress.TabIndex = 7;
			this.lblAddress.Text = "收件:";
			// 
			// pnlAddress
			// 
			this.pnlAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.pnlAddress.Controls.Add(this.lblRecipientName);
			this.pnlAddress.Controls.Add(this.txtRecipientName);
			this.pnlAddress.Controls.Add(this.lblIdNumber);
			this.pnlAddress.Controls.Add(this.txtIdInfo);
			this.pnlAddress.Controls.Add(this.txtProvince);
			this.pnlAddress.Controls.Add(this.lblProvince);
			this.pnlAddress.Controls.Add(this.txtCity1);
			this.pnlAddress.Controls.Add(this.lblCity1);
			this.pnlAddress.Controls.Add(this.txtCity2);
			this.pnlAddress.Controls.Add(this.lblCity2);
			this.pnlAddress.Controls.Add(this.txtDistrict);
			this.pnlAddress.Controls.Add(this.lblDistrict);
			this.pnlAddress.Controls.Add(this.txtStreetAddress);
			this.pnlAddress.Controls.Add(this.lblMobile);
			this.pnlAddress.Controls.Add(this.txtMobile);
			this.pnlPrint.SetFlowBreak(this.pnlAddress, true);
			this.pnlAddress.Location = new System.Drawing.Point(41, 3);
			this.pnlAddress.Name = "pnlAddress";
			this.pnlAddress.Size = new System.Drawing.Size(452, 128);
			this.pnlAddress.TabIndex = 8;
			this.pnlAddress.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlAddress_Paint);
			// 
			// lblRecipientName
			// 
			this.lblRecipientName.AutoSize = true;
			this.lblRecipientName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblRecipientName.Location = new System.Drawing.Point(4, 7);
			this.lblRecipientName.Margin = new System.Windows.Forms.Padding(4, 7, 0, 0);
			this.lblRecipientName.Name = "lblRecipientName";
			this.lblRecipientName.Size = new System.Drawing.Size(71, 17);
			this.lblRecipientName.TabIndex = 13;
			this.lblRecipientName.Text = "收件人姓名:";
			// 
			// txtRecipientName
			// 
			this.txtRecipientName.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtRecipientName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtRecipientName.Location = new System.Drawing.Point(75, 6);
			this.txtRecipientName.Margin = new System.Windows.Forms.Padding(0, 6, 3, 6);
			this.txtRecipientName.Name = "txtRecipientName";
			this.txtRecipientName.Size = new System.Drawing.Size(122, 23);
			this.txtRecipientName.TabIndex = 2;
			// 
			// lblIdNumber
			// 
			this.lblIdNumber.AutoSize = true;
			this.lblIdNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblIdNumber.Location = new System.Drawing.Point(212, 7);
			this.lblIdNumber.Margin = new System.Windows.Forms.Padding(12, 7, 0, 0);
			this.lblIdNumber.Name = "lblIdNumber";
			this.lblIdNumber.Size = new System.Drawing.Size(47, 17);
			this.lblIdNumber.TabIndex = 11;
			this.lblIdNumber.Text = "身份证:";
			// 
			// txtIdInfo
			// 
			this.txtIdInfo.BackColor = System.Drawing.SystemColors.Window;
			this.txtIdInfo.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtIdInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtIdInfo.Location = new System.Drawing.Point(262, 6);
			this.txtIdInfo.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.txtIdInfo.Name = "txtIdInfo";
			this.txtIdInfo.Size = new System.Drawing.Size(177, 23);
			this.txtIdInfo.TabIndex = 3;
			// 
			// txtProvince
			// 
			this.txtProvince.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtProvince.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtProvince.Location = new System.Drawing.Point(6, 35);
			this.txtProvince.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.txtProvince.Name = "txtProvince";
			this.txtProvince.Size = new System.Drawing.Size(80, 23);
			this.txtProvince.TabIndex = 4;
			// 
			// lblProvince
			// 
			this.lblProvince.AutoSize = true;
			this.lblProvince.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblProvince.Location = new System.Drawing.Point(86, 38);
			this.lblProvince.Margin = new System.Windows.Forms.Padding(0, 3, 8, 0);
			this.lblProvince.Name = "lblProvince";
			this.lblProvince.Size = new System.Drawing.Size(20, 17);
			this.lblProvince.TabIndex = 1;
			this.lblProvince.Text = "省";
			// 
			// txtCity1
			// 
			this.txtCity1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCity1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtCity1.Location = new System.Drawing.Point(117, 35);
			this.txtCity1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.txtCity1.Name = "txtCity1";
			this.txtCity1.Size = new System.Drawing.Size(80, 23);
			this.txtCity1.TabIndex = 5;
			// 
			// lblCity1
			// 
			this.lblCity1.AutoSize = true;
			this.lblCity1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblCity1.Location = new System.Drawing.Point(197, 38);
			this.lblCity1.Margin = new System.Windows.Forms.Padding(0, 3, 8, 0);
			this.lblCity1.Name = "lblCity1";
			this.lblCity1.Size = new System.Drawing.Size(20, 17);
			this.lblCity1.TabIndex = 3;
			this.lblCity1.Text = "市";
			// 
			// txtCity2
			// 
			this.txtCity2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCity2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtCity2.Location = new System.Drawing.Point(228, 35);
			this.txtCity2.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.txtCity2.Name = "txtCity2";
			this.txtCity2.Size = new System.Drawing.Size(80, 23);
			this.txtCity2.TabIndex = 6;
			// 
			// lblCity2
			// 
			this.lblCity2.AutoSize = true;
			this.lblCity2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblCity2.Location = new System.Drawing.Point(308, 38);
			this.lblCity2.Margin = new System.Windows.Forms.Padding(0, 3, 8, 0);
			this.lblCity2.Name = "lblCity2";
			this.lblCity2.Size = new System.Drawing.Size(20, 17);
			this.lblCity2.TabIndex = 5;
			this.lblCity2.Text = "县";
			// 
			// txtDistrict
			// 
			this.txtDistrict.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDistrict.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtDistrict.Location = new System.Drawing.Point(339, 35);
			this.txtDistrict.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.txtDistrict.Name = "txtDistrict";
			this.txtDistrict.Size = new System.Drawing.Size(65, 23);
			this.txtDistrict.TabIndex = 7;
			// 
			// lblDistrict
			// 
			this.lblDistrict.AutoSize = true;
			this.lblDistrict.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblDistrict.Location = new System.Drawing.Point(404, 38);
			this.lblDistrict.Margin = new System.Windows.Forms.Padding(0, 3, 8, 0);
			this.lblDistrict.Name = "lblDistrict";
			this.lblDistrict.Size = new System.Drawing.Size(40, 17);
			this.lblDistrict.TabIndex = 7;
			this.lblDistrict.Text = "区(镇)";
			// 
			// txtStreetAddress
			// 
			this.txtStreetAddress.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtStreetAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtStreetAddress.Location = new System.Drawing.Point(6, 66);
			this.txtStreetAddress.Margin = new System.Windows.Forms.Padding(6, 8, 3, 3);
			this.txtStreetAddress.Name = "txtStreetAddress";
			this.txtStreetAddress.Size = new System.Drawing.Size(433, 23);
			this.txtStreetAddress.TabIndex = 8;
			// 
			// lblMobile
			// 
			this.lblMobile.AutoSize = true;
			this.lblMobile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblMobile.Location = new System.Drawing.Point(3, 98);
			this.lblMobile.Margin = new System.Windows.Forms.Padding(3, 6, 0, 0);
			this.lblMobile.Name = "lblMobile";
			this.lblMobile.Size = new System.Drawing.Size(59, 17);
			this.lblMobile.TabIndex = 9;
			this.lblMobile.Text = "联系手机:";
			// 
			// txtMobile
			// 
			this.txtMobile.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtMobile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtMobile.Location = new System.Drawing.Point(62, 95);
			this.txtMobile.Margin = new System.Windows.Forms.Padding(0, 3, 16, 3);
			this.txtMobile.Name = "txtMobile";
			this.txtMobile.Size = new System.Drawing.Size(125, 23);
			this.txtMobile.TabIndex = 9;
			// 
			// lblRemark
			// 
			this.lblRemark.AutoSize = true;
			this.lblRemark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblRemark.Location = new System.Drawing.Point(3, 37);
			this.lblRemark.Name = "lblRemark";
			this.lblRemark.Size = new System.Drawing.Size(59, 17);
			this.lblRemark.TabIndex = 10;
			this.lblRemark.Text = "客服备注:";
			// 
			// txtRemark
			// 
			this.txtRemark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtRemark.Location = new System.Drawing.Point(65, 35);
			this.txtRemark.Multiline = true;
			this.txtRemark.Name = "txtRemark";
			this.txtRemark.Size = new System.Drawing.Size(502, 22);
			this.txtRemark.TabIndex = 1;
			// 
			// lblBuyerRemark
			// 
			this.lblBuyerRemark.AutoSize = true;
			this.lblBuyerRemark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblBuyerRemark.Location = new System.Drawing.Point(3, 10);
			this.lblBuyerRemark.Name = "lblBuyerRemark";
			this.lblBuyerRemark.Size = new System.Drawing.Size(59, 17);
			this.lblBuyerRemark.TabIndex = 12;
			this.lblBuyerRemark.Text = "买家留言:";
			// 
			// txtBuyerRemark
			// 
			this.txtBuyerRemark.ForeColor = System.Drawing.Color.Red;
			this.txtBuyerRemark.Location = new System.Drawing.Point(65, 8);
			this.txtBuyerRemark.Multiline = true;
			this.txtBuyerRemark.Name = "txtBuyerRemark";
			this.txtBuyerRemark.Size = new System.Drawing.Size(502, 22);
			this.txtBuyerRemark.TabIndex = 0;
			// 
			// pnlPrint
			// 
			this.pnlPrint.BackColor = System.Drawing.Color.White;
			this.pnlPrint.Controls.Add(this.lblAddress);
			this.pnlPrint.Controls.Add(this.pnlAddress);
			this.pnlPrint.Controls.Add(this.lblProducts);
			this.pnlPrint.Controls.Add(this.pnlProductList);
			this.pnlPrint.Controls.Add(this.lblWarning);
			this.pnlPrint.Location = new System.Drawing.Point(65, 63);
			this.pnlPrint.Name = "pnlPrint";
			this.pnlPrint.Size = new System.Drawing.Size(502, 229);
			this.pnlPrint.TabIndex = 2;
			this.pnlPrint.LocationChanged += new System.EventHandler(this.OnProductsTextBoxLocationSizeChanged);
			this.pnlPrint.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPrint_Paint);
			this.pnlPrint.SizeChanged += new System.EventHandler(this.OnProductsTextBoxLocationSizeChanged);
			// 
			// pnlProductList
			// 
			this.pnlProductList.Controls.Add(this.tsAddProduct);
			this.pnlProductList.Location = new System.Drawing.Point(41, 137);
			this.pnlProductList.Name = "pnlProductList";
			this.pnlProductList.Size = new System.Drawing.Size(452, 75);
			this.pnlProductList.TabIndex = 22;
			this.pnlProductList.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlProductList_Paint);
			// 
			// tsAddProduct
			// 
			this.tsAddProduct.AutoSize = false;
			this.tsAddProduct.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsAddProduct.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnAddProduct});
			this.tsAddProduct.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.tsAddProduct.Location = new System.Drawing.Point(207, 0);
			this.tsAddProduct.Margin = new System.Windows.Forms.Padding(207, 0, 0, 0);
			this.tsAddProduct.Name = "tsAddProduct";
			this.tsAddProduct.Size = new System.Drawing.Size(26, 24);
			this.tsAddProduct.TabIndex = 10;
			this.tsAddProduct.Text = "toolStrip1";
			// 
			// tsbtnAddProduct
			// 
			this.tsbtnAddProduct.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnAddProduct.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAddProduct.Image")));
			this.tsbtnAddProduct.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnAddProduct.Name = "tsbtnAddProduct";
			this.tsbtnAddProduct.Size = new System.Drawing.Size(23, 20);
			this.tsbtnAddProduct.Text = "Add Product";
			this.tsbtnAddProduct.Click += new System.EventHandler(this.tsbtnAddProduct_Click);
			// 
			// lblWarning
			// 
			this.lblWarning.AutoSize = true;
			this.lblWarning.ForeColor = System.Drawing.Color.IndianRed;
			this.lblWarning.Location = new System.Drawing.Point(38, 215);
			this.lblWarning.Margin = new System.Windows.Forms.Padding(38, 0, 3, 0);
			this.lblWarning.Name = "lblWarning";
			this.lblWarning.Size = new System.Drawing.Size(279, 17);
			this.lblWarning.TabIndex = 21;
			this.lblWarning.Text = "数量不做校验, 请人肉确认产品数量在免税额度以内";
			// 
			// lblPrint
			// 
			this.lblPrint.AutoSize = true;
			this.lblPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblPrint.Location = new System.Drawing.Point(2, 69);
			this.lblPrint.Name = "lblPrint";
			this.lblPrint.Size = new System.Drawing.Size(59, 17);
			this.lblPrint.TabIndex = 16;
			this.lblPrint.Text = "出单信息:";
			// 
			// lblMoney
			// 
			this.lblMoney.AutoSize = true;
			this.lblMoney.BackColor = System.Drawing.SystemColors.Window;
			this.lblMoney.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblMoney.ForeColor = System.Drawing.Color.OrangeRed;
			this.lblMoney.Location = new System.Drawing.Point(-11, 147);
			this.lblMoney.Name = "lblMoney";
			this.lblMoney.Size = new System.Drawing.Size(89, 26);
			this.lblMoney.TabIndex = 17;
			this.lblMoney.Text = "0000.00";
			this.lblMoney.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblMoney.Visible = false;
			this.lblMoney.SizeChanged += new System.EventHandler(this.OnProductsTextBoxLocationSizeChanged);
			// 
			// pnlMoney
			// 
			this.pnlMoney.BackColor = System.Drawing.Color.White;
			this.pnlMoney.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pnlMoney.ForeColor = System.Drawing.Color.OrangeRed;
			this.pnlMoney.Location = new System.Drawing.Point(7, 176);
			this.pnlMoney.Name = "pnlMoney";
			this.pnlMoney.Size = new System.Drawing.Size(31, 87);
			this.pnlMoney.TabIndex = 18;
			this.pnlMoney.Visible = false;
			this.pnlMoney.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMoney_Paint);
			// 
			// btnAdd
			// 
			this.btnAdd.BackColor = System.Drawing.Color.CornflowerBlue;
			this.btnAdd.ForeColor = System.Drawing.Color.White;
			this.btnAdd.Location = new System.Drawing.Point(65, 334);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(396, 27);
			this.btnAdd.TabIndex = 12;
			this.btnAdd.Text = "添加到出单队列";
			this.btnAdd.UseVisualStyleBackColor = false;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnNingboList
			// 
			this.btnNingboList.Location = new System.Drawing.Point(6, 334);
			this.btnNingboList.Name = "btnNingboList";
			this.btnNingboList.Size = new System.Drawing.Size(100, 27);
			this.btnNingboList.TabIndex = 13;
			this.btnNingboList.Text = "查看出单队列";
			this.btnNingboList.UseVisualStyleBackColor = true;
			this.btnNingboList.Visible = false;
			// 
			// cboLogistics
			// 
			this.cboLogistics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLogistics.FormattingEnabled = true;
			this.cboLogistics.Location = new System.Drawing.Point(65, 300);
			this.cboLogistics.Name = "cboLogistics";
			this.cboLogistics.Size = new System.Drawing.Size(396, 25);
			this.cboLogistics.TabIndex = 11;
			// 
			// picNb
			// 
			this.picNb.Image = ((System.Drawing.Image)(resources.GetObject("picNb.Image")));
			this.picNb.Location = new System.Drawing.Point(22, 276);
			this.picNb.Name = "picNb";
			this.picNb.Size = new System.Drawing.Size(16, 16);
			this.picNb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.picNb.TabIndex = 20;
			this.picNb.TabStop = false;
			this.picNb.Visible = false;
			// 
			// txtProducts
			// 
			this.txtProducts.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtProducts.Location = new System.Drawing.Point(517, 306);
			this.txtProducts.Margin = new System.Windows.Forms.Padding(78, 3, 3, 3);
			this.txtProducts.Multiline = true;
			this.txtProducts.Name = "txtProducts";
			this.txtProducts.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtProducts.Size = new System.Drawing.Size(44, 22);
			this.txtProducts.TabIndex = 10;
			this.txtProducts.Visible = false;
			this.txtProducts.LocationChanged += new System.EventHandler(this.OnProductsTextBoxLocationSizeChanged);
			this.txtProducts.SizeChanged += new System.EventHandler(this.OnProductsTextBoxLocationSizeChanged);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(467, 334);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(100, 27);
			this.btnCancel.TabIndex = 14;
			this.btnCancel.Text = "Close";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// ConsignNingboForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(573, 370);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.picNb);
			this.Controls.Add(this.cboLogistics);
			this.Controls.Add(this.btnNingboList);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.txtProducts);
			this.Controls.Add(this.pnlMoney);
			this.Controls.Add(this.lblMoney);
			this.Controls.Add(this.lblPrint);
			this.Controls.Add(this.pnlPrint);
			this.Controls.Add(this.txtBuyerRemark);
			this.Controls.Add(this.lblBuyerRemark);
			this.Controls.Add(this.txtRemark);
			this.Controls.Add(this.lblRemark);
			this.Controls.Add(this.lblLogistics);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "ConsignNingboForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "宁波保税区出单";
			this.Load += new System.EventHandler(this.ConsignShForm_Load);
			this.Shown += new System.EventHandler(this.ConsignShForm_Shown);
			this.pnlAddress.ResumeLayout(false);
			this.pnlAddress.PerformLayout();
			this.pnlPrint.ResumeLayout(false);
			this.pnlPrint.PerformLayout();
			this.pnlProductList.ResumeLayout(false);
			this.tsAddProduct.ResumeLayout(false);
			this.tsAddProduct.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picNb)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblLogistics;
		private System.Windows.Forms.Label lblProducts;
		private System.Windows.Forms.Label lblAddress;
		private System.Windows.Forms.FlowLayoutPanel pnlAddress;
		private System.Windows.Forms.TextBox txtProvince;
		private System.Windows.Forms.Label lblProvince;
		private System.Windows.Forms.TextBox txtCity1;
		private System.Windows.Forms.Label lblCity1;
		private System.Windows.Forms.TextBox txtCity2;
		private System.Windows.Forms.Label lblCity2;
		private System.Windows.Forms.TextBox txtDistrict;
		private System.Windows.Forms.Label lblDistrict;
		private System.Windows.Forms.TextBox txtStreetAddress;
		private System.Windows.Forms.Label lblMobile;
		private System.Windows.Forms.TextBox txtMobile;
		private System.Windows.Forms.Label lblRecipientName;
		private System.Windows.Forms.TextBox txtRecipientName;
		private System.Windows.Forms.Label lblRemark;
		private System.Windows.Forms.TextBox txtRemark;
		private System.Windows.Forms.Label lblBuyerRemark;
		private System.Windows.Forms.TextBox txtBuyerRemark;
		private System.Windows.Forms.FlowLayoutPanel pnlPrint;
		private System.Windows.Forms.Label lblPrint;
		private System.Windows.Forms.Label lblMoney;
		private System.Windows.Forms.Panel pnlMoney;
		private System.Windows.Forms.TextBox txtIdInfo;
		private System.Windows.Forms.Label lblIdNumber;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnNingboList;
		private System.Windows.Forms.ComboBox cboLogistics;
		private System.Windows.Forms.FlowLayoutPanel pnlProductList;
		private System.Windows.Forms.ToolStrip tsAddProduct;
		private System.Windows.Forms.ToolStripButton tsbtnAddProduct;
		private System.Windows.Forms.PictureBox picNb;
		private System.Windows.Forms.Label lblWarning;
		private System.Windows.Forms.TextBox txtProducts;
		private System.Windows.Forms.Button btnCancel;
	}
}