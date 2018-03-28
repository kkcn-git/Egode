using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using OrderParser;

namespace Egode
{
	public partial class PacketDetailsControl : UserControl
	{
		private PacketInfo _packetInfo;
	
		public PacketDetailsControl(PacketInfo pi, int index)
		{
			_packetInfo = pi;
			InitializeComponent();
			lblIndex.Text = index.ToString();

			// Generate description for products.
			StringBuilder sbProducts = new StringBuilder();
			string[] itemInfos = _packetInfo.ProductInfo.Split('★');
			for (int i = 0; i < itemInfos.Length; i++)
			{
				string itemInfo = itemInfos[i];
				string[] infos = itemInfo.Split('☆');
				if (infos.Length < 3)
					continue;

				bool cancelled = false;
				if (infos.Length >= 4)
					cancelled = (((Order.OrderStatus)Enum.Parse(typeof(Order.OrderStatus), infos[3])) == Order.OrderStatus.Closed);
				if (cancelled)
					continue;

				if (infos[0].Contains("加强包装费"))
					continue;
				if (infos[0].Contains("气囊加固"))
					continue;

				sbProducts.Append(string.Format("{0}; {1}\r\n", Order.SimplifyItemSubject(infos[0], true).Trim(), infos[2])); // [0]:title, [1]:amount.
			}

			if (sbProducts.Length > 2 && sbProducts.ToString().EndsWith("\r\n"))
				sbProducts.Remove(sbProducts.Length-2, 2); // remove return at the end of string.
			txtProduct.Text = sbProducts.ToString();
			txtProduct.Height = txtProduct.PreferredSize.Height;

			cboPackets.Items.Add(new Packet(PacketTypes.Rainbow, 6000, 265));
			cboPackets.Items.Add(new Packet(PacketTypes.Rainbow, 8000, 275));
			cboPackets.Items.Add(new Packet(PacketTypes.Rainbow, 9000, 285));
			cboPackets.Items.Add(new Packet(PacketTypes.Rainbow, 10000, 300));
			cboPackets.Items.Add(new Packet(PacketTypes.Rainbow, 15000, 350));
			cboPackets.Items.Add(new Packet(PacketTypes.Rainbow, 20000, 420));
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
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 11500, 334));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 12000, 344));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 12500, 354));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 13000, 364));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 14000, 374));
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 15000, 394));
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
			txtPhoneNumber.Text = _packetInfo.PhoneNumber;

			try // If error occurs let operator edit address itself.
			{
				// Get post code.
				string postCode = string.Empty;
				Regex regex = new Regex(@"(\(\d{6}\))|(,\d{6}$)");
				Match m = regex.Match(_packetInfo.FullAddress);
				//Trace.Assert(m.Success);
				if (m.Success)
				{
					postCode = m.Value;
					postCode = postCode.Replace("(", string.Empty).Replace(")", string.Empty).Replace(",", string.Empty);
					txtPostCode.Text = postCode;
				}

				// Analyse address. Get information for original address.
				string[] recipientInfos = _packetInfo.FullAddress.Replace(m.Value, string.Empty).Trim().Split(',');
				txtRecipientName.Text = recipientInfos[0].Trim();

				// Get address details.
				string[] addressInfos = recipientInfos[recipientInfos.Length - 1].Split(' ');
				string province = string.Empty;
				string city1 = string.Empty;
				string city2 = string.Empty;
				foreach (string s in addressInfos)
				{
					string ss = s.Trim();
					
					if (ss.EndsWith("省") || ss.Equals("北京") || ss.Equals("上海") || ss.Equals("天津") || ss.Equals("重庆"))
						province = ss;
					if (ss.EndsWith("市") && !ss.StartsWith(province))
					{
						if (string.IsNullOrEmpty(city1))
							city1 = ss;
						else
							city2 = ss;
					}
				}
				if (!province.EndsWith("省") && !province.EndsWith("市")) // 直辖市.
					province += "市";
				txtProvinceCity.Text = string.Format("{0} {1} {2}", province, city1, city2);
				
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
			catch {}
		}
		
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
			_packetInfo.ProductInfo = txtProduct.Text;
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
		}
	}
}