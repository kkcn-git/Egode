using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode.WebBrowserForms
{
	public partial class WebBrowserForm : Form
	{
		protected string _url;
		private bool _aboutblank;
	
		public WebBrowserForm():this(string.Empty)
		{
		}
		public WebBrowserForm(string url)
		{
			_url = url;
		
			InitializeComponent();
			wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);
			
			if (!string.IsNullOrEmpty(_url))
				wb.Navigate(url);
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			_aboutblank = false;
			wb.Navigate("about:blank");
			while (true)
			{
				Application.DoEvents();
				if (_aboutblank)
					break;
			}
		}

		void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			// Added by KK on 2016/07/27.
			//if (null != wb.Document && null != wb.Document.Body)
			//    wb.Size = new Size(wb.Document.Body.ScrollRectangle.Width, wb.Document.Body.ScrollRectangle.Height);

			if (e.Url.AbsoluteUri.Equals("about:blank"))
			{
				_aboutblank = true;
				return;
			}

			this.OnDocumentCompleted(e);
		}
		
		protected virtual void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
		{
		}
		
		public void Navigate(string url)
		{
			wb.Navigate(url);
		}
	}
}