using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode.OuterOrder
{
	public partial class OuterOrderDetailsForm : Form
	{
		private Egode.OuterOrder.OuterOrdersForm.EgodeSimpleOrder _order;
		private bool _productInfoChanged;
		private bool _printed;
		private bool _stockedout;

		public OuterOrderDetailsForm(Egode.OuterOrder.OuterOrdersForm.EgodeSimpleOrder o)
		{
			_order = o;
			InitializeComponent();
		}
		
		public string BillNumber
		{
			get { return txtBillNumber.Text; }
		}

		private void OuterOrderDetailsForm_Load(object sender, EventArgs e)
		{
			txtTaobaoId.Text = _order.TaobaoId;
			txtMoney.Text = _order.Money;
			txtDemand.Text = _order.Demand;
			txtAddr.Text = _order.Addr;
			txtProduct.Text = _order.Product;
			txtCount.Text = _order.Count;
			txtBillNumber.Text = _order.BillNumber;
			
			if (_order.Express.Trim().Equals("顺丰"))
				rdoSf.Checked = true;
			else
				rdoYto.Checked = true;
			
			txtSenderName.Text = "德 国 e 购";
			txtSenderAd.Text = "http://eur8.taobao.com";
			txtSenderTel.Text = "18964913317";
		}

		private void tsbtnAddProduct_Click(object sender, EventArgs e)
		{
			SoldProductInfoControl spic = new SoldProductInfoControl(null, false);
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
			pnlProductList.Height = Math.Max(75, tsAddProduct.Bottom);
			txtProducts.Height = pnlProductList.Height;
			this.ClientSize = new Size(this.ClientSize.Width, pnlMain.Bottom + 36);
		}

		private void pnlProductList_Paint(object sender, PaintEventArgs e)
		{
			ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, pnlProductList.Width, pnlProductList.Height), Color.LightGray, ButtonBorderStyle.Solid);
		}

		private void pnlAddress_Paint(object sender, PaintEventArgs e)
		{
			ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, pnlAddress.Width, pnlAddress.Height), Color.FromArgb(0xff, 130, 116, 205), ButtonBorderStyle.Solid);
		}

		private void pnlConsign_Paint(object sender, PaintEventArgs e)
		{
			ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, pnlConsign.Width, pnlConsign.Height), Color.LightGray, ButtonBorderStyle.Solid);
		}

		private void pnlSf_Paint(object sender, PaintEventArgs e)
		{
			ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, pnlSf.Width, pnlSf.Height), Color.FromArgb(0xff, 0xa0, 0xa0, 0xa0), ButtonBorderStyle.Solid);
		}

		private void btnParseAddr_Click(object sender, EventArgs e)
		{
			AddressWithRecipientPhoneParser ap = null;
			try
			{
				ap = new AddressWithRecipientPhoneParser(txtAddr.Text);
			}
			catch
			{
				MessageBox.Show(this, "地址解析出错, 手动修正格式后再重新解析.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			txtRecipientName.Text = ap.Recipient;
			txtMobile.Text = ap.Mobile;
			txtPhone.Text = ap.Phone;
			txtProvince.Text = ap.ProvinceCityDistrictStreetAddr.Province;
			txtCity1.Text = ap.ProvinceCityDistrictStreetAddr.City1;
			txtCity2.Text = ap.ProvinceCityDistrictStreetAddr.City2;
			txtDistrict.Text = ap.ProvinceCityDistrictStreetAddr.District;
			txtStreetAddress.Text = ap.ProvinceCityDistrictStreetAddr.StreetAddress;
			
			btnPrint.Enabled = true;
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;

			if (!rdoYto.Checked && !rdoSf.Checked)
			{
				MessageBox.Show(this, "选择快递公司先.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			if (btnPrint.Text.Equals("Printed"))
			{
				DialogResult dr = MessageBox.Show(this, "已打印, 是否再次打印?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (DialogResult.No == dr)
					return;
			}
			
			if (string.IsNullOrEmpty(txtProducts.Text))
			{
				DialogResult dr = MessageBox.Show(this, "产品信息为空, 是否继续打印?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (DialogResult.No == dr)
					return;
			}

			//if (rdoSf.Checked)
			//    MessageBox.Show(this, "发顺丰!\n确认是否顺丰面单!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

			string printerDocumentName = string.Format("{0}({1})", txtRecipientName.Text, txtBuyerAccount.Text);
			BillPrinterBase printer;
			if (rdoYto.Checked) // yto
			{
				printer = new YtoPrinter(printerDocumentName);
				printer.PrinterName = Settings.Instance.YtoPrinter;
			}
			else // sf
			{
				string destCityCode = string.Empty;
				if (chkSfOldBill.Checked)
				{
					printer = new SfPrinter(printerDocumentName);
					printer.PrinterName = Settings.Instance.SfPrinter;

					destCityCode = CityCodes.GetCityCode(txtProvince.Text.Trim());
					if (string.IsNullOrEmpty(destCityCode))
						destCityCode = CityCodes.GetCityCode(txtCity1.Text.Trim());
					((SfPrinter)printer).DestCode = destCityCode;
				}
				else
				{
					SfOrder sforder = null;
					if (chkAutoSfBillNumber.Checked)
					{
						sforder = new SfOrder();
						sforder.Province = txtProvince.Text.Trim();
						sforder.City1 = txtCity1.Text.Trim();
						sforder.City2 = txtCity2.Text.Trim();
						sforder.District = txtDistrict.Text.Trim();
						sforder.StreetAddress = txtStreetAddress.Text.Trim();
						sforder.RecipientName = txtRecipientName.Text.Trim();
						sforder.Mobile = txtMobile.Text.Trim();
						sforder.Phone = txtPhone.Text.Trim();

						// random order if for sf service.
						string randomOrderId = new Random(Environment.TickCount).Next(100000000, 999999999).ToString();
						
						//  check arrivable.
						bool arrivable = sforder.Arrivable(randomOrderId);//_orders[0].OrderId);
						if (!string.IsNullOrEmpty(sforder.ErrorCode))
						{
							MessageBox.Show(
								this,
								string.Format("筛单时发生错误: 错误代码={0}, 错误信息={1}", sforder.ErrorCode, sforder.ErrorMessage),
								this.Text,
								MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}
						if (!arrivable)
						{
							DialogResult dr = MessageBox.Show(
								this,
								string.Format("此地址顺丰不到, 可能需要收件人自提.\n{0}\n\n是否继续发顺丰?",
									string.Format("{0} {1} {2} {3} {4}", txtProvince.Text.Trim(), txtCity1.Text.Trim(), txtCity2.Text.Trim(), txtDistrict.Text.Trim(), txtStreetAddress.Text.Trim())),
								this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
							if (DialogResult.No == dr)
								return;
						}

						sforder.RunOrderService(randomOrderId);//_orders[0].OrderId);
						if (!string.IsNullOrEmpty(sforder.ErrorCode))
						{
							MessageBox.Show(
								this,
								string.Format("获取顺丰单号时发生错误: 错误代码={0}, 错误信息={1}", sforder.ErrorCode, sforder.ErrorMessage),
								this.Text,
								MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}

						// Got mail number but no dest code. Address may be invalid.
						if (string.IsNullOrEmpty(sforder.DestCode))
						{
							MessageBox.Show(
								this,
								string.Format("未获得目标地区代码(DestCode).\n请检查是否地址有误并重新打印."),
								this.Text,
								MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}

						txtBillNumber.Text = sforder.MailNumber;
						txtDestCode.Text = sforder.DestCode;
					}
					else
					{
						if (txtBillNumber.Text.Length < 12)
						{
							MessageBox.Show(
								this,
								"请手动输入正确的顺丰单号.",
								this.Text,
								MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							txtBillNumber.Focus();
							return;
						}
					}

					printer = new SfBillNewPrinter(txtBillNumber.Text, printerDocumentName);
					printer.PrinterName = Settings.Instance.SfNewPrinter;
					((SfBillNewPrinter)printer).DestCode = txtDestCode.Text;

					// Calculate total count to display on bill.
					((SfBillNewPrinter)printer).ItemAmount = GetProductTotalCount();
				}

				((SfPrinter)printer).IsFreightCollect = chkFriehghtCollect.Checked;
				((SfPrinter)printer).IsPickup = chkPickup.Checked;
			}

			string senderName = txtSenderName.Text;
			if (txtSenderName.Text.Equals("德 国 e 购") || txtSenderName.Text.Equals("buy欧洲"))
				senderName = string.IsNullOrEmpty(txtSenderName.Text) ? string.Empty : string.Format("{0}****", txtSenderName.Text.Substring(0, 1));
			string buyerAccount = string.IsNullOrEmpty(txtBuyerAccount.Text) ? string.Empty : string.Format("{0}***{1}", txtBuyerAccount.Text.Substring(0, 1), txtBuyerAccount.Text.Substring(txtBuyerAccount.Text.Length - 1, 1));

			printer.Font = new Font("Microsoft Yahei", 11);
			printer.SenderName = senderName; //txtSenderName.Text.Trim();
			printer.SenderAd = txtSenderAd.Text.Trim();
			printer.SenderTel = txtSenderTel.Text.Trim();
			printer.DisplayBuyerAccount = false;//(txtSenderName.Text.Trim().Equals(ShopProfile.Current.DisplayNameOnBill));
			printer.RecipientName = txtRecipientName.Text.Trim();
			printer.BuyerAccount = buyerAccount; //txtBuyerAccount.Text.Trim();
			printer.Province = txtProvince.Text;
			printer.City1 = txtCity1.Text;
			printer.City2 = txtCity2.Text;
			printer.District = txtDistrict.Text;
			printer.StreetAddress = txtStreetAddress.Text;
			printer.Mobile = txtMobile.Text.Trim();
			printer.Phone = txtPhone.Text.Trim();
			printer.HolidayDelivery = chkHoliday.Checked;
			printer.ProductInfos = txtProducts.Lines;
			printer.Print();
			_printed = true;

			#region obsoleted code on 2015/01/09
			//Size docSize = new Size(1000, 1000);
			//if (rdoYto.Checked)
			//    docSize = new Size(900, 550);
			//else if (rdoSf.Checked)
			//    docSize = new Size(780, 550);

			//PrintDocument pdoc = new PrintDocument();
			//pdoc.DocumentName = string.Format("{0}({1})", txtRecipientName.Text, txtBuyerAccount.Text);
			//pdoc.DefaultPageSettings.PaperSize = new PaperSize("custom", docSize.Width, docSize.Height);
			//pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
			//pdoc.Print();
			#endregion

			this.Focus();
			txtBillNumber.Focus();
			btnPrint.Text = "Printed";
			btnPrint.ForeColor = Color.OrangeRed;
			this.Activate();
			Cursor.Current = Cursors.Default;
			
			btnGo.Enabled = true;
		}
		
		private int GetProductTotalCount()
		{
			int count = 0;
			foreach (Control c in pnlProductList.Controls)
			{
				if (!(c is SoldProductInfoControl))
					continue;
				SoldProductInfoControl spic = c as SoldProductInfoControl;
				if (spic.Count == 0)
					continue;
				if (spic.SelectedProductInfo.Id.Equals("0"))
					continue;
				count += spic.Count;
			}
			
			return count;
		}

		private void btnGo_Click(object sender, EventArgs e)
		{
			// 出库登记.
			try
			{
				if (0 == GetProductTotalCount())
				{
					MessageBox.Show(this, "没有任何产品, 无需执行出库操作!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			
				Application.DoEvents();
				Cursor.Current = Cursors.WaitCursor;

				List<SoldProductInfo> soldProductInfos = new List<SoldProductInfo>();
				foreach (Control c in pnlProductList.Controls)
				{
					if (!(c is SoldProductInfoControl))
						continue;
					SoldProductInfoControl spic = c as SoldProductInfoControl;
					if (spic.Count == 0)
						continue;
					if (spic.SelectedProductInfo.Id.Equals("0"))
						continue;
					SoldProductInfo spi = new SoldProductInfo(spic.SelectedProductInfo);
					spi.Count = spic.Count;
					soldProductInfos.Add(spi);
				}

				string result = StockActionAdvForm.StockAction(
					true,
					soldProductInfos,
					string.Format("德国e购\\{0} [{1},{2}]", txtTaobaoId.Text, txtRecipientName.Text, txtMobile.Text),
					(rdoSf.Checked ? "sf" : "yto") + txtBillNumber.Text,
					OrderLib.ShippingOrigins.Shanghai);

				MessageBox.Show(
					this,
					"Result from server: \n" + result,
					this.Text,
					MessageBoxButtons.OK, MessageBoxIcon.Information);
				
				if (result.StartsWith("Succeeded"))
					btnClose.Enabled = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void txtBillNumber_TextChanged(object sender, EventArgs e)
		{
			btnGo.Enabled = (txtBillNumber.Text.Length == 12);
		}

		private void txtBillNumber_Enter(object sender, EventArgs e)
		{
			txtBillNumber.SelectionStart = txtBillNumber.Text.Length + 1;
		}
		
		private void txtBillNumber_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.KeyValue >= 0x30 && e.KeyValue <= 0x39) || e.KeyValue == 13)
			{
				if (e.KeyValue == 13)
					btnGo_Click(btnGo, EventArgs.Empty);
			}
			else if (e.Control && (e.KeyValue == 'c' || e.KeyValue == 'C' || e.KeyValue == 'v' || e.KeyValue == 'V' || e.KeyValue == 'x' || e.KeyValue == 'X'))
			{
			}
			else if (e.KeyValue == 37 || e.KeyValue == 39) // left arrow and right arrow
			{
			}
			else
			{
				e.Handled = true;
			}
		}

		private void ConsignShForm_KeyPress(object sender, KeyPressEventArgs e)
		{
			//Trace.WriteLine(e.KeyChar);
			if (e.KeyChar >= '0' && e.KeyChar <= '9')
			{
				txtBillNumber.Focus();
				txtBillNumber.Text += new string(new char[] { e.KeyChar });
				e.Handled = true;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (btnClose.Enabled)
			{
				DialogResult dr = MessageBox.Show(
					this,
					"已完成所有打印、出库, 如取消会影响库存数据以及已打印面单.\n是否确认取消?", this.Text,
					MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (DialogResult.No == dr)
					return;
			}
		
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void rdoYto_CheckedChanged(object sender, EventArgs e)
		{
			if (rdoYto.Checked)
				rdoSf.Checked = false;

			// Added by KK on 2015/11/08.
			if (rdoYto.Checked)
			{
				if (!string.IsNullOrEmpty(Settings.Instance.RecentYtoBillNumber) && Settings.Instance.RecentYtoBillNumber.Length >= 9)
				{
					txtBillNumber.Text = Settings.Instance.RecentYtoBillNumber.Substring(0, 9);
					txtBillNumber.SelectionStart = 10;
				}

				txtDestCode.Text = string.Empty;
			}
		}

		private void rdoSf_CheckedChanged(object sender, EventArgs e)
		{
			if (rdoSf.Checked)
				rdoYto.Checked = false;

			// Added by KK on 2015/11/08.
			if (rdoSf.Checked)
				txtBillNumber.Text = string.Empty;

			chkFriehghtCollect.Enabled = rdoSf.Checked;
			chkPickup.Enabled = rdoSf.Checked;
			chkSfOldBill.Enabled = rdoSf.Checked;
		}
	}
}