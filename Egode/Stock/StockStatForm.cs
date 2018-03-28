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
	
		#region class DayStat
		private abstract class DayStat
		{
			protected DateTime _date;
			protected List<ProductCount> _products;
			
			public DayStat(DateTime date)
			{
				_date = date;
				_products = new List<ProductCount>();
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
				//ProductCount pc = null;
				
				foreach (ProductCount p in _products)
				{
					if (p.Id.Equals(productId))
					{
						//pc = p;
						p.AddCount(count);
						break;
					}
				}
				
				//if (null == pc)
				//{
				//    pc = new ProductCount(productId);
				//    _products.Add(pc);
				//}
				//pc.AddCount(count);
			}
		}
		
		private class DayStatAttribute : Attribute
		{
			private readonly string _name;
			public DayStatAttribute(string name)
			{
				_name = name;
			}
			
			public string Name
			{
				get { return _name; }
			}
		}

		[DayStatAttribute("Aptamil")]
		private class AptamilDayStat : DayStat
		{
			public AptamilDayStat(DateTime date)
				: base(date)
			{
				_products.Add(new ProductCount("001-0001"));
				_products.Add(new ProductCount("001-0002"));
				_products.Add(new ProductCount("001-0003"));
				_products.Add(new ProductCount("001-0004"));
				_products.Add(new ProductCount("001-0005"));
				_products.Add(new ProductCount("001-0006"));
			}
		}

		[DayStatAttribute("Aptamil Profutura")]
		private class AptamilProfuturaDayStat : DayStat
		{
			public AptamilProfuturaDayStat(DateTime date)
				: base(date)
			{
				_products.Add(new ProductCount("001-0016"));
				_products.Add(new ProductCount("001-0017"));
				_products.Add(new ProductCount("001-0018"));
			}
		}

		[DayStatAttribute("Aptamil 1200g")]
		private class Aptamil1200DayStat : DayStat
		{
			public Aptamil1200DayStat(DateTime date)
				: base(date)
			{
				_products.Add(new ProductCount("001-0012"));
				_products.Add(new ProductCount("001-0013"));
				_products.Add(new ProductCount("001-0014"));
				_products.Add(new ProductCount("001-0015"));
			}
		}

		[DayStatAttribute("HiPP Bio")]
		private class HiPPBioDayStat : DayStat
		{
			public HiPPBioDayStat(DateTime date)
				: base(date)
			{
				_products.Add(new ProductCount("002-0001"));
				_products.Add(new ProductCount("002-0002"));
				_products.Add(new ProductCount("002-0003"));
				_products.Add(new ProductCount("002-0004"));
				_products.Add(new ProductCount("002-0005"));
			}
		}

		[DayStatAttribute("HiPP Combiotik")]
		private class HiPPCombiotikDayStat : DayStat
		{
			public HiPPCombiotikDayStat(DateTime date)
				: base(date)
			{
				_products.Add(new ProductCount("002-0006"));
				_products.Add(new ProductCount("002-0007"));
				_products.Add(new ProductCount("002-0008"));
				_products.Add(new ProductCount("002-0009"));
				_products.Add(new ProductCount("002-0010"));
				_products.Add(new ProductCount("002-0011"));
			}
		}

		[DayStatAttribute("HiPP Corn 1")]
		private class HiPPCorn1DayStat : DayStat
		{
			public HiPPCorn1DayStat(DateTime date)
				: base(date)
			{
				_products.Add(new ProductCount("002-0012"));
				_products.Add(new ProductCount("002-0041"));
				_products.Add(new ProductCount("002-0015"));
				_products.Add(new ProductCount("002-0040"));
			}
		}

		[DayStatAttribute("HiPP Corn 2")]
		private class HiPPCorn2DayStat : DayStat
		{
			public HiPPCorn2DayStat(DateTime date)
				: base(date)
			{
				_products.Add(new ProductCount("002-0025"));
				_products.Add(new ProductCount("002-0018"));
				_products.Add(new ProductCount("002-0020"));
			}
		}

		[DayStatAttribute("HiPP Corn 3")]
		private class HiPPCorn3DayStat : DayStat
		{
			public HiPPCorn3DayStat(DateTime date)
				: base(date)
			{
				_products.Add(new ProductCount("002-0033"));
				_products.Add(new ProductCount("002-0037"));
				_products.Add(new ProductCount("002-0027"));
				_products.Add(new ProductCount("002-0024"));
			}
		}

		[DayStatAttribute("altapharma")]
		private class AltapharmaDayStat : DayStat
		{
			public AltapharmaDayStat(DateTime date)
				: base(date)
			{
				_products.Add(new ProductCount("033-0001"));
				_products.Add(new ProductCount("033-0002"));
				_products.Add(new ProductCount("033-0003"));
				_products.Add(new ProductCount("033-0004"));
				_products.Add(new ProductCount("033-0005"));
				_products.Add(new ProductCount("033-0006"));
			}
		}

		// for store and display in combobox of product bunds.
		private class DayStatInfo
		{
			private readonly Type _dayStatType;
			
			public DayStatInfo(Type t)
			{
				_dayStatType = t;
			}
			
			public Type DayStatType
			{
				get { return _dayStatType; }
			}

			public override string ToString()
			{
				return ((DayStatAttribute)_dayStatType.GetCustomAttributes(typeof(DayStatAttribute), false)[0]).Name;
			}
		}
		#endregion
		
		private class DayStatListViewItem : ListViewItem
		{
			private DayStat _daystat;
			
			public DayStatListViewItem(DayStat daystat)
			{
				_daystat = daystat;
				this.Text = _daystat.Date.ToString("yyyy/MM/dd");
				int total = 0;
				foreach (ProductCount pc in _daystat.Products)
				{
					this.SubItems.Add(pc.Count.ToString());
					total += pc.Count;
				}

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
		
		private string _databasePath; // Added by KK on 2015/12/16. 
		private DateTimePicker _dtpFrom;
		private DateTimePicker _dtpTo;
		private List<DayStat> _daystats;
		private string _stockHistoryXml;
	
		public StockStatForm(string databasePath)
		{
			_databasePath = databasePath;
		
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
			
			Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
			foreach (Type t in types)
			{
				if (!t.BaseType.Equals(typeof(DayStat)))
					continue;
				if (t.IsAbstract)
					continue;

				tscboProductBunds.Items.Add(new DayStatInfo(t));
			}
			tscboProductBunds.SelectedIndexChanged += new EventHandler(tscboProductBunds_SelectedIndexChanged);
			tscboProductBunds.SelectedIndex = 0;
		}

		void tscboProductBunds_SelectedIndexChanged(object sender, EventArgs e)
		{
			while (lvwDayStats.Columns.Count > 2)
				lvwDayStats.Columns.RemoveAt(1);
			
			ResetColumns();
			Stat(_stockHistoryXml);
		}
		
		void ResetColumns()
		{
			if (ProductInfo.Products.Count <= 0)
				return;

			DayStat ds = (DayStat)Activator.CreateInstance(((DayStatInfo)tscboProductBunds.SelectedItem).DayStatType, DateTime.Now);
			foreach (ProductCount pc in ds.Products)
				lvwDayStats.Columns.Insert(lvwDayStats.Columns.Count - 1, ProductInfo.GetProductInfo(pc.Id).ShortName, 80);
		}
		
		void btnGo_Click(object sender, EventArgs e)
		{
			if (null == _daystats)
				return;
			Stat(_stockHistoryXml);
			RefreshList();
		}

		void tscboPeroid_SelectedIndexChanged(object sender, EventArgs e)
		{
			_dtpFrom.Value = DateTime.Now.AddDays(-1 * ((Peroid)tscboPeroid.SelectedItem).Days);
		}

		private void StockStatForm_Shown(object sender, EventArgs e)
		{
			//HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(string.Format(MainForm.URL_DATA_CENTER, "getstockhistorysh"));
			//request.Method = "GET";
			//request.ContentType = "text/xml";
			//WebResponse response = request.GetResponse();
			//StreamReader reader = new StreamReader(response.GetResponseStream());
			////Trace.WriteLine(reader.ReadToEnd());
			////Trace.WriteLine("");
			
			PromptForm prompt = new PromptForm();
			prompt.MaxLine = 2;
			prompt.Owner = this;
			prompt.Show(this);

			StartDownloadProductInfos(prompt);
		}

		void StartDownloadProductInfos(PromptForm prompt)
		{
			prompt.AddMessage("正在下载产品信息...0%");
			WebClient wc = new WebClient();
			wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wcProductInfo_DownloadProgressChanged);
			wc.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wcProductInfo_DownloadDataCompleted);
			wc.DownloadDataAsync(new Uri(Common.URL_PRODUCTS), prompt);
		}

		void wcProductInfo_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;
			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("正在下载产品信息...{0}%", e.ProgressPercentage);
			prompt.RefreshDisplay();
		}

		void wcProductInfo_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;

			string xml = Encoding.UTF8.GetString(e.Result);
			ProductInfo.InitializeProducts(xml);

			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("成功下载{0}个产品信息.", ProductInfo.Products.Count);
			prompt.RefreshDisplay();

			ResetColumns();

			StartDownloadStockHistory(prompt);
		}
		
		void StartDownloadStockHistory(PromptForm prompt)
		{
			prompt.AddMessage("正在下载出入库数据...0%");
			WebClient wcDownloadStockHistory = new WebClient();
			wcDownloadStockHistory.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wcDownloadStockHistory_DownloadProgressChanged);
			wcDownloadStockHistory.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wcDownloadStockHistory_DownloadDataCompleted);
			wcDownloadStockHistory.DownloadDataAsync(new Uri(_databasePath), prompt);
		}

		void wcDownloadStockHistory_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;
			prompt.OKEnabled = true;

			_stockHistoryXml = Encoding.UTF8.GetString(e.Result);
			Stat(_stockHistoryXml);
		}

		void wcDownloadStockHistory_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;
			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("正在下载出入库数据...{0}%", e.ProgressPercentage);
			prompt.RefreshDisplay();
		}
		
		private void Stat(string xml)
		{
			if (string.IsNullOrEmpty(xml))
				return;
		
			lvwDayStats.Items.Clear();
			
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
					day = (DayStat)Activator.CreateInstance(((DayStatInfo)tscboProductBunds.SelectedItem).DayStatType, date);
					_daystats.Add(day);
				}

				day.AddProductStockRecord(
					nodeStockout.Attributes.GetNamedItem("product").Value,
					-1 * int.Parse(nodeStockout.Attributes.GetNamedItem("amount").Value));
			}
			
			RefreshList();			
		}
		
		private void RefreshList()
		{
			try
			{
				lvwDayStats.Items.Clear();
			
				int[] totals = new int[_daystats[0].Products.Count];// total0 = 0, total1 = 0, total2 = 0, total3 = 0, total4 = 0, total5 = 0;

				foreach (DayStat day in _daystats)
				{
					if (day.Date < _dtpFrom.Value || day.Date > _dtpTo.Value)
						continue;
				
					DayStatListViewItem item = new DayStatListViewItem(day);
					lvwDayStats.Items.Add(item);

					for (int i = 0; i < day.Products.Count; i++)
						totals[i] += day.Products[i].Count;
				}
				
				if (lvwDayStats.Items.Count > 0)
				{
					StringBuilder sb = new StringBuilder();
					sb.Append(string.Format("统计时间段: \n{0} - {1}\n\n",
						((DayStatListViewItem)lvwDayStats.Items[lvwDayStats.Items.Count - 1]).DayStat.Date.ToString("yyyy/MM/dd"),
						((DayStatListViewItem)lvwDayStats.Items[0]).DayStat.Date.ToString("yyyy/MM/dd")));
					
					for (int i = 0; i < _daystats[0].Products.Count; i++)
					{
						ProductCount pc = _daystats[0].Products[i];
						sb.Append(string.Format(
							"{0}: {1}({2:0.0})\n", 
							ProductInfo.GetProductInfo(pc.Id).ShortName, 
							totals[i], (float)totals[i] / lvwDayStats.Items.Count));
					}
					
					int periodTotal = 0;
					foreach (int i in totals)
						periodTotal += i;
					
					sb.Append(string.Format("\n总计: {0}({1:0.0})", periodTotal, (float)periodTotal / lvwDayStats.Items.Count));
					
					lblTotalInfo.Text = sb.ToString();

					//lblTotalInfo.Text = string.Format(
					//    "统计时间段: \n{7} - {8}\n\nPre: {0}({9:0.0})\n1段: {1}({10:0.0})\n2段: {2}({11:0.0})\n3段: {3}({12:0.0})\n1+: {4}({13:0.0})\n2+: {5}({14:0.0})\n\n总计: {6}({15:0.0})",
					//    total0, total1, total2, total3, total4, total5,
					//    total0 + total1 + total2 + total3 + total4 + total5,
					//    ((DayStatListViewItem)lvwDayStats.Items[lvwDayStats.Items.Count-1]).DayStat.Date.ToString("yyyy/MM/dd"),
					//    ((DayStatListViewItem)lvwDayStats.Items[0]).DayStat.Date.ToString("yyyy/MM/dd"),
					//    (float)total0 / lvwDayStats.Items.Count, (float)total1 / lvwDayStats.Items.Count, (float)total2 / lvwDayStats.Items.Count,
					//    (float)total3 / lvwDayStats.Items.Count, (float)total4 / lvwDayStats.Items.Count, (float)total5 / lvwDayStats.Items.Count,
					//    (float)(total0 + total1 + total2 + total3 + total4 + total5) / lvwDayStats.Items.Count);
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
			}
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