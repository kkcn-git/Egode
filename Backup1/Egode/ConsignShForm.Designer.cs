namespace Egode
{
	partial class ConsignShForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsignShForm));
			this.btnPrint = new System.Windows.Forms.Button();
			this.lblBillNumber = new System.Windows.Forms.Label();
			this.txtBillNumber = new System.Windows.Forms.TextBox();
			this.lblProducts = new System.Windows.Forms.Label();
			this.txtProducts = new System.Windows.Forms.TextBox();
			this.btnGo = new System.Windows.Forms.Button();
			this.lblAddress = new System.Windows.Forms.Label();
			this.pnlAddress = new System.Windows.Forms.FlowLayoutPanel();
			this.lblRecipientName = new System.Windows.Forms.Label();
			this.txtRecipientName = new System.Windows.Forms.TextBox();
			this.txtBuyerAccount = new System.Windows.Forms.TextBox();
			this.lblDistributorFlag = new System.Windows.Forms.Label();
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
			this.lblPhone = new System.Windows.Forms.Label();
			this.txtPhone = new System.Windows.Forms.TextBox();
			this.btnSelectAddress = new System.Windows.Forms.Button();
			this.lblRemark = new System.Windows.Forms.Label();
			this.txtRemark = new System.Windows.Forms.TextBox();
			this.lblBuyerRemark = new System.Windows.Forms.Label();
			this.txtBuyerRemark = new System.Windows.Forms.TextBox();
			this.pnlConsign = new System.Windows.Forms.FlowLayoutPanel();
			this.pnlLogisticsCompanys = new System.Windows.Forms.FlowLayoutPanel();
			this.rdoYto = new System.Windows.Forms.RadioButton();
			this.rdoZto = new System.Windows.Forms.RadioButton();
			this.rdoYunda = new System.Windows.Forms.RadioButton();
			this.pnlSf = new System.Windows.Forms.FlowLayoutPanel();
			this.rdoSf = new System.Windows.Forms.RadioButton();
			this.chkFriehghtCollect = new System.Windows.Forms.CheckBox();
			this.chkPickup = new System.Windows.Forms.CheckBox();
			this.chkAutoSfBillNumber = new System.Windows.Forms.CheckBox();
			this.chkSfOldBill = new System.Windows.Forms.CheckBox();
			this.chkPartial = new System.Windows.Forms.CheckBox();
			this.txtDestCode = new System.Windows.Forms.TextBox();
			this.btnStockout = new System.Windows.Forms.Button();
			this.btnAppenRemark = new System.Windows.Forms.Button();
			this.pnlPrint = new System.Windows.Forms.FlowLayoutPanel();
			this.lblSender = new System.Windows.Forms.Label();
			this.pnlSender = new System.Windows.Forms.FlowLayoutPanel();
			this.lblSenderName = new System.Windows.Forms.Label();
			this.txtSenderName = new System.Windows.Forms.TextBox();
			this.lblSenderTel = new System.Windows.Forms.Label();
			this.txtSenderTel = new System.Windows.Forms.TextBox();
			this.txtHideShop = new System.Windows.Forms.Button();
			this.lblSenderAddr = new System.Windows.Forms.Label();
			this.txtSenderAddr = new System.Windows.Forms.TextBox();
			this.btnRegDistributor = new System.Windows.Forms.Button();
			this.lblSenderCompany = new System.Windows.Forms.Label();
			this.txtSenderAd = new System.Windows.Forms.TextBox();
			this.cboDistributors = new System.Windows.Forms.ComboBox();
			this.pnlProductList = new System.Windows.Forms.FlowLayoutPanel();
			this.tsAddProduct = new System.Windows.Forms.ToolStrip();
			this.tsbtnAddRossmannTablets = new System.Windows.Forms.ToolStripButton();
			this.tsbtnAddDmTablets = new System.Windows.Forms.ToolStripButton();
			this.tsbtnAddProduct = new System.Windows.Forms.ToolStripButton();
			this.chkHoliday = new System.Windows.Forms.CheckBox();
			this.lblUnknownProductWarning = new System.Windows.Forms.Label();
			this.lblPrint = new System.Windows.Forms.Label();
			this.lblMoney = new System.Windows.Forms.Label();
			this.pnlMoney = new System.Windows.Forms.Panel();
			this.pnlAddress.SuspendLayout();
			this.pnlConsign.SuspendLayout();
			this.pnlLogisticsCompanys.SuspendLayout();
			this.pnlSf.SuspendLayout();
			this.pnlPrint.SuspendLayout();
			this.pnlSender.SuspendLayout();
			this.pnlProductList.SuspendLayout();
			this.tsAddProduct.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnPrint
			// 
			this.btnPrint.BackColor = System.Drawing.SystemColors.HotTrack;
			this.btnPrint.ForeColor = System.Drawing.Color.White;
			this.btnPrint.Location = new System.Drawing.Point(40, 320);
			this.btnPrint.Margin = new System.Windows.Forms.Padding(40, 3, 3, 3);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(539, 30);
			this.btnPrint.TabIndex = 21;
			this.btnPrint.Text = "&Print";
			this.btnPrint.UseVisualStyleBackColor = false;
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// lblBillNumber
			// 
			this.lblBillNumber.AutoSize = true;
			this.lblBillNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblBillNumber.Location = new System.Drawing.Point(29, 469);
			this.lblBillNumber.Name = "lblBillNumber";
			this.lblBillNumber.Size = new System.Drawing.Size(35, 17);
			this.lblBillNumber.TabIndex = 2;
			this.lblBillNumber.Text = "单号:";
			// 
			// txtBillNumber
			// 
			this.txtBillNumber.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtBillNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtBillNumber.Location = new System.Drawing.Point(84, 35);
			this.txtBillNumber.Margin = new System.Windows.Forms.Padding(6, 0, 3, 3);
			this.txtBillNumber.Name = "txtBillNumber";
			this.txtBillNumber.Size = new System.Drawing.Size(507, 27);
			this.txtBillNumber.TabIndex = 32;
			this.txtBillNumber.TextChanged += new System.EventHandler(this.txtBillNumber_TextChanged);
			this.txtBillNumber.Enter += new System.EventHandler(this.txtBillNumber_Enter);
			this.txtBillNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBillNumber_KeyDown);
			this.txtBillNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBillNumber_KeyPress);
			this.txtBillNumber.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBillNumber_KeyUp);
			// 
			// lblProducts
			// 
			this.lblProducts.AutoSize = true;
			this.lblProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblProducts.Location = new System.Drawing.Point(3, 244);
			this.lblProducts.Margin = new System.Windows.Forms.Padding(3, 8, 1, 0);
			this.lblProducts.Name = "lblProducts";
			this.lblProducts.Size = new System.Drawing.Size(35, 17);
			this.lblProducts.TabIndex = 4;
			this.lblProducts.Text = "商品:";
			// 
			// txtProducts
			// 
			this.txtProducts.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtProducts.ForeColor = System.Drawing.Color.Gray;
			this.txtProducts.ImeMode = System.Windows.Forms.ImeMode.On;
			this.txtProducts.Location = new System.Drawing.Point(356, 239);
			this.txtProducts.Multiline = true;
			this.txtProducts.Name = "txtProducts";
			this.txtProducts.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtProducts.Size = new System.Drawing.Size(228, 75);
			this.txtProducts.TabIndex = 20;
			this.txtProducts.WordWrap = false;
			this.txtProducts.LocationChanged += new System.EventHandler(this.OnProductsTextBoxLocationSizeChanged);
			this.txtProducts.SizeChanged += new System.EventHandler(this.OnProductsTextBoxLocationSizeChanged);
			// 
			// btnGo
			// 
			this.btnGo.BackColor = System.Drawing.Color.Tomato;
			this.btnGo.Enabled = false;
			this.btnGo.ForeColor = System.Drawing.Color.White;
			this.btnGo.Location = new System.Drawing.Point(2, 108);
			this.btnGo.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(405, 30);
			this.btnGo.TabIndex = 24;
			this.btnGo.Text = "Go (输入单号并回车后自动执行点发货、出库)";
			this.btnGo.UseVisualStyleBackColor = false;
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// lblAddress
			// 
			this.lblAddress.AutoSize = true;
			this.lblAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblAddress.Location = new System.Drawing.Point(3, 113);
			this.lblAddress.Margin = new System.Windows.Forms.Padding(3, 10, 1, 0);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(35, 17);
			this.lblAddress.TabIndex = 7;
			this.lblAddress.Text = "收件:";
			// 
			// pnlAddress
			// 
			this.pnlAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(188)))), ((int)(((byte)(231)))));
			this.pnlAddress.Controls.Add(this.lblRecipientName);
			this.pnlAddress.Controls.Add(this.txtRecipientName);
			this.pnlAddress.Controls.Add(this.txtBuyerAccount);
			this.pnlAddress.Controls.Add(this.lblDistributorFlag);
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
			this.pnlAddress.Controls.Add(this.lblPhone);
			this.pnlAddress.Controls.Add(this.txtPhone);
			this.pnlAddress.Controls.Add(this.btnSelectAddress);
			this.pnlPrint.SetFlowBreak(this.pnlAddress, true);
			this.pnlAddress.Location = new System.Drawing.Point(42, 106);
			this.pnlAddress.Name = "pnlAddress";
			this.pnlAddress.Size = new System.Drawing.Size(542, 127);
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
			this.txtRecipientName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtRecipientName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtRecipientName.ImeMode = System.Windows.Forms.ImeMode.On;
			this.txtRecipientName.Location = new System.Drawing.Point(75, 6);
			this.txtRecipientName.Margin = new System.Windows.Forms.Padding(0, 6, 3, 6);
			this.txtRecipientName.Name = "txtRecipientName";
			this.txtRecipientName.Size = new System.Drawing.Size(155, 23);
			this.txtRecipientName.TabIndex = 9;
			// 
			// txtBuyerAccount
			// 
			this.txtBuyerAccount.BackColor = System.Drawing.SystemColors.Window;
			this.txtBuyerAccount.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtBuyerAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
			this.txtBuyerAccount.ImeMode = System.Windows.Forms.ImeMode.On;
			this.txtBuyerAccount.Location = new System.Drawing.Point(236, 6);
			this.txtBuyerAccount.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.txtBuyerAccount.Name = "txtBuyerAccount";
			this.txtBuyerAccount.Size = new System.Drawing.Size(248, 23);
			this.txtBuyerAccount.TabIndex = 10;
			// 
			// lblDistributorFlag
			// 
			this.lblDistributorFlag.AutoSize = true;
			this.lblDistributorFlag.BackColor = System.Drawing.Color.MediumSeaGreen;
			this.lblDistributorFlag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlAddress.SetFlowBreak(this.lblDistributorFlag, true);
			this.lblDistributorFlag.ForeColor = System.Drawing.Color.White;
			this.lblDistributorFlag.Location = new System.Drawing.Point(490, 9);
			this.lblDistributorFlag.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
			this.lblDistributorFlag.Name = "lblDistributorFlag";
			this.lblDistributorFlag.Size = new System.Drawing.Size(34, 19);
			this.lblDistributorFlag.TabIndex = 14;
			this.lblDistributorFlag.Text = "代发";
			this.lblDistributorFlag.Visible = false;
			// 
			// txtProvince
			// 
			this.txtProvince.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtProvince.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtProvince.ImeMode = System.Windows.Forms.ImeMode.On;
			this.txtProvince.Location = new System.Drawing.Point(6, 35);
			this.txtProvince.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.txtProvince.Name = "txtProvince";
			this.txtProvince.Size = new System.Drawing.Size(100, 23);
			this.txtProvince.TabIndex = 11;
			// 
			// lblProvince
			// 
			this.lblProvince.AutoSize = true;
			this.lblProvince.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblProvince.Location = new System.Drawing.Point(106, 38);
			this.lblProvince.Margin = new System.Windows.Forms.Padding(0, 3, 8, 0);
			this.lblProvince.Name = "lblProvince";
			this.lblProvince.Size = new System.Drawing.Size(20, 17);
			this.lblProvince.TabIndex = 1;
			this.lblProvince.Text = "省";
			// 
			// txtCity1
			// 
			this.txtCity1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtCity1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtCity1.ImeMode = System.Windows.Forms.ImeMode.On;
			this.txtCity1.Location = new System.Drawing.Point(137, 35);
			this.txtCity1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.txtCity1.Name = "txtCity1";
			this.txtCity1.Size = new System.Drawing.Size(93, 23);
			this.txtCity1.TabIndex = 12;
			// 
			// lblCity1
			// 
			this.lblCity1.AutoSize = true;
			this.lblCity1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblCity1.Location = new System.Drawing.Point(230, 38);
			this.lblCity1.Margin = new System.Windows.Forms.Padding(0, 3, 8, 0);
			this.lblCity1.Name = "lblCity1";
			this.lblCity1.Size = new System.Drawing.Size(20, 17);
			this.lblCity1.TabIndex = 3;
			this.lblCity1.Text = "市";
			// 
			// txtCity2
			// 
			this.txtCity2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtCity2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtCity2.ImeMode = System.Windows.Forms.ImeMode.On;
			this.txtCity2.Location = new System.Drawing.Point(261, 35);
			this.txtCity2.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.txtCity2.Name = "txtCity2";
			this.txtCity2.Size = new System.Drawing.Size(90, 23);
			this.txtCity2.TabIndex = 13;
			// 
			// lblCity2
			// 
			this.lblCity2.AutoSize = true;
			this.lblCity2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblCity2.Location = new System.Drawing.Point(351, 38);
			this.lblCity2.Margin = new System.Windows.Forms.Padding(0, 3, 8, 0);
			this.lblCity2.Name = "lblCity2";
			this.lblCity2.Size = new System.Drawing.Size(20, 17);
			this.lblCity2.TabIndex = 5;
			this.lblCity2.Text = "县";
			// 
			// txtDistrict
			// 
			this.txtDistrict.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtDistrict.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtDistrict.ImeMode = System.Windows.Forms.ImeMode.On;
			this.txtDistrict.Location = new System.Drawing.Point(382, 35);
			this.txtDistrict.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.txtDistrict.Name = "txtDistrict";
			this.txtDistrict.Size = new System.Drawing.Size(102, 23);
			this.txtDistrict.TabIndex = 14;
			// 
			// lblDistrict
			// 
			this.lblDistrict.AutoSize = true;
			this.lblDistrict.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblDistrict.Location = new System.Drawing.Point(484, 38);
			this.lblDistrict.Margin = new System.Windows.Forms.Padding(0, 3, 8, 0);
			this.lblDistrict.Name = "lblDistrict";
			this.lblDistrict.Size = new System.Drawing.Size(40, 17);
			this.lblDistrict.TabIndex = 7;
			this.lblDistrict.Text = "区(镇)";
			// 
			// txtStreetAddress
			// 
			this.pnlAddress.SetFlowBreak(this.txtStreetAddress, true);
			this.txtStreetAddress.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtStreetAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtStreetAddress.ImeMode = System.Windows.Forms.ImeMode.On;
			this.txtStreetAddress.Location = new System.Drawing.Point(6, 66);
			this.txtStreetAddress.Margin = new System.Windows.Forms.Padding(6, 8, 3, 3);
			this.txtStreetAddress.Name = "txtStreetAddress";
			this.txtStreetAddress.Size = new System.Drawing.Size(478, 23);
			this.txtStreetAddress.TabIndex = 15;
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
			this.txtMobile.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtMobile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtMobile.ImeMode = System.Windows.Forms.ImeMode.On;
			this.txtMobile.Location = new System.Drawing.Point(62, 95);
			this.txtMobile.Margin = new System.Windows.Forms.Padding(0, 3, 16, 3);
			this.txtMobile.Name = "txtMobile";
			this.txtMobile.Size = new System.Drawing.Size(153, 23);
			this.txtMobile.TabIndex = 16;
			// 
			// lblPhone
			// 
			this.lblPhone.AutoSize = true;
			this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblPhone.Location = new System.Drawing.Point(243, 98);
			this.lblPhone.Margin = new System.Windows.Forms.Padding(12, 6, 0, 0);
			this.lblPhone.Name = "lblPhone";
			this.lblPhone.Size = new System.Drawing.Size(59, 17);
			this.lblPhone.TabIndex = 11;
			this.lblPhone.Text = "固定电话:";
			// 
			// txtPhone
			// 
			this.txtPhone.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtPhone.ImeMode = System.Windows.Forms.ImeMode.On;
			this.txtPhone.Location = new System.Drawing.Point(302, 95);
			this.txtPhone.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.txtPhone.Name = "txtPhone";
			this.txtPhone.Size = new System.Drawing.Size(182, 23);
			this.txtPhone.TabIndex = 17;
			// 
			// btnSelectAddress
			// 
			this.btnSelectAddress.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSelectAddress.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnSelectAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.btnSelectAddress.Location = new System.Drawing.Point(490, 95);
			this.btnSelectAddress.Name = "btnSelectAddress";
			this.btnSelectAddress.Size = new System.Drawing.Size(36, 23);
			this.btnSelectAddress.TabIndex = 18;
			this.btnSelectAddress.Text = "@";
			this.btnSelectAddress.UseVisualStyleBackColor = true;
			this.btnSelectAddress.Click += new System.EventHandler(this.btnSelectAddress_Click);
			// 
			// lblRemark
			// 
			this.lblRemark.AutoSize = true;
			this.lblRemark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblRemark.Location = new System.Drawing.Point(3, 45);
			this.lblRemark.Name = "lblRemark";
			this.lblRemark.Size = new System.Drawing.Size(59, 17);
			this.lblRemark.TabIndex = 10;
			this.lblRemark.Text = "客服备注:";
			// 
			// txtRemark
			// 
			this.txtRemark.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtRemark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtRemark.Location = new System.Drawing.Point(66, 41);
			this.txtRemark.Multiline = true;
			this.txtRemark.Name = "txtRemark";
			this.txtRemark.Size = new System.Drawing.Size(598, 25);
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
			this.txtBuyerRemark.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtBuyerRemark.ForeColor = System.Drawing.Color.Red;
			this.txtBuyerRemark.Location = new System.Drawing.Point(66, 8);
			this.txtBuyerRemark.Multiline = true;
			this.txtBuyerRemark.Name = "txtBuyerRemark";
			this.txtBuyerRemark.Size = new System.Drawing.Size(598, 25);
			this.txtBuyerRemark.TabIndex = 0;
			// 
			// pnlConsign
			// 
			this.pnlConsign.BackColor = System.Drawing.Color.White;
			this.pnlConsign.Controls.Add(this.pnlLogisticsCompanys);
			this.pnlConsign.Controls.Add(this.pnlSf);
			this.pnlConsign.Controls.Add(this.chkPartial);
			this.pnlConsign.Controls.Add(this.txtBillNumber);
			this.pnlConsign.Controls.Add(this.txtDestCode);
			this.pnlConsign.Controls.Add(this.btnGo);
			this.pnlConsign.Controls.Add(this.btnStockout);
			this.pnlConsign.Controls.Add(this.btnAppenRemark);
			this.pnlConsign.Location = new System.Drawing.Point(66, 463);
			this.pnlConsign.Name = "pnlConsign";
			this.pnlConsign.Size = new System.Drawing.Size(598, 109);
			this.pnlConsign.TabIndex = 14;
			this.pnlConsign.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlConsign_Paint);
			// 
			// pnlLogisticsCompanys
			// 
			this.pnlLogisticsCompanys.AutoSize = true;
			this.pnlLogisticsCompanys.Controls.Add(this.rdoYto);
			this.pnlLogisticsCompanys.Controls.Add(this.rdoZto);
			this.pnlLogisticsCompanys.Controls.Add(this.rdoYunda);
			this.pnlLogisticsCompanys.Location = new System.Drawing.Point(6, 1);
			this.pnlLogisticsCompanys.Margin = new System.Windows.Forms.Padding(6, 1, 3, 0);
			this.pnlLogisticsCompanys.Name = "pnlLogisticsCompanys";
			this.pnlLogisticsCompanys.Size = new System.Drawing.Size(156, 32);
			this.pnlLogisticsCompanys.TabIndex = 0;
			// 
			// rdoYto
			// 
			this.rdoYto.AutoSize = true;
			this.rdoYto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.rdoYto.Location = new System.Drawing.Point(0, 8);
			this.rdoYto.Margin = new System.Windows.Forms.Padding(0, 8, 0, 3);
			this.rdoYto.Name = "rdoYto";
			this.rdoYto.Size = new System.Drawing.Size(50, 21);
			this.rdoYto.TabIndex = 23;
			this.rdoYto.Text = "圆通";
			this.rdoYto.UseVisualStyleBackColor = true;
			this.rdoYto.Visible = false;
			this.rdoYto.CheckedChanged += new System.EventHandler(this.rdoYto_CheckedChanged);
			// 
			// rdoZto
			// 
			this.rdoZto.AutoSize = true;
			this.rdoZto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.rdoZto.Location = new System.Drawing.Point(50, 8);
			this.rdoZto.Margin = new System.Windows.Forms.Padding(0, 8, 0, 3);
			this.rdoZto.Name = "rdoZto";
			this.rdoZto.Size = new System.Drawing.Size(50, 21);
			this.rdoZto.TabIndex = 24;
			this.rdoZto.Text = "中通";
			this.rdoZto.UseVisualStyleBackColor = true;
			this.rdoZto.CheckedChanged += new System.EventHandler(this.rdoZto_CheckedChanged);
			// 
			// rdoYunda
			// 
			this.rdoYunda.AutoSize = true;
			this.rdoYunda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.rdoYunda.Location = new System.Drawing.Point(106, 8);
			this.rdoYunda.Margin = new System.Windows.Forms.Padding(6, 8, 0, 3);
			this.rdoYunda.Name = "rdoYunda";
			this.rdoYunda.Size = new System.Drawing.Size(50, 21);
			this.rdoYunda.TabIndex = 25;
			this.rdoYunda.Text = "韵达";
			this.rdoYunda.UseVisualStyleBackColor = true;
			this.rdoYunda.CheckedChanged += new System.EventHandler(this.rdoYunda_CheckedChanged);
			// 
			// pnlSf
			// 
			this.pnlSf.BackColor = System.Drawing.Color.Gainsboro;
			this.pnlSf.Controls.Add(this.rdoSf);
			this.pnlSf.Controls.Add(this.chkFriehghtCollect);
			this.pnlSf.Controls.Add(this.chkPickup);
			this.pnlSf.Controls.Add(this.chkAutoSfBillNumber);
			this.pnlSf.Controls.Add(this.chkSfOldBill);
			this.pnlSf.Location = new System.Drawing.Point(166, 4);
			this.pnlSf.Margin = new System.Windows.Forms.Padding(1, 4, 3, 3);
			this.pnlSf.Name = "pnlSf";
			this.pnlSf.Size = new System.Drawing.Size(373, 28);
			this.pnlSf.TabIndex = 23;
			this.pnlSf.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlSf_Paint);
			// 
			// rdoSf
			// 
			this.rdoSf.AutoSize = true;
			this.rdoSf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.rdoSf.Location = new System.Drawing.Point(4, 5);
			this.rdoSf.Margin = new System.Windows.Forms.Padding(4, 5, 0, 3);
			this.rdoSf.Name = "rdoSf";
			this.rdoSf.Size = new System.Drawing.Size(50, 21);
			this.rdoSf.TabIndex = 26;
			this.rdoSf.Text = "顺丰";
			this.rdoSf.UseVisualStyleBackColor = true;
			this.rdoSf.CheckedChanged += new System.EventHandler(this.rdoSf_CheckedChanged);
			// 
			// chkFriehghtCollect
			// 
			this.chkFriehghtCollect.AutoSize = true;
			this.chkFriehghtCollect.Enabled = false;
			this.chkFriehghtCollect.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkFriehghtCollect.Location = new System.Drawing.Point(60, 4);
			this.chkFriehghtCollect.Margin = new System.Windows.Forms.Padding(6, 4, 0, 3);
			this.chkFriehghtCollect.Name = "chkFriehghtCollect";
			this.chkFriehghtCollect.Size = new System.Drawing.Size(54, 23);
			this.chkFriehghtCollect.TabIndex = 27;
			this.chkFriehghtCollect.Text = "到付";
			this.chkFriehghtCollect.UseVisualStyleBackColor = true;
			// 
			// chkPickup
			// 
			this.chkPickup.AutoSize = true;
			this.chkPickup.Enabled = false;
			this.chkPickup.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkPickup.Location = new System.Drawing.Point(120, 4);
			this.chkPickup.Margin = new System.Windows.Forms.Padding(6, 4, 3, 3);
			this.chkPickup.Name = "chkPickup";
			this.chkPickup.Size = new System.Drawing.Size(54, 23);
			this.chkPickup.TabIndex = 28;
			this.chkPickup.Text = "自取";
			this.chkPickup.UseVisualStyleBackColor = true;
			// 
			// chkAutoSfBillNumber
			// 
			this.chkAutoSfBillNumber.AutoSize = true;
			this.chkAutoSfBillNumber.Checked = true;
			this.chkAutoSfBillNumber.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAutoSfBillNumber.Location = new System.Drawing.Point(194, 5);
			this.chkAutoSfBillNumber.Margin = new System.Windows.Forms.Padding(17, 5, 0, 3);
			this.chkAutoSfBillNumber.Name = "chkAutoSfBillNumber";
			this.chkAutoSfBillNumber.Size = new System.Drawing.Size(99, 21);
			this.chkAutoSfBillNumber.TabIndex = 29;
			this.chkAutoSfBillNumber.Text = "自动生成单号";
			this.chkAutoSfBillNumber.UseVisualStyleBackColor = true;
			// 
			// chkSfOldBill
			// 
			this.chkSfOldBill.AutoSize = true;
			this.chkSfOldBill.BackColor = System.Drawing.Color.Transparent;
			this.chkSfOldBill.Enabled = false;
			this.chkSfOldBill.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkSfOldBill.ForeColor = System.Drawing.Color.MediumOrchid;
			this.chkSfOldBill.Location = new System.Drawing.Point(293, 4);
			this.chkSfOldBill.Margin = new System.Windows.Forms.Padding(0, 4, 0, 3);
			this.chkSfOldBill.Name = "chkSfOldBill";
			this.chkSfOldBill.Size = new System.Drawing.Size(80, 23);
			this.chkSfOldBill.TabIndex = 30;
			this.chkSfOldBill.Text = "传统面单";
			this.chkSfOldBill.UseVisualStyleBackColor = false;
			// 
			// chkPartial
			// 
			this.chkPartial.AutoSize = true;
			this.chkPartial.Location = new System.Drawing.Point(0, 48);
			this.chkPartial.Margin = new System.Windows.Forms.Padding(0, 13, 3, 0);
			this.chkPartial.Name = "chkPartial";
			this.chkPartial.Size = new System.Drawing.Size(75, 21);
			this.chkPartial.TabIndex = 31;
			this.chkPartial.Text = "部分发货";
			this.chkPartial.UseVisualStyleBackColor = true;
			// 
			// txtDestCode
			// 
			this.pnlConsign.SetFlowBreak(this.txtDestCode, true);
			this.txtDestCode.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtDestCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtDestCode.Location = new System.Drawing.Point(0, 69);
			this.txtDestCode.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
			this.txtDestCode.Name = "txtDestCode";
			this.txtDestCode.Size = new System.Drawing.Size(75, 27);
			this.txtDestCode.TabIndex = 33;
			// 
			// btnStockout
			// 
			this.btnStockout.BackColor = System.Drawing.SystemColors.HotTrack;
			this.btnStockout.ForeColor = System.Drawing.Color.White;
			this.btnStockout.Location = new System.Drawing.Point(410, 108);
			this.btnStockout.Margin = new System.Windows.Forms.Padding(0, 3, 2, 3);
			this.btnStockout.Name = "btnStockout";
			this.btnStockout.Size = new System.Drawing.Size(96, 30);
			this.btnStockout.TabIndex = 25;
			this.btnStockout.Text = "手动出库";
			this.btnStockout.UseVisualStyleBackColor = false;
			this.btnStockout.Click += new System.EventHandler(this.btnStockout_Click);
			// 
			// btnAppenRemark
			// 
			this.btnAppenRemark.BackColor = System.Drawing.SystemColors.HotTrack;
			this.btnAppenRemark.ForeColor = System.Drawing.Color.White;
			this.btnAppenRemark.Location = new System.Drawing.Point(509, 108);
			this.btnAppenRemark.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
			this.btnAppenRemark.Name = "btnAppenRemark";
			this.btnAppenRemark.Size = new System.Drawing.Size(80, 30);
			this.btnAppenRemark.TabIndex = 26;
			this.btnAppenRemark.Text = "添加备注";
			this.btnAppenRemark.UseVisualStyleBackColor = false;
			this.btnAppenRemark.Click += new System.EventHandler(this.btnAppenRemark_Click);
			// 
			// pnlPrint
			// 
			this.pnlPrint.BackColor = System.Drawing.Color.White;
			this.pnlPrint.Controls.Add(this.lblSender);
			this.pnlPrint.Controls.Add(this.pnlSender);
			this.pnlPrint.Controls.Add(this.lblAddress);
			this.pnlPrint.Controls.Add(this.pnlAddress);
			this.pnlPrint.Controls.Add(this.lblProducts);
			this.pnlPrint.Controls.Add(this.pnlProductList);
			this.pnlPrint.Controls.Add(this.txtProducts);
			this.pnlPrint.Controls.Add(this.btnPrint);
			this.pnlPrint.Controls.Add(this.chkHoliday);
			this.pnlPrint.Controls.Add(this.lblUnknownProductWarning);
			this.pnlPrint.Location = new System.Drawing.Point(66, 74);
			this.pnlPrint.Name = "pnlPrint";
			this.pnlPrint.Size = new System.Drawing.Size(598, 382);
			this.pnlPrint.TabIndex = 2;
			this.pnlPrint.LocationChanged += new System.EventHandler(this.OnProductsTextBoxLocationSizeChanged);
			this.pnlPrint.SizeChanged += new System.EventHandler(this.OnProductsTextBoxLocationSizeChanged);
			this.pnlPrint.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPrint_Paint);
			// 
			// lblSender
			// 
			this.lblSender.AutoSize = true;
			this.lblSender.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblSender.Location = new System.Drawing.Point(3, 13);
			this.lblSender.Margin = new System.Windows.Forms.Padding(3, 13, 1, 0);
			this.lblSender.Name = "lblSender";
			this.lblSender.Size = new System.Drawing.Size(35, 17);
			this.lblSender.TabIndex = 20;
			this.lblSender.Text = "发件:";
			// 
			// pnlSender
			// 
			this.pnlSender.BackColor = System.Drawing.Color.Transparent;
			this.pnlSender.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlSender.Controls.Add(this.lblSenderName);
			this.pnlSender.Controls.Add(this.txtSenderName);
			this.pnlSender.Controls.Add(this.lblSenderTel);
			this.pnlSender.Controls.Add(this.txtSenderTel);
			this.pnlSender.Controls.Add(this.txtHideShop);
			this.pnlSender.Controls.Add(this.lblSenderAddr);
			this.pnlSender.Controls.Add(this.txtSenderAddr);
			this.pnlSender.Controls.Add(this.btnRegDistributor);
			this.pnlSender.Controls.Add(this.lblSenderCompany);
			this.pnlSender.Controls.Add(this.txtSenderAd);
			this.pnlSender.Controls.Add(this.cboDistributors);
			this.pnlPrint.SetFlowBreak(this.pnlSender, true);
			this.pnlSender.Location = new System.Drawing.Point(42, 3);
			this.pnlSender.Name = "pnlSender";
			this.pnlSender.Size = new System.Drawing.Size(542, 97);
			this.pnlSender.TabIndex = 2;
			// 
			// lblSenderName
			// 
			this.lblSenderName.AutoSize = true;
			this.lblSenderName.ForeColor = System.Drawing.Color.OrangeRed;
			this.lblSenderName.Location = new System.Drawing.Point(3, 9);
			this.lblSenderName.Margin = new System.Windows.Forms.Padding(3, 9, 1, 0);
			this.lblSenderName.Name = "lblSenderName";
			this.lblSenderName.Size = new System.Drawing.Size(59, 17);
			this.lblSenderName.TabIndex = 0;
			this.lblSenderName.Text = "寄  件 人:";
			// 
			// txtSenderName
			// 
			this.txtSenderName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtSenderName.ImeMode = System.Windows.Forms.ImeMode.On;
			this.txtSenderName.Location = new System.Drawing.Point(63, 6);
			this.txtSenderName.Margin = new System.Windows.Forms.Padding(0, 6, 3, 3);
			this.txtSenderName.Name = "txtSenderName";
			this.txtSenderName.Size = new System.Drawing.Size(120, 23);
			this.txtSenderName.TabIndex = 2;
			// 
			// lblSenderTel
			// 
			this.lblSenderTel.AutoSize = true;
			this.lblSenderTel.ForeColor = System.Drawing.Color.OrangeRed;
			this.lblSenderTel.Location = new System.Drawing.Point(198, 9);
			this.lblSenderTel.Margin = new System.Windows.Forms.Padding(12, 9, 0, 0);
			this.lblSenderTel.Name = "lblSenderTel";
			this.lblSenderTel.Size = new System.Drawing.Size(80, 17);
			this.lblSenderTel.TabIndex = 4;
			this.lblSenderTel.Text = "*寄件人电话: ";
			// 
			// txtSenderTel
			// 
			this.txtSenderTel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtSenderTel.ImeMode = System.Windows.Forms.ImeMode.On;
			this.txtSenderTel.Location = new System.Drawing.Point(278, 6);
			this.txtSenderTel.Margin = new System.Windows.Forms.Padding(0, 6, 3, 3);
			this.txtSenderTel.Name = "txtSenderTel";
			this.txtSenderTel.Size = new System.Drawing.Size(119, 23);
			this.txtSenderTel.TabIndex = 3;
			// 
			// txtHideShop
			// 
			this.pnlSender.SetFlowBreak(this.txtHideShop, true);
			this.txtHideShop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtHideShop.Location = new System.Drawing.Point(403, 6);
			this.txtHideShop.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.txtHideShop.Name = "txtHideShop";
			this.txtHideShop.Size = new System.Drawing.Size(128, 23);
			this.txtHideShop.TabIndex = 6;
			this.txtHideShop.Text = "不显示店铺信息";
			this.txtHideShop.UseVisualStyleBackColor = true;
			this.txtHideShop.Click += new System.EventHandler(this.txtHideShop_Click);
			// 
			// lblSenderAddr
			// 
			this.lblSenderAddr.AutoSize = true;
			this.lblSenderAddr.ForeColor = System.Drawing.Color.OrangeRed;
			this.lblSenderAddr.Location = new System.Drawing.Point(3, 41);
			this.lblSenderAddr.Margin = new System.Windows.Forms.Padding(3, 9, 1, 0);
			this.lblSenderAddr.Name = "lblSenderAddr";
			this.lblSenderAddr.Size = new System.Drawing.Size(59, 17);
			this.lblSenderAddr.TabIndex = 8;
			this.lblSenderAddr.Text = "寄件地址:";
			// 
			// txtSenderAddr
			// 
			this.txtSenderAddr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtSenderAddr.Location = new System.Drawing.Point(63, 38);
			this.txtSenderAddr.Margin = new System.Windows.Forms.Padding(0, 6, 3, 3);
			this.txtSenderAddr.Name = "txtSenderAddr";
			this.txtSenderAddr.Size = new System.Drawing.Size(334, 23);
			this.txtSenderAddr.TabIndex = 4;
			// 
			// btnRegDistributor
			// 
			this.btnRegDistributor.Location = new System.Drawing.Point(403, 35);
			this.btnRegDistributor.Name = "btnRegDistributor";
			this.btnRegDistributor.Size = new System.Drawing.Size(128, 23);
			this.btnRegDistributor.TabIndex = 7;
			this.btnRegDistributor.Text = "登记代发信息";
			this.btnRegDistributor.UseVisualStyleBackColor = true;
			this.btnRegDistributor.Click += new System.EventHandler(this.btnRegDistributor_Click);
			// 
			// lblSenderCompany
			// 
			this.lblSenderCompany.AutoSize = true;
			this.lblSenderCompany.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblSenderCompany.Location = new System.Drawing.Point(3, 70);
			this.lblSenderCompany.Margin = new System.Windows.Forms.Padding(3, 6, 1, 0);
			this.lblSenderCompany.Name = "lblSenderCompany";
			this.lblSenderCompany.Size = new System.Drawing.Size(161, 17);
			this.lblSenderCompany.TabIndex = 2;
			this.lblSenderCompany.Text = "寄件公司/广告/链接(非必填):";
			// 
			// txtSenderAd
			// 
			this.txtSenderAd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtSenderAd.ImeMode = System.Windows.Forms.ImeMode.On;
			this.txtSenderAd.Location = new System.Drawing.Point(168, 67);
			this.txtSenderAd.Name = "txtSenderAd";
			this.txtSenderAd.Size = new System.Drawing.Size(229, 23);
			this.txtSenderAd.TabIndex = 5;
			// 
			// cboDistributors
			// 
			this.cboDistributors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDistributors.DropDownWidth = 280;
			this.cboDistributors.FormattingEnabled = true;
			this.cboDistributors.Location = new System.Drawing.Point(403, 67);
			this.cboDistributors.Name = "cboDistributors";
			this.cboDistributors.Size = new System.Drawing.Size(128, 25);
			this.cboDistributors.TabIndex = 8;
			this.cboDistributors.SelectedIndexChanged += new System.EventHandler(this.cboDistributors_SelectedIndexChanged);
			// 
			// pnlProductList
			// 
			this.pnlProductList.Controls.Add(this.tsAddProduct);
			this.pnlProductList.Location = new System.Drawing.Point(42, 239);
			this.pnlProductList.Name = "pnlProductList";
			this.pnlProductList.Size = new System.Drawing.Size(308, 75);
			this.pnlProductList.TabIndex = 19;
			this.pnlProductList.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlProductList_Paint);
			// 
			// tsAddProduct
			// 
			this.tsAddProduct.AutoSize = false;
			this.tsAddProduct.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsAddProduct.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsbtnAddRossmannTablets,
			this.tsbtnAddDmTablets,
			this.tsbtnAddProduct});
			this.tsAddProduct.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.tsAddProduct.Location = new System.Drawing.Point(205, 0);
			this.tsAddProduct.Margin = new System.Windows.Forms.Padding(205, 0, 0, 0);
			this.tsAddProduct.Name = "tsAddProduct";
			this.tsAddProduct.Size = new System.Drawing.Size(100, 24);
			this.tsAddProduct.TabIndex = 0;
			this.tsAddProduct.Text = "Tool bar of adding products";
			// 
			// tsbtnAddRossmannTablets
			// 
			this.tsbtnAddRossmannTablets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbtnAddRossmannTablets.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tsbtnAddRossmannTablets.ForeColor = System.Drawing.Color.Maroon;
			this.tsbtnAddRossmannTablets.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAddRossmannTablets.Image")));
			this.tsbtnAddRossmannTablets.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnAddRossmannTablets.Name = "tsbtnAddRossmannTablets";
			this.tsbtnAddRossmannTablets.Size = new System.Drawing.Size(32, 20);
			this.tsbtnAddRossmannTablets.Text = "+R";
			this.tsbtnAddRossmannTablets.ToolTipText = "Add Rossmann Tablets";
			this.tsbtnAddRossmannTablets.Click += new System.EventHandler(this.tsbtnAddRossmannTablets_Click);
			// 
			// tsbtnAddDmTablets
			// 
			this.tsbtnAddDmTablets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbtnAddDmTablets.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tsbtnAddDmTablets.ForeColor = System.Drawing.Color.Maroon;
			this.tsbtnAddDmTablets.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAddDmTablets.Image")));
			this.tsbtnAddDmTablets.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnAddDmTablets.Name = "tsbtnAddDmTablets";
			this.tsbtnAddDmTablets.Size = new System.Drawing.Size(42, 20);
			this.tsbtnAddDmTablets.Text = "+dm";
			this.tsbtnAddDmTablets.Click += new System.EventHandler(this.tsbtnAddDmTablets_Click);
			// 
			// tsbtnAddProduct
			// 
			this.tsbtnAddProduct.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnAddProduct.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAddProduct.Image")));
			this.tsbtnAddProduct.Name = "tsbtnAddProduct";
			this.tsbtnAddProduct.Size = new System.Drawing.Size(23, 20);
			this.tsbtnAddProduct.Text = "Add Product";
			this.tsbtnAddProduct.Click += new System.EventHandler(this.tsbtnAddProduct_Click);
			// 
			// chkHoliday
			// 
			this.chkHoliday.AutoSize = true;
			this.chkHoliday.Checked = true;
			this.chkHoliday.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkHoliday.ForeColor = System.Drawing.Color.ForestGreen;
			this.chkHoliday.Location = new System.Drawing.Point(42, 356);
			this.chkHoliday.Margin = new System.Windows.Forms.Padding(42, 3, 3, 0);
			this.chkHoliday.Name = "chkHoliday";
			this.chkHoliday.Size = new System.Drawing.Size(87, 21);
			this.chkHoliday.TabIndex = 22;
			this.chkHoliday.Text = "节假日派送";
			this.chkHoliday.UseVisualStyleBackColor = true;
			// 
			// lblUnknownProductWarning
			// 
			this.lblUnknownProductWarning.AutoSize = true;
			this.lblUnknownProductWarning.BackColor = System.Drawing.SystemColors.Info;
			this.lblUnknownProductWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblUnknownProductWarning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.lblUnknownProductWarning.Location = new System.Drawing.Point(196, 357);
			this.lblUnknownProductWarning.Margin = new System.Windows.Forms.Padding(64, 4, 3, 0);
			this.lblUnknownProductWarning.Name = "lblUnknownProductWarning";
			this.lblUnknownProductWarning.Size = new System.Drawing.Size(45, 19);
			this.lblUnknownProductWarning.TabIndex = 22;
			this.lblUnknownProductWarning.Text = "label1";
			this.lblUnknownProductWarning.Visible = false;
			// 
			// lblPrint
			// 
			this.lblPrint.AutoSize = true;
			this.lblPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblPrint.Location = new System.Drawing.Point(2, 86);
			this.lblPrint.Name = "lblPrint";
			this.lblPrint.Size = new System.Drawing.Size(59, 17);
			this.lblPrint.TabIndex = 16;
			this.lblPrint.Text = "打印面单:";
			// 
			// lblMoney
			// 
			this.lblMoney.AutoSize = true;
			this.lblMoney.BackColor = System.Drawing.SystemColors.Window;
			this.lblMoney.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblMoney.ForeColor = System.Drawing.Color.OrangeRed;
			this.lblMoney.Location = new System.Drawing.Point(0, 158);
			this.lblMoney.Name = "lblMoney";
			this.lblMoney.Size = new System.Drawing.Size(75, 22);
			this.lblMoney.TabIndex = 17;
			this.lblMoney.Text = "0000.00";
			this.lblMoney.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblMoney.SizeChanged += new System.EventHandler(this.OnProductsTextBoxLocationSizeChanged);
			// 
			// pnlMoney
			// 
			this.pnlMoney.BackColor = System.Drawing.Color.White;
			this.pnlMoney.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pnlMoney.ForeColor = System.Drawing.Color.OrangeRed;
			this.pnlMoney.Location = new System.Drawing.Point(7, 187);
			this.pnlMoney.Name = "pnlMoney";
			this.pnlMoney.Size = new System.Drawing.Size(31, 87);
			this.pnlMoney.TabIndex = 18;
			this.pnlMoney.Visible = false;
			this.pnlMoney.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMoney_Paint);
			// 
			// ConsignShForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(674, 583);
			this.Controls.Add(this.pnlMoney);
			this.Controls.Add(this.lblMoney);
			this.Controls.Add(this.lblPrint);
			this.Controls.Add(this.pnlPrint);
			this.Controls.Add(this.pnlConsign);
			this.Controls.Add(this.txtBuyerRemark);
			this.Controls.Add(this.lblBuyerRemark);
			this.Controls.Add(this.txtRemark);
			this.Controls.Add(this.lblRemark);
			this.Controls.Add(this.lblBillNumber);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "ConsignShForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "发货 - 上海";
			this.Load += new System.EventHandler(this.ConsignShForm_Load);
			this.Shown += new System.EventHandler(this.ConsignShForm_Shown);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ConsignShForm_KeyPress);
			this.pnlAddress.ResumeLayout(false);
			this.pnlAddress.PerformLayout();
			this.pnlConsign.ResumeLayout(false);
			this.pnlConsign.PerformLayout();
			this.pnlLogisticsCompanys.ResumeLayout(false);
			this.pnlLogisticsCompanys.PerformLayout();
			this.pnlSf.ResumeLayout(false);
			this.pnlSf.PerformLayout();
			this.pnlPrint.ResumeLayout(false);
			this.pnlPrint.PerformLayout();
			this.pnlSender.ResumeLayout(false);
			this.pnlSender.PerformLayout();
			this.pnlProductList.ResumeLayout(false);
			this.tsAddProduct.ResumeLayout(false);
			this.tsAddProduct.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Label lblBillNumber;
		private System.Windows.Forms.TextBox txtBillNumber;
		private System.Windows.Forms.Label lblProducts;
		private System.Windows.Forms.TextBox txtProducts;
		private System.Windows.Forms.Button btnGo;
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
		private System.Windows.Forms.Label lblPhone;
		private System.Windows.Forms.TextBox txtPhone;
		private System.Windows.Forms.Label lblRecipientName;
		private System.Windows.Forms.TextBox txtRecipientName;
		private System.Windows.Forms.Label lblRemark;
		private System.Windows.Forms.TextBox txtRemark;
		private System.Windows.Forms.Label lblBuyerRemark;
		private System.Windows.Forms.TextBox txtBuyerRemark;
		private System.Windows.Forms.TextBox txtBuyerAccount;
		private System.Windows.Forms.FlowLayoutPanel pnlConsign;
		private System.Windows.Forms.FlowLayoutPanel pnlLogisticsCompanys;
		private System.Windows.Forms.RadioButton rdoYto;
		private System.Windows.Forms.RadioButton rdoSf;
		private System.Windows.Forms.FlowLayoutPanel pnlPrint;
		private System.Windows.Forms.Label lblPrint;
		private System.Windows.Forms.Label lblMoney;
		private System.Windows.Forms.Panel pnlMoney;
		private System.Windows.Forms.CheckBox chkHoliday;
		private System.Windows.Forms.FlowLayoutPanel pnlSender;
		private System.Windows.Forms.Label lblSenderName;
		private System.Windows.Forms.TextBox txtSenderName;
		private System.Windows.Forms.Label lblSenderCompany;
		private System.Windows.Forms.TextBox txtSenderAd;
		private System.Windows.Forms.Label lblSenderTel;
		private System.Windows.Forms.TextBox txtSenderTel;
		private System.Windows.Forms.Label lblSender;
		private System.Windows.Forms.Button txtHideShop;
		private System.Windows.Forms.Label lblDistributorFlag;
		private System.Windows.Forms.CheckBox chkPartial;
		private System.Windows.Forms.FlowLayoutPanel pnlSf;
		private System.Windows.Forms.CheckBox chkFriehghtCollect;
		private System.Windows.Forms.CheckBox chkPickup;
		private System.Windows.Forms.Button btnAppenRemark;
		private System.Windows.Forms.Button btnStockout;
		private System.Windows.Forms.CheckBox chkSfOldBill;
		private System.Windows.Forms.Button btnRegDistributor;
		private System.Windows.Forms.FlowLayoutPanel pnlProductList;
		private System.Windows.Forms.ToolStrip tsAddProduct;
		private System.Windows.Forms.ToolStripButton tsbtnAddProduct;
		private System.Windows.Forms.CheckBox chkAutoSfBillNumber;
		private System.Windows.Forms.TextBox txtDestCode;
		private System.Windows.Forms.RadioButton rdoYunda;
		private System.Windows.Forms.RadioButton rdoZto;
		private System.Windows.Forms.Label lblUnknownProductWarning;
		private System.Windows.Forms.ComboBox cboDistributors;
		private System.Windows.Forms.Button btnSelectAddress;
		private System.Windows.Forms.ToolStripButton tsbtnAddRossmannTablets;
		private System.Windows.Forms.ToolStripButton tsbtnAddDmTablets;
		private System.Windows.Forms.Label lblSenderAddr;
		private System.Windows.Forms.TextBox txtSenderAddr;
	}
}