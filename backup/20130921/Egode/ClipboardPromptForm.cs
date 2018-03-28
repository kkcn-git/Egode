using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class ClipboardPromptForm : Form
	{
		private string _infoPart1;
		private string _infoPart2;
		private Timer _tmr;
	
		public ClipboardPromptForm(string infoPart1, string infoPart2)
		{
			_infoPart1 = infoPart1;
			_infoPart2 = infoPart2;
			InitializeComponent();
			
			_tmr = new Timer();
			_tmr.Interval = 100;
			_tmr.Tick += new EventHandler(_tmr_Tick);
			_tmr.Start();
		}

		void _tmr_Tick(object sender, EventArgs e)
		{
			if (this.Opacity > 0.7)
				this.Opacity -= 0.05;
			else
				this.Opacity -= 0.1;

			if (this.Opacity <= 0)
			{
				_tmr.Stop();
				this.Close();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			
			e.Graphics.DrawRectangle(new Pen(Color.LightGray), new Rectangle(0, 0, this.Width - 1, this.Height - 1));
			SizeF size1 = e.Graphics.MeasureString(_infoPart1, this.Font, 1024, StringFormat.GenericTypographic);
			SizeF size2 = e.Graphics.MeasureString(_infoPart2, this.Font, 1024, StringFormat.GenericTypographic);
			SizeF size3 = e.Graphics.MeasureString("已复制到剪贴板", this.Font, 1024, StringFormat.GenericTypographic);
			
			Point p = new Point((this.Width - (int)size1.Width - (int)size2.Width - (int)size3.Width)/2, 12);
			
			e.Graphics.DrawString(_infoPart1, this.Font, new SolidBrush(this.ForeColor), p);
			p.Offset((int)size1.Width+2, 0);

			e.Graphics.DrawString(_infoPart2, this.Font, new SolidBrush(Color.Blue), p);
			p.Offset((int)size2.Width, 0);

			e.Graphics.DrawString("已复制到剪贴板", this.Font, new SolidBrush(this.ForeColor), p);
		}
	}
}