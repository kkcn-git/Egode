using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class PromptForm : Form
	{
		public class Message
		{
			private string _content;
			
			public Message(string s)
			{
				_content = s;
			}
			
			public string Content
			{
				get { return _content; }
				set { _content = value; }
			}
		}
		
		private List<Message> _messages;
	
		public PromptForm()
		{
			InitializeComponent();
			_messages = new List<Message>();
			
			btnOK.Enabled = false;
		}
		
		public List<Message> Messages
		{
			get { return _messages; }
		}
		
		public void AddMessage(string s)
		{
			_messages.Add(new Message(s));
			RefreshDisplay();
		}
		
		public void RefreshDisplay()
		{
			lblInfo.Text = string.Empty;
			foreach (Message m in _messages)
				lblInfo.Text += m.Content + "\n";
			Application.DoEvents();
		}

		public bool OKEnabled
		{
			get { return btnOK.Enabled; }
			set { btnOK.Enabled = value; }
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void lblInfo_SizeChanged(object sender, EventArgs e)
		{
			//lblInfo.Location = new Point(6, 6);
			//btnOK.Location = new Point(lblInfo.Location.X + (lblInfo.Width - btnOK.Width)/2, lblInfo.Bottom + 12);
			//this.ClientSize = new Size(lblInfo.Right + 6, btnOK.Bottom + 6);
		}
	}
}