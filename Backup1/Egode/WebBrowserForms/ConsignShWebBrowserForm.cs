using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode.WebBrowserForms
{
	public partial class ConsignShWebBrowserForm : AutoSigninWebBrowserForm
	{
		private static ConsignShWebBrowserForm _instance;
		private string _orderId;
		private string _billNumber;
		private OrderLib.ShipmentCompanies _expressCompany;
		private bool _autoSubmit = true;

		private Timer _tmrAutoSubmit; // ugly code -_-

		// 是否填入过运单号.
		// 用于判断当显示发货成功页面时, 是订单本来就是已发货状态, 还是本次点击发货进入已发货状态页面.
		private bool _filledShipmentNumber = false;

		public ConsignShWebBrowserForm() : base(string.Empty)
		{
			InitializeComponent();
		}
		
		public static ConsignShWebBrowserForm Instance
		{
			get
			{
				if (null == _instance)
					_instance = new ConsignShWebBrowserForm();
				return _instance;
			}
		}
		
		public string OrderId
		{
			get { return _orderId; }
			set { _orderId = value; }
		}
		
		public string BillNumber
		{
			get { return _billNumber; }
			set { _billNumber = value; }
		}
		
		public OrderLib.ShipmentCompanies ShipmentCompany
		{
			get { return _expressCompany; }
			set { _expressCompany = value; }
		}
		
		public bool AutoSubmit
		{
			get { return _autoSubmit; }
			set { _autoSubmit = value; }
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			
			txtBillNumber.Text = _billNumber;
			switch (_expressCompany)
			{
				case OrderLib.ShipmentCompanies.Zto:
					//临时注释. 2018年春节后, 韵达系统没搞定, 暂时使用大面单打印, 使用zto流程.
					//lblExpressCompany.Text = "中通";
					//break;
				case OrderLib.ShipmentCompanies.Sf:
					lblExpressCompany.Text = "顺丰";
					break;
				case OrderLib.ShipmentCompanies.Best:
					lblExpressCompany.Text = "百世";
					break;
				case OrderLib.ShipmentCompanies.Yunda:
					lblExpressCompany.Text = "韵达";
					break;
			}
			
			if (!string.IsNullOrEmpty(_orderId))
			{
				//_waitOnce = false;
				//_clickedSubtmit = false;
				_javascriptfalse = 0;
				//this.DialogResult = DialogResult.Retry;
				this.Navigate(string.Format("http://wuliu.taobao.com/user/consign.htm?trade_id={0}", _orderId));
			}

			btnCopy_Click(btnCopy, EventArgs.Empty);
			btnSendManually.Focus();
		}

		void b_Click(object sender, EventArgs e)
		{
			Go();
		}
		
		void Go()
		{
			if (wb.Document.Body.OuterHtml.Contains("自己联系物流"))
			{
				System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"请检查您的电话号码，保证它是正确的");
				System.Text.RegularExpressions.Match m = r.Match(wb.Document.Body.OuterHtml.ToLower().Replace("//alert(\"手机号码必须由8到16位数字构成\");", string.Empty));
				if (!m.Success)
				{
					r = new System.Text.RegularExpressions.Regex(@"必须由\d*到\d*位数字构成");
					m = r.Match(wb.Document.Body.OuterHtml.ToLower().Replace("//alert(\"手机号码必须由8到16位数字构成\");", string.Empty));
				}

				if (m.Success)
				{
					if (_suspendMsg)
						return;
					_suspendMsg = true;
					MessageBox.Show(this, "电话或地址格式错误, 需要手动点发货.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				if (wb.Document.Body.OuterHtml.Contains("您输入的运单号有误，请核对后重新输入"))
				{
					MessageBox.Show(this, "运单号有误, 需手动处理.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				
				System.Diagnostics.Trace.WriteLine("javascriptfalse: " + _javascriptfalse.ToString());
				
				// GO!
				//if (1 == _javascriptfalse) // Added by KK on 2015/10/09. Ugly code! But I dont know why -_-|||
				{
					_javascriptfalse = 0; // reset.
					string billNumberTextBoxId = string.Empty;
					string buttonId = string.Empty;
					
					switch (_expressCompany)
					{
						case OrderLib.ShipmentCompanies.Yto:
							billNumberTextBoxId = "offlineMailNoYTO";
							buttonId = "YTO";
							break;
						case OrderLib.ShipmentCompanies.Sf:
							billNumberTextBoxId = "offlineMailNoSF";
							buttonId = "SF";
							break;
						case OrderLib.ShipmentCompanies.Best:
							billNumberTextBoxId = "offlineMailNoHTKY";
							buttonId = "HTKY";
							break;
						case OrderLib.ShipmentCompanies.Post:
							billNumberTextBoxId = "offlineMailNoPOSTB";
							buttonId = "POSTB";
							break;
						case OrderLib.ShipmentCompanies.EMS:
							billNumberTextBoxId = "offlineMailNoEMS";
							buttonId = "EMS";
							break;
						case OrderLib.ShipmentCompanies.Zto:
							//临时注释. 2018年春节后, 韵达系统没搞定, 暂时使用大面单打印, 使用zto流程.
							//billNumberTextBoxId = "offlineMailNoZTO";
							//buttonId = "ZTO";
							//break;
						case OrderLib.ShipmentCompanies.Yunda:
							billNumberTextBoxId = "offlineMailNoYUNDA";
							buttonId = "YUNDA";
							break;
					}
					
					HtmlElement billNumberTextBox = wb.Document.GetElementById(billNumberTextBoxId);
					if (null == billNumberTextBox)
						return;
					Application.DoEvents();
					billNumberTextBox.SetAttribute("value", _billNumber);
					_filledShipmentNumber = true;

					//if (_autoSubmit)
					{
						HtmlElement button = wb.Document.GetElementById(buttonId);
						if (null == button)
							return;
						// Removed by KK on 2015/10/09. Found a better solution.
						//if (!_waitOnce)
						//{
						//    _waitOnce = true;
						//    return;
						//}
						//_clickedSubtmit = true;
						//System.Diagnostics.Trace.WriteLine("sleep...");
						//System.Threading.Thread.Sleep(new Random(Environment.TickCount).Next(300, 800));
						button.InvokeMember("click");
						//Application.DoEvents();
					}
				}
			}	
		}

		//private bool _waitOnce; // 第1次检测到点击发货按钮不点, 第2次点.
		//private bool _clickedSubtmit; // 已经点击过subtmi按钮, 避免重复点击.
		private int _javascriptfalse = 0;
		private bool _suspendMsg;
		protected override void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
		{
			System.Diagnostics.Trace.WriteLine(e.Url.AbsoluteUri);
			base.OnDocumentCompleted(e);

			if (e.Url.AbsoluteUri.ToLower().Equals("about:blank"))
			    return;
			if (e.Url.AbsoluteUri.ToLower().Equals("javascript:false;"))
				_javascriptfalse++;
				//return;
			if (e.Url.AbsoluteUri.ToLower().EndsWith("www.taobao.com/go/rgn/shopexpress/express_ad_2.php")) // http:// or https://
			    return;

			if (!this.SignedIn)
				return;

			if (wb.Document.Body.OuterHtml.Contains("您的请求无法完成，可能原因是"))
			{
				this.Navigate(string.Format(@"http://wuliu.taobao.com/user/consign.htm?trade_id={0}", _orderId));
				return;
			}
			
			if (wb.Document.Body.OuterHtml.Contains("自己联系物流"))
			{
				foreach (HtmlElement elm in wb.Document.Body.All)
				{
					if (elm.Name.Equals("自己联系物流"))
						System.Diagnostics.Trace.WriteLine("");
					//System.Diagnostics.Trace.WriteLine(string.Format("{0}, {1}, {2}, {3}", elm.TagName, elm.Name, elm.Parent.Name, elm.InnerText));
				}
			
				// Added by KK on 2016/07/26.
				if (null != wb.Document && null != wb.Document.Window)
					wb.Document.Window.ScrollTo(190, wb.Document.Body.ScrollRectangle.Height - wb.Height - 80);
			
				//System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"请检查您的电话号码，保证它是正确的");
				//System.Text.RegularExpressions.Match m = r.Match(wb.Document.Body.OuterHtml.ToLower().Replace("//alert(\"手机号码必须由8到16位数字构成\");", string.Empty));
				//if (!m.Success)
				//{
				//    r = new System.Text.RegularExpressions.Regex(@"必须由\d*到\d*位数字构成");
				//    m = r.Match(wb.Document.Body.OuterHtml.ToLower().Replace("//alert(\"手机号码必须由8到16位数字构成\");", string.Empty));
				//}

				//if (m.Success)
				//{
				//    if (_suspendMsg)
				//        return;
				//    _suspendMsg = true;
				//    MessageBox.Show(this, "电话或地址格式错误, 需要手动点发货.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				//    return;
				//}

				//if (wb.Document.Body.OuterHtml.Contains("您输入的运单号有误，请核对后重新输入"))
				//{
				//    MessageBox.Show(this, "运单号有误, 需手动处理.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				//    return;
				//}
				
				//System.Diagnostics.Trace.WriteLine("javascriptfalse: " + _javascriptfalse.ToString());
				
				//// GO!
				////if (1 == _javascriptfalse) // Added by KK on 2015/10/09. Ugly code! But I dont know why -_-|||
				//{
				//    _javascriptfalse = 0; // reset.
				//    string billNumberTextBoxId = string.Empty;
				//    string buttonId = string.Empty;
					
				//    switch (_expressCompany)
				//    {
				//        case OrderLib.ShipmentCompanies.Yto:
				//            billNumberTextBoxId = "offlineMailNoYTO";
				//            buttonId = "YTO";
				//            break;
				//        case OrderLib.ShipmentCompanies.Sf:
				//            billNumberTextBoxId = "offlineMailNoSF";
				//            buttonId = "SF";
				//            break;
				//        case OrderLib.ShipmentCompanies.Best:
				//            billNumberTextBoxId = "offlineMailNoHTKY";
				//            buttonId = "HTKY";
				//            break;
				//        case OrderLib.ShipmentCompanies.Post:
				//            billNumberTextBoxId = "offlineMailNoPOSTB";
				//            buttonId = "POSTB";
				//            break;
				//        case OrderLib.ShipmentCompanies.EMS:
				//            billNumberTextBoxId = "offlineMailNoEMS";
				//            buttonId = "EMS";
				//            break;
				//        case OrderLib.ShipmentCompanies.Zto:
				//            billNumberTextBoxId = "offlineMailNoZTO";
				//            buttonId = "ZTO";
				//            break;
				//    }
					
				//    HtmlElement billNumberTextBox = wb.Document.GetElementById(billNumberTextBoxId);
				//    if (null == billNumberTextBox)
				//        return;
				//    Application.DoEvents();
				//    billNumberTextBox.SetAttribute("value", _billNumber);
				//    _filledShipmentNumber = true;

				//    if (_autoSubmit)
				//    {
				//        HtmlElement button = wb.Document.GetElementById(buttonId);
				//        if (null == button)
				//            return;
				//        // Removed by KK on 2015/10/09. Found a better solution.
				//        //if (!_waitOnce)
				//        //{
				//        //    _waitOnce = true;
				//        //    return;
				//        //}
				//        //_clickedSubtmit = true;
				//        //System.Diagnostics.Trace.WriteLine("sleep...");
				//        //System.Threading.Thread.Sleep(new Random(Environment.TickCount).Next(300, 800));
				//        //button.InvokeMember("click");
				//        //Application.DoEvents();

				//    }
			}
			else if (wb.Document.Body.OuterHtml.Contains("恭喜您，操作成功") || (wb.Document.Body.OuterHtml.Contains("物流编号")&&wb.Document.Body.OuterHtml.Contains("发货方式")&&wb.Document.Body.OuterHtml.Contains("运单号码")))
			{
				this.DialogResult = DialogResult.OK;// _filledShipmentNumber ? DialogResult.OK : DialogResult.Yes; // Yes means the order had benn consigned.
				this.Close();
				System.Diagnostics.Trace.WriteLine("close");
				return;
			}
			else if (wb.Document.Body.OuterHtml.Contains("啊哦，系统升级中，小二在紧急处理"))
			{
				//this.DialogResult = DialogResult.Retry;
				this.Navigate(string.Format("http://wuliu.taobao.com/user/consign.htm?trade_id={0}", _orderId));
				return;
			}
			
			//if (_clickedSubtmit)
			//    return;

			//if (wb.Document.Body.OuterHtml.Contains("位数字构成")) // invalid tel number.
			//{
			//    if (_suspendMsg)
			//        return;
			//    _suspendMsg = true;
			//    MessageBox.Show(this, "电话或地址格式错误, 需要手动点发货.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//    return;
			//}

			//if (!wb.Document.Body.OuterHtml.Contains("确认收货信息及交易详情"))
			//    return;
			
			btnSendManually.Focus();
			
			// Added by KK on 2017/10/14.
			if (_autoSubmit)
			{
				if (null == _tmrAutoSubmit)
				{
					_tmrAutoSubmit = new Timer();
					_tmrAutoSubmit.Interval = 500;
					_tmrAutoSubmit.Tick += new EventHandler(_tmrAutoSubmit_Tick);
				}
				_tmrAutoSubmit.Start();
			}
		}

		void _tmrAutoSubmit_Tick(object sender, EventArgs e)
		{
			if (!_autoSubmit)
				return;
			if (null == wb)
				return;
			if (null == wb.Document)
				return;
			if (null == wb.Document.GetElementById("offlineMailNoYUNDA"))
				return;
			if (null == wb.Document.GetElementById("YUNDA"))
				return;

			_tmrAutoSubmit.Stop();
			Application.DoEvents();
			System.Threading.Thread.Sleep(new Random(Environment.TickCount).Next(507, 1073));
			Go();
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			int retry = 0;
			while (true && retry++ < 5)
			{
				try
				{
					Clipboard.SetText(txtBillNumber.Text);
					Application.DoEvents();
					break;
				}
				catch {}
			}
			Application.DoEvents();
			lblInfo.Text = string.Format("单号{0}已复制到剪贴板", txtBillNumber.Text);
			lblInfo.Visible = true;

			//Cursor.Position = new Point(this.Left + wb.Left + wb.Width / 2, this.Top + wb.Top + wb.Height - 20);
			//if (SystemInformation.MouseButtonsSwapped)
			//    KmRobot.Win32.ClickRightMouse();
			//else
			//    KmRobot.Win32.ClickLeftMouse();
		}

		private void ConsignShWebBrowserForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
				Go();
		}
	}
}