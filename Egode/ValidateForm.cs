using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Management;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Egode
{
	public partial class ValidateForm : Form
	{
		private string _responseFromServer;
		private bool _done;
		private System.Windows.Forms.Timer _tmr;
	
		public ValidateForm()
		{
			InitializeComponent();
			_tmr = new System.Windows.Forms.Timer();
			_tmr.Interval = 100;
			_tmr.Tick += new EventHandler(_tmr_Tick);
			_tmr.Start();
		}
		
		public string ResponseFromServer
		{
			get { return _responseFromServer; }
		}

		void _tmr_Tick(object sender, EventArgs e)
		{
			if (_done)
				this.Close();
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			
			Thread t = new Thread(new ThreadStart(ValidateOnline));
			t.IsBackground = true;
			t.Start();
		}

		void ValidateOnline()
		{
			ManagementObjectSearcher query =new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration") ;
			ManagementObjectCollection queryCollection = query.Get();
			string mac = string.Empty;
			foreach( ManagementObject mo in queryCollection )
			{
				if(mo["IPEnabled"].ToString() == "True")
					mac = mo["MacAddress"].ToString();
			}
			
			try
			{
				string url = string.Format("{0}datacenter.aspx?cmd=val&com={1}&mac={2}", Common.URL_ROOT, Environment.MachineName, mac);
				HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
				request.Method = "GET";
				request.ContentType = "text/xml";
				WebResponse response = request.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream());
				_responseFromServer = reader.ReadToEnd();
				reader.Close();
			}
			catch (Exception ex)
			{
				_responseFromServer = ex.Message;
			}

			if (!string.IsNullOrEmpty(_responseFromServer) && _responseFromServer.StartsWith("ok"))
				this.DialogResult = DialogResult.Yes;
			else
				this.DialogResult = DialogResult.No;
			
			_done = true;
		}

		//protected override void OnShown(EventArgs e)
		//{
		//    base.OnShown(e);

		//    ManagementObjectSearcher query =new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration") ;
		//    ManagementObjectCollection queryCollection = query.Get();
		//    string mac = string.Empty;
		//    foreach( ManagementObject mo in queryCollection )
		//    {
		//        if(mo["IPEnabled"].ToString() == "True")
		//            mac = mo["MacAddress"].ToString();
		//    }
			
		//    string result = string.Empty;
		//    try
		//    {
		//        string url = string.Format("http://www.sozi.com.cn/kktb/datacenter.aspx?cmd=val&com={0}&mac={1}", Environment.MachineName, mac);
		//        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
		//        request.Method = "POST";
		//        request.ContentType = "text/xml";
		//        request.BeginGetResponse(new AsyncCallback(RespCallback), request);
		//    }
		//    catch {}
		//}
		
		//private void RespCallback(IAsyncResult asynchronousResult)
		//{
		//    string result = string.Empty;
		//    try
		//    {
		//        HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
		//        WebResponse response = request.EndGetResponse(asynchronousResult);
		//        StreamReader reader = new StreamReader(response.GetResponseStream());
		//        result = reader.ReadToEnd();
		//        reader.Close();
		//    }
		//    catch {}

		//    if (!string.IsNullOrEmpty(result) && result.StartsWith("ffff"))
		//        this.DialogResult = DialogResult.No;
		//    else
		//        this.DialogResult = DialogResult.Yes;
			
		//    _done = true;
		//}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}