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
	public partial class StockoutForm : Form
	{
		#region class BillInfo
		private class BillInfo
		{
			private string _senderName;
			private string _senderAd;
			private string _senderTel;

			private bool _displayBuyerAccount = true;
			private string _recipientName;
			private string _buyerAccount;

			private string _province;
			private string _city1;
			private string _city2;
			private string _district;
			private string _streetAddr;
			private string _mobile;
			private string _phone;

			private string[] _productInfos;

			private bool _holidayDelivery;

			protected BillInfo()
			{
			}

			public string SenderName
			{
				get { return _senderName; }
				set { _senderName = value; }
			}

			public string SenderAd
			{
				get { return _senderAd; }
				set { _senderAd = value; }
			}

			public string SenderTel
			{
				get { return _senderTel; }
				set { _senderTel = value; }
			}

			public bool DisplayBuyerAccount
			{
				get { return _displayBuyerAccount; }
				set { _displayBuyerAccount = value; }
			}

			public string RecipientName
			{
				get { return _recipientName; }
				set { _recipientName = value; }
			}

			public string BuyerAccount
			{
				get { return _buyerAccount; }
				set { _buyerAccount = value; }
			}

			public string Province
			{
				get { return _province; }
				set { _province = value; }
			}

			public string City1
			{
				get { return _city1; }
				set { _city1 = value; }
			}

			public string City2
			{
				get { return _city2; }
				set { _city2 = value; }
			}

			public string District
			{
				get { return _district; }
				set { _district = value; }
			}

			public string StreetAddress
			{
				get { return _streetAddr; }
				set { _streetAddr = value; }
			}

			public string Mobile
			{
				get { return _mobile; }
				set { _mobile = value; }
			}

			public string Phone
			{
				get { return _phone; }
				set { _phone = value; }
			}
			
			public string[] ProductInfos
			{
				get { return _productInfos; }
				set { _productInfos = value; }
			}
			
			public bool HolidayDelivery
			{
				get { return _holidayDelivery; }
				set { _holidayDelivery = value; }
			}
		}
		#endregion
	
		#region class BillPrinterBase
		private abstract class BillPrinterBase : BillInfo
		{
			private PrintDocument _pdoc;
			private Font _font;

			protected BillPrinterBase(string documentName)
			{
				_pdoc = new PrintDocument();
				_pdoc.DocumentName = documentName;
				_pdoc.DefaultPageSettings.PaperSize = this.PaperSize;
				_pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
			}

			protected BillPrinterBase()
				: this(string.Empty)
			{
			}

			void pdoc_PrintPage(object sender, PrintPageEventArgs e)
			{
				this.OnPrintPage(sender, e);
			}

			protected virtual void OnPrintPage(object sender, PrintPageEventArgs e)
			{
			}

			protected virtual PaperSize PaperSize
			{
				get { return new PaperSize("custom", 1000, 1000); }
			}

			protected void DrawString(Graphics g, string s, Point p)
			{
				DrawString(g, s, p, this.Font.Size, FontStyle.Regular);
			}

			protected void DrawString(Graphics g, string s, Point p, float fontSize, FontStyle fs)
			{
				if (null == g)
					return;
				if (string.IsNullOrEmpty(s))
					return;

				p.Offset(10, -20);

				int plusPos = -1;
				if (s.Contains("1+") || s.Contains("2+") || s.Contains("12+"))
				{
					plusPos = s.IndexOf("+");
					s = s.Replace("+", "  ");
				}

				g.DrawString(
					s,
					new Font(this.Font.Name, fontSize, fs), new SolidBrush(Color.Black), p);/*, 
		            new RectangleF((float)p.X, (float)p.Y, 360, 50), 
		            StringFormat.GenericDefault);*/

				if (plusPos > 0)
				{
					SizeF size = g.MeasureString(s.Substring(0, plusPos), new Font(this.Font.Name, fontSize, fs));
					p.Offset((int)size.Width - 7, -7);
					g.DrawString("+", new Font(this.Font.Name, fontSize + 2, fs), new SolidBrush(Color.Black), p);
				}
			}
			
			public string PrinterName
			{
				get { return _pdoc.PrinterSettings.PrinterName; }
				set { _pdoc.PrinterSettings.PrinterName = value; }
			}

			public void Print()
			{
				_pdoc.Print();
			}

			public Font Font
			{
				get { return _font; }
				set { _font = value; }
			}
		}
		#endregion

		#region class YtoBillPrinter
		private class YtoBillPrinter : BillPrinterBase
		{
			public YtoBillPrinter(string documentName)
				: base(documentName)
			{
			}

			public YtoBillPrinter()
				: this(string.Empty)
			{
			}

			protected override PaperSize PaperSize
			{
				get { return new PaperSize("custom", 900, 550); }
			}

			protected override void OnPrintPage(object sender, PrintPageEventArgs e)
			{
				e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
				e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

				DrawString(e.Graphics, this.SenderName, new Point(70, 112), this.Font.Size, FontStyle.Regular);
				DrawString(e.Graphics, this.SenderAd, new Point(70, 135), this.Font.Size - 2, FontStyle.Regular);
				DrawString(e.Graphics, this.SenderTel, new Point(92, 227));

				DrawString(e.Graphics, DateTime.Now.ToString("yyyy/MM/dd"), new Point(265, 392));
				DrawString(e.Graphics, this.RecipientName, new Point(430, 112));
				if (this.DisplayBuyerAccount && this.BuyerAccount.Length > 0)
					DrawString(
						e.Graphics, string.Format("({0})", this.BuyerAccount),
						new Point(430 + e.Graphics.MeasureString(this.RecipientName, this.Font).ToSize().Width + 1, 116),
						this.Font.Size - 3, FontStyle.Regular);

				/*
				DrawString(e.Graphics, txtProvince.Text, new Point(585 - e.Graphics.MeasureString(txtProvince.Text, this.Font).ToSize().Width, 155));
				
				Size city1Size = e.Graphics.MeasureString(txtCity1.Text, this.Font).ToSize();
				DrawString(e.Graphics, txtCity1.Text, new Point(660 - city1Size.Width, city1Size.Width > 72 ? 135 : 155));

				Size city2Size = e.Graphics.MeasureString(txtCity2.Text, this.Font).ToSize();
				DrawString(e.Graphics, txtCity2.Text, new Point(730 - city2Size.Width, city2Size.Width > 72 ? 135 : 155));

				Size districtSize = e.Graphics.MeasureString(txtDistrict.Text, this.Font).ToSize();
				DrawString(e.Graphics, txtDistrict.Text, new Point(790 - districtSize.Width, districtSize.Width > 72 ? 135 : 155));
				
				if (e.Graphics.MeasureString(txtStreetAddress.Text, this.Font).Width > 320)
					DrawString(e.Graphics, txtStreetAddress.Text, new Point(830 - e.Graphics.MeasureString(txtStreetAddress.Text, this.Font).ToSize().Width, 182));
				else
					DrawString(e.Graphics, txtStreetAddress.Text, new Point(450 + (350 - e.Graphics.MeasureString(txtStreetAddress.Text, this.Font).ToSize().Width)/2, 182));
				*/
				string fullAddress = string.Format("{0} {1} {2} {3}\n{4}", this.Province, this.City1, this.City2, this.District, this.StreetAddress);
				SizeF fullAddressSize = e.Graphics.MeasureString(fullAddress, this.Font, 840 - 505);
				int fullAddressTop = fullAddressSize.Height - 6 > e.Graphics.MeasureString("A\nB\nC", this.Font).Height ? 122 : 146;
				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Near;
				sf.LineAlignment = StringAlignment.Near;
				e.Graphics.DrawString(fullAddress, this.Font, new SolidBrush(Color.Black), new RectangleF(430, fullAddressTop, (760 - 430), (225 - fullAddressTop)));

				string phone = this.Mobile.Trim();
				if (phone.Length > 0 && this.Phone.Trim().Length > 0)
					phone += ", ";
				if (this.Phone.Trim().Length > 0)
					phone += this.Phone;
				DrawString(e.Graphics, phone, new Point(464, 227));

				string destCity = string.Empty;
				if (string.IsNullOrEmpty(this.City1) && string.IsNullOrEmpty(this.City2)) // ֱϽ��
				{
					if (this.Province.StartsWith("�Ϻ�"))
					{
						if (this.District.StartsWith("�ֶ�") || this.District.StartsWith("�ϻ�"))
							destCity = "PD";//"�ֶ�";
						else
							destCity = "PX";//"����";
					}
					else
					{
						destCity = this.Province;
					}
				}
				else
				{
					destCity = (string.IsNullOrEmpty(this.City2) ? this.City1 : this.City2);
				}

				Size destCitySize = e.Graphics.MeasureString(destCity, new Font(this.Font.Name, this.Font.Size + 14, FontStyle.Bold)).ToSize();
				DrawString(
					e.Graphics, destCity,
					new Point(destCitySize.Width <= 120 ? 620 + (120 - destCitySize.Width) / 2 : 740 - destCitySize.Width, 46),
					this.Font.Size + 14, FontStyle.Bold);

				// products
				// items.
				int x = (this.ProductInfos.Length <= 2) ? 5 : 42;
				int y = (this.ProductInfos.Length <= 2) ? 295 : 288;
				if (this.ProductInfos.Length == 1)
					y += 5;
				//foreach (ProductInfo pi in _soldProductInfos)
				foreach (string s in this.ProductInfos)
				{
					//DrawString(e.Graphics, string.Format("{0} x {1}", ProductInfo.GetProductDesc(pi.Id), pi.Count), new Point(160, y));
					DrawString(e.Graphics, s, new Point(x, y), this.Font.Size - 2, FontStyle.Regular);
					y += 14;
				}

				if (this.HolidayDelivery)
					DrawString(e.Graphics, "��˾�ɼ������ע�⣬�˼��ڼ����������͡�\n��л�����������ˣ�", new Point(395, 450));

				//// Weight.
				//if (!_productDescChanged)
				//{
				//    string weight = GetWeight(_soldProductInfos);
				//    DrawString(e.Graphics, weight, new Point(480, 265));
				//}

				//// Draw yto seal.
				//if (null != _ytoSeal)
				//    e.Graphics.DrawImage(_ytoSeal, 250, 140);			
			}
		}
		#endregion

		#region class SfBillPrinter
		private class SfBillPrinter : BillPrinterBase
		{
			private string _destCode;
			private bool _isFreightCollect = false;
			private bool _isPickup = false;
		
			public SfBillPrinter(string documentName)
				: base(documentName)
			{
			}

			public SfBillPrinter()
				: this(string.Empty)
			{
			}
			
			public string DestCode
			{
				get { return _destCode; }
				set { _destCode = value; }
			}
			
			public bool IsFreightCollect
			{
				get { return _isFreightCollect; }
				set { _isFreightCollect = value; }
			}
			
			public bool IsPickup
			{
				get { return _isPickup; }
				set { _isPickup = value; }
			}

			protected override PaperSize PaperSize
			{
				get { return new PaperSize("custom", 780, 550); }
			}

			protected override void OnPrintPage(object sender, PrintPageEventArgs e)
			{
				e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
				e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

				string senderInfo = string.Format("{0}\n{1}\n{2}", this.SenderName, this.SenderAd, this.SenderTel);
				DrawString(e.Graphics, senderInfo, new Point(60, 170), this.Font.Size, FontStyle.Regular);

				DrawString(e.Graphics, DateTime.Now.ToString("yyyy/MM/dd"), new Point(420, 427));

				//DrawString(e.Graphics, txtRecipientName.Text, new Point(125, 240));
				//if (txtBuyerAccount.Text.Length > 0)
				//    DrawString(
				//        e.Graphics, string.Format("({0})", txtBuyerAccount.Text), 
				//        new Point(125 + e.Graphics.MeasureString(txtRecipientName.Text, this.Font).ToSize().Width + 1, 240), 
				//        this.Font.Size - 3, FontStyle.Regular);

				/*
				DrawString(e.Graphics, txtProvince.Text, new Point(585 - e.Graphics.MeasureString(txtProvince.Text, this.Font).ToSize().Width, 155));
			
				Size city1Size = e.Graphics.MeasureString(txtCity1.Text, this.Font).ToSize();
				DrawString(e.Graphics, txtCity1.Text, new Point(660 - city1Size.Width, city1Size.Width > 72 ? 135 : 155));

				Size city2Size = e.Graphics.MeasureString(txtCity2.Text, this.Font).ToSize();
				DrawString(e.Graphics, txtCity2.Text, new Point(730 - city2Size.Width, city2Size.Width > 72 ? 135 : 155));

				Size districtSize = e.Graphics.MeasureString(txtDistrict.Text, this.Font).ToSize();
				DrawString(e.Graphics, txtDistrict.Text, new Point(790 - districtSize.Width, districtSize.Width > 72 ? 135 : 155));
			
				if (e.Graphics.MeasureString(txtStreetAddress.Text, this.Font).Width > 320)
					DrawString(e.Graphics, txtStreetAddress.Text, new Point(830 - e.Graphics.MeasureString(txtStreetAddress.Text, this.Font).ToSize().Width, 182));
				else
					DrawString(e.Graphics, txtStreetAddress.Text, new Point(450 + (350 - e.Graphics.MeasureString(txtStreetAddress.Text, this.Font).ToSize().Width)/2, 182));
				*/
				string phone = this.Mobile;
				if (phone.Length > 0 && this.Phone.Length > 0)
					phone += ", ";
				if (this.Phone.Length > 0)
					phone += this.Phone;

				string fullAddress = string.Format(
					"           {0}{1}\n{7}\n{2} {3} {4} {5}\n{6}",
					this.RecipientName,
					this.DisplayBuyerAccount ? string.Format("({0})", this.BuyerAccount) : string.Empty,
					this.Province, this.City1, this.City2, this.District, this.StreetAddress,
					phone);
				//SizeF fullAddressSize = e.Graphics.MeasureString(fullAddress, this.Font, 840-505);
				//int fullAddressTop = fullAddressSize.Height - 6 > e.Graphics.MeasureString("A\nB\nC", this.Font).Height ? 110 : 131;
				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Near;
				sf.LineAlignment = StringAlignment.Near;
				int fullAddressTop = 250;
				e.Graphics.DrawString(fullAddress, this.Font, new SolidBrush(Color.Black), new RectangleF(60, fullAddressTop, (360 - 60), (370 - fullAddressTop)));

				//string phone = txtMobile.Text.Trim();
				//if (phone.Length > 0 && txtPhone.Text.Trim().Length > 0)
				//    phone += ", ";
				//if (txtPhone.Text.Trim().Length > 0)
				//    phone += txtPhone.Text;
				//DrawString(e.Graphics, phone, new Point(110, 396));
				////DrawString(e.Graphics, txtPhone.Text, new Point(680, 215));

				//string destCity = string.Empty;
				//if (string.IsNullOrEmpty(txtCity1.Text) && string.IsNullOrEmpty(txtCity2.Text)) // ֱϽ��
				//{
				//    if (txtProvince.Text.StartsWith("�Ϻ�"))
				//    {
				//        if (txtDistrict.Text.StartsWith("�ֶ�") || txtDistrict.Text.StartsWith("�ϻ�"))
				//            destCity = "PD";//"�ֶ�";
				//        else
				//            destCity = "PX";//"����";
				//    }
				//    else
				//    {
				//        destCity = txtProvince.Text;
				//    }
				//}
				//else
				//{
				//    destCity = (string.IsNullOrEmpty(txtCity2.Text) ? txtCity1.Text : txtCity2.Text);
				//}

				//Size destCitySize = e.Graphics.MeasureString(destCity, new Font(this.Font.Name, this.Font.Size + 14, FontStyle.Bold)).ToSize();
				//DrawString(
				//    e.Graphics, destCity, 
				//    new Point(destCitySize.Width <= 120 ? 700 + (120 - destCitySize.Width)/2 : 820 - destCitySize.Width, 46), 
				//    this.Font.Size + 14, FontStyle.Bold);

				// products
				// items.
				int y = 402;
				//foreach (ProductInfo pi in _soldProductInfos)
				foreach (string s in this.ProductInfos)
				{
					//DrawString(e.Graphics, string.Format("{0} x {1}", ProductInfo.GetProductDesc(pi.Id), pi.Count), new Point(160, y));
					DrawString(e.Graphics, s, new Point(20, y), this.Font.Size - 2, FontStyle.Regular);
					y += 14;
				}
				DrawString(e.Graphics, "�̷�", new Point(20, y), this.Font.Size - 2, FontStyle.Regular);

				//string empno = "4 8 8 1 3 3";
				//DrawString(e.Graphics, empno, new Point(555, 46), this.Font.Size, FontStyle.Bold);

				//// Weight.
				//if (!_productDescChanged)
				//{
				//    string weight = GetWeight(_soldProductInfos);
				//    DrawString(e.Graphics, weight, new Point(480, 265));
				//}

				// �½��˺�
				if (!this.IsFreightCollect)
					DrawString(e.Graphics, "0218687063", new Point(488, 348), this.Font.Size, FontStyle.Bold); 

				DrawString(e.Graphics, _destCode, new Point(625, 76), this.Font.Size + 4, FontStyle.Bold);
				DrawString(e.Graphics, "021", new Point(550, 80), this.Font.Size, FontStyle.Regular);
				DrawString(
					e.Graphics, 
					string.Format("ת��Э��ͻ�(����)    {0}", this.HolidayDelivery ? "�˼��ڼ�����������, лл!" : string.Empty), 
					new Point(412, 450), this.Font.Size, FontStyle.Regular);
			}
		}
		#endregion

		#region class SfBillNewPrinter
		private class SfBillNewPrinter : SfBillPrinter
		{
			private const int MAX_PRODUCT_LINES	= 4;
			private readonly string _shipmentNumber;
			private int _itemAmount=1;
			
			private static Image _sfLogo;
			
			static SfBillNewPrinter()
			{
				ResourceManager rm = new ResourceManager("Egode.Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());
				//_sfLogo = (Image)rm.GetObject("sf_logo_new");
				_sfLogo = (Image)rm.GetObject("sf_logo_vertical"); // modified by KK on 2015/09/14
			}
		
			public SfBillNewPrinter(string shipmentNumber, string documentName)
				: base(documentName)
			{
				_shipmentNumber = shipmentNumber;
			}

			public SfBillNewPrinter(string shipmentNumber)
				: this(shipmentNumber, string.Empty)
			{
			}

			protected override PaperSize PaperSize
			{
				get { return new PaperSize("Custom", CalcInch(100f), CalcInch(134f)); ; }
			}
			
			public int ItemAmount
			{
				get { return _itemAmount; }
				set { _itemAmount = value; }
			}
			
			private static int CalcInch(float mm)
			{
				return (int)(mm / 25.4f * 100);
			}

			#region obsoleted code. 2015.03ǰ�ϰ��浥.
			/*
			 * 2015.03ǰ�ϰ��浥.
			protected override void OnPrintPage(object sender, PrintPageEventArgs e)
			{
				BarCode.Code128 code128 = new BarCode.Code128();
				code128.Height = (uint)CalcInch(10f);
				Image barCodeImage = code128.GetCodeImage(_shipmentNumber, BarCode.Code128.Encode.Code128C);
				
				StringBuilder sbSenderInfo = new StringBuilder();
				if (!string.IsNullOrEmpty(this.SenderName))
					sbSenderInfo.Append(this.SenderName+"\n");
				if (!string.IsNullOrEmpty(this.SenderAd))
					sbSenderInfo.Append(this.SenderAd+"\n");
				if (!string.IsNullOrEmpty(this.SenderTel))
					sbSenderInfo.Append(this.SenderTel);
				
				StringBuilder sbReceiverInfo = new StringBuilder();
				sbReceiverInfo.Append(this.RecipientName);
				if (this.DisplayBuyerAccount)
					sbReceiverInfo.Append(string.Format("({0})", this.BuyerAccount));
				if (!string.IsNullOrEmpty(this.Mobile))
					sbReceiverInfo.Append("  " + this.Mobile + ",");
				if (!string.IsNullOrEmpty(this.Phone))
					sbReceiverInfo.Append(this.Phone);
				if (sbReceiverInfo.ToString().EndsWith(","))
					sbReceiverInfo.Remove(sbReceiverInfo.Length-1, 1);
				sbReceiverInfo.Append("\n");
				if (!string.IsNullOrEmpty(this.Province))
					sbReceiverInfo.Append(this.Province + " ");
				if (!string.IsNullOrEmpty(this.City1))
					sbReceiverInfo.Append(this.City1 + " ");
				if (!string.IsNullOrEmpty(this.City2))
					sbReceiverInfo.Append(this.City2 + " ");
				if (!string.IsNullOrEmpty(this.District))
					sbReceiverInfo.Append(this.District + " ");
				sbReceiverInfo.Append("\n");
				sbReceiverInfo.Append(this.StreetAddress);

				PointF upperLeft = new PointF(CalcInch(0f), CalcInch(0f));
				PointF lowerRight = new PointF(CalcInch(97f), CalcInch(88f));
				SizeF fullSize = new SizeF(lowerRight.X - upperLeft.X, lowerRight.Y - upperLeft.Y);

				Pen pen = new Pen(Color.Black, 1f);
				Pen thickPen = new Pen(Color.Black, 2f);
				e.Graphics.DrawRectangle(pen, upperLeft.X, upperLeft.Y, fullSize.Width, fullSize.Height);
				
				e.Graphics.DrawLine(pen, upperLeft.X, upperLeft.Y + CalcInch(15f), lowerRight.X, upperLeft.Y + CalcInch(15f));
				e.Graphics.DrawLine(pen, upperLeft.X, upperLeft.Y + CalcInch(27f), lowerRight.X, upperLeft.Y + CalcInch(27f));
				e.Graphics.DrawLine(thickPen, upperLeft.X, upperLeft.Y + CalcInch(39f), lowerRight.X, upperLeft.Y + CalcInch(39f));
				e.Graphics.DrawLine(thickPen, upperLeft.X, upperLeft.Y + CalcInch(61.5f), lowerRight.X, upperLeft.Y + CalcInch(61.5f));
				e.Graphics.DrawLine(pen, upperLeft.X+CalcInch(68f), upperLeft.Y + CalcInch(46.5f), lowerRight.X, upperLeft.Y + CalcInch(46.5f));
				e.Graphics.DrawLine(pen, upperLeft.X+CalcInch(68f), upperLeft.Y + CalcInch(51.5f), lowerRight.X, upperLeft.Y + CalcInch(51.5f));
				e.Graphics.DrawLine(pen, upperLeft.X+CalcInch(68f), upperLeft.Y + CalcInch(56.5f), lowerRight.X, upperLeft.Y + CalcInch(56.5f));
				e.Graphics.DrawLine(pen, upperLeft.X, upperLeft.Y + CalcInch(66f), lowerRight.X, upperLeft.Y + CalcInch(66f));
				e.Graphics.DrawLine(pen, upperLeft.X, upperLeft.Y + CalcInch(71f), lowerRight.X, upperLeft.Y + CalcInch(71f));
				e.Graphics.DrawLine(pen, upperLeft.X + CalcInch(19.6f), upperLeft.Y + CalcInch(61.5f), upperLeft.X + CalcInch(19.6f), upperLeft.Y + CalcInch(71f));
				e.Graphics.DrawLine(pen, upperLeft.X + CalcInch(39.2f), upperLeft.Y + CalcInch(61.5f), upperLeft.X + CalcInch(39.2f), upperLeft.Y + CalcInch(71f));
				e.Graphics.DrawLine(pen, upperLeft.X + CalcInch(58.8f), upperLeft.Y + CalcInch(61.5f), upperLeft.X + CalcInch(58.8f), upperLeft.Y + CalcInch(71f));
				e.Graphics.DrawLine(pen, upperLeft.X + CalcInch(78.4f), upperLeft.Y + CalcInch(61.5f), upperLeft.X + CalcInch(78.4f), upperLeft.Y + CalcInch(71f));

				e.Graphics.DrawLine(pen, upperLeft.X+CalcInch(68f), upperLeft.Y + CalcInch(15f), upperLeft.X+CalcInch(68f), upperLeft.Y + CalcInch(61.5f));
				
				SolidBrush b = new SolidBrush(Color.Black);
				//e.Graphics.DrawString("E", new Font("Arial Black", 45f), b, new PointF(CalcInch(68f-15f), CalcInch(-4.5f))); // �����ػ�, not˳���ػ�
				e.Graphics.DrawString("�ķ�: ", new Font("����", 7f), b, new PointF(CalcInch(1f), CalcInch(16.5f)));
				e.Graphics.DrawString(sbSenderInfo.ToString().Trim(), new Font("����", 7f), b, new PointF(CalcInch(8f), CalcInch(16.5f)));
				//e.Graphics.DrawString("�¹�e��", new Font("����", 7f), b, new PointF(CalcInch(7f), CalcInch(16f)));
				//e.Graphics.DrawString("http://egode.taobao.com", new Font("Arial", 7f), b, new PointF(CalcInch(7f), CalcInch(19.5f)));
				//e.Graphics.DrawString("18964913317", new Font("Arial", 7f), b, new PointF(CalcInch(7f), CalcInch(23f)));
				e.Graphics.DrawString("ԭ�ĵ�: ", new Font("����", 7.5f), b, new PointF(CalcInch(68f), CalcInch(16f)));
				e.Graphics.DrawString("021CF", new Font("Arial", 12f), b, new PointF(CalcInch(80f), CalcInch(20f)));
				e.Graphics.DrawString("�շ�: ", new Font("����", 7f), b, new PointF(CalcInch(1f), CalcInch(27.5f)));
				e.Graphics.DrawString(sbReceiverInfo.ToString().Trim(), new Font("����", 7f), b, new RectangleF(CalcInch(8f), CalcInch(27.5f), CalcInch(68f-8f), CalcInch(11f)));
				if (this.IsPickup)
					e.Graphics.DrawString("��ȡ", new Font("����", 11f), b, new RectangleF(CalcInch(-1f), CalcInch(32f), CalcInch(68f-7f), CalcInch(11f)));
				e.Graphics.DrawString("Ŀ�ĵ�: ", new Font("����", 7.5f), b, new PointF(CalcInch(68f), CalcInch(28f)));
				e.Graphics.DrawString(this.DestCode, new Font("Arial", 26f), b, new PointF(CalcInch(75f), CalcInch(30f)));
				e.Graphics.DrawString("˳���ػ�", new Font("����", 16f), b, new PointF(CalcInch(69.5f), CalcInch(40f)));
				e.Graphics.DrawString("�ռ�Ա", new Font("����", 7f), b, new PointF(CalcInch(68f), CalcInch(48f)));
				e.Graphics.DrawString("488133", new Font("Arial", 9f), b, new PointF(CalcInch(80f), CalcInch(47.5f)));
				e.Graphics.DrawString("�ļ�����", new Font("����", 7f), b, new PointF(CalcInch(68f), CalcInch(53f)));
				e.Graphics.DrawString(DateTime.Now.ToString("yyyy/MM/dd"), new Font("Arial", 9f), b, new PointF(CalcInch(78.3f), CalcInch(52.5f)));
				e.Graphics.DrawString("�ɼ�Ա", new Font("����", 7f), b, new PointF(CalcInch(68f), CalcInch(58f)));
				e.Graphics.DrawString("�м�������", new Font("����", 7f), b, new PointF(CalcInch(3f), CalcInch(62.5f)));
				e.Graphics.DrawString("ʵ������", new Font("����", 7f), b, new PointF(CalcInch(24f), CalcInch(62.5f)));
				e.Graphics.DrawString("�Ʒ�����", new Font("����", 7f), b, new PointF(CalcInch(43.5f), CalcInch(62.5f)));
				e.Graphics.DrawString("�˷�", new Font("����", 7f), b, new PointF(CalcInch(65.5f), CalcInch(62.5f)));
				e.Graphics.DrawString("���úϼ�", new Font("����", 7f), b, new PointF(CalcInch(82f), CalcInch(62.5f)));
				e.Graphics.DrawString(this.ItemAmount.ToString(), new Font("Arial", 9f), b, new PointF(CalcInch(9f), CalcInch(67f)));
				string payment = "���ʽ:";
				payment += (this.IsFreightCollect ? "����" : "�ĸ��½�");
				payment += (this.IsFreightCollect ? string.Empty : "  �½��˺�:0218687063");
				e.Graphics.DrawString(payment, new Font("����", 8f), b, new PointF(CalcInch(2f), CalcInch(73f)));
				e.Graphics.DrawString("�շ�ǩ��:\n����:        ��    ��", new Font("����", 7f, FontStyle.Italic), b, new PointF(CalcInch(65.5f), CalcInch(82f)));
				
				e.Graphics.DrawImage(barCodeImage, new RectangleF(CalcInch(12f), CalcInch(41f), CalcInch(45f), CalcInch(10f)));
				//����ĸ�������ӡ.
				//e.Graphics.DrawString("1/1", new Font("Arial", 10f), b, new PointF(CalcInch(5f), CalcInch(44.5f)));
				//e.Graphics.DrawString("ĸ����", new Font("����", 7f), b, new PointF(CalcInch(19f), CalcInch(53f)));
				//e.Graphics.DrawString(NormalizeNumber(_shipmentNumber), new Font("Arial", 9f), b, new PointF(CalcInch(28f), CalcInch(52.5f)));
				e.Graphics.DrawString(NormalizeNumber(_shipmentNumber), new Font("Arial", 9f), b, new PointF(CalcInch(23f), CalcInch(52.5f)));
				
				// ��2��
				PointF upperLeft2 = new PointF(CalcInch(0f), CalcInch(91f));
				PointF lowerRight2 = new PointF(CalcInch(97f), CalcInch(149f));
				SizeF fullSize2 = new SizeF(lowerRight2.X - upperLeft2.X, lowerRight2.Y - upperLeft2.Y);
				e.Graphics.DrawRectangle(pen, upperLeft2.X, upperLeft2.Y, fullSize2.Width, fullSize2.Height);
				e.Graphics.DrawLine(pen, upperLeft2.X, upperLeft2.Y + CalcInch(16f), lowerRight2.X, upperLeft2.Y + CalcInch(16f));
				e.Graphics.DrawLine(pen, upperLeft2.X, upperLeft2.Y + CalcInch(32f), lowerRight2.X, upperLeft2.Y + CalcInch(32f));
				e.Graphics.DrawLine(pen, upperLeft2.X, upperLeft2.Y + CalcInch(37f), lowerRight2.X, upperLeft2.Y + CalcInch(37f));
				e.Graphics.DrawLine(pen, upperLeft2.X+CalcInch(53f), upperLeft2.Y, upperLeft2.X+CalcInch(53f), upperLeft2.Y + CalcInch(32f));

				e.Graphics.DrawImage(barCodeImage, new RectangleF(upperLeft2.X+CalcInch(4f), upperLeft2.Y+CalcInch(2f), CalcInch(45f), CalcInch(8f)));
				//e.Graphics.DrawString("ĸ����", new Font("����", 7f), b, new PointF(upperLeft2.X+CalcInch(9.5f), upperLeft2.Y+CalcInch(11.5f)));
				//e.Graphics.DrawString(NormalizeNumber(_shipmentNumber), new Font("Arial", 9f), b, new PointF(upperLeft2.X+CalcInch(18.5f), upperLeft2.Y+CalcInch(11f)));
				e.Graphics.DrawString(NormalizeNumber(_shipmentNumber), new Font("Arial", 9f), b, new PointF(upperLeft2.X+CalcInch(15f), upperLeft2.Y+CalcInch(11f)));
				e.Graphics.DrawString("�ķ�: ", new Font("����", 7f), b, new PointF(upperLeft2.X+CalcInch(54f), upperLeft2.Y+CalcInch(2f)));
				e.Graphics.DrawString(sbSenderInfo.ToString().Trim(), new Font("����", 7f), b, new PointF(upperLeft2.X+CalcInch(61f), upperLeft2.Y+CalcInch(2f)));
				e.Graphics.DrawString("�շ�: ", new Font("����", 7f), b, new PointF(upperLeft2.X+CalcInch(1f), upperLeft2.Y+CalcInch(17f)));
				e.Graphics.DrawString(sbReceiverInfo.ToString().Trim(), new Font("����", 7f), b, new RectangleF(upperLeft2.X+CalcInch(8f), upperLeft2.Y+CalcInch(17f), CalcInch(53f-8f), CalcInch(15f)));
				e.Graphics.DrawString("�м���", new Font("����", 7f), b, new PointF(upperLeft2.X+CalcInch(1f), upperLeft2.Y+CalcInch(33.2f)));
				
				//string[] itemInfos = SplitItemInfo(_itemInfo);
				float x = 3f;
				//foreach (string s in itemInfos)
				//{
				//    e.Graphics.DrawString(s, new Font("����", 7f), b, new PointF(upperLeft2.X+CalcInch(x), upperLeft2.Y+CalcInch(38f)));
				//    x += 34; //e.Graphics.MeasureString(s, new Font("����", 7f)).Width *15/1440*2.54;
				//}
				
				StringBuilder sbProducts = new StringBuilder();
				for (int i = 0; i < this.ProductInfos.Length; i++)
				{
					sbProducts.Append(this.ProductInfos[i]);

					if (i == this.ProductInfos.Length-1 || (i > 0 && i % MAX_PRODUCT_LINES == MAX_PRODUCT_LINES-1))
					{
						e.Graphics.DrawString(sbProducts.ToString(), new Font("����", 7f), b, new PointF(upperLeft2.X+CalcInch(x), upperLeft2.Y+CalcInch(38f)));
						x += 34; //e.Graphics.MeasureString(s, new Font("����", 7f)).Width *15/1440*2.54;
						sbProducts = new StringBuilder();
					}
				}

				// ��3��
				PointF upperLeft3 = new PointF(CalcInch(0f), CalcInch(153f));
				PointF lowerRight3 = new PointF(CalcInch(97f), CalcInch(211f));
				SizeF fullSize3 = new SizeF(lowerRight3.X - upperLeft3.X, lowerRight3.Y - upperLeft3.Y);
				e.Graphics.DrawRectangle(pen, upperLeft3.X, upperLeft3.Y, fullSize3.Width, fullSize3.Height);
				e.Graphics.DrawLine(pen, upperLeft3.X, upperLeft3.Y + CalcInch(16f), lowerRight3.X, upperLeft3.Y + CalcInch(16f));
				e.Graphics.DrawLine(pen, upperLeft3.X, upperLeft3.Y + CalcInch(32f), lowerRight3.X, upperLeft3.Y + CalcInch(32f));
				e.Graphics.DrawLine(pen, upperLeft3.X, upperLeft3.Y + CalcInch(37f), lowerRight3.X, upperLeft3.Y + CalcInch(37f));
				e.Graphics.DrawLine(pen, upperLeft3.X+CalcInch(53f), upperLeft3.Y, upperLeft3.X+CalcInch(53f), upperLeft3.Y + CalcInch(32f));

				e.Graphics.DrawImage(barCodeImage, new RectangleF(upperLeft3.X+CalcInch(4f), upperLeft3.Y+CalcInch(2f), CalcInch(45f), CalcInch(8f)));
				//e.Graphics.DrawString("ĸ����", new Font("����", 7f), b, new PointF(upperLeft3.X+CalcInch(9.5f), upperLeft3.Y+CalcInch(11.5f)));
				//e.Graphics.DrawString(NormalizeNumber(_shipmentNumber), new Font("Arial", 9f), b, new PointF(upperLeft3.X+CalcInch(18.5f), upperLeft3.Y+CalcInch(11f)));
				e.Graphics.DrawString(NormalizeNumber(_shipmentNumber), new Font("Arial", 9f), b, new PointF(upperLeft3.X+CalcInch(15f), upperLeft3.Y+CalcInch(11f)));
				e.Graphics.DrawString("�ķ�: ", new Font("����", 7f), b, new PointF(upperLeft3.X+CalcInch(54f), upperLeft3.Y+CalcInch(2f)));
				e.Graphics.DrawString(sbSenderInfo.ToString().Trim(), new Font("����", 7f), b, new PointF(upperLeft3.X+CalcInch(61f), upperLeft3.Y+CalcInch(2f)));
				e.Graphics.DrawString("�շ�: ", new Font("����", 7f), b, new PointF(upperLeft3.X+CalcInch(1f), upperLeft3.Y+CalcInch(17f)));
				e.Graphics.DrawString(sbReceiverInfo.ToString().Trim(), new Font("����", 7f), b, new RectangleF(upperLeft3.X+CalcInch(8f), upperLeft3.Y+CalcInch(17f), CalcInch(53f-8f), CalcInch(15f)));
				e.Graphics.DrawString("�м���", new Font("����", 7f), b, new PointF(upperLeft3.X+CalcInch(1f), upperLeft3.Y+CalcInch(33.2f)));
				
				x = 3f;
				//foreach (string s in itemInfos)
				//{
				//    e.Graphics.DrawString(s, new Font("����", 7f), b, new PointF(upperLeft3.X+CalcInch(x), upperLeft3.Y+CalcInch(38f)));
				//    x += 34; //e.Graphics.MeasureString(s, new Font("����", 7f)).Width *15/1440*2.54;
				//}			
				sbProducts = new StringBuilder();
				for (int i = 0; i < this.ProductInfos.Length; i++)
				{
					sbProducts.Append(this.ProductInfos[i]);

					if (i == this.ProductInfos.Length - 1 || (i > 0 && i % MAX_PRODUCT_LINES == MAX_PRODUCT_LINES - 1))
					{
						e.Graphics.DrawString(sbProducts.ToString(), new Font("����", 7f), b, new PointF(upperLeft3.X+CalcInch(x), upperLeft3.Y+CalcInch(38f)));
						x += 34; //e.Graphics.MeasureString(s, new Font("����", 7f)).Width *15/1440*2.54;
						sbProducts = new StringBuilder();
					}
				}
			}
			*/
			#endregion

			/*
			// 2015.03��ʼ�°��浥.
			protected override void OnPrintPage(object sender, PrintPageEventArgs e)
			{
				BarCode.Code128 code128 = new BarCode.Code128();
				code128.Height = (uint)CalcInch(10f);
				Image barCodeImage = code128.GetCodeImage(_shipmentNumber, BarCode.Code128.Encode.Code128C);

				StringBuilder sbSenderInfo = new StringBuilder();
				if (!string.IsNullOrEmpty(this.SenderName))
					sbSenderInfo.Append(this.SenderName + "\n");
				if (!string.IsNullOrEmpty(this.SenderAd))
					sbSenderInfo.Append(this.SenderAd + "\n");
				if (!string.IsNullOrEmpty(this.SenderTel))
					sbSenderInfo.Append(this.SenderTel);

				StringBuilder sbProvinceCityDistrict = new StringBuilder();
				sbProvinceCityDistrict.Append(this.Province);
				if (!string.IsNullOrEmpty(this.City1))
					sbProvinceCityDistrict.Append("," + this.City1);
				if (!string.IsNullOrEmpty(this.City2))
					sbProvinceCityDistrict.Append("," + this.City2);
				if (!string.IsNullOrEmpty(this.District))
					sbProvinceCityDistrict.Append("," + this.District);

				StringBuilder sbReceiverInfo = new StringBuilder();
				sbReceiverInfo.Append(this.StreetAddress);
				sbReceiverInfo.Append("\n");
				sbReceiverInfo.Append(this.RecipientName);
				if (this.DisplayBuyerAccount)
					sbReceiverInfo.Append(string.Format("({0})", this.BuyerAccount));
				if (!string.IsNullOrEmpty(this.Mobile))
					sbReceiverInfo.Append(" " + this.Mobile + ",");
				if (!string.IsNullOrEmpty(this.Phone))
					sbReceiverInfo.Append(this.Phone);
				if (sbReceiverInfo.ToString().EndsWith(","))
					sbReceiverInfo.Remove(sbReceiverInfo.Length - 1, 1);

				PointF upperLeft = new PointF(CalcInch(0f), CalcInch(0f));
				PointF lowerRight = new PointF(CalcInch(97f), CalcInch(88f));
				SizeF fullSize = new SizeF(lowerRight.X - upperLeft.X, lowerRight.Y - upperLeft.Y);

				Pen pen = new Pen(Color.Black, 1f);
				Pen thickPen = new Pen(Color.Black, 2f);
				e.Graphics.DrawRectangle(pen, upperLeft.X, upperLeft.Y, fullSize.Width, fullSize.Height);

				e.Graphics.DrawLine(pen, upperLeft.X, upperLeft.Y + CalcInch(15f), lowerRight.X, upperLeft.Y + CalcInch(15f));
				e.Graphics.DrawLine(pen, upperLeft.X, upperLeft.Y + CalcInch(35f), lowerRight.X, upperLeft.Y + CalcInch(35f));
				e.Graphics.DrawLine(pen, upperLeft.X, upperLeft.Y + CalcInch(53f), lowerRight.X, upperLeft.Y + CalcInch(53f));
				e.Graphics.DrawLine(pen, upperLeft.X, upperLeft.Y + CalcInch(74f), lowerRight.X, upperLeft.Y + CalcInch(74f));
				e.Graphics.DrawLine(pen, upperLeft.X, upperLeft.Y + CalcInch(35f), lowerRight.X, upperLeft.Y + CalcInch(35f));
				e.Graphics.DrawLine(pen, upperLeft.X, upperLeft.Y + CalcInch(35f), lowerRight.X, upperLeft.Y + CalcInch(35f));
				e.Graphics.DrawLine(pen, upperLeft.X + CalcInch(66f), upperLeft.Y + CalcInch(15f), upperLeft.X + CalcInch(66f), upperLeft.Y + CalcInch(35f));
				e.Graphics.DrawLine(pen, upperLeft.X + CalcInch(66f), upperLeft.Y + CalcInch(53f), upperLeft.X + CalcInch(66f), upperLeft.Y + CalcInch(74f));
				e.Graphics.DrawLine(pen, upperLeft.X + CalcInch(66f), upperLeft.Y + CalcInch(64f), lowerRight.X, upperLeft.Y + CalcInch(64f));
				e.Graphics.DrawLine(pen, upperLeft.X + CalcInch(53f), upperLeft.Y + CalcInch(74f), upperLeft.X + CalcInch(53f), lowerRight.Y);
				e.Graphics.DrawLine(pen, upperLeft.X + CalcInch(75f), upperLeft.Y + CalcInch(74f), upperLeft.X + CalcInch(75f), lowerRight.Y);

				SolidBrush b = new SolidBrush(Color.Black);

				e.Graphics.DrawImage(barCodeImage, new RectangleF(CalcInch(12f), CalcInch(17f), CalcInch(45f), CalcInch(12f)));
				//����ĸ�������ӡ.
				//e.Graphics.DrawString("1/1", new Font("Arial", 10f), b, new PointF(CalcInch(5f), CalcInch(44.5f)));
				//e.Graphics.DrawString("ĸ����", new Font("����", 7f), b, new PointF(CalcInch(19f), CalcInch(53f)));
				//e.Graphics.DrawString(NormalizeNumber(_shipmentNumber), new Font("Arial", 9f), b, new PointF(CalcInch(28f), CalcInch(52.5f)));
				e.Graphics.DrawString(NormalizeNumber(_shipmentNumber), new Font("Arial", 9f), b, new PointF(CalcInch(23f), CalcInch(30f)));

				e.Graphics.DrawString("˳���ػ�", new Font("����", 12f), b, new PointF(CalcInch(72f), CalcInch(16f)));
				e.Graphics.DrawString("Ŀ�ĵ� ", new Font("����", 6f), b, new PointF(CalcInch(67f), CalcInch(23f)));
				e.Graphics.DrawString(this.DestCode, new Font("Arial Bold", 24f), b, new PointF(CalcInch(72f), CalcInch(25f)));
				e.Graphics.DrawString("��\n�� ", new Font("����", 14f), b, new PointF(CalcInch(1.5f), CalcInch(37f)));
				if (this.IsPickup)
					e.Graphics.DrawString("(��ȡ)", new Font("����", 8f), b, new PointF(CalcInch(0.5f), CalcInch(49f)));

				e.Graphics.DrawString(sbProvinceCityDistrict.ToString(), new Font("����", 14f), b, new PointF(CalcInch(10f), CalcInch(36f)));
				e.Graphics.DrawString(sbReceiverInfo.ToString(), new Font("����", 8f), b, new RectangleF(CalcInch(10f), CalcInch(42f), CalcInch(88f), CalcInch(11f)));

				string payment = "���ʽ:";
				payment += (this.IsFreightCollect ? "����" : "�ĸ��½�");
				payment += (this.IsFreightCollect ? string.Empty : "  �½��˺�:0218687063");
				e.Graphics.DrawString(payment, new Font("����", 6f), b, new PointF(CalcInch(2f), CalcInch(55f)));
				e.Graphics.DrawString("ʵ������:          �Ʒ�����:", new Font("����", 6f), b, new PointF(CalcInch(2f), CalcInch(59f)));
				e.Graphics.DrawString("�˷�:\n���úϼ�:", new Font("����", 9f), b, new PointF(CalcInch(68f), CalcInch(65.5f)));

				e.Graphics.DrawString("��\n�� ", new Font("����", 10f), b, new PointF(CalcInch(1.5f), CalcInch(76f)));
				e.Graphics.DrawString(sbSenderInfo.ToString().Trim(), new Font("����", 6f), b, new PointF(CalcInch(8f), CalcInch(76.5f)));
				e.Graphics.DrawString("ԭ�ĵ�: ", new Font("����", 6f), b, new PointF(CalcInch(39f), CalcInch(85f)));
				e.Graphics.DrawString("021", new Font("Arial", 6f), b, new PointF(CalcInch(47.5f), CalcInch(85f)));

				e.Graphics.DrawString("�ռ�Ա:", new Font("����", 6f), b, new PointF(CalcInch(53.2f), CalcInch(76f)));
				//e.Graphics.DrawString("488133", new Font("Arial", 6f), b, new PointF(CalcInch(63.2f), CalcInch(76f)));
				e.Graphics.DrawString("�ļ�����:", new Font("����", 6f), b, new PointF(CalcInch(53.2f), CalcInch(80f)));
				e.Graphics.DrawString(DateTime.Now.ToString("yyyy/MM/dd"), new Font("Arial", 6f), b, new PointF(CalcInch(63.2f), CalcInch(79.7f)));
				e.Graphics.DrawString("�ɼ�Ա:", new Font("����", 6f), b, new PointF(CalcInch(53.2f), CalcInch(84f)));
				e.Graphics.DrawString("�շ�ǩ��", new Font("����", 7f), b, new PointF(CalcInch(80.5f), CalcInch(75f)));
				e.Graphics.DrawString("����:   ��    ��", new Font("����", 7f, FontStyle.Italic), b, new PointF(CalcInch(75f), CalcInch(84.5f)));
				
				// ��2��
				PointF upperLeft2 = new PointF(CalcInch(0f), CalcInch(91f));
				PointF lowerRight2 = new PointF(CalcInch(97f), CalcInch(149f));
				SizeF fullSize2 = new SizeF(lowerRight2.X - upperLeft2.X, lowerRight2.Y - upperLeft2.Y);
				e.Graphics.DrawRectangle(pen, upperLeft2.X, upperLeft2.Y, fullSize2.Width, fullSize2.Height);
				e.Graphics.DrawLine(pen, upperLeft2.X, upperLeft2.Y + CalcInch(17f), lowerRight2.X, upperLeft2.Y + CalcInch(17f));
				e.Graphics.DrawLine(pen, upperLeft2.X, upperLeft2.Y + CalcInch(36f), lowerRight2.X, upperLeft2.Y + CalcInch(36f));
				e.Graphics.DrawLine(pen, upperLeft2.X, upperLeft2.Y + CalcInch(48f), lowerRight2.X, upperLeft2.Y + CalcInch(48f));
				//e.Graphics.DrawLine(pen, upperLeft2.X+CalcInch(46f), upperLeft2.Y + CalcInch(31f), lowerRight2.X, upperLeft2.Y + CalcInch(31f));
				//e.Graphics.DrawLine(pen, upperLeft2.X + CalcInch(46f), upperLeft2.Y, upperLeft2.X + CalcInch(46f), upperLeft2.Y + CalcInch(31f));
				e.Graphics.DrawLine(pen, upperLeft2.X + CalcInch(46f), upperLeft2.Y, upperLeft2.X + CalcInch(46f), upperLeft2.Y + CalcInch(36f));
				e.Graphics.DrawLine(pen, upperLeft2.X + CalcInch(80f), upperLeft2.Y + CalcInch(48f), upperLeft2.X + CalcInch(80f), lowerRight2.Y);
				
				e.Graphics.DrawImage(_sfLogo, new RectangleF(upperLeft2.X + CalcInch(1f), upperLeft2.Y + CalcInch(1f), CalcInch(44f), CalcInch(8.3f)));

				e.Graphics.DrawImage(barCodeImage, new RectangleF(upperLeft2.X + CalcInch(49f), upperLeft2.Y + CalcInch(2f), CalcInch(45f), CalcInch(8f)));
				//e.Graphics.DrawString("ĸ����", new Font("����", 7f), b, new PointF(upperLeft2.X+CalcInch(9.5f), upperLeft2.Y+CalcInch(11.5f)));
				//e.Graphics.DrawString(NormalizeNumber(_shipmentNumber), new Font("Arial", 9f), b, new PointF(upperLeft2.X+CalcInch(18.5f), upperLeft2.Y+CalcInch(11f)));
				e.Graphics.DrawString(NormalizeNumber(_shipmentNumber), new Font("Arial", 10f), b, new PointF(upperLeft2.X + CalcInch(59f), upperLeft2.Y + CalcInch(11f)));

				e.Graphics.DrawString("��\n�� ", new Font("����", 6f), b, new PointF(CalcInch(1f), upperLeft2.Y + CalcInch(18f)));
				e.Graphics.DrawString(sbSenderInfo.ToString().Trim(), new Font("����", 6f), b, new PointF(CalcInch(5f), upperLeft2.Y + CalcInch(18f)));

				e.Graphics.DrawString("��\n�� ", new Font("����", 6f), b, new PointF(CalcInch(47f), upperLeft2.Y +CalcInch(18f)));
				e.Graphics.DrawString(
					string.Format("{0}\n{1}", sbProvinceCityDistrict.ToString(), sbReceiverInfo.ToString()), 
					new Font("����", 6f), b, new RectangleF(CalcInch(51f), upperLeft2.Y + CalcInch(18f), CalcInch(47f), CalcInch(17f)));
				e.Graphics.DrawString(
					string.Format("�ļ�����: {0}", DateTime.Now.ToString("yyyy/MM/dd")),
					 new Font("����", 6f), b, new PointF(CalcInch(47f), upperLeft2.Y + CalcInch(33f)));
				
				e.Graphics.DrawString("�м���", new Font("����", 6f), b, new PointF(upperLeft2.X + CalcInch(1f), upperLeft2.Y + CalcInch(37f)));
				e.Graphics.DrawString("�̷�", new Font("����", 7f), b, new PointF(upperLeft2.X + CalcInch(1.5f), upperLeft2.Y + CalcInch(41f)));
				e.Graphics.DrawString("��ע", new Font("����", 6f), b, new PointF(upperLeft2.X + CalcInch(1f), upperLeft2.Y + CalcInch(49f)));
				e.Graphics.DrawString(
					string.Format("ת��Э��ͻ�(����){0}",this.HolidayDelivery ? "\n�˼��ڼ�����������, лл!":string.Empty), 
					new Font("����", 8f, FontStyle.Bold), b, new PointF(upperLeft2.X + CalcInch(10f), upperLeft2.Y + CalcInch(49f)));
				e.Graphics.DrawString("���úϼ�:", new Font("����", 6f), b, new PointF(upperLeft2.X + CalcInch(81f), upperLeft2.Y + CalcInch(49f)));

				//string[] itemInfos = SplitItemInfo(_itemInfo);
				float x = 10f;
				//foreach (string s in itemInfos)
				//{
				//    e.Graphics.DrawString(s, new Font("����", 7f), b, new PointF(upperLeft2.X+CalcInch(x), upperLeft2.Y+CalcInch(38f)));
				//    x += 34; //e.Graphics.MeasureString(s, new Font("����", 7f)).Width *15/1440*2.54;
				//}

				StringBuilder sbProducts = new StringBuilder();
				for (int i = 0; i < this.ProductInfos.Length; i++)
				{
					sbProducts.Append(this.ProductInfos[i].Trim());

					if (i == this.ProductInfos.Length - 1 || (i > 0 && i % MAX_PRODUCT_LINES == MAX_PRODUCT_LINES - 1))
					{
						Font f = new Font("����", 7f, FontStyle.Bold);
						e.Graphics.DrawString(sbProducts.ToString(), f, b, new PointF(upperLeft2.X + CalcInch(x), upperLeft2.Y + CalcInch(36.6f)));
						
						x += (e.Graphics.MeasureString(sbProducts.ToString(), f).Width+10) / 4; //32; //e.Graphics.MeasureString(s, new Font("����", 7f)).Width *15/1440*2.54;
						sbProducts = new StringBuilder();
					}
					else
					{
						sbProducts.Append("\n");
					}
				}

				//// ��3��
				PointF upperLeft3 = new PointF(CalcInch(0f), CalcInch(153f));
				PointF lowerRight3 = new PointF(CalcInch(97f), CalcInch(211f));
				SizeF fullSize3 = new SizeF(lowerRight3.X - upperLeft3.X, lowerRight3.Y - upperLeft3.Y);
				e.Graphics.DrawRectangle(pen, upperLeft3.X, upperLeft3.Y, fullSize3.Width, fullSize3.Height);
				e.Graphics.DrawLine(pen, upperLeft3.X, upperLeft3.Y + CalcInch(17f), lowerRight3.X, upperLeft3.Y + CalcInch(17f));
				e.Graphics.DrawLine(pen, upperLeft3.X, upperLeft3.Y + CalcInch(36f), lowerRight3.X, upperLeft3.Y + CalcInch(36f));
				e.Graphics.DrawLine(pen, upperLeft3.X, upperLeft3.Y + CalcInch(48f), lowerRight3.X, upperLeft3.Y + CalcInch(48f));
				//e.Graphics.DrawLine(pen, upperLeft3.X+CalcInch(46f), upperLeft3.Y + CalcInch(31f), lowerRight3.X, upperLeft3.Y + CalcInch(31f));
				//e.Graphics.DrawLine(pen, upperLeft3.X + CalcInch(46f), upperLeft3.Y, upperLeft3.X + CalcInch(46f), upperLeft3.Y + CalcInch(31f));
				e.Graphics.DrawLine(pen, upperLeft3.X + CalcInch(46f), upperLeft3.Y, upperLeft3.X + CalcInch(46f), upperLeft3.Y + CalcInch(36f));
				e.Graphics.DrawLine(pen, upperLeft3.X + CalcInch(80f), upperLeft3.Y + CalcInch(48f), upperLeft3.X + CalcInch(80f), lowerRight3.Y);

				e.Graphics.DrawImage(_sfLogo, new RectangleF(upperLeft3.X + CalcInch(1f), upperLeft3.Y + CalcInch(1f), CalcInch(44f), CalcInch(8.3f)));
				e.Graphics.DrawImage(barCodeImage, new RectangleF(upperLeft3.X + CalcInch(49f), upperLeft3.Y + CalcInch(2f), CalcInch(45f), CalcInch(8f)));
				//e.Graphics.DrawString("ĸ����", new Font("����", 7f), b, new PointF(upperLeft3.X+CalcInch(9.5f), upperLeft3.Y+CalcInch(11.5f)));
				//e.Graphics.DrawString(NormalizeNumber(_shipmentNumber), new Font("Arial", 9f), b, new PointF(upperLeft3.X+CalcInch(18.5f), upperLeft3.Y+CalcInch(11f)));
				e.Graphics.DrawString(NormalizeNumber(_shipmentNumber), new Font("Arial", 10f), b, new PointF(upperLeft3.X + CalcInch(59f), upperLeft3.Y + CalcInch(11f)));

				e.Graphics.DrawString("��\n�� ", new Font("����", 6f), b, new PointF(CalcInch(1f), upperLeft3.Y + CalcInch(18f)));
				e.Graphics.DrawString(sbSenderInfo.ToString().Trim(), new Font("����", 6f), b, new PointF(CalcInch(5f), upperLeft3.Y + CalcInch(18f)));

				e.Graphics.DrawString("��\n�� ", new Font("����", 6f), b, new PointF(CalcInch(47f), upperLeft3.Y + CalcInch(18f)));
				e.Graphics.DrawString(
					string.Format("{0}\n{1}", sbProvinceCityDistrict.ToString(), sbReceiverInfo.ToString()),
					new Font("����", 6f), b, new RectangleF(CalcInch(51f), upperLeft3.Y + CalcInch(18f), CalcInch(47f), CalcInch(17f)));
				e.Graphics.DrawString(
					string.Format("�ļ�����: {0}", DateTime.Now.ToString("yyyy/MM/dd")),
					 new Font("����", 6f), b, new PointF(CalcInch(47f), upperLeft3.Y + CalcInch(33f)));

				e.Graphics.DrawString("�м���", new Font("����", 6f), b, new PointF(upperLeft3.X + CalcInch(1f), upperLeft3.Y + CalcInch(37f)));
				e.Graphics.DrawString("�̷�", new Font("����", 7f), b, new PointF(upperLeft3.X + CalcInch(1.5f), upperLeft3.Y + CalcInch(41f)));
				e.Graphics.DrawString("��ע", new Font("����", 6f), b, new PointF(upperLeft3.X + CalcInch(1f), upperLeft3.Y + CalcInch(49f)));
				e.Graphics.DrawString("ת��Э��ͻ�(����)\n�˼��ڼ�����������, лл!", new Font("����", 8f, FontStyle.Bold), b, new PointF(upperLeft3.X + CalcInch(10f), upperLeft3.Y + CalcInch(49f)));
				e.Graphics.DrawString("���úϼ�:", new Font("����", 6f), b, new PointF(upperLeft3.X + CalcInch(81f), upperLeft3.Y + CalcInch(49f)));

				//string[] itemInfos = SplitItemInfo(_itemInfo);
				x = 10f;
				//foreach (string s in itemInfos)
				//{
				//    e.Graphics.DrawString(s, new Font("����", 7f), b, new PointF(upperLeft3.X+CalcInch(x), upperLeft3.Y+CalcInch(38f)));
				//    x += 34; //e.Graphics.MeasureString(s, new Font("����", 7f)).Width *15/1440*2.54;
				//}

				for (int i = 0; i < this.ProductInfos.Length; i++)
				{
					sbProducts.Append(this.ProductInfos[i].Trim());

					if (i == this.ProductInfos.Length - 1 || (i > 0 && i % MAX_PRODUCT_LINES == MAX_PRODUCT_LINES - 1))
					{
						Font f = new Font("����", 7f, FontStyle.Bold);
						e.Graphics.DrawString(sbProducts.ToString(), f, b, new PointF(upperLeft3.X + CalcInch(x), upperLeft3.Y + CalcInch(36.6f)));

						x += (e.Graphics.MeasureString(sbProducts.ToString(), f).Width + 10) / 4; //32; //e.Graphics.MeasureString(s, new Font("����", 7f)).Width *15/1440*2.54;
						sbProducts = new StringBuilder();
					}
					else
					{
						sbProducts.Append("\n");
					}
				}
			}
			*/
			
			// 2015.09.10��ʼ���浥.
			// ֻ��2��, �����������, ��ӡֽ����˳��logo.
			protected override void OnPrintPage(object sender, PrintPageEventArgs e)
			{
				BarCode.Code128 code128 = new BarCode.Code128();
				code128.Height = (uint)CalcInch(10f);
				Image barCodeImage = code128.GetCodeImage(_shipmentNumber, BarCode.Code128.Encode.Code128C);

				StringBuilder sbSenderInfo = new StringBuilder();
				if (!string.IsNullOrEmpty(this.SenderName))
					sbSenderInfo.Append(this.SenderName);
				if (!string.IsNullOrEmpty(this.SenderAd))
				{
					if (sbSenderInfo.Length > 0)
						sbSenderInfo.Append(", ");
						sbSenderInfo.Append(this.SenderAd);
				}
				if (!string.IsNullOrEmpty(this.SenderTel))
				{
					if (sbSenderInfo.Length > 0)
						sbSenderInfo.Append(", ");
					sbSenderInfo.Append(this.SenderTel);
				}

				StringBuilder sbProvinceCityDistrict = new StringBuilder();
				sbProvinceCityDistrict.Append(this.Province);
				if (!string.IsNullOrEmpty(this.City1))
					sbProvinceCityDistrict.Append("," + this.City1);
				if (!string.IsNullOrEmpty(this.City2))
					sbProvinceCityDistrict.Append("," + this.City2);
				if (!string.IsNullOrEmpty(this.District))
					sbProvinceCityDistrict.Append("," + this.District);

				StringBuilder sbReceiverInfo = new StringBuilder();
				sbReceiverInfo.Append(this.StreetAddress);
				sbReceiverInfo.Append("\n");
				sbReceiverInfo.Append(this.RecipientName);
				if (this.DisplayBuyerAccount)
					sbReceiverInfo.Append(string.Format("({0})", this.BuyerAccount));
				if (!string.IsNullOrEmpty(this.Mobile))
					sbReceiverInfo.Append(" " + this.Mobile + ",");
				if (!string.IsNullOrEmpty(this.Phone))
					sbReceiverInfo.Append(this.Phone);
				if (sbReceiverInfo.ToString().EndsWith(","))
					sbReceiverInfo.Remove(sbReceiverInfo.Length - 1, 1);

				PointF upperLeft = new PointF(CalcInch(0f), CalcInch(0f));
				PointF lowerRight = new PointF(CalcInch(97f), CalcInch(95f));
				SizeF fullSize = new SizeF(lowerRight.X - upperLeft.X, lowerRight.Y - upperLeft.Y);

				Pen pen = new Pen(Color.Black, 1f);
				Pen thickPen = new Pen(Color.Black, 2f);
				e.Graphics.DrawRectangle(pen, upperLeft.X, upperLeft.Y, fullSize.Width, fullSize.Height);

				e.Graphics.DrawLine(pen, upperLeft.X, upperLeft.Y + CalcInch(24f), lowerRight.X, upperLeft.Y + CalcInch(24f));
				e.Graphics.DrawLine(pen, upperLeft.X, upperLeft.Y + CalcInch(30f), lowerRight.X, upperLeft.Y + CalcInch(30f));
				e.Graphics.DrawLine(pen, upperLeft.X, upperLeft.Y + CalcInch(50f), lowerRight.X, upperLeft.Y + CalcInch(50f));
				e.Graphics.DrawLine(pen, upperLeft.X, upperLeft.Y + CalcInch(64f), lowerRight.X, upperLeft.Y + CalcInch(64f));
				e.Graphics.DrawLine(pen, upperLeft.X, upperLeft.Y + CalcInch(72f), lowerRight.X, upperLeft.Y + CalcInch(72f));
				//e.Graphics.DrawLine(pen, upperLeft.X + CalcInch(60f), upperLeft.Y + CalcInch(76f), lowerRight.X, upperLeft.Y + CalcInch(76f));
				e.Graphics.DrawLine(pen, upperLeft.X, upperLeft.Y + CalcInch(84f), lowerRight.X, upperLeft.Y + CalcInch(84f));
				e.Graphics.DrawLine(pen, upperLeft.X + CalcInch(27f), upperLeft.Y, upperLeft.X + CalcInch(27f), upperLeft.Y + CalcInch(24f));
				e.Graphics.DrawLine(pen, upperLeft.X + CalcInch(76f), upperLeft.Y, upperLeft.X + CalcInch(76f), upperLeft.Y + CalcInch(50f));
				e.Graphics.DrawLine(pen, upperLeft.X + CalcInch(60f), upperLeft.Y + CalcInch(64f), upperLeft.X + CalcInch(60f), upperLeft.Y + CalcInch(84f));
				e.Graphics.DrawLine(pen, upperLeft.X + CalcInch(27f), upperLeft.Y+CalcInch(72f), upperLeft.X + CalcInch(27f), upperLeft.Y + CalcInch(84f));
				//e.Graphics.DrawLine(pen, upperLeft.X + CalcInch(66f), upperLeft.Y + CalcInch(53f), upperLeft.X + CalcInch(66f), upperLeft.Y + CalcInch(74f));
				//e.Graphics.DrawLine(pen, upperLeft.X + CalcInch(66f), upperLeft.Y + CalcInch(64f), lowerRight.X, upperLeft.Y + CalcInch(64f));
				//e.Graphics.DrawLine(pen, upperLeft.X + CalcInch(53f), upperLeft.Y + CalcInch(74f), upperLeft.X + CalcInch(53f), lowerRight.Y);
				//e.Graphics.DrawLine(pen, upperLeft.X + CalcInch(75f), upperLeft.Y + CalcInch(74f), upperLeft.X + CalcInch(75f), lowerRight.Y);

				SolidBrush b = new SolidBrush(Color.Black);

				e.Graphics.DrawImage(_sfLogo, new RectangleF(upperLeft.X + CalcInch(3f), upperLeft.Y + CalcInch(4f), CalcInch(22f), CalcInch(16.6f)));
				e.Graphics.DrawImage(barCodeImage, new RectangleF(CalcInch(32f), CalcInch(3f), CalcInch(40f), CalcInch(15f)));
				e.Graphics.DrawString(NormalizeNumber(_shipmentNumber), new Font("Arial", 9f), b, new PointF(CalcInch(40f), CalcInch(19f)));
				e.Graphics.DrawString("˳���ػ�", new Font("����", 12f), b, new PointF(CalcInch(77f), CalcInch(10f)));

				e.Graphics.DrawString("�ķ�: ", new Font("����", 8f), b, new PointF(CalcInch(2f), CalcInch(25.5f)));
				e.Graphics.DrawString(sbSenderInfo.ToString().Trim(), new Font("����", 6f), b, new PointF(CalcInch(10f), CalcInch(26f)));
				e.Graphics.DrawString("ԭ�ĵ� ", new Font("����", 6f), b, new PointF(CalcInch(77f), CalcInch(26f)));
				e.Graphics.DrawString("021", new Font("Arial", 12f), b, new PointF(CalcInch(85f), CalcInch(25f)));

				e.Graphics.DrawString("�շ�: ", new Font("����", 8f), b, new PointF(CalcInch(2f), CalcInch(31.5f)));
				if (this.IsPickup)
					e.Graphics.DrawString("(��ȡ)", new Font("����", 8f), b, new PointF(CalcInch(0.5f), CalcInch(36f)));
				e.Graphics.DrawString(sbProvinceCityDistrict.ToString(), new Font("����", 10f), b, new PointF(CalcInch(10f), CalcInch(31f)));
				e.Graphics.DrawString(sbReceiverInfo.ToString(), new Font("����", 8f), b, new RectangleF(CalcInch(10f), CalcInch(35f), CalcInch(67f), CalcInch(40f)));
				e.Graphics.DrawString("Ŀ�ĵ� ", new Font("����", 6f), b, new PointF(CalcInch(77f), CalcInch(32f)));
				e.Graphics.DrawString(this.DestCode, new Font("Arial Bold", 20f), b, new PointF(CalcInch(78f), CalcInch(35f)));

				e.Graphics.DrawString("�м���", new Font("����", 8f), b, new PointF(upperLeft.X + CalcInch(1f), upperLeft.Y + CalcInch(51f)));
				e.Graphics.DrawString("�̷�", new Font("����", 7f), b, new PointF(upperLeft.X + CalcInch(2.5f), upperLeft.Y + CalcInch(55f)));

				float x = 11f;
				StringBuilder sbProducts = new StringBuilder();
				for (int i = 0; i < this.ProductInfos.Length; i++)
				{
					sbProducts.Append(this.ProductInfos[i].Trim());

					if (i == this.ProductInfos.Length - 1 || (i > 0 && i % MAX_PRODUCT_LINES == MAX_PRODUCT_LINES - 1))
					{
						Font f = new Font("����", 8f, FontStyle.Bold);
						e.Graphics.DrawString(sbProducts.ToString(), f, b, new PointF(upperLeft.X + CalcInch(x), upperLeft.Y + CalcInch(51f)));
						
						x += (e.Graphics.MeasureString(sbProducts.ToString(), f).Width+10) / 4; //32; //e.Graphics.MeasureString(s, new Font("����", 7f)).Width *15/1440*2.54;
						sbProducts = new StringBuilder();
					}
					else
					{
						sbProducts.Append("\n");
					}
				}

				string payment = "���ʽ:";
				payment += (this.IsFreightCollect ? "����" : "�ĸ��½�");
				payment += (this.IsFreightCollect ? string.Empty : "  �½��˺�:0218687063");
				e.Graphics.DrawString(payment, new Font("����", 6f), b, new PointF(CalcInch(2f), CalcInch(65f)));
				e.Graphics.DrawString("ʵ������:          �Ʒ�����:", new Font("����", 6f), b, new PointF(CalcInch(2f), CalcInch(69f)));
				e.Graphics.DrawString("�˷�:\n���úϼ�:", new Font("����", 9f), b, new PointF(CalcInch(60f), CalcInch(65f)));

				e.Graphics.DrawString("�ռ�Ա:", new Font("����", 6f), b, new PointF(CalcInch(2f), CalcInch(73f)));
				//e.Graphics.DrawString("488133", new Font("Arial", 6f), b, new PointF(CalcInch(63.2f), CalcInch(76f)));
				e.Graphics.DrawString("�ļ�����:", new Font("����", 6f), b, new PointF(CalcInch(2f), CalcInch(76.5f)));
				e.Graphics.DrawString(DateTime.Now.ToString("yyyy/MM/dd"), new Font("Arial", 6f), b, new PointF(CalcInch(12f), CalcInch(76.5f)));
				e.Graphics.DrawString("�ɼ�Ա:", new Font("����", 6f), b, new PointF(CalcInch(30f), CalcInch(73f)));
				e.Graphics.DrawString("�ɼ�����:", new Font("����", 6f), b, new PointF(CalcInch(30f), CalcInch(76.5f)));
				e.Graphics.DrawString("�շ�ǩ��", new Font("����", 7f), b, new PointF(CalcInch(60f), CalcInch(73f)));
				e.Graphics.DrawString("����:   ��    ��", new Font("����", 7f, FontStyle.Italic), b, new PointF(CalcInch(60f), CalcInch(80f)));

				e.Graphics.DrawString("���ӷ���:", new Font("����", 6f), b, new PointF(upperLeft.X + CalcInch(2f), upperLeft.Y + CalcInch(85f)));
				e.Graphics.DrawString(
					string.Format("ת��Э��ͻ�(����){0}", this.HolidayDelivery ? "\n�˼��ڼ�����������, лл!" : string.Empty),
					new Font("����", 8f, FontStyle.Bold), b, new PointF(upperLeft.X + CalcInch(2f), upperLeft.Y + CalcInch(88f)));

				// ��2��
				PointF upperLeft2 = new PointF(CalcInch(0f), CalcInch(99f));
				PointF lowerRight2 = new PointF(CalcInch(97f), CalcInch(133f));
				SizeF fullSize2 = new SizeF(lowerRight2.X - upperLeft2.X, lowerRight2.Y - upperLeft2.Y);
				e.Graphics.DrawRectangle(pen, upperLeft2.X, upperLeft2.Y, fullSize2.Width, fullSize2.Height);
				e.Graphics.DrawLine(pen, upperLeft2.X, upperLeft2.Y + CalcInch(5f), lowerRight2.X, upperLeft2.Y + CalcInch(5f));
				e.Graphics.DrawLine(pen, upperLeft2.X, upperLeft2.Y + CalcInch(20f), lowerRight2.X, upperLeft2.Y + CalcInch(20f));
				e.Graphics.DrawLine(pen, upperLeft2.X + CalcInch(60f), upperLeft2.Y + CalcInch(27f), lowerRight2.X, upperLeft2.Y + CalcInch(27f));
				e.Graphics.DrawLine(pen, upperLeft.X + CalcInch(60f), upperLeft2.Y+CalcInch(20f), upperLeft.X + CalcInch(60f), lowerRight2.Y);

				e.Graphics.DrawString("�ķ�: ", new Font("����", 8f), b, new PointF(CalcInch(2f), upperLeft2.Y + CalcInch(1f)));
				e.Graphics.DrawString(sbSenderInfo.ToString().Trim(), new Font("����", 6f), b, new PointF(CalcInch(10f), upperLeft2.Y + CalcInch(1f)));

				e.Graphics.DrawString("�շ�: ", new Font("����", 8f), b, new PointF(CalcInch(2f), upperLeft2.Y + CalcInch(6f)));
				if (this.IsPickup)
					e.Graphics.DrawString("(��ȡ)", new Font("����", 8f), b, new PointF(CalcInch(0.5f), upperLeft2.Y + CalcInch(10f)));
				e.Graphics.DrawString(sbProvinceCityDistrict.ToString(), new Font("����", 10f), b, new PointF(CalcInch(10f), upperLeft2.Y + CalcInch(6f)));
				e.Graphics.DrawString(sbReceiverInfo.ToString(), new Font("����", 8f), b, new RectangleF(CalcInch(10f), upperLeft2.Y + CalcInch(10f), CalcInch(88f), CalcInch(15f)));

				e.Graphics.DrawImage(barCodeImage, new RectangleF(CalcInch(8f), upperLeft2.Y + CalcInch(21f), CalcInch(40f), CalcInch(9f)));
				e.Graphics.DrawString(NormalizeNumber(_shipmentNumber), new Font("Arial", 9f), b, new PointF(CalcInch(16f), upperLeft2.Y + CalcInch(30f)));

				e.Graphics.DrawString("���úϼ�:", new Font("����", 9f), b, new PointF(CalcInch(61f), upperLeft2.Y + CalcInch(21f)));
				e.Graphics.DrawString("���ʽ:", new Font("����", 9f), b, new PointF(CalcInch(61f), upperLeft2.Y + CalcInch(28f)));
				e.Graphics.DrawString((this.IsFreightCollect ? "����" : "�ĸ�"), new Font("����", 9f), b, new PointF(CalcInch(76f), upperLeft2.Y + CalcInch(28f)));

				return;

				//����ĸ�������ӡ.
				//e.Graphics.DrawString("1/1", new Font("Arial", 10f), b, new PointF(CalcInch(5f), CalcInch(44.5f)));
				//e.Graphics.DrawString("ĸ����", new Font("����", 7f), b, new PointF(CalcInch(19f), CalcInch(53f)));
				//e.Graphics.DrawString(NormalizeNumber(_shipmentNumber), new Font("Arial", 9f), b, new PointF(CalcInch(28f), CalcInch(52.5f)));


				// ��2��
				/*
				PointF upperLeft2 = new PointF(CalcInch(0f), CalcInch(91f));
				PointF lowerRight2 = new PointF(CalcInch(97f), CalcInch(149f));
				SizeF fullSize2 = new SizeF(lowerRight2.X - upperLeft2.X, lowerRight2.Y - upperLeft2.Y);
				e.Graphics.DrawRectangle(pen, upperLeft2.X, upperLeft2.Y, fullSize2.Width, fullSize2.Height);
				e.Graphics.DrawLine(pen, upperLeft2.X, upperLeft2.Y + CalcInch(17f), lowerRight2.X, upperLeft2.Y + CalcInch(17f));
				e.Graphics.DrawLine(pen, upperLeft2.X, upperLeft2.Y + CalcInch(36f), lowerRight2.X, upperLeft2.Y + CalcInch(36f));
				e.Graphics.DrawLine(pen, upperLeft2.X, upperLeft2.Y + CalcInch(48f), lowerRight2.X, upperLeft2.Y + CalcInch(48f));
				//e.Graphics.DrawLine(pen, upperLeft2.X+CalcInch(46f), upperLeft2.Y + CalcInch(31f), lowerRight2.X, upperLeft2.Y + CalcInch(31f));
				//e.Graphics.DrawLine(pen, upperLeft2.X + CalcInch(46f), upperLeft2.Y, upperLeft2.X + CalcInch(46f), upperLeft2.Y + CalcInch(31f));
				e.Graphics.DrawLine(pen, upperLeft2.X + CalcInch(46f), upperLeft2.Y, upperLeft2.X + CalcInch(46f), upperLeft2.Y + CalcInch(36f));
				e.Graphics.DrawLine(pen, upperLeft2.X + CalcInch(80f), upperLeft2.Y + CalcInch(48f), upperLeft2.X + CalcInch(80f), lowerRight2.Y);
				
				e.Graphics.DrawImage(barCodeImage, new RectangleF(upperLeft2.X + CalcInch(49f), upperLeft2.Y + CalcInch(2f), CalcInch(45f), CalcInch(8f)));
				//e.Graphics.DrawString("ĸ����", new Font("����", 7f), b, new PointF(upperLeft2.X+CalcInch(9.5f), upperLeft2.Y+CalcInch(11.5f)));
				//e.Graphics.DrawString(NormalizeNumber(_shipmentNumber), new Font("Arial", 9f), b, new PointF(upperLeft2.X+CalcInch(18.5f), upperLeft2.Y+CalcInch(11f)));
				e.Graphics.DrawString(NormalizeNumber(_shipmentNumber), new Font("Arial", 10f), b, new PointF(upperLeft2.X + CalcInch(59f), upperLeft2.Y + CalcInch(11f)));

				e.Graphics.DrawString("��\n�� ", new Font("����", 6f), b, new PointF(CalcInch(1f), upperLeft2.Y + CalcInch(18f)));
				e.Graphics.DrawString(sbSenderInfo.ToString().Trim(), new Font("����", 6f), b, new PointF(CalcInch(5f), upperLeft2.Y + CalcInch(18f)));

				e.Graphics.DrawString("��\n�� ", new Font("����", 6f), b, new PointF(CalcInch(47f), upperLeft2.Y +CalcInch(18f)));
				e.Graphics.DrawString(
					string.Format("{0}\n{1}", sbProvinceCityDistrict.ToString(), sbReceiverInfo.ToString()), 
					new Font("����", 6f), b, new RectangleF(CalcInch(51f), upperLeft2.Y + CalcInch(18f), CalcInch(47f), CalcInch(17f)));
				e.Graphics.DrawString(
					string.Format("�ļ�����: {0}", DateTime.Now.ToString("yyyy/MM/dd")),
					 new Font("����", 6f), b, new PointF(CalcInch(47f), upperLeft2.Y + CalcInch(33f)));
				
				e.Graphics.DrawString("��ע", new Font("����", 6f), b, new PointF(upperLeft2.X + CalcInch(1f), upperLeft2.Y + CalcInch(49f)));
				e.Graphics.DrawString(
					string.Format("ת��Э��ͻ�(����){0}",this.HolidayDelivery ? "\n�˼��ڼ�����������, лл!":string.Empty), 
					new Font("����", 8f, FontStyle.Bold), b, new PointF(upperLeft2.X + CalcInch(10f), upperLeft2.Y + CalcInch(49f)));
				e.Graphics.DrawString("���úϼ�:", new Font("����", 6f), b, new PointF(upperLeft2.X + CalcInch(81f), upperLeft2.Y + CalcInch(49f)));
				 */

			}
			
			// ÿ3λ�м����ո�
			private static string NormalizeNumber(string number)
			{
				string s = number;
				int i = 3;
				while (i < number.Length)
				{
					s = s.Insert(i, " ");
					i += 4;
				}
				return s;
			}

			// 1�����ֿ����MAX_LINES��, ��MAX_LINES-1������.
			private string[] SplitItemInfo(string itemInfo)
			{
				const int MAX_LINES = 7;
			
				string[] lines = itemInfo.Split(new char[]{'\n'});
				if (lines.Length <= MAX_LINES)
					return new string[]{itemInfo};
					
				string[] result = new string[(int)Math.Ceiling((float)lines.Length/(float)MAX_LINES)];

				for (int i = 0; i <= Math.Ceiling((float)lines.Length/(float)MAX_LINES); i++)
				{
					for (int j = i*MAX_LINES; j<=i*MAX_LINES+(MAX_LINES-1); j++)
					{
						if (j >= lines.Length)
							break;
						result[i] += lines[j] + "\n";
					}
				}
				return result;
			}
		}
		#endregion
		
		#region class SfOrder
		private class SfOrder : BillInfo
		{
			//sample
//            private const string ORDER_SERVICE = 
//                @"<Request service='OrderService' lang='zh-CN'>
//				<Head>CHEN,kbDwqIn0VezhsOlh</Head><Body>
//				<Order orderid='909002198628829'
//							  express_type='2'
//							  j_company='�¹�e��'
//							  j_contact='������'
//							  j_tel=''
//							  j_mobile='18964913317'
//							  j_province='�Ϻ���'
//							  j_city='�Ϻ���'
//							  j_county='������'
//							  j_address='ݷ��·'
//							  d_company='˳������'
//							  d_contact='�'
//							  d_tel='0632-3982127'
//							  d_mobile='13306375506'
//							  d_province='ɽ��ʡ'
//							  d_city='��ׯ��'
//							  d_county='������'
//							  d_address='ɽ��ʡ ��ׯ�� ������ ��ű�·��۽ֶ���¥�����ŷ�Դ����'
//							  parcel_quantity='1'
//							  pay_method='1'
//							  custid='0218687063'
//							  cargo_total_weight='0'
//							  sendstarttime='2014-12-26 12:07:11'
//							  remark='' >
//						<Cargo name='�̷�' count='1' unit='��' weight='1' amount='1' currency='CNY' source_area='�й�'></Cargo>
//					</Order>
//				</Body>
//				</Request>";

			// code for testing interface.
			// 	<Head>CHEN,kbDwqIn0VezhsOlh</Head><Body>
			private const string ORDER_SERVICE = 
				@"<Request service='OrderService' lang='zh-CN'>
				<Head>ckkdzj,5njOCXQuRi3ig2rejXGKEyZgczyIAASU</Head><Body>
				<Order orderid='{0}'
							  express_type='2'
							  j_company='�¹�e��'
							  j_contact='������'
							  j_tel=''
							  j_mobile='18964913317'
							  j_province='�Ϻ�'
							  j_city='�Ϻ�'
							  j_county='������'
							  j_address='ݷ��·'
							  d_company=''
							  d_contact='{1}'
							  d_tel='{2}'
							  d_mobile='{3}'
							  d_province='{4}'
							  d_city='{5}'
							  d_county='{6}'
							  d_address='{7}'
							  parcel_quantity='1'
							  pay_method='1'
							  custid='0218687063'
							  cargo_total_weight='0'
							  sendstarttime='{8}'
							  remark='' >
						<Cargo name='�̷�' count='1' unit='��' weight='1' amount='1' currency='CNY' source_area='�й�'></Cargo>
					</Order>
				</Body>
				</Request>";

			 // sample
//            private const string ORDER_FILTER_SERVICE = 
//                @"<Request service='OrderFilterService'  lang='zh-CN'>
//				<Head>CHEN,kbDwqIn0VezhsOlh</Head> 
//				<Body>
//				<OrderFilter filter_type='1'
//						   orderid='909002198628829'
//						   d_address='ɽ��ʡ ��ׯ�� ������ ��ű�·��۽ֶ���¥�����ŷ�Դ����' >
//				<OrderFilterOption j_tel='18964913317'
//						   j_address='�Ϻ���������'
//						   d_tel='13306375506'
//						   j_custid='0218687063' />
//				</OrderFilter>
//				</Body>
//				</Request>";
			private const string ORDER_FILTER_SERVICE = 
				@"<Request service='OrderFilterService'  lang='zh-CN'>
				<Head>ckkdzj,5njOCXQuRi3ig2rejXGKEyZgczyIAASU</Head> 
				<Body>
				<OrderFilter filter_type='1'
						   orderid='{0}'
						   d_address='{1}' >
				<OrderFilterOption j_tel='18964913317'
						   j_address='�Ϻ���������'
						   d_tel='18964913317'
						   j_custid='0218687063' />
				</OrderFilter>
				</Body>
				</Request>";
			
			private string _destCode;
			private string _mailNumber;
			private string _errorCode;
			private string _errorMessage;
			
			public string DestCode
			{
				get { return _destCode; }
			}
			
			public string MailNumber
			{
				get { return _mailNumber; }
			}

			public string ErrorCode
			{
				get { return _errorCode; }
			}

			public string ErrorMessage
			{
				get { return _errorMessage; }
			}

			public void RunOrderService(string orderId)
			{
				string province = this.Province.Trim();
				if (province.EndsWith("��")) //4��ֱϽ������"��"��Ҫ.
					province = province.Remove(province.Length - 1, 1);
				
				string request = string.Format(
					ORDER_SERVICE, 
					orderId+DateTime.Now.Millisecond.ToString(), 
					this.RecipientName, this.Phone, this.Mobile,
					province, this.City1, string.IsNullOrEmpty(this.City2) ? this.District : this.City2,
					string.Format("{0} {1} {2} {3} {4}", this.Province, this.City1, this.City2, this.District, this.StreetAddress),
					DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				com.sf_express.test.bsp_oisp.CommonServiceService sf = new Egode.com.sf_express.test.bsp_oisp.CommonServiceService();
				
				string response = string.Empty;
				try
				{
					response = sf.sfexpressService(request);
				}
				catch (Exception ex)
				{
					Trace.WriteLine(ex);
					
					_errorCode = "1001";
					_errorMessage = ex.Message;
					return;
				}

				// response sample.
				//<Response service="OrderService"><Head>OK</Head><Body><OrderResponse filter_result="2" destcode="755"mailno="966996868270"
				//origincode="010" orderid="WSD1306223330001"/></Body>
				//</Response>
				System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
				xmldoc.LoadXml(response);
				
				// check error.
				System.Xml.XmlNode nodeError = xmldoc.SelectSingleNode(".//ERROR");
				if (null == nodeError)
				{
					_errorCode = string.Empty;
					_errorMessage = string.Empty;
					System.Xml.XmlNode node = xmldoc.SelectSingleNode(".//OrderResponse");
					Trace.Assert(null != node);
					
					if (null != node.Attributes.GetNamedItem("destcode"))
						_destCode = node.Attributes.GetNamedItem("destcode").Value;
					if (null != node.Attributes.GetNamedItem("mailno"))
						_mailNumber = node.Attributes.GetNamedItem("mailno").Value;
				}
				else
				{
					_errorCode = nodeError.Attributes.GetNamedItem("code").Value;
					_errorMessage = nodeError.InnerText;
				}
			}
			
			public bool Arrivable(string orderId)
			{
				string province = this.Province.Trim();
				if (province.EndsWith("��")) //4��ֱϽ������"��"��Ҫ.
					province = province.Remove(province.Length - 1, 1);
				string request = string.Format(
					ORDER_FILTER_SERVICE, 
					orderId,
					string.Format("{0} {1} {2} {3} {4}", province, this.City1, this.City2, this.District, this.StreetAddress));

				com.sf_express.test.bsp_oisp.CommonServiceService sf = new Egode.com.sf_express.test.bsp_oisp.CommonServiceService();
				string response = string.Empty;
				try
				{
					response = sf.sfexpressService(request);
				}
				catch (Exception ex)
				{
					Trace.WriteLine(ex);
					
					_errorCode = "1001";
					_errorMessage = ex.Message;
					return false;
				}
				//System.Diagnostics.Trace.WriteLine(result);

				// response sample.
				//<Response service="OrderService"><Head>OK</Head><Body><OrderResponse filter_result="2" destcode="755"mailno="966996868270"
				//origincode="010" orderid="WSD1306223330001"/></Body>
				//</Response>
				System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
				xmldoc.LoadXml(response);
				
				// check error.
				System.Xml.XmlNode nodeError = xmldoc.SelectSingleNode(".//ERROR");
				if (null == nodeError)
				{
					_errorCode = string.Empty;
					_errorMessage = string.Empty;
					System.Xml.XmlNode node = xmldoc.SelectSingleNode(".//OrderFilterResponse");
					Trace.Assert(null != node);
					return node.Attributes.GetNamedItem("filter_result").Value.Equals("2");
				}
				else
				{
					_errorCode = nodeError.Attributes.GetNamedItem("code").Value;
					_errorMessage = nodeError.InnerText;
				}
				return false;
			}
		}
		#endregion
	
		private List<Order> _orders;
		List<SoldProductInfo> _soldProductInfos;
		private bool _productDescChanged;
		private bool _productInfoChanged; // Added by KK on 2015/05/24.
		private int _manualStockoutCount = 0;
	
		//private static Image _ytoSeal;
		
		private Timer _tmrFlashSf;
		private Timer _tmrFlashYto;
		
		public StockoutForm(List<Order> orders)
		{
			_orders = orders;
			//ResourceManager rm = new ResourceManager("Egode.Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());
			//_ytoSeal = (Image)rm.GetObject("yto_seal");
			InitializeComponent();
			
			chkSfOldBill.Checked = DateTime.Now < new DateTime(2015, 4, 1, 0, 0, 0);
		}

		private void StockoutForm_Load(object sender, EventArgs e)
		{
			if (_orders.Count > 1)
				this.Text += string.Format(" [{0}�������ϲ�����]", _orders.Count);

			// Sender >>>>
			Distributor distributor = Distributor.Match(_orders[0].BuyerAccount.Trim());
			if (null == distributor)
			{
				txtSenderName.Text = ShopProfile.Current.DisplayNameOnBill;// "�� �� e ��";
				txtSenderAd.Text = ShopProfile.Current.Url;//"http://egode.taobao.com";
				txtSenderTel.Text = ShopProfile.Current.Phone;//"18964913317";
			}
			else
			{
				lblDistributorFlag.Visible = true;
				txtSenderName.Text = distributor.Name;
				txtSenderAd.Text = distributor.Ad;
				txtSenderTel.Text = distributor.Tel;
			}
			// Sender <<<<

			txtBuyerAccount.Text = _orders[0].BuyerAccount.Trim();

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
				txtPhone.Text = _orders[0].PhoneNumber.Trim();
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
				//MessageBox.Show(this, "��Ҫ�ֶ������ַ!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			// Product info.
			int cMustSendFromDe = 0, cBonded = 0;
			_soldProductInfos = GetProducts(_orders, out cMustSendFromDe, out cBonded);
			foreach (SoldProductInfo spi in _soldProductInfos)
				txtProducts.Text += string.Format("{0} x {1}{2}\r\n", spi.ShortName, spi.Count, string.IsNullOrEmpty(spi.Comment) ? string.Empty : (" " + spi.Comment));
			if (txtProducts.Text.Length > 2 && txtProducts.Text.EndsWith("\r\n"))
				txtProducts.Text = txtProducts.Text.Remove(txtProducts.Text.Length - 2, 2); // remove return at the end of string.
			txtProducts.TextChanged += new EventHandler(txtProducts_TextChanged);
			
			foreach (SoldProductInfo spi in _soldProductInfos)
			{
				SoldProductInfoControl spic = new SoldProductInfoControl(spi, false);
				spic.OnRemove += new EventHandler(spic_OnRemove);
				spic.OnProductChanged += new EventHandler(spic_OnProductChanged);
				spic.OnCountChanged += new EventHandler(spic_OnCountChanged);
				pnlProductList.Controls.Add(spic);
				pnlProductList.Controls.SetChildIndex(spic, pnlProductList.Controls.IndexOf(tsAddProduct));
				spic.Margin = new Padding(3, 2, 3, 0);
			}
			//
			
			float money = 0;
			foreach (Order o in _orders)
				money += o.TotalMoney;
			
			lblMoney.Text = money.ToString("0.00");

			//Bitmap bmp = new Bitmap(1200, 600);//picExpressBill.BackgroundImage.Width+100, picExpressBill.BackgroundImage.Height+100);
			//Graphics g = Graphics.FromImage(bmp);
			//DrawBill(g, true);
			//picExpressBill.Image = bmp;

			LayoutControls();

			txtBillNumber.Focus();

			this.Activated += new EventHandler(StockoutForm_Activated);
			
			//pnlMoney.Location = new Point(pnlPrint.Left + 15, pnlPrint.Top + 190);
			//lblMoney.Location = new Point(612-lblMoney.Width, 232);
		}

		void StockoutForm_Activated(object sender, EventArgs e)
		{
			txtBillNumber.Focus();
		}
		
		private void ParseEditedAddress()
		{
			try
			{
				string[] infos = _orders[0].EditedRecipientAddress.Split(',');
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
			catch (Exception ex)
			{
				MessageBox.Show(
					this,
					string.Format("�����µ�ַ����! ������ϵKK~\n{0}\n{1}", _orders[0].EditedRecipientAddress, ex.ToString()),
					this.Text,
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		void txtProducts_TextChanged(object sender, EventArgs e)
		{
			_productDescChanged = true;
		}

		private void StockoutForm_Shown(object sender, EventArgs e)
		{
			//if (_orders[0].Remark.Contains("˳��") || _orders[0].Remark.Contains("˳��") || _orders[0].Remark.Contains("Բͨ"))
			//{
			//    MessageBox.Show(
			//        this, 
			//        "����ָ�����! \n������Ҫ�ֶ�ѡ���ݹ�˾.", this.Text,
			//        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//}

			//lblSfReason.Text = string.Empty;
			//if (_orders[0].Remark.Contains("˳��"))
			//{
			//    //string prompt = string.Format("�˶���������Ҫ��˳��.\n�ͷ���ע: {0}\n\n�Ƿ�˳��?\n", _orders[0].Remark);
			//    //DialogResult dr = MessageBox.Show(this, prompt, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			//    rdoSf.Checked = true;
			//    lblSfReason.Text = "(�ͷ���ע)";
			//    lblSfReason.ForeColor = Color.DodgerBlue;
			//}
			//else if (!IsJiangZheHu())
			//{
			//    int c = GetProductCount();

			//    if (c >= 4)
			//    {
			//        rdoSf.Checked = true;
			//        lblSfReason.Text = "(˳�����)";
			//        lblSfReason.ForeColor = Color.BlueViolet;
			//    }
			//    else if (c >= 2 && IsFarProvince())
			//    {
			//        rdoSf.Checked = true;
			//        lblSfReason.Text = "(�ʷѽ���)";
			//        lblSfReason.ForeColor = Color.DarkGreen;
			//    }
			//}
			
			bool sfFreightCollect = false, sfPickup = false;
			bool sf = RemarkContainsSf(out sfFreightCollect, out sfPickup);
			bool yto = RemarkContainsYto();
			
			if (sf && yto)
			{
				rdoYto.Checked = false;
				rdoSf.Checked = false;
			}
			else
			{
				if (sf)
				{
					rdoSf.Checked = true;
					chkFriehghtCollect.Checked = sfFreightCollect;
					chkPickup.Checked = sfPickup;
					_tmrFlashSf = new Timer();
					_tmrFlashSf.Interval = 300;
					_tmrFlashSf.Tick += new EventHandler(_tmrFlashSf_Tick);
					_tmrFlashSf.Tag = true;
					_tmrFlashSf.Start();
				}
				else if (yto)
				{
					rdoYto.Checked = true;
					_tmrFlashYto = new Timer();
					_tmrFlashYto.Interval = 300;
					_tmrFlashYto.Tick += new EventHandler(_tmrFlashYto_Tick);
					_tmrFlashYto.Tag = true;
					_tmrFlashYto.Start();
				}
				else if (!IsJiangZheHu())
				{
					int c = GetProductCount();

					if (c >= 4)
					{
						rdoSf.Checked = true;
						//lblSfReason.Text = "(˳�����)";
						//lblSfReason.ForeColor = Color.BlueViolet;
					}
					else if (c >= 2 && IsFarProvince())
					{
						rdoSf.Checked = true;
						//lblSfReason.Text = "(�ʷѽ���)";
						//lblSfReason.ForeColor = Color.DarkGreen;
					}
					else
					{
						rdoYto.Checked = true;
					}
				}
				else // Added by KK on 2015/11/08.
				{
					rdoYto.Checked = true;
				}
			}

			// Added by KK on 2015/03/30.
			int cMustSendFromDe = 0, cBonded = 0;
			GetProducts(_orders, out cMustSendFromDe, out cBonded);
			if (cMustSendFromDe > 0)
			{
				MessageBox.Show(
					this, 
					string.Format("����{0}������ֱ����Ʒ����!!!\n�Ѵӷ����嵥��ɾ��, ��Ҫ�����ַ���.",cMustSendFromDe), this.Text, 
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				chkPartial.Checked = true;
			}
			// Added by KK on 2015/12/09.
			if (RemarkContainsDeOrBonded())
			{
				MessageBox.Show(
					this, 
					string.Format("���ܴ���ֱ�ʻ��߱�˰����Ʒ, ������Ҫ�����ַ���.",cMustSendFromDe), this.Text, 
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				chkPartial.Checked = true;
			}		

			//if (cBonded > 0)
			//{
			//    MessageBox.Show(
			//        this,
			//        "���ֱ�˰����Ʒ����!!!\n�Ѵӷ����嵥��ɾ��, ��Ҫ�����ַ���.", this.Text,
			//        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//    chkPartial.Checked = true;
			//}

			foreach (SoldProductInfo spi in _soldProductInfos)
			{
				if (spi.Id.Equals("900-0015"))
				{
					MessageBox.Show(this, "ˮ��! ˮ��!\nע��, ����Ҫ�ֶ�������о!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
					break;
				}

				if (spi.Conflict)
				{
					MessageBox.Show(
						this, 
						"��⵽�в�Ʒ�������ͺ�(�����ɫ��).\n������Ҫ�ֶ��޸Ĳ�Ʒ˵�����ֶ�����.\n\n��������ϸС��de����~", this.Text, 
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}

			if (txtProducts.Text.Trim().Length < 5)
				MessageBox.Show(this, "��Ʒ����δ��ȷʶ��, ��Ҫ�ֶ�����.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			
			
			if (RemarkContainsDistributorInfo())
			{
				Distributor distributor = Distributor.Match(_orders[0].BuyerAccount.Trim());
				if (null == distributor)
					MessageBox.Show(
						this, 
						string.Format("������Ի�ͷ���ע�п��ܰ���������Ϣ.\n����δʶ��<{0}>�Ǽ�Ϊ�����˺�.\n\n��ע���޸ķ�������Ϣ!!!", txtBuyerAccount.Text), 
						this.Text, 
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			
			this.Activate();
			btnPrint.Focus();
		}

		void _tmrFlashYto_Tick(object sender, EventArgs e)
		{
			_tmrFlashYto.Tag = !(bool)_tmrFlashYto.Tag;
			rdoYto.ForeColor = (bool)_tmrFlashYto.Tag ? this.ForeColor : Color.FromKnownColor(KnownColor.Window);
		}

		void _tmrFlashSf_Tick(object sender, EventArgs e)
		{
			_tmrFlashSf.Tag = !(bool)_tmrFlashSf.Tag;
			rdoSf.ForeColor = (bool)_tmrFlashSf.Tag ? this.ForeColor : Color.FromKnownColor(KnownColor.Window);
		}
		
		private bool RemarkContainsSf(out bool freightCollect, out bool pickup)
		{
			freightCollect = false;
			pickup = false;
			bool sf = false;
		
			foreach (Order o in _orders)
			{
				if (o.Remark.Contains("˳��"))
					sf = true;
				if (o.Remark.Contains("˳��"))
					sf = true;
				if (o.BuyerRemark.Contains("˳��"))
					sf = true;
				if (o.BuyerRemark.Contains("˳��"))
					sf = true;
				
				if (o.Remark.Contains("����"))
					freightCollect = true;
				if (o.BuyerRemark.Contains("����"))
					freightCollect = true;
					
				if (o.Remark.Contains("����"))
					pickup = true;
				if (o.Remark.Contains("��ȡ"))
					pickup = true;
				if (o.BuyerRemark.Contains("����"))
					pickup = true;
				if (o.BuyerRemark.Contains("��ȡ"))
					pickup = true;
			}
			return sf;
		}
		
		private bool RemarkContainsDeOrBonded()
		{
			foreach (Order o in _orders)
			{
				if (o.Remark.Contains("ֱ��") || o.Remark.Contains("��˰��") || o.Remark.Contains("��˰��"))
					return true;
				if (o.BuyerRemark.Contains("ֱ��") || o.BuyerRemark.Contains("��˰��") || o.BuyerRemark.Contains("��˰��"))
					return true;
			}
			return false;
		}
		
		private bool RemarkContainsYto()
		{
			foreach (Order o in _orders)
			{
				if (o.Remark.Contains("Բͨ"))
					return true;
				if (o.BuyerRemark.Contains("Բͨ"))
					return true;
			}
			return false;
		}
		
		private bool RemarkContainsDistributorInfo()
		{
			foreach (Order o in _orders)
			{
				if (o.Remark.Contains("����"))
					return true;
				if (o.Remark.Contains("��Ҫ����"))
					return true;
				if (o.Remark.Contains("������Ϣ"))
					return true;
				if (o.Remark.Contains("������Ϣ"))
					return true;
				if (o.Remark.Contains("������"))
					return true;
				if (o.BuyerRemark.Contains("����"))
					return true;
				if (o.BuyerRemark.Contains("��Ҫ����"))
					return true;
				if (o.BuyerRemark.Contains("������Ϣ"))
					return true;
				if (o.BuyerRemark.Contains("������Ϣ"))
					return true;
				if (o.BuyerRemark.Contains("������"))
					return true;
			}
			return false;
		}
		
		private bool IsFarProvince()
		{
			if (txtProvince.Text.Trim().StartsWith("ɽ��"))
				return true;
			if (txtProvince.Text.Trim().StartsWith("����"))
				return true;
			if (txtProvince.Text.Trim().StartsWith("����"))
				return true;
			if (txtProvince.Text.Trim().StartsWith("�Ĵ�"))
				return true;
			if (txtProvince.Text.Trim().StartsWith("����"))
				return true;
			if (txtProvince.Text.Trim().StartsWith("����"))
				return true;
			if (txtProvince.Text.Trim().StartsWith("����"))
				return true;
			if (txtProvince.Text.Trim().StartsWith("������"))
				return true;
			if (txtProvince.Text.Trim().StartsWith("����"))
				return true;
			if (txtProvince.Text.Trim().StartsWith("����"))
				return true;
			if (txtProvince.Text.Trim().StartsWith("���ɹ�"))
				return true;
			if (txtProvince.Text.Trim().StartsWith("����"))
				return true;
			if (txtProvince.Text.Trim().StartsWith("����"))
				return true;
			if (txtProvince.Text.Trim().StartsWith("�ຣ"))
				return true;
			return false;
		}
		
		private bool IsJiangZheHu()
		{
			if (txtProvince.Text.Trim().StartsWith("�Ϻ�"))
				return true;
			if (txtProvince.Text.Trim().StartsWith("�㽭"))
				return true;
			if (txtProvince.Text.Trim().StartsWith("����"))
				return true;
			if (txtProvince.Text.Trim().StartsWith("����"))
				return true;
			return false;
		}
		
		private int GetProductCount()
		{
			int c = 0;
			foreach (SoldProductInfo spi in _soldProductInfos)
			{
				if (!IsAptamilHippMilk(spi.Id))
					continue;
				c += spi.Count;
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
		//        if (o.Items.Contains("��˰��"))
		//            return true;
		//    }
		//    return false;
		//}

		// �Ӷ����������ȡ��Ʒ��Ϣ, ��ͬ��Ʒ�ϲ�����.
		private List<SoldProductInfo> GetProducts(List<Order> orders, out int cMustSendFromDe, out int cBonded)
		{
			cMustSendFromDe = 0;
			cBonded = 0;
			SortedList<string, SoldProductInfo> sortedProducts = new SortedList<string, SoldProductInfo>();

			foreach (Order o in orders)
			{
				string allItems = o.Items;
				string[] items = allItems.Split('��');
				for (int i = 0; i < items.Length; i++)
				{
					string item = items[i];
					string[] infos = item.Split('��');
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
						if (productTitle.Contains(string.Format("{0}��װ", c)) || productTitle.Contains(string.Format("{0}��װ", c)))
						{
							count *= c;
							break;
						}
					}
					
					if (productTitle.Contains("Knoppers"))
						count *= 10;
					
					//// ˫11����, 1+12������, 1+4������, 2+12������, 2+4������, 3��4������
					//if (productTitle.Contains("12�а��ʰ�˰") || productTitle.Contains("ֱ��12��")) 
					//    count *= 12;
					//if (productTitle.Contains("ֱ��9��"))
					//    count *= 9;
					//if (productTitle.Contains("�ֻ�4��"))
					//    count *= 4;
					//if (productTitle.Contains("�ֻ�4��"))
					//    count *= 4;
					//if (pi.Id.Equals("001-0005") && productTitle.Contains("12�а��ʰ�˰"))
					//    count *= 12;
					//if (pi.Id.Equals("001-0005") && productTitle.Contains("4��"))
					//    count *= 4;
					//if (pi.Id.Equals("001-0006") && productTitle.Contains("12�а��ʰ�˰"))
					//    count *= 12;
					//if (pi.Id.Equals("001-0006") && productTitle.Contains("4��"))
					//    count *= 4;
					//if (pi.Id.Equals("001-0004") && productTitle.Contains("4��"))
					//    count *= 4;

					Order.OrderStatus status = (Order.OrderStatus)Enum.Parse(typeof(Order.OrderStatus), infos[3]);
					bool cancelled = false;
					bool sent = false;
					if (infos.Length >= 4)
					{
						cancelled = (status == Order.OrderStatus.Closed);
						sent = (status == Order.OrderStatus.Sent);
					}
					if (cancelled || sent)
						continue;
					
					string code = string.Empty;
					if (infos.Length >= 5)
						code = infos[4];
					if (code.ToLower().StartsWith("d-"))
					{
						cMustSendFromDe++;
						continue;
					}
					// modified by KK on 2015/08/21.
					// ֱ�ʡ���˰���ֻ����Ӻϲ�, ���ٴӱ���ʶ��ĳ�������Ǳ�˰��ר������.
					//if (code.ToLower().StartsWith("b-") || productTitle.Contains("��˰��"))
					//if (code.ToLower().StartsWith("b-"))
					//{
					//    cBonded++;
					//    continue;
					//}

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
						sortedProducts.Add(pi.Id, new SoldProductInfo(pi.Id, pi.BrandId, pi.NingboId, pi.Name, pi.Price, pi.Specification, pi.ShortName, pi.Keywords, pi.Conflict, count, status, code));
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

		private void btnPrint_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			
			if (!rdoYto.Checked && !rdoSf.Checked)
			{
				MessageBox.Show(this, "ѡ���ݹ�˾��.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			if (btnPrint.Text.Equals("Printed"))
			{
				DialogResult dr = MessageBox.Show(this, "�Ѵ�ӡ, �Ƿ��ٴδ�ӡ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (DialogResult.No == dr)
					return;
			}

			//if (rdoSf.Checked)
			//    MessageBox.Show(this, "��˳��!\nȷ���Ƿ�˳���浥!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

			string printerDocumentName = string.Format("{0}({1})", txtRecipientName.Text, txtBuyerAccount.Text);
			BillPrinterBase printer;
			if (rdoYto.Checked) // yto
			{
				printer = new YtoBillPrinter(printerDocumentName);
				printer.PrinterName = Settings.Instance.YtoPrinter;
			}
			else // sf
			{
				string destCityCode = string.Empty;
				if (chkSfOldBill.Checked)
				{
					printer = new SfBillPrinter(printerDocumentName);
					printer.PrinterName = Settings.Instance.SfPrinter;

					destCityCode = CityCodes.GetCityCode(txtProvince.Text.Trim());
					if (string.IsNullOrEmpty(destCityCode))
						destCityCode = CityCodes.GetCityCode(txtCity1.Text.Trim());
					((SfBillPrinter)printer).DestCode = destCityCode;
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

						//  check arrivable.
						bool arrivable = sforder.Arrivable(_orders[0].OrderId);
						if (!string.IsNullOrEmpty(sforder.ErrorCode))
						{
							MessageBox.Show(
								this,
								string.Format("ɸ��ʱ��������: �������={0}, ������Ϣ={1}", sforder.ErrorCode, sforder.ErrorMessage),
								this.Text,
								MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}
						if (!arrivable)
						{
							DialogResult dr = MessageBox.Show(
								this,
								string.Format("�˵�ַ˳�᲻��, ������Ҫ�ռ�������.\n{0}\n\n�Ƿ������˳��?",
									string.Format("{0} {1} {2} {3} {4}", txtProvince.Text.Trim(), txtCity1.Text.Trim(), txtCity2.Text.Trim(), txtDistrict.Text.Trim(), txtStreetAddress.Text.Trim())),
								this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
							if (DialogResult.No == dr)
								return;
						}

						sforder.RunOrderService(_orders[0].OrderId);
						if (!string.IsNullOrEmpty(sforder.ErrorCode))
						{
							MessageBox.Show(
								this,
								string.Format("��ȡ˳�ᵥ��ʱ��������: �������={0}, ������Ϣ={1}", sforder.ErrorCode, sforder.ErrorMessage),
								this.Text,
								MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}
						
						// Got mail number but no dest code. Address may be invalid.
						if (string.IsNullOrEmpty(sforder.DestCode))
						{
							MessageBox.Show(
								this,
								string.Format("δ���Ŀ���������(DestCode).\n�����Ƿ��ַ�������´�ӡ."),
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
								"���ֶ�������ȷ��˳�ᵥ��.",
								this.Text,
								MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							txtBillNumber.Focus();
							return;
						}
					}
					
					printer = new SfBillNewPrinter(txtBillNumber.Text, printerDocumentName);
					printer.PrinterName = Settings.Instance.SfNewPrinter;
					((SfBillNewPrinter)printer).DestCode = txtDestCode.Text;

					int count = 0;
					foreach (SoldProductInfo spi in _soldProductInfos)
						count += spi.Count;
					((SfBillNewPrinter)printer).ItemAmount = count;
				}
				
				((SfBillPrinter)printer).IsFreightCollect = chkFriehghtCollect.Checked;
				((SfBillPrinter)printer).IsPickup = chkPickup.Checked;
			}
			
			string senderName = txtSenderName.Text;
			if (txtSenderName.Text.Equals("�� �� e ��"))
				senderName = string.IsNullOrEmpty(txtSenderName.Text) ? string.Empty : string.Format("{0}****", txtSenderName.Text.Substring(0, 1));
			string buyerAccount = string.IsNullOrEmpty(txtBuyerAccount.Text) ? string.Empty : string.Format("{0}***{1}", txtBuyerAccount.Text.Substring(0, 1), txtBuyerAccount.Text.Substring(txtBuyerAccount.Text.Length-1, 1));

			printer.Font = new Font("Microsoft Yahei", 11);
			printer.SenderName = senderName; //txtSenderName.Text.Trim();
			printer.SenderAd = txtSenderAd.Text.Trim();
			printer.SenderTel = txtSenderTel.Text.Trim();
			printer.DisplayBuyerAccount = (txtSenderName.Text.Trim().Equals(ShopProfile.Current.DisplayNameOnBill));
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
		}

		#region obsoleted code on 2015/01/09.
		//void pdoc_PrintPage(object sender, PrintPageEventArgs e)
		//{
		//    Graphics g = e.Graphics;
			
		//    if (rdoYto.Checked)
		//        DrawBillYto(g);
		//    else if (rdoSf.Checked)
		//        DrawBillSf(g);
				
		//    txtBillNumber.Focus();
		//}
		#endregion

		#region obsolected code for old yto bill.
		//void DrawBill(Graphics g, bool withBg)
		//{
		//    if (null == g)
		//        return;
			
		//    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
		//    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

		//    if (withBg)
		//        g.DrawImage(picExpressBill.BackgroundImage, new Point(0, 0));

		//    DrawString(g, "�� �� e ��", new Point(150, 131), this.Font.Size + 4, FontStyle.Regular);
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
		//    if (string.IsNullOrEmpty(txtCity1.Text) && string.IsNullOrEmpty(txtCity2.Text)) // ֱϽ��
		//    {
		//        if (txtProvince.Text.StartsWith("�Ϻ�"))
		//        {
		//            if (txtDistrict.Text.StartsWith("�ֶ�") || txtDistrict.Text.StartsWith("�ϻ�"))
		//                destCity = "PD";//"�ֶ�";
		//            else
		//                destCity = "PX";//"����";
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
		#endregion

		#region obsoleted code on 2015/01/09
		//void DrawBillYto(Graphics g)
		//{
		//    if (null == g)
		//        return;
			
		//    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
		//    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

		//    bool displayBuyerAccount = (txtSenderName.Text.Trim().Equals(ShopProfile.Current.DisplayNameOnBill));
			
		//    DrawString(g, txtSenderName.Text, new Point(70, 112), this.Font.Size, FontStyle.Regular);
		//    DrawString(g, txtSenderAd.Text, new Point(70, 135), this.Font.Size - 2, FontStyle.Regular);
		//    DrawString(g, txtSenderTel.Text, new Point(92, 227));

		//    DrawString(g, DateTime.Now.ToString("yyyy/MM/dd"), new Point(265, 392));
		//    DrawString(g, txtRecipientName.Text, new Point(430, 112));
		//    if (displayBuyerAccount && txtBuyerAccount.Text.Length > 0)
		//        DrawString(
		//            g, string.Format("({0})", txtBuyerAccount.Text), 
		//            new Point(430 + g.MeasureString(txtRecipientName.Text, this.Font).ToSize().Width + 1, 116), 
		//            this.Font.Size - 3, FontStyle.Regular);

		//    /*
		//    DrawString(g, txtProvince.Text, new Point(585 - g.MeasureString(txtProvince.Text, this.Font).ToSize().Width, 155));
			
		//    Size city1Size = g.MeasureString(txtCity1.Text, this.Font).ToSize();
		//    DrawString(g, txtCity1.Text, new Point(660 - city1Size.Width, city1Size.Width > 72 ? 135 : 155));

		//    Size city2Size = g.MeasureString(txtCity2.Text, this.Font).ToSize();
		//    DrawString(g, txtCity2.Text, new Point(730 - city2Size.Width, city2Size.Width > 72 ? 135 : 155));

		//    Size districtSize = g.MeasureString(txtDistrict.Text, this.Font).ToSize();
		//    DrawString(g, txtDistrict.Text, new Point(790 - districtSize.Width, districtSize.Width > 72 ? 135 : 155));
			
		//    if (g.MeasureString(txtStreetAddress.Text, this.Font).Width > 320)
		//        DrawString(g, txtStreetAddress.Text, new Point(830 - g.MeasureString(txtStreetAddress.Text, this.Font).ToSize().Width, 182));
		//    else
		//        DrawString(g, txtStreetAddress.Text, new Point(450 + (350 - g.MeasureString(txtStreetAddress.Text, this.Font).ToSize().Width)/2, 182));
		//    */
		//    string fullAddress = string.Format("{0} {1} {2} {3}\n{4}", txtProvince.Text, txtCity1.Text, txtCity2.Text, txtDistrict.Text, txtStreetAddress.Text);
		//    SizeF fullAddressSize = g.MeasureString(fullAddress, this.Font, 840-505);
		//    int fullAddressTop = fullAddressSize.Height - 6 > g.MeasureString("A\nB\nC", this.Font).Height ? 122 : 146;
		//    StringFormat sf = new StringFormat();
		//    sf.Alignment = StringAlignment.Near;
		//    sf.LineAlignment = StringAlignment.Near;
		//    g.DrawString(fullAddress, this.Font, new SolidBrush(Color.Black), new RectangleF(430, fullAddressTop, (760-430), (225-fullAddressTop)));
			
		//    string phone = txtMobile.Text.Trim();
		//    if (phone.Length > 0 && txtPhone.Text.Trim().Length > 0)
		//        phone += ", ";
		//    if (txtPhone.Text.Trim().Length > 0)
		//        phone += txtPhone.Text;
		//    DrawString(g, phone, new Point(464, 227));

		//    string destCity = string.Empty;
		//    if (string.IsNullOrEmpty(txtCity1.Text) && string.IsNullOrEmpty(txtCity2.Text)) // ֱϽ��
		//    {
		//        if (txtProvince.Text.StartsWith("�Ϻ�"))
		//        {
		//            if (txtDistrict.Text.StartsWith("�ֶ�") || txtDistrict.Text.StartsWith("�ϻ�"))
		//                destCity = "PD";//"�ֶ�";
		//            else
		//                destCity = "PX";//"����";
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
			
		//    Size destCitySize = g.MeasureString(destCity, new Font(this.Font.Name, this.Font.Size + 14, FontStyle.Bold)).ToSize();
		//    DrawString(
		//        g, destCity, 
		//        new Point(destCitySize.Width <= 120 ? 620 + (120 - destCitySize.Width)/2 : 740 - destCitySize.Width, 46), 
		//        this.Font.Size + 14, FontStyle.Bold);

		//    // products
		//    // items.
		//    int x = (txtProducts.Lines.Length <= 2) ? 5 : 42;
		//    int y = (txtProducts.Lines.Length <= 2) ? 295 : 288;
		//    if (txtProducts.Lines.Length == 1)
		//        y += 5;
		//    //foreach (ProductInfo pi in _soldProductInfos)
		//    foreach (string s in txtProducts.Lines)
		//    {
		//        //DrawString(g, string.Format("{0} x {1}", ProductInfo.GetProductDesc(pi.Id), pi.Count), new Point(160, y));
		//        DrawString(g, s, new Point(x, y), this.Font.Size - 2, FontStyle.Regular);
		//        y += 14;
		//    }
			
		//    if (chkHoliday.Checked)
		//        DrawString(g, "��˾�ɼ������ע�⣬�˼��ڼ����������͡�\n��л�����������ˣ�", new Point(395, 450));

		//    //// Weight.
		//    //if (!_productDescChanged)
		//    //{
		//    //    string weight = GetWeight(_soldProductInfos);
		//    //    DrawString(g, weight, new Point(480, 265));
		//    //}
			
		//    //// Draw yto seal.
		//    //if (null != _ytoSeal)
		//    //    g.DrawImage(_ytoSeal, 250, 140);
		//}

		//void DrawBillSf(Graphics g)
		//{
		//    if (null == g)
		//        return;
			
		//    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
		//    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

		//    bool displayBuyerAccount = (txtSenderName.Text.Trim().Equals(ShopProfile.Current.DisplayNameOnBill));

		//    string sender = string.Format("{0}\n{1}\n{2}", txtSenderName.Text.Trim(), txtSenderAd.Text.Trim(), txtSenderTel.Text.Trim());
		//    DrawString(g, sender, new Point(60, 170), this.Font.Size, FontStyle.Regular);

		//    DrawString(g, DateTime.Now.ToString("yyyy/MM/dd"), new Point(420, 427));

		//    //DrawString(g, txtRecipientName.Text, new Point(125, 240));
		//    //if (txtBuyerAccount.Text.Length > 0)
		//    //    DrawString(
		//    //        g, string.Format("({0})", txtBuyerAccount.Text), 
		//    //        new Point(125 + g.MeasureString(txtRecipientName.Text, this.Font).ToSize().Width + 1, 240), 
		//    //        this.Font.Size - 3, FontStyle.Regular);

		//    /*
		//    DrawString(g, txtProvince.Text, new Point(585 - g.MeasureString(txtProvince.Text, this.Font).ToSize().Width, 155));
			
		//    Size city1Size = g.MeasureString(txtCity1.Text, this.Font).ToSize();
		//    DrawString(g, txtCity1.Text, new Point(660 - city1Size.Width, city1Size.Width > 72 ? 135 : 155));

		//    Size city2Size = g.MeasureString(txtCity2.Text, this.Font).ToSize();
		//    DrawString(g, txtCity2.Text, new Point(730 - city2Size.Width, city2Size.Width > 72 ? 135 : 155));

		//    Size districtSize = g.MeasureString(txtDistrict.Text, this.Font).ToSize();
		//    DrawString(g, txtDistrict.Text, new Point(790 - districtSize.Width, districtSize.Width > 72 ? 135 : 155));
			
		//    if (g.MeasureString(txtStreetAddress.Text, this.Font).Width > 320)
		//        DrawString(g, txtStreetAddress.Text, new Point(830 - g.MeasureString(txtStreetAddress.Text, this.Font).ToSize().Width, 182));
		//    else
		//        DrawString(g, txtStreetAddress.Text, new Point(450 + (350 - g.MeasureString(txtStreetAddress.Text, this.Font).ToSize().Width)/2, 182));
		//    */
		//    string phone = txtMobile.Text.Trim();
		//    if (phone.Length > 0 && txtPhone.Text.Trim().Length > 0)
		//        phone += ", ";
		//    if (txtPhone.Text.Trim().Length > 0)
		//        phone += txtPhone.Text;

		//    string fullAddress = string.Format(
		//        "           {0}{1}\n{7}\n{2} {3} {4} {5}\n{6}", 
		//        txtRecipientName.Text, 
		//        displayBuyerAccount ? string.Format("({0})", txtBuyerAccount.Text) : string.Empty, 
		//        txtProvince.Text, txtCity1.Text, txtCity2.Text, txtDistrict.Text, txtStreetAddress.Text,
		//        phone);
		//    //SizeF fullAddressSize = g.MeasureString(fullAddress, this.Font, 840-505);
		//    //int fullAddressTop = fullAddressSize.Height - 6 > g.MeasureString("A\nB\nC", this.Font).Height ? 110 : 131;
		//    StringFormat sf = new StringFormat();
		//    sf.Alignment = StringAlignment.Near;
		//    sf.LineAlignment = StringAlignment.Near;
		//    int fullAddressTop = 250;
		//    g.DrawString(fullAddress, this.Font, new SolidBrush(Color.Black), new RectangleF(60, fullAddressTop, (360-60), (370-fullAddressTop)));
			
		//    //string phone = txtMobile.Text.Trim();
		//    //if (phone.Length > 0 && txtPhone.Text.Trim().Length > 0)
		//    //    phone += ", ";
		//    //if (txtPhone.Text.Trim().Length > 0)
		//    //    phone += txtPhone.Text;
		//    //DrawString(g, phone, new Point(110, 396));
		//    ////DrawString(g, txtPhone.Text, new Point(680, 215));

		//    //string destCity = string.Empty;
		//    //if (string.IsNullOrEmpty(txtCity1.Text) && string.IsNullOrEmpty(txtCity2.Text)) // ֱϽ��
		//    //{
		//    //    if (txtProvince.Text.StartsWith("�Ϻ�"))
		//    //    {
		//    //        if (txtDistrict.Text.StartsWith("�ֶ�") || txtDistrict.Text.StartsWith("�ϻ�"))
		//    //            destCity = "PD";//"�ֶ�";
		//    //        else
		//    //            destCity = "PX";//"����";
		//    //    }
		//    //    else
		//    //    {
		//    //        destCity = txtProvince.Text;
		//    //    }
		//    //}
		//    //else
		//    //{
		//    //    destCity = (string.IsNullOrEmpty(txtCity2.Text) ? txtCity1.Text : txtCity2.Text);
		//    //}
			
		//    //Size destCitySize = g.MeasureString(destCity, new Font(this.Font.Name, this.Font.Size + 14, FontStyle.Bold)).ToSize();
		//    //DrawString(
		//    //    g, destCity, 
		//    //    new Point(destCitySize.Width <= 120 ? 700 + (120 - destCitySize.Width)/2 : 820 - destCitySize.Width, 46), 
		//    //    this.Font.Size + 14, FontStyle.Bold);

		//    // products
		//    // items.
		//    int y = 402;
		//    //foreach (ProductInfo pi in _soldProductInfos)
		//    foreach (string s in txtProducts.Lines)
		//    {
		//        //DrawString(g, string.Format("{0} x {1}", ProductInfo.GetProductDesc(pi.Id), pi.Count), new Point(160, y));
		//        DrawString(g, s, new Point(20, y), this.Font.Size - 2, FontStyle.Regular);
		//        y += 14;
		//    }
		//    DrawString(g, "�̷�", new Point(20, y), this.Font.Size - 2, FontStyle.Regular);
			
		//    string empno = "4 8 8 1 3 3";
		//    DrawString(g, empno, new Point(555, 46), this.Font.Size, FontStyle.Bold);
			
		//    //// Weight.
		//    //if (!_productDescChanged)
		//    //{
		//    //    string weight = GetWeight(_soldProductInfos);
		//    //    DrawString(g, weight, new Point(480, 265));
		//    //}
			
		//    string destCityCode = CityCodes.GetCityCode(txtProvince.Text);
		//    if (string.IsNullOrEmpty(destCityCode))
		//        destCityCode = CityCodes.GetCityCode(txtCity1.Text);
		//    DrawString(g, destCityCode, new Point(625, 76), this.Font.Size + 4, FontStyle.Bold);


		//    DrawString(g, "021CF", new Point(550, 80), this.Font.Size, FontStyle.Regular);
		//    DrawString(g, "���ſͻ�(����)    �˼��ڼ�����������, лл!", new Point(420, 450), this.Font.Size, FontStyle.Regular);
		//}
				
		//private void DrawString(Graphics g, string s, Point p)
		//{
		//    DrawString(g, s, p, this.Font.Size, FontStyle.Regular);
		//}

		//private void DrawString(Graphics g, string s, Point p, float fontSize, FontStyle fs)
		//{
		//    if (null == g)
		//        return;
		//    if (string.IsNullOrEmpty(s))
		//        return;

		//    p.Offset(10, -20);
			
		//    int plusPos = -1;
		//    if (s.Contains("1+") || s.Contains("2+") || s.Contains("12+"))
		//    {
		//        plusPos = s.IndexOf("+");
		//        s = s.Replace("+", "  "); 
		//    }
			
		//    g.DrawString(
		//        s,
		//        new Font(this.Font.Name, fontSize, fs), new SolidBrush(Color.Black), p);/*, 
		//        new RectangleF((float)p.X, (float)p.Y, 360, 50), 
		//        StringFormat.GenericDefault);*/
			
		//    if (plusPos > 0)
		//    {
		//        SizeF size = g.MeasureString(s.Substring(0, plusPos), new Font(this.Font.Name, fontSize, fs));
		//        p.Offset((int)size.Width-7, -7);
		//        g.DrawString("+", new Font(this.Font.Name, fontSize + 2, fs), new SolidBrush(Color.Black), p);
		//    }
		//}
		#endregion

		//private void txtBillNumber_TextChanged(object sender, EventArgs e)
		//{
		//    if (rdoSf.Checked)
		//    {
		//        if (txtBillNumber.Text.Length == 12) // sf
		//        {
		//            btnGo.Enabled = true;
		//            btnGo_Click(btnGo, EventArgs.Empty);
		//        }
		//        return;
		//    }
			
		//    if (txtBillNumber.Text.Length == 10) // yto
		//    {
		//        btnGo.Enabled = true;
		//        btnGo_Click(btnGo, EventArgs.Empty);
		//    }
		//}

		private void txtBillNumber_KeyPress(object sender, KeyPressEventArgs e)
		{
			//if (((int)e.KeyChar >= 0x30 && (int)e.KeyChar <= 0x39) || (int)e.KeyChar == 13)
			//{
			//    if ((int)e.KeyChar == 13)
			//    {
			//        btnGo_Click(btnGo, EventArgs.Empty);
			//    }
			//}
			////else if ( (e.KeyChar == 'c' || e.KeyChar == 'C' || e.KeyChar == 'v' || e.KeyChar == 'V' || e.KeyChar == 'x' || e.KeyChar == 'X'))
			//else
			//{
			//    e.Handled = true;
			//}
		}

		private void txtBillNumber_KeyUp(object sender, KeyEventArgs e)
		{

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

		private void StockoutForm_KeyPress(object sender, KeyPressEventArgs e)
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
			
			if (txtBillNumber.Text.Trim().Length < 12)
			{
				MessageBox.Show(this, "The shipment number of both Sf and Yto is 12.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			
			// �㷢��.
			DialogResult dr = DialogResult.Cancel;
			foreach (Order o in _orders)
			{
				ConsignShWebBrowserForm.Instance.OrderId = o.OrderId;
				ConsignShWebBrowserForm.Instance.BillNumber = txtBillNumber.Text;
				ConsignShWebBrowserForm.Instance.ExpressCompany = (rdoSf.Checked ? ExpressCompanies.Sf : ExpressCompanies.Yto);
				ConsignShWebBrowserForm.Instance.AutoSubmit = !chkPartial.Checked;
				dr = ConsignShWebBrowserForm.Instance.ShowDialog(this);

				//if (DialogResult.Retry == dr) // retry.
				//    dr = ConsignShWebBrowserForm.Instance.ShowDialog(this);
				
				if (DialogResult.OK != dr)
				    break;
				o.Consign();
			}

			if (DialogResult.OK != dr)
			{
				MessageBox.Show(this, "δ��ȷ�㷢��, �������¿�����, �����к˶Բ�������Ӧ����!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			// Removed by KK on 2015/05/24. Replaced by _productInfoChanged.
			//if (_productDescChanged)
			//{
			//    if (DialogResult.No == MessageBox.Show(this, "��Ʒ�������޸Ĺ�, �Ƿ������������Ʒ���¿��?\n", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
			//        return;
			//}
			if (_productInfoChanged) // Added by KK on 2015/05/24.
			{
				if (DialogResult.No == MessageBox.Show(this, "��Ʒ��Ϣ���޸Ĺ�.\n�Ƿ��޸Ĺ�����Ʒ��Ϣ���¿��.\n", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
					return;
			}
			else if (_manualStockoutCount > 0)
			{
				if (DialogResult.No == MessageBox.Show(this, "�Ѿ��ֶ�����, �Ƿ�������¿��?\n", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
					return;
			}

			// ����Ǽ�.
			try
			{
				string ids = string.Empty;
				string counts = string.Empty;
				// Removed by KK on 2015/05/24.
				// ���ٰ�������Ʒ���¿��, ���ǰ������ϵ���Ʒ�б���¿��.
				// ͨ�������, �����ϵ���Ʒ�б���Ƕ�����Ʒ. ���ڽ������ֶ��޸Ĺ���Ʒ��Ϣ, �����ǰ��޸ĺ����Ʒ��Ϣ���¿��.
				// ��, �Զ����¿�����Ǻʹ�ӡ�������һ��, ���ǺͶ�������һ��.
				//foreach (SoldProductInfo spi in _soldProductInfos)
				//{
				//    ids += spi.Id + ",";
				//    counts += "-" + spi.Count.ToString() + ",";
				//}
				foreach (Control c in pnlProductList.Controls)
				{
					if (!(c is SoldProductInfoControl))
						continue;
					SoldProductInfoControl spic = c as SoldProductInfoControl;
					if (spic.Count == 0)
						continue;
					if (spic.SelectedProductInfo.Id.Equals("0"))
						continue;
					ids += spic.SelectedProductInfo.Id + ",";
					counts += "-" + spic.Count.ToString() + ",";
				}
				
				string url = string.Format(Common.URL_DATA_CENTER, "stock");
				url += string.Format(
					"&productids={0}&counts={1}&dest={2}&op={3}&comment={4}{5}", 
					ids, counts, 
					(ShopProfile.Current.Shop==ShopEnum.Egode?string.Empty:(ShopProfile.Current.ShortName + "\\")) + string.Format("{0} [{1}]", txtBuyerAccount.Text, txtRecipientName.Text), 
					Settings.Operator, 
					rdoSf.Checked ? "sf" : "yto",  txtBillNumber.Text);
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

		private void pnlProductList_Paint(object sender, PaintEventArgs e)
		{
			ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, pnlProductList.Width, pnlProductList.Height), Color.LightGray, ButtonBorderStyle.Solid);
		}

		private void pnlPrint_Paint(object sender, PaintEventArgs e)
		{
			ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, pnlMain.Width, pnlMain.Height), Color.LightGray, ButtonBorderStyle.Solid);
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
			Point p = new Point(btnPrint.Right-2, btnPrint.Bottom+1);
			p.Offset(pnlMain.Location);
			p.Offset(-lblMoney.Width, 0);
			lblMoney.Location = p;

			pnlMoney.Location = new Point(pnlMain.Left + 15, pnlMain.Top + 190);
		}

		private void txtHideShop_Click(object sender, EventArgs e)
		{
			txtSenderName.Text = string.Empty;
			txtSenderAd.Text = string.Empty;
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

		private void btnAppenRemark_Click(object sender, EventArgs e)
		{
			Utility.InputBoxForm inputbox = new Egode.Utility.InputBoxForm();
			if (DialogResult.OK == inputbox.ShowDialog(this))
			{
				UpdateSellMemoWebBrowserForm usmf = new UpdateSellMemoWebBrowserForm(_orders[0], inputbox.Message, true);
				usmf.ShowDialog(this);
			}
		}

		private void btnStockout_Click(object sender, EventArgs e)
		{
			StockActionForm saf = new StockActionForm(true, "001", "001-0001");
			saf.Comment = (rdoYto.Checked ? "yto" : "sf") + txtBillNumber.Text.Trim();
			saf.FromTo = (ShopProfile.Current.Shop==ShopEnum.Egode?string.Empty:(ShopProfile.Current.ShortName + "\\")) + string.Format("{0} [{1},{2}]", txtBuyerAccount.Text, txtRecipientName.Text, txtMobile.Text);
			if (DialogResult.OK == saf.ShowDialog(this))
			{
				// ����Ǽ�.
				try
				{
					string url = string.Format(Common.URL_DATA_CENTER, "stock");
					url += string.Format(
						"&productids={0}&counts=-{1}&dest={2}&op={3}&comment={4}", 
						saf.Product.Id, saf.Count, 
						saf.FromTo, 
						Settings.Operator, saf.Comment);
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

					btnStockout.Text = string.Format("�ֶ�����({0})", ++_manualStockoutCount);
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}

		private void txtBillNumber_TextChanged(object sender, EventArgs e)
		{
			btnGo.Enabled = (txtBillNumber.Text.Length == 12);
			
			// Added by KK on 2015/11/08.
			// Save last yto number.
			if (rdoYto.Checked && txtBillNumber.Text.Length == 12)
				Settings.Instance.RecentYtoBillNumber = txtBillNumber.Text;
		}

		private void btnRegDistributor_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			
			Distributor d = Distributor.Match(txtBuyerAccount.Text);
			if (null != d)
			{
				string warning = string.Format(
					"��ID�ѵǼǴ�����Ϣ, ��Ϣ����: \n\nID: {0}\n��1����Ϣ(ͨ���Ƿ�����): {1}\n��2����Ϣ(ͨ�������ӵȹ����������): {2}\n��3����Ϣ(ͨ���ǵ绰����): {3}", 
					d.Id, d.Name, d.Ad, d.Tel);
				MessageBox.Show(this, warning, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			
			if (string.IsNullOrEmpty(txtSenderTel.Text.Trim()))
			{
				MessageBox.Show(this, "�����˵绰���벻��Ϊ��", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			
			if (string.IsNullOrEmpty(txtSenderName.Text) || string.IsNullOrEmpty(txtSenderAd.Text))
			{
				if (DialogResult.No == MessageBox.Show(this, "��������Ϣ��1�л��2����ϢΪ��(�Ǳ�����), �Ƿ����?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
					return;
			}

			string s = string.Format(
				"�ǼǴ�����Ϣ����: \n\nID: {0}\n��1����Ϣ(ͨ���Ƿ�����): {1}\n��2����Ϣ(ͨ�������ӵȹ����������): {2}\n��3����Ϣ(ͨ���ǵ绰����): {3}\n\nȷ����Ϣ��������Yes�ύ.", 
				txtBuyerAccount.Text, txtSenderName.Text, txtSenderAd.Text, txtSenderTel.Text);
			
			DialogResult dr = MessageBox.Show(this, s, this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
			if (DialogResult.Yes == dr)
			{
				string url = string.Format(Common.URL_DATA_CENTER, "regdistributor");
				url += string.Format("&id={0}&name={1}&ad={2}&tel={3}", txtBuyerAccount.Text, txtSenderName.Text, txtSenderAd.Text, txtSenderTel.Text);
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
				
				if (result.Equals("ok"))
					Distributor.Distributors.Add(new Distributor(txtBuyerAccount.Text, txtSenderName.Text, txtSenderAd.Text, txtSenderTel.Text));
			}

			Cursor.Current = Cursors.Default;
		}

		//protected override void OnPaint(PaintEventArgs e)
		//{
		//    base.OnPaint(e);
			
		//    int y = btnPrint.Bottom + (txtBillNumber.Top - btnPrint.Bottom) / 2;
			
		//    e.Graphics.DrawLine(new Pen(Color.FromArgb(192,192,192)), new Point(6, y), new Point(this.ClientRectangle.Right - 6, y));
		//    e.Graphics.DrawLine(new Pen(Color.White), new Point(6, y+1), new Point(this.ClientRectangle.Right - 6, y+1));
		//}

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
					MessageBox.Show(this, "����Ʒ�Ѵ������б���, �벻Ҫ�ظ�ѡ��.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
			DialogResult dr = MessageBox.Show(this, "ȷ��Ҫɾ����?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
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
			pnlMain.Top = txtRemark.Bottom + 6;
			pnlProductList.Height = Math.Max(115, tsAddProduct.Bottom);
			txtProducts.Height = pnlProductList.Height;
			pnlMain.Height = chkHoliday.Bottom + 2;
			lblPrint.Top = pnlMain.Top + 2;
			pnlConsign.Top = pnlMain.Bottom + 6;
			lblBillNumber.Top = pnlConsign.Top + 2;
			this.ClientSize = new Size(this.ClientSize.Width, pnlConsign.Bottom + 6);
			pnlMain.Refresh();
		}

		private void txtBillNumber_Enter(object sender, EventArgs e)
		{
			txtBillNumber.SelectionStart = txtBillNumber.Text.Length + 1;
		}
	}
}