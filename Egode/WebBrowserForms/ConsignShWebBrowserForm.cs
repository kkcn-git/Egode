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

		// �Ƿ�������˵���.
		// �����жϵ���ʾ�����ɹ�ҳ��ʱ, �Ƕ������������ѷ���״̬, ���Ǳ��ε�����������ѷ���״̬ҳ��.
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
					//��ʱע��. 2018�괺�ں�, �ϴ�ϵͳû�㶨, ��ʱʹ�ô��浥��ӡ, ʹ��zto����.
					//lblExpressCompany.Text = "��ͨ";
					//break;
				case OrderLib.ShipmentCompanies.Sf:
					lblExpressCompany.Text = "˳��";
					break;
				case OrderLib.ShipmentCompanies.Best:
					lblExpressCompany.Text = "����";
					break;
				case OrderLib.ShipmentCompanies.Yunda:
					lblExpressCompany.Text = "�ϴ�";
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
			if (wb.Document.Body.OuterHtml.Contains("�Լ���ϵ����"))
			{
				System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"�������ĵ绰���룬��֤������ȷ��");
				System.Text.RegularExpressions.Match m = r.Match(wb.Document.Body.OuterHtml.ToLower().Replace("//alert(\"�ֻ����������8��16λ���ֹ���\");", string.Empty));
				if (!m.Success)
				{
					r = new System.Text.RegularExpressions.Regex(@"������\d*��\d*λ���ֹ���");
					m = r.Match(wb.Document.Body.OuterHtml.ToLower().Replace("//alert(\"�ֻ����������8��16λ���ֹ���\");", string.Empty));
				}

				if (m.Success)
				{
					if (_suspendMsg)
						return;
					_suspendMsg = true;
					MessageBox.Show(this, "�绰���ַ��ʽ����, ��Ҫ�ֶ��㷢��.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				if (wb.Document.Body.OuterHtml.Contains("��������˵���������˶Ժ���������"))
				{
					MessageBox.Show(this, "�˵�������, ���ֶ�����.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
							//��ʱע��. 2018�괺�ں�, �ϴ�ϵͳû�㶨, ��ʱʹ�ô��浥��ӡ, ʹ��zto����.
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

		//private bool _waitOnce; // ��1�μ�⵽���������ť����, ��2�ε�.
		//private bool _clickedSubtmit; // �Ѿ������subtmi��ť, �����ظ����.
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

			if (wb.Document.Body.OuterHtml.Contains("���������޷���ɣ�����ԭ����"))
			{
				this.Navigate(string.Format(@"http://wuliu.taobao.com/user/consign.htm?trade_id={0}", _orderId));
				return;
			}
			
			if (wb.Document.Body.OuterHtml.Contains("�Լ���ϵ����"))
			{
				foreach (HtmlElement elm in wb.Document.Body.All)
				{
					if (elm.Name.Equals("�Լ���ϵ����"))
						System.Diagnostics.Trace.WriteLine("");
					//System.Diagnostics.Trace.WriteLine(string.Format("{0}, {1}, {2}, {3}", elm.TagName, elm.Name, elm.Parent.Name, elm.InnerText));
				}
			
				// Added by KK on 2016/07/26.
				if (null != wb.Document && null != wb.Document.Window)
					wb.Document.Window.ScrollTo(190, wb.Document.Body.ScrollRectangle.Height - wb.Height - 80);
			
				//System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"�������ĵ绰���룬��֤������ȷ��");
				//System.Text.RegularExpressions.Match m = r.Match(wb.Document.Body.OuterHtml.ToLower().Replace("//alert(\"�ֻ����������8��16λ���ֹ���\");", string.Empty));
				//if (!m.Success)
				//{
				//    r = new System.Text.RegularExpressions.Regex(@"������\d*��\d*λ���ֹ���");
				//    m = r.Match(wb.Document.Body.OuterHtml.ToLower().Replace("//alert(\"�ֻ����������8��16λ���ֹ���\");", string.Empty));
				//}

				//if (m.Success)
				//{
				//    if (_suspendMsg)
				//        return;
				//    _suspendMsg = true;
				//    MessageBox.Show(this, "�绰���ַ��ʽ����, ��Ҫ�ֶ��㷢��.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				//    return;
				//}

				//if (wb.Document.Body.OuterHtml.Contains("��������˵���������˶Ժ���������"))
				//{
				//    MessageBox.Show(this, "�˵�������, ���ֶ�����.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
			else if (wb.Document.Body.OuterHtml.Contains("��ϲ���������ɹ�") || (wb.Document.Body.OuterHtml.Contains("�������")&&wb.Document.Body.OuterHtml.Contains("������ʽ")&&wb.Document.Body.OuterHtml.Contains("�˵�����")))
			{
				this.DialogResult = DialogResult.OK;// _filledShipmentNumber ? DialogResult.OK : DialogResult.Yes; // Yes means the order had benn consigned.
				this.Close();
				System.Diagnostics.Trace.WriteLine("close");
				return;
			}
			else if (wb.Document.Body.OuterHtml.Contains("��Ŷ��ϵͳ�����У�С���ڽ�������"))
			{
				//this.DialogResult = DialogResult.Retry;
				this.Navigate(string.Format("http://wuliu.taobao.com/user/consign.htm?trade_id={0}", _orderId));
				return;
			}
			
			//if (_clickedSubtmit)
			//    return;

			//if (wb.Document.Body.OuterHtml.Contains("λ���ֹ���")) // invalid tel number.
			//{
			//    if (_suspendMsg)
			//        return;
			//    _suspendMsg = true;
			//    MessageBox.Show(this, "�绰���ַ��ʽ����, ��Ҫ�ֶ��㷢��.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			//    return;
			//}

			//if (!wb.Document.Body.OuterHtml.Contains("ȷ���ջ���Ϣ����������"))
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
			lblInfo.Text = string.Format("����{0}�Ѹ��Ƶ�������", txtBillNumber.Text);
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