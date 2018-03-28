//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Drawing.Printing;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Windows.Forms;
//using System.Diagnostics;
//using System.Net;
//using System.Net.Sockets;
//using System.IO;
//using System.Resources;
//using Egode.WebBrowserForms;
//using System.Management;
//using OrderLib;

//namespace Egode
//{
//    public partial class ManualPrintForm : Form
//    {
//        List<SoldProductInfo> _soldProductInfos;
//        private int _manualStockoutCount = 0;
	
//        //private static Image _ytoSeal;
		
//        private Timer _tmrFlashSf;
//        private Timer _tmrFlashYto;
//        private Timer _tmrFlashZto;
//        private Timer _tmrFlashBest;
		
//        public ManualPrintForm()
//        {
//            //ResourceManager rm = new ResourceManager("Egode.Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());
//            //_ytoSeal = (Image)rm.GetObject("yto_seal");
//            InitializeComponent();
			
//            chkSfOldBill.Checked = DateTime.Now < new DateTime(2015, 4, 1, 0, 0, 0);
//        }

//        private void ConsignShForm_Load(object sender, EventArgs e)
//        {
//            // Sender >>>>
//            // Added by KK on 2016/09/29.
//            // Distributors list.
//            foreach (Distributor d in Distributor.Distributors)
//                cboDistributors.Items.Add(d);

//            txtSenderTel.Text = ShopProfile.Current.Phone;//"18964913317";
//            lblDistributorFlag.Visible = true;
//            // Sender <<<<

//            LayoutControls();

//            txtBillNumber.Focus();
			
//            this.Activated += new EventHandler(ConsignShForm_Activated);
//        }

//        void ConsignShForm_Activated(object sender, EventArgs e)
//        {
//            txtBillNumber.Focus();
//        }
		
//        private void ConsignShForm_Shown(object sender, EventArgs e)
//        {
//            //if (_orders[0].Remark.Contains("顺丰") || _orders[0].Remark.Contains("顺风") || _orders[0].Remark.Contains("圆通"))
//            //{
//            //    MessageBox.Show(
//            //        this, 
//            //        "包含指定快递! \n可能需要手动选择快递公司.", this.Text,
//            //        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//            //}

//            //lblSfReason.Text = string.Empty;
//            //if (_orders[0].Remark.Contains("顺丰"))
//            //{
//            //    //string prompt = string.Format("此订单可能需要发顺丰.\n客服备注: {0}\n\n是否发顺丰?\n", _orders[0].Remark);
//            //    //DialogResult dr = MessageBox.Show(this, prompt, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
//            //    rdoSf.Checked = true;
//            //    lblSfReason.Text = "(客服备注)";
//            //    lblSfReason.ForeColor = Color.DodgerBlue;
//            //}
//            //else if (!IsJiangZheHu())
//            //{
//            //    int c = GetProductCount();

//            //    if (c >= 4)
//            //    {
//            //        rdoSf.Checked = true;
//            //        lblSfReason.Text = "(顺丰包邮)";
//            //        lblSfReason.ForeColor = Color.BlueViolet;
//            //    }
//            //    else if (c >= 2 && IsFarProvince())
//            //    {
//            //        rdoSf.Checked = true;
//            //        lblSfReason.Text = "(邮费建议)";
//            //        lblSfReason.ForeColor = Color.DarkGreen;
//            //    }
//            //}
		
//            rdoZto.Checked = true;

//            this.Activate();
//            btnPrint.Focus();
//        }

//        void _tmrFlashYto_Tick(object sender, EventArgs e)
//        {
//            _tmrFlashYto.Tag = !(bool)_tmrFlashYto.Tag;
//            rdoYto.ForeColor = (bool)_tmrFlashYto.Tag ? this.ForeColor : Color.FromKnownColor(KnownColor.Window);
//        }

//        void _tmrFlashSf_Tick(object sender, EventArgs e)
//        {
//            _tmrFlashSf.Tag = !(bool)_tmrFlashSf.Tag;
//            rdoSf.ForeColor = (bool)_tmrFlashSf.Tag ? this.ForeColor : Color.FromKnownColor(KnownColor.Window);
//        }
		
//        void _tmrFlashZto_Tick(object sender, EventArgs e)
//        {
//            _tmrFlashZto.Tag = !(bool)_tmrFlashZto.Tag;
//            rdoZto.ForeColor = (bool)_tmrFlashZto.Tag ? this.ForeColor : Color.FromKnownColor(KnownColor.Window);
//        }
		
//        void _tmrFlashBest_Tick(object sender, EventArgs e)
//        {
//            _tmrFlashBest.Tag = !(bool)_tmrFlashBest.Tag;
//            rdoBest.ForeColor = (bool)_tmrFlashBest.Tag ? this.ForeColor : Color.FromKnownColor(KnownColor.Window);
//        }
		
//        private int GetProductCount()
//        {
//            int c = 0;
//            if (null != _soldProductInfos && _soldProductInfos.Count > 0)
//            {
//                foreach (SoldProductInfo spi in _soldProductInfos)
//                {
//                    if (!IsAptamilHippMilk(spi.Id))
//                        continue;
//                    c += spi.Count;
//                }
//            }
			
//            return c;
//        }

//        private bool IsAptamilHippMilk(string productId)
//        {
//            if (string.IsNullOrEmpty(productId))
//                return false;
//            if (productId.Equals("001-0001") || productId.Equals("001-0002") || productId.Equals("001-0003") || productId.Equals("001-0004") || productId.Equals("001-0005") || productId.Equals("001-0006"))
//                return true;
//            if (productId.Equals("001-0012") || productId.Equals("001-0013") || productId.Equals("001-0014") || productId.Equals("001-0015"))
//                return true;
//            if (productId.Equals("001-0016") || productId.Equals("001-0017") || productId.Equals("001-0018"))
//                return true;

//            if (productId.Equals("002-0001") || productId.Equals("002-0002") || productId.Equals("002-0003") || productId.Equals("002-0004") || productId.Equals("002-0005"))
//                return true;
//            if (productId.Equals("002-0006") || productId.Equals("002-0007") || productId.Equals("002-0008") || productId.Equals("002-0009") || productId.Equals("002-0010") || productId.Equals("002-0011"))
//                return true;
			
//            return false;
//        }

//        //private bool ContainsBondedProducts(List<Order> orders)
//        //{
//        //    if (null == orders || orders.Count <= 0)
//        //        return false;
			
//        //    foreach (Order o in orders)
//        //    {
//        //        if (o.Items.Contains("保税区"))
//        //            return true;
//        //    }
//        //    return false;
//        //}

//        // 从多个订单中提取产品信息, 相同产品合并计数.
//        // includeAllStatus: 是否包含所有状态商品. 如否, 则只提取待发货商品. // Added by KK on 2016/03/18.
//        public static List<SoldProductInfo> GetProducts(List<Order> orders, out int cMustSendFromDe, out int cBonded, bool includeAllStatus)
//        {
//            cMustSendFromDe = 0;
//            cBonded = 0;
//            SortedList<string, SoldProductInfo> sortedProducts = new SortedList<string, SoldProductInfo>();

//            foreach (Order o in orders)
//            {
//                string allItems = o.Items;
//                string[] items = allItems.Split('★');
//                for (int i = 0; i < items.Length; i++)
//                {
//                    string item = items[i];
//                    string[] infos = item.Split(new char[]{'☆'}, StringSplitOptions.RemoveEmptyEntries);
//                    if (infos.Length < 3)
//                        continue;

//                    if (string.IsNullOrEmpty(infos[0]))
//                        Trace.WriteLine("null product found!!!");

//                    string productTitle = infos[0];
//                    int count = int.Parse(infos[2]);
					
//                    // added by KK on 2016/06/06.
//                    string skuCode = string.Empty;
//                    if (infos.Length >= 5)
//                        skuCode = infos[4];

					
//                    ProductInfo pi = null; 
//                    if (string.IsNullOrEmpty(skuCode))
//                        pi = ProductInfo.Match(productTitle, o.Remark);
//                    else
//                    {
//                        pi = ProductInfo.GetProductBySkuCode(skuCode);
//                        if (null == pi)
//                            pi = ProductInfo.Match(productTitle, o.Remark);
//                    }
//                    if (null == pi)
//                        continue;
						
//                    for (int c = 16; c > 0; c--)
//                    {
//                        if (productTitle.Contains(string.Format("{0}盒装", c)) || productTitle.Contains(string.Format("{0}罐装", c)))
//                        {
//                            count *= c;
//                            break;
//                        }
//                    }
					
//                    if (productTitle.Contains("Knoppers"))
//                        count *= 10;
					
//                    Order.OrderStatus status = (Order.OrderStatus)Enum.Parse(typeof(Order.OrderStatus), infos[3]);
//                    bool succeeded = false;
//                    bool cancelled = false;
//                    bool sent = false;
//                    if (infos.Length >= 4)
//                    {
//                        succeeded = (status == Order.OrderStatus.Succeeded);
//                        cancelled = (status == Order.OrderStatus.Closed);
//                        sent = (status == Order.OrderStatus.Sent);
//                    }

//                    if (!includeAllStatus) // Added by KK on 2016/03/18.
//                    {
//                        if (succeeded || cancelled || sent)
//                            continue;
//                    }
					
//                    string taobaoCode = string.Empty;
//                    if (infos.Length >= 5)
//                        taobaoCode = infos[4];
//                    if (taobaoCode.ToLower().StartsWith("d-"))
//                    {
//                        cMustSendFromDe++;
//                        continue;
//                    }
//                    // modified by KK on 2015/08/21.
//                    // 直邮、包税、现货链接合并, 不再从标题识别某个链接是保税区专用链接.
//                    //if (code.ToLower().StartsWith("b-") || productTitle.Contains("保税区"))
//                    //if (code.ToLower().StartsWith("b-"))
//                    //{
//                    //    cBonded++;
//                    //    continue;
//                    //}

//                    bool bingo = false;
//                    foreach (SoldProductInfo spi in sortedProducts.Values)
//                    {
//                        if (spi.Id.Equals(pi.Id))
//                        {
//                            bingo = true;
//                            spi.AddCount(count);
//                            break;
//                        }
//                    }

//                    if (!bingo)
//                        sortedProducts.Add(pi.Id, new SoldProductInfo(pi.Id, pi.BrandId, pi.SkuCode, pi.NingboId, pi.DangdangCode, pi.Name, pi.Price, pi.Specification, pi.ShortName, pi.Keywords, pi.Conflict, count, status, taobaoCode));
//                        //products.Add(new SoldProductInfo(pi.Id, pi.BrandId, pi.Name, pi.Price, pi.Specification, pi.ShortName, pi.Keywords, count, status));
//                }
//            }
			
//            List<SoldProductInfo> products = new List<SoldProductInfo>();
//            foreach (SoldProductInfo p in sortedProducts.Values)
//                products.Add(p);
//            return products;
//        }
		
//        private string GetWeight(List<SoldProductInfo> soldProductInfos)
//        {
//            if (null == soldProductInfos || soldProductInfos.Count <= 0)
//                return string.Empty;
//            if (soldProductInfos.Count > 1)
//                return string.Empty;
			
//            if (soldProductInfos[0].Id == "001-0001" || soldProductInfos[0].Id == "001-0002" || soldProductInfos[0].Id == "001-0003" || soldProductInfos[0].Id == "001-0004")
//            {
//                if (soldProductInfos[0].Count == 1)
//                    return "1.18";
//                if (soldProductInfos[0].Count == 2)
//                    return "2.23";
//                if (soldProductInfos[0].Count == 4)
//                    return "4.52";
//            }

//            if (soldProductInfos[0].Id == "001-0005" || soldProductInfos[0].Id == "001-0006")
//            {
//                if (soldProductInfos[0].Count == 1)
//                    return "0.85";
//                if (soldProductInfos[0].Count == 2)
//                    return "1.71";
//                if (soldProductInfos[0].Count == 3)
//                    return "2.40";
//                if (soldProductInfos[0].Count == 4)
//                    return "3.26";
//                if (soldProductInfos[0].Count == 5)
//                    return "4.31";
//                if (soldProductInfos[0].Count == 6)
//                    return "4.91";
//            }
			
//            return string.Empty;
//        }

//        private void btnPrint_Click(object sender, EventArgs e)
//        {
//            Cursor.Current = Cursors.WaitCursor;
			
//            if (!rdoYto.Checked && !rdoSf.Checked && !rdoBest.Checked && ! rdoZto.Checked)
//            {
//                MessageBox.Show(this, "选择快递公司先.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                return;
//            }
			
//            if (txtProducts.Text.Trim().Length <= 0)
//            {
//                DialogResult dr = MessageBox.Show(this, "没有任何商品信息, 是否继续打印?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
//                if (DialogResult.No == dr)
//                    return;
//            }

//            if (btnPrint.Text.Equals("Printed"))
//            {
//                DialogResult dr = MessageBox.Show(this, "已打印, 是否再次打印?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
//                if (DialogResult.No == dr)
//                    return;
//            }

//            //if (rdoSf.Checked)
//            //    MessageBox.Show(this, "发顺丰!\n确认是否顺丰面单!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

//            string printerDocumentName = string.Format("{0}({1})", txtRecipientName.Text, txtBuyerAccount.Text);
//            BillPrinterBase printer;
//            if (rdoYto.Checked || rdoBest.Checked) // yto
//            {
//                printer = new YtoPrinter(printerDocumentName);
//                printer.PrinterName = Settings.Instance.YtoPrinter;
//            }
//            else if (rdoZto.Checked)
//            {
//                printer = new ZtoPrinter(printerDocumentName);
//                printer.PrinterName = Settings.Instance.YtoPrinter;
//            }
//            else // sf
//            {
//                string destCityCode = string.Empty;
//                if (chkSfOldBill.Checked)
//                {
//                    printer = new SfPrinter(printerDocumentName);
//                    printer.PrinterName = Settings.Instance.SfPrinter;

//                    destCityCode = CityCodes.GetCityCode(txtProvince.Text.Trim());
//                    if (string.IsNullOrEmpty(destCityCode))
//                        destCityCode = CityCodes.GetCityCode(txtCity1.Text.Trim());
//                    ((SfPrinter)printer).DestCode = destCityCode;
//                }
//                else
//                {
//                    SfOrder sforder = null;
//                    if (chkAutoSfBillNumber.Checked)
//                    {
//                        sforder = new SfOrder();
//                        sforder.Province = txtProvince.Text.Trim();
//                        sforder.City1 = txtCity1.Text.Trim();
//                        sforder.City2 = txtCity2.Text.Trim();
//                        sforder.District = txtDistrict.Text.Trim();
//                        sforder.StreetAddress = txtStreetAddress.Text.Trim();
//                        sforder.RecipientName = txtRecipientName.Text.Trim();
//                        sforder.Mobile = txtMobile.Text.Trim();
//                        sforder.Phone = txtPhone.Text.Trim();

//                        //  check arrivable.
//                        bool arrivable = sforder.Arrivable(_orders[0].OrderId);
//                        if (!string.IsNullOrEmpty(sforder.ErrorCode))
//                        {
//                            MessageBox.Show(
//                                this,
//                                string.Format("筛单时发生错误: 错误代码={0}, 错误信息={1}", sforder.ErrorCode, sforder.ErrorMessage),
//                                this.Text,
//                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                            return;
//                        }
//                        if (!arrivable)
//                        {
//                            DialogResult dr = MessageBox.Show(
//                                this,
//                                string.Format("此地址顺丰不到, 可能需要收件人自提.\n{0}\n\n是否继续发顺丰?",
//                                    string.Format("{0} {1} {2} {3} {4}", txtProvince.Text.Trim(), txtCity1.Text.Trim(), txtCity2.Text.Trim(), txtDistrict.Text.Trim(), txtStreetAddress.Text.Trim())),
//                                this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
//                            if (DialogResult.No == dr)
//                                return;
//                        }

//                        sforder.RunOrderService(_orders[0].OrderId);
//                        if (!string.IsNullOrEmpty(sforder.ErrorCode))
//                        {
//                            MessageBox.Show(
//                                this,
//                                string.Format("获取顺丰单号时发生错误: 错误代码={0}, 错误信息={1}", sforder.ErrorCode, sforder.ErrorMessage),
//                                this.Text,
//                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                            return;
//                        }
						
//                        // Got mail number but no dest code. Address may be invalid.
//                        if (string.IsNullOrEmpty(sforder.DestCode))
//                        {
//                            MessageBox.Show(
//                                this,
//                                string.Format("未获得目标地区代码(DestCode).\n请检查是否地址有误并重新打印."),
//                                this.Text,
//                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                            return;
//                        }

//                        txtBillNumber.Text = sforder.MailNumber;
//                        txtDestCode.Text = sforder.DestCode;
//                    }
//                    else
//                    {
//                        if (txtBillNumber.Text.Length < 12)
//                        {
//                            MessageBox.Show(
//                                this,
//                                "请手动输入正确的顺丰单号.",
//                                this.Text,
//                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                            txtBillNumber.Focus();
//                            return;
//                        }
//                    }
					
//                    printer = new SfBillNewPrinter(txtBillNumber.Text, printerDocumentName);
//                    printer.PrinterName = Settings.Instance.SfNewPrinter;
//                    ((SfBillNewPrinter)printer).DestCode = txtDestCode.Text;

//                    int count = 0;
//                    if (null != _soldProductInfos && _soldProductInfos.Count > 0)
//                    {
//                        foreach (SoldProductInfo spi in _soldProductInfos)
//                            count += spi.Count;
//                    }
//                    ((SfBillNewPrinter)printer).ItemAmount = count;
//                }
				
//                ((SfPrinter)printer).IsFreightCollect = chkFriehghtCollect.Checked;
//                ((SfPrinter)printer).IsPickup = chkPickup.Checked;
//            }
			
//            if (!BillPrinterBase.PrinterIsOnline(printer.PrinterName))
//            {
//                MessageBox.Show(this, "打印机处于脱机状态, 请先修改打印机状态再重新打印!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                return;
//            }

//            string senderName = txtSenderName.Text;
//            if (txtSenderName.Text.Equals("德 国 e 购") || txtSenderName.Text.Equals("buy欧洲"))
//                senderName = string.IsNullOrEmpty(txtSenderName.Text) ? string.Empty : string.Format("{0}****", txtSenderName.Text.Substring(0, 1));
//            string buyerAccount = string.IsNullOrEmpty(txtBuyerAccount.Text) ? string.Empty : string.Format("{0}***{1}", txtBuyerAccount.Text.Substring(0, 1), txtBuyerAccount.Text.Substring(txtBuyerAccount.Text.Length-1, 1));

//            printer.Font = new Font("Microsoft Yahei", 11);
//            printer.SenderName = senderName; //txtSenderName.Text.Trim();
//            printer.SenderAd = txtSenderAd.Text.Trim();
//            printer.SenderTel = txtSenderTel.Text.Trim();
//            printer.DisplayBuyerAccount = (txtSenderName.Text.Trim().Equals(ShopProfile.Current.DisplayNameOnBill));
//            printer.RecipientName = txtRecipientName.Text.Trim();
//            printer.BuyerAccount = buyerAccount; //txtBuyerAccount.Text.Trim();
//            printer.Province = txtProvince.Text;
//            printer.City1 = txtCity1.Text;
//            printer.City2 = txtCity2.Text;
//            printer.District = txtDistrict.Text;
//            printer.StreetAddress = txtStreetAddress.Text;
//            printer.Mobile = txtMobile.Text.Trim();
//            printer.Phone = txtPhone.Text.Trim();
//            printer.HolidayDelivery = chkHoliday.Checked;
//            printer.ProductInfos = txtProducts.Lines;
//            printer.Print();

//            #region obsoleted code on 2015/01/09
//            //Size docSize = new Size(1000, 1000);
//            //if (rdoYto.Checked)
//            //    docSize = new Size(900, 550);
//            //else if (rdoSf.Checked)
//            //    docSize = new Size(780, 550);

//            //PrintDocument pdoc = new PrintDocument();
//            //pdoc.DocumentName = string.Format("{0}({1})", txtRecipientName.Text, txtBuyerAccount.Text);
//            //pdoc.DefaultPageSettings.PaperSize = new PaperSize("custom", docSize.Width, docSize.Height);
//            //pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
//            //pdoc.Print();
//            #endregion

//            this.Focus();
//            txtBillNumber.Focus();
//            btnPrint.Text = "Printed";
//            btnPrint.ForeColor = Color.OrangeRed;
//            this.Activate();
			
//            // Added by KK on 2017/06/22.
//            // Update address database.
//            string addrType = ((txtBuyerAccount.Text.Trim().Length > 0 && !txtBuyerAccount.Text.Trim().StartsWith("@")) ? "tb" : string.Empty);
			
//            Address addr = new Address(addrType, txtBuyerAccount.Text, DateTime.Now, 
//                txtProvince.Text, txtCity1.Text, txtCity2.Text, txtDistrict.Text, txtStreetAddress.Text, 
//                txtRecipientName.Text, txtMobile.Text, txtPhone.Text, "000000", string.Empty);
//            if (Addresses.Instance.Add(addr))
//                Addresses.AddToServer(addr);

//            Cursor.Current = Cursors.Default;
//        }

//        #region obsoleted code on 2015/01/09.
//        //void pdoc_PrintPage(object sender, PrintPageEventArgs e)
//        //{
//        //    Graphics g = e.Graphics;
			
//        //    if (rdoYto.Checked)
//        //        DrawBillYto(g);
//        //    else if (rdoSf.Checked)
//        //        DrawBillSf(g);
				
//        //    txtBillNumber.Focus();
//        //}
//        #endregion

//        #region obsolected code for old yto bill.
//        //void DrawBill(Graphics g, bool withBg)
//        //{
//        //    if (null == g)
//        //        return;
			
//        //    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
//        //    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

//        //    if (withBg)
//        //        g.DrawImage(picExpressBill.BackgroundImage, new Point(0, 0));

//        //    DrawString(g, "德 国 e 购", new Point(150, 131), this.Font.Size + 4, FontStyle.Regular);
//        //    DrawString(g, "http://egode.taobao.com", new Point(260, 140), this.Font.Size - 2, FontStyle.Regular);
//        //    DrawString(g, "18964913317", new Point(172, 235));
//        //    DrawString(g, DateTime.Now.ToString("yyyy/MM/dd"), new Point(120, 405));

//        //    DrawString(g, txtRecipientName.Text, new Point(510, 120));
//        //    if (txtBuyerAccount.Text.Length > 0)
//        //        DrawString(g, string.Format("({0})", txtBuyerAccount.Text), new Point(510, 145), this.Font.Size - 2, FontStyle.Regular);

//        //    DrawString(g, txtProvince.Text, new Point(585 - g.MeasureString(txtProvince.Text, this.Font).ToSize().Width, 176));
			
//        //    Size city1Size = g.MeasureString(txtCity1.Text, this.Font).ToSize();
//        //    DrawString(g, txtCity1.Text, new Point(660 - city1Size.Width, city1Size.Width > 72 ? 157 : 176));

//        //    Size city2Size = g.MeasureString(txtCity2.Text, this.Font).ToSize();
//        //    DrawString(g, txtCity2.Text, new Point(730 - city2Size.Width, city2Size.Width > 72 ? 157 : 176));

//        //    Size districtSize = g.MeasureString(txtDistrict.Text, this.Font).ToSize();
//        //    DrawString(g, txtDistrict.Text, new Point(790 - districtSize.Width, districtSize.Width > 72 ? 157 : 176));
			
//        //    string destCity = string.Empty;
//        //    if (string.IsNullOrEmpty(txtCity1.Text) && string.IsNullOrEmpty(txtCity2.Text)) // 直辖市
//        //    {
//        //        if (txtProvince.Text.StartsWith("上海"))
//        //        {
//        //            if (txtDistrict.Text.StartsWith("浦东") || txtDistrict.Text.StartsWith("南汇"))
//        //                destCity = "PD";//"浦东";
//        //            else
//        //                destCity = "PX";//"浦西";
//        //        }
//        //        else
//        //        {
//        //            destCity = txtProvince.Text;
//        //        }
//        //    }
//        //    else
//        //    {
//        //        destCity = (string.IsNullOrEmpty(txtCity2.Text) ? txtCity1.Text : txtCity2.Text);
//        //    }
			
//        //    Size destCitySize = g.MeasureString(destCity, new Font(this.Font.Name, this.Font.Size + 10, FontStyle.Bold)).ToSize();
//        //    DrawString(
//        //        g, destCity, 
//        //        new Point(destCitySize.Width <= 120 ? 700 + (120 - destCitySize.Width)/2 : 820 - destCitySize.Width, 65), 
//        //        this.Font.Size + 10, FontStyle.Bold);

//        //    if (g.MeasureString(txtStreetAddress.Text, this.Font).Width > 320)
//        //        DrawString(g, txtStreetAddress.Text, new Point(830 - g.MeasureString(txtStreetAddress.Text, this.Font).ToSize().Width, 212));
//        //    else
//        //        DrawString(g, txtStreetAddress.Text, new Point(450 + (350 - g.MeasureString(txtStreetAddress.Text, this.Font).ToSize().Width)/2, 212));

//        //    DrawString(g, txtMobile.Text, new Point(545, 235));
//        //    DrawString(g, txtPhone.Text, new Point(710, 235));

//        //    // products
//        //    // items.
//        //    int y = 290;
//        //    //foreach (ProductInfo pi in _soldProductInfos)
//        //    foreach (string s in txtProducts.Lines)
//        //    {
//        //        //DrawString(g, string.Format("{0} x {1}", ProductInfo.GetProductDesc(pi.Id), pi.Count), new Point(160, y));
//        //        DrawString(g, s, new Point(160, y));
//        //        y += 20;
//        //    }
			
//        //    // Weight.
//        //    if (!_productDescChanged)
//        //    {
//        //        string weight = GetWeight(_soldProductInfos);
//        //        DrawString(g, weight, new Point(480, 265));
//        //    }
//        //}
//        #endregion

//        #region obsoleted code on 2015/01/09
//        //void DrawBillYto(Graphics g)
//        //{
//        //    if (null == g)
//        //        return;
			
//        //    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
//        //    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

//        //    bool displayBuyerAccount = (txtSenderName.Text.Trim().Equals(ShopProfile.Current.DisplayNameOnBill));
			
//        //    DrawString(g, txtSenderName.Text, new Point(70, 112), this.Font.Size, FontStyle.Regular);
//        //    DrawString(g, txtSenderAd.Text, new Point(70, 135), this.Font.Size - 2, FontStyle.Regular);
//        //    DrawString(g, txtSenderTel.Text, new Point(92, 227));

//        //    DrawString(g, DateTime.Now.ToString("yyyy/MM/dd"), new Point(265, 392));
//        //    DrawString(g, txtRecipientName.Text, new Point(430, 112));
//        //    if (displayBuyerAccount && txtBuyerAccount.Text.Length > 0)
//        //        DrawString(
//        //            g, string.Format("({0})", txtBuyerAccount.Text), 
//        //            new Point(430 + g.MeasureString(txtRecipientName.Text, this.Font).ToSize().Width + 1, 116), 
//        //            this.Font.Size - 3, FontStyle.Regular);

//        //    /*
//        //    DrawString(g, txtProvince.Text, new Point(585 - g.MeasureString(txtProvince.Text, this.Font).ToSize().Width, 155));
			
//        //    Size city1Size = g.MeasureString(txtCity1.Text, this.Font).ToSize();
//        //    DrawString(g, txtCity1.Text, new Point(660 - city1Size.Width, city1Size.Width > 72 ? 135 : 155));

//        //    Size city2Size = g.MeasureString(txtCity2.Text, this.Font).ToSize();
//        //    DrawString(g, txtCity2.Text, new Point(730 - city2Size.Width, city2Size.Width > 72 ? 135 : 155));

//        //    Size districtSize = g.MeasureString(txtDistrict.Text, this.Font).ToSize();
//        //    DrawString(g, txtDistrict.Text, new Point(790 - districtSize.Width, districtSize.Width > 72 ? 135 : 155));
			
//        //    if (g.MeasureString(txtStreetAddress.Text, this.Font).Width > 320)
//        //        DrawString(g, txtStreetAddress.Text, new Point(830 - g.MeasureString(txtStreetAddress.Text, this.Font).ToSize().Width, 182));
//        //    else
//        //        DrawString(g, txtStreetAddress.Text, new Point(450 + (350 - g.MeasureString(txtStreetAddress.Text, this.Font).ToSize().Width)/2, 182));
//        //    */
//        //    string fullAddress = string.Format("{0} {1} {2} {3}\n{4}", txtProvince.Text, txtCity1.Text, txtCity2.Text, txtDistrict.Text, txtStreetAddress.Text);
//        //    SizeF fullAddressSize = g.MeasureString(fullAddress, this.Font, 840-505);
//        //    int fullAddressTop = fullAddressSize.Height - 6 > g.MeasureString("A\nB\nC", this.Font).Height ? 122 : 146;
//        //    StringFormat sf = new StringFormat();
//        //    sf.Alignment = StringAlignment.Near;
//        //    sf.LineAlignment = StringAlignment.Near;
//        //    g.DrawString(fullAddress, this.Font, new SolidBrush(Color.Black), new RectangleF(430, fullAddressTop, (760-430), (225-fullAddressTop)));
			
//        //    string phone = txtMobile.Text.Trim();
//        //    if (phone.Length > 0 && txtPhone.Text.Trim().Length > 0)
//        //        phone += ", ";
//        //    if (txtPhone.Text.Trim().Length > 0)
//        //        phone += txtPhone.Text;
//        //    DrawString(g, phone, new Point(464, 227));

//        //    string destCity = string.Empty;
//        //    if (string.IsNullOrEmpty(txtCity1.Text) && string.IsNullOrEmpty(txtCity2.Text)) // 直辖市
//        //    {
//        //        if (txtProvince.Text.StartsWith("上海"))
//        //        {
//        //            if (txtDistrict.Text.StartsWith("浦东") || txtDistrict.Text.StartsWith("南汇"))
//        //                destCity = "PD";//"浦东";
//        //            else
//        //                destCity = "PX";//"浦西";
//        //        }
//        //        else
//        //        {
//        //            destCity = txtProvince.Text;
//        //        }
//        //    }
//        //    else
//        //    {
//        //        destCity = (string.IsNullOrEmpty(txtCity2.Text) ? txtCity1.Text : txtCity2.Text);
//        //    }
			
//        //    Size destCitySize = g.MeasureString(destCity, new Font(this.Font.Name, this.Font.Size + 14, FontStyle.Bold)).ToSize();
//        //    DrawString(
//        //        g, destCity, 
//        //        new Point(destCitySize.Width <= 120 ? 620 + (120 - destCitySize.Width)/2 : 740 - destCitySize.Width, 46), 
//        //        this.Font.Size + 14, FontStyle.Bold);

//        //    // products
//        //    // items.
//        //    int x = (txtProducts.Lines.Length <= 2) ? 5 : 42;
//        //    int y = (txtProducts.Lines.Length <= 2) ? 295 : 288;
//        //    if (txtProducts.Lines.Length == 1)
//        //        y += 5;
//        //    //foreach (ProductInfo pi in _soldProductInfos)
//        //    foreach (string s in txtProducts.Lines)
//        //    {
//        //        //DrawString(g, string.Format("{0} x {1}", ProductInfo.GetProductDesc(pi.Id), pi.Count), new Point(160, y));
//        //        DrawString(g, s, new Point(x, y), this.Font.Size - 2, FontStyle.Regular);
//        //        y += 14;
//        //    }
			
//        //    if (chkHoliday.Checked)
//        //        DrawString(g, "贵司派件大哥请注意，此件节假日正常派送。\n感谢合作！辛苦了！", new Point(395, 450));

//        //    //// Weight.
//        //    //if (!_productDescChanged)
//        //    //{
//        //    //    string weight = GetWeight(_soldProductInfos);
//        //    //    DrawString(g, weight, new Point(480, 265));
//        //    //}
			
//        //    //// Draw yto seal.
//        //    //if (null != _ytoSeal)
//        //    //    g.DrawImage(_ytoSeal, 250, 140);
//        //}

//        //void DrawBillSf(Graphics g)
//        //{
//        //    if (null == g)
//        //        return;
			
//        //    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
//        //    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

//        //    bool displayBuyerAccount = (txtSenderName.Text.Trim().Equals(ShopProfile.Current.DisplayNameOnBill));

//        //    string sender = string.Format("{0}\n{1}\n{2}", txtSenderName.Text.Trim(), txtSenderAd.Text.Trim(), txtSenderTel.Text.Trim());
//        //    DrawString(g, sender, new Point(60, 170), this.Font.Size, FontStyle.Regular);

//        //    DrawString(g, DateTime.Now.ToString("yyyy/MM/dd"), new Point(420, 427));

//        //    //DrawString(g, txtRecipientName.Text, new Point(125, 240));
//        //    //if (txtBuyerAccount.Text.Length > 0)
//        //    //    DrawString(
//        //    //        g, string.Format("({0})", txtBuyerAccount.Text), 
//        //    //        new Point(125 + g.MeasureString(txtRecipientName.Text, this.Font).ToSize().Width + 1, 240), 
//        //    //        this.Font.Size - 3, FontStyle.Regular);

//        //    /*
//        //    DrawString(g, txtProvince.Text, new Point(585 - g.MeasureString(txtProvince.Text, this.Font).ToSize().Width, 155));
			
//        //    Size city1Size = g.MeasureString(txtCity1.Text, this.Font).ToSize();
//        //    DrawString(g, txtCity1.Text, new Point(660 - city1Size.Width, city1Size.Width > 72 ? 135 : 155));

//        //    Size city2Size = g.MeasureString(txtCity2.Text, this.Font).ToSize();
//        //    DrawString(g, txtCity2.Text, new Point(730 - city2Size.Width, city2Size.Width > 72 ? 135 : 155));

//        //    Size districtSize = g.MeasureString(txtDistrict.Text, this.Font).ToSize();
//        //    DrawString(g, txtDistrict.Text, new Point(790 - districtSize.Width, districtSize.Width > 72 ? 135 : 155));
			
//        //    if (g.MeasureString(txtStreetAddress.Text, this.Font).Width > 320)
//        //        DrawString(g, txtStreetAddress.Text, new Point(830 - g.MeasureString(txtStreetAddress.Text, this.Font).ToSize().Width, 182));
//        //    else
//        //        DrawString(g, txtStreetAddress.Text, new Point(450 + (350 - g.MeasureString(txtStreetAddress.Text, this.Font).ToSize().Width)/2, 182));
//        //    */
//        //    string phone = txtMobile.Text.Trim();
//        //    if (phone.Length > 0 && txtPhone.Text.Trim().Length > 0)
//        //        phone += ", ";
//        //    if (txtPhone.Text.Trim().Length > 0)
//        //        phone += txtPhone.Text;

//        //    string fullAddress = string.Format(
//        //        "           {0}{1}\n{7}\n{2} {3} {4} {5}\n{6}", 
//        //        txtRecipientName.Text, 
//        //        displayBuyerAccount ? string.Format("({0})", txtBuyerAccount.Text) : string.Empty, 
//        //        txtProvince.Text, txtCity1.Text, txtCity2.Text, txtDistrict.Text, txtStreetAddress.Text,
//        //        phone);
//        //    //SizeF fullAddressSize = g.MeasureString(fullAddress, this.Font, 840-505);
//        //    //int fullAddressTop = fullAddressSize.Height - 6 > g.MeasureString("A\nB\nC", this.Font).Height ? 110 : 131;
//        //    StringFormat sf = new StringFormat();
//        //    sf.Alignment = StringAlignment.Near;
//        //    sf.LineAlignment = StringAlignment.Near;
//        //    int fullAddressTop = 250;
//        //    g.DrawString(fullAddress, this.Font, new SolidBrush(Color.Black), new RectangleF(60, fullAddressTop, (360-60), (370-fullAddressTop)));
			
//        //    //string phone = txtMobile.Text.Trim();
//        //    //if (phone.Length > 0 && txtPhone.Text.Trim().Length > 0)
//        //    //    phone += ", ";
//        //    //if (txtPhone.Text.Trim().Length > 0)
//        //    //    phone += txtPhone.Text;
//        //    //DrawString(g, phone, new Point(110, 396));
//        //    ////DrawString(g, txtPhone.Text, new Point(680, 215));

//        //    //string destCity = string.Empty;
//        //    //if (string.IsNullOrEmpty(txtCity1.Text) && string.IsNullOrEmpty(txtCity2.Text)) // 直辖市
//        //    //{
//        //    //    if (txtProvince.Text.StartsWith("上海"))
//        //    //    {
//        //    //        if (txtDistrict.Text.StartsWith("浦东") || txtDistrict.Text.StartsWith("南汇"))
//        //    //            destCity = "PD";//"浦东";
//        //    //        else
//        //    //            destCity = "PX";//"浦西";
//        //    //    }
//        //    //    else
//        //    //    {
//        //    //        destCity = txtProvince.Text;
//        //    //    }
//        //    //}
//        //    //else
//        //    //{
//        //    //    destCity = (string.IsNullOrEmpty(txtCity2.Text) ? txtCity1.Text : txtCity2.Text);
//        //    //}
			
//        //    //Size destCitySize = g.MeasureString(destCity, new Font(this.Font.Name, this.Font.Size + 14, FontStyle.Bold)).ToSize();
//        //    //DrawString(
//        //    //    g, destCity, 
//        //    //    new Point(destCitySize.Width <= 120 ? 700 + (120 - destCitySize.Width)/2 : 820 - destCitySize.Width, 46), 
//        //    //    this.Font.Size + 14, FontStyle.Bold);

//        //    // products
//        //    // items.
//        //    int y = 402;
//        //    //foreach (ProductInfo pi in _soldProductInfos)
//        //    foreach (string s in txtProducts.Lines)
//        //    {
//        //        //DrawString(g, string.Format("{0} x {1}", ProductInfo.GetProductDesc(pi.Id), pi.Count), new Point(160, y));
//        //        DrawString(g, s, new Point(20, y), this.Font.Size - 2, FontStyle.Regular);
//        //        y += 14;
//        //    }
//        //    DrawString(g, "奶粉", new Point(20, y), this.Font.Size - 2, FontStyle.Regular);
			
//        //    string empno = "4 8 8 1 3 3";
//        //    DrawString(g, empno, new Point(555, 46), this.Font.Size, FontStyle.Bold);
			
//        //    //// Weight.
//        //    //if (!_productDescChanged)
//        //    //{
//        //    //    string weight = GetWeight(_soldProductInfos);
//        //    //    DrawString(g, weight, new Point(480, 265));
//        //    //}
			
//        //    string destCityCode = CityCodes.GetCityCode(txtProvince.Text);
//        //    if (string.IsNullOrEmpty(destCityCode))
//        //        destCityCode = CityCodes.GetCityCode(txtCity1.Text);
//        //    DrawString(g, destCityCode, new Point(625, 76), this.Font.Size + 4, FontStyle.Bold);


//        //    DrawString(g, "021CF", new Point(550, 80), this.Font.Size, FontStyle.Regular);
//        //    DrawString(g, "集团客户(汽运)    此件节假日正常派送, 谢谢!", new Point(420, 450), this.Font.Size, FontStyle.Regular);
//        //}
				
//        //private void DrawString(Graphics g, string s, Point p)
//        //{
//        //    DrawString(g, s, p, this.Font.Size, FontStyle.Regular);
//        //}

//        //private void DrawString(Graphics g, string s, Point p, float fontSize, FontStyle fs)
//        //{
//        //    if (null == g)
//        //        return;
//        //    if (string.IsNullOrEmpty(s))
//        //        return;

//        //    p.Offset(10, -20);
			
//        //    int plusPos = -1;
//        //    if (s.Contains("1+") || s.Contains("2+") || s.Contains("12+"))
//        //    {
//        //        plusPos = s.IndexOf("+");
//        //        s = s.Replace("+", "  "); 
//        //    }
			
//        //    g.DrawString(
//        //        s,
//        //        new Font(this.Font.Name, fontSize, fs), new SolidBrush(Color.Black), p);/*, 
//        //        new RectangleF((float)p.X, (float)p.Y, 360, 50), 
//        //        StringFormat.GenericDefault);*/
			
//        //    if (plusPos > 0)
//        //    {
//        //        SizeF size = g.MeasureString(s.Substring(0, plusPos), new Font(this.Font.Name, fontSize, fs));
//        //        p.Offset((int)size.Width-7, -7);
//        //        g.DrawString("+", new Font(this.Font.Name, fontSize + 2, fs), new SolidBrush(Color.Black), p);
//        //    }
//        //}
//        #endregion

//        //private void txtBillNumber_TextChanged(object sender, EventArgs e)
//        //{
//        //    if (rdoSf.Checked)
//        //    {
//        //        if (txtBillNumber.Text.Length == 12) // sf
//        //        {
//        //            btnGo.Enabled = true;
//        //            btnGo_Click(btnGo, EventArgs.Empty);
//        //        }
//        //        return;
//        //    }
			
//        //    if (txtBillNumber.Text.Length == 10) // yto
//        //    {
//        //        btnGo.Enabled = true;
//        //        btnGo_Click(btnGo, EventArgs.Empty);
//        //    }
//        //}

//        private void txtBillNumber_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            //if (((int)e.KeyChar >= 0x30 && (int)e.KeyChar <= 0x39) || (int)e.KeyChar == 13)
//            //{
//            //    if ((int)e.KeyChar == 13)
//            //    {
//            //        btnGo_Click(btnGo, EventArgs.Empty);
//            //    }
//            //}
//            ////else if ( (e.KeyChar == 'c' || e.KeyChar == 'C' || e.KeyChar == 'v' || e.KeyChar == 'V' || e.KeyChar == 'x' || e.KeyChar == 'X'))
//            //else
//            //{
//            //    e.Handled = true;
//            //}
//        }

//        private void txtBillNumber_KeyUp(object sender, KeyEventArgs e)
//        {

//        }

//        private void txtBillNumber_KeyDown(object sender, KeyEventArgs e)
//        {
//            if ((e.KeyValue >= 0x30 && e.KeyValue <= 0x39) || e.KeyValue == 13)
//            {
//            }
//            else if (e.Control && (e.KeyValue == 'c' || e.KeyValue == 'C' || e.KeyValue == 'v' || e.KeyValue == 'V' || e.KeyValue == 'x' || e.KeyValue == 'X'))
//            {
//            }
//            else if (e.KeyValue == 37 || e.KeyValue == 39) // left arrow and right arrow
//            {
//            }
//            else
//            {
//                e.Handled = true;
//            }
//        }

//        private void ConsignShForm_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            //Trace.WriteLine(e.KeyChar);
//            if (e.KeyChar >= '0' && e.KeyChar <= '9')
//            {
//                txtBillNumber.Focus();
//                txtBillNumber.Text += new string(new char[] { e.KeyChar });
//                e.Handled = true;
//            }
//        }

//        private void btnGo_Click(object sender, EventArgs e)
//        {
//            Cursor.Current = Cursors.WaitCursor;
			
//            if (txtBillNumber.Text.Trim().Length < 12)
//            {
//                MessageBox.Show(this, "The shipment number of both Sf and Yto is 12.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                return;
//            }
			
//            OrderLib.ShipmentCompanies ec = OrderLib.ShipmentCompanies.Zto;
//            if (rdoSf.Checked)
//                ec = OrderLib.ShipmentCompanies.Sf;
//            else if (rdoZto.Checked)
//                ec = OrderLib.ShipmentCompanies.Zto;
//            else if (rdoBest.Checked)
//                ec = OrderLib.ShipmentCompanies.Best;

//            // 点发货.
//            DialogResult dr = DialogResult.Cancel;
//            foreach (Order o in _orders)
//            {
//                ConsignShWebBrowserForm.Instance.OrderId = o.OrderId;
//                ConsignShWebBrowserForm.Instance.BillNumber = txtBillNumber.Text;
//                ConsignShWebBrowserForm.Instance.ShipmentCompany = ec;
//                ConsignShWebBrowserForm.Instance.AutoSubmit = !chkPartial.Checked;
//                dr = ConsignShWebBrowserForm.Instance.ShowDialog(this);

//                //if (DialogResult.Retry == dr) // retry.
//                //    dr = ConsignShWebBrowserForm.Instance.ShowDialog(this);

//                if (DialogResult.OK != dr)
//                    break;
//                o.Consign();
//            }

//            if (DialogResult.OK != dr)
//            {
//                MessageBox.Show(this, "未正确点发货, 放弃更新库存操作, 请自行核对并进行相应操作!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                return;
//            }

//            // Removed by KK on 2015/05/24. Replaced by _productInfoChanged.
//            //if (_productDescChanged)
//            //{
//            //    if (DialogResult.No == MessageBox.Show(this, "商品描述被修改过, 是否继续按订单商品更新库存?\n", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
//            //        return;
//            //}
//            if (_productInfoChanged) // Added by KK on 2015/05/24.
//            {
//                if (DialogResult.No == MessageBox.Show(this, "商品信息被修改过.\n是否按修改过的商品信息更新库存.\n", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
//                    return;
//            }
//            else if (_manualStockoutCount > 0)
//            {
//                if (DialogResult.No == MessageBox.Show(this, "已经手动出库, 是否继续更新库存?\n", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
//                    return;
//            }

//            // 出库登记.
//            try
//            {
//                Application.DoEvents();
//                Cursor.Current = Cursors.WaitCursor;
			
//                List<SoldProductInfo> soldProductInfos = new List<SoldProductInfo>();
//                foreach (Control c in pnlProductList.Controls)
//                {
//                    if (!(c is SoldProductInfoControl))
//                        continue;
//                    SoldProductInfoControl spic = c as SoldProductInfoControl;
//                    if (spic.Count == 0)
//                        continue;
//                    if (spic.SelectedProductInfo.Id.Equals("0"))
//                        continue;
//                    SoldProductInfo spi = new SoldProductInfo(spic.SelectedProductInfo);
//                    spi.Count = spic.Count;
//                    soldProductInfos.Add(spi);
//                }
				
//                string result = StockActionAdvForm.StockAction(
//                    true, 
//                    soldProductInfos, 
//                    (ShopProfile.Current.Shop==ShopEnum.Egode?string.Empty:(ShopProfile.Current.ShortName + "\\")) + string.Format("{0} [{1}]", txtBuyerAccount.Text, txtRecipientName.Text),
//                    ec.ToString().ToLower() + txtBillNumber.Text,
//                    OrderLib.ShippingOrigins.Shanghai);
					
//                MessageBox.Show(
//                    this,
//                    "Result from server: \n" + result,
//                    this.Text,
//                    MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//            }
//            finally
//            {
//                Cursor.Current = Cursors.Default;
//            }
			
//            this.Close();
			
//            Cursor.Current = Cursors.Default;
//        }

//        private void pnlProductList_Paint(object sender, PaintEventArgs e)
//        {
//            ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, pnlProductList.Width, pnlProductList.Height), Color.LightGray, ButtonBorderStyle.Solid);
//        }

//        private void pnlPrint_Paint(object sender, PaintEventArgs e)
//        {
//            ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, pnlPrint.Width, pnlPrint.Height), Color.LightGray, ButtonBorderStyle.Solid);
//        }

//        private void pnlAddress_Paint(object sender, PaintEventArgs e)
//        {
//            ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, pnlAddress.Width, pnlAddress.Height), Color.FromArgb(0xff, 130, 116, 205), ButtonBorderStyle.Solid);
//        }

//        private void pnlConsign_Paint(object sender, PaintEventArgs e)
//        {
//            ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, pnlConsign.Width, pnlConsign.Height), Color.LightGray, ButtonBorderStyle.Solid);
//        }

//        private void pnlSf_Paint(object sender, PaintEventArgs e)
//        {
//            ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, pnlSf.Width, pnlSf.Height), Color.FromArgb(0xff, 0xa0, 0xa0, 0xa0), ButtonBorderStyle.Solid);
//        }

//        private void OnProductsTextBoxLocationSizeChanged(object sender, EventArgs e)
//        {
//            Point p = new Point(btnPrint.Right-2, btnPrint.Bottom+1);
//            p.Offset(pnlPrint.Location);
//        }

//        private void txtHideShop_Click(object sender, EventArgs e)
//        {
//            txtSenderName.Text = string.Empty;
//            txtSenderAd.Text = string.Empty;
//        }

//        private void rdoYto_CheckedChanged(object sender, EventArgs e)
//        {
//            if (rdoYto.Checked)
//            {
//                rdoZto.Checked = true;
//                rdoSf.Checked = false;
//                rdoBest.Checked = false;
//            }
			
//            // Added by KK on 2015/11/08.
//            if (rdoYto.Checked)
//            {
//                if (!string.IsNullOrEmpty(Settings.Instance.RecentYtoBillNumber) && Settings.Instance.RecentYtoBillNumber.Length >= 9)
//                {
//                    txtBillNumber.Text = Settings.Instance.RecentYtoBillNumber.Substring(0, 9);
//                    txtBillNumber.SelectionStart = 10;
//                }
				
//                txtDestCode.Text = string.Empty;
//            }
//        }

//        // Added by KK on 2016/06/07.
//        private void rdoZto_CheckedChanged(object sender, EventArgs e)
//        {
//            if (rdoZto.Checked)
//            {
//                rdoYto.Checked = false;
//                rdoSf.Checked = false;
//                rdoBest.Checked = false;
//            }
			
//            if (rdoZto.Checked)
//            {
//                if (!string.IsNullOrEmpty(Settings.Instance.RecentZtoBillNumber) && Settings.Instance.RecentZtoBillNumber.Length >= 9)
//                {
//                    txtBillNumber.Text = Settings.Instance.RecentZtoBillNumber.Substring(0, 9);
//                    txtBillNumber.SelectionStart = 10;
//                }
				
//                txtDestCode.Text = string.Empty;
//            }
//        }

//        private void rdoSf_CheckedChanged(object sender, EventArgs e)
//        {
//            if (rdoSf.Checked)
//            {
//                rdoYto.Checked = false;
//                rdoZto.Checked = false;
//                rdoBest.Checked = false;
//            }
			
//            // Added by KK on 2015/11/08.
//            if (rdoSf.Checked) 
//                txtBillNumber.Text = string.Empty;
			
//            chkFriehghtCollect.Enabled = rdoSf.Checked;
//            chkPickup.Enabled = rdoSf.Checked;
//            chkSfOldBill.Enabled = rdoSf.Checked;
//        }

//        private void rdoYunda_CheckedChanged(object sender, EventArgs e)
//        {
//            if (rdoBest.Checked)
//            {
//                rdoSf.Checked = false;
//                rdoYto.Checked = false;
//                rdoZto.Checked = false;
//            }

//            if (rdoBest.Checked)
//            {
//                if (!string.IsNullOrEmpty(Settings.Instance.RecentBestBillNumber) && Settings.Instance.RecentBestBillNumber.Length >= 9)
//                {
//                    txtBillNumber.Text = Settings.Instance.RecentBestBillNumber.Substring(0, 9);
//                    txtBillNumber.SelectionStart = 10;
//                }
				
//                txtDestCode.Text = string.Empty;
//            }
//        }

//        private void btnAppenRemark_Click(object sender, EventArgs e)
//        {
//            // Removed by KK on 2015/12/26.
//            // Replaced by new remark form.
//            //Utility.InputBoxForm inputbox = new Egode.Utility.InputBoxForm();
//            //if (DialogResult.OK == inputbox.ShowDialog(this))
//            //{
//            //    UpdateSellMemoWebBrowserForm usmf = new UpdateSellMemoWebBrowserForm(_orders[0], inputbox.Message, true);
//            //    usmf.ShowDialog(this);
//            //}

//            OrderLib.ShipmentCompanies ec = OrderLib.ShipmentCompanies.Zto;
//            if (rdoSf.Checked)
//                ec = OrderLib.ShipmentCompanies.Sf;
//            else if (rdoZto.Checked)
//                ec = OrderLib.ShipmentCompanies.Zto;
//            else if (rdoBest.Checked)
//                ec = OrderLib.ShipmentCompanies.Best;

//            UpdateSellMemoForm usmf = new UpdateSellMemoForm(_orders[0].Remark, ec.ToString().ToLower()+txtBillNumber.Text);
//            DialogResult dr = usmf.ShowDialog(this.FindForm());
//            if (DialogResult.Cancel == dr)
//                return;

//            WebBrowserForms.UpdateSellMemoWebBrowserForm usmbf = new UpdateSellMemoWebBrowserForm(_orders[0], usmf.AppendMemo, true);
//            usmbf.ShowDialog(this.FindForm());
//        }

//        private void btnStockout_Click(object sender, EventArgs e)
//        {
//            List<SoldProductInfo> currentSoldProductInfos = new List<SoldProductInfo>();
//            foreach (Control c in pnlProductList.Controls)
//            {
//                if (!(c is SoldProductInfoControl))
//                    continue;
//                SoldProductInfoControl spic = c as SoldProductInfoControl;
//                if (spic.Count == 0)
//                    continue;
//                if (spic.SelectedProductInfo.Id.Equals("0"))
//                    continue;
//                SoldProductInfo spi = new SoldProductInfo(spic.SelectedProductInfo);
//                spi.Count = spic.Count;
//                currentSoldProductInfos.Add(spi);
//            }

//            OrderLib.ShipmentCompanies ec = OrderLib.ShipmentCompanies.Zto;
//            if (rdoSf.Checked)
//                ec = OrderLib.ShipmentCompanies.Sf;
//            else if (rdoZto.Checked)
//                ec = OrderLib.ShipmentCompanies.Zto;
//            else if (rdoBest.Checked)
//                ec = OrderLib.ShipmentCompanies.Best;

//            StockActionAdvForm saaf = new StockActionAdvForm(true, currentSoldProductInfos);
//            saaf.FromToPart1 = (ShopProfile.Current.Shop == ShopEnum.Egode ? string.Empty : ShopProfile.Current.ShortName);
//            saaf.FromToPart2 = string.Format("{0} [{1},{2}]", txtBuyerAccount.Text, txtRecipientName.Text, txtMobile.Text);
//            saaf.Comment = ec.ToString().ToLower() + txtBillNumber.Text.Trim();
//            if (DialogResult.OK == saaf.ShowDialog(this))
//            {
//                // 出库登记.
//                try
//                {
//                    Application.DoEvents();
//                    Cursor.Current = Cursors.WaitCursor;
					
//                    string result = StockActionAdvForm.StockAction(true, saaf.SelectedProductInfos, saaf.FromToFull, saaf.Comment, ShippingOrigins.Shanghai);
//                    MessageBox.Show(
//                        this,
//                        "Result from server: \n" + result,
//                        this.Text,
//                        MessageBoxButtons.OK, MessageBoxIcon.Information);

//                    btnStockout.Text = string.Format("手动出库({0})", ++_manualStockoutCount);
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                }
//                finally
//                {
//                    Cursor.Current = Cursors.Default;
//                }
//            }
//        }

//        private void txtBillNumber_TextChanged(object sender, EventArgs e)
//        {
//            // Added by KK on 2015/11/08.
//            // Save last yto number.
//            if (rdoYto.Checked && txtBillNumber.Text.Length == 12)
//                Settings.Instance.RecentYtoBillNumber = txtBillNumber.Text;
//            if (rdoZto.Checked && txtBillNumber.Text.Length == 12)
//                Settings.Instance.RecentZtoBillNumber = txtBillNumber.Text;
//            if (rdoBest.Checked && txtBillNumber.Text.Length == 12)
//                Settings.Instance.RecentBestBillNumber = txtBillNumber.Text;
//        }

//        private void btnRegDistributor_Click(object sender, EventArgs e)
//        {
//            Cursor.Current = Cursors.WaitCursor;
			
//            Distributor d = Distributor.Match(txtBuyerAccount.Text);
//            if (null != d)
//            {
//                string warning = string.Format(
//                    "此ID已登记代发信息, 信息如下: \n\nID: {0}\n第1行信息(通常是发件人): {1}\n第2行信息(通常是链接等广告性质文字): {2}\n第3行信息(通常是电话号码): {3}", 
//                    d.Id, d.Name, d.Ad, d.Tel);
//                MessageBox.Show(this, warning, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                return;
//            }
			
//            if (string.IsNullOrEmpty(txtSenderTel.Text.Trim()))
//            {
//                MessageBox.Show(this, "发件人电话号码不能为空", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                return;
//            }
			
//            if (string.IsNullOrEmpty(txtSenderName.Text) || string.IsNullOrEmpty(txtSenderAd.Text))
//            {
//                if (DialogResult.No == MessageBox.Show(this, "发件人信息第1行或第2行信息为空(非必填项), 是否继续?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
//                    return;
//            }

//            string s = string.Format(
//                "登记代发信息如下: \n\nID: {0}\n第1行信息(通常是发件人): {1}\n第2行信息(通常是链接等广告性质文字): {2}\n第3行信息(通常是电话号码): {3}\n\n确认信息无误请点击Yes提交.", 
//                txtBuyerAccount.Text, txtSenderName.Text, txtSenderAd.Text, txtSenderTel.Text);
			
//            DialogResult dr = MessageBox.Show(this, s, this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
//            if (DialogResult.Yes == dr)
//            {
//                string url = string.Format(Common.URL_DATA_CENTER, "regdistributor");
//                url += string.Format("&id={0}&name={1}&ad={2}&tel={3}", txtBuyerAccount.Text, txtSenderName.Text, txtSenderAd.Text, txtSenderTel.Text);
//                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
//                request.Method = "GET";
//                request.ContentType = "text/xml";
//                WebResponse response = request.GetResponse();
//                StreamReader reader = new StreamReader(response.GetResponseStream());
//                string result = reader.ReadToEnd();
//                reader.Close();
//                //Trace.WriteLine(result);

//                MessageBox.Show(
//                    this,
//                    "Result from server: \n" + result,
//                    this.Text,
//                    MessageBoxButtons.OK, MessageBoxIcon.Information);
				
//                if (result.Equals("ok"))
//                    Distributor.Distributors.Add(new Distributor(txtBuyerAccount.Text, txtSenderName.Text, txtSenderAd.Text, txtSenderTel.Text));
//            }

//            Cursor.Current = Cursors.Default;
//        }

//        //protected override void OnPaint(PaintEventArgs e)
//        //{
//        //    base.OnPaint(e);
			
//        //    int y = btnPrint.Bottom + (txtBillNumber.Top - btnPrint.Bottom) / 2;
			
//        //    e.Graphics.DrawLine(new Pen(Color.FromArgb(192,192,192)), new Point(6, y), new Point(this.ClientRectangle.Right - 6, y));
//        //    e.Graphics.DrawLine(new Pen(Color.White), new Point(6, y+1), new Point(this.ClientRectangle.Right - 6, y+1));
//        //}

//        void spic_OnCountChanged(object sender, EventArgs e)
//        {
//            Cursor.Current = Cursors.WaitCursor;
//            RefreshProductText();
//            _productInfoChanged = true;
//            Cursor.Current = Cursors.Default;
//        }

//        void spic_OnProductChanged(object sender, EventArgs e)
//        {
//            Cursor.Current = Cursors.WaitCursor;
			
//            SoldProductInfoControl spicSender = sender as SoldProductInfoControl;
//            foreach (Control c in pnlProductList.Controls)
//            {
//                if (!(c is SoldProductInfoControl))
//                    continue;
//                if (c.Equals(sender))
//                    continue;
//                SoldProductInfoControl spic = c as SoldProductInfoControl;
//                if (spic.SelectedProductInfo.Id.Equals(spicSender.SelectedProductInfo.Id))
//                {
//                    MessageBox.Show(this, "此商品已存在于列表中, 请不要重复选择.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                    spicSender.SetSelectedProduct("0");
//                    return;
//                }
//            }
						
//            RefreshProductText();
//            _productInfoChanged = true;
//            Cursor.Current = Cursors.Default;
//        }

//        void spic_OnRemove(object sender, EventArgs e)
//        {
//            DialogResult dr = MessageBox.Show(this, "确定要删除吗?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
//            if (DialogResult.No == dr)
//                return;
			
//            Cursor.Current = Cursors.WaitCursor;
		
//            pnlProductList.Controls.Remove(sender as SoldProductInfoControl);
//            RefreshProductText();
//            _productInfoChanged = true;

//            LayoutControls();
						
//            Cursor.Current = Cursors.Default;
//        }
		
//        private void tsbtnAddProduct_Click(object sender, EventArgs e)
//        {
//            SoldProductInfoControl spic = new SoldProductInfoControl(null, false);
//            spic.OnRemove += new EventHandler(spic_OnRemove);
//            spic.OnProductChanged += new EventHandler(spic_OnProductChanged);
//            spic.OnCountChanged += new EventHandler(spic_OnCountChanged);
//            pnlProductList.Controls.Add(spic);
//            pnlProductList.Controls.SetChildIndex(spic, pnlProductList.Controls.IndexOf(tsAddProduct));
//            spic.Margin = new Padding(3, 2, 3, 0);
//            spic.Width = pnlProductList.Width - tsAddProduct.Margin.Right - spic.Margin.Left - 7;
			
//            RefreshProductText();
//            _productInfoChanged = true;

//            LayoutControls();
//        }

//        void RefreshProductText()
//        {
//            txtProducts.Text = string.Empty;

//            foreach (Control c in pnlProductList.Controls)
//            {
//                if (!(c is SoldProductInfoControl))
//                    continue;
//                SoldProductInfoControl spic = c as SoldProductInfoControl;
//                if (0 == spic.Count)
//                    continue;
//                if (null == spic.SelectedProductInfo)
//                    continue;
//                if (spic.SelectedProductInfo.Id.Equals("0"))
//                    continue;
//                string productName = string.IsNullOrEmpty(spic.SelectedProductInfo.ShortName)?spic.SelectedProductInfo.Name: spic.SelectedProductInfo.ShortName;
//                txtProducts.Text += string.Format("{0} x {1}\r\n", productName, spic.Count);
//            }
//        }
		
//        void LayoutControls()
//        {
//            txtRemark.Top = txtFullAddress.Bottom + 6;
//            lblRemark.Top = txtRemark.Top + 2;
//            pnlPrint.Top = txtRemark.Bottom + 6;
//            pnlProductList.Height = Math.Max(115, tsAddProduct.Bottom);
//            txtProducts.Height = pnlProductList.Height;
//            pnlPrint.Height = chkHoliday.Bottom + 2;
//            lblPrint.Top = pnlPrint.Top + 2;
//            pnlConsign.Top = pnlPrint.Bottom + 6;
//            lblBillNumber.Top = pnlConsign.Top + 2;
//            this.ClientSize = new Size(this.ClientSize.Width, pnlConsign.Bottom + 6);
//            pnlPrint.Refresh();
//        }

//        private void txtBillNumber_Enter(object sender, EventArgs e)
//        {
//            txtBillNumber.SelectionStart = txtBillNumber.Text.Length + 1;
//        }

//        private void cboDistributors_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            if (null == cboDistributors.SelectedItem)
//                return;
//            Distributor d = cboDistributors.SelectedItem as Distributor;
//            txtSenderName.Text = d.Name;
//            txtSenderAd.Text = d.Ad;
//            txtSenderTel.Text = d.Tel;
//        }

//        private void btnSelectAddress_Click(object sender, EventArgs e)
//        {
//            Cursor.Current = Cursors.WaitCursor;
			
//            AddressSelectorForm asf = new AddressSelectorForm();
//            if (DialogResult.OK == asf.ShowDialog(this))
//            {
//                if (null != asf.SelectedAddress)
//                {
//                    txtRecipientName.Text = asf.SelectedAddress.Recipient;
//                    txtBuyerAccount.Text = asf.SelectedAddress.Id;
//                    txtProvince.Text = asf.SelectedAddress.Province;
//                    txtCity1.Text = asf.SelectedAddress.City1;
//                    txtCity2.Text = asf.SelectedAddress.City2;
//                    txtDistrict.Text = asf.SelectedAddress.District;
//                    txtStreetAddress.Text = asf.SelectedAddress.StreetAddress;
//                    txtMobile.Text = asf.SelectedAddress.Mobile;
//                    txtPhone.Text = asf.SelectedAddress.Phone;
//                }
//            }
			
//            Cursor.Current = Cursors.Default;
//        }
//    }
//}