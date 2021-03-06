using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.IO;
using System.Resources;
using OrderLib;

namespace Egode
{
	public partial class MainForm : Form
	{
		private const int NUMBER_PER_PAGE = 200;
		
		#region class DurationFilter
		private class DurationFilter
		{
			private readonly int _days;
		
			public DurationFilter(int days)
			{
				_days = days;
			}
			
			public int Days
			{
				get { return _days; }
			}

			public override string ToString()
			{
				if (_days > 36500) // 100 years...-_-|||
					return "全部显示";
				
				return string.Format("仅显示{0}天内订单", _days);
			}
		}
		#endregion
		
		#region class ShippingOriginFilter
		private class ShippingOriginFilter
		{
			private readonly ShippingOrigins _shippingOrigin;
			
			public ShippingOriginFilter(ShippingOrigins origin)
			{
				_shippingOrigin = origin;
			}
			
			public ShippingOrigins ShippingOrigin
			{
				get { return _shippingOrigin; }
			}

			public override string ToString()
			{
				if (ShippingOrigins.Deutschland == _shippingOrigin)
					return "直邮";
				else if (ShippingOrigins.Shanghai == _shippingOrigin)
					return "现货";
				else if (ShippingOrigins.Ningbo == _shippingOrigin)
					return "宁波";
				return "All";
			}
		}
		#endregion
		
		#region class CountItem
		private class CountItem
		{
			private string _name;
			private int _count;
			
			public CountItem(string name)
			{
				_name = name;
			}
			
			public string Name
			{
				get { return _name; }
			}
			
			public int Count
			{
				get { return _count; }
			}
			
			public void Add()
			{
				_count++;
			}
		}
		#endregion
		
		private static MainForm _instance;
		
		private bool _localDb;
		
		private LinkLabel lblPrevPage;
		private LinkLabel lblNextPage;
		private Label lblPageInfo;
		private ComboBox cboDurationFilters;
		private ComboBox cboShippingOriginFilters;
		private CheckBox chkShowBooking;

		//private string _downloadedOrderXml; // 每次刷新(搜索、过滤)都必须重新加载下面的_orders, 否则程序的user objects会1直增加直至程序崩溃. i dont know why!!!
		private List<Order> _orders;
		private List<Order> _filteredOrders;
		private List<PacketInfo> _packets;
		private int _currentPage;

		private string _filterBuyer;
		
		private List<string> _warningKeywords;

		private MainForm(bool localDb)
		{
			_localDb = localDb;
			InitializeComponent();
		}
		
		public static MainForm Instance
		{
			get 
			{
				if (null == _instance)
					_instance = new MainForm(false);
				return _instance;
			}
		}
		
		public Order GetOrder(string orderId)
		{
			if (string.IsNullOrEmpty(orderId))
				return null;
			if (null == _orders || _orders.Count <= 0)
				return null;
			
			foreach (Order o in _orders)
			{
				if (o.OrderId.Equals(orderId))
					return o;
			}
			return null;
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			this.DoubleBuffered = true;
			this.Text = string.Format("Orders - {0}", ShopProfile.Current.DisplayName);

			CheckBox chkShowDeal = new CheckBox();
			chkShowDeal.Text = "未付款";
			chkShowDeal.BackColor = Color.Transparent;
			chkShowDeal.AutoSize = true;
			chkShowDeal.Padding = new Padding(2, 0, 6, 0);
			chkShowDeal.Checked = Settings.Instance.ShowDeal;
			chkShowDeal.CheckedChanged += new EventHandler(chkShowDeal_CheckedChanged);
			tsMain.Items.Add(new ToolStripControlHost(chkShowDeal));

			CheckBox chkShowPaid = new CheckBox();
			chkShowPaid.Text = "已付款";
			chkShowPaid.BackColor = Color.Transparent;
			chkShowPaid.AutoSize = true;
			chkShowPaid.Padding = new Padding(0, 0, 6, 0);
			chkShowPaid.Checked = Settings.Instance.ShowPaid;
			chkShowPaid.CheckedChanged += new EventHandler(chkShowPaid_CheckedChanged);
			tsMain.Items.Add(new ToolStripControlHost(chkShowPaid));

			CheckBox chkShowPrepared = new CheckBox();
			chkShowPrepared.Text = "已出单";
			chkShowPrepared.BackColor = Color.Transparent;
			chkShowPrepared.AutoSize = true;
			chkShowPrepared.Padding = new Padding(0, 0, 6, 0);
			chkShowPrepared.Checked = Settings.Instance.ShowPrepared;
			chkShowPrepared.CheckedChanged += new EventHandler(chkShowPrepared_CheckedChanged);
			tsMain.Items.Add(new ToolStripControlHost(chkShowPrepared));

			CheckBox chkShowSent = new CheckBox();
			chkShowSent.Text = "已发货";
			chkShowSent.BackColor = Color.Transparent;
			chkShowSent.AutoSize = true;
			chkShowSent.Padding = new Padding(0, 0, 6, 0);
			chkShowSent.Checked = Settings.Instance.ShowSent;
			chkShowSent.CheckedChanged += new EventHandler(chkShowSent_CheckedChanged);
			tsMain.Items.Add(new ToolStripControlHost(chkShowSent));

			CheckBox chkShowSucceeded = new CheckBox();
			chkShowSucceeded.Text = "交易成功";
			chkShowSucceeded.BackColor = Color.Transparent;
			chkShowSucceeded.AutoSize = true;
			chkShowSucceeded.Padding = new Padding(0, 0, 6, 0);
			chkShowSucceeded.Checked = Settings.Instance.ShowSucceeded;
			chkShowSucceeded.CheckedChanged += new EventHandler(chkShowSucceeded_CheckedChanged);
			tsMain.Items.Add(new ToolStripControlHost(chkShowSucceeded));

			CheckBox chkShowClosed = new CheckBox();
			chkShowClosed.Text = "已关闭";
			chkShowClosed.BackColor = Color.Transparent;
			chkShowClosed.AutoSize = true;
			chkShowClosed.Padding = new Padding(0, 0, 0, 0);
			chkShowClosed.Checked = Settings.Instance.ShowClosed;
			chkShowClosed.CheckedChanged += new EventHandler(chkShowClosed_CheckedChanged);
			tsMain.Items.Add(new ToolStripControlHost(chkShowClosed));
			
			tsMain.Items.Add(new ToolStripSeparator());
			
			cboDurationFilters = new ComboBox();
			cboDurationFilters.DropDownStyle = ComboBoxStyle.DropDownList;
			cboDurationFilters.ForeColor = Color.FromArgb(0x60, 0x60, 0x60);
			cboDurationFilters.Items.Add(new DurationFilter(3));
			cboDurationFilters.Items.Add(new DurationFilter(7));
			cboDurationFilters.Items.Add(new DurationFilter(30));
			cboDurationFilters.Items.Add(new DurationFilter(365000));
			cboDurationFilters.SelectedIndex = Settings.Instance.DurationFilterIndex;
			cboDurationFilters.SelectedIndexChanged += new EventHandler(cboDurationFilters_SelectedIndexChanged);
			tsMain.Items.Add(new ToolStripControlHost(cboDurationFilters));
			
			// just space.
			Label lbl = new Label();
			lbl.AutoSize = true;
			lbl.Text = " ";
			lbl.BackColor = Color.Transparent;
			tsMain.Items.Add(new ToolStripControlHost(lbl));
			
			cboShippingOriginFilters = new ComboBox();
			cboShippingOriginFilters.DropDownStyle = ComboBoxStyle.DropDownList;
			cboShippingOriginFilters.ForeColor = Color.FromArgb(0x60, 0x60, 0x60);
			cboShippingOriginFilters.Width = 60;
			cboShippingOriginFilters.Margin = new Padding(6, 0, 0, 0);
			cboShippingOriginFilters.Items.Add(new ShippingOriginFilter(ShippingOrigins.Deutschland));
			cboShippingOriginFilters.Items.Add(new ShippingOriginFilter(ShippingOrigins.Shanghai));
			cboShippingOriginFilters.Items.Add(new ShippingOriginFilter(ShippingOrigins.Ningbo));
			cboShippingOriginFilters.Items.Add(new ShippingOriginFilter(ShippingOrigins.Deutschland | ShippingOrigins.Shanghai | ShippingOrigins.Ningbo));
			cboShippingOriginFilters.SelectedIndex = Settings.Instance.ShippingOriginFilterIndex;
			cboShippingOriginFilters.SelectedIndexChanged += new EventHandler(cboShippingOriginFilters_SelectedIndexChanged);
			tsMain.Items.Add(new ToolStripControlHost(cboShippingOriginFilters));
			
			chkShowBooking = new CheckBox();
			chkShowBooking.Text = "显示预售";
			chkShowBooking.Checked = false;
			chkShowBooking.CheckedChanged += new EventHandler(chkShowBooking_CheckedChanged);
			tsMain.Items.Add(new ToolStripControlHost(chkShowBooking));

			tsMain.Items.Add(new ToolStripSeparator());

			lblNextPage = new LinkLabel();
			lblNextPage.Text = "下一页";
			lblNextPage.Enabled = false;
			lblNextPage.Click += new EventHandler(lblNextPage_Click);
			ToolStripControlHost tschNextPage = new ToolStripControlHost(lblNextPage);
			tschNextPage.Alignment = ToolStripItemAlignment.Right;
			statusMain.Items.Add(tschNextPage);
			
			lblPageInfo = new Label();
			lblPageInfo.Text = "0/0";
			lblPageInfo.ForeColor = Color.FromArgb(0x60, 0x60, 0x60);
			ToolStripControlHost tschPageInfo = new ToolStripControlHost(lblPageInfo);
			tschPageInfo.Alignment = ToolStripItemAlignment.Right;
			statusMain.Items.Add(tschPageInfo);

			lblPrevPage = new LinkLabel();
			lblPrevPage.Text = "上一页";
			lblPrevPage.Enabled = false;
			lblPrevPage.Click += new EventHandler(lblPrevPage_Click);
			ToolStripControlHost tschPrevPage = new ToolStripControlHost(lblPrevPage);
			tschPrevPage.Alignment = ToolStripItemAlignment.Right;
			statusMain.Items.Add(tschPrevPage);
			
			tsslblInfo.ForeColor = Color.FromArgb(0x0, 0x60, 0xff);
			
			//LoadWarningKeywords();	
		}

		void chkShowBooking_CheckedChanged(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			Settings.Instance.ShowBooking = chkShowBooking.Checked;;
			Reload();
			Cursor.Current = Cursors.Default;
		}
		
		private void MainForm_Shown(object sender, EventArgs e)
		{
			if (!_localDb)
				tsbtnDownloadData_Click(tsbtnDownloadData, EventArgs.Empty);
			
			// Added by KK on 2017/12/01.
			new WebBrowserForms.LoginForm().ShowDialog(this);
		}

		void cboShippingOriginFilters_SelectedIndexChanged(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			Settings.Instance.ShippingOriginFilterIndex = ((ComboBox)sender).SelectedIndex;
			Reload();
			Cursor.Current = Cursors.Default;
		}

		void cboDurationFilters_SelectedIndexChanged(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			Settings.Instance.DurationFilterIndex = ((ComboBox)sender).SelectedIndex;
			Reload();
			Cursor.Current = Cursors.Default;
		}

		void lblPrevPage_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			SetCurrentPage(--_currentPage);
			RefreshOrderLists();
			Cursor.Current = Cursors.Default;
		}

		void lblNextPage_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			SetCurrentPage(++_currentPage);
			RefreshOrderLists();
			Cursor.Current = Cursors.Default;
		}

		void chkShowClosed_CheckedChanged(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			Settings.Instance.ShowClosed = ((CheckBox)sender).Checked;
			Reload();
			Cursor.Current = Cursors.Default;
		}

		void chkShowSucceeded_CheckedChanged(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			Settings.Instance.ShowSucceeded = ((CheckBox)sender).Checked;
			Reload();
			Cursor.Current = Cursors.Default;
		}

		void chkShowSent_CheckedChanged(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			Settings.Instance.ShowSent = ((CheckBox)sender).Checked;
			Reload();
			Cursor.Current = Cursors.Default;
		}

		void chkShowPrepared_CheckedChanged(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			Settings.Instance.ShowPrepared = ((CheckBox)sender).Checked;
			Reload();
			Cursor.Current = Cursors.Default;
		}

		void chkShowPaid_CheckedChanged(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			Settings.Instance.ShowPaid = ((CheckBox)sender).Checked;
			Reload();
			Cursor.Current = Cursors.Default;
		}

		void chkShowDeal_CheckedChanged(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			Settings.Instance.ShowDeal = ((CheckBox)sender).Checked;
			Reload();
			Cursor.Current = Cursors.Default;
		}

		private void LoadWarningKeywords()
		{
			string path = Path.Combine(Directory.GetParent(Application.ExecutablePath).FullName, "warningkeywords.txt");
			if (!System.IO.File.Exists(path))
				return;

			System.IO.StreamReader r = new StreamReader(path, Encoding.Default);
			_warningKeywords = new List<string>();
			while (!r.EndOfStream)
			{
				string s = r.ReadLine();
				_warningKeywords.Add(s.Trim());
			}
			r.Close();
		}

		// 从Page 1开始, 用户账号筛选保留, 关键字保留.
		void Reload()
		{
			/* Removed by KK on 2014/03/03.
			 * It needs lots of time to reload from xml!!!
			 * And cause memory error.
			// 如果不每次都重新生成_orders集合, 会导致刷新时object handles无法释放, 1直增加直到程序崩溃.
			// i dont know why!!!
			
			// 备份LocalPrepared状态.
			List<Order> orders = Order.LoadXmlStream(_downloadedOrderXml, true);
			if (null != _orders)
			{
				for (int i = 0; i < orders.Count; i++)
				{
					orders[i].Status = _orders[i].Status;
					orders[i].Prepared = _orders[i].Prepared;
					orders[i].LocalPrepared = _orders[i].LocalPrepared;
				}
			}
			_orders = orders;
			*/

			//_orders = Order.LoadXmlStream(_downloadedOrderXml, true);
			UpdateFilteredOrders();
			SetCurrentPage(0);
			lblPrevPage.Enabled = false;
			lblNextPage.Enabled = false;
			RefreshOrderLists();
		}
		
		void SetCurrentPage(int currentPage)
		{
			_currentPage = currentPage;
			int totalPages = (null == _filteredOrders ?  0 : _filteredOrders.Count / NUMBER_PER_PAGE);
			if (null != _filteredOrders && totalPages * NUMBER_PER_PAGE < _filteredOrders.Count)
				totalPages++;
			lblPageInfo.Text = string.Format("{0}/{1}", _currentPage+1, totalPages);
			//lblPageInfo.Text = string.Format("{0}/{1}", (_currentPage+1)*NUMBER_PER_PAGE, null != _filteredOrders ? _filteredOrders.Count : 0);
		}

		void UpdateFilteredOrders()
		{
			_filteredOrders = new List<Order>();
			
			if (null == _orders || _orders.Count <= 0)
				return;

			foreach (Order o in _orders)
			{
				if (MatchFilter(o, false))
					_filteredOrders.Add(o);
			}
			
			if (_filteredOrders.Count <= 0 && txtKeyword.Text.Length > 0)
			{
				DialogResult dr = MessageBox.Show(this, "当前过滤规则下未找到符合条件的订单.\n是否继续在所有订单中查找?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (DialogResult.Yes == dr)
				{
					foreach (Order o in _orders)
					{
						if (MatchFilter(o, true))
							_filteredOrders.Add(o);
					}
				}
			}
		}
		
		private bool MatchFilter(Order order, bool matchInAll)
		{
			if (null == order)
				return false;
			
			//if (!matchInAll && order.PayingTime > new DateTime(2014, 12, 12, 23, 59, 59))
			//    return false;
			
			if (!matchInAll)
			{
				if (order.Status == Order.OrderStatus.Deal && !Settings.Instance.ShowDeal)
					return false;
				if (order.Status == Order.OrderStatus.Paid || order.Status == Order.OrderStatus.PartialSent)
				{
					if (order.Prepared || order.LocalPrepared || order.PreparedNingbo || order.LocalPreparedNingbo)
					{
						if (!Settings.Instance.ShowPrepared)
							return false;
					}
					else if (!Settings.Instance.ShowPaid)
						return false;
				}
				if (order.Status == Order.OrderStatus.Sent && !Settings.Instance.ShowSent)
					return false;
				if (order.Status == Order.OrderStatus.PartialSent && (!Settings.Instance.ShowPaid && ! Settings.Instance.ShowSent))
					return false;
				if (order.Status == Order.OrderStatus.Succeeded && !Settings.Instance.ShowSucceeded)
					return false;
				if (order.Status == Order.OrderStatus.Closed && !Settings.Instance.ShowClosed)
					return false;
				
				// Duration filter.
				TimeSpan ts = DateTime.Now - order.DealTime;
				if (ts.Days > ((DurationFilter)cboDurationFilters.SelectedItem).Days)
					return false;
				
				//if (!chkShowBooking.Checked && order.Items.Contains("预售"))
				//    return false;
				//if (!order.Items.Contains("★") && (order.Items.Contains("12盒包邮包税") || order.Items.Contains("直邮9罐装")))
				//    return false;
				//if (string.IsNullOrEmpty(order.Remark))
				//    return false;
				// 发货地筛选.
				if (!MatchShippingOrigin(order))
					return false;
			}

			// buyer account.
			if (!string.IsNullOrEmpty(_filterBuyer) && !order.BuyerAccount.Equals(_filterBuyer))
				return false;
			
			// keyword of searching.
			if (!string.IsNullOrEmpty(txtKeyword.Text.Trim()))
			{
				string[] keywords = txtKeyword.Text.Trim().Split(' ');
				if (!order.MatchKeyword(keywords))
					return false;
			}
		
			return true;
		}
		
		private bool MatchShippingOrigin(Order o)
		{
			ShippingOrigins so = ((ShippingOriginFilter)cboShippingOriginFilters.SelectedItem).ShippingOrigin;
			string remark = o.Remark;
			remark = remark.Replace("＃", "#");
			
			//// 确定该订单是否全是保税区链接.
			//bool isAllBonded = true;
			//string[] products = o.Items.Split(new char[]{'★'});
			//foreach (string s in products)
			//{
			//    if (string.IsNullOrEmpty(s))
			//        continue;
			//    if (s.Contains("保税区"))
			//        continue;
			//    isAllBonded = false;
			//    break;
			//}

			switch (so)
			{
				case ShippingOrigins.Deutschland:
					//if (isAllBonded)
					//    return false;
					return (remark.Contains("#直邮") || remark.Contains("#包税直邮") || remark.ToLower().Contains("#dhl直邮") || (!remark.Contains("#现货") && !remark.Contains("#保税区")));
					break;
				
				case ShippingOrigins.Shanghai:
					//if (isAllBonded)
					//    return false;
					return (remark.Contains("#现货") || (!remark.Contains("#直邮") && !remark.Contains("#包税直邮") && !remark.ToLower().Contains("#dhl直邮") && !remark.Contains("#保税区")));
					break;
					
				case ShippingOrigins.Ningbo:
					//// 保税区按商品标题识别. 即不看备注.
					//if (!o.Items.Contains("保税区"))
					//    return false;
					return (remark.Contains("#保税区") || (!remark.Contains("#直邮") && !remark.Contains("#包税直邮") && !remark.ToLower().Contains("#dhl直邮") && !remark.Contains("#现货")));
					break;
			}
			
			return true;
		}
		
		private void DisposeControl(Control c)
		{
			foreach (Control sc in c.Controls)
				DisposeControl(sc);
			c.Dispose();
		}

		private void RefreshOrderLists()
		{
			pnlOrders.Controls.Clear();
			GC.Collect();
			
			if (null == _filteredOrders || _filteredOrders.Count <= 0)
				return;

			try
			{
				Application.DoEvents();
				pnlOrders.SuspendLayout();

				for (int i = _currentPage * NUMBER_PER_PAGE; i < ((_currentPage+1) * NUMBER_PER_PAGE); i++)
				{
					if (i < 0 || i >= _filteredOrders.Count)
						break;
					
					Order o = _filteredOrders[i];
	
					try
					{
						OrderDetailsControl odc = new OrderDetailsControl(o, Order.GetOrdersByBuyerAccount(o.BuyerAccount, _filteredOrders).Count);
						odc.OnBuyerClick += new EventHandler(odc_OnBuyerClick);
						odc.OnAddPacket += new OrderDetailsControl.PacketEventHandler(odc_OnAddPacket);
						odc.OnConsignSh += new EventHandler(odc_OnConsignSh);
						odc.OnConsignNb += new EventHandler(odc_OnConsignNb);
						pnlOrders.Controls.Add(odc);
						odc.Width = pnlOrders.Width - 26;
					}
					catch (Exception ex)
					{
						MessageBox.Show(
							this,
							string.Format("Error occured during initialize order:\nOrder ID: {0}\nBuyer: {1}\nDeal Time: {2}", o.OrderId, o.BuyerAccount, o.DealTime.ToString("yyyy/MM/dd HH:mm:ss")), this.Text,
							MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}

				pnlOrders.ResumeLayout();
				
				float totalMoney = 0;
				foreach (Order o in _filteredOrders)
					totalMoney += o.TotalMoney;

				lblPrevPage.Enabled = (_currentPage >= 1);
				lblNextPage.Enabled = ((_currentPage+1) * NUMBER_PER_PAGE) < _filteredOrders.Count;
				tsslblOrders.Text = string.Format("{0} orders filtered. Total Money: ¥{1}", _filteredOrders.Count, totalMoney.ToString("0.00"));
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
			}
		}

		public List<Order> GetBuyerOrders(string buyerAccount)
		{
			if (string.IsNullOrEmpty(buyerAccount))
				return null;

			List<Order> buyerOrders = new List<Order>();
			foreach (Order o in _orders)
			{
				if (o.BuyerAccount.Equals(buyerAccount))
					buyerOrders.Add(o);
			}
			
			return buyerOrders;
		}
		
		// 返回指定账号是否有多个订单. 仅检测deal, deal, sent, partialSent3种状态订单.
		bool IsMultipleOrders(string buyerAccount, List<Order> orders, out List<Order> buyerOrders, out string prompt)
		{
			buyerOrders = new List<Order>();
			prompt = string.Empty;

			if (string.IsNullOrEmpty(buyerAccount))
				return false;
			if (null == orders || orders.Count <= 0)
				return false;

			int deal = 0, paid = 0, sent = 0, partialSent = 0;
			foreach (Order o in orders)
			{
				if (!buyerAccount.Equals(o.BuyerAccount))
					continue;
				if (o.Status != Order.OrderStatus.Deal && o.Status != Order.OrderStatus.Paid && o.Status != Order.OrderStatus.Sent && o.Status != Order.OrderStatus.PartialSent)
					continue;
				buyerOrders.Add(o);
				
				switch(o.Status)
				{
					case Order.OrderStatus.Deal:
						deal++;
						break;
					case Order.OrderStatus.Paid:
						paid++;
						break;
					case Order.OrderStatus.Sent:
						sent++;
						break;
					case Order.OrderStatus.PartialSent:
						partialSent++;
						break;
				}
			}

			if (deal > 0)
				prompt += string.Format("未付款:{0}, ", deal.ToString());
			if (paid > 0)
				prompt += string.Format("已付款:{0}, ", paid.ToString());
			if (sent > 0)
				prompt += string.Format("已发货:{0}, ", sent.ToString());
			if (partialSent > 0)
				prompt += string.Format("部分发货:{0}, ", partialSent.ToString());

			if (prompt.EndsWith(", "))
				prompt = prompt.Substring(0, prompt.Length - 2);

			return (deal + paid + sent + partialSent > 1);
		}
		
		void odc_OnConsignSh(object sender, EventArgs e)
		{
			ConsignSh((OrderDetailsControl)sender, false);
		}
			
		void ConsignSh(OrderDetailsControl odc, bool silent)
		{
			if (null == odc)
				return;
			
			// Added by KK on 2018/01/13.
			if (odc.Order.MobileNumber.StartsWith("177") || odc.Order.MobileNumber.StartsWith("176"))
			{
				if (silent)
				{
					
				}
				else
				{
					DialogResult dr = MessageBox.Show(
						this, 
						string.Format("高危!\n电话号码以{0}开头.\n\n是否继续发货?", odc.Order.MobileNumber.Substring(0, 3)), this.Text, 
						MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
					if (DialogResult.Yes == dr)
						dr = MessageBox.Show(
							this, 
							string.Format("高危!\n电话号码以{0}开头.\n\n是否继续发货?", odc.Order.MobileNumber.Substring(0, 3)), this.Text, 
							MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
					if (DialogResult.No == dr)
						return;
				}
			}
		
			string bitchWarningMsg = string.Empty;
			if (CheckBitchRed(odc.Order, out bitchWarningMsg))
			{
				if (silent)
				{
				
				}
				else
				{
					DialogResult dr = MessageBox.Show(this, bitchWarningMsg, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					if (DialogResult.Yes == dr)
					{
						dr = MessageBox.Show(this, bitchWarningMsg, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
						if (DialogResult.No == dr)
							return;
					}
					else
						return;
				}
			}
			else if (CheckBitchBlue(odc.Order, out bitchWarningMsg))
			{
				if (silent)
				{
				}
				else
				{
					DialogResult dr = MessageBox.Show(this, bitchWarningMsg, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					if (DialogResult.No == dr)
						return;
				}
			}
			
			string orderDetailsHtml = WebBrowserForms.OrderDetailsPageWebBrowserForm.GetOrderDetailsHtml(odc.Order.OrderId, this);
			OrderDetailsScript orderDetailsScript = new OrderDetailsScript(orderDetailsHtml); // Added by KK on 2016/10/26.

			if (orderDetailsHtml.ToLower().Contains("退款信息"))
			{
				if (AvailableItemExistsInRefundOrder(orderDetailsHtml))
				{
					// Removed by KK. The following message will prompt on consigning form.
					//MessageBox.Show(
					//    this,
					//    "此订单有退款信息!\n并且存在待处理的商品!!\n为安全起见, 不自动识别商品, 需手动添加商品!!!", this.Text,
					//    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					MessageBox.Show(this, "此订单已申请退款. 终止发货!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}
			
			if (orderDetailsHtml.ToLower().Contains("交易关闭"))
			{
				MessageBox.Show(this, "此订单已关闭. 终止发货!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			
			if (!orderDetailsHtml.Contains("买家已付款，等待商家发货"))
			{
				DialogResult dr = MessageBox.Show(this, "此订单不是<买家已付款, 等待商家发货>状态, 是否继续发货操作?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (DialogResult.No == dr)
					return;
			}
			
			List<Order> selectedOrders = new List<Order>();

			string warning;
			List<Order> buyerOrders;
			if (!odc.Order.BuyerAccount.Equals("octech2013") && IsMultipleOrders(odc.Order.BuyerAccount, _orders, out buyerOrders, out warning))
			{
				OrdersForm of = new OrdersForm(buyerOrders, odc.Order);
				of.Prompt = string.Format("注意! 此账号有多个订单. {0}. 点<OK>继续发货, 点<Cancel>取消发货.", warning);
				DialogResult dr = of.ShowDialog(this);
				if (DialogResult.Cancel == dr)
					return;
				selectedOrders = of.SelectedOrders;
			}
			else
			{
				selectedOrders.Add(odc.Order);
			}

			// Have not get full edited address yet
			if (orderDetailsScript.HasNewAddress)
			{
				
				// Removed by KK on 2014/09/11. Do not prompt this message any more.
				//MessageBox.Show(
				//    this.FindForm(),
				//    "收货地址发生修改.\n导出数据无法获得完整修改后的地址, 需要访问订单详情页面获得完整地址.",
				//    this.Text,
				//    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				//odc.RefreshFullEditedAddress();
				odc.RefreshFullNewAddress(orderDetailsScript.NewAddress);
			}

			ConsignShForm csf = new ConsignShForm(selectedOrders, orderDetailsHtml.ToLower().Contains("退款信息"));
			
			// Added by KK on 2017/10/25.
			csf.Silent = silent;
			csf.ShowDialog(this);
		}
		
		bool CheckBitchRed(Order order, out string warningMsg)
		{
			warningMsg = string.Empty;
			
			if (null == order)
				return false;
			
			Blacklist b = Blacklist.MatchRed(order);
			if (null == b)
				return false;
			
			warningMsg = string.Format(
					"红色黑名单! 红色黑名单! 红色黑名单!\n\n[Red]\n{0}\n\n[Blue]\n{1}\n\n[Comment]\n{2}\n\n继续发货?", 
					b.Red, b.Blue, b.Comment);
			return true;
		}
		
		bool CheckBitchBlue(Order order, out string warningMsg)
		{
			warningMsg = string.Empty;
			
			if (null == order)
				return false;
			warningMsg = string.Empty;

			float matchPercentage = 0;
			Blacklist b = Blacklist.MatchBlue(order, out matchPercentage);
			if (null == b)
				return false;

			warningMsg = string.Format(
					"蓝色黑名单(匹配度：{0}): \n\n[Red]\n{1}\n\n[Blue]\n{2}\n\n[Comment]\n{3}\n\n继续发货?", 
					matchPercentage.ToString("p"), b.Red, b.Blue, b.Comment);
			return true;
		}

		void odc_OnConsignNb(object sender, EventArgs e)
		{
			OrderDetailsControl odc = sender as OrderDetailsControl;

			if (odc.Order.PreparedNingbo || odc.Order.LocalPreparedNingbo)
			{
				DialogResult dr = MessageBox.Show(this, "此订单已出单, 是否要继续出单?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (DialogResult.No == dr)
					return;
			}

			Blacklist b = Blacklist.MatchRed(odc.Order);
			if (null != b)
			{
				DialogResult dr = MessageBox.Show(
					this,
					string.Format("红色黑名单! 红色黑名单! 红色黑名单!\n{0}\n{1}\n{2}\n\n继续发货?", b.Red, b.Blue, b.Comment), this.Text,
					MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (DialogResult.Yes == dr)
				{
					dr = MessageBox.Show(
						this,
					string.Format("红色黑名单! 红色黑名单! 红色黑名单!\n{0}\n{1}\n{2}\n\n继续发货?", b.Red, b.Blue, b.Comment), this.Text,
						MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					if (DialogResult.No == dr)
						return;
				}
				else
					return;
			}
			else
			{
				float matchPercentage = 0;
				b = Blacklist.MatchBlue(odc.Order, out matchPercentage);
				if (null != b)
				{
					DialogResult dr = MessageBox.Show(
						this,
						string.Format("蓝色黑名单(匹配度：{0}): \n{1}\n{2}\n{3}\n\n继续发货?", matchPercentage.ToString("p"), b.Red, b.Blue, b.Comment), this.Text,
						MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					if (DialogResult.No == dr)
						return;
				}
			}

			float matchPercentage1 = 0;
			string warningKeyword = WarningKeyword.Match(odc.Order, out matchPercentage1);
			if (!string.IsNullOrEmpty(warningKeyword))
			{
				DialogResult dr = MessageBox.Show(
					this,
						string.Format("发现警告关键字(匹配度：{1}): \n{0}\n\n继续发货?", warningKeyword, matchPercentage1.ToString("p")), this.Text,
					MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (DialogResult.No == dr)
					return;
			}

			string orderDetailsHtml = WebBrowserForms.OrderDetailsPageWebBrowserForm.GetOrderDetailsHtml(odc.Order.OrderId, this);
			OrderDetailsScript orderDetailsScript = new OrderDetailsScript(orderDetailsHtml); // Added by KK on 2016/10/26.
			if (orderDetailsHtml.ToLower().Contains("退款信息"))
			{
				if (AvailableItemExistsInRefundOrder(orderDetailsHtml))
				{
					// Removed by KK. The following message will prompt on consigning form.
					//MessageBox.Show(
					//    this, 
					//    "此订单有退款信息!\n并且存在待处理的商品!!\n为安全起见, 不自动识别商品, 需手动添加商品!!!", this.Text, 
					//    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					MessageBox.Show(this, "此订单已申请退款. 终止发货!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}

			if (orderDetailsHtml.ToLower().Contains("交易关闭"))
			{
				MessageBox.Show(this, "此订单已关闭. 终止发货!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			if (!orderDetailsHtml.Contains("买家已付款，等待卖家发货"))
			{
				DialogResult dr = MessageBox.Show(this, "此订单不是<买家已付款, 等待卖家发货>状态, 是否继续发货操作?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (DialogResult.No == dr)
					return;
			}
			
			odc.Order.AlipayNumber = WebBrowserForms.OrderDetailsPageWebBrowserForm.GetAlipayNumber(orderDetailsHtml);
			
			List<Order> selectedOrders = new List<Order>();

			string warning;
			List<Order> buyerOrders;
			if (!odc.Order.BuyerAccount.Equals("octech2013") && IsMultipleOrders(odc.Order.BuyerAccount, _orders, out buyerOrders, out warning))
			{
				OrdersForm of = new OrdersForm(buyerOrders, odc.Order);
				of.Prompt = string.Format("注意! 此账号有多个订单. {0}. 点<OK>继续发货, 点<Cancel>取消发货.", warning);
				DialogResult dr = of.ShowDialog(this);
				if (DialogResult.Cancel == dr)
					return;
				selectedOrders = of.SelectedOrders;
			}
			else
			{
				selectedOrders.Add(odc.Order);
			}

			// Have not get full edited address yet
			if (orderDetailsScript.HasNewAddress)
			{
				// Removed by KK on 2014/09/11. Do not prompt this message any more.
				//MessageBox.Show(
				//    this.FindForm(),
				//    "收货地址发生修改.\n导出数据无法获得完整修改后的地址, 需要访问订单详情页面获得完整地址.",
				//    this.Text,
				//    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				//odc.RefreshFullEditedAddress();
				odc.RefreshFullNewAddress(orderDetailsScript.NewAddress);
			}

			ConsignNingboForm cnf = new ConsignNingboForm(selectedOrders, orderDetailsHtml.ToLower().Contains("退款信息"));
			cnf.OnNingboOrderAdded += new EventHandler(cnf_OnNingboOrderAdded);
			cnf.ShowDialog(this);
			tsslblNingboOrders.Text = string.Format("{0} Ningbo order{1}", NingboOrder.Orders.Count, NingboOrder.Orders.Count > 1 ? "s":string.Empty);
		}

		void cnf_OnNingboOrderAdded(object sender, EventArgs e)
		{
			tsslblNingboOrders.Text = string.Format("{0} Ningbo order{1}", NingboOrder.Orders.Count, NingboOrder.Orders.Count > 1 ? "s" : string.Empty);
			tsbtnNingboOrders.Enabled = NingboOrder.Orders.Count > 0;
		}

		void odc_OnAddPacket(object sender, PacketInfo p)
		{
			try
			{
				OrderDetailsControl odc = sender as OrderDetailsControl;

				Blacklist b = Blacklist.MatchRed(odc.Order);
				if (null != b)
				{
					DialogResult dr = MessageBox.Show(
						this, 
						string.Format("红色黑名单! 红色黑名单! 红色黑名单!\n{0}\n{1}\n{2}\n\n继续发货?", b.Red, b.Blue, b.Comment), this.Text, 
						MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					if (DialogResult.Yes == dr)
					{
						dr = MessageBox.Show(
							this,
						string.Format("红色黑名单! 红色黑名单! 红色黑名单!\n{0}\n{1}\n{2}\n\n继续发货?", b.Red, b.Blue, b.Comment), this.Text, 
							MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
						if (DialogResult.No == dr)
							return;
					}
					else
						return;
				}
				else
				{
					float matchPercentage = 0;
					b = Blacklist.MatchBlue(odc.Order, out matchPercentage);
					if (null != b)
					{
					DialogResult dr = MessageBox.Show(
						this,
						string.Format("蓝色黑名单(匹配度：{0}): \n{1}\n{2}\n{3}\n\n继续发货?", matchPercentage.ToString("p"), b.Red, b.Blue, b.Comment), this.Text,
						MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					if (DialogResult.No == dr)
						return;
					}
				}
				
				float matchPercentage1 = 0;
				string warningKeyword = WarningKeyword.Match(odc.Order, out matchPercentage1);
				if (!string.IsNullOrEmpty(warningKeyword))
				{
					DialogResult dr = MessageBox.Show(
						this,
							string.Format("发现警告关键字(匹配度：{1}): \n{0}\n\n继续发货?", warningKeyword, matchPercentage1.ToString("p")), this.Text,
						MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					if (DialogResult.No == dr)
						return;
				}

				string orderDetailsHtml = WebBrowserForms.OrderDetailsPageWebBrowserForm.GetOrderDetailsHtml(p.OrderId, this);
				OrderDetailsScript orderDetailsScript = new OrderDetailsScript(orderDetailsHtml); // Added by KK on 2016/10/26.
				if (orderDetailsHtml.Contains("对不起，获取页面内容失败，请刷新重试！"))
				{
					MessageBox.Show(this, "对不起，获取页面内容失败，请重试！\n(浏览器加载淘宝订单详情页失败, 非软件问题~)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (orderDetailsHtml.ToLower().Contains("退款信息"))
				{
					MessageBox.Show(this, "此订单已申请退款. 终止发货!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				if (orderDetailsHtml.ToLower().Contains("交易关闭"))
				{
					MessageBox.Show(this, "此订单已关闭. 终止发货!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				if (!orderDetailsHtml.Contains("买家已付款，等待卖家发货") && !orderDetailsHtml.Contains("买家已付款，等待卖家操作"))
				{
					DialogResult dr = MessageBox.Show(this, "此订单不是<买家已付款, 等待卖家发货>状态, 是否继续出单?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					if (DialogResult.No == dr)
						return;
				}

				if (odc.Order.LocalPrepared)
				{
					DialogResult dr = MessageBox.Show(
						this,
						"此订单已添加包裹单.\n是否要为此订单生成多个包裹单?", this.Text,
						MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (DialogResult.No == dr)
						return;
				}

				// Have not get full edited address yet
				if (orderDetailsScript.HasNewAddress)
				{
					// Removed by KK on 2014/09/11. Do not prompt this message any more.
					//MessageBox.Show(
					//    this,
					//    "收货地址发生修改.\n导出数据无法获得完整修改后的地址, 需要访问订单详情页面获得完整地址.",
					//    this.Text,
					//    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					//odc.RefreshFullEditedAddress();
					odc.RefreshFullNewAddress(orderDetailsScript.NewAddress);

					// Check if get edited address successfully.
					// Somtimes it would fail to get edited address.
					System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"\d{6,}"); // 至少包含6个连续数字, 表示电话号码
					System.Text.RegularExpressions.Match m = r.Match(odc.Order.EditedRecipientAddress);
					if (!m.Success)
					{
						MessageBox.Show(this, "收货地址发生修改, 并且无法读取完整修改后的地址. \n请手动更新修改后的地址再添加包裹单.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}

					p.FullAddress = odc.Order.EditedRecipientAddress;
				}

				string warning;
				List<Order> buyerOrders;
				if (IsMultipleOrders(Order.GetOrderByOrderId(p.OrderId, _orders).BuyerAccount, _orders, out buyerOrders, out warning))
				{
					OrdersForm of = new OrdersForm(buyerOrders, odc.Order);
					of.Prompt = string.Format("注意! 此账号有多个订单. {0}. 是否继续出单?", warning);
					DialogResult dr = of.ShowDialog(this);
					if (DialogResult.Cancel == dr)
						return;
					
					foreach (Order o in of.SelectedOrders)
					{
						o.LocalPrepare();
						if (!o.OrderId.Equals(p.OrderId))
							p.AppendProductLongString(o.Items);
					}
				}

				odc.Order.LocalPrepare();

				// motion effect.
				ResourceManager rm = new ResourceManager("Egode.Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());
				Image imgDhl = (Image)rm.GetObject("dhl");
				Point flyingDest = statusMain.PointToScreen(new Point(0, 2));
				FlyingForm flying = new FlyingForm(imgDhl, flyingDest);
				flying.Show();

				if (null == _packets)
					_packets = new List<PacketInfo>();

				_packets.Add(p);
				tsbtnGeneratePackets.Enabled = (_packets.Count > 0);
				tsslblInfo.Text = string.Format("{0} packet{1} added.", _packets.Count, _packets.Count > 1? "s":string.Empty);
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		
		// 根据有退款信息的订单的详情页html代码, 判定该订单中是否有需要处理的商品.
		// 主要针对部分退款订单, 会判定为退款中订单, 系统将会拒绝处理此订单.
		// 而对于部分退款, 部分商品仍需要正常处理的退款订单, 将会阻碍订单的正常执行.
		// 因此需要通过分析详情页代码, 判定退款中订单是否存在需要正常处理的商品.
		// 在详情页中, 如果某个商品状态显示为"未发货", 即需要正常处理.
		// 详情页中, 商品的退款信息会显示为: 退款中, 退款成功等.
		// 推而广之, 此函数实际可以针对所有订单进行判断, 而不局限于退款中订单. 考虑到效率, 仅对退款中订单做进1步的判定.
		private bool AvailableItemExistsInRefundOrder(string detailsPageHtml)
		{
			if (string.IsNullOrEmpty(detailsPageHtml))
				return false;
		
			XmlDocument xmldoc = Common.ConvertHtmlToXml(detailsPageHtml.ToLower());
			if (null == xmldoc)
				return false;
			
			XmlNodeList nlOrderItems = xmldoc.SelectNodes(@".//tr[@class='order-item']");
			if (null == nlOrderItems || nlOrderItems.Count <= 0)
				return false;
			
			foreach (XmlNode nodeOrderItem in nlOrderItems)
			{
				XmlNode nodeStatus = nodeOrderItem.SelectSingleNode(@".//td[@class='status']");
				if (null != nodeStatus && nodeStatus.InnerText.Trim().Equals("未发货"))
					return true;
			}
			return false;
		}
		
		/* obsoleted code
		private bool CheckRefund(string orderId)
		{
			if (string.IsNullOrEmpty(orderId))
				return false;
			
			//WebBrowserForms.OrderDetailsPageWebBrowserForm odpbf = new Egode.WebBrowserForms.OrderDetailsPageWebBrowserForm(orderId);
			//odpbf.ShowDialog(this);
			//if (odpbf.Html.ToLower().Contains("退款信息"))
			//    return true;
			//return false;
			
			WebBrowserForms.OrderDetailsPageWebBrowserForm.Instance.OrderId = orderId;
			WebBrowserForms.OrderDetailsPageWebBrowserForm.Instance.ShowDialog(this);
			if (WebBrowserForms.OrderDetailsPageWebBrowserForm.Instance.Html.ToLower().Contains("退款信息"))
			    return true;
			return false;
		}
		*/

		private void tsbtnDownloadData_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
		
			try
			{
				//TimeSpan ts = DateTime.Now - new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 0, 0);
				//if (Math.Abs(ts.TotalSeconds) <= 600)
				//{
				//    MessageBox.Show(
				//        this, 
				//        string.Format("啊哦...现在时间: {0}", DateTime.Now.ToString("HH:mm:ss")),
				//        this.Text,
				//        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				//    //return;
				//}

				if (null != _orders)
				{
					DialogResult dr = MessageBox.Show(
						this, 
						"重新下载将会覆盖未同步到服务器的数据(例如出单状态).\n是否继续下载?",
						this.Text, 
						MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					if (DialogResult.No == dr)
						return;
				}
				
				tsbtnDownloadData.Enabled = false;
				StartDownload();
			}
			catch (WebException ex)
			{
				MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
				tsbtnDownloadData.Enabled = true;
			}
		}
		
		void StartDownload()
		{
			PromptForm prompt = new PromptForm();
			prompt.MaxLine = 11;
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
			BrandInfo.InitializeBrands(xml);
			ProductInfo.InitializeProducts(xml);

			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("成功下载{0}个产品信息.", ProductInfo.Products.Count);
			prompt.RefreshDisplay();

			StartDownloadDistributors(prompt);
		}

		void StartDownloadDistributors(PromptForm prompt)
		{
			prompt.AddMessage("正在下载分销商信息...0%");
			WebClient wc = new WebClient();
			wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wcDistributor_DownloadProgressChanged);
			wc.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wcDistributor_DownloadDataCompleted);
			wc.DownloadDataAsync(new Uri(Common.URL_DISTRIBUTORS), prompt);
		}

		void wcDistributor_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;
			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("正在下载分销商信息...{0}%", e.ProgressPercentage);
			prompt.RefreshDisplay();
		}

		void wcDistributor_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;

			string xml = Encoding.UTF8.GetString(e.Result);
			Distributor.InitializeDistributors(xml);

			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("成功下载{0}个分销商信息.", Distributor.Distributors.Count);
			prompt.RefreshDisplay();

			StartDownloadBlacklist(prompt);
		}

		void StartDownloadBlacklist(PromptForm prompt)
		{
			prompt.AddMessage("正在下载黑名单...0%");
			WebClient wcDownloadBlacklist = new WebClient();
			wcDownloadBlacklist.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wcDownloadBlacklist_DownloadDataCompleted);
			wcDownloadBlacklist.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wcDownloadBlacklist_DownloadProgressChanged);
			wcDownloadBlacklist.DownloadDataAsync(new Uri(Common.URL_BLACKLIST), prompt);
		}

		void wcDownloadBlacklist_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;
			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("正在下载黑名单...{0}%", e.ProgressPercentage);
			prompt.RefreshDisplay();
		}

		void wcDownloadBlacklist_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;

			string xml = Encoding.UTF8.GetString(e.Result);
			Blacklist.InitializeBlacklist(xml);

			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("成功下载{0}个黑名单.", Blacklist.Blacklists.Count);
			prompt.RefreshDisplay();

			StartDownloadWarningKeywords(prompt);
		}

		void StartDownloadWarningKeywords(PromptForm prompt)
		{
			prompt.AddMessage("正在下载警告关键字...0%");
			WebClient wcDownloadWarningKeywords = new WebClient();
			wcDownloadWarningKeywords.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wcDownloadWarningKeywords_DownloadDataCompleted);
			wcDownloadWarningKeywords.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wcDownloadWarningKeywords_DownloadProgressChanged);
			wcDownloadWarningKeywords.DownloadDataAsync(new Uri(Common.URL_WARNING_KEYWARDS), prompt);
		}

		void wcDownloadWarningKeywords_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;
			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("正在下载警告关键字...{0}%", e.ProgressPercentage);
			prompt.RefreshDisplay();
		}

		void wcDownloadWarningKeywords_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;

			string s = Encoding.UTF8.GetString(e.Result);
			string[] kws = s.Split(new char[]{'\r', '\n'});
			int c = 0;
			foreach (string kw in kws)
			{
				if (string.IsNullOrEmpty(kw.Trim()))
					continue;
				WarningKeyword.AddKeywordRecord(kw.Trim());
				c++;
			}

			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("成功下载{0}个警告关键字.", c);
			prompt.RefreshDisplay();

			//StartDownloadSubjectInfos(prompt);
			StartDownloadPrepareHistory(prompt);
		}

		// Removed by KK on 2016/06/06.
		//void StartDownloadSubjectInfos(PromptForm prompt)
		//{
		//    prompt.AddMessage("正在下载产品标题数据...0%");
		//    WebClient wcDownloadSubjectInfos = new WebClient();
		//    wcDownloadSubjectInfos.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wcDownloadSubjectInfos_DownloadDataCompleted);
		//    wcDownloadSubjectInfos.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wcDownloadSubjectInfos_DownloadProgressChanged);
		//    wcDownloadSubjectInfos.DownloadDataAsync(new Uri(Common.URL_SUBJECTS), prompt);
		//}

		//void wcDownloadSubjectInfos_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		//{
		//    PromptForm prompt = e.UserState as PromptForm;
		//    prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("正在下载产品标题数据...{0}%", e.ProgressPercentage);
		//    prompt.RefreshDisplay();
		//}

		//void wcDownloadSubjectInfos_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		//{
		//    MemoryStream ms = new MemoryStream(e.Result);
		//    SubjectInfo.InitializeSubjectInfos(Encoding.UTF8.GetString(e.Result));
		//    ms.Close();
			
		//    PromptForm prompt = e.UserState as PromptForm;
		//    prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("成功下载{0}个产品标题.", SubjectInfo.SubjectInfos.Count);

		//    StartDownloadPrepareHistory(prompt);
		//}

		void StartDownloadPrepareHistory(PromptForm prompt)
		{
			prompt.AddMessage("正在下载出单记录...0%");
			WebClient wcDownloadPrepareHistory = new WebClient();
			wcDownloadPrepareHistory.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wcDownloadPrepareHistory_DownloadDataCompleted);
			wcDownloadPrepareHistory.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wcDownloadPrepareHistory_DownloadProgressChanged);
			wcDownloadPrepareHistory.DownloadDataAsync(new Uri(Common.URL_PREPARE_HISTORY), prompt);
		}

		void wcDownloadPrepareHistory_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;
			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("正在下载直邮出单记录...{0}%", e.ProgressPercentage);
			prompt.RefreshDisplay();
		}

		void wcDownloadPrepareHistory_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			MemoryStream ms = new MemoryStream(e.Result);
			StreamReader reader = new StreamReader(ms);
			string xml = reader.ReadToEnd();
			//Trace.WriteLine(xml);
			reader.Close();
			ms.Close();

			int c = PrepareHistory.Load(xml);

			PromptForm prompt = e.UserState as PromptForm;
			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("成功下载{0}条直邮出单记录.", c);
			prompt.RefreshDisplay();

			StartDownloadPrepareHistoryNingbo(prompt);
		}

		void StartDownloadPrepareHistoryNingbo(PromptForm prompt)
		{
			prompt.AddMessage("正在下载保税区出单记录...0%");
			WebClient wcDownloadPrepareHistoryNingbo = new WebClient();
			wcDownloadPrepareHistoryNingbo.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wcDownloadPrepareHistoryNingbo_DownloadDataCompleted);
			wcDownloadPrepareHistoryNingbo.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wcDownloadPrepareHistoryNingbo_DownloadProgressChanged);
			wcDownloadPrepareHistoryNingbo.DownloadDataAsync(new Uri(Common.URL_PREPARE_HISTORY_NINGBO), prompt);
		}

		void wcDownloadPrepareHistoryNingbo_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;
			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("正在下载保税区出单记录...{0}%", e.ProgressPercentage);
			prompt.RefreshDisplay();
		}

		void wcDownloadPrepareHistoryNingbo_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			MemoryStream ms = new MemoryStream(e.Result);
			StreamReader reader = new StreamReader(ms);
			string xml = reader.ReadToEnd();
			//Trace.WriteLine(xml);
			reader.Close();
			ms.Close();

			int c = PrepareHistory.LoadNingbo(xml);

			PromptForm prompt = e.UserState as PromptForm;
			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("成功下载{0}条保税区出单记录.", c);
			prompt.RefreshDisplay();

			StartDownloadAddresses(prompt);
		}
		
		void StartDownloadAddresses(PromptForm prompt)
		{
			prompt.AddMessage("正在下载地址库...0%");
			WebClient wcDownloadAddresses = new WebClient();
			wcDownloadAddresses.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wcDownloadAddresses_DownloadDataCompleted);
			wcDownloadAddresses.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wcDownloadAddresses_DownloadProgressChanged);

			wcDownloadAddresses.DownloadDataAsync(new Uri(Common.URL_ADDRESSES), prompt);
		}

		void wcDownloadAddresses_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;
			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("正在下载地址库...{0}%", e.ProgressPercentage);
			prompt.RefreshDisplay();
		}

		void wcDownloadAddresses_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			MemoryStream ms = new MemoryStream(e.Result);
			StreamReader reader = new StreamReader(ms);
			string xml = reader.ReadToEnd();
			//Trace.WriteLine(xml);
			reader.Close();
			ms.Close();

			int c = Addresses.Instance.LoadXml(xml);

			PromptForm prompt = e.UserState as PromptForm;
			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("成功下载{0}条地址记录.", c);
			prompt.RefreshDisplay();

			StartDownloadOrders(prompt);
		}

		void StartDownloadOrders(PromptForm prompt)
		{
			prompt.AddMessage("正在下载订单...0%");
			WebClient wcDownloadOrders = new WebClient();
			wcDownloadOrders.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wcDownloadOrders_DownloadDataCompleted);
			wcDownloadOrders.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wcDownloadOrders_DownloadProgressChanged);

			string dbfilename = string.Empty;
			if (Settings.Instance.OrderDownloadMode == Settings.OrderDownloadModes.Speed)
				dbfilename = ShopProfile.Current.OrdersDbFilename;
			else if (Settings.Instance.OrderDownloadMode == Settings.OrderDownloadModes.Full)
				dbfilename = ShopProfile.Current.FullOrdersDbFilename;
				
			wcDownloadOrders.DownloadDataAsync(new Uri(Common.URL_ROOT_DB + dbfilename), prompt);
		}

		void wcDownloadOrders_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;
			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("正在下载订单...{0}%", e.ProgressPercentage);
			prompt.RefreshDisplay();
		}

		void wcDownloadOrders_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			MemoryStream ms = new MemoryStream(e.Result);
			System.IO.Compression.GZipStream decompress = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress);
			byte[] buf = new byte[1024];
			int len = 0;
			MemoryStream decompressedStream = new MemoryStream();
			while ((len = decompress.Read(buf, 0, buf.Length)) > 0)
				decompressedStream.Write(buf, 0, len);
			string xml = Encoding.UTF8.GetString(decompressedStream.GetBuffer());
			//Trace.WriteLine(xml);
			decompressedStream.Close();
			decompress.Close();
			ms.Close();

			PromptForm prompt = e.UserState as PromptForm;
			LoadOrdersFormXmlStream(xml, prompt);
		}

		void LoadOrdersFormXmlStream(string xml, PromptForm prompt)
		{
			_orders = Order.LoadXmlStream(xml, true);
			
			// Added by KK on 2014/11/30.
			foreach (Order o in _orders)
			{
				if (PrepareHistory.Exists(o.OrderId))
					o.Prepared = true;
				if (PrepareHistory.ExistsNingbo(o.OrderId))
					o.PreparedNingbo = true;
			}
			
			//// temp statistics.
			//int xx = 0, jj = 0, su = 0, ruyi = 0, yoyo = 0;
			//foreach (Order o in _orders)
			//{
			//    if (o.Remark.ToLower().Contains("@xx"))
			//        xx++;
			//    if (o.Remark.ToLower().Contains("@俊俊"))
			//        jj++;
			//    if (o.Remark.ToLower().Contains("@小苏"))
			//        su++;
			//    if (o.Remark.ToLower().Contains("@如意"))
			//        ruyi++;
			//    if (o.Remark.ToLower().Contains("@yoyo") || o.Remark.ToLower().Contains("#yoyo"))
			//        yoyo++;
			//}
			//Trace.Write(string.Format("xx={0}, jj={1}, su={2}, ruyi={3}, yoyo={4}", xx, jj, su, ruyi, yoyo));

			if (null == _orders || _orders.Count <= 0)
			{
				MessageBox.Show(
					this,
					"数据下载(导入)成功, 但是解析失败, 没有解析到任何订单信息.", this.Text,
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("成功下载{0}个订单.", _orders.Count);
			prompt.RefreshDisplay();
			prompt.AddMessage("正在显示订单...");

			_filterBuyer = string.Empty;
			txtKeyword.Text = string.Empty;
			Reload();
			
			// Get last modified date and time.
			XmlDocument xmldoc = new XmlDocument();
			xmldoc.LoadXml(xml);
			string lastModified = "Unknown";//xmldoc.DocumentElement.Attributes.GetNamedItem("last_modified").Value;
			if (null != xmldoc.DocumentElement.Attributes.GetNamedItem("last_modified"))
				lastModified = xmldoc.DocumentElement.Attributes.GetNamedItem("last_modified").Value;

			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("成功显示{0}个订单.\n", _filteredOrders.Count);
			prompt.AddMessage(string.Format("服务器订单数据最后更新时间: {0}", lastModified));
			tsslblLastModifiedDate.Text = string.Format("Last Modified: {0}", lastModified);
			
			DateTime startDate=DateTime.MaxValue, endDate=DateTime.MinValue;
			foreach (Order o in _orders)
			{
				if (o.DealTime < startDate)
					startDate = o.DealTime;
				if (o.DealTime > endDate)
					endDate = o.DealTime;
			}
			prompt.AddMessage(string.Format("已下载订单起止时间(按拍下时间): {0} ～ {1}", startDate.ToString("yyyy-MM-dd HH:mm:ss"), endDate.ToString("yyyy-MM-dd HH:mm:ss")));
			
			prompt.OKEnabled = true;
		}
		
		void odc_OnBuyerClick(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			_filterBuyer = ((OrderDetailsControl)sender).Order.BuyerAccount;
			Reload();
			Cursor.Current = Cursors.Default;
		}
		
		private void btnShowAll_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			txtKeyword.Text = string.Empty;
			_filterBuyer = string.Empty;
			Reload();
			Cursor.Current = Cursors.Default;
		}

		private void tsbtnSearch_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			_filterBuyer = string.Empty;
			Reload();
			Cursor.Current = Cursors.Default;
		}

		private void txtKeyword_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
				tsbtnSearch_Click(tsbtnSearch, EventArgs.Empty);
		}

		private void tsbtnGeneratePackets_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
		
			try
			{
				List<PacketInfo> packetInfos = new List<PacketInfo>();
				foreach (PacketInfo pi in _packets)
					packetInfos.Add(pi.Clone());

				PacketsForm pf = new PacketsForm(packetInfos);
				pf.OnUpdateStatus += new EventHandler(pf_OnUpdateStatus);
				pf.ShowDialog(this);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		void pf_OnUpdateStatus(object sender, EventArgs e)
		{
			UpdateStatusToServer();
		}

		private void UpdateStatusToServer()
		{
			StringBuilder sbOrderIds = new StringBuilder();

			foreach (Order o in _orders)
			{
				if (o.LocalPrepared)
					sbOrderIds.Append(o.OrderId + ",");
			}
				
			if (sbOrderIds.ToString().EndsWith(","))
				sbOrderIds.Remove(sbOrderIds.Length - 1, 1);

			try
			{
				string url = string.Format(Common.URL_DATA_CENTER, "preorders");
				url += string.Format("&shop={0}&orderids={1}&op={2}", ShopProfile.Current.Account, sbOrderIds.ToString(), Settings.Operator);
				HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
				request.Method = "GET";
				request.ContentType = "text/xml";
				WebResponse response = request.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream());
				string result = reader.ReadToEnd();
				reader.Close();
				//Trace.WriteLine(result);

				if (result.StartsWith("Succeeded"))
				{
					foreach (Order o in _orders)
					{
						if (o.LocalPrepared)
							o.Prepare();
					}
				}

				MessageBox.Show(
					this,
					"Result from server: \n" + result,
					this.Text,
					MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void tsbtnAutoFillShipmentNumber_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			
			try
			{
				PacketInfoListForm gllpf = new PacketInfoListForm();
				gllpf.OnSearch += new PacketInfoListForm.OnSearchEventHandler(pmrf_OnSearch);
				gllpf.Show(this);
				return;
			
				//FolderBrowserDialog fbd = new FolderBrowserDialog();
				//fbd.Description = "选择1个出单输出目录. 此目录中应该包含给仓库的发货清单和所有包裹单文件(pdf).";
				////fbd.SelectedPath = Path.GetDirectoryName(Application.ExecutablePath);
				//if (Directory.Exists(@"J:\=egode=\=出单="))
				//    fbd.SelectedPath = @"J:\=egode=\=出单=";
				
				//if (DialogResult.OK == fbd.ShowDialog(this))
				//{
				//    List<PdfPacketInfoEx> pdfPackets = PdfPacketInfo.GetPdfPacketInfos(fbd.SelectedPath, false);
				//    if (null == pdfPackets)
				//    {
				//        MessageBox.Show(this, "No PDF file found in selected folder.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				//        return;
				//    }
					
				//    string[] packingListExcels = Directory.GetFiles(fbd.SelectedPath, "发货清单*.xls");
				//    if (null == packingListExcels || packingListExcels.Length <= 0)
				//    {
				//        MessageBox.Show(this, "No excel files for packing list found in selected folder.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				//        //return;
				//    }

				//    foreach (string packingListExcel in packingListExcels)
				//        UpdateShipmentNumberInPackingList(packingListExcel, pdfPackets);
					
				//    PdfMatchResultForm pmrf = new PdfMatchResultForm(pdfPackets);
				//    pmrf.Text = fbd.SelectedPath;
				//    pmrf.OnSearch += new PdfMatchResultForm.OnSearchEventHandler(pmrf_OnSearch);
				//    pmrf.Show(this);
				//}
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		void pmrf_OnSearch(string keyword)
		{
			txtKeyword.Text = keyword;
			Application.DoEvents();
			tsbtnSearch_Click(tsbtnSearch, EventArgs.Empty);
		}
		
		private void UpdateShipmentNumberInPackingList(string packingListExcel, List<PdfPacketInfoEx> pdfPackets)
		{
			Cursor.Current = Cursors.WaitCursor;
		
			Excel excel = null;
			
			try
			{
				excel = new Excel(packingListExcel, true, Excel.OledbVersions.OLEDB40);
			}
			catch 
			{
				MessageBox.Show(
					this, 
					"Open Excel file of packing list failed.\nMake sure the Excel file was not opened and try again.", this.Text,
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			
			try
			{
				DataSet ds = excel.Get("Sheet1", string.Empty);
				if (null == ds)
					return;

				for (int i = 1; i <= ds.Tables[0].Rows.Count; i++)
					excel.Insert("Sheet1", string.Format("G{0}:G{0}", i), string.Format("'x{0}'", Guid.NewGuid().ToString()));
				excel.Close();
				System.Threading.Thread.Sleep(500);
				Application.DoEvents();
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
			}
			
			try
			{
				excel = new Excel(packingListExcel, true, Excel.OledbVersions.OLEDB40);
			
				DataSet ds = excel.Get("Sheet1", string.Empty);
				if (null == ds)
					return;
				
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					DataRow dr = ds.Tables[0].Rows[i];
					//Trace.WriteLine(dr.ItemArray[1].ToString());
					string recipientNameCn = dr.ItemArray[1].ToString();
					if (string.IsNullOrEmpty(recipientNameCn))
						continue;
					PdfPacketInfoEx ppi = PdfPacketInfoEx.GetItemByRecipientName(recipientNameCn, pdfPackets, true);
					if (null == ppi)
					{
						PdfPacketInfoEx ppi1 = new PdfPacketInfoEx(string.Empty, PacketTypes.Unknown, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty);
						ppi1.MatchedRecipientName = recipientNameCn;
						pdfPackets.Add(ppi1);
						continue;
					}

					ppi.MatchedRecipientName = recipientNameCn;

					try
					{
						excel.Update(
							"Sheet1", 
							"运单号", string.Format("{0}:{1}", ppi.RecipientName, ppi.ShipmentNumber),
							//"序号", dr.ItemArray[0].ToString());
							//"收货人", recipientNameCn);
							//"序号", dr.ItemArray[0].ToString());
							"reserved", dr.ItemArray[6].ToString());
						ppi.Updated = true;
						//Trace.WriteLine(dr.ItemArray[0].ToString());
						//if (dr.ItemArray[0].ToString().Equals("28"))
						//    Trace.WriteLine("");
					}
					catch (Exception ex)
					{
						Trace.WriteLine(ex);
					}
					//Trace.WriteLine(string.Format("Matched: {0}, {1}, {2}, {3}", recipientNameCn, ppi.RecipientName, ppi.ShipmentNumber, ppi.Weight));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, "Error occured during udpate shipment number into excel file.\n"+ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			finally
			{
				excel.Close();
				Cursor.Current = Cursors.Default;
			}	
		}

		private void tsbtnImportOrders_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
		
			try
			{
				if (null != _orders)
				{
					DialogResult dr = MessageBox.Show(
						this,
						"重新导入订单将会覆盖未同步到服务器的数据(例如出单状态).\n是否继续导入?",
						this.Text,
						MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					if (DialogResult.No == dr)
						return;
				}

				OpenFileDialog ofd = new OpenFileDialog();
				ofd.Filter = "XML Files (*.xml)|*.xml";
				ofd.Multiselect = false;
				ofd.ShowReadOnly = false;
				
				if (DialogResult.OK == ofd.ShowDialog(this))
				{
					PromptForm dof = new PromptForm();
					dof.Owner = this;
					dof.AddMessage("正在解析订单...");
					dof.Show();
					Application.DoEvents();

					XmlDocument doc = new XmlDocument();
					doc.Load(ofd.FileName);
					LoadOrdersFormXmlStream(doc.OuterXml, dof);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void tsbtnAutoCommentBuyer_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;

			try
			{
				if (null != _orders)
				{
					DialogResult dr = MessageBox.Show(
						this,
						"程序无法自动识别需要评价订单.\n需要手动导入待评价订单.",
						this.Text,
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				
				new RateForm(null).ShowDialog(this);

				//OpenFileDialog ofd = new OpenFileDialog();
				//ofd.Filter = "XML Files (*.xml)|*.xml";
				//ofd.Multiselect = false;
				//ofd.ShowReadOnly = false;

				//if (DialogResult.OK == ofd.ShowDialog(this))
				//{
				//    DownloadingOrdersForm dof = new DownloadingOrdersForm();
				//    dof.Owner = this;
				//    dof.Message = "正在解析订单...";
				//    dof.Show();
				//    Application.DoEvents();

				//    List<Order> orders = Order.LoadXmlFile(ofd.FileName, true);
				//    RateForm cbf = new RateForm(orders);
				//    cbf.ShowDialog(this);
				//}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}
		
		public Point NbStatusLocation
		{
			get { return this.PointToScreen(new Point(tsslblInfo.Width + tsslblNingboOrders.Margin.Left, statusMain.Top)); }
		}

		private void tsslblInfo_DoubleClick(object sender, EventArgs e)
		{
			if (tsbtnGeneratePackets.Enabled)
				tsbtnGeneratePackets_Click(tsbtnGeneratePackets, EventArgs.Empty);
		}

		private void tsslblNingboOrders_Click(object sender, EventArgs e)
		{
			if (tsbtnNingboOrders.Enabled)
				tsbtnNingboOrders_Click(tsbtnNingboOrders, EventArgs.Empty);
		}

		private void tsbtnNingboOrders_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			NingboOrdersForm nof = new NingboOrdersForm();
			nof.OnSearch += new NingboOrdersForm.OnSearchEventHandler(nof_OnSearch);
			nof.OnSearchMobile += new NingboOrdersForm.OnSearchEventHandler(nof_OnSearchMobile);
			nof.OnNingboOrderRemoved += new EventHandler(nof_OnNingboOrderRemoved);
			nof.Show(this);
			Cursor.Current = Cursors.Default;
		}

		void nof_OnSearchMobile(string mobile)
		{
			_filteredOrders = new List<Order>();
			if (null == _orders || _orders.Count <= 0)
				return;
		
			foreach (Order o in _orders)
			{
				if (o.MobileNumber.Equals(mobile))
				{
					_filteredOrders.Add(o);
					break;
				}
			}

			SetCurrentPage(0);
			lblPrevPage.Enabled = false;
			lblNextPage.Enabled = false;
			RefreshOrderLists();
		}

		void nof_OnNingboOrderRemoved(object sender, EventArgs e)
		{
			tsslblNingboOrders.Text = string.Format("{0} Ningbo order{1}", NingboOrder.Orders.Count, NingboOrder.Orders.Count > 1 ? "s" : string.Empty);
			tsbtnNingboOrders.Enabled = NingboOrder.Orders.Count > 0;
		}

		void nof_OnSearch(string taobaoOrderId)
		{
			_filteredOrders = new List<Order>();
			if (null == _orders || _orders.Count <= 0)
				return;
		
			foreach (Order o in _orders)
			{
				if (o.OrderId.Equals(taobaoOrderId))
				{
					_filteredOrders.Add(o);
					break;
				}
			}

			SetCurrentPage(0);
			lblPrevPage.Enabled = false;
			lblNextPage.Enabled = false;
			RefreshOrderLists();
		}

		//private void tsbtnAnalyseOrders_Click(object sender, EventArgs e)
		//{
		//    List<string> mobileNumbers = new List<string>();
		
		//    foreach (Order o in _orders)
		//    {
		//        if (o.DealTime < new DateTime(2015, 1, 1, 0, 0, 0))
		//            continue;
		//        //if (o.DealTime > new DateTime(2015, 12, 31, 0, 0, 0))
		//        //    continue;
		//        //Trace.WriteLine(o.ShipmentCompany); // 物流公司: 顺丰速运, 圆通速递, POSTNL, DHL, 邮政快递包裹, EMS
		//        if (!o.ShipmentCompany.Contains("EMS") && !o.ShipmentCompany.Contains("顺丰") && !o.ShipmentCompany.Contains("圆通"))
		//            continue;
		//        //if (!o.ShipmentCompany.Contains("顺丰") && !o.ShipmentCompany.Contains("圆通"))
		//        //    continue;
				
		//        List<Order> os = new List<Order>();
		//        os.Add(o);

		//        int i1, i2;
		//        List<SoldProductInfo> productInfos = ConsignShForm.GetProducts(os, out i1, out i2, true);
		//        if (null == productInfos || productInfos.Count <= 0)
		//            continue;
					
		//        bool bingo=false;
		//        foreach (SoldProductInfo spi in productInfos)
		//        {	
		//            //if (spi.Id.Equals("002-0010"))
		//            if (spi.Id.StartsWith("001-"))
		//            {
		//                bingo = true;
		//                break;
		//            }
		//        }
		//        if (!bingo)
		//            continue;
				
		//        if (string.IsNullOrEmpty(o.MobileNumber))
		//            continue;
		//        if (o.MobileNumber.Equals("18061886808")) //ruyi
		//            continue;
		//        if (o.MobileNumber.Equals("18311076895")) //yangzhao
		//            continue;
		//        if (o.MobileNumber.Equals("18017972823")) //tangweiwei
		//            continue;
		//        if (o.MobileNumber.Equals("18019080391")) //xiabin
		//            continue;
		//        if (o.MobileNumber.Equals("18116408021")) //
		//            continue;
		//        if (o.MobileNumber.Equals("13739247056")) //qiangwei
		//            continue;
		//        if (o.MobileNumber.Equals("13818282057")) //qing
		//            continue;

		//        Trace.WriteLine(string.Format("{0}:{1},{2}", o.RecipientName, o.MobileNumber, o.ShipmentCompany));
		//        if (mobileNumbers.Contains(o.MobileNumber))
		//            continue;
		//        mobileNumbers.Add(o.MobileNumber);
		//    }
			
		//    foreach (string mobileNumber in mobileNumbers)
		//        Trace.WriteLine(mobileNumber);
		//    Trace.WriteLine("Total: "+mobileNumbers.Count.ToString());
		//}
		
		private void tsbtnAnalyseOrders_Click(object sender, EventArgs e)
		{
			// analyse 1
			//int[] hours = new int[24];
			
			//foreach (Order o in _orders)
			//{
			//    if (o.DealTime < new DateTime(2017, 1, 1))
			//        continue;
			//    if (!o.Items.Contains("米粉"))
			//        continue;
				
			//    for (int i = 1; i <= 24; i++)
			//    {
			//        if (o.DealTime.Hour < i && o.DealTime.Hour >= i - 1)
			//            hours[i-1]++;
			//    }
			//}
			
			//for (int i = 0; i < hours.Length; i++)
			//    Trace.WriteLine(string.Format("{0:00}:00-{1:00}:00: {2}", i, i+1, hours[i]));
			
//			// analyse 2
//			string[] provinces = new string[]{"北京","上海","江苏","浙江","广东","山东","河南","河北","陕西","湖南","重庆","福建","天津","云南","四川","广西","安徽","海南","江西","湖北","山西","辽宁","黑龙江","内蒙古","贵州","甘肃","青海","新疆","西藏","吉林","宁夏"};
//			List<CountItem> provinceCounts = new List<CountItem>();
//			foreach (string province in provinces)
//				provinceCounts.Add(new CountItem(province));
//			foreach (Order o in _orders)
//			{
//			    if (o.DealTime < new DateTime(2017, 1, 1))
//			        continue;
//			       
//			    if (!o.Items.Contains("profissimo"))
//					continue;
//
//			    foreach (CountItem ci in provinceCounts)
//			    {
//					if (o.RecipientAddress.Trim().StartsWith(ci.Name))
//					{
//						ci.Add();
//						break;
//					}
//			    }
//			}
//			
//			foreach (CountItem ci in provinceCounts)
//			{
//				Trace.WriteLine(string.Format("{0}: {1}", ci.Name, ci.Count));
//			}
			
			// analyse 3.
			// output temparary order informations for xx to print yunda bills.
			foreach (Order o in _orders)
			{
				if (o.Status != OrderLib.Order.OrderStatus.Paid)
					continue;
				
				StringBuilder sb = new StringBuilder();
				sb.Append(string.Format(
					"买家账号: <b>{0}</b>, 下单时间: {1}, 付款时间: {2}<br>", 
					o.BuyerAccount, o.DealTime.ToString("yyyy/MM/dd HH:mm:ss"), o.PayingTime.ToString("yyyy/MM/dd HH:mm:ss")));

				if (!string.IsNullOrEmpty(o.Remark))
				{
					sb.Append("备注: ");
					if (o.Remark.Contains("#魔力店长#"))
						sb.Append(string.Format("<font color=#808000>{0}</font>", o.Remark));
					else
						sb.Append(o.Remark);
					sb.Append("<br>");
				}
				
				// phone.
				string phone = string.Empty;
				if (!string.IsNullOrEmpty(o.MobileNumber))
					phone += o.MobileNumber + ", ";
				if (!string.IsNullOrEmpty(o.PhoneNumber))
					phone += o.PhoneNumber;
				sb.Append(string.Format("电话号码: <b>{0}</b><br>", phone));
				
				sb.Append(string.Format("收货地址: <b>{0}</b><br>", o.RecipientAddress));
				
				if (!string.IsNullOrEmpty(o.EditedRecipientAddress))
					sb.Append(string.Format("新地址: <b>{0}</b><br>", o.EditedRecipientAddress));
				
				// items.
				string[] items = o.Items.Split('★');
				for (int i = 0; i < items.Length; i++)
				{
					string item = items[i];
					string[] infos = item.Split('☆');
					if (infos.Length < 3)
						continue;

					if (string.IsNullOrEmpty(infos[0]))
						Trace.WriteLine("null product found!!!");
					
					sb.Append(string.Format("<font color=#ff0000><b>{0}</b></font>, {1}<br>", infos[2], infos[0]));
				}
				
				sb.Append("<br>");
				Trace.WriteLine(sb.ToString());
			}
		}

		// Added by KK on 2017/10/14.
		private void tsbtnYundaOrders_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			
			new Yunda.YdOrderListForm(ConsignShForm.YundaOrders).ShowDialog(this);
			
			Cursor.Current = Cursors.Default;
		}

		// Added by KK on 2017/10/14.
		private void tsbtnImportYundaOrders_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			
			try
			{
				OpenFileDialog ofd = new OpenFileDialog();
				ofd.Filter = "Excel Files (*.xls)|*.xls|All Files (*.*)|*.*";
				ofd.FileName = "客户订单管理.xls";
				if (DialogResult.OK == ofd.ShowDialog(this))
				{
					List<Yunda.YdExportedOrder> ydExportedOrders = Yunda.YdExportedOrder.LoadCsv(ofd.FileName);
					if (null == ydExportedOrders)
					{
						MessageBox.Show(this, "导入韵达订单失败.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					
					//UpdateFilteredOrders();
					_filteredOrders = new List<Order>();
					foreach (Yunda.YdExportedOrder ydo in ydExportedOrders)
					{
						// ydo中保存的订单编号格式: 111111*****111111xyz
						// 其中订单编号中间n位用星号代替, 最后3位是随机数, 并不包含在真实的订单编号中.
						string[] halfOrderIds = ydo.OrderId.Split("*****".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

						foreach (Order o in _orders)
						{
							if (!o.OrderId.StartsWith(halfOrderIds[0]))
								continue;
							if (!o.OrderId.EndsWith(halfOrderIds[1].Substring(0, 6)))
								continue;
							_filteredOrders.Add(o);
							o.PrepareYunda(ydo.TrackingNumber);
							break;
						}
					}
					
					SetCurrentPage(0);
					lblPrevPage.Enabled = false;
					lblNextPage.Enabled = false;
					RefreshOrderLists();

					DialogResult dr = MessageBox.Show(
						this, 
						string.Format("导入{0}个订单.\n点击Yes开始自动点发货+出库, 点击No返回等待手动处理.", ydExportedOrders.Count), this.Text, 
						MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
					if (DialogResult.Yes == dr)
					{
						foreach (OrderDetailsControl odc in pnlOrders.Controls)
						{
							pnlOrders.ScrollControlIntoView(odc);
							ConsignSh(odc, true);
						}
					}
				}
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void tsbtnConfig_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			new ConfigurationForm().ShowDialog(this);
			Cursor.Current = Cursors.Default;
		}
		
		void MiRunZtoClick(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			
			int i = 0;
			foreach (Order o in _orders)
			{
				if (o.AutoTransmissionProcessor == ShipmentCompanies.Zto)
					i++;
			}
			MessageBox.Show(this, i.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			
			Cursor.Current = Cursors.Default;
		}
		
		void MiRunYundaClick(object sender, EventArgs e)
		{
	
		}
	}
}