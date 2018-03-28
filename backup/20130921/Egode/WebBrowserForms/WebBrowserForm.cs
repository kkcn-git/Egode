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
		public WebBrowserForm(string url)
		{
			InitializeComponent();
			wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);
			wb.Navigate(url);
		}

		void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			this.OnDocumentCompleted(e);
		}
		
		protected virtual void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
		{
		}
	}
}