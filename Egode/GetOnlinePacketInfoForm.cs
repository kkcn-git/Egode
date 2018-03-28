using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml;
using System.Net;
using System.IO;

namespace Egode
{
	public partial class GetOnlinePacketInfoForm : Form
	{
		private string _time24Code;
		private List<PdfPacketInfoEx> _packetInfos;
		
		public List<PdfPacketInfoEx> PacketInfos
		{
			get { return _packetInfos; }
		}
		
		public string Time24Code
		{
			get { return _time24Code; }
		}

		public GetOnlinePacketInfoForm()
		{
			InitializeComponent();
		}

		private void GetOnlinePacketInfoForm_Load(object sender, EventArgs e)
		{
			lblStatus.Text = "正在打开首页...";
			wb.Navigate("http://www.mypost4u.com/zh/index");
		}

		private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			Trace.WriteLine(e.Url.AbsoluteUri.ToLower().Trim());
			switch (e.Url.AbsoluteUri.ToLower().Trim())
			{
				case "http://www.mypost4u.com/zh/index":
					if (wb.Document.Body.OuterHtml.Contains("欢迎您，Hanslord e.K."))
					{
						goto case "http://www.mypost4u.com/zh/index#content";
					}
					else
					{
						lblStatus.Text = "正在自动登录...";
						Signin();
					}
					//wb.Navigate("http://www.mypost4u.com/zh/users/sign_in#content");
					break;

				//case "http://www.mypost4u.com/zh/users/sign_in#content":
				//    lblStatus.Text = "正在自动登录...";
				//    Signin();
				//    break;

				case "http://www.mypost4u.com/zh/index#content":
					if (!wb.Document.Body.OuterHtml.Contains("欢迎您，Hanslord e.K."))
						return;
					lblStatus.Text = "正在打开订单列表页面...";
					wb.Navigate("http://www.mypost4u.com/zh/parcel_orders#content");
					break;

				case "http://www.mypost4u.com/zh/parcel_orders#content":
					lblStatus.Text = "到达订单列表页面, 请选择对应订单进入订单详情页面获得包裹单信息.";
					lblStatus.ForeColor = Color.Blue;
					break;

				default:
					if (e.Url.AbsoluteUri.ToLower().Trim().StartsWith("http://www.mypost4u.com/zh/parcel_orders/"))
					{
						lblStatus.Text = "到达订单详情页面, 点击按钮获取包裹单信息并返回.";
						lblStatus.ForeColor = Color.DarkGreen;
						btnOK.Enabled = true;
					}
					break;
			}
		}

		private void Signin()
		{
			HtmlElement userEmail = wb.Document.GetElementById("user_email");
			if (null == userEmail)
				return;
			userEmail.SetAttribute("value", "leoncai78@gmail.com");
			
			HtmlElement userPassword = wb.Document.GetElementById("user_password");
			if (null == userPassword)
				return;
			userPassword.SetAttribute("value", "19780830");
			
			HtmlElementCollection inputs = wb.Document.GetElementsByTagName("input");
			if (null == inputs || inputs.Count <= 0)
				return;
			
			wb.Document.Forms["new_user"].InvokeMember("submit");
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			lblStatus.ForeColor = Color.OrangeRed;
			lblStatus.Text = "正在获取包裹单信息...";
		
			XmlDocument doc = Common.ConvertHtmlToXml(wb.Document.Body.OuterHtml.ToLower().Trim());
			if (null == doc)
				return;
			
			XmlNode nodeParcelListTitle = doc.SelectSingleNode(@"//h3[text()='包裹清单']");
			if (null == nodeParcelListTitle)
				return;
			
			btnOK.Enabled = false;
			
			string s = wb.Document.Body.OuterHtml;
			string s1 = s.Replace("包裹详情", string.Empty);
			if (s1.Length >= s.Length)
				return;
			int totalPacketCount = (s.Length - s1.Length)/"包裹详情".Length;
			lblStatus.Text = string.Format("正在获取包裹单详情...0\\{0}", totalPacketCount);
			
			System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"(time\d{9})");
			System.Text.RegularExpressions.Match m = r.Match(wb.Document.Body.OuterHtml.ToLower());
			if (m.Success)
			{
				_time24Code = m.Value.Replace("time", "TIME");
			}
			
			// packet type.
			PacketTypes packetType = PacketTypes.Unknown;
			switch (doc.SelectSingleNode(".//span[text()='包裹服务种类：']").NextSibling.Value)
			{
				case "dhl":
					packetType = PacketTypes.Time24_DHL;
					break;
				case "postnl":
					packetType = PacketTypes.Time24_PostNL;
					break;
				case "奶粉快件":
					packetType = PacketTypes.Time24_MilkExpress;
					break;
			}
			
			_packetInfos = new List<PdfPacketInfoEx>();
			string saveParcelListUrl = wb.Url.AbsoluteUri;
			XmlNode nodeTbody = nodeParcelListTitle.NextSibling.ChildNodes[1];
			int current = 0;
			while (null != nodeTbody)
			{
				XmlNode nodeTr = nodeTbody.FirstChild;
				if (nodeTr.ChildNodes.Count < 8)
				{
					MessageBox.Show(this, "Some error occured.\nClick OK button to retry.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				string shipmentNumber = nodeTr.ChildNodes[2].InnerText.Trim().ToUpper();
				string status = nodeTr.ChildNodes[3].InnerText.Trim().ToUpper();
				float weight = float.Parse(nodeTr.ChildNodes[7].InnerText.Trim().Replace("kg", string.Empty));
				string recipientName = nodeTr.ChildNodes[5].InnerText.Trim();
				lblStatus.Text = string.Format("正在获取<{0}>包裹单信息...{1}\\{2}", recipientName, ++current, totalPacketCount);

				// get details for packet.
				string detailsUrl = string.Format(@"http://www.mypost4u.com{0}", nodeTr.ChildNodes[9].FirstChild.Attributes.GetNamedItem("href").Value);
				_packetDetailsPageHtml = string.Empty;
				if (null != wbPacketDetails.Document)
					wbPacketDetails.Document.Body.OuterHtml = string.Empty;
				wbPacketDetails.Navigate(detailsUrl);
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				int retry = 0;
				while (string.IsNullOrEmpty(_packetDetailsPageHtml))
				{
					Application.DoEvents();
					// wait for up to 20 seconds.
					if (stopwatch.ElapsedMilliseconds >= 20000) // retry.
					{
						if (retry++ > 3)
							break;
						stopwatch.Reset();
						stopwatch.Start();
						wbPacketDetails.Navigate(detailsUrl);
					}
				}
				if (string.IsNullOrEmpty(_packetDetailsPageHtml))
				{
					lblStatus.Text += string.Format("获取包裹单详细信息失败! @_@");
					break;
				}
				
				int i = _packetDetailsPageHtml.IndexOf(recipientName);
				if (i > 0)
				{
					i = _packetDetailsPageHtml.IndexOf("p:</abbr>", i);
					if (i > 0)
					{
						int j = _packetDetailsPageHtml.IndexOf("<br", i);
						if (j > 0)
						{
							int l = "p:</abbr>".Length;
							string phoneNumber = _packetDetailsPageHtml.Substring(i+l, j-i-l).Replace("&nbsp;", string.Empty).Replace("+86", string.Empty);
							string fullAddress = GetFullAddress(_packetDetailsPageHtml);
							_packetInfos.Add(new PdfPacketInfoEx(string.Empty, packetType, recipientName, phoneNumber, shipmentNumber, weight, status, fullAddress));
						}
					}
				}
				else
				{
					Trace.WriteLine("");
				}

				nodeTbody = nodeTbody.NextSibling;
			}
			
			if (null == _packetInfos || _packetInfos.Count <= 0)
			{
				MessageBox.Show(this, "No any packet info found.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (lblStatus.Text.EndsWith("@_@"))
			{
				MessageBox.Show(this, "获取包裹单详细信息失败, 需要重试!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}
		
		private string GetFullAddress(string html)
		{
			XmlDocument doc = Common.ConvertHtmlToXml(html);
			XmlNode nodeRecipientInfo = doc.SelectSingleNode(".//span[text()='收件信息']");
			if (null == nodeRecipientInfo)
				return string.Empty;
			XmlNode nodeAddress = nodeRecipientInfo.NextSibling;
			if (null == nodeAddress)
				return string.Empty;
			
			return string.Format("{0}, {1}, {2}", 
				nodeAddress.ChildNodes[4].InnerText.Replace("中国大陆", string.Empty).Trim(), 
				nodeAddress.ChildNodes[2].InnerText.Trim(),
				nodeAddress.ChildNodes[6].InnerText.Trim());
		}

		private string _packetDetailsPageHtml;
		private void wbPacketDetails_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			Trace.WriteLine("----" + e.Url.AbsolutePath.ToString());
			if (!wbPacketDetails.Document.Body.OuterHtml.Contains("包裹详情"))
				return;
			_packetDetailsPageHtml = wbPacketDetails.Document.Body.OuterHtml.ToLower().Trim();
		}
	}
}