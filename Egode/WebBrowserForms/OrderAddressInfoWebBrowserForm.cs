using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace Egode.WebBrowserForms
{
	public partial class OrderAddressInfoWebBrowserForm : AutoSigninWebBrowserForm
	{
		/*
		 * data without modified address.
		({
		   "splitStr":"��",
		   "address":{
		 "name":"���۱�",
		 "mobilephone":"18255228898",
		 "phone":"",
		 "addr":"����ʡ ������ ������ ����������·818��˫�����ŵ����ع�˾ ",
		 "post":"233000"
		 }
		 })

		 * data with modified address.
		({
		   "splitStr":"��",
		   "address":{
		 "name":"������",
		 "mobilephone":"13486103758",
		 "phone":"",
		 "addr":"�㽭ʡ ������ ��ɽ�� �����ֵ���ͨ·798�� ���ݻ��������������޹�˾ ",
		 "post":"310018"
		 }
		   ,
		 "modifyAddress":{
		 "name":"���Ҳ�",
		 "mobilephone":" 15024494599",
		 "phone":"",
		 "addr":"�㽭ʡ ������ ��ɽ�� ������ԡ��ʩ·11��ʢ�����루����㳡�ԣ�",
		 "post":"311251"
		 }
		 })
		 */
		
		private static OrderAddressInfoWebBrowserForm _instance;
		private string _orderId;

		public OrderAddressInfoWebBrowserForm() : base(string.Empty)
		{
			InitializeComponent();
		}
		
		public static OrderAddressInfoWebBrowserForm Instance
		{
			get 
			{
				if (null == _instance)
					_instance = new OrderAddressInfoWebBrowserForm();
				return _instance;
			}
		}
		
		public string OrderId
		{
			get { return _orderId; }
			set { _orderId = value; }
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			
			if (!string.IsNullOrEmpty(_orderId))
				this.Navigate(string.Format(@"https://trade.taobao.com/trade/json/order_address_info.htm?biz_order_id={0}", _orderId));
		}

		protected override void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
		{
			base.OnDocumentCompleted(e);

			//Trace.WriteLine(e.Url.AbsolutePath);
			//Trace.WriteLine(e.Url.AbsoluteUri);
			
			this.DialogResult = DialogResult.Cancel; // default value.

			if (this.SignedIn)
			{
				string html = wb.Document.Body.OuterHtml.Trim().ToLower();
				html = html.Replace("\"", string.Empty);
				
				//Regex r = new Regex("modifyaddress");
				//Match m = r.Match(html);
				//if (m.Success)
				//{

				//    r = new Regex(@"modifyaddress.*name:([\s\S]*)(,$)");
				//    m = r.Match(html);
				//    if (m.Success)
				//    {
				//        _modifiedAddress += m.Groups[1].Value;
				//        _modifiedAddress += ",";
				//    }
				//}
				
				if (html.Contains("modifyaddress"))
				{
					int start = html.IndexOf("modifyaddress");

					int nameIndex = html.IndexOf("name:", start);
					int commaIndex = html.IndexOf(",", nameIndex);
					string name = html.Substring(nameIndex + "name:".Length, commaIndex - (nameIndex + "name:".Length)).Trim();

					int mobilePhoneIndex = html.IndexOf("mobilephone:", commaIndex);
					commaIndex = html.IndexOf(",", mobilePhoneIndex);
					string mobilePhone = html.Substring(mobilePhoneIndex + "mobilephone:".Length, commaIndex - (mobilePhoneIndex + "mobilephone:".Length)).Trim();
				
					int phoneIndex = html.IndexOf("phone:", commaIndex);
					commaIndex = html.IndexOf(",", phoneIndex);
					string phone = html.Substring(phoneIndex + "phone:".Length, commaIndex - (phoneIndex + "phone:".Length)).Trim();
				
					int addrIndex = html.IndexOf("addr:", commaIndex);
					commaIndex = html.IndexOf(",", addrIndex);
					string addr = html.Substring(addrIndex + "addr:".Length, commaIndex - (addrIndex + "addr:".Length)).Trim();
				
					int postIndex = html.IndexOf("post:", commaIndex);
					string post = html.Substring(postIndex + "post:".Length, 6).Trim();
					
					_modifiedAddress = string.Format("{0},{1},{2},{3},{4}", name, mobilePhone, phone, addr, post);

					this.DialogResult = DialogResult.OK;
				}
				
				this.Close();
			}
		}
		
		// format: recipient_name,phone1,phone2,province,city1,city2,district,street address,000000
		private string _modifiedAddress;
		public string ModifiedAddress
		{
			get { return _modifiedAddress; }
		}
	}
}