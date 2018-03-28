/*
 * Created by SharpDevelop.
 * User: KKcn
 * Date: 11/28/2017
 * Time: 17:14:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Egode.WebBrowserForms
{
	/// <summary>
	/// Description of LoginForm.
	/// </summary>
	public partial class LoginForm : Form
	{
		private Timer _tmr;
		
		public LoginForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		void LoginFormLoad(object sender, EventArgs e)
		{
			wb.Navigate(@"https://myseller.taobao.com");
		}
		
		void WbDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			Trace.WriteLine(e.Url.ToString());
			
			if (e.Url.ToString().ToLower().StartsWith(@"https://login.taobao.com"))
			{
				if (null != wb.Document && null != wb.Document.Window)
				{
					wb.Document.Window.ScrollTo(wb.Document.Body.ScrollRectangle.Width-wb.Size.Width, 210);
					
					if (null == _tmr)
					{
						_tmr = new Timer();
						_tmr.Interval = 50;
						_tmr.Tick += _tmr_Tick;
						_tmr.Start();
					}
				}
			}
			else if (e.Url.ToString().ToLower().StartsWith(@"https://myseller.taobao.com"))
			{
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private bool _cursorPositionSet =  false;
		void _tmr_Tick(object sender, EventArgs e)
		{
			HtmlElement u = wb.Document.GetElementById("TPL_username");
			if (null == u)
				return;
			
			_tmr.Stop();

			if (_cursorPositionSet)
			{
				if (SystemInformation.MouseButtonsSwapped)
				    KmRobot.Win32.ClickRightMouse();
				else
				    KmRobot.Win32.ClickLeftMouse();
			}
			else
			{
				Cursor.Position = new Point(this.Left + 200, this.Bottom - 110);
				_cursorPositionSet = true;
				_tmr.Interval = 500;
				_tmr.Start();
			}
		}
	}
}