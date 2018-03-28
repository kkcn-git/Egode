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
		public RateBuyerWebBrowserForm(string url) : base(url)
		{
			InitializeComponent();
		}

		private bool _flag;
		protected override void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
		{
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
			
			HtmlElement elmRateGoodAll = wb.Document.GetElementById("rate-good-all");
			if (null == elmRateGoodAll)
				return;
			
			//elmRateGoodAll.SetAttribute("checked", "true");
			elmRateGoodAll.InvokeMember("click");
			
			HtmlElementCollection textareas = wb.Document.GetElementsByTagName("textarea");
			if (null == textareas || textareas.Count <= 0)
				return;
			
			foreach (HtmlElement textarea in textareas)
			{
				textarea.InnerText = @"感谢对小店的支持
您的支持是我们前进的动力~
我们会努力把更多更好物美价廉的德国、欧洲产品带给您~

egode.taobao.com";
			}
			
			HtmlElementCollection buttons = wb.Document.GetElementsByTagName("button");
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