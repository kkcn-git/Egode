using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class FlyingForm : Form
	{
		public event EventHandler FlyingCompleted;
		private Point _dest;
		private Timer _tmr;
		private int _xstep;
		private int _ystep;
		private int _steps;
	
		public FlyingForm(Image img, Point dest)
		{
			_dest = dest;
			InitializeComponent();
			pic.Image = img;
		}
		
		private void NbFlyingForm_Shown(object sender, EventArgs e)
		{
			this.Size = pic.Image.Size;
			this.Location = Cursor.Position;
			_xstep = (_dest.X - this.Location.X) / 10;
			_ystep = (_dest.Y - this.Location.Y) / 10;
			_steps = 0;
		
			_tmr = new Timer();
			_tmr.Interval = 30;
			_tmr.Tick += new EventHandler(_tmr_Tick);
			_tmr.Start();
		}

		void _tmr_Tick(object sender, EventArgs e)
		{
			Point p = this.Location;
			p.Offset(_xstep, _ystep);
			this.Location = p;
			
			if (++_steps >= 10)
			{
				_tmr.Stop();
				System.Threading.Thread.Sleep(100);
				Application.DoEvents();
				
				for (int i = 0; i < 10; i ++)
				{
					this.Size = new Size(this.Width + 2, this.Height + 2);
					System.Threading.Thread.Sleep(10);
					Application.DoEvents();
				}

				System.Threading.Thread.Sleep(100);
				Application.DoEvents();
				
				this.Close();
				
				if (null != this.FlyingCompleted)
					this.FlyingCompleted(this, EventArgs.Empty);
			}
		}
	}
}