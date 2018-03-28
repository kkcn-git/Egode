using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using OrderParser;
using Egode.WebBrowserForms;

namespace Egode
{
	public partial class OrderDetailsControl : UserControl
	{
		#region class OrderStatusIconPath
		private class OrderStatusIconPath
		{
			private static string _statusPathDeal = string.Empty;
			private static string _statusPathDhl = string.Empty;
			private static string _statusPathLocalDhl = string.Empty;
			private static string _statusPathPaid = string.Empty;
			private static string _statusPathSent = string.Empty;
			private static string _statusPathPartialSent = string.Empty;
			private static string _statusPathDelivered = string.Empty;
			private static string _statusPathClosed = string.Empty;
			
			public static string Get(Order o)
			{
				switch (o.Status)
				{
					case Order.OrderStatus.Deal:
						return GetPath(ref _statusPathDeal, "deal");

					case Order.OrderStatus.Paid:
						if (o.Prepared)
							return GetPath(ref _statusPathDhl, "dhl");
						else if (o.LocalPrepared)
							return GetPath(ref _statusPathLocalDhl, "localdhl");
						else
							return GetPath(ref _statusPathPaid, "paid");
						
					case Order.OrderStatus.Sent:
						return GetPath(ref _statusPathSent, "sent");
							
					case Order.OrderStatus.PartialSent:
						return GetPath(ref _statusPathPartialSent, "partialsent");
					case Order.OrderStatus.Succeeded:
						return GetPath(ref _statusPathDelivered, "delivered");
					case Order.OrderStatus.Closed:
						return GetPath(ref _statusPathClosed, "closed");
				}
				return null;
			}
			
			private static string GetPath(ref string path, string imgResId)
			{
				if (!string.IsNullOrEmpty(path))
					return path;

				ResourceManager rm = new ResourceManager("Egode.Properties.Resources", Assembly.GetExecutingAssembly());
				Image img = (Image)rm.GetObject(imgResId);
				if (null == img)
					return string.Empty;

				path = string.Format("{0}.png", System.IO.Path.GetTempFileName());
				img.Save(path);
				return path;
			}
		}
		#endregion
	
		public delegate void PacketEventHandler(object sender, PacketInfo p);
	
		public event EventHandler OnBuyerClick;
		public event PacketEventHandler OnAddPacket;
		public event EventHandler OnConsignSh;
		private Order _order;
		private int _buyerOrderCount;
		
		private bool _refreshedEditedAddr;

		public OrderDetailsControl(Order order, int buyerOrderCount)
		{
			InitializeComponent();
			
			_buyerOrderCount = buyerOrderCount;
			_order = order;
			_order.OnStatusChanged += new EventHandler(_order_OnStatusChanged);

			//wb.Visible = false;
			wb.ScrollBarsEnabled = false;
			wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);
			wb.Navigating += new WebBrowserNavigatingEventHandler(wb_Navigating);
			InitializeHtmlView();

			// ...
			btnPreparePacket.Visible = false;
			
			if (_order.Remark.Contains("#直邮"))
				tblHeaderInfo.BackColor = Color.FromArgb(0xff, 127, 191, 127);
			else if (_order.Remark.Contains("#现货"))
				tblHeaderInfo.BackColor = Color.FromArgb(0xff, 255, 127, 127);
			else
				tblHeaderInfo.BackColor = Color.FromArgb(0xe0, 0xe0, 0xe0);
			
			// details.
			orderId.Order = _order;
			orderId.ConsignVisible = _order.Status == (Order.OrderStatus.Paid);
			orderId.OnConsignSh += new EventHandler(orderId_OnConsignSh);
			lblDealTime.Text = string.Format("成交时间: {0}", _order.DealTime.ToString("yyyy-MM-dd HH:mm:ss"));
			lblPayingTime.Text = string.Format("付款时间: {0}", _order.PayingTime.Equals(DateTime.MinValue) ? "<未付款>" : _order.PayingTime.ToString("yyyy-MM-dd HH:mm:ss"));
			buyerInfo.BuyerAccount = _order.BuyerAccount;
			buyerInfo.OrderAmount = buyerOrderCount;
			orderMoney.Money = _order.TotalMoney;
			orderMoney.Freight = _order.Freight;
			txtBuyerRemark.Text = _order.BuyerRemark;
			txtRemark.Text = _order.Remark;
			txtAddress.Text = _order.GetFullAddress();
			txtPinyinAddress.Text = HanZiToPinYin.Convert(txtAddress.Text.Substring(0, txtAddress.Text.Length - 8)); // remove (123456) at the end of address.
			txtEditedAddress.Text = _order.EditedRecipientAddress;
			
			if (string.IsNullOrEmpty(_order.BuyerRemark.Trim()))
			{
				lblBuyerRemarkTitle.Visible = false;
				txtBuyerRemark.Visible = false;
			}
			
			//// title color.
			//switch (_order.Status)
			//{
			//    case Order.OrderStatus.Deal:
			//        tblHeaderInfo.BackColor = Color.FromArgb(248, 200, 113);
			//        break;
				
			//    case Order.OrderStatus.Paid:
			//        tblHeaderInfo.BackColor = Color.FromArgb(120, 222, 120);
			//        break;
				
			//    case Order.OrderStatus.Sent:
			//        tblHeaderInfo.BackColor = Color.FromArgb(199, 228, 255);
			//        break;
				
			//    case Order.OrderStatus.Succeeded:
			//        tblHeaderInfo.BackColor = Color.FromArgb(196, 196, 196);
			//        break;
				
			//    case Order.OrderStatus.Closed:
			//        tblHeaderInfo.BackColor = Color.FromArgb(196, 196, 196);
			//        break;
			//}
			
			// info about address.
			if (string.IsNullOrEmpty(_order.EditedRecipientAddress))
			{
				lblEditedAddressTitle.Visible = false;
				txtEditedAddress.Visible = false;
				btnGetFullEditedAddress.Visible = false;
			}
			else 
			{
				// If edited address exists the address is not full address.
				// So, it is not necessary to show pinyin address.
				lblPinyinAddressTitle.Visible = false;
				txtPinyinAddress.Visible = false;
			}

			// items.
			string[] items = _order.Items.Split('★');
			for (int i = 0; i < items.Length; i++)
			{
				string item = items[i];
				string[] infos = item.Split('☆');
				if (infos.Length < 3)
					continue;
				
				if (string.IsNullOrEmpty(infos[0]))
					Trace.WriteLine("null product found!!!");
				
				OrderParser.Order.OrderStatus status = Order.OrderStatus.Succeeded;
				if (infos.Length >= 4)
					status = ((Order.OrderStatus)Enum.Parse(typeof(Order.OrderStatus), infos[3]));
				
				productList.AddProduct(
					//Order.SimplifyItemSubject(infos[0]), 
					infos[0],
					float.Parse(infos[1]), int.Parse(infos[2]), status);
			}
			
			// packet.
			float netWeight = _order.GetNetWeight();
			lblNetWeight.Text = string.Format("净重: {0}kg", (netWeight / 1000).ToString("0.0"));
			cboPackets.Items.Add(new Packet(PacketTypes.Unknown, 0, 0));
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
			cboPackets.Items.Add(new Packet(PacketTypes.Ouhua, 11000, 324));
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
			
			if (4800 == netWeight)
				cboPackets.SelectedIndex = GetPacketIndex(PacketTypes.Supermarket, 7);
			else if (6400 == netWeight)
				cboPackets.SelectedIndex = GetPacketIndex(PacketTypes.Rainbow, 8);
			else if (8000 == netWeight)
				cboPackets.SelectedIndex = GetPacketIndex(PacketTypes.Rainbow, 10);
			else if (8400 == netWeight)
				cboPackets.SelectedIndex = GetPacketIndex(PacketTypes.Rainbow, 10);
			else
				cboPackets.SelectedIndex = 0;
			
			// autosize me.
			//this.Height = tblDetails.Top + tblPacket.Bottom + this.Padding.Bottom;
			
			//Trace.WriteLine(_order.RecipientAddress);
			//AddressParser ap = new AddressParser(_order.RecipientAddress);
			//Trace.WriteLine(string.Format("{0}#{1}#{2}#{3}#{4}", ap.Province, ap.City1, ap.City2, ap.District, ap.StreetAddress));
			//Trace.WriteLine("");
		}

		void wb_Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			if (e.Url.AbsoluteUri.ToLower().Equals("about:blank"))
				return;
		
			e.Cancel = true;

			if (e.Url.AbsoluteUri.StartsWith("about:buyer?"))
			{
				if (null != this.OnBuyerClick)
					this.OnBuyerClick(this, EventArgs.Empty);
			}
			else
			{
				Process.Start(e.Url.AbsoluteUri);
			}
		}

		void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			HtmlElement btnConsignDe = wb.Document.GetElementById("btnConsignDe");
			if (null != btnConsignDe)
				btnConsignDe.AttachEventHandler("onclick", new EventHandler(btnConsignDe_Click));
			
			HtmlElement btnConsignSh = wb.Document.GetElementById("btnConsignSh");
			if (null != btnConsignSh)
				btnConsignSh.AttachEventHandler("onclick", new EventHandler(btnConsignSh_Click));

			HtmlElement btnGetFullEditedAddr = wb.Document.GetElementById("btnGetFullEditedAddr");
			if (null != btnGetFullEditedAddr)
				btnGetFullEditedAddr.AttachEventHandler("onclick", new EventHandler(btnGetFullEditedAddr_Click));
			
			HtmlElement tblContainer = wb.Document.GetElementById("tblContainer");
			

			//Trace.WriteLine(wb.Document.Body.ClientRectangle.Height);
			//wb.Location = new Point(1, 1);
			//wb.Size = new Size(this.Width-2, wb.Document.Body.ScrollRectangle.Height);
			//this.Height = wb.Height + this.Padding.Bottom;
		}
		
		// Added by KK on 2013/09/19. automn day!
		void InitializeHtmlView()
		{
			string normalTdStyle = "\"font-family:microsoft yahei;font-size:12px;color:#707070;white-space:nowrap;text-align:left;vertical-align:top;overflow:hidden;word-break:break-all\"";
			string normalTdStyleAlighRight = "\"font-family:microsoft yahei;font-size:12px;color:#707070;white-space:nowrap;text-align:right;vertical-align:top;overflow:hidden;word-break:break-all\"";
			string normalTdStyleBottomAlign = "\"font-family:microsoft yahei;font-size:12px;color:#707070;white-space:nowrap;text-align:left;vertical-align:bottom;overflow:hidden;word-break:break-all\"";
			string contentTdStyle = "\"font-family:microsoft yahei;font-size:12px;color:#404040;white-space:nowrap;text-align:left;vertical-align:top;overflow:hidden;word-break:break-all;\"";
			//string contentTextStyle = "\"font-family:microsoft yahei;font-size:12px;color:#404040;\"";
			string contentSmallTextStyle = "\"font-family:microsoft yahei;font-size:8px;color:#404040;padding-top:auto;padding-bottom:auto\"";
			string contentRedStyle = "\"font-family:microsoft yahei;font-size:12px;color:#ff0000;white-space:nowrap;text-align:left;vertical-align:top;overflow:hidden;word-break:break-all;\"";
			
			string titleBackColor = "#e0e0e0";
			if (_order.Remark.Contains("#直邮"))
				titleBackColor = "#c0e0c0";
			else if (_order.Remark.Contains("#现货"))
				titleBackColor = "#ffa0a0";
		
			StringBuilder sb = new StringBuilder();
			sb.Append("<body style=\"margin:0px\">");
			sb.Append("<span color=\"#707070\"><table id=tblContainer border=0 width=\"100%\" style=\"border-collapse:collapse;padding:0px;cell-padding:0px;border-spacing:0px\">");
			sb.Append("<tr><td>");
				sb.Append(string.Format("<table width=100% style=\"background-color:{0};border-collapse:collapse;padding:0px;cell-padding:0px;border-spacing:0px\">", titleBackColor));
				sb.Append("<tr>");
					sb.Append(string.Format("<td><img src=\"{0}\" /></td>", OrderStatusIconPath.Get(_order))); // icon.
					sb.Append(string.Format("<td width=165px style={0}>订单编号: <a href='http://trade.taobao.com/trade/detail/trade_item_detail.htm?bizOrderId={1}'>{1}</a></td>", normalTdStyleBottomAlign, _order.OrderId)); // order id.
					sb.Append("<td width=16px><input type=\"button\" id=\"btnConsignDe\" style=\"background-color:green;width:15px;height:15px\" /></td>");
					sb.Append("<td width=16px><input type=\"button\" id=\"btnConsignSh\" style=\"background-color:red;width:15px;height:15px\" /></td>");
					sb.Append("<td width=16></td>");
					sb.Append(string.Format("<td width=180px style={0}>成交时间: <font color=#404040>{1}</font></td>", normalTdStyleBottomAlign, _order.DealTime.ToString("yyyy-MM-dd HH:ss:mm")));
					sb.Append("<td width=16></td>");
					sb.Append(string.Format("<td width=180px style={0}>付款时间: <font color=#404040>{1}</font></td>", normalTdStyleBottomAlign, _order.PayingTime.ToString("yyyy-MM-dd HH:ss:mm")));
					sb.Append("<td width=16></td>");
					sb.Append(string.Format("<td width=180 style={0}>买家账号: <a id=\"linkBuyerAccount\" href=\"buyer?{1}\">{1}</a><font color={3}>({2})</font></td>", normalTdStyleBottomAlign, _order.BuyerAccount, _buyerOrderCount, _buyerOrderCount > 1 ? "a448a4" : "#404040"));
					sb.Append("<td width=16></td>");
					sb.Append(string.Format("<td style={0}>金额: <font style=\"font-size:14px;color=#ff4000\"><strong>{1}</strong></font><font color=#404040>({2})</font></td>", normalTdStyleBottomAlign, _order.TotalMoney.ToString("0.00"), _order.Freight.ToString("0.00")));
				sb.Append("</tr>");
				sb.Append("</table>");
			sb.Append("</td></tr>");

			sb.Append("<tr><td>");
				sb.Append(string.Format("<table width=100% style=\"border-collapse:collapse;padding:0px;cell-padding:0px;border-spacing:0px;\">", titleBackColor));
				sb.Append("<tr>");
					if (!string.IsNullOrEmpty(_order.BuyerRemark))
					{
						sb.Append("<td width=17></td>");
						sb.Append(string.Format("<td width=55px style={0}>买家留言:</td>", normalTdStyleAlighRight));
						sb.Append(string.Format("<td style={0}>{1}</td>", contentRedStyle, _order.BuyerRemark)); 
					}
				sb.Append("</tr>");
				sb.Append("<tr>");
					sb.Append("<td width=17></td>");
					sb.Append(string.Format("<td width=55px style={0}>客服备注:</td>", normalTdStyleAlighRight)); 
					sb.Append(string.Format("<td style={0}>{1}</td>", contentTdStyle, _order.Remark)); 
				sb.Append("</tr>");
				
				string fullAddress = _order.GetFullAddress();
				
				sb.Append("<tr>");
					sb.Append("<td width=17></td>");
					sb.Append(string.Format("<td width=55px style={0}>收货地址:</td>", normalTdStyleAlighRight)); 
					sb.Append(string.Format("<td style={0}>{1}</td>", contentTdStyle, fullAddress)); 
				sb.Append("</tr>");

				//if (string.IsNullOrEmpty(_order.EditedRecipientAddress))
				//{
				//    sb.Append("<tr>");
				//    sb.Append("<td width=17></td>");
				//    sb.Append(string.Format("<td width=55px style={0}>拼音地址:</td>", normalTdStyleAlighRight));
				//    sb.Append(string.Format("<td width={2}px style={0}>{1}</td>", contentTdStyle, HanZiToPinYin.Convert(fullAddress.Substring(0, fullAddress.Length - 8)) + HanZiToPinYin.Convert(fullAddress.Substring(0, fullAddress.Length - 8)).Length.ToString(), this.Width - 100)); 
				//    sb.Append("</tr>");
				//}
				
				if (!string.IsNullOrEmpty(_order.EditedRecipientAddress))
				{
					sb.Append("<tr>");
						sb.Append("<td width=17></td>");
						sb.Append(string.Format("<td width=55px style={0}>新地址:</td>", normalTdStyleAlighRight));
						sb.Append(string.Format("<td style={0}><font color={1}>{2}</font> ", contentRedStyle, _refreshedEditedAddr ? "#008000" : "#404040", _order.EditedRecipientAddress)); 
						sb.Append(string.Format("<input style={0} type=\"button\" id=\"btnGetFullEditedAddr\" value=\"\" style=\"background-color:#0080ff;width:30px;height:18px\" /></td>", contentSmallTextStyle));
					sb.Append("</tr>");
				}

				sb.Append("<tr>");
				sb.Append("<td width=17></td>");
				sb.Append(string.Format("<td width=55px style={0}>商品列表:</td>", normalTdStyleAlighRight));
				sb.Append(string.Format("<td width={1}px style={0}>", contentTdStyle, this.Width - 100));
					sb.Append("<table width=100% style=\"border-collapse:collapse;padding:0px;cell-padding:0px;border-spacing:0px\">");

					// items.
					string[] items = _order.Items.Split('★');
					for (int i = 0; i < items.Length; i++)
					{
						string item = items[i];
						string[] infos = item.Split('☆');
						if (infos.Length < 3)
							continue;

						if (string.IsNullOrEmpty(infos[0]))
							Trace.WriteLine("null product found!!!");

						OrderParser.Order.OrderStatus status = Order.OrderStatus.Succeeded;
						if (infos.Length >= 4)
							status = ((Order.OrderStatus)Enum.Parse(typeof(Order.OrderStatus), infos[3]));
						
						sb.Append("<tr>");
						sb.Append(string.Format(
							"<td width=520px style={3}><font color={4}>{0}</font> <font color={2}>({1})</font></td>",
							infos[0],
							GetStautsDesc(status), GetStatusDescColor(status),
							normalTdStyle, status == Order.OrderStatus.Closed ? "#d0d0d0" : "#4169e1"));
						sb.Append(string.Format("<td width=60px style={1}><font color=#ff4000>{0}</font></td>", float.Parse(infos[1]).ToString("0.00"), normalTdStyle));
						sb.Append(string.Format("<td style={1}><font color=#606060><strong>{0}</strong></font></td>", int.Parse(infos[2]), normalTdStyle));
						sb.Append("</tr>");

						productList.AddProduct(
							//Order.SimplifyItemSubject(infos[0]), 
							infos[0],
							float.Parse(infos[1]), int.Parse(infos[2]), status);
					}
					
					sb.Append("</table>");
				sb.Append("</td>");
				sb.Append("</tr>");

				sb.Append("</table>");
			sb.Append("</td></tr>");

			sb.Append("</table></span>");
			sb.Append("</body>");
			
			wb.DocumentText = sb.ToString();

			int height = 69;
			if (!string.IsNullOrEmpty(_order.EditedRecipientAddress))
				height += 19;
			if (!string.IsNullOrEmpty(_order.BuyerRemark))
				height += 19;
			height += 19 * (items.Length - 1);
			wb.Height = height;
			this.Height = wb.Height + this.Padding.Bottom;
		}
		
		private static string GetStautsDesc(Order.OrderStatus status)
		{
			switch (status)
			{
				case OrderParser.Order.OrderStatus.Deal:
					return "未付款";

				case OrderParser.Order.OrderStatus.Paid:
					return "已付款";

				case OrderParser.Order.OrderStatus.Sent:
					return "已发货";

				case OrderParser.Order.OrderStatus.Succeeded:
					return "交易成功";

				case OrderParser.Order.OrderStatus.Closed:
					return "已取消";
			}
			return string.Empty;
		}
		
		private static string GetStatusDescColor(Order.OrderStatus status)
		{
			switch (status)
			{
				case OrderParser.Order.OrderStatus.Deal:
					return "#808080";

				case OrderParser.Order.OrderStatus.Paid:
					return "#ff4000";

				case OrderParser.Order.OrderStatus.Sent:
					return "#800080";

				case OrderParser.Order.OrderStatus.Succeeded:
					return "#008000";

				case OrderParser.Order.OrderStatus.Closed:
					return "#d0d0d0";
			}
			return "#404040";
		}

		void btnGetFullEditedAddr_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			RefreshFullEditedAddress();			
			Cursor.Current = Cursors.Default;
		}

		void btnConsignDe_Click(object sender, EventArgs e)
		{
			ConsignForm cf = new ConsignForm(_order.OrderId);
			cf.ShowDialog(this.FindForm());
		}

		void btnConsignSh_Click(object sender, EventArgs e)
		{
			orderId_OnConsignSh(sender, e);
		}
		void orderId_OnConsignSh(object sender, EventArgs e)
		{
			if (null != this.OnConsignSh)
				this.OnConsignSh(this, EventArgs.Empty);
		}
		
		private static Image GetStatusImage(Order o)
		{
			ResourceManager rm = new ResourceManager("Egode.Properties.Resources", Assembly.GetExecutingAssembly());
			switch (o.Status)
			{
			    case Order.OrderStatus.Deal:
			        return (Image)rm.GetObject("deal");
			    case Order.OrderStatus.Paid:
					if (o.Prepared)
						return (Image)rm.GetObject("dhl");
					else if (o.LocalPrepared)
						return (Image)rm.GetObject("localdhl");
					else
						return (Image)rm.GetObject("paid");
				case Order.OrderStatus.Sent:
					return (Image)rm.GetObject("sent");
				case Order.OrderStatus.PartialSent:
					return (Image)rm.GetObject("partialsent");
				case Order.OrderStatus.Succeeded:
			        return (Image)rm.GetObject("delivered");
			    case Order.OrderStatus.Closed:
			        return (Image)rm.GetObject("closed");
			}
			return null;
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Pen p = new Pen(Color.FromArgb(0x60, 0x60, 0x60));
			Rectangle r = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
			e.Graphics.DrawRectangle(p, r);

			//Image img = GetStatusImage(_order);
			//e.Graphics.DrawImage(img, new Point(0, 0));
		}

		private void tblHeaderInfo_Paint(object sender, PaintEventArgs e)
		{
			try
			{
			    Image img = GetStatusImage(_order);
			    e.Graphics.DrawImage(img, 2, 4, 18, 18);
			}
			catch (Exception ex)
			{
			    Trace.Write(ex);
			}
		}

		void _order_OnStatusChanged(object sender, EventArgs e)
		{
			//wb.Navigate("about:blank"); // refresh.
			tblHeaderInfo.Refresh();
		}
		
		public Order Order
		{
			get { return _order; }
		}
		
		public string FullEditedAddress
		{
			get { return txtEditedAddress.Text; }
		}
		
		private void buyerInfo_OnBuyerClick(object sender, EventArgs e)
		{
			if (null != this.OnBuyerClick)
				this.OnBuyerClick(this, EventArgs.Empty);
		}

		private void btnGetFullEditedAddress_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			RefreshFullEditedAddress();			
			Cursor.Current = Cursors.Default;
		}
		
		public void RefreshFullEditedAddress()
		{
			OrderDetailsPageWebBrowserForm odpf = new OrderDetailsPageWebBrowserForm(_order.OrderId);
			if (DialogResult.OK == odpf.ShowDialog(this.FindForm()))
			{
				string html = odpf.Html;
				Regex r = new Regex(@"<th>新收货地址：</th>\s*<td>(.*)</td>");
		        Match m = r.Match(html);
		        if (m.Success)
		        {
					string fullEditedAddress = m.Groups[1].Value.Trim();
					fullEditedAddress = fullEditedAddress.Replace(" ，", ",");
					fullEditedAddress = fullEditedAddress.Replace(",,", ",");
					txtEditedAddress.Text = fullEditedAddress;
					_order.EditedRecipientAddress = fullEditedAddress;
					_refreshedEditedAddr = true;
					wb.Navigate("about:blank"); // cause reload html.
					
					fullEditedAddress = fullEditedAddress.Substring(0, fullEditedAddress.Length - 6); // remove last post code to translate to pinyin.
					if (fullEditedAddress.EndsWith(","))
						fullEditedAddress = fullEditedAddress.Substring(0, fullEditedAddress.Length - 1);
					txtPinyinAddress.Text = HanZiToPinYin.Convert(fullEditedAddress);
					lblPinyinAddressTitle.Visible = true;
					txtPinyinAddress.Visible = true;
					
					txtEditedAddress.SelectionStart = 0;
					txtEditedAddress.SelectionLength = txtEditedAddress.Text.Length;
					txtEditedAddress.Focus();
					
					// autosize me.
					this.Height = tblDetails.Top + tblPacket.Bottom + this.Padding.Bottom;
				}
			}
		}

		private void txtAddress_TextChanged(object sender, EventArgs e)
		{
			txtPinyinAddress.Text = HanZiToPinYin.Convert(txtAddress.Text);
		}
		
		private int GetPacketIndex(PacketTypes packetType, int weight)
		{
			for (int i = 0; i < cboPackets.Items.Count; i++)
			{
				Packet p = (Packet)cboPackets.Items[i];
				if (p.Type == packetType && p.Weight == weight)
					return i;
			}

			return 0;
		}

		private void btnAddPacket_Click(object sender, EventArgs e)
		{
			if (null != this.OnAddPacket)
			{
				// 提醒
				if (_order.LocalPrepared)
				{
					DialogResult dr = MessageBox.Show(
						this.FindForm(),
						"此订单已添加包裹单.\n是否要为此订单生成多个包裹单?", this.FindForm().Text,
						MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (DialogResult.No == dr)
						return;
				}
			
				// Have not get full edited address yet
				if (!string.IsNullOrEmpty(_order.EditedRecipientAddress.Trim()) && txtEditedAddress.Text.Equals(_order.EditedRecipientAddress.Trim()))
				{
					MessageBox.Show(
						this.FindForm(),
						"收货地址发生修改.\n导出数据无法获得完整修改后的地址, 需要访问订单详情页面获得完整地址.",
						this.FindForm().Text,
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					btnGetFullEditedAddress_Click(btnGetFullEditedAddress, EventArgs.Empty);
				}

				Packet p = (Packet)cboPackets.SelectedItem;
				string fullAddress = !string.IsNullOrEmpty(txtEditedAddress.Text) ? txtEditedAddress.Text : txtAddress.Text;
				string phoneNumber = string.IsNullOrEmpty(_order.MobileNumber) ? _order.PhoneNumber : _order.MobileNumber;
				PacketInfo pi = new PacketInfo(_order.OrderId, p.Type, p.Weight, p.Price, fullAddress, phoneNumber, _order.PhoneNumber, _order.Items);
				this.OnAddPacket(this, pi);
				_order.LocalPrepare();
			}
		}

		private void cboPackets_SelectedIndexChanged(object sender, EventArgs e)
		{
			Packet p = (Packet)cboPackets.SelectedItem;
			btnAddPacket.Enabled = (p.Weight > 0);
		}

		private void btnMarkPrepared_Click(object sender, EventArgs e)
		{
			_order.LocalPrepare();
		}

		private void OrderDetailsControl_SizeChanged(object sender, EventArgs e)
		{
			wb.Location = new Point(1, 1);
			wb.Width = this.Width - 2;
		}
	}
}