using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class DelayForm : Form
	{
		private string _orderId;
		private int _days;
		
		public DelayForm(string orderId, int days)
		{
			_orderId = orderId;
			_days = days;
			InitializeComponent();
		}

		private void DelayForm_Load(object sender, EventArgs e)
		{
			string url = string.Format("http://trade.taobao.com/trade/delay_time_out_date.htm?biz_order_id={0}&biz_type=200&has_refund=true", _orderId);
			wb.Navigate(url);
		}

		private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			if (!wb.Document.Body.OuterHtml.Contains("延长收货时间可以让买家有更多时间来“确定收货”，而不会急于去申请退款。"))
				return;
			if (wb.Document.Body.OuterHtml.Contains("延长收货时间成功!"))
			{
				this.Close();
				return;
			}
		
			int index = 0;
			switch (_days)
			{
				case 3:
					index = 0;
					break;
				case 5:
					index = 1;
					break;
				case 7:
					index = 2;
					break;
				case 10:
					index = 3;
					break;
			}
			
			wb.Document.GetElementById("J_DelayDays").SetAttribute("selectedindex", index.ToString());
			
			HtmlElementCollection buttons = wb.Document.GetElementsByTagName("button");
			if (null != buttons && buttons.Count > 0)
			{
				foreach (HtmlElement button in buttons)
				{
					if (button.InnerText.Equals("确 定"))
					{
						button.InvokeMember("click");
						break;
					}
				}
			}
		}
	}
}