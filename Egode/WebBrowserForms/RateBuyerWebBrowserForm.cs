using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode.WebBrowserForms
{
	public partial class RateBuyerWebBrowserForm : AutoSigninWebBrowserForm
	{
		private static RateBuyerWebBrowserForm _instance;
		private string _tradeId;
	
		public RateBuyerWebBrowserForm() : base(string.Empty)
		{
			InitializeComponent();
		}
		
		public static RateBuyerWebBrowserForm Instance
		{
			get 
			{
				if (null == _instance)
					_instance = new RateBuyerWebBrowserForm();
				return _instance;
			}
		}
		
		public string TradeId
		{
			get { return _tradeId; }
			set { _tradeId = value; }
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			
			if (!string.IsNullOrEmpty(_tradeId))
			{
				_flag = false;
				this.Navigate(string.Format(@"http://rate.taobao.com/remark_buyer.jhtml?tradeID={0}", _tradeId));
			}
		}

		private bool _flag;
		protected override void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
		{
			//System.Diagnostics.Trace.WriteLine(e.Url.AbsoluteUri);
			base.OnDocumentCompleted(e);
			
			if (!this.SignedIn)
				return;

			if (wb.Document.Body.OuterHtml.Contains("信用评价成功"))
			{
				this.Close();
				return;
			}

			if (wb.Document.Body.OuterHtml.Contains("抱歉，评价发布失败！"))
			{
				this.Close();
				return;
			}

			if (!wb.Document.Body.OuterHtml.Contains("被评买家："))
				return;
			
			if (_flag)
				return;
			_flag = true;
			
			/*
			HtmlElement elmRateGoodAll = wb.Document.GetElementById("rate-good-all");
			if (null == elmRateGoodAll)
				return;

			elmRateGoodAll.SetAttribute("checked", "checked");
			//elmRateGoodAll.InvokeMember("click");
			*/
			
			HtmlElementCollection inputs = base.wb.Document.GetElementsByTagName("input");
			foreach (HtmlElement input in inputs)
			{
				//System.Diagnostics.Trace.Write(input.GetAttribute("id"));
				//System.Diagnostics.Trace.Write(",");
				//System.Diagnostics.Trace.Write(input.GetAttribute("class"));
				//System.Diagnostics.Trace.Write(",");
				//System.Diagnostics.Trace.WriteLine(input.GetAttribute("type"));
			
				if (input.GetAttribute("id").StartsWith("rate-good-") && input.GetAttribute("type").Equals("radio"))
					input.SetAttribute("checked", "checked");
			}
			
			HtmlElementCollection textareas = base.wb.Document.GetElementsByTagName("textarea");
			if (null == textareas || textareas.Count <= 0)
				return;
			
			foreach (HtmlElement textarea in textareas)
			{
				textarea.InnerText = @"感谢对小店的支持
您的支持是我们前进的动力~
我们会努力把更多更好物美价廉的德国、欧洲产品带给您~";
			}
			
			HtmlElementCollection buttons = base.wb.Document.GetElementsByTagName("button");
			if (null == buttons || buttons.Count <= 0)
				return;
			
			foreach (HtmlElement button in buttons)
			{
				if (button.InnerText.Equals("提交评论"))
				{
					button.InvokeMember("click");
					break;
				}
			}
		}
	}
}