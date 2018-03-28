using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			
			LinkLabel lblNextPage = new LinkLabel();
			lblNextPage.Text = "Next Page";
			statusStrip1.Items.Add(new ToolStripControlHost(lblNextPage));
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			textBox1.Text = "atm1 x 4\r\natm2 x 6";
			textBox1.Height = textBox1.PreferredSize.Height;
		}
	}
}