using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using Egode.WebBrowserForms;
using OrderLib;

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
			private static string _statusPathNb = string.Empty;
			private static string _statusPathLocalNb = string.Empty;
			private static string _statusPathPaid = string.Empty;
			private static string _statusPathSent = string.Empty;
			private static string _statusPathPartialSent = string.Empty;
			private static string _statusPathDelivered = string.Empty;
			private static string _statusPathClosed = string.Empty;
			private static string _statusPathYunda = string.Empty;
			
			private static string _warningPath = string.Empty;
			
			public static string Get(Order o)
			{
				switch (o.Status)
				{
					case Order.OrderStatus.Deal:
						return GetPath(ref _statusPathDeal, "deal");

					case Order.OrderStatus.Paid:
						goto case Order.OrderStatus.PartialSent;
						break;
					case Order.OrderStatus.PartialSent:
						if (o.Prepared)
							return GetPath(ref _statusPathDhl, "dhl");
						else if (o.LocalPrepared)
							return GetPath(ref _statusPathLocalDhl, "localdhl");
						else if (o.PreparedNingbo)
							return GetPath(ref _statusPathNb, "nb");
						else if (o.LocalPreparedNingbo)
							return GetPath(ref _statusPathLocalNb, "localnb");
						else if (o.YundaPrepared) // Added by KK on 2017/10/14.
							return GetPath(ref _statusPathYunda, "yunda");
						else if (o.Status == Order.OrderStatus.Paid)
							return GetPath(ref _statusPathPaid, "paid");
						else if (o.Status == Order.OrderStatus.PartialSent)
							return GetPath(ref _statusPathPartialSent, "partialsent");
						break;
						
					case Order.OrderStatus.Sent:
						return GetPath(ref _statusPathSent, "sent");
							
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
			
			// warning.gif
			public static string GetWarningPath()
			{
				if (!string.IsNullOrEmpty(_warningPath))
					return _warningPath;
				
				ResourceManager rm = new ResourceManager("Egode.Properties.Resources", Assembly.GetExecutingAssembly());
				Image img = (Image)rm.GetObject("warning");
				if (null == img)
					return string.Empty;

				_warningPath = string.Format("{0}.gif", System.IO.Path.GetTempFileName());
				img.Save(_warningPath);
				return _warningPath;
			}
		}
		#endregion
	
		public delegate void PacketEventHandler(object sender, PacketInfo p);
	
		public event EventHandler OnBuyerClick;
		public event PacketEventHandler OnAddPacket;
		public event EventHandler OnConsignSh;
		public event EventHandler OnConsignNb;
		private Order _order;
		private int _buyerOrderCount;
		
		private bool _refreshedNewAddr;
		private string _fullNewAddr;
		
		private bool _selectable;
		
		private Rectangle RECT_YUNDA = new Rectangle();
		private Rectangle RECT_ZTO = new Rectangle();
		private Rectangle RECT_MANUAL = new Rectangle();

		public OrderDetailsControl(Order order, int buyerOrderCount)
		{
			InitializeComponent();
			
			_buyerOrderCount = buyerOrderCount;
			_order = order;
			_order.OnStatusChanged += new EventHandler(_order_OnStatusChanged);
			_order.OnTransmissionChanged += new EventHandler(_order_OnTransmissionChanged); // Added by KK on 2017/10/19
			_order.AutoTransmissionProcessor = ShipmentCompanies.Unknown; // Added by KK on 2017/10/19. 默认手动处理.
			_order_OnTransmissionChanged(_order, EventArgs.Empty); // 强制刷新
			
			if (!string.IsNullOrEmpty(_order.EditedRecipientAddress) && !_order.EditedRecipientAddress.Equals("否"))
				_fullNewAddr = _order.EditedRecipientAddress;

			//wb.Visible = false;
			wb.ScrollBarsEnabled = false;
			wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);
			wb.Navigating += new WebBrowserNavigatingEventHandler(wb_Navigating);
			InitializeHtmlView();

			cboPackets.Items.Add(new Packet(PacketTypes.Unknown, 0, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.PostNL, 5000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.PostNL, 5500, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.PostNL, 6000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.PostNL, 6500, 0));
			cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 5000, 0));
			cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 6000, 0));
			cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 7000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.PostNL, 7500, 0));
			cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 8000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.PostNL, 8500, 0));
			cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 9000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.PostNL, 9500, 0));
			cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 10000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.PostNL, 10500, 0));
			cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 11000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.PostNL, 11500, 0));
			cboPackets.Items.Add(new Packet(PacketTypes.Time24_PostNL, 12000, 0));
			//cboPackets.Items.Add(new Packet(PacketTypes.PostNL, 12500, 0));
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
			//cboPackets.Items.Add(new Packet(PacketTypes.Hanslord, 30000, 0));

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
			#region obsoleted code
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
			#endregion
			
			int netWeight = GetNetWeight();
			if (4800 == netWeight)
				cboPackets.SelectedIndex = GetPacketIndex(PacketTypes.Hanslord, 6000);
			else if (6400 == netWeight)
				cboPackets.SelectedIndex = GetPacketIndex(PacketTypes.Hanslord, 8000);
			else if (7200 == netWeight)
				cboPackets.SelectedIndex = GetPacketIndex(PacketTypes.Hanslord, 8500);
			else if (8000 == netWeight)
				cboPackets.SelectedIndex = GetPacketIndex(PacketTypes.Hanslord, 10000);
			else if (8400 == netWeight)
				cboPackets.SelectedIndex = GetPacketIndex(PacketTypes.Hanslord, 10000);
			else if (8800 == netWeight)
				cboPackets.SelectedIndex = GetPacketIndex(PacketTypes.Hanslord, 11000);
			else if (9600 == netWeight)
				cboPackets.SelectedIndex = GetPacketIndex(PacketTypes.Hanslord, 12000);
			else
				cboPackets.SelectedIndex = 0;
			
			//Trace.WriteLine(_order.RecipientAddress);
			//AddressParser ap = new AddressParser(_order.RecipientAddress);
			//Trace.WriteLine(string.Format("{0}#{1}#{2}#{3}#{4}", ap.Province, ap.City1, ap.City2, ap.District, ap.StreetAddress));
			//Trace.WriteLine("");
			
			this.Selectable = false;
		}

		public bool Selectable
		{
			get { return _selectable; }
			set
			{
				if (_selectable != value)
				{
					_selectable = value;
					chkSelected.Visible = _selectable;
					this.OnResize(EventArgs.Empty);
				}
			}
		}
		
		public bool Selected
		{
			get { return chkSelected.Checked; }
			set { chkSelected.Checked = value; }
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
			HtmlElement btnAddComment = wb.Document.GetElementById("btnAddComment");
			if (null != btnAddComment)
				btnAddComment.AttachEventHandler("onclick", new EventHandler(btnAddComment_Click));

			HtmlElement btnConsignDe = wb.Document.GetElementById("btnConsignDe");
			if (null != btnConsignDe)
				btnConsignDe.AttachEventHandler("onclick", new EventHandler(btnConsignDe_Click));

			HtmlElement btnConsignSh = wb.Document.GetElementById("btnConsignSh");
			if (null != btnConsignSh)
				btnConsignSh.AttachEventHandler("onclick", new EventHandler(btnConsignSh_Click));

			HtmlElement btnConsignNb = wb.Document.GetElementById("btnConsignNb");
			if (null != btnConsignNb)
				btnConsignNb.AttachEventHandler("onclick", new EventHandler(btnConsignNb_Click));

			HtmlElement btnGetFullEditedAddr = wb.Document.GetElementById("btnGetFullEditedAddr");
			if (null != btnGetFullEditedAddr)
				btnGetFullEditedAddr.AttachEventHandler("onclick", new EventHandler(btnGetFullEditedAddr_Click));

			// Added by KK on 2015/12/23.			
			HtmlElement imgStatus = wb.Document.GetElementById("imgStatus");
			if (null != imgStatus)
				imgStatus.AttachEventHandler("onclick", new EventHandler(imgStatus_Click));
			
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
			
			string remark = _order.Remark;
			remark = remark.Replace("＃", "#");
			remark = remark.Replace("开盖", "<span style=\"background-color:#b04040;color:#ffffff\">开盖</span>");

			string titleBackColor = "#e0e0e0";
			if (remark.Contains("#直邮"))
				titleBackColor = "#c0e0c0";
			else if (remark.Contains("#现货"))
				titleBackColor = "#ffa0a0";
		
			StringBuilder sb = new StringBuilder();
			sb.Append("<body style=\"margin:0px\">");
			sb.Append("<span color=\"#707070\"><table id=tblContainer border=0 width=\"100%\" style=\"border-collapse:collapse;padding:0px;cell-padding:0px;border-spacing:0px\">");
			sb.Append("<tr><td>");
				sb.Append(string.Format("<table width=100% style=\"background-color:{0};border-collapse:collapse;padding:0px;cell-padding:0px;border-spacing:0px\">", titleBackColor));
				sb.Append("<tr>");
					sb.Append(string.Format("<td width=1px><img id=\"imgStatus\" src=\"{0}\" /></td>", OrderStatusIconPath.Get(_order))); // icon.
					sb.Append(string.Format("<td width=182px style={0}>订单编号: <a href='http://trade.taobao.com/trade/detail/trade_item_detail.htm?bizOrderId={1}'>{1}</a></td>", normalTdStyleBottomAlign, _order.OrderId)); // order id.
					sb.Append("<td width=14px><input type=\"button\" id=\"btnAddComment\" style=\"background-color:#0080ff;width:12px;height:12px\" /></td>");
					if (Settings.Instance.UiType != Settings.UiTypes.Shanghai)
					{
						sb.Append("<td width=14px><input type=\"button\" id=\"btnConsignDe\" style=\"background-color:#408040;width:12px;height:12px\" /></td>");
					}
					if (Settings.Instance.UiType != Settings.UiTypes.Deutschland)
					{
						sb.Append("<td width=14px><input type=\"button\" id=\"btnConsignSh\" style=\"background-color:#ff4040;width:12px;height:12px\" /></td>");
						sb.Append("<td width=14px><input type=\"button\" id=\"btnConsignNb\" style=\"background-color:#ff8010;width:12px;height:12px\" /></td>");
					}
					sb.Append("<td width=16></td>");
					sb.Append(string.Format("<td width=180px style={0}>成交时间: <font color=#404040>{1}</font></td>", normalTdStyleBottomAlign, _order.DealTime.ToString("yyyy-MM-dd HH:mm:ss")));
					sb.Append("<td width=16></td>");
					sb.Append(string.Format("<td width=180px style={0}>付款时间: <font color=#404040>{1}</font></td>", normalTdStyleBottomAlign, _order.PayingTime.ToString("yyyy-MM-dd HH:mm:ss")));
					sb.Append("<td width=16></td>");
					string blacklistWarningString = string.Format("<img src=\"{0}\">", OrderStatusIconPath.GetWarningPath());
					sb.Append(string.Format(
						"<td width=220 style={0}>买家账号: <a id=\"linkBuyerAccount\" href=\"buyer?{1}\">{1}</a><font color={3}>({2})</font>{4}</td>", 
						normalTdStyleBottomAlign, 
						_order.BuyerAccount, _buyerOrderCount, _buyerOrderCount > 1 ? "a448a4" : "#404040", 
						(null != Blacklist.MatchRed(_order)) ? blacklistWarningString : string.Empty));
					sb.Append("<td width=16></td>");
					sb.Append(string.Format("<td style={0}>金额: <font style=\"font-size:14px;color=#ff4000\"><strong>{1}</strong></font><font color=#404040>({2})</font></td>", normalTdStyleBottomAlign, _order.TotalMoney.ToString("0.00"), _order.Freight.ToString("0.00")));
				sb.Append("</tr>");
				sb.Append("</table>");
			sb.Append("</td></tr>");

			sb.Append("<tr><td>");
				sb.Append(string.Format("<table width=100% style=\"border-collapse:collapse;padding:0px;cell-padding:0px;border-spacing:0px;\">", titleBackColor));
					if (!string.IsNullOrEmpty(_order.BuyerRemark))
					{
						sb.Append("<tr>");
						sb.Append("<td width=14px></td>");
						sb.Append(string.Format("<td width=55px style={0}>买家留言:</td>", normalTdStyleAlighRight));
						sb.Append("<td width=2px></td>");
						sb.Append(string.Format("<td style={0}><font color=#ff2020>{1}</font></td>", contentTdStyle, _order.BuyerRemark));
						sb.Append("</tr>");
					}
				sb.Append("<tr>");
				sb.Append("<td width=14px></td>");
				sb.Append(string.Format("<td width=55px style={0}>客服备注:</td>", normalTdStyleAlighRight));
				sb.Append("<td width=2px></td>");
				if (string.IsNullOrEmpty(remark.TrimEnd()))
				{
					List<Order> buyerOrders = MainForm.Instance.GetBuyerOrders(_order.BuyerAccount);
					int cSent=0, cSucceeded=0;
					foreach (Order o in buyerOrders)
					{
						if (o.Status == Order.OrderStatus.Sent || o.Status == Order.OrderStatus.PartialSent)
							cSent++;
						if (o.Status == Order.OrderStatus.Succeeded)
							cSucceeded++;
					}
					string color = (cSent + cSucceeded) > 0 ? "#40a040" : "#b04040";
					remark = string.Format("<span style=\"color:{0}\">[无备注. 已发货订单:{1}, 交易成功订单:{2}]</span>", color, cSent, cSucceeded);
				}
				//remark = remark.Replace("#直邮", "<span style=\"background-color:#60b060;color:#ffffff\">#直邮</span>").Replace("#现货", "<span style=\"background-color:#ff8080;color:#ffffff\">#现货</span>"))); 
				sb.Append(string.Format("<td width={2}px style={0}><font color=#606060><strong>{1}</strong></font></td>", contentTdStyle, remark, 960));//.Replace("#直邮", "<span style=\"background-color:#60b060;color:#ffffff\">#直邮</span>").Replace("#现货", "<span style=\"background-color:#ff8080;color:#ffffff\">#现货</span>"))); 
				sb.Append("</tr>");
				
				string fullAddress = _order.GetFullAddress();
				if (!string.IsNullOrEmpty(_order.ShipmentNumber))
					fullAddress += string.Format(" <font color=#707070>|</font> <font color=#800080>{0}: {1}</font>", _order.GetShipmentCompany(), _order.ShipmentNumber);
				
				sb.Append("<tr>");
				sb.Append("<td width=14px></td>");
				sb.Append(string.Format("<td width=55px style={0}>收货地址:</td>", normalTdStyleAlighRight));
				sb.Append("<td width=2px></td>");
				sb.Append(string.Format("<td style={0}>{1}</td>", contentTdStyle, fullAddress)); 
				sb.Append("</tr>");

				//if (string.IsNullOrEmpty(_order.EditedRecipientAddress))
				//{
				//    sb.Append("<tr>");
				//    sb.Append("<td width=14px></td>");
				//    sb.Append(string.Format("<td width=55px style={0}>拼音地址:</td>", normalTdStyleAlighRight));
				//sb.Append("<td width=2px></td>");
				//    sb.Append(string.Format("<td width={2}px style={0}>{1}</td>", contentTdStyle, HanZiToPinYin.Convert(fullAddress.Substring(0, fullAddress.Length - 8)) + HanZiToPinYin.Convert(fullAddress.Substring(0, fullAddress.Length - 8)).Length.ToString(), this.Width - 100)); 
				//    sb.Append("</tr>");
				//}
				
				if (!string.IsNullOrEmpty(_order.EditedRecipientAddress))
				{
					sb.Append("<tr>");
					sb.Append("<td width=14px></td>");
						sb.Append(string.Format("<td width=55px style={0}>新地址:</td>", normalTdStyleAlighRight));
						sb.Append("<td width=2px></td>");
						sb.Append(string.Format("<td style={0}><font color={1}>{2}</font> ", contentTdStyle, _refreshedNewAddr ? "#008000" : "#404040", _order.EditedRecipientAddress)); 
						sb.Append(string.Format("<input style={0} type=\"button\" id=\"btnGetFullEditedAddr\" value=\"\" style=\"background-color:#0080ff;width:30px;height:18px\" /></td>", contentSmallTextStyle));
					sb.Append("</tr>");
				}

				sb.Append("<tr>");
				sb.Append("<td width=14px></td>");
				sb.Append(string.Format("<td width=55px style={0}>商品列表:</td>", normalTdStyleAlighRight));
				sb.Append("<td width=2px></td>");
				sb.Append(string.Format("<td style={0}>", contentTdStyle));//, this.Width - 200));
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

						Order.OrderStatus status = Order.OrderStatus.Succeeded;
						if (infos.Length >= 4)
							status = ((Order.OrderStatus)Enum.Parse(typeof(Order.OrderStatus), infos[3]));
						
						sb.Append("<tr>");
						sb.Append(string.Format(
							"<td width=620px style={3}><font color={4}>{0}</font> <font color={2}>({1})</font></td>",
							infos[0],
							GetStautsDesc(status), GetStatusDescColor(status),
							normalTdStyle, status == Order.OrderStatus.Closed ? "#d0d0d0" : "#4169e1"));
						sb.Append(string.Format("<td width=60px style={1}><font color=#ff4000>{0}</font></td>", float.Parse(infos[1]).ToString("0.00"), normalTdStyle));
						sb.Append(string.Format("<td style={1}><font color=#606060><strong>{0}</strong></font></td>", int.Parse(infos[2]), normalTdStyle));
						sb.Append("</tr>");
					}
					
					sb.Append("</table>");
				sb.Append("</td>");
				sb.Append("</tr>");

				sb.Append("</table>");
			sb.Append("</td></tr>");

			sb.Append("</table></span>");
			sb.Append("</body>");
			
			wb.DocumentText = sb.ToString();

			int height = 62;
			if (!string.IsNullOrEmpty(_order.EditedRecipientAddress))
				height += 19;
			if (!string.IsNullOrEmpty(_order.BuyerRemark))
				height += 19;
			height += 19 * items.Length;
			if (!string.IsNullOrEmpty(_order.BuyerRemark) && Graphics.FromHwnd(this.Handle).MeasureString(_order.BuyerRemark, this.Font, 960).Height > 20)
				height += 19;
			if (!string.IsNullOrEmpty(_order.Remark) && Graphics.FromHwnd(this.Handle).MeasureString(_order.Remark, new Font(this.Font, FontStyle.Bold), 960).Height > 20)
				height += 19;
			
			wb.Height = height;
			
			if (Settings.Instance.UiType != Settings.UiTypes.Shanghai)
				height += this.Padding.Bottom;
			else
				height += 2;
			this.Height = height;
			this.Refresh();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			// border.
			Pen p = new Pen(Color.FromArgb(0x60, 0x60, 0x60));
			Rectangle r = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
			e.Graphics.DrawRectangle(p, r);

			// draw caption about adding packet at the bottom of this view.
			e.Graphics.DrawString("出单:", this.Font, new SolidBrush(Color.FromArgb(0xff, 0x70, 0x70, 0x70)), new Point(44, wb.Bottom + 2));
			e.Graphics.DrawString(
				string.Format("净重: {0}kg", ((float)GetNetWeight() / 1000).ToString("0.0")),
				this.Font, new SolidBrush(Color.DarkMagenta), new Point(79, wb.Bottom + 2));
			e.Graphics.DrawString("包裹单:", this.Font, new SolidBrush(Color.FromArgb(0xff, 0x70, 0x70, 0x70)), new Point(188, wb.Bottom + 2));
			
			// Added by KK on 2017/10/17.
			e.Graphics.DrawString("自动化:", this.Font, new SolidBrush(Color.FromArgb(0xff, 0x40, 0x40, 0x40)), new Point(922, wb.Bottom + 2));
			RefreshAutoTransmissionPictures(e.Graphics);
		}

		void RefreshAutoTransmissionPictures(Graphics g)
		{
			if (null == g)
				return;
			
			Image imgYunda = null;
			if (ShipmentCompanies.Yunda == _order.AutoTransmissionProcessor)
				imgYunda = Properties.Resources.yunda_32x18;
			else
				imgYunda = Properties.Resources.yunda_32x18_gray;
			g.DrawImage(imgYunda, RECT_YUNDA);
			
			Image imgZto = null;
			if (ShipmentCompanies.Zto == _order.AutoTransmissionProcessor)
				imgZto = Properties.Resources.zto_32x18;
			else
				imgZto = Properties.Resources.zto_32x18_gray;
			g.DrawImage(imgZto, RECT_ZTO);
			
			Image imgManual = null;
			if (ShipmentCompanies.Unknown == _order.AutoTransmissionProcessor)
				imgManual = Properties.Resources.tools_32x18;
			else
				imgManual = Properties.Resources.tools_32x18_gray;
			g.DrawImage(imgManual, RECT_MANUAL);
		}

		private void OrderDetailsControl_SizeChanged(object sender, EventArgs e)
		{
			if (_selectable)
				chkSelected.Location = new Point(1, 1);
			else
				chkSelected.Location = new Point(1-chkSelected.Width, 1);
		
			wb.Location = new Point(chkSelected.Right, chkSelected.Top);
			wb.Width = this.Width - 1 - wb.Left;

			cboPackets.Location = new Point(233, wb.Bottom + 1);
			btnAddPacket.Location = new Point(cboPackets.Right + 2, wb.Bottom + 1);
			btnMarkPrepared.Location = new Point(btnAddPacket.Right + 2, wb.Bottom + 1);

			RECT_YUNDA = new Rectangle(968, wb.Bottom + 1, 32, 18);
			RECT_ZTO = new Rectangle(RECT_YUNDA.Right + 4, wb.Bottom + 1, 32, 18);
			RECT_MANUAL = new Rectangle(RECT_ZTO.Right + 4, wb.Bottom + 1, 32, 18);
			this.Refresh();
		}

		private void wb_SizeChanged(object sender, EventArgs e)
		{
			//InitializeHtmlView();
		}

		private static string GetStautsDesc(Order.OrderStatus status)
		{
			switch (status)
			{
				case Order.OrderStatus.Deal:
					return "未付款";

				case Order.OrderStatus.Paid:
					return "已付款";

				case Order.OrderStatus.Sent:
					return "已发货";

				case Order.OrderStatus.Succeeded:
					return "交易成功";

				case Order.OrderStatus.Closed:
					return "已取消";
			}
			return string.Empty;
		}
		
		private static string GetStatusDescColor(Order.OrderStatus status)
		{
			switch (status)
			{
				case Order.OrderStatus.Deal:
					return "#808080";

				case Order.OrderStatus.Paid:
					return "#ff4000";

				case Order.OrderStatus.Sent:
					return "#800080";

				case Order.OrderStatus.Succeeded:
					return "#008000";

				case Order.OrderStatus.Closed:
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

		void imgStatus_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			
			StringBuilder sb = new StringBuilder();
			sb.Append(string.Format("下单时间: {0}\n", _order.DealTime.ToString("yyyy/MM/dd HH:mm:ss")));
			sb.Append(string.Format("付款时间: {0}\n", _order.DealTime.ToString("yyyy/MM/dd HH:mm:ss")));
			if (_order.PreparedNingbo)
			{
				PrepareHistory ph = PrepareHistory.GetNingbo(_order.OrderId);
				if (null != ph)
					sb.Append(string.Format("出单时间(宁波): {0}, {1}\n", ph.Date.ToString("yyyy/MM/dd HH:mm:ss"), ph.Operator));
			}
			if (_order.Prepared)
			{
				PrepareHistory ph = PrepareHistory.Get(_order.OrderId);
				if (null != ph)
					sb.Append(string.Format("出单时间(德国): {0}, {1}\n", ph.Date.ToString("yyyy/MM/dd HH:mm:ss"), ph.Operator));
			}

			MessageBox.Show(this.FindForm(), sb.ToString(), this.FindForm().Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

			Cursor.Current = Cursors.Default;
		}

		void btnAddComment_Click(object sender, EventArgs e)
		{
			string defaultAppendMemo = string.Empty;
			if (null != PacketInfoListForm.SelectedPacketInfo)
				defaultAppendMemo = string.Format(
					"stockout: [{0}]{1}", 
					Packet.GetPacketTypeDesc(PacketInfoListForm.SelectedPacketInfo.Type).Replace("Time24-", string.Empty), 
					PacketInfoListForm.SelectedPacketInfo.ShipmentNumber);
			else // similar to a shipment number in clipboard.
			{
				string s = Clipboard.GetText();
				s = s.Trim();
				if (!string.IsNullOrEmpty(s) && s.Length >= 8) // 通常快递单号都是12位或者10位, 为避免漏处理(漏处理无副作用), 以8位为判断标准.
				{
					Regex r = new Regex("^[0-9]*$");
					Match m = r.Match(s);
					if (m.Success)
						defaultAppendMemo = string.Format("stockout: {0}", s);
				}
			}
			
			UpdateSellMemoForm usmf = new UpdateSellMemoForm(_order.Remark, defaultAppendMemo);
			DialogResult dr = usmf.ShowDialog(this.FindForm());
			if (DialogResult.Cancel == dr)
				return;
		
			WebBrowserForms.UpdateSellMemoWebBrowserForm usmbf = new UpdateSellMemoWebBrowserForm(_order, usmf.AppendMemo, true);
			usmbf.ShowDialog(this.FindForm());
		}

		void btnConsignDe_Click(object sender, EventArgs e)
		{
			//string shipmentInfo = Clipboard.GetText().Trim();
			//string shipmentNumber;
			//bool isExpress;
			//bool isPostNL;
			//if (!IsShipmentInfoString(shipmentInfo, out shipmentNumber, out isExpress, out isPostNL))
			//{
			//    MessageBox.Show(
			//        this.FindForm(),
			//        "Please copy DHL shipment number into clipboard first.", this.FindForm().Text,
			//        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//    return;
			//}
			
			if (null == PacketInfoListForm.SelectedPacketInfo)
			{
				MessageBox.Show(
					this.FindForm(),
					"没有包裹单被选中, 无法点击发货.", this.FindForm().Text,
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (PacketInfoListForm.SelectedPacketInfo.Status.Contains("取消"))
			{
				MessageBox.Show(
					this.FindForm(),
					"该包裹单正在取消或已经取消, 不能用于点发货.", this.FindForm().Text,
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (string.IsNullOrEmpty(PacketInfoListForm.SelectedPacketInfo.Status))
			{
				DialogResult dr = MessageBox.Show(
					this.FindForm(),
					"此包裹并未实际发货.\n继续点发货?", this.FindForm().Text,
					MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (DialogResult.No == dr)
					return;
			}
			
			ConsignDeWebBrowserForm.Instance.Order = _order;
			ConsignDeWebBrowserForm.Instance.PacketInfo = PacketInfoListForm.SelectedPacketInfo;
			ConsignDeWebBrowserForm.Instance.ShowDialog(this.FindForm());
		}

		void btnConsignSh_Click(object sender, EventArgs e)
		{
			if (null != this.OnConsignSh)
				this.OnConsignSh(this, EventArgs.Empty);
		}

		void btnConsignNb_Click(object sender, EventArgs e)
		{
			if (null != this.OnConsignNb)
				this.OnConsignNb(this, EventArgs.Empty);
		}

		// shipment info string: [x] 000000000000, x=E or P.
		bool IsShipmentInfoString(string s, out string shipmentNumber, out bool isExpress, out bool isPostNL)
		{
			shipmentNumber = string.Empty;
			isExpress = false;
			isPostNL = false;
		
			Regex r = new Regex(@"\[(E|P|N)\] ([\d|\w|\W]{10,16})");
			Match m = r.Match(s);
			if (m.Success)
			{
				shipmentNumber = m.Groups[2].Value;
				isExpress = m.Groups[1].Value.Equals("E");
				isPostNL = m.Groups[1].Value.Equals("N");
				return true;
			}
			return false;
		}
		
		#region obsoleted code
		//private void tblHeaderInfo_Paint(object sender, PaintEventArgs e)
		//{
		//    try
		//    {
		//        Image img = GetStatusImage(_order);
		//        e.Graphics.DrawImage(img, 2, 4, 18, 18);
		//    }
		//    catch (Exception ex)
		//    {
		//        Trace.Write(ex);
		//    }
		//}

		//private static Image GetStatusImage(Order o)
		//{
		//    ResourceManager rm = new ResourceManager("Egode.Properties.Resources", Assembly.GetExecutingAssembly());
		//    switch (o.Status)
		//    {
		//        case Order.OrderStatus.Deal:
		//            return (Image)rm.GetObject("deal");
		//        case Order.OrderStatus.Paid:
		//            if (o.Prepared)
		//                return (Image)rm.GetObject("dhl");
		//            else if (o.LocalPrepared)
		//                return (Image)rm.GetObject("localdhl");
		//            else
		//                return (Image)rm.GetObject("paid");
		//        case Order.OrderStatus.Sent:
		//            return (Image)rm.GetObject("sent");
		//        case Order.OrderStatus.PartialSent:
		//            return (Image)rm.GetObject("partialsent");
		//        case Order.OrderStatus.Succeeded:
		//            return (Image)rm.GetObject("delivered");
		//        case Order.OrderStatus.Closed:
		//            return (Image)rm.GetObject("closed");
		//    }
		//    return null;
		//}
		#endregion
		
		void _order_OnStatusChanged(object sender, EventArgs e)
		{
			//InitializeHtmlView();
			HtmlElement imgStatus = wb.Document.GetElementById("imgStatus");
			if (null == imgStatus)
			    return;
			imgStatus.SetAttribute("src", OrderStatusIconPath.Get(_order));
		}

		// Added by KK on 2017/10/19.
		void _order_OnTransmissionChanged(object sender, EventArgs e)
		{
			this.Refresh();
		}

		public Order Order
		{
			get { return _order; }
		}
		
		public string FullEditedAddress
		{
			get { return _fullNewAddr; }
		}

		//Modified by KK on 2014/06/10.
		// 从发货页面或者物流页面获得完整地址
		public void RefreshFullEditedAddress()
		{
			/* Modified by KK on 2016/10/19
			 * Cannot get new address from the old page.
			OrderAddressInfoWebBrowserForm.Instance.OrderId = _order.OrderId;
			if (DialogResult.OK == OrderAddressInfoWebBrowserForm.Instance.ShowDialog(this.FindForm()))
			{
				_fullNewAddr = OrderAddressInfoWebBrowserForm.Instance.ModifiedAddress;
				_order.EditedRecipientAddress = OrderAddressInfoWebBrowserForm.Instance.ModifiedAddress;
				_refreshedNewAddr = true;
				InitializeHtmlView();
				Application.DoEvents();
				
				//string fullEditedAddress = cwbf.Address;
				//fullEditedAddress = fullEditedAddress.Substring(0, fullEditedAddress.Length - 6); // remove last post code to translate to pinyin.
				//if (fullEditedAddress.EndsWith(","))
				//    fullEditedAddress = fullEditedAddress.Substring(0, fullEditedAddress.Length - 1);
			}
			*/
			string orderDetailsHtml = WebBrowserForms.OrderDetailsPageWebBrowserForm.GetOrderDetailsHtml(_order.OrderId, this.FindForm());
			OrderDetailsScript orderDetailsScript = new OrderDetailsScript(orderDetailsHtml);
			if (orderDetailsScript.HasNewAddress)
				this.RefreshFullNewAddress(orderDetailsScript.NewAddress);
		}
		
		public void RefreshFullNewAddress(string newAddress)
		{
			_fullNewAddr = newAddress;
			_order.EditedRecipientAddress = newAddress;
			_refreshedNewAddr = true;
			InitializeHtmlView();
			Application.DoEvents();
		}
		//public void RefreshFullEditedAddress()
		//{
		//    OrderDetailsPageWebBrowserForm odpf = new OrderDetailsPageWebBrowserForm(_order.OrderId);
		//    if (DialogResult.OK == odpf.ShowDialog(this.FindForm()))
		//    {
		//        string html = odpf.Html;
		//        Regex r = new Regex(@"<th>新收货地址：</th>\s*<td>(.*)</td>");
		//        Match m = r.Match(html);
		//        if (m.Success)
		//        {
		//            string fullEditedAddress = m.Groups[1].Value.Trim();
		//            fullEditedAddress = fullEditedAddress.Replace(" ，", ",");
		//            fullEditedAddress = fullEditedAddress.Replace(",,", ",");
		//            _fullNewAddr = fullEditedAddress;
		//            _order.EditedRecipientAddress = fullEditedAddress;
		//            _refreshedNewAddr = true;
		//            InitializeHtmlView();
					
		//            fullEditedAddress = fullEditedAddress.Substring(0, fullEditedAddress.Length - 6); // remove last post code to translate to pinyin.
		//            if (fullEditedAddress.EndsWith(","))
		//                fullEditedAddress = fullEditedAddress.Substring(0, fullEditedAddress.Length - 1);
		//        }
		//    }
		//}

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
				Packet p = (Packet)cboPackets.SelectedItem;
				string fullAddress = !string.IsNullOrEmpty(_fullNewAddr) ? _fullNewAddr : _order.GetFullAddress();
				string phoneNumber = string.IsNullOrEmpty(_order.MobileNumber) ? _order.PhoneNumber : _order.MobileNumber;
				PacketInfo pi = new PacketInfo(_order.OrderId, p.Type, p.Weight, p.Price, fullAddress, phoneNumber, _order.PhoneNumber, _order.Items);
				pi.BuyerRemark = _order.BuyerRemark;
				pi.Remark = _order.Remark;
				this.OnAddPacket(this, pi);
				if (_order.LocalPrepared)
					btnAddPacket.BackColor = Color.Tomato;
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

		// 单位: 克(g)
		private int GetNetWeight()
		{
			int weight = 0;
			string[] itemInfos = _order.Items.Split(Order.ITEM_SEPARATOR.ToCharArray()[0]);//('★');
			for (int i = 0; i < itemInfos.Length; i++)
			{
				string itemInfo = itemInfos[i];
				string[] infos = itemInfo.Split(Order.ITEM_INFO_SEPARATOR.ToCharArray()[0]);//('☆');
				if (infos.Length < 3)
					continue;

				string productTitle = infos[0];
				Egode.ProductInfo pi = Egode.ProductInfo.Match(productTitle, _order.Remark);
				if (null == pi)
					continue;
				int amount = int.Parse(infos[2]);
				weight += (int)pi.Weight * amount;
			}

			return weight;
		}

		private void OnYundaTransmissionClick()
		{
			// 已经点亮的, 再次点击没有效果.
			if (ShipmentCompanies.Yunda != _order.AutoTransmissionProcessor)
				_order.AutoTransmissionProcessor = ShipmentCompanies.Yunda;
		}

		private void OnZtoTransmissionClick()
		{
			// 已经点亮的, 再次点击没有效果.
			if (ShipmentCompanies.Zto != _order.AutoTransmissionProcessor)
				_order.AutoTransmissionProcessor = ShipmentCompanies.Zto;
		}

		private void OnManualTransmissionClick()
		{
			// 已经点亮的, 再次点击没有效果.
			if (ShipmentCompanies.Unknown != _order.AutoTransmissionProcessor)
				_order.AutoTransmissionProcessor = ShipmentCompanies.Unknown;
		}
		
		void OrderDetailsControlMouseDown(object sender, MouseEventArgs e)
		{
			if (RECT_YUNDA.Contains(e.X, e.Y))
				OnYundaTransmissionClick();
			if (RECT_ZTO.Contains(e.X, e.Y))
				OnZtoTransmissionClick();
			if (RECT_MANUAL.Contains(e.X, e.Y))
				OnManualTransmissionClick();
		}
	}
}