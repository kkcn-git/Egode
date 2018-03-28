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
	public partial class ValidateForm : WaitingFormBase
	{
		private string _responseFromServer;
	
		public ValidateForm()
		{
			InitializeComponent();

			base.WorkingThread = new Thread(new ThreadStart(ValidateOnline));
			this.Info = "Connecting to server...";
		}
		
		public string ResponseFromServer
		{
			get { return _responseFromServer; }
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
		        base.Succeed();
		    else
		        base.Fail();
		}
	}
}