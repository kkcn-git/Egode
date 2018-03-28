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
using OrderParser;

namespace Egode
{
	public partial class MainForm : Form
	{
		//public const string URL_ORDERS = "http://www.sozi.com.cn/kktb/db/orders.xml";
		//public const string URL_PRODUCTS = "http://www.sozi.com.cn/kktb/db/products.xml";
		//public const string URL_DATA_CENTER = "http://www.sozi.com.cn/kktb/datacenter.aspx?cmd={0}";
		public const string URL_ORDERS = "http://localhost:14204/CloudEgode/db/orders.xml";
		public const string URL_PRODUCTS = "http://localhost:14204/CloudEgode/db/products.xml";
		public const string URL_DATA_CENTER = "http://localhost:14204/CloudEgode/datacenter.aspx?cmd={0}";
		private const int NUMBER_PER_PAGE = 50;
		
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
				return "Both";
			}
		}
		#endregion
		
		private bool _localDb;
		
		private LinkLabel lblPrevPage;
		private LinkLabel lblNextPage;
		private Label lblPageInfo;
		private ComboBox cboDurationFilters;
		private ComboBox cboShippingOriginFilters;

		private string _downloadedOrderXml; // 每次刷新(搜索、过滤)都必须重新加载下面的_orders, 否则程序的user objects会1直增加直至程序崩溃. i dont know why!!!
		private List<Order> _orders;
		private List<Order> _filteredOrders;
		private List<PacketInfo> _packets;
		private int _currentPage;

		private string _filterBuyer;
	
		public MainForm(bool localDb)
		{
			_localDb = localDb;
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			this.DoubleBuffered = true;
		
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
			cboShippingOriginFilters.Items.Add(new ShippingOriginFilter(ShippingOrigins.Deutschland | ShippingOrigins.Shanghai));
			cboShippingOriginFilters.SelectedIndex = Settings.Instance.ShippingOriginFilterIndex;
			cboShippingOriginFilters.SelectedIndexChanged += new EventHandler(cboShippingOriginFilters_SelectedIndexChanged);
			tsMain.Items.Add(new ToolStripControlHost(cboShippingOriginFilters));

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
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			if (!_localDb)
				tsbtnDownloadData_Click(tsbtnDownloadData, EventArgs.Empty);
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
		
		// 从Page 1开始, 用户账号筛选保留, 关键字保留.
		void Reload()
		{
			// 如果不每次都重新生成_orders集合, 会导致刷新时object handles无法释放, 1直增加直到程序崩溃.
			// i dont know why!!!
			
			// 备份LocalPrepared状态.
			List<Order> orders = Order.LoadXmlStream(_downloadedOrderXml, true);
			if (null != _orders)
			{
				for (int i = 0; i < orders.Count; i++)
					orders[i].LocalPrepared = _orders[i].LocalPrepared;
			}
			_orders = orders;

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
				if (MatchFilter(o))
					_filteredOrders.Add(o);
			}
		}
		
		private bool MatchFilter(Order order)
		{
			if (null == order)
				return false;

			if (order.Status == Order.OrderStatus.Deal && !Settings.Instance.ShowDeal)
				return false;
			if (order.Status == Order.OrderStatus.Paid)
			{
				if (order.Prepared || order.LocalPrepared)
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
			
			// 直邮 or 现货.
			ShippingOrigins so = ((ShippingOriginFilter)cboShippingOriginFilters.SelectedItem).ShippingOrigin;
			if ((order.Remark.Contains("#直邮") && so == ShippingOrigins.Deutschland) || (order.Remark.Contains("#现货") && so == ShippingOrigins.Shanghai))
			{
			}
			else
			{
				if (order.Remark.Contains("#直邮") && so == ShippingOrigins.Shanghai)
					return false;
				if (order.Remark.Contains("#现货") && so == ShippingOrigins.Deutschland)
					return false;
			}
			
			// buyer account.
			if (!string.IsNullOrEmpty(_filterBuyer) && !order.BuyerAccount.Equals(_filterBuyer))
				return false;
			
			// keyword of searching.
			if (!string.IsNullOrEmpty(txtKeyword.Text))
			{
				string[] keywords = txtKeyword.Text.Split(' ');
				if (!order.MatchKeyword(keywords))
					return false;
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

					OrderDetailsControl odc = new OrderDetailsControl(o, Order.GetOrdersByBuyerAccount(o.BuyerAccount, _filteredOrders).Count);
					odc.OnBuyerClick += new EventHandler(odc_OnBuyerClick);
					odc.OnAddPacket += new OrderDetailsControl.PacketEventHandler(odc_OnAddPacket);
					odc.OnConsignSh += new EventHandler(odc_OnConsignSh);
					pnlOrders.Controls.Add(odc);
					odc.Width = pnlOrders.Width - 26;
				}

				pnlOrders.ResumeLayout();

				lblPrevPage.Enabled = (_currentPage >= 1);
				lblNextPage.Enabled = ((_currentPage+1) * NUMBER_PER_PAGE) < _filteredOrders.Count;
				tsslblOrders.Text = string.Format("{0} orders filtered.", _filteredOrders.Count);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
			}
		}
		
		// 返回指定账号是否有多个订单. 仅检测deal, deal, sent3种状态订单.
		bool IsMultipleOrders(string buyerAccount, List<Order> orders, out List<Order> buyerOrders, out string prompt)
		{
			buyerOrders = new List<Order>();
			prompt = string.Empty;

			if (string.IsNullOrEmpty(buyerAccount))
				return false;
			if (null == orders || orders.Count <= 0)
				return false;

			int deal = 0, paid = 0, sent = 0;
			foreach (Order o in orders)
			{
				if (!buyerAccount.Equals(o.BuyerAccount))
					continue;
				if (o.Status != Order.OrderStatus.Deal && o.Status != Order.OrderStatus.Paid && o.Status != Order.OrderStatus.Sent)
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
				}
			}

			if (deal > 0)
				prompt += string.Format("未付款: {0}, ", deal.ToString());
			if (paid > 0)
				prompt += string.Format("已付款: {0}, ", paid.ToString());
			if (sent > 0)
				prompt += string.Format("已发货: {0}, ", sent.ToString());
			
			if (prompt.EndsWith(", "))
				prompt = prompt.Substring(0, prompt.Length - 2);

			return (deal + paid + sent > 1);
		}

		void odc_OnConsignSh(object sender, EventArgs e)
		{
			OrderDetailsControl odc = sender as OrderDetailsControl;
			
			string orderDetailsHtml = GetOrderDetailsHtml(odc.Order.OrderId);
			if (orderDetailsHtml.ToLower().Contains("退款信息"))
			{
				MessageBox.Show(this, "此订单已申请退款. 终止发货!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			
			if (!orderDetailsHtml.Contains("买家已付款，等待卖家发货"))
			{
				DialogResult dr = MessageBox.Show(this, "此订单不是<买家已付款, 等待卖家发货>状态, 是否继续发货操作?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (DialogResult.No == dr)
					return;
			}

			string warning;
			List<Order> buyerOrders;
			if (IsMultipleOrders(odc.Order.BuyerAccount, _orders, out buyerOrders, out warning))
			{
				OrdersForm of = new OrdersForm(buyerOrders);
				of.Prompt = string.Format("注意! 此账号有多个订单. {0}. 是否继续发货", warning);
				DialogResult dr = of.ShowDialog(this);
				if (DialogResult.Cancel == dr)
					return;
			}

			// Have not get full edited address yet
			if (!string.IsNullOrEmpty(odc.Order.EditedRecipientAddress.Trim()))
			{
				MessageBox.Show(
					this.FindForm(),
					"收货地址发生修改.\n导出数据无法获得完整修改后的地址, 需要访问订单详情页面获得完整地址.",
					this.Text,
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				odc.RefreshFullEditedAddress();
			}

			ConsignShForm csf = new ConsignShForm(odc.Order);
			csf.ShowDialog(this);
		}

		void odc_OnAddPacket(object sender, PacketInfo p)
		{
			try
			{
				string orderDetailsHtml = GetOrderDetailsHtml(p.OrderId);
				if (orderDetailsHtml.ToLower().Contains("退款信息"))
				{
					MessageBox.Show(this, "此订单已申请退款. 终止发货!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				
				if (!orderDetailsHtml.Contains("买家已付款，等待卖家发货"))
				{
					DialogResult dr = MessageBox.Show(this, "此订单不是<买家已付款, 等待卖家发货>状态, 是否继续发货操作?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					if (DialogResult.No == dr)
						return;
				}

				string warning;
				List<Order> buyerOrders;
				if (IsMultipleOrders(Order.GetOrderByOrderId(p.OrderId, _orders).BuyerAccount, _orders, out buyerOrders, out warning))
				{
					OrdersForm of = new OrdersForm(buyerOrders);
					of.Prompt = string.Format("注意! 此账号有多个订单. {0}. 是否继续出单?", warning);
					DialogResult dr = of.ShowDialog(this);
					if (DialogResult.Cancel == dr)
						return;
				}

				//if (IsMultipleOrders(Order.GetOrderByOrderId(p.OrderId, _orders).BuyerAccount, _orders, out warning))
				//{
				//    DialogResult dr = MessageBox.Show(
				//        this, 
				//        string.Format("注意!!! 此账号有多个订单:\n{0}\n\n是否继续出单?", warning), this.Text,
				//        MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				//    if (DialogResult.No == dr)
				//        return;
				//}

				if (null == _packets)
					_packets = new List<PacketInfo>();

				_packets.Add(p);
				tsbtnGeneratePackets.Enabled = (_packets.Count > 0);
				tsslblInfo.Text = string.Format("{0} packets added.", _packets.Count);
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		
		private string GetOrderDetailsHtml(string orderId)
		{
			if (string.IsNullOrEmpty(orderId))
				return string.Empty;

			WebBrowserForms.OrderDetailsPageWebBrowserForm odpbf = new Egode.WebBrowserForms.OrderDetailsPageWebBrowserForm(orderId);
			odpbf.ShowDialog(this);
			return odpbf.Html;
		}
		
		private bool CheckRefund(string orderId)
		{
			if (string.IsNullOrEmpty(orderId))
				return false;
			
			WebBrowserForms.OrderDetailsPageWebBrowserForm odpbf = new Egode.WebBrowserForms.OrderDetailsPageWebBrowserForm(orderId);
			odpbf.ShowDialog(this);
			if (odpbf.Html.ToLower().Contains("退款信息"))
				return true;
			return false;
		}

		private void tsbtnDownloadData_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
		
			try
			{
				TimeSpan ts = DateTime.Now - new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 0, 0);
				if (Math.Abs(ts.TotalSeconds) <= 600)
				{
					MessageBox.Show(
						this, 
						string.Format("啊哦...现在时间: {0}", DateTime.Now.ToString("HH:mm:ss")),
						this.Text,
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

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
			wc.DownloadDataAsync(new Uri(URL_PRODUCTS), prompt);
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
			XmlDocument xmldoc = new XmlDocument();
			xmldoc.LoadXml(xml);
			XmlNodeList nlProducts = xmldoc.SelectNodes(".//product");
			if (null == nlProducts || nlProducts.Count <= 0)
				return;

			foreach (XmlNode nodeProduct in nlProducts)
			{
				string id = nodeProduct.Attributes.GetNamedItem("id").Value;
				string brand = nodeProduct.Attributes.GetNamedItem("brand").Value;
				string name = nodeProduct.Attributes.GetNamedItem("name").Value;
				string shortname = nodeProduct.Attributes.GetNamedItem("shortname").Value;
				string keywords = nodeProduct.Attributes.GetNamedItem("keywords").Value;
				ProductInfo.ProductInfos.Add(new ProductInfo(id, brand, name, shortname, keywords));
			}

			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("成功下载{0}个产品信息.", ProductInfo.ProductInfos.Count);
			prompt.RefreshDisplay();
			
			StartDownloadOrders(prompt);
		}

		void StartDownloadOrders(PromptForm prompt)
		{
			prompt.AddMessage("正在下载订单...0%");
			WebClient wcDownloadOrders = new WebClient();
			wcDownloadOrders.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wcDownloadOrders_DownloadDataCompleted);
			wcDownloadOrders.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wcDownloadOrders_DownloadProgressChanged);
			wcDownloadOrders.DownloadDataAsync(new Uri(URL_ORDERS), prompt);

			//HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(string.Format(URL_DATA_CENTER, "getallorders"));
			//request.Method = "GET";
			//request.ContentType = "text/xml";
			//WebResponse response = request.GetResponse();
			//StreamReader reader = new StreamReader(response.GetResponseStream());
			////Trace.WriteLine(reader.ReadToEnd());
			////Trace.WriteLine("");
			//string xml = reader.ReadToEnd();
			//reader.Close();
		}

		void wcDownloadOrders_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;
			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("正在下载订单...{0}%", e.ProgressPercentage);
			prompt.RefreshDisplay();
		}

		void wcDownloadOrders_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			PromptForm prompt = e.UserState as PromptForm;
			string xml = Encoding.UTF8.GetString(e.Result);
			LoadOrdersFormXmlStream(xml, prompt);
		}

		void LoadOrdersFormXmlStream(string xml, PromptForm prompt)
		{
			_downloadedOrderXml = xml;
			List<Order> orders = Order.LoadXmlStream(_downloadedOrderXml, true);
			
			//// temp statistics.
			//int xx = 0, jj = 0, su = 0, ruyi = 0, yoyo = 0;
			//foreach (Order o in orders)
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

			if (null == orders || orders.Count <= 0)
			{
				MessageBox.Show(
					this,
					"数据下载(导入)成功, 但是解析失败, 没有解析到任何订单信息.", this.Text,
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("成功下载(导入){0}个订单.", orders.Count);
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
				string url = string.Format(URL_DATA_CENTER, "preorders");
				url += string.Format("&orderids={0}", sbOrderIds.ToString());
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
				FolderBrowserDialog fbd = new FolderBrowserDialog();
				fbd.Description = "选择1个出单输出目录. 此目录中应该包含给仓库的发货清单和所有包裹单文件(pdf).";
				fbd.SelectedPath = Path.GetDirectoryName(Application.ExecutablePath);
				
				if (DialogResult.OK == fbd.ShowDialog(this))
				{
					List<PdfPacketInfoEx> pdfPackets = GetPdfPacketInfos(fbd.SelectedPath);
					if (null == pdfPackets)
					{
						MessageBox.Show(this, "No PDF file found in selected folder.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					
					string[] packingListExcels = Directory.GetFiles(fbd.SelectedPath, "发货清单*.xls");
					if (null == packingListExcels || packingListExcels.Length <= 0)
					{
						MessageBox.Show(this, "No excel files for packing list found in selected folder.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						//return;
					}

					foreach (string packingListExcel in packingListExcels)
						UpdateShipmentNumberInPackingList(packingListExcel, pdfPackets);
					
					PdfMatchResultForm pmrf = new PdfMatchResultForm(pdfPackets);
					pmrf.OnSearch += new PdfMatchResultForm.OnSearchEventHandler(pmrf_OnSearch);
					pmrf.Show(this);
				}
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
		
		private List<PdfPacketInfoEx> GetPdfPacketInfos(string folder)
		{
			string[] pdfFiles = Directory.GetFiles(folder, "*.pdf");
			if (null == pdfFiles || pdfFiles.Length <= 0)
				return null;
			
			List<PdfPacketInfoEx> pdfPackets = new List<PdfPacketInfoEx>();
			foreach (string pdfFile in pdfFiles)
			{
				PdfPacketInfoEx ppi = PdfPacketInfoEx.GetPdfPacketInfoEx(pdfFile);
				if (null == ppi)
					continue;
				pdfPackets.Add(ppi);
				Trace.WriteLine(string.Format("{0},{1},{2},{3}", ppi.Type, ppi.RecipientName, ppi.ShipmentNumber, ppi.Weight));
			}

			return pdfPackets;
		}
		
		private void UpdateShipmentNumberInPackingList(string packingListExcel, List<PdfPacketInfoEx> pdfPackets)
		{
			Cursor.Current = Cursors.WaitCursor;
		
			Excel excel = null;
			
			try
			{
				excel = new Excel(packingListExcel, true);
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
				
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					DataRow dr = ds.Tables[0].Rows[i];
					//Trace.WriteLine(dr.ItemArray[1].ToString());
					string recipientNameCn = dr.ItemArray[1].ToString();
					if (string.IsNullOrEmpty(recipientNameCn))
						continue;
					if (recipientNameCn.Equals("张秋兰"))
						Trace.Write("");
					string recipientNamePinyin = HanZiToPinYin.Convert(recipientNameCn);
					PdfPacketInfoEx ppi = PdfPacketInfoEx.GetItem(recipientNamePinyin, pdfPackets, true);
					if (null == ppi)
					{
						PdfPacketInfoEx ppi1 = new PdfPacketInfoEx(string.Empty, PacketTypes.Unknown, string.Empty, string.Empty, 0);
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
							"序号", dr.ItemArray[0].ToString());
							//"收货人", recipientNameCn);
						ppi.Updated = true;
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
	}
}