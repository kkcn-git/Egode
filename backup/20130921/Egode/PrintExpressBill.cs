using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using OrderParser;

namespace Egode
{
	public class PrintExpressBill
	{
		private Order _order;
		private Font _defaultFont = new Font("����", 12);

		public PrintExpressBill(Order order)
		{
			_order = order;
			if (null == _order)
				return;

			PrintDocument pdoc = new PrintDocument();
			pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
			pdoc.Print();
		}

		void pdoc_PrintPage(object sender, PrintPageEventArgs e)
		{
			Graphics g = e.Graphics;
			//g.DrawImage(picExpressForm.Image, new Point(0, 0));
			DrawString(e.Graphics, "�� �� e ��", new Point(160, 133));
			DrawString(e.Graphics, "13801681873", new Point(180, 231));
			//DrawString(e.Graphics, "ATM Pre x 2", new Point(70, 295));
			DrawString(e.Graphics, _order.RecipientName, new Point(500, 120));
			//DrawString(e.Graphics, "����", new Point(520, 176));
			//DrawString(e.Graphics, "����", new Point(620, 176));
			//DrawString(e.Graphics, "������", new Point(700, 176));
			//DrawString(e.Graphics, "����·�뺣Դ������ ������ۡ6��¥1¥�ͷ���", new Point(430, 210));
			DrawString(e.Graphics, _order.MobileNumber, new Point(545, 230));
			DrawString(e.Graphics, _order.PhoneNumber, new Point(685, 230));
		}

		private void DrawString(Graphics g, string s, Point p)
		{
			if (null == g)
				return;
			if (string.IsNullOrEmpty(s))
				return;

			p.Offset(10, -20);
			g.DrawString(s, _defaultFont, new SolidBrush(Color.Black), p);
		}
	}
}