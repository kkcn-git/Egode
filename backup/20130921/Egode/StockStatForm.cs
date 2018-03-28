using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Xml;

namespace Egode
{
	public partial class StockStatForm : Form
	{
		private class ProductCount
		{
			private readonly string _id;
			private int _count;
			
			public ProductCount(string id)
			{
				_id = id;
			}
			
			public string Id
			{
				get { return _id; }
			}
			
			public int Count
			{
				get { return _count; }
			}
			
			public void AddCount(int count)
			{
				_count += count;
			}
		}
	
		private class DayStat
		{
			private DateTime _date;
			private List<ProductCount> _products;
			
			public DayStat(DateTime date)
			{
				_date = date;
				_products = new List<ProductCount>();
				_products.Add(new ProductCount("001-0001"));
				_products.Add(new ProductCount("001-0002"));
				_products.Add(new ProductCount("001-0003"));
				_products.Add(new ProductCount("001-0004"));
				_products.Add(new ProductCount("001-0005"));
				_products.Add(new ProductCount("001-0006"));
			}
			
			public DateTime Date
			{
				get { return _date; }
			}

			public List<ProductCount> Products
			{
				get { return _products; }
			}
			
			public bool MatchDate(DateTime date)
			{
				if (_date.Year != date.Year)
					return false;
				if (_date.Month != date.Month)
					return false;
				if (_date.Day != date.Day)
					return false;
				return true;
			}
			
			public void AddProductStockRecord(string productId, int count)
			{
				ProductCount pc = null;
				
				foreach (ProductCount p in _products)
				{
					if (p.Id.Equals(productId))
					{
						pc = p;
						break;
					}
				}
				
				if (null == pc)
				{
					pc = new ProductCount(productId);
					_products.Add(pc);
				}
				pc.AddCount(count);
			}
		}
		
		private class DayStatListViewItem : ListViewItem
		{
			private DayStat _daystat;
			
			public DayStatListViewItem(DayStat daystat)
			{
				_daystat = daystat;
				this.Text = _daystat.Date.ToString("yyyy/MM/dd");
				this.SubItems.Add(_daystat.Products[0].Count.ToString());
				this.SubItems.Add(_daystat.Products[1].Count.ToString());
				this.SubItems.Add(_daystat.Products[2].Count.ToString());
				this.SubItems.Add(_daystat.Products[3].Count.ToString());
				this.SubItems.Add(_daystat.Products[4].Count.ToString());
				this.SubItems.Add(_daystat.Products[5].Count.ToString());

				int total = _daystat.Products[0].Count;
				total += _daystat.Products[1].Count;
				total += _daystat.Products[2].Count;
				total += _daystat.Products[3].Count;
				total += _daystat.Products[4].Count;
				total += _daystat.Products[5].Count;
				
				this.SubItems.Add(total.ToString());
			}
			
			public DayStat DayStat
			{
				get { return _daystat; }
			}
		}
		
		private class Peroid
		{
			private int _days;
			
			public Peroid(int days)
			{
				_days = days;
			}
			
			public int Days
			{
				get { return _days; }
			}

			public override string ToString()
			{
				if (_days >= 9999)
					return "全部显示";
				return string.Format("最近{0}天", _days);
			}
		}
		
		private DateTimePicker _dtpFrom;
		private DateTimePicker _dtpTo;
		private List<DayStat> _daystats;
	
		public StockStatForm()
		{
			InitializeComponent();

			Label lblFrom = new Label();
			lblFrom.Text = "From: ";
			lblFrom.Font = this.Font;
			lblFrom.ForeColor = this.ForeColor;
			lblFrom.TextAlign = ContentAlignment.MiddleLeft;
			lblFrom.Margin = new Padding(6, 0, 0, 0);

			Label lblTo = new Label();
			lblTo.Text = "To: ";
			lblTo.Font = this.Font;
			lblTo.ForeColor = this.ForeColor;
			lblTo.TextAlign = ContentAlignment.MiddleLeft;
			lblTo.Margin = new Padding(6, 0, 0, 0);
			
			_dtpFrom = new DateTimePicker();
			_dtpTo = new DateTimePicker();
			
			Button btnGo = new Button();
			btnGo.Text = "Go";
			btnGo.Width = 30;
			btnGo.Click += new EventHandler(btnGo_Click);

			tsMain.Items.Add(new ToolStripControlHost(lblFrom));
			tsMain.Items.Add(new ToolStripControlHost(_dtpFrom));
			tsMain.Items.Add(new ToolStripControlHost(lblTo));
			tsMain.Items.Add(new ToolStripControlHost(_dtpTo));
			tsMain.Items.Add(new ToolStripControlHost(btnGo));

			tscboPeroid.Items.Add(new Peroid(3));
			tscboPeroid.Items.Add(new Peroid(7));
			tscboPeroid.Items.Add(new Peroid(14));
			tscboPeroid.Items.Add(new Peroid(30));
			tscboPeroid.Items.Add(new Peroid(9999));
			tscboPeroid.SelectedIndexChanged += new EventHandler(tscboPeroid_SelectedIndexChanged);
			tscboPeroid.SelectedIndex = 1;
		}

		void btnGo_Click(object sender, EventArgs e)
		{
			if (null == _daystats)
				return;
			RefreshList();
		}

		void tscboPeroid_SelectedIndexChanged(object sender, EventArgs e)
		{
			_dtpFrom.Value = DateTime.Now.AddDays(-1 * ((Peroid)tscboPeroid.SelectedItem).Days);
		}

		private void StockStatForm_Shown(object sender, EventArgs e)
		{
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(string.Format(MainForm.URL_DATA_CENTER, "getstockhistorysh"));
			request.Method = "GET";
			request.ContentType = "text/xml";
			WebResponse response = request.GetResponse();
			StreamReader reader = new StreamReader(response.GetResponseStream());
			//Trace.WriteLine(reader.ReadToEnd());
			//Trace.WriteLine("");
			string xml = reader.ReadToEnd();
			reader.Close();
			
			Stat(xml);
		}
		
		private void Stat(string xml)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			
			XmlNodeList nlStockout = doc.SelectNodes(".//action[@amount<0]");
			_daystats = new List<DayStat>();
			
			foreach (XmlNode nodeStockout in nlStockout)
			{
				DateTime date = DateTime.Parse(nodeStockout.Attributes.GetNamedItem("date").Value);
				DayStat day = GetDayStat(_daystats, date);
				if (null == day)
				{
					day = new DayStat(date);
					_daystats.Add(day);
				}

				day.AddProductStockRecord(
					nodeStockout.Attributes.GetNamedItem("product").Value,
					-1 * int.Parse(nodeStockout.Attributes.GetNamedItem("amount").Value));
			}
			
			// draw output information.
			//picStat.Image = DrawStatImage(_daystats);
			RefreshList();			
		}
		
		private void RefreshList()
		{
			lvwDayStats.Items.Clear();
		
			int total0 = 0, total1 = 0, total2 = 0, total3 = 0, total4 = 0, total5 = 0;

			foreach (DayStat day in _daystats)
			{
				if (day.Date < _dtpFrom.Value || day.Date > _dtpTo.Value)
					continue;
			
				DayStatListViewItem item = new DayStatListViewItem(day);
				lvwDayStats.Items.Add(item);

				total0 += day.Products[0].Count;
				total1 += day.Products[1].Count;
				total2 += day.Products[2].Count;
				total3 += day.Products[3].Count;
				total4 += day.Products[4].Count;
				total5 += day.Products[5].Count;
			}

			lblTotalInfo.Text = string.Format(
				"统计时间段: \n{7} - {8}\n\nPre: {0}({9:0.0})\n1段: {1}({10:0.0})\n2段: {2}({11:0.0})\n3段: {3}({12:0.0})\n1+: {4}({13:0.0})\n2+: {5}({14:0.0})\n\n总计: {6}({15:0.0})",
				total0, total1, total2, total3, total4, total5,
				total0 + total1 + total2 + total3 + total4 + total5,
				((DayStatListViewItem)lvwDayStats.Items[lvwDayStats.Items.Count-1]).DayStat.Date.ToString("yyyy/MM/dd"),
				((DayStatListViewItem)lvwDayStats.Items[0]).DayStat.Date.ToString("yyyy/MM/dd"),
				(float)total0 / lvwDayStats.Items.Count, (float)total1 / lvwDayStats.Items.Count, (float)total2 / lvwDayStats.Items.Count,
				(float)total3 / lvwDayStats.Items.Count, (float)total4 / lvwDayStats.Items.Count, (float)total5 / lvwDayStats.Items.Count,
				(float)(total0 + total1 + total2 + total3 + total4 + total5) / lvwDayStats.Items.Count);
		}
		
		/*
		private Bitmap DrawStatImage(List<DayStat> days)
		{
			Bitmap bmp = new Bitmap(1280, 1600);
			Graphics g = Graphics.FromImage(bmp);
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			
			int y = 0;
			
			for (int i = 0; i < 7; i++)
			{
				DayStat day = days[i];
				g.DrawString(
					day.Date.ToString("yyyy/MM/dd"), 
					new Font(this.Font.Name, 12, FontStyle.Bold), 
					new SolidBrush(Color.FromArgb(0xff, 0x60, 0x60, 0x60)), 
					new Point(0, y)); 
				y += (int)(g.MeasureString(day.Date.ToString("yyyy/MM/dd"), new Font(this.Font.Name, 12, FontStyle.Bold)).Height + 3);
				
				foreach (ProductCount pc in day.Products)
				{
					g.DrawString(ProductInfo.GetProductDesc(pc.Id), this.Font, new SolidBrush(this.ForeColor), new Point(32, y));
					g.DrawString(pc.Count.ToString(), this.Font, new SolidBrush(this.ForeColor), new Point(128, y));
					y += (int)(g.MeasureString(ProductInfo.GetProductDesc(pc.Id), new Font(this.Font.Name, 12, FontStyle.Bold)).Height);
				}
				
				y += 6;
			}
			
			// diagram.
			// axies
			g.DrawLine(new Pen(Color.Gray), new Point(16, y), new Point(16, y+300));
			g.DrawLine(new Pen(Color.Gray), new Point(16, y + 300), new Point(16+1240, y + 300));

			Point[] points0 = new Point[31];
			Point[] points1 = new Point[31];
			Point[] points2 = new Point[31];
			Point[] points3 = new Point[31];
			Point[] points4 = new Point[31];
			Point[] points5 = new Point[31];
			
			for (int i = 30; i >= 0; i--)
			{
				DayStat day = days[i];
				g.DrawString(day.Date.ToString("MM.dd"), this.Font, new SolidBrush(this.ForeColor), new Point(16 + (30-i)*40, y + 300 + 2));

				points0[i] = new Point(16 + (30 - i) * 40 + 20, y + 300 - day.Products[0].Count * 5);
				points1[i] = new Point(16 + (30 - i) * 40 + 20, y + 300 - day.Products[1].Count * 5);
				points2[i] = new Point(16 + (30 - i) * 40 + 20, y + 300 - day.Products[2].Count * 5);
				points3[i] = new Point(16 + (30 - i) * 40 + 20, y + 300 - day.Products[3].Count * 5);
				points4[i] = new Point(16 + (30 - i) * 40 + 20, y + 300 - day.Products[4].Count * 5);
				points5[i] = new Point(16 + (30 - i) * 40 + 20, y + 300 - day.Products[5].Count * 5);
				
				//g.DrawString(".", new Font(this.Font, FontStyle.Bold), new SolidBrush(Color.Red), new PointF(16 + (30 - i) * 40 + 20, y + 300 - day.Products[0].Count * 5));
				//g.DrawString(".", new Font(this.Font, FontStyle.Bold), new SolidBrush(Color.Blue), new PointF(16 + (30 - i) * 40 + 20, y + 300 - day.Products[1].Count * 5));
				//g.DrawString(".", new Font(this.Font, FontStyle.Bold), new SolidBrush(Color.Green), new PointF(16 + (30 - i) * 40 + 20, y + 300 - day.Products[2].Count * 5));
				//g.DrawString(".", new Font(this.Font, FontStyle.Bold), new SolidBrush(Color.Yellow), new PointF(16 + (30 - i) * 40 + 20, y + 300 - day.Products[3].Count * 5));
				//g.DrawString(".", new Font(this.Font, FontStyle.Bold), new SolidBrush(Color.Pink), new PointF(16 + (30 - i) * 40 + 20, y + 300 - day.Products[4].Count * 5));
				//g.DrawString(".", new Font(this.Font, FontStyle.Bold), new SolidBrush(Color.Orange), new PointF(16 + (30 - i) * 40 + 20, y + 300 - day.Products[5].Count * 5));
			}

			g.DrawLines(new Pen(Color.Purple), points0);
			g.DrawLines(new Pen(Color.Blue), points1);
			g.DrawLines(new Pen(Color.Yellow), points2);
			g.DrawLines(new Pen(Color.Pink), points3);
			g.DrawLines(new Pen(Color.Red), points4);
			g.DrawLines(new Pen(Color.Green), points5);		
			
			return bmp;
		}
		*/
		
		private DayStat GetDayStat(List<DayStat> days, DateTime date)
		{
			if (null == days || days.Count <= 0)
				return null;
			
			foreach (DayStat day in days)
			{
				if (day.MatchDate(date))
					return day;
			}
			
			return null;
		}
	}
}