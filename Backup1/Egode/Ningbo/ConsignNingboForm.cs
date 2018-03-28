using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Resources;
using Egode.WebBrowserForms;
using OrderLib;

namespace Egode
{
	public partial class ConsignNingboForm : Form
	{
		public event EventHandler OnNingboOrderAdded;
	
		private List<Order> _orders;
		List<SoldProductInfo> _soldProductInfos;
		private bool _productDescChanged;
		private bool _productInfoChanged;
		//private NingboOrder _ningboOrder;
		private bool _isRefundingOrder; // Added by KK on 2016/01/03.
	
		public ConsignNingboForm(List<Order> orders, bool isRefundingOrder)
		{
			_orders = orders;
			_isRefundingOrder = isRefundingOrder;
			InitializeComponent();
		}
		
		//public NingboOrder NingboOrder
		//{
		//    get { return _ningboOrder; }
		//}

		private void ConsignShForm_Load(object sender, EventArgs e)
		{
			if (_orders.Count > 1)
				this.Text += string.Format(" [{0}个订单合并发货]", _orders.Count);

			StringBuilder sbRemark = new StringBuilder();
			foreach (Order o in _orders)
			{
				if (string.IsNullOrEmpty(o.Remark))
					continue;

				if (_orders.Count > 1)
					sbRemark.Append(string.Format("#{0}: ", _orders.IndexOf(o) + 1));
				sbRemark.Append(o.Remark);
				if (_orders.IndexOf(o) < _orders.Count - 1)
					sbRemark.Append("\r\n");
			}

			StringBuilder sbBuyerRemark = new StringBuilder();
			foreach (Order o in _orders)
			{
				if (string.IsNullOrEmpty(o.BuyerRemark))
					continue;
			
				if (_orders.Count > 1)
					sbBuyerRemark.Append(string.Format("#{0}: ", _orders.IndexOf(o) + 1));
				sbBuyerRemark.Append(o.BuyerRemark);
				if (_orders.IndexOf(o) < _orders.Count - 1)
					sbBuyerRemark.Append("\r\n");
			}

			txtRemark.Text = sbRemark.ToString().Trim();
			txtBuyerRemark.Text = sbBuyerRemark.ToString().Trim();
			
			SizeF size = Graphics.FromHwnd(this.Handle).MeasureString(txtRemark.Text, this.Font, txtRemark.Width - 6);
			txtRemark.Height = size.ToSize().Height + 6;
			
			// address info.
			if (string.IsNullOrEmpty(_orders[0].EditedRecipientAddress))
			{
				AddressParser ap = new AddressParser(_orders[0].RecipientAddress);
				txtRecipientName.Text = _orders[0].RecipientName.Trim();
				txtProvince.Text = ap.Province.Trim();
				txtCity1.Text = ap.City1.Trim();
				txtCity2.Text = ap.City2.Trim();
				txtDistrict.Text = ap.District.Trim();
				txtStreetAddress.Text = ap.StreetAddress.Trim();
				txtMobile.Text = _orders[0].MobileNumber.Trim();
			}
			else
			{
				txtRecipientName.BackColor= Color.FromArgb(255, 200, 200);
				txtProvince.BackColor= Color.FromArgb(255, 200, 200);
				txtCity1.BackColor= Color.FromArgb(255, 200, 200);
				txtCity2.BackColor= Color.FromArgb(255, 200, 200);
				txtDistrict.BackColor= Color.FromArgb(255, 200, 200);
				txtStreetAddress.BackColor= Color.FromArgb(255, 200, 200);
				txtMobile.BackColor= Color.FromArgb(255, 200, 200);

				ParseEditedAddress();
				//MessageBox.Show(this, "需要手动输入地址!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			
			if (string.IsNullOrEmpty(txtCity1.Text))
			{
				if (txtProvince.Text.StartsWith("北京") || txtProvince.Text.StartsWith("上海") || txtProvince.Text.StartsWith("天津") || txtProvince.Text.StartsWith("重庆"))
					txtCity1.Text = txtProvince.Text;
			}

			// Product info.
			if (!_isRefundingOrder)
			{
				int cMustSendFromDe = 0, cBonded = 0;
				_soldProductInfos = GetProducts(_orders, out cMustSendFromDe, out cBonded);
				foreach (SoldProductInfo spi in _soldProductInfos)
					txtProducts.Text += string.Format("{0} x {1}{2}\r\n", spi.ShortName, spi.Count, string.IsNullOrEmpty(spi.Comment) ? string.Empty : (" " + spi.Comment));
				if (txtProducts.Text.Length > 2 && txtProducts.Text.EndsWith("\r\n"))
					txtProducts.Text = txtProducts.Text.Remove(txtProducts.Text.Length - 2, 2); // remove return at the end of string.

				foreach (SoldProductInfo spi in _soldProductInfos)
				{
					SoldProductInfoControl spic = new SoldProductInfoControl(spi, true);
					spic.OnRemove += new EventHandler(spic_OnRemove);
					spic.OnProductChanged += new EventHandler(spic_OnProductChanged);
					spic.OnCountChanged += new EventHandler(spic_OnCountChanged);
					pnlProductList.Controls.Add(spic);
					pnlProductList.Controls.SetChildIndex(spic, pnlProductList.Controls.IndexOf(tsAddProduct));
					spic.Margin = new Padding(3, 2, 3, 0);
				}
			}
			pnlProductList.Height = Math.Max(115, tsAddProduct.Bottom);
			txtProducts.Height = pnlProductList.Height;
			txtProducts.TextChanged += new EventHandler(txtProducts_TextChanged);
			//
			
			float money = 0;
			foreach (Order o in _orders)
				money += o.TotalMoney;
			
			lblMoney.Text = money.ToString("0.00");

			cboLogistics.Items.Add("邮政国内小包");
			cboLogistics.Items.Add("EMS");
			cboLogistics.Items.Add("中通");
			cboLogistics.SelectedIndex = 0;

			LayoutControls();
		}

		private void ParseEditedAddress()
		{
			try
			{
				string[] infos = _orders[0].EditedRecipientAddress.Replace("，", ",").Split(',');
				if (null == infos || infos.Length <= 0)
					return;
				
				int i = 0;
				
				// first segment should be recipient name.
				txtRecipientName.Text = infos[i++].Trim();
				
				// second segment should be mobile number.
				txtMobile.Text = infos[i++].Trim();
				
				if (infos.Length >= 5)
					txtMobile.Text += "," + infos[i++].Trim();
				
				AddressParser ap = new AddressParser(infos[i++].Trim());
				txtProvince.Text = ap.Province;
				txtCity1.Text = ap.City1;
				txtCity2.Text = ap.City2;
				txtDistrict.Text = ap.District;
				txtStreetAddress.Text = ap.StreetAddress;
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					this, 
					string.Format("解析新地址出错! 火速联系KK~\n{0}\n{1}", _orders[0].EditedRecipientAddress, ex.ToString()), 
					this.Text, 
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		void txtProducts_TextChanged(object sender, EventArgs e)
		{
			_productDescChanged = true;
		}

		private void ConsignShForm_Shown(object sender, EventArgs e)
		{
			// Removed by KK on 2015/08/12.
			// 直邮、现货、保税区链接混用.
			//// Added by KK on 2015/03/30.
			//int cMustSendFromDe = 0, cBonded = 0;
			//GetProducts(_orders, out cMustSendFromDe, out cBonded);
			//if (cMustSendFromDe > 0)
			//{
			//    MessageBox.Show(
			//        this, 
			//        string.Format("发现{0}个必须直邮商品链接!!!\n已从发货清单中删除.",cMustSendFromDe), this.Text, 
			//        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//}

			//if (cBonded <= 0)
			//{
			//    MessageBox.Show(
			//        this,
			//        "此订单没有待发货的包含保税区商品!!!", this.Text,
			//        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//}

			if (_isRefundingOrder)
				MessageBox.Show(
					this,
					"此订单有退款信息!\n并且存在待处理的商品!!\n为安全起见, 不自动识别商品, 需手动添加商品!!!", this.Text,
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

			if (txtProducts.Text.Trim().Length < 5)
				MessageBox.Show(this, "商品描述未正确识别, 需要手动输入.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

			// ID number.
			Regex r = new Regex(@"\d{17}[\d|x|X]");
			Match m;
			List<string> idNumbers = new List<string>();
			foreach (Order o in _orders)
			{
				m = r.Match(o.BuyerRemark + "||" + o.Remark);
				if (m.Success)
				{
					while (null != m && !string.IsNullOrEmpty(m.Value))
					{
						if (!idNumbers.Contains(m.Value))
							idNumbers.Add(m.Value);
						m = m.NextMatch();
					}
				}
			}
			if (idNumbers.Count > 1)
				MessageBox.Show(this, "订单信息中发现多个身份证号码, 自动填入第1个身份证号码.\n请仔细核对身份证号码, 必要时手动修改.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			if (idNumbers.Count > 0)
				txtIdInfo.Text = string.Format("{0},{1}", txtRecipientName.Text, idNumbers[0]);

			this.Activate();
		}

		private bool RemarkContainsPost()
		{
			foreach (Order o in _orders)
			{
				if (o.Remark.Contains("邮政国内小包"))
					return true;
				if (o.Remark.Contains("邮政"))
					return true;
				if (o.BuyerRemark.Contains("小包"))
					return true;
			}
			return false;
		}

		private bool RemarkContainsZto()
		{
			foreach (Order o in _orders)
			{
				if (o.Remark.Contains("中通"))
					return true;
				if (o.BuyerRemark.Contains("中通"))
					return true;
			}
			return false;
		}

		private bool RemarkContainsEms()
		{
			foreach (Order o in _orders)
			{
				if (o.Remark.ToLower().Contains("ems"))
					return true;
				if (o.BuyerRemark.ToLower().Contains("ems"))
					return true;
			}
			return false;
		}

		private int GetProductCount()
		{
			int c = 0;
			if (null != _soldProductInfos && _soldProductInfos.Count > 0)
			{
				foreach (SoldProductInfo spi in _soldProductInfos)
				{
					if (!IsAptamilHippMilk(spi.Id))
						continue;
					c += spi.Count;
				}
			}
			
			return c;
		}

		private bool IsAptamilHippMilk(string productId)
		{
			if (string.IsNullOrEmpty(productId))
				return false;
			if (productId.Equals("001-0001") || productId.Equals("001-0002") || productId.Equals("001-0003") || productId.Equals("001-0004") || productId.Equals("001-0005") || productId.Equals("001-0006"))
				return true;
			if (productId.Equals("001-0012") || productId.Equals("001-0013") || productId.Equals("001-0014") || productId.Equals("001-0015"))
				return true;
			if (productId.Equals("001-0016") || productId.Equals("001-0017") || productId.Equals("001-0018"))
				return true;

			if (productId.Equals("002-0001") || productId.Equals("002-0002") || productId.Equals("002-0003") || productId.Equals("002-0004") || productId.Equals("002-0005"))
				return true;
			if (productId.Equals("002-0006") || productId.Equals("002-0007") || productId.Equals("002-0008") || productId.Equals("002-0009") || productId.Equals("002-0010") || productId.Equals("002-0011"))
				return true;
			
			return false;
		}

		//private bool ContainsBondedProducts(List<Order> orders)
		//{
		//    if (null == orders || orders.Count <= 0)
		//        return false;
			
		//    foreach (Order o in orders)
		//    {
		//        if (o.Items.Contains("保税区"))
		//            return true;
		//    }
		//    return false;
		//}

		// 从多个订单中提取产品信息, 相同产品合并计数.
		private List<SoldProductInfo> GetProducts(List<Order> orders, out int cMustSendFromDe, out int cBonded)
		{
			cMustSendFromDe = 0;
			cBonded = 0;
			SortedList<string, SoldProductInfo> sortedProducts = new SortedList<string, SoldProductInfo>();

			foreach (Order o in orders)
			{
				string allItems = o.Items;
				string[] items = allItems.Split('★');
				for (int i = 0; i < items.Length; i++)
				{
					string item = items[i];
					string[] infos = item.Split('☆');
					if (infos.Length < 3)
						continue;

					if (string.IsNullOrEmpty(infos[0]))
						Trace.WriteLine("null product found!!!");

					string productTitle = infos[0];
					int count = int.Parse(infos[2]);

					ProductInfo pi = ProductInfo.Match(productTitle, o.Remark);
					if (null == pi)
						continue;
						
					for (int c = 16; c > 0; c--)
					{
						if (productTitle.Contains(string.Format("{0}盒装", c)) || productTitle.Contains(string.Format("{0}罐装", c)))
						{
							count *= c;
							break;
						}
					}

					//// 双11链接, 1+12盒链接, 1+4盒链接, 2+12盒链接, 2+4盒链接, 3段4罐链接
					//if (productTitle.Contains("12盒包邮包税") || productTitle.Contains("直邮12盒")) 
					//    count *= 12;
					//if (productTitle.Contains("直邮9罐"))
					//    count *= 9;
					//if (productTitle.Contains("现货4盒"))
					//    count *= 4;
					//if (productTitle.Contains("现货4罐"))
					//    count *= 4;
					//if (productTitle.Contains("3罐"))
					//    count *= 3;
					//if (pi.Id.Equals("001-0005") && productTitle.Contains("12盒包邮包税"))
					//    count *= 12;
					//if (pi.Id.Equals("001-0005") && productTitle.Contains("4盒"))
					//    count *= 4;
					//if (pi.Id.Equals("001-0006") && productTitle.Contains("12盒包邮包税"))
					//    count *= 12;
					//if (pi.Id.Equals("001-0006") && productTitle.Contains("4盒"))
					//    count *= 4;
					//if (pi.Id.Equals("001-0004") && productTitle.Contains("4罐"))
					//    count *= 4;

					Order.OrderStatus status = (Order.OrderStatus)Enum.Parse(typeof(Order.OrderStatus), infos[3]);
					bool succeeded = false;
					bool cancelled = false;
					bool sent = false;
					if (infos.Length >= 4)
					{
						succeeded = (status == Order.OrderStatus.Succeeded);
						cancelled = (status == Order.OrderStatus.Closed);
						sent = (status == Order.OrderStatus.Sent);
					}
					if (succeeded || cancelled || sent)
						continue;
					
					string code = string.Empty;
					if (infos.Length >= 5)
						code = infos[4];
					if (code.ToLower().StartsWith("d-"))
					{
						cMustSendFromDe++;
						continue;
					}
					// Removed by KK on 2015/09/18.
					// 保税区不再设单独链接, 可以/可能跟其他链接共用.
					// 同时, cBonded不再准确.
					//if (!code.ToLower().StartsWith("b-") && !productTitle.Contains("保税区"))
					//    continue;
					cBonded++;
					
					bool bingo = false;
					foreach (SoldProductInfo spi in sortedProducts.Values)
					{
						if (spi.Id.Equals(pi.Id))
						{
							bingo = true;
							spi.AddCount(count);
							break;
						}
					}

					if (!bingo)
						sortedProducts.Add(pi.Id, new SoldProductInfo(pi.Id, pi.BrandId, pi.SkuCode, pi.NingboId, pi.DangdangCode, pi.Name, pi.Price, pi.Specification, pi.ShortName, pi.Keywords, pi.Conflict, count, status, code));
						//products.Add(new SoldProductInfo(pi.Id, pi.BrandId, pi.Name, pi.Price, pi.Specification, pi.ShortName, pi.Keywords, count, status));
				}
			}
			
			List<SoldProductInfo> products = new List<SoldProductInfo>();
			foreach (SoldProductInfo p in sortedProducts.Values)
				products.Add(p);
			return products;
		}
		
		private string GetWeight(List<SoldProductInfo> soldProductInfos)
		{
			if (null == soldProductInfos || soldProductInfos.Count <= 0)
				return string.Empty;
			if (soldProductInfos.Count > 1)
				return string.Empty;
			
			if (soldProductInfos[0].Id == "001-0001" || soldProductInfos[0].Id == "001-0002" || soldProductInfos[0].Id == "001-0003" || soldProductInfos[0].Id == "001-0004")
			{
				if (soldProductInfos[0].Count == 1)
					return "1.18";
				if (soldProductInfos[0].Count == 2)
					return "2.23";
				if (soldProductInfos[0].Count == 4)
					return "4.52";
			}

			if (soldProductInfos[0].Id == "001-0005" || soldProductInfos[0].Id == "001-0006")
			{
				if (soldProductInfos[0].Count == 1)
					return "0.85";
				if (soldProductInfos[0].Count == 2)
					return "1.71";
				if (soldProductInfos[0].Count == 3)
					return "2.40";
				if (soldProductInfos[0].Count == 4)
					return "3.26";
				if (soldProductInfos[0].Count == 5)
					return "4.31";
				if (soldProductInfos[0].Count == 6)
					return "4.91";
			}
			
			return string.Empty;
		}

		private void pnlProductList_Paint(object sender, PaintEventArgs e)
		{
			ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, pnlProductList.Width, pnlProductList.Height), Color.LightGray, ButtonBorderStyle.Solid);
		}

		private void pnlPrint_Paint(object sender, PaintEventArgs e)
		{
			ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, pnlPrint.Width, pnlPrint.Height), Color.LightGray, ButtonBorderStyle.Solid);
		}

		private void pnlAddress_Paint(object sender, PaintEventArgs e)
		{
			ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, pnlAddress.Width, pnlAddress.Height), Color.LightGray, ButtonBorderStyle.Solid);
		}

		private void pnlMoney_Paint(object sender, PaintEventArgs e)
		{
			float money = 0;
			foreach (Order o in _orders)
				money += o.TotalMoney;
			
			//e.Graphics.DrawString(money.ToString("0.00"), pnlMoney.Font, new SolidBrush(pnlMoney.ForeColor), new PointF(0, 0));
			SizeF s = e.Graphics.MeasureString(money.ToString("0.00"), pnlMoney.Font);
			e.Graphics.TranslateTransform(s.Height, 0);
			e.Graphics.RotateTransform(90.0f);
			e.Graphics.DrawString(money.ToString("0.00"), pnlMoney.Font, new SolidBrush(pnlMoney.ForeColor), new PointF(0,0));
		}

		private void OnProductsTextBoxLocationSizeChanged(object sender, EventArgs e)
		{
			Point p = new Point(txtProducts.Right, txtProducts.Top+1);
			p.Offset(pnlPrint.Location);
			p.Offset(-lblMoney.Width, 0);
			lblMoney.Location = p;

			pnlMoney.Location = new Point(pnlPrint.Left + 15, pnlPrint.Top + 190);
		}

		private void btnAppenRemark_Click(object sender, EventArgs e)
		{
			Utility.InputBoxForm inputbox = new Egode.Utility.InputBoxForm();
			if (DialogResult.OK == inputbox.ShowDialog(this))
			{
				UpdateSellMemoWebBrowserForm usmf = new UpdateSellMemoWebBrowserForm(_orders[0], inputbox.Message, true);
				usmf.ShowDialog(this);
			}
		}

		void spic_OnCountChanged(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			RefreshProductText();
			_productInfoChanged = true;
			Cursor.Current = Cursors.Default;
		}

		void spic_OnProductChanged(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;

			SoldProductInfoControl spicSender = sender as SoldProductInfoControl;
			foreach (Control c in pnlProductList.Controls)
			{
				if (!(c is SoldProductInfoControl))
					continue;
				if (c.Equals(sender))
					continue;
				SoldProductInfoControl spic = c as SoldProductInfoControl;
				if (spic.SelectedProductInfo.Id.Equals(spicSender.SelectedProductInfo.Id))
				{
					MessageBox.Show(this, "此商品已存在于列表中, 请不要重复选择.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					spicSender.SetSelectedProduct("0");
					return;
				}
			}

			RefreshProductText();
			_productInfoChanged = true;
			Cursor.Current = Cursors.Default;
		}

		void spic_OnRemove(object sender, EventArgs e)
		{
			DialogResult dr = MessageBox.Show(this, "确定要删除吗?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (DialogResult.No == dr)
				return;

			Cursor.Current = Cursors.WaitCursor;

			pnlProductList.Controls.Remove(sender as SoldProductInfoControl);
			RefreshProductText();
			_productInfoChanged = true;

			LayoutControls();

			Cursor.Current = Cursors.Default;
		}

		private void tsbtnAddProduct_Click(object sender, EventArgs e)
		{
			SoldProductInfoControl spic = new SoldProductInfoControl(null, true);
			spic.OnRemove += new EventHandler(spic_OnRemove);
			spic.OnProductChanged += new EventHandler(spic_OnProductChanged);
			spic.OnCountChanged += new EventHandler(spic_OnCountChanged);
			pnlProductList.Controls.Add(spic);
			pnlProductList.Controls.SetChildIndex(spic, pnlProductList.Controls.IndexOf(tsAddProduct));
			spic.Margin = new Padding(3, 2, 3, 0);

			RefreshProductText();
			_productInfoChanged = true;

			LayoutControls();
		}

		void RefreshProductText()
		{
			txtProducts.Text = string.Empty;

			foreach (Control c in pnlProductList.Controls)
			{
				if (!(c is SoldProductInfoControl))
					continue;
				SoldProductInfoControl spic = c as SoldProductInfoControl;
				if (0 == spic.Count)
					continue;
				if (spic.SelectedProductInfo.Id.Equals("0"))
					continue;
				txtProducts.Text += string.Format("{0} x {1}\r\n", spic.SelectedProductInfo.ShortName, spic.Count);
			}
		}
		
		void LayoutControls()
		{
			txtRemark.Top = txtBuyerRemark.Bottom + 6;
			lblRemark.Top = txtRemark.Top + 2;
			pnlPrint.Top = txtRemark.Bottom + 6;
			pnlProductList.Height = Math.Max(115, tsAddProduct.Bottom);
			txtProducts.Height = pnlProductList.Height;
			pnlPrint.Height = lblWarning.Bottom + 6;
			lblPrint.Top = pnlPrint.Top + 2;
			cboLogistics.Top = pnlPrint.Bottom + 6;
			lblLogistics.Top = cboLogistics.Top + 2;
			btnAdd.Top = cboLogistics.Bottom + 6;
			btnNingboList.Top = btnAdd.Top;
			btnCancel.Top = btnAdd.Top;
			this.ClientSize = new Size(this.ClientSize.Width, btnAdd.Bottom + 6);
			pnlPrint.Refresh();
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
		
			try
			{
				if (string.IsNullOrEmpty(txtRecipientName.Text.Trim()))
				{
					MessageBox.Show(this, "收件人姓名不能为空.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (string.IsNullOrEmpty(txtMobile.Text.Trim()))
				{
					MessageBox.Show(this, "收件人电话能为空.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (string.IsNullOrEmpty(txtIdInfo.Text.Trim()))
				{
					MessageBox.Show(this, "身份证信息不能为空.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				Regex r = new Regex(@"\d{17}[\d|x|X]");
				if (!r.Match(txtIdInfo.Text).Success)
				{
					MessageBox.Show(this, "身份证信息格式不正确.\n身份证信息格式为: 张三,000000000000000000", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (!txtIdInfo.Text.Contains(",") && !txtIdInfo.Text.Contains("，"))
				{
					MessageBox.Show(this, "身份证信息格式不正确.\n身份证信息格式为: 张三,000000000000000000", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (txtIdInfo.Text.Length < 21) // 18位数字+逗号+至少2个字名字=21
				{
					MessageBox.Show(this, "身份证信息格式不正确.\n身份证信息格式为: 张三,000000000000000000", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (string.IsNullOrEmpty(txtProvince.Text.Trim()))
				{
					MessageBox.Show(this, "省份信息不能为空.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (string.IsNullOrEmpty(txtCity1.Text.Trim()))
				{
					MessageBox.Show(this, "城市信息不能为空.\n直辖市填入省份名即可.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (string.IsNullOrEmpty(txtCity2.Text.Trim()) && string.IsNullOrEmpty(txtDistrict.Text.Trim()))
				{
					MessageBox.Show(this, "区县信息不能为空.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (string.IsNullOrEmpty(txtStreetAddress.Text.Trim()))
				{
					MessageBox.Show(this, "详细地址不能为空.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (pnlProductList.Controls.Count <= 1)
				{
					MessageBox.Show(this, "产品信息不能为空.\n至少需要选择1个产品.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				
				List<SoldProductInfo> products = new List<SoldProductInfo>();
				foreach (Control c in pnlProductList.Controls)
				{
					if (!(c is SoldProductInfoControl))
						continue;
					SoldProductInfoControl spic = c as SoldProductInfoControl;
					SoldProductInfo spi = new SoldProductInfo(spic.SelectedProductInfo);
					spi.Count = spic.Count;
					products.Add(spi);
				}
					
				NingboOrder ningboOrder = new NingboOrder(
					cboLogistics.SelectedItem.ToString(), string.Empty, _orders[0].OrderId, 
					txtRecipientName.Text, txtMobile.Text, txtProvince.Text, txtCity1.Text, 
					string.IsNullOrEmpty(txtDistrict.Text)?txtCity2.Text:txtDistrict.Text, txtStreetAddress.Text, 
					products, txtIdInfo.Text, _orders[0].AlipayNumber);
				
				if (_orders.Count > 1)
				{
					string linkedOrderIds = string.Empty;
					for (int i = 1; i < _orders.Count; i++)
					{
						linkedOrderIds += _orders[i].OrderId;
						if (i < _orders.Count - 1)
							linkedOrderIds += ",";
					}
					ningboOrder.LinkedTaobaoOrderIds = linkedOrderIds;
				}

				NingboOrder.Orders.Add(ningboOrder);
				//this.DialogResult = DialogResult.OK;
				//this.Close();

				// motion effect.
				ResourceManager rm = new ResourceManager("Egode.Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());
				Image imgNb = (Image)rm.GetObject("nb1");
				Point p = MainForm.Instance.NbStatusLocation;
				p.Offset(2, 8);
				FlyingForm flying = new FlyingForm(imgNb, p);
				flying.FlyingCompleted += new EventHandler(flying_FlyingCompleted);
				flying.Show();
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		void flying_FlyingCompleted(object sender, EventArgs e)
		{
			foreach (Order o in _orders)
				o.LocalPrepareNingbo();
				
			if (null != this.OnNingboOrderAdded)
				this.OnNingboOrderAdded(this, EventArgs.Empty);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			//this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}