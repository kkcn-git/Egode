using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Egode.WebBrowserForms;

namespace Egode.WebBrowserForms
{
	public partial class AutoSigninWebBrowserForm : WebBrowserForm
	{
		private bool _signedIn;
	
		public AutoSigninWebBrowserForm(string url) : base(url)
		{
			InitializeComponent();
		}

		protected override void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
		{
			base.OnDocumentCompleted(e);

			if (wb.Document.Body.OuterHtml.Contains("��¼����") && wb.Document.Body.OuterHtml.Contains("��¼���룺"))
			{
				HtmlElement u = wb.Document.GetElementById("TPL_username_1");
				if (null == u)
					return;

				HtmlElement p = wb.Document.GetElementById("TPL_password_1");
				if (null == p)
					return;

				HtmlElement s = wb.Document.GetElementById("J_SubmitStatic");
				if (null == s)
					return;

				u.SetAttribute("value", "�¹�e��");
				p.SetAttribute("value", "ta0ba01g0d1");
				s.InvokeMember("click");
				return;
			}

			if (wb.Document.Body.OuterHtml.Contains("ʹ�������˻���¼"))
			{
				HtmlElement a = wb.Document.GetElementById("J_OtherAccountV");
				wb.Navigate(a.GetAttribute("href"));
				return;
			}

			if (wb.Document.Body.OuterHtml.Contains("��ǰ����״̬"))
				_signedIn = true;

			if (wb.Document.Body.OuterHtml.Contains("�������ı���") && wb.Document.Body.OuterHtml.Contains("�����еı���"))
				_signedIn = true;

			if (wb.Document.Body.OuterHtml.Contains("����λ�ã�") && wb.Document.Body.OuterHtml.ToLower().Contains("�ҵ��Ա�</a><span>&gt;"))
				_signedIn = true;
		}
		
		public bool SignedIn
		{
			get { return _signedIn; }
		}
	}
}