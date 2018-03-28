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
			this.tblMain = new System.Windows.Forms.TableLayoutPanel();
			this.tblHeaderInfo = new System.Windows.Forms.TableLayoutPanel();
			this.btnPreparePacket = new System.Windows.Forms.Button();
			this.orderMoney = new Egode.OrderMoneyControl();
			this.orderId = new Egode.OrderIdControl();
			this.buyerInfo = new Egode.BuyerInfoControl();
			this.lblPayingTime = new System.Windows.Forms.Label();
			this.lblDealTime = new System.Windows.Forms.Label();
			this.tblDetails = new System.Windows.Forms.TableLayoutPanel();
			this.lblBuyerRemarkTitle = new System.Windows.Forms.Label();
			this.lblRemarkTitle = new System.Windows.Forms.Label();
			this.txtBuyerRemark = new System.Windows.Forms.TextBox();
			this.txtRemark = new System.Windows.Forms.TextBox();
			this.lblAddressTitle = new System.Windows.Forms.Label();
			this.txtAddress = new System.Windows.Forms.TextBox();
			this.lblEditedAddressTitle = new System.Windows.Forms.Label();
			this.pnlEditedAddress = new System.Windows.Forms.FlowLayoutPanel();
			this.txtEditedAddress = new System.Windows.Forms.TextBox();
			this.btnGetFullEditedAddress = new System.Windows.Forms.Button();
			this.lblPinyinAddressTitle = new System.Windows.Forms.Label();
			this.txtPinyinAddress = new System.Windows.Forms.TextBox();
			this.lblProductListTitle = new System.Windows.Forms.Label();
			this.productList = new Egode.ProductListControl();
			this.tblPacket = new System.Windows.Forms.TableLayoutPanel();
			this.lblNetWeight = new System.Windows.Forms.Label();
			this.lblPacket = new System.Windows.Forms.Label();
			this.cboPackets = new System.Windows.Forms.ComboBox();
			this.btnAddPacket = new System.Windows.Forms.Button();
			this.btnMarkPrepared = new System.Windows.Forms.Button();
			this.lblPacketing = new System.Windows.Forms.Label();
			this.wb = new System.Windows.Forms.WebBrowser();
			this.tblMain.SuspendLayout();
			this.tblHeaderInfo.SuspendLayout();
			this.tblDetails.SuspendLayout();
			this.pnlEditedAddress.SuspendLayout();
			this.tblPacket.SuspendLayout();
			this.SuspendLayout();
			// 
			// tblMain
			// 
			this.tblMain.ColumnCount = 1;
			this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblMain.Controls.Add(this.tblHeaderInfo, 0, 0);
			this.tblMain.Controls.Add(this.tblDetails, 0, 1);
			this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblMain.Location = new System.Drawing.Point(1, 1);
			this.tblMain.Margin = new System.Windows.Forms.Padding(1);
			this.tblMain.Name = "tblMain";
			this.tblMain.RowCount = 2;
			this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblMain.Size = new System.Drawing.Size(958, 99);
			this.tblMain.TabIndex = 0;
			// 
			// tblHeaderInfo
			// 
			this.tblHeaderInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tblHeaderInfo.BackColor = System.Drawing.Color.Transparent;
			this.tblHeaderInfo.ColumnCount = 7;
			this.tblHeaderInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18F));
			this.tblHeaderInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblHeaderInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
			this.tblHeaderInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
			this.tblHeaderInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblHeaderInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblHeaderInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblHeaderInfo.Controls.Add(this.btnPreparePacket, 6, 0);
			this.tblHeaderInfo.Controls.Add(this.orderMoney, 5, 0);
			this.tblHeaderInfo.Controls.Add(this.orderId, 1, 0);
			this.tblHeaderInfo.Controls.Add(this.buyerInfo, 4, 0);
			this.tblHeaderInfo.Controls.Add(this.lblPayingTime, 3, 0);
			this.tblHeaderInfo.Controls.Add(this.lblDealTime, 2, 0);
			this.tblHeaderInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblHeaderInfo.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
			this.tblHeaderInfo.Location = new System.Drawing.Point(3, 3);
			this.tblHeaderInfo.Name = "tblHeaderInfo";
			this.tblHeaderInfo.RowCount = 1;
			this.tblHeaderInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblHeaderInfo.Size = new System.Drawing.Size(952, 24);
			this.tblHeaderInfo.TabIndex = 0;
			this.tblHeaderInfo.Paint += new System.Windows.Forms.PaintEventHandler(this.tblHeaderInfo_Paint);
			// 
			// btnPreparePacket
			// 
			this.btnPreparePacket.AutoSize = true;
			this.btnPreparePacket.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnPreparePacket.BackColor = System.Drawing.Color.CornflowerBlue;
			this.btnPreparePacket.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnPreparePacket.ForeColor = System.Drawing.Color.White;
			this.btnPreparePacket.Location = new System.Drawing.Point(931, 3);
			this.btnPreparePacket.Name = "btnPreparePacket";
			this.btnPreparePacket.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
			this.btnPreparePacket.Size = new System.Drawing.Size(57, 18);
			this.btnPreparePacket.TabIndex = 11;
			this.btnPreparePacket.Text = "出单";
			this.btnPreparePacket.UseVisualStyleBackColor = false;
			// 
			// orderMoney
			// 
			this.orderMoney.BackColor = System.Drawing.Color.Transparent;
			this.orderMoney.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.orderMoney.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.orderMoney.Freight = 0F;
			this.orderMoney.Location = new System.Drawing.Point(778, 0);
			this.orderMoney.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
			this.orderMoney.Money = 0F;
			this.orderMoney.Name = "orderMoney";
			this.orderMoney.Size = new System.Drawing.Size(150, 20);
			this.orderMoney.TabIndex = 13;
			// 
			// orderId
			// 
			this.orderId.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.orderId.BackColor = System.Drawing.Color.Transparent;
			this.orderId.ConsignVisible = true;
			this.orderId.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.orderId.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.orderId.Location = new System.Drawing.Point(18, 7);
			this.orderId.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
			this.orderId.Name = "orderId";
			this.orderId.Order = null;
			this.orderId.Padding = new System.Windows.Forms.Padding(0, 0, 16, 0);
			this.orderId.Size = new System.Drawing.Size(190, 13);
			this.orderId.TabIndex = 14;
			// 
			// buyerInfo
			// 
			this.buyerInfo.BackColor = System.Drawing.Color.Transparent;
			this.buyerInfo.BuyerAccount = "buyer";
			this.buyerInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.buyerInfo.Location = new System.Drawing.Point(578, 2);
			this.buyerInfo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
			this.buyerInfo.Name = "buyerInfo";
			this.buyerInfo.OrderAmount = 0;
			this.buyerInfo.Size = new System.Drawing.Size(200, 18);
			this.buyerInfo.TabIndex = 12;
			this.buyerInfo.OnBuyerClick += new System.EventHandler(this.buyerInfo_OnBuyerClick);
			// 
			// lblPayingTime
			// 
			this.lblPayingTime.AutoSize = true;
			this.lblPayingTime.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lblPayingTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblPayingTime.Location = new System.Drawing.Point(378, 7);
			this.lblPayingTime.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
			this.lblPayingTime.Name = "lblPayingTime";
			this.lblPayingTime.Size = new System.Drawing.Size(200, 13);
			this.lblPayingTime.TabIndex = 15;
			this.lblPayingTime.Text = "付款时间: 0000-00-00 00:00:00";
			// 
			// lblDealTime
			// 
			this.lblDealTime.AutoSize = true;
			this.lblDealTime.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lblDealTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblDealTime.Location = new System.Drawing.Point(208, 7);
			this.lblDealTime.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
			this.lblDealTime.Name = "lblDealTime";
			this.lblDealTime.Size = new System.Drawing.Size(170, 13);
			this.lblDealTime.TabIndex = 3;
			this.lblDealTime.Text = "成交时间: 0000-00-00 00:00:00";
			// 
			// tblDetails
			// 
			this.tblDetails.AutoSize = true;
			this.tblDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tblDetails.ColumnCount = 2;
			this.tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblDetails.Controls.Add(this.lblBuyerRemarkTitle, 0, 0);
			this.tblDetails.Controls.Add(this.lblRemarkTitle, 0, 1);
			this.tblDetails.Controls.Add(this.txtBuyerRemark, 1, 0);
			this.tblDetails.Controls.Add(this.txtRemark, 1, 1);
			this.tblDetails.Controls.Add(this.lblAddressTitle, 0, 2);
			this.tblDetails.Controls.Add(this.txtAddress, 1, 2);
			this.tblDetails.Controls.Add(this.lblEditedAddressTitle, 0, 3);
			this.tblDetails.Controls.Add(this.pnlEditedAddress, 1, 3);
			this.tblDetails.Controls.Add(this.lblPinyinAddressTitle, 0, 4);
			this.tblDetails.Controls.Add(this.txtPinyinAddress, 1, 4);
			this.tblDetails.Controls.Add(this.lblProductListTitle, 0, 5);
			this.tblDetails.Controls.Add(this.productList, 1, 5);
			this.tblDetails.Controls.Add(this.tblPacket, 1, 6);
			this.tblDetails.Controls.Add(this.lblPacketing, 0, 6);
			this.tblDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblDetails.Location = new System.Drawing.Point(3, 33);
			this.tblDetails.Name = "tblDetails";
			this.tblDetails.RowCount = 7;
			this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tblDetails.Size = new System.Drawing.Size(952, 155);
			this.tblDetails.TabIndex = 1;
			// 
			// lblBuyerRemarkTitle
			// 
			this.lblBuyerRemarkTitle.AutoSize = true;
			this.lblBuyerRemarkTitle.Dock = System.Windows.Forms.DockStyle.Right;
			this.lblBuyerRemarkTitle.ForeColor = System.Drawing.Color.Gray;
			this.lblBuyerRemarkTitle.Location = new System.Drawing.Point(0, 3);
			this.lblBuyerRemarkTitle.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.lblBuyerRemarkTitle.Name = "lblBuyerRemarkTitle";
			this.lblBuyerRemarkTitle.Size = new System.Drawing.Size(59, 19);
			this.lblBuyerRemarkTitle.TabIndex = 0;
			this.lblBuyerRemarkTitle.Text = "买家留言:";
			this.lblBuyerRemarkTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblRemarkTitle
			// 
			this.lblRemarkTitle.AutoSize = true;
			this.lblRemarkTitle.Dock = System.Windows.Forms.DockStyle.Right;
			this.lblRemarkTitle.ForeColor = System.Drawing.Color.Gray;
			this.lblRemarkTitle.Location = new System.Drawing.Point(0, 25);
			this.lblRemarkTitle.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.lblRemarkTitle.Name = "lblRemarkTitle";
			this.lblRemarkTitle.Size = new System.Drawing.Size(59, 19);
			this.lblRemarkTitle.TabIndex = 2;
			this.lblRemarkTitle.Text = "客服备注:";
			this.lblRemarkTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtBuyerRemark
			// 
			this.txtBuyerRemark.BackColor = System.Drawing.Color.White;
			this.txtBuyerRemark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtBuyerRemark.Location = new System.Drawing.Point(59, 0);
			this.txtBuyerRemark.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.txtBuyerRemark.Name = "txtBuyerRemark";
			this.txtBuyerRemark.ReadOnly = true;
			this.txtBuyerRemark.Size = new System.Drawing.Size(870, 21);
			this.txtBuyerRemark.TabIndex = 3;
			// 
			// txtRemark
			// 
			this.txtRemark.BackColor = System.Drawing.Color.White;
			this.txtRemark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtRemark.Location = new System.Drawing.Point(59, 22);
			this.txtRemark.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.txtRemark.Name = "txtRemark";
			this.txtRemark.ReadOnly = true;
			this.txtRemark.Size = new System.Drawing.Size(870, 21);
			this.txtRemark.TabIndex = 4;
			// 
			// lblAddressTitle
			// 
			this.lblAddressTitle.AutoSize = true;
			this.lblAddressTitle.Dock = System.Windows.Forms.DockStyle.Right;
			this.lblAddressTitle.ForeColor = System.Drawing.Color.Gray;
			this.lblAddressTitle.Location = new System.Drawing.Point(0, 47);
			this.lblAddressTitle.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.lblAddressTitle.Name = "lblAddressTitle";
			this.lblAddressTitle.Size = new System.Drawing.Size(59, 19);
			this.lblAddressTitle.TabIndex = 5;
			this.lblAddressTitle.Text = "收货地址:";
			this.lblAddressTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtAddress
			// 
			this.txtAddress.BackColor = System.Drawing.Color.White;
			this.txtAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtAddress.Location = new System.Drawing.Point(59, 44);
			this.txtAddress.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new System.Drawing.Size(870, 21);
			this.txtAddress.TabIndex = 6;
			this.txtAddress.TextChanged += new System.EventHandler(this.txtAddress_TextChanged);
			// 
			// lblEditedAddressTitle
			// 
			this.lblEditedAddressTitle.AutoSize = true;
			this.lblEditedAddressTitle.Dock = System.Windows.Forms.DockStyle.Right;
			this.lblEditedAddressTitle.ForeColor = System.Drawing.Color.Gray;
			this.lblEditedAddressTitle.Location = new System.Drawing.Point(12, 69);
			this.lblEditedAddressTitle.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.lblEditedAddressTitle.Name = "lblEditedAddressTitle";
			this.lblEditedAddressTitle.Size = new System.Drawing.Size(47, 19);
			this.lblEditedAddressTitle.TabIndex = 9;
			this.lblEditedAddressTitle.Text = "新地址:";
			this.lblEditedAddressTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// pnlEditedAddress
			// 
			this.pnlEditedAddress.AutoSize = true;
			this.pnlEditedAddress.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.pnlEditedAddress.BackColor = System.Drawing.Color.Transparent;
			this.pnlEditedAddress.Controls.Add(this.txtEditedAddress);
			this.pnlEditedAddress.Controls.Add(this.btnGetFullEditedAddress);
			this.pnlEditedAddress.Location = new System.Drawing.Point(59, 66);
			this.pnlEditedAddress.Margin = new System.Windows.Forms.Padding(0);
			this.pnlEditedAddress.Name = "pnlEditedAddress";
			this.pnlEditedAddress.Size = new System.Drawing.Size(870, 22);
			this.pnlEditedAddress.TabIndex = 13;
			// 
			// txtEditedAddress
			// 
			this.txtEditedAddress.BackColor = System.Drawing.Color.White;
			this.txtEditedAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtEditedAddress.Location = new System.Drawing.Point(0, 0);
			this.txtEditedAddress.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.txtEditedAddress.Name = "txtEditedAddress";
			this.txtEditedAddress.ReadOnly = true;
			this.txtEditedAddress.Size = new System.Drawing.Size(830, 21);
			this.txtEditedAddress.TabIndex = 11;
			// 
			// btnGetFullEditedAddress
			// 
			this.btnGetFullEditedAddress.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnGetFullEditedAddress.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnGetFullEditedAddress.Location = new System.Drawing.Point(831, 0);
			this.btnGetFullEditedAddress.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
			this.btnGetFullEditedAddress.Name = "btnGetFullEditedAddress";
			this.btnGetFullEditedAddress.Size = new System.Drawing.Size(39, 21);
			this.btnGetFullEditedAddress.TabIndex = 12;
			this.btnGetFullEditedAddress.Text = "Get";
			this.btnGetFullEditedAddress.UseVisualStyleBackColor = true;
			this.btnGetFullEditedAddress.Click += new System.EventHandler(this.btnGetFullEditedAddress_Click);
			// 
			// lblPinyinAddressTitle
			// 
			this.lblPinyinAddressTitle.AutoSize = true;
			this.lblPinyinAddressTitle.Dock = System.Windows.Forms.DockStyle.Right;
			this.lblPinyinAddressTitle.ForeColor = System.Drawing.Color.Gray;
			this.lblPinyinAddressTitle.Location = new System.Drawing.Point(0, 91);
			this.lblPinyinAddressTitle.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.lblPinyinAddressTitle.Name = "lblPinyinAddressTitle";
			this.lblPinyinAddressTitle.Size = new System.Drawing.Size(59, 19);
			this.lblPinyinAddressTitle.TabIndex = 7;
			this.lblPinyinAddressTitle.Text = "拼音地址:";
			this.lblPinyinAddressTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtPinyinAddress
			// 
			this.txtPinyinAddress.BackColor = System.Drawing.Color.White;
			this.txtPinyinAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.txtPinyinAddress.Location = new System.Drawing.Point(59, 88);
			this.txtPinyinAddress.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.txtPinyinAddress.Name = "txtPinyinAddress";
			this.txtPinyinAddress.ReadOnly = true;
			this.txtPinyinAddress.Size = new System.Drawing.Size(870, 21);
			this.txtPinyinAddress.TabIndex = 8;
			// 
			// lblProductListTitle
			// 
			this.lblProductListTitle.AutoSize = true;
			this.lblProductListTitle.Dock = System.Windows.Forms.DockStyle.Right;
			this.lblProductListTitle.ForeColor = System.Drawing.Color.Gray;
			this.lblProductListTitle.Location = new System.Drawing.Point(0, 113);
			this.lblProductListTitle.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.lblProductListTitle.Name = "lblProductListTitle";
			this.lblProductListTitle.Size = new System.Drawing.Size(59, 13);
			this.lblProductListTitle.TabIndex = 11;
			this.lblProductListTitle.Text = "商品列表:";
			this.lblProductListTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// productList
			// 
			this.productList.AutoSize = true;
			this.productList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.productList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.productList.Location = new System.Drawing.Point(59, 113);
			this.productList.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.productList.Name = "productList";
			this.productList.Size = new System.Drawing.Size(3, 3);
			this.productList.TabIndex = 12;
			// 
			// tblPacket
			// 
			this.tblPacket.AutoSize = true;
			this.tblPacket.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tblPacket.ColumnCount = 7;
			this.tblPacket.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblPacket.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblPacket.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblPacket.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblPacket.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblPacket.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblPacket.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tblPacket.Controls.Add(this.lblNetWeight, 0, 0);
			this.tblPacket.Controls.Add(this.lblPacket, 1, 0);
			this.tblPacket.Controls.Add(this.cboPackets, 2, 0);
			this.tblPacket.Controls.Add(this.btnAddPacket, 3, 0);
			this.tblPacket.Controls.Add(this.btnMarkPrepared, 4, 0);
			this.tblPacket.Location = new System.Drawing.Point(59, 132);
			this.tblPacket.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
			this.tblPacket.Name = "tblPacket";
			this.tblPacket.RowCount = 1;
			this.tblPacket.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblPacket.Size = new System.Drawing.Size(359, 23);
			this.tblPacket.TabIndex = 14;
			// 
			// lblNetWeight
			// 
			this.lblNetWeight.AutoSize = true;
			this.lblNetWeight.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblNetWeight.ForeColor = System.Drawing.Color.Gray;
			this.lblNetWeight.Location = new System.Drawing.Point(0, 0);
			this.lblNetWeight.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
			this.lblNetWeight.Name = "lblNetWeight";
			this.lblNetWeight.Size = new System.Drawing.Size(35, 13);
			this.lblNetWeight.TabIndex = 1;
			this.lblNetWeight.Text = "000g";
			// 
			// lblPacket
			// 
			this.lblPacket.AutoSize = true;
			this.lblPacket.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.lblPacket.Location = new System.Drawing.Point(54, 0);
			this.lblPacket.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.lblPacket.Name = "lblPacket";
			this.lblPacket.Size = new System.Drawing.Size(47, 13);
			this.lblPacket.TabIndex = 2;
			this.lblPacket.Text = "包裹单:";
			// 
			// cboPackets
			// 
			this.cboPackets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPackets.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
			this.cboPackets.FormattingEnabled = true;
			this.cboPackets.Location = new System.Drawing.Point(101, 0);
			this.cboPackets.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
			this.cboPackets.Name = "cboPackets";
			this.cboPackets.Size = new System.Drawing.Size(150, 21);
			this.cboPackets.TabIndex = 3;
			this.cboPackets.SelectedIndexChanged += new System.EventHandler(this.cboPackets_SelectedIndexChanged);
			// 
			// btnAddPacket
			// 
			this.btnAddPacket.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAddPacket.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAddPacket.Location = new System.Drawing.Point(253, 0);
			this.btnAddPacket.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
			this.btnAddPacket.Name = "btnAddPacket";
			this.btnAddPacket.Size = new System.Drawing.Size(30, 22);
			this.btnAddPacket.TabIndex = 4;
			this.btnAddPacket.Text = "+";
			this.btnAddPacket.UseVisualStyleBackColor = true;
			this.btnAddPacket.Click += new System.EventHandler(this.btnAddPacket_Click);
			// 
			// btnMarkPrepared
			// 
			this.btnMarkPrepared.Location = new System.Drawing.Point(284, 0);
			this.btnMarkPrepared.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
			this.btnMarkPrepared.Name = "btnMarkPrepared";
			this.btnMarkPrepared.Size = new System.Drawing.Size(75, 22);
			this.btnMarkPrepared.TabIndex = 5;
			this.btnMarkPrepared.Text = "标记出单";
			this.btnMarkPrepared.UseVisualStyleBackColor = true;
			this.btnMarkPrepared.Click += new System.EventHandler(this.btnMarkPrepared_Click);
			// 
			// lblPacketing
			// 
			this.lblPacketing.AutoSize = true;
			this.lblPacketing.Dock = System.Windows.Forms.DockStyle.Right;
			this.lblPacketing.ForeColor = System.Drawing.Color.Gray;
			this.lblPacketing.Location = new System.Drawing.Point(24, 132);
			this.lblPacketing.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
			this.lblPacketing.Name = "lblPacketing";
			this.lblPacketing.Size = new System.Drawing.Size(35, 23);
			this.lblPacketing.TabIndex = 15;
			this.lblPacketing.Text = "出单:";
			this.lblPacketing.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// wb
			// 
			this.wb.Location = new System.Drawing.Point(1, 1);
			this.wb.MinimumSize = new System.Drawing.Size(20, 20);
			this.wb.Name = "wb";
			this.wb.Size = new System.Drawing.Size(804, 360);
			this.wb.TabIndex = 1;
			// 
			// OrderDetailsControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.wb);
			this.Controls.Add(this.tblMain);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "OrderDetailsControl";
			this.Padding = new System.Windows.Forms.Padding(1, 1, 1, 20);
			this.Size = new System.Drawing.Size(960, 120);
			this.SizeChanged += new System.EventHandler(this.OrderDetailsControl_SizeChanged);
			this.tblMain.ResumeLayout(false);
			this.tblMain.PerformLayout();
			this.tblHeaderInfo.ResumeLayout(false);
			this.tblHeaderInfo.PerformLayout();
			this.tblDetails.ResumeLayout(false);
			this.tblDetails.PerformLayout();
			this.pnlEditedAddress.ResumeLayout(false);
			this.pnlEditedAddress.PerformLayout();
			this.tblPacket.ResumeLayout(false);
			this.tblPacket.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tblMain;
		private System.Windows.Forms.TableLayoutPanel tblHeaderInfo;
		private System.Windows.Forms.Label lblDealTime;
		private System.Windows.Forms.Button btnPreparePacket;
		private BuyerInfoControl buyerInfo;
		private OrderMoneyControl orderMoney;
		private OrderIdControl orderId;
		private System.Windows.Forms.Label lblPayingTime;
		private System.Windows.Forms.TableLayoutPanel tblDetails;
		private System.Windows.Forms.Label lblBuyerRemarkTitle;
		private System.Windows.Forms.Label lblRemarkTitle;
		private System.Windows.Forms.TextBox txtBuyerRemark;
		private System.Windows.Forms.TextBox txtRemark;
		private System.Windows.Forms.Label lblAddressTitle;
		private System.Windows.Forms.TextBox txtAddress;
		private System.Windows.Forms.Label lblPinyinAddressTitle;
		private System.Windows.Forms.TextBox txtPinyinAddress;
		private System.Windows.Forms.Label lblEditedAddressTitle;
		private System.Windows.Forms.Label lblProductListTitle;
		private ProductListControl productList;
		private System.Windows.Forms.FlowLayoutPanel pnlEditedAddress;
		private System.Windows.Forms.TextBox txtEditedAddress;
		private System.Windows.Forms.Button btnGetFullEditedAddress;
		private System.Windows.Forms.TableLayoutPanel tblPacket;
		private System.Windows.Forms.Label lblPacketing;
		private System.Windows.Forms.Label lblNetWeight;
		private System.Windows.Forms.Label lblPacket;
		private System.Windows.Forms.ComboBox cboPackets;
		private System.Windows.Forms.Button btnAddPacket;
		private System.Windows.Forms.Button btnMarkPrepared;
		private System.Windows.Forms.WebBrowser wb;
	}
}
