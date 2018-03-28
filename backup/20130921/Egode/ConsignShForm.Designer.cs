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
			this.picExpressBill = new System.Windows.Forms.PictureBox();
			this.lblRemark = new System.Windows.Forms.Label();
			this.txtRemark = new System.Windows.Forms.TextBox();
			this.lblBuyerRemark = new System.Windows.Forms.Label();
			this.txtBuyerRemark = new System.Windows.Forms.TextBox();
			this.pnlConsign = new System.Windows.Forms.FlowLayoutPanel();
			this.pnlLogisticsCompanys = new System.Windows.Forms.FlowLayoutPanel();
			this.rdoYto = new System.Windows.Forms.RadioButton();
			this.rdoSf = new System.Windows.Forms.RadioButton();
			this.pnlPrint = new System.Windows.Forms.FlowLayoutPanel();
			this.lblPrint = new System.Windows.Forms.Label();
			this.pnlAddress.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picExpressBill)).BeginInit();
			this.pnlConsign.SuspendLayout();
			this.pnlLogisticsCompanys.SuspendLayout();
			this.pnlPrint.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnPrint
			// 
			this.btnPrint.BackColor = System.Drawing.SystemColors.HotTrack;
			this.btnPrint.ForeColor = System.Drawing.Color.White;
			this.btnPrint.Location = new System.Drawing.Point(51, 234);
			this.btnPrint.Margin = new System.Windows.Forms.Padding(51, 3, 3, 3);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(494, 36);
			this.btnPrint.TabIndex = 13;
			this.btnPrint.Text = "&Print";
			this.btnPrint.UseVisualStyleBackColor = false;
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// lblBillNumber
			// 
			this.lblBillNumber.AutoSize = true;
			this.lblBillNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblBillNumber.Location = new System.Drawing.Point(35, 372);
			this.lblBillNumber.Name = "lblBillNumber";
			this.lblBillNumber.Size = new System.Drawing.Size(43, 20);
			this.lblBillNumber.TabIndex = 2;
			this.lblBillNumber.Text = "单号:";
			// 
			// txtBillNumber
			// 
			this.pnlConsign.SetFlowBreak(this.txtBillNumber, true);
			this.txtBillNumber.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtBillNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtBillNumber.Location = new System.Drawing.Point(6, 31);
			this.txtBillNumber.Margin = new System.Windows.Forms.Padding(6, 0, 3, 3);
			this.txtBillNumber.Name = "txtBillNumber";
			this.txtBillNumber.Size = new System.Drawing.Size(539, 27);
			this.txtBillNumber.TabIndex = 16;
			this.txtBillNumber.TextChanged += new System.EventHandler(this.txtBillNumber_TextChanged);
			// 
			// lblProducts
			// 
			this.lblProducts.AutoSize = true;
			this.lblProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblProducts.Location = new System.Drawing.Point(3, 153);
			this.lblProducts.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.lblProducts.Name = "lblProducts";
			this.lblProducts.Size = new System.Drawing.Size(43, 20);
			this.lblProducts.TabIndex = 4;
			this.lblProducts.Text = "商品:";
			// 
			// txtProducts
			// 
			this.pnlPrint.SetFlowBreak(this.txtProducts, true);
			this.txtProducts.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtProducts.Location = new System.Drawing.Point(52, 153);
			this.txtProducts.Multiline = true;
			this.txtProducts.Name = "txtProducts";
			this.txtProducts.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtProducts.Size = new System.Drawing.Size(493, 75);
			this.txtProducts.TabIndex = 12;
			// 
			// btnGo
			// 
			this.btnGo.BackColor = System.Drawing.Color.Tomato;
			this.btnGo.Enabled = false;
			this.btnGo.ForeColor = System.Drawing.Color.White;
			this.btnGo.Location = new System.Drawing.Point(6, 64);
			this.btnGo.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(539, 30);
			this.btnGo.TabIndex = 17;
			this.btnGo.Text = "Go (点发货 && 出库登记) (输入单号后将自动执行)";
			this.btnGo.UseVisualStyleBackColor = false;
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// lblAddress
			// 
			this.lblAddress.AutoSize = true;
			this.lblAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblAddress.Location = new System.Drawing.Point(3, 10);
			this.lblAddress.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(43, 20);
			this.lblAddress.TabIndex = 7;
			this.lblAddress.Text = "地址:";
			// 
			// pnlAddress
			// 
			this.pnlAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(188)))), ((int)(((byte)(231)))));
			this.pnlAddress.Controls.Add(this.lblRecipientName);
			this.pnlAddress.Controls.Add(this.txtRecipientName);
			this.pnlAddress.Controls.Add(this.txtBuyerAccount);
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
			this.pnlPrint.SetFlowBreak(this.pnlAddress, true);
			this.pnlAddress.Location = new System.Drawing.Point(52, 3);
			this.pnlAddress.Name = "pnlAddress";
			this.pnlAddress.Size = new System.Drawing.Size(493, 144);
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
			this.lblRecipientName.Size = new System.Drawing.Size(88, 20);
			this.lblRecipientName.TabIndex = 13;
			this.lblRecipientName.Text = "收件人姓名:";
			// 
			// txtRecipientName
			// 
			this.txtRecipientName.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtRecipientName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtRecipientName.Location = new System.Drawing.Point(92, 6);
			this.txtRecipientName.Margin = new System.Windows.Forms.Padding(0, 6, 3, 6);
			this.txtRecipientName.Name = "txtRecipientName";
			this.txtRecipientName.Size = new System.Drawing.Size(110, 27);
			this.txtRecipientName.TabIndex = 3;
			// 
			// txtBuyerAccount
			// 
			this.pnlAddress.SetFlowBreak(this.txtBuyerAccount, true);
			this.txtBuyerAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
			this.txtBuyerAccount.Location = new System.Drawing.Point(208, 6);
			this.txtBuyerAccount.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.txtBuyerAccount.Name = "txtBuyerAccount";
			this.txtBuyerAccount.Size = new System.Drawing.Size(224, 27);
			this.txtBuyerAccount.TabIndex = 4;
			// 
			// txtProvince
			// 
			this.txtProvince.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtProvince.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtProvince.Location = new System.Drawing.Point(6, 39);
			this.txtProvince.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.txtProvince.Name = "txtProvince";
			this.txtProvince.Size = new System.Drawing.Size(80, 27);
			this.txtProvince.TabIndex = 5;
			// 
			// lblProvince
			// 
			this.lblProvince.AutoSize = true;
			this.lblProvince.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblProvince.Location = new System.Drawing.Point(86, 42);
			this.lblProvince.Margin = new System.Windows.Forms.Padding(0, 3, 8, 0);
			this.lblProvince.Name = "lblProvince";
			this.lblProvince.Size = new System.Drawing.Size(24, 20);
			this.lblProvince.TabIndex = 1;
			this.lblProvince.Text = "省";
			// 
			// txtCity1
			// 
			this.txtCity1.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtCity1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtCity1.Location = new System.Drawing.Point(121, 39);
			this.txtCity1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.txtCity1.Name = "txtCity1";
			this.txtCity1.Size = new System.Drawing.Size(80, 27);
			this.txtCity1.TabIndex = 6;
			// 
			// lblCity1
			// 
			this.lblCity1.AutoSize = true;
			this.lblCity1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblCity1.Location = new System.Drawing.Point(201, 42);
			this.lblCity1.Margin = new System.Windows.Forms.Padding(0, 3, 8, 0);
			this.lblCity1.Name = "lblCity1";
			this.lblCity1.Size = new System.Drawing.Size(24, 20);
			this.lblCity1.TabIndex = 3;
			this.lblCity1.Text = "市";
			// 
			// txtCity2
			// 
			this.txtCity2.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtCity2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtCity2.Location = new System.Drawing.Point(236, 39);
			this.txtCity2.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.txtCity2.Name = "txtCity2";
			this.txtCity2.Size = new System.Drawing.Size(80, 27);
			this.txtCity2.TabIndex = 7;
			// 
			// lblCity2
			// 
			this.lblCity2.AutoSize = true;
			this.lblCity2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblCity2.Location = new System.Drawing.Point(316, 42);
			this.lblCity2.Margin = new System.Windows.Forms.Padding(0, 3, 8, 0);
			this.lblCity2.Name = "lblCity2";
			this.lblCity2.Size = new System.Drawing.Size(24, 20);
			this.lblCity2.TabIndex = 5;
			this.lblCity2.Text = "县";
			// 
			// txtDistrict
			// 
			this.txtDistrict.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtDistrict.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtDistrict.Location = new System.Drawing.Point(351, 39);
			this.txtDistrict.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.txtDistrict.Name = "txtDistrict";
			this.txtDistrict.Size = new System.Drawing.Size(80, 27);
			this.txtDistrict.TabIndex = 8;
			// 
			// lblDistrict
			// 
			this.lblDistrict.AutoSize = true;
			this.lblDistrict.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblDistrict.Location = new System.Drawing.Point(431, 42);
			this.lblDistrict.Margin = new System.Windows.Forms.Padding(0, 3, 8, 0);
			this.lblDistrict.Name = "lblDistrict";
			this.lblDistrict.Size = new System.Drawing.Size(49, 20);
			this.lblDistrict.TabIndex = 7;
			this.lblDistrict.Text = "区(镇)";
			// 
			// txtStreetAddress
			// 
			this.txtStreetAddress.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtStreetAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtStreetAddress.Location = new System.Drawing.Point(6, 74);
			this.txtStreetAddress.Margin = new System.Windows.Forms.Padding(6, 8, 3, 3);
			this.txtStreetAddress.Name = "txtStreetAddress";
			this.txtStreetAddress.Size = new System.Drawing.Size(474, 27);
			this.txtStreetAddress.TabIndex = 9;
			// 
			// lblMobile
			// 
			this.lblMobile.AutoSize = true;
			this.lblMobile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblMobile.Location = new System.Drawing.Point(3, 110);
			this.lblMobile.Margin = new System.Windows.Forms.Padding(3, 6, 0, 0);
			this.lblMobile.Name = "lblMobile";
			this.lblMobile.Size = new System.Drawing.Size(73, 20);
			this.lblMobile.TabIndex = 9;
			this.lblMobile.Text = "联系手机:";
			// 
			// txtMobile
			// 
			this.txtMobile.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtMobile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtMobile.Location = new System.Drawing.Point(76, 107);
			this.txtMobile.Margin = new System.Windows.Forms.Padding(0, 3, 16, 3);
			this.txtMobile.Name = "txtMobile";
			this.txtMobile.Size = new System.Drawing.Size(120, 27);
			this.txtMobile.TabIndex = 10;
			// 
			// lblPhone
			// 
			this.lblPhone.AutoSize = true;
			this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblPhone.Location = new System.Drawing.Point(224, 110);
			this.lblPhone.Margin = new System.Windows.Forms.Padding(12, 6, 0, 0);
			this.lblPhone.Name = "lblPhone";
			this.lblPhone.Size = new System.Drawing.Size(73, 20);
			this.lblPhone.TabIndex = 11;
			this.lblPhone.Text = "固定电话:";
			// 
			// txtPhone
			// 
			this.txtPhone.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.txtPhone.Location = new System.Drawing.Point(297, 107);
			this.txtPhone.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.txtPhone.Name = "txtPhone";
			this.txtPhone.Size = new System.Drawing.Size(182, 27);
			this.txtPhone.TabIndex = 11;
			// 
			// picExpressBill
			// 
			this.picExpressBill.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picExpressBill.BackgroundImage")));
			this.picExpressBill.Location = new System.Drawing.Point(-1, 313);
			this.picExpressBill.Name = "picExpressBill";
			this.picExpressBill.Size = new System.Drawing.Size(100, 50);
			this.picExpressBill.TabIndex = 9;
			this.picExpressBill.TabStop = false;
			this.picExpressBill.Visible = false;
			// 
			// lblRemark
			// 
			this.lblRemark.AutoSize = true;
			this.lblRemark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
			this.lblRemark.Location = new System.Drawing.Point(9, 47);
			this.lblRemark.Name = "lblRemark";
			this.lblRemark.Size = new System.Drawing.Size(73, 20);
			this.lblRemark.TabIndex = 10;
			this.lblRemark.Text = "客服备注:";
			// 
			// txtRemark
			// 
			this.txtRemark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
			this.txtRemark.Location = new System.Drawing.Point(84, 45);
			this.txtRemark.Name = "txtRemark";
			this.txtRemark.Size = new System.Drawing.Size(556, 27);
			this.txtRemark.TabIndex = 2;
			// 
			// lblBuyerRemark
			// 
			this.lblBuyerRemark.AutoSize = true;
			this.lblBuyerRemark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
			this.lblBuyerRemark.Location = new System.Drawing.Point(9, 14);
			this.lblBuyerRemark.Name = "lblBuyerRemark";
			this.lblBuyerRemark.Size = new System.Drawing.Size(73, 20);
			this.lblBuyerRemark.TabIndex = 12;
			this.lblBuyerRemark.Text = "买家留言:";
			// 
			// txtBuyerRemark
			// 
			this.txtBuyerRemark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
			this.txtBuyerRemark.Location = new System.Drawing.Point(84, 12);
			this.txtBuyerRemark.Name = "txtBuyerRemark";
			this.txtBuyerRemark.Size = new System.Drawing.Size(556, 27);
			this.txtBuyerRemark.TabIndex = 1;
			// 
			// pnlConsign
			// 
			this.pnlConsign.BackColor = System.Drawing.Color.White;
			this.pnlConsign.Controls.Add(this.pnlLogisticsCompanys);
			this.pnlConsign.Controls.Add(this.txtBillNumber);
			this.pnlConsign.Controls.Add(this.btnGo);
			this.pnlConsign.Location = new System.Drawing.Point(84, 369);
			this.pnlConsign.Name = "pnlConsign";
			this.pnlConsign.Size = new System.Drawing.Size(556, 100);
			this.pnlConsign.TabIndex = 14;
			this.pnlConsign.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlConsign_Paint);
			// 
			// pnlLogisticsCompanys
			// 
			this.pnlLogisticsCompanys.AutoSize = true;
			this.pnlLogisticsCompanys.Controls.Add(this.rdoYto);
			this.pnlLogisticsCompanys.Controls.Add(this.rdoSf);
			this.pnlConsign.SetFlowBreak(this.pnlLogisticsCompanys, true);
			this.pnlLogisticsCompanys.Location = new System.Drawing.Point(6, 1);
			this.pnlLogisticsCompanys.Margin = new System.Windows.Forms.Padding(6, 1, 3, 0);
			this.pnlLogisticsCompanys.Name = "pnlLogisticsCompanys";
			this.pnlLogisticsCompanys.Size = new System.Drawing.Size(126, 30);
			this.pnlLogisticsCompanys.TabIndex = 0;
			// 
			// rdoYto
			// 
			this.rdoYto.AutoSize = true;
			this.rdoYto.Checked = true;
			this.rdoYto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.rdoYto.Location = new System.Drawing.Point(0, 3);
			this.rdoYto.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.rdoYto.Name = "rdoYto";
			this.rdoYto.Size = new System.Drawing.Size(57, 24);
			this.rdoYto.TabIndex = 14;
			this.rdoYto.TabStop = true;
			this.rdoYto.Text = "圆通";
			this.rdoYto.UseVisualStyleBackColor = true;
			// 
			// rdoSf
			// 
			this.rdoSf.AutoSize = true;
			this.rdoSf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.rdoSf.Location = new System.Drawing.Point(66, 3);
			this.rdoSf.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
			this.rdoSf.Name = "rdoSf";
			this.rdoSf.Size = new System.Drawing.Size(57, 24);
			this.rdoSf.TabIndex = 15;
			this.rdoSf.Text = "顺丰";
			this.rdoSf.UseVisualStyleBackColor = true;
			// 
			// pnlPrint
			// 
			this.pnlPrint.BackColor = System.Drawing.Color.White;
			this.pnlPrint.Controls.Add(this.lblAddress);
			this.pnlPrint.Controls.Add(this.pnlAddress);
			this.pnlPrint.Controls.Add(this.lblProducts);
			this.pnlPrint.Controls.Add(this.txtProducts);
			this.pnlPrint.Controls.Add(this.btnPrint);
			this.pnlPrint.Location = new System.Drawing.Point(84, 78);
			this.pnlPrint.Name = "pnlPrint";
			this.pnlPrint.Size = new System.Drawing.Size(556, 281);
			this.pnlPrint.TabIndex = 15;
			this.pnlPrint.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPrint_Paint);
			// 
			// lblPrint
			// 
			this.lblPrint.AutoSize = true;
			this.lblPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblPrint.Location = new System.Drawing.Point(8, 84);
			this.lblPrint.Name = "lblPrint";
			this.lblPrint.Size = new System.Drawing.Size(73, 20);
			this.lblPrint.TabIndex = 16;
			this.lblPrint.Text = "打印面单:";
			// 
			// ConsignShForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(652, 481);
			this.Controls.Add(this.lblPrint);
			this.Controls.Add(this.pnlPrint);
			this.Controls.Add(this.pnlConsign);
			this.Controls.Add(this.txtBuyerRemark);
			this.Controls.Add(this.lblBuyerRemark);
			this.Controls.Add(this.txtRemark);
			this.Controls.Add(this.lblRemark);
			this.Controls.Add(this.lblBillNumber);
			this.Controls.Add(this.picExpressBill);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
			((System.ComponentModel.ISupportInitialize)(this.picExpressBill)).EndInit();
			this.pnlConsign.ResumeLayout(false);
			this.pnlConsign.PerformLayout();
			this.pnlLogisticsCompanys.ResumeLayout(false);
			this.pnlLogisticsCompanys.PerformLayout();
			this.pnlPrint.ResumeLayout(false);
			this.pnlPrint.PerformLayout();
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
		private System.Windows.Forms.PictureBox picExpressBill;
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
	}
}