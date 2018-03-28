using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class TextBoxForm : Form
	{
		public TextBoxForm(string info)
		{
			InitializeComponent();
			txt.Text = info;
		}
	}
}