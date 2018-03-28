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

			if (wb.Document.Body.OuterHtml.Contains("�������۳ɹ�"))
			{
				this.Close();
				return;
			}

			if (wb.Document.Body.OuterHtml.Contains("��Ǹ�����۷���ʧ�ܣ�"))
			{
				this.Close();
				return;
			}

			if (!wb.Document.Body.OuterHtml.Contains("������ң�"))
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
				textarea.InnerText = @"��л��С���֧��
����֧��������ǰ���Ķ���~
���ǻ�Ŭ���Ѹ���������������ĵ¹���ŷ�޲�Ʒ������~

egode.taobao.com";
			}
			
			HtmlElementCollection buttons = wb.Document.GetElementsByTagName("button");
			if (null == buttons || buttons.Count <= 0)
				return;
			
			foreach (HtmlElement button in buttons)
			{
				if (button.InnerText.Equals("�ύ����"))
				{
					button.InvokeMember("click");
					break;
				}
			}
		}
	}
}