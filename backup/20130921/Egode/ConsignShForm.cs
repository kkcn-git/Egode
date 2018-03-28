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
using OrderParser;
using Egode.WebBrowserForms;

namespace Egode
{
	public partial class ConsignShForm : Form
	{
		private Order _order;
		List<SoldProductInfo> _soldProductInfos;
		private bool _productDescChanged;
	
		public ConsignShForm(Order order)
		{
			_order = order;
			InitializeComponent();
		}

		private void ConsignShForm_Load(object sender, EventArgs e)
		{
			txtBuyerAccount.Text = _order.BuyerAccount;
			txtRemark.Text = _order.Remark;
			txtBuyerRemark.Text = _order.BuyerRemark;

			// address info.
			if (string.IsNullOrEmpty(_order.EditedRecipientAddress))
			{
				AddressParser ap = new AddressParser(_order.RecipientAddress);
				txtRecipientName.Text = _order.RecipientName;
				txtProvince.Text = ap.Province;
				txtCity1.Text = ap.City1;
				txtCity2.Text = ap.City2;
				txtDistrict.Text = ap.District;
				txtStreetAddress.Text = ap.StreetAddress;
				txtMobile.Text = _order.MobileNumber;
				txtPhone.Text = _order.PhoneNumber;
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
				txtPhone.BackColor= Color.FromArgb(255, 200, 200);

				ParseEditedAddress();
				//MessageBox.Show(this, "需要手动输入地址!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			// Product info.
			_soldProductInfos = GetProducts(_order.Items);
			foreach (SoldProductInfo spi in _soldProductInfos)
				txtProducts.Text += string.Format("{0} x {1}\r\n", spi.ShortName, spi.Count);
			if (txtProducts.Text.Length > 2 && txtProducts.Text.EndsWith("\r\n"))
				txtProducts.Text = txtProducts.Text.Remove(txtProducts.Text.Length - 2, 2); // remove return at the end of string.
			txtProducts.TextChanged += new EventHandler(txtProducts_TextChanged);
			//

			//Bitmap bmp = new Bitmap(1200, 600);//picExpressBill.BackgroundImage.Width+100, picExpressBill.BackgroundImage.Height+100);
			//Graphics g = Graphics.FromImage(bmp);
			//DrawBill(g, true);
			//picExpressBill.Image = bmp;
			
			txtBillNumber.Focus();

			this.Activated += new EventHandler(ConsignShForm_Activated);
		}

		void ConsignShForm_Activated(object sender, EventArgs e)
		{
			txtBillNumber.Focus();
		}
		
		private void ParseEditedAddress()
		{
			string[] infos = _order.EditedRecipientAddress.Split(',');
			if (null == infos || infos.Length <= 0)
				return;
			
			int i = 0;
			
			// first segment should be recipient name.
			txtRecipientName.Text = infos[i++].Trim();
			
			// second segment should be mobile number.
			txtMobile.Text = infos[i++].Trim();
			
			if (infos.Length >= 5)
				txtPhone.Text = infos[i++].Trim();
			
			AddressParser ap = new AddressParser(infos[i++].Trim());
			txtProvince.Text = ap.Province;
			txtCity1.Text = ap.City1;
			txtCity2.Text = ap.City2;
			txtDistrict.Text = ap.District;
			txtStreetAddress.Text = ap.StreetAddress;
		}

		void txtProducts_TextChanged(object sender, EventArgs e)
		{
			_productDescChanged = true;
		}

		private void ConsignShForm_Shown(object sender, EventArgs e)
		{
			if (_order.Remark.Contains("顺丰") || _order.Remark.Contains("顺风"))
			{
				string prompt = string.Format("此订单可能需要发顺丰.\n客服备注: {0}\n\n是否发顺丰?\n(顺丰面单暂时需要手写, 打印按钮禁用)", _order.Remark);
				DialogResult dr = MessageBox.Show(this, prompt, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (DialogResult.Yes == dr)
				{
					btnPrint.Enabled = false;
					rdoSf.Checked = true;
				}
			}
		
			if (txtProducts.Text.Trim().Length < 5)
				MessageBox.Show(this, "商品描述未正确识别, 需要手动输入.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

			this.Activate();
			btnPrint.Focus();
		}
		
		private List<SoldProductInfo> GetProducts(string allItems)
		{
			List<SoldProductInfo> products = new List<SoldProductInfo>();
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

				ProductInfo pi = ProductInfo.Match(productTitle);
				if (null == pi)
					continue;
				
				bool cancelled = false;
				if (infos.Length >= 4)
					cancelled = (((Order.OrderStatus)Enum.Parse(typeof(Order.OrderStatus), infos[3])) == Order.OrderStatus.Closed);
				if (cancelled)
					continue;
				
				products.Add(new SoldProductInfo(pi.Id, pi.BrandId, pi.Name, pi.ShortName, pi.Keywords, count));
			}
			
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

		private void btnPrint_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			
			if (btnPrint.Text.Equals("Printed"))
			{
				DialogResult dr = MessageBox.Show(this, "已打印, 是否再次打印?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (DialogResult.No == dr)
					return;
			}

			PrintDocument pdoc = new PrintDocument();
			pdoc.DefaultPageSettings.PaperSize = new PaperSize("custom", 900, 450);
			pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
			pdoc.Print();

			this.Focus();
			txtBillNumber.Focus();
			btnPrint.Text = "Printed";
			btnPrint.ForeColor = Color.OrangeRed;
			this.Activate();
			Cursor.Current = Cursors.Default;
		}

		void pdoc_PrintPage(object sender, PrintPageEventArgs e)
		{
			Graphics g = e.Graphics;
			DrawBill(g, false);
			txtBillNumber.Focus();
		}

		//void DrawBill(Graphics g, bool withBg)
		//{
		//    if (null == g)
		//        return;
			
		//    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
		//    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

		//    if (withBg)
		//        g.DrawImage(picExpressBill.BackgroundImage, new Point(0, 0));

		//    DrawString(g, "德 国 e 购", new Point(150, 131), this.Font.Size + 4, FontStyle.Regular);
		//    DrawString(g, "http://egode.taobao.com", new Point(260, 140), this.Font.Size - 2, FontStyle.Regular);
		//    DrawString(g, "18964913317", new Point(172, 235));
		//    DrawString(g, DateTime.Now.ToString("yyyy/MM/dd"), new Point(120, 405));

		//    DrawString(g, txtRecipientName.Text, new Point(510, 120));
		//    if (txtBuyerAccount.Text.Length > 0)
		//        DrawString(g, string.Format("({0})", txtBuyerAccount.Text), new Point(510, 145), this.Font.Size - 2, FontStyle.Regular);

		//    DrawString(g, txtProvince.Text, new Point(585 - g.MeasureString(txtProvince.Text, this.Font).ToSize().Width, 176));
			
		//    Size city1Size = g.MeasureString(txtCity1.Text, this.Font).ToSize();
		//    DrawString(g, txtCity1.Text, new Point(660 - city1Size.Width, city1Size.Width > 72 ? 157 : 176));

		//    Size city2Size = g.MeasureString(txtCity2.Text, this.Font).ToSize();
		//    DrawString(g, txtCity2.Text, new Point(730 - city2Size.Width, city2Size.Width > 72 ? 157 : 176));

		//    Size districtSize = g.MeasureString(txtDistrict.Text, this.Font).ToSize();
		//    DrawString(g, txtDistrict.Text, new Point(790 - districtSize.Width, districtSize.Width > 72 ? 157 : 176));
			
		//    string destCity = string.Empty;
		//    if (string.IsNullOrEmpty(txtCity1.Text) && string.IsNullOrEmpty(txtCity2.Text)) // 直辖市
		//    {
		//        if (txtProvince.Text.StartsWith("上海"))
		//        {
		//            if (txtDistrict.Text.StartsWith("浦东") || txtDistrict.Text.StartsWith("南汇"))
		//                destCity = "PD";//"浦东";
		//            else
		//                destCity = "PX";//"浦西";
		//        }
		//        else
		//        {
		//            destCity = txtProvince.Text;
		//        }
		//    }
		//    else
		//    {
		//        destCity = (string.IsNullOrEmpty(txtCity2.Text) ? txtCity1.Text : txtCity2.Text);
		//    }
			
		//    Size destCitySize = g.MeasureString(destCity, new Font(this.Font.Name, this.Font.Size + 10, FontStyle.Bold)).ToSize();
		//    DrawString(
		//        g, destCity, 
		//        new Point(destCitySize.Width <= 120 ? 700 + (120 - destCitySize.Width)/2 : 820 - destCitySize.Width, 65), 
		//        this.Font.Size + 10, FontStyle.Bold);

		//    if (g.MeasureString(txtStreetAddress.Text, this.Font).Width > 320)
		//        DrawString(g, txtStreetAddress.Text, new Point(830 - g.MeasureString(txtStreetAddress.Text, this.Font).ToSize().Width, 212));
		//    else
		//        DrawString(g, txtStreetAddress.Text, new Point(450 + (350 - g.MeasureString(txtStreetAddress.Text, this.Font).ToSize().Width)/2, 212));

		//    DrawString(g, txtMobile.Text, new Point(545, 235));
		//    DrawString(g, txtPhone.Text, new Point(710, 235));

		//    // products
		//    // items.
		//    int y = 290;
		//    //foreach (ProductInfo pi in _soldProductInfos)
		//    foreach (string s in txtProducts.Lines)
		//    {
		//        //DrawString(g, string.Format("{0} x {1}", ProductInfo.GetProductDesc(pi.Id), pi.Count), new Point(160, y));
		//        DrawString(g, s, new Point(160, y));
		//        y += 20;
		//    }
			
		//    // Weight.
		//    if (!_productDescChanged)
		//    {
		//        string weight = GetWeight(_soldProductInfos);
		//        DrawString(g, weight, new Point(480, 265));
		//    }
		//}

		void DrawBill(Graphics g, bool withBg)
		{
			if (null == g)
				return;
			
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			if (withBg)
				g.DrawImage(picExpressBill.BackgroundImage, new Point(0, 0));

			DrawString(g, "德 国 e 购", new Point(150, 128), this.Font.Size, FontStyle.Regular);
			DrawString(g, "http://egode.taobao.com", new Point(235, 131), this.Font.Size - 2, FontStyle.Regular);
			DrawString(g, "18964913317", new Point(172, 210));
			DrawString(g, DateTime.Now.ToString("yyyy/MM/dd"), new Point(320, 380));

			DrawString(g, txtRecipientName.Text, new Point(510, 107));
			if (txtBuyerAccount.Text.Length > 0)
				DrawString(
					g, string.Format("({0})", txtBuyerAccount.Text), 
					new Point(510 + g.MeasureString(txtRecipientName.Text, this.Font).ToSize().Width + 1, 112), 
					this.Font.Size - 3, FontStyle.Regular);

			/*
			DrawString(g, txtProvince.Text, new Point(585 - g.MeasureString(txtProvince.Text, this.Font).ToSize().Width, 155));
			
			Size city1Size = g.MeasureString(txtCity1.Text, this.Font).ToSize();
			DrawString(g, txtCity1.Text, new Point(660 - city1Size.Width, city1Size.Width > 72 ? 135 : 155));

			Size city2Size = g.MeasureString(txtCity2.Text, this.Font).ToSize();
			DrawString(g, txtCity2.Text, new Point(730 - city2Size.Width, city2Size.Width > 72 ? 135 : 155));

			Size districtSize = g.MeasureString(txtDistrict.Text, this.Font).ToSize();
			DrawString(g, txtDistrict.Text, new Point(790 - districtSize.Width, districtSize.Width > 72 ? 135 : 155));
			
			if (g.MeasureString(txtStreetAddress.Text, this.Font).Width > 320)
				DrawString(g, txtStreetAddress.Text, new Point(830 - g.MeasureString(txtStreetAddress.Text, this.Font).ToSize().Width, 182));
			else
				DrawString(g, txtStreetAddress.Text, new Point(450 + (350 - g.MeasureString(txtStreetAddress.Text, this.Font).ToSize().Width)/2, 182));
			*/
			string fullAddress = string.Format("{0} {1} {2} {3}\n{4}", txtProvince.Text, txtCity1.Text, txtCity2.Text, txtDistrict.Text, txtStreetAddress.Text);
			SizeF fullAddressSize = g.MeasureString(fullAddress, this.Font, 840-505);
			int fullAddressTop = fullAddressSize.Height - 6 > g.MeasureString("A\nB\nC", this.Font).Height ? 110 : 131;
			StringFormat sf = new StringFormat();
			sf.Alignment = StringAlignment.Near;
			sf.LineAlignment = StringAlignment.Near;
			g.DrawString(fullAddress, this.Font, new SolidBrush(Color.Black), new RectangleF(510, fullAddressTop, (840-510), (195-fullAddressTop)));
			
			DrawString(g, txtMobile.Text + ((txtMobile.Text.Length > 0 && txtPhone.Text.Length> 0) ? ("," + txtPhone.Text) : string.Empty), new Point(544, 210));
			//DrawString(g, txtPhone.Text, new Point(680, 215));

			string destCity = string.Empty;
			if (string.IsNullOrEmpty(txtCity1.Text) && string.IsNullOrEmpty(txtCity2.Text)) // 直辖市
			{
				if (txtProvince.Text.StartsWith("上海"))
				{
					if (txtDistrict.Text.StartsWith("浦东") || txtDistrict.Text.StartsWith("南汇"))
						destCity = "PD";//"浦东";
					else
						destCity = "PX";//"浦西";
				}
				else
				{
					destCity = txtProvince.Text;
				}
			}
			else
			{
				destCity = (string.IsNullOrEmpty(txtCity2.Text) ? txtCity1.Text : txtCity2.Text);
			}
			
			Size destCitySize = g.MeasureString(destCity, new Font(this.Font.Name, this.Font.Size + 14, FontStyle.Bold)).ToSize();
			DrawString(
				g, destCity, 
				new Point(destCitySize.Width <= 120 ? 700 + (120 - destCitySize.Width)/2 : 820 - destCitySize.Width, 46), 
				this.Font.Size + 14, FontStyle.Bold);

			// products
			// items.
			int y = 268;
			//foreach (ProductInfo pi in _soldProductInfos)
			foreach (string s in txtProducts.Lines)
			{
				//DrawString(g, string.Format("{0} x {1}", ProductInfo.GetProductDesc(pi.Id), pi.Count), new Point(160, y));
				DrawString(g, s, new Point(128, y), this.Font.Size - 2, FontStyle.Regular);
				y += 14;
			}
			
			//// Weight.
			//if (!_productDescChanged)
			//{
			//    string weight = GetWeight(_soldProductInfos);
			//    DrawString(g, weight, new Point(480, 265));
			//}
		}
		private void DrawString(Graphics g, string s, Point p)
		{
			DrawString(g, s, p, this.Font.Size, FontStyle.Regular);
		}

		private void DrawString(Graphics g, string s, Point p, float fontSize, FontStyle fs)
		{
			if (null == g)
				return;
			if (string.IsNullOrEmpty(s))
				return;

			p.Offset(10, -20);
			g.DrawString(
				s,
				new Font(this.Font.Name, fontSize, fs), new SolidBrush(Color.Black), p);/*, 
				new RectangleF((float)p.X, (float)p.Y, 360, 50), 
				StringFormat.GenericDefault);*/
		}

		private void txtBillNumber_TextChanged(object sender, EventArgs e)
		{
			if (rdoSf.Checked)
			{
				if (txtBillNumber.Text.Length == 12) // sf
				{
					btnGo.Enabled = true;
					btnGo_Click(btnGo, EventArgs.Empty);
				}
				return;
			}
			
			if (txtBillNumber.Text.Length == 10) // yto
			{
				btnGo.Enabled = true;
				btnGo_Click(btnGo, EventArgs.Empty);
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

		private void btnGo_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			
			// 点发货.
			ConsignShWebBrowserForm cswbf = new ConsignShWebBrowserForm(_order.OrderId, txtBillNumber.Text);
			DialogResult dr = cswbf.ShowDialog(this);
			
			if (DialogResult.Retry == dr) // retry.
			{
				ConsignShWebBrowserForm cswbfRetry = new ConsignShWebBrowserForm(_order.OrderId, txtBillNumber.Text);
				dr = cswbfRetry.ShowDialog(this);
			}

			if (DialogResult.OK != dr)
			{
				MessageBox.Show(this, "未正确点发货, 放弃更新库存操作, 请自行核对并进行相应操作!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			_order.Consign();
			
			if (_productDescChanged)
			{
				if (DialogResult.No == MessageBox.Show(this, "商品描述被修改过, 是否继续按订单商品更新库存?\n", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
					return;
			}

			// 出库登记.
			try
			{
				string ids = string.Empty;
				string counts = string.Empty;
				foreach (SoldProductInfo spi in _soldProductInfos)
				{
					ids += spi.Id + ",";
					counts += spi.Count.ToString() + ",";
				}
				
				string url = string.Format(MainForm.URL_DATA_CENTER, "stockout");
				url += string.Format("&productids={0}&counts={1}&dest={2}&op={3}", ids, counts, _order.BuyerAccount, "kk");
				HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
				request.Method = "GET";
				request.ContentType = "text/xml";
				WebResponse response = request.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream());
				string result = reader.ReadToEnd();
				reader.Close();
				//Trace.WriteLine(result);

				MessageBox.Show(
					this,
					"Result from server: \n" + result,
					this.Text,
					MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			
			this.Close();
			
			Cursor.Current = Cursors.Default;
		}

		private void pnlPrint_Paint(object sender, PaintEventArgs e)
		{
			ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, pnlPrint.Width, pnlPrint.Height), Color.LightGray, ButtonBorderStyle.Solid);
		}

		private void pnlAddress_Paint(object sender, PaintEventArgs e)
		{
			ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, pnlAddress.Width, pnlAddress.Height), Color.FromArgb(0xff, 130, 116, 205), ButtonBorderStyle.Solid);
		}

		private void pnlConsign_Paint(object sender, PaintEventArgs e)
		{
			ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, pnlConsign.Width, pnlConsign.Height), Color.LightGray, ButtonBorderStyle.Solid);
		}

		//protected override void OnPaint(PaintEventArgs e)
		//{
		//    base.OnPaint(e);
			
		//    int y = btnPrint.Bottom + (txtBillNumber.Top - btnPrint.Bottom) / 2;
			
		//    e.Graphics.DrawLine(new Pen(Color.FromArgb(192,192,192)), new Point(6, y), new Point(this.ClientRectangle.Right - 6, y));
		//    e.Graphics.DrawLine(new Pen(Color.White), new Point(6, y+1), new Point(this.ClientRectangle.Right - 6, y+1));
		//}
	}
}