using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using OrderLib;

namespace Egode
{
	public partial class PacketDetailsControl : UserControl
	{
		#region class ExportItemMenuItem
		private class ExportItemMenuItem : MenuItem
		{
			private ExportItem _ei;
			
			public ExportItemMenuItem(ExportItem ei)
			{
				_ei = ei;
				this.Text = ei.ExportItemString;
			}
			
			public ExportItem ExportItem
			{
				get { return _ei; }
			}
		}
		#endregion

		private PacketInfo _packetInfo;

		public PacketDetailsControl(PacketInfo pi, int index)
		{
			_packetInfo = pi;
			InitializeComponent();
			lblIndex.Text = index.ToString();
			
			txtBuyerRemark.Text = pi.BuyerRemark;
			pnlBuyerRemark.Visible = !string.IsNullOrEmpty(pi.BuyerRemark);
			txtRemark.Text = pi.Remark;
			pnlRemark.Visible = !string.IsNullOrEmpty(pi.Remark);

			// Generate description for products.
			#region obsoleted code
			/*
			StringBuilder sbProducts = new StringBuilder();
			string[] itemInfos = _packetInfo.ProductInfo.Split('★');
			for (int i = 0; i < itemInfos.Length; i++)
			{
				string itemInfo = itemInfos[i];
				string[] infos = itemInfo.Split('☆');
				if (infos.Length < 3)
					continue;

				bool cancelled = false;
				bool sent = false;
				if (infos.Length >= 4)
				{
					cancelled = (((Order.OrderStatus)Enum.Parse(typeof(Order.OrderStatus), infos[3])) == Order.OrderStatus.Closed);
					sent = (((Order.OrderStatus)Enum.Parse(typeof(Order.OrderStatus), infos[3])) == Order.OrderStatus.Sent);
				}
				if (cancelled || sent)
					continue;

				if (infos[0].Contains("加强包装费"))
					continue;
				if (infos[0].Contains("气囊加固"))
					continue;

				ProductInfo productInfo = ProductInfo.Match(infos[0]);
				string productTitle = string.Empty;
				if (null != productInfo)
				{
					productTitle = productInfo.ShortName;
					nudNetWeight.Value += (decimal)(productInfo.Weight * int.Parse(infos[2]) / 1000);
				}
				else
				{
					productTitle = Order.SimplifyItemSubject(infos[0], true).Trim();
				}

				sbProducts.Append(string.Format("{0}; {1}\r\n", productTitle, infos[2])); // [0]:title, [1]:amount.
			}
			*/
			#endregion
			
			StringBuilder sbProducts = new StringBuilder();
			foreach (SoldProductInfo spi in _packetInfo.Products)
			{
				if (spi.Status == Order.OrderStatus.Sent || spi.Status == Order.OrderStatus.Closed)
					continue;
				sbProducts.Append(string.Format("{0}; {1}\r\n", spi.ShortName, spi.Count)); // [0]:title, [1]:amount.
				nudNetWeight.Value += (decimal)(spi.Weight * spi.Count / 1000);
			}

			if (sbProducts.Length > 2 && sbProducts.ToString().EndsWith("\r\n"))
				sbProducts.Remove(sbProducts.Length-2, 2); // remove return at the end of string.
			txtProduct.Text = sbProducts.ToString();
			txtProduct.Height = txtProduct.PreferredSize.Height;

			cboPackets.Items.Add(new Packet(PacketTypes.Unknown, 0, 0));

			//cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 5000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 5500, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 6000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 6500, 0));
			cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 5000, 0));
			cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 6000, 0));
			cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 7000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 7500, 0));
			cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 8000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 8500, 0));
			cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 9000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 9500, 0));
			cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 10000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 10500, 0));
			cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 11000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 11500, 0));
			cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 12000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 12500, 0));
			cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 13000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.PostNL, 14000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.PostNL, 15000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.PostNL, 20000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.PostNL, 25000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.PostNL, 30000, 0));

			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 5000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 5500, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 6000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 6500, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 7000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 7500, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 8000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 8500, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 9000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 9500, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 10000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 10500, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 11000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 11500, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 12000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 12500, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 13000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 13500, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 14000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 14500, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 15000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 20000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 25000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 31500, 0));

			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 5000, 239));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 5500, 245));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 6000, 250));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 6500, 255));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 7000, 260));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 7500, 265));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 8000, 270));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 8500, 275));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 9000, 290));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 9500, 295));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 10000, 300));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 10500, 310));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 11000, 323));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 11500, 333));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 12000, 343));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 12500, 353));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 13000, 363));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 14000, 373));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 15000, 393));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 20000, 483));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 25000, 573));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 30000, 663));
			
			//cboPackets.Items.Add(new Packet(PacketTypes.Rainbow, 6000, 265));
			//cboPackets.Items.Add(new Packet(PacketTypes.Rainbow, 8000, 275));
			//cboPackets.Items.Add(new Packet(PacketTypes.Rainbow, 9000, 285));
			//cboPackets.Items.Add(new Packet(PacketTypes.Rainbow, 10000, 300));
			//cboPackets.Items.Add(new Packet(PacketTypes.Rainbow, 11000, 310));
			//cboPackets.Items.Add(new Packet(PacketTypes.Rainbow, 12000, 320));
			//cboPackets.Items.Add(new Packet(PacketTypes.Rainbow, 13000, 330));
			//cboPackets.Items.Add(new Packet(PacketTypes.Rainbow, 14000, 340));
			//cboPackets.Items.Add(new Packet(PacketTypes.Rainbow, 15000, 375));
			//cboPackets.Items.Add(new Packet(PacketTypes.Rainbow, 20000, 420));
			//cboPackets.Items.Add(new Packet(PacketTypes.Supermarket, 7000, 241));
			//cboPackets.Items.Add(new Packet(PacketTypes.Supermarket, 8000, 258));
			//cboPackets.Items.Add(new Packet(PacketTypes.Supermarket, 9000, 285));
			//cboPackets.Items.Add(new Packet(PacketTypes.Supermarket, 15000, 358));
			//cboPackets.Items.Add(new Packet(PacketTypes.Dealworthier, 5000, 246));
			//cboPackets.Items.Add(new Packet(PacketTypes.Dealworthier, 5500, 251));
			//cboPackets.Items.Add(new Packet(PacketTypes.Dealworthier, 6000, 258));
			//cboPackets.Items.Add(new Packet(PacketTypes.Dealworthier, 6500, 263));
			//cboPackets.Items.Add(new Packet(PacketTypes.Dealworthier, 7000, 268));
			//cboPackets.Items.Add(new Packet(PacketTypes.Dealworthier, 7500, 273));
			//cboPackets.Items.Add(new Packet(PacketTypes.Dealworthier, 8000, 278));
			//cboPackets.Items.Add(new Packet(PacketTypes.Dealworthier, 8500, 288));
			//cboPackets.Items.Add(new Packet(PacketTypes.Dealworthier, 9000, 298));
			//cboPackets.Items.Add(new Packet(PacketTypes.Dealworthier, 9500, 303));
			//cboPackets.Items.Add(new Packet(PacketTypes.Dealworthier, 10000, 308));
			//cboPackets.Items.Add(new Packet(PacketTypes.Dealworthier, 10500, 320));
			
			for (int i = 0; i < cboPackets.Items.Count; i++)
			{
				Packet p = (Packet)cboPackets.Items[i];
				if (p.Type == _packetInfo.Type && p.Weight == _packetInfo.Weight)
				{
					cboPackets.SelectedIndex = i;
					break;
				}
			}
			
			// fill full original address.
			txtFullAddress.Text = _packetInfo.FullAddress;
			
			// Modified by KK on 2013/09/25.
			// 对于修改过地址的订单, 下述代码取到的是老地址的电话号码.
			//txtPhoneNumber.Text = _packetInfo.PhoneNumber;
			// 如果么有修改过地址, FullAddress就是根据老地址生成; 如果修改过, 就是根据新地址生成. 因此从FullAddress中取到的电话号码总是正确的.
			txtPhoneNumber.Text = (_packetInfo.FullAddress.Contains("，")? _packetInfo.FullAddress.Replace("，", ","):_packetInfo.FullAddress).Split(',')[1].Trim(); // Assert!!!

			try // If error occurs let operator edit address itself.
			{
				// Get post code.
				string postCode = string.Empty;
				Regex regex = new Regex(@"(\(\d{6}\))|(,\d{6}$)");
				Match m = regex.Match(_packetInfo.FullAddress.Replace("，", ","));
				//Trace.Assert(m.Success);
				if (m.Success)
				{
					postCode = m.Value;
					postCode = postCode.Replace("(", string.Empty).Replace(")", string.Empty).Replace(",", string.Empty);
					txtPostCode.Text = postCode;
				}

				// Analyse address. Get information for original address.
				string[] recipientInfos = _packetInfo.FullAddress.Replace("，", ",").Replace(m.Value, string.Empty).Trim().Split(',');
				txtRecipientName.Text = recipientInfos[0].Trim();

				// Get address details.
				string[] addressInfos = recipientInfos[recipientInfos.Length - 1].Split(' ');
				string province = string.Empty;
				string city1 = string.Empty;
				string city2 = string.Empty;
				foreach (string s in addressInfos)
				{
					string ss = s.Trim();

					if (ss.EndsWith("省") || ss.EndsWith("自治区") || ss.Equals("北京") || ss.Equals("上海") || ss.Equals("天津") || ss.Equals("重庆"))
						province = ss;
					if (ss.EndsWith("市") && !ss.StartsWith(province))
					{
						if (string.IsNullOrEmpty(city1))
							city1 = ss;
						else
							city2 = ss;
					}
				}
				if (!province.EndsWith("省") && !province.EndsWith("自治区") && !province.EndsWith("市")) // 直辖市.
					province += "市";
				txtProvinceCity.Text = string.Format("{0} {1} {2}", province, city1, city2).Replace("广西壮族自治区", "广西省").Replace("宁夏回族自治区", "宁夏省").Replace("内蒙古自治区", "内蒙古").Replace("西藏自治区", "西藏").Replace("新疆维吾尔自治区", "新疆");
				
				// Get district;
				string streetAddress = RemoveStartingProvinceCity(recipientInfos[recipientInfos.Length - 1], province, city1, city2);
				string[] streetAddressInfos = streetAddress.Split(' '); // 此处如果还有空格分隔, 则认为第1个字符串是区. 但是如果买家在街道地址中也输入了空格, 可能出错.
				string district = string.Empty;
				if (streetAddressInfos.Length >= 2)
					district = streetAddressInfos[0];
				if (!string.IsNullOrEmpty(district))
					streetAddress = streetAddress.Replace(district, string.Empty).Trim();
				
				// 至此, 得到了去掉了前面的省市区的地址, 也即是买家在淘宝上手动输入的部分. 
				// 需要去除买家可能重复输入的省市区信息.
				if (streetAddress.StartsWith(province)) // remove starting province.
				{
					streetAddress = streetAddress.Substring(province.Length, streetAddress.Length - province.Length);
					
					// 如果是直辖市, 可能会在字符串首留下个"市".
					if (streetAddress.StartsWith("市"))
						streetAddress = streetAddress.Substring(1, streetAddress.Length - 1);
				}
				
				if (streetAddress.StartsWith(city1)) // remove starting city1.
					streetAddress = streetAddress.Substring(city1.Length, streetAddress.Length - city1.Length);

				if (streetAddress.StartsWith(city2)) // remove starting city2.
					streetAddress = streetAddress.Substring(city2.Length, streetAddress.Length - city2.Length);

				if (streetAddress.StartsWith(district)) // remove starting district.
					streetAddress = streetAddress.Substring(district.Length, streetAddress.Length - district.Length);

				txtStreetAddress.Text = district + streetAddress.Replace(m.Value, string.Empty);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}
			
			// Added by KK on 2016/12/12.
			System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"\d{17}[\d|x|X]");
			System.Text.RegularExpressions.Match match = r.Match(_packetInfo.Remark);
			if (match.Success)
				txtExportInfo.Text = match.Value;
			
			tblOuter.Height = pnlPinyinAddress.Top + tblPinyinAddress.Height + 2; // 修正尺寸. 不知道为啥各panel都是autosize最后尺寸还是有误差 -_-|||
			this.Height = tblOuter.Height;
		}
		
		//private List<SoldProductInfo> ParseProducts(PacketInfo pi)
		//{
		//    Trace.Assert(null != pi);

		//    List<SoldProductInfo> products = new List<SoldProductInfo>();
		
		//    string[] itemInfos = pi.ProductLongString.Split('★');
		//    for (int i = 0; i < itemInfos.Length; i++)
		//    {
		//        string itemInfo = itemInfos[i];
		//        string[] infos = itemInfo.Split('☆');
		//        if (infos.Length < 3)
		//            continue;

		//        if (infos[0].Contains("加强包装费"))
		//            continue;
		//        if (infos[0].Contains("气囊加固"))
		//            continue;

		//        ProductInfo p = ProductInfo.Match(infos[0]);
		//        if (null == p)
		//            continue;
		//        SoldProductInfo product = new SoldProductInfo(p);

		//        product.Count = int.Parse(infos[2]);
		//        product.Status = (Order.OrderStatus)Enum.Parse(typeof(Order.OrderStatus), infos[3]);
		//        products.Add(product);
		//    }
			
		//    return products;
		//}
		
		private string RemoveStartingProvinceCity(string address, string province, string city1, string city2)
		{
			address = address.Trim();
		
			string shortProvince = province.Substring(0, province.Length - 1); // 去掉尾巴上的"省"、"市".
			
			while (true)
			{
				if (!string.IsNullOrEmpty(shortProvince) && address.StartsWith(shortProvince))
				{
					address = address.Substring(shortProvince.Length, address.Length - shortProvince.Length).Trim();
				
					if (address.StartsWith("省 ") || address.StartsWith("市 "))
						address = address.Substring(1, address.Length - 1).Trim();
				}
				else if (!string.IsNullOrEmpty(city1) && address.StartsWith(city1))
				{
					address = address.Substring(city1.Length, address.Length - city1.Length).Trim();
				}
				else if (!string.IsNullOrEmpty(city2) && address.StartsWith(city2))
				{
					address = address.Substring(city2.Length, address.Length - city2.Length).Trim();
				}
				else
				{
					break;
				}
			}

			return address;
		}

		private void txtRecipientName_TextChanged(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtRecipientName.Text))
				txtPinyinRecipientName.Text = string.Empty;
			else
				txtPinyinRecipientName.Text = string.Format(
				"{0} {1}",
				HanZiToPinYin.Convert(txtRecipientName.Text.Substring(0, 1)),
				HanZiToPinYin.Convert(txtRecipientName.Text.Substring(1, txtRecipientName.Text.Length - 1)));

			txtRecipientName.BackColor = ContainInvalidChar(txtRecipientName.Text.Trim()) ? Color.Tomato : Color.White;
		}
		
		private bool ContainInvalidChar(string recipientName)
		{
			if (string.IsNullOrEmpty(recipientName))
				return false;
		
			if (recipientName.Contains("先生"))
				return true;
			if (recipientName.Contains("小姐"))
				return true;
			if (HanZiToPinYin.Convert(recipientName).Contains("Zuo"))
				return true;
			
			Regex r = new Regex("[A-Za-z]");
			Match m = r.Match(recipientName);
			if (m.Success)
				return true;
			
			return false;
		}

		private void txtProvinceCity_TextChanged(object sender, EventArgs e)
		{
			txtPinyinProvinceCity.Text = string.Empty;
			if (!string.IsNullOrEmpty(txtProvinceCity.Text))
			{
				string[] infos = txtProvinceCity.Text.Split(' ');
				foreach (string info in infos)
					txtPinyinProvinceCity.Text += " " + HanZiToPinYin.Convert(info);
				txtPinyinProvinceCity.Text = txtPinyinProvinceCity.Text.Trim();
			}
		}

		private void txtStreetAddress_TextChanged(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtStreetAddress.Text))
				txtPinyinStreetAddress.Text = string.Empty;
			else
				txtPinyinStreetAddress.Text = HanZiToPinYin.Convert(txtStreetAddress.Text);
		}

		public PacketInfo PacketInfo
		{
			get { return _packetInfo; }
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Pen p = new Pen(Color.FromArgb(0x60, 0x60, 0x60));
			Rectangle r = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
			e.Graphics.DrawRectangle(p, r);
		}
		
		public void UpdatePacketInfo()
		{
			_packetInfo.ProductLongString = txtProduct.Text;
			_packetInfo.Type = ((Packet)cboPackets.SelectedItem).Type;
			_packetInfo.Weight = ((Packet)cboPackets.SelectedItem).Weight;
			_packetInfo.Price = ((Packet)cboPackets.SelectedItem).Price;

			_packetInfo.FullAddress = txtFullAddress.Text.Trim();
			_packetInfo.PhoneNumber = txtPhoneNumber.Text.Trim();
			_packetInfo.RecipientNameCn = txtRecipientName.Text.Trim();
			_packetInfo.RecipientNameEn = txtPinyinRecipientName.Text.Trim();
			_packetInfo.ProvinceCityCn = txtProvinceCity.Text.Trim();
			_packetInfo.ProvinceCityEn = txtPinyinProvinceCity.Text.Trim();
			_packetInfo.AddressCn = txtStreetAddress.Text.Trim();
			_packetInfo.AddressEn = txtPinyinStreetAddress.Text.Trim();
			_packetInfo.PostCode = txtPostCode.Text;
			
			_packetInfo.ExportItems.Clear();
			// Modified by KK on 2016/12/12.
			// 把报关信息用于保存ems直邮的身份证信息.
			foreach (string line in txtExportInfo.Lines)
			{
			    if (string.IsNullOrEmpty(line.Trim()))
			        continue;
				
			    _packetInfo.ExportItems.Add(new ExportItem(line));
			}
		}

		private void txtProduct_TextChanged(object sender, EventArgs e)
		{
			txtProduct.Height = 19 + (Math.Max(txtProduct.Lines.Length, 1) - 1) * 13;
			tblOuter.Height = pnlPinyinAddress.Top + tblPinyinAddress.Height + 2; // 修正尺寸. 不知道为啥各panel都是autosize最后尺寸还是有误差 -_-|||
			this.Height = tblOuter.Height;
			this.Refresh();
			
			if (txtProduct.Text.Contains("；"))
				txtProduct.BackColor = Color.Tomato;
			else
				txtProduct.BackColor = Color.White;
		}

		private void txtExportInfo_TextChanged(object sender, EventArgs e)
		{
			txtExportInfo.Height = 19 + (Math.Max(txtExportInfo.Lines.Length, 1) - 1) * 13;
			tblOuter.Height = pnlPinyinAddress.Top + tblPinyinAddress.Height + 2; // 修正尺寸. 不知道为啥各panel都是autosize最后尺寸还是有误差 -_-|||
			this.Height = tblOuter.Height;
			this.Refresh();
		}

		private void btnAddExportInfo_Click(object sender, EventArgs e)
		{
			ContextMenu menu = new ContextMenu();
			foreach (ExportItem ei in ExportItem.ExportItems)
			{
				ExportItemMenuItem mi = new ExportItemMenuItem(ei);
				mi.Click += new EventHandler(mi_Click);
				menu.MenuItems.Add(mi);
			}
			menu.Show(btnAddExportInfo, new Point(0, btnAddExportInfo.Height));
		}

		void mi_Click(object sender, EventArgs e)
		{
			ExportItemMenuItem mi = sender as ExportItemMenuItem;
			if (txtExportInfo.Text.Length > 0)
				txtExportInfo.Text += "\r\n";
			txtExportInfo.Text += mi.ExportItem.ExportItemString;
		}
		
		//public List<ExportItem> GetExportItems()
		//{
		//    if (txtExportInfo.Text.Trim().Length <= 0)
		//        return null;
			
		//    List<ExportItem> items = new List<ExportItem>();
		//    foreach (string line in txtExportInfo.Lines)
		//        items.Add(new ExportItem(line));
			
		//    return items;
		//}
	}
}