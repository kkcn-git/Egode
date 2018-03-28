using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Egode
{
	public partial class UploadingAddressForm : Egode.WaitingFormBase
	{
		private Address _addr;

		public UploadingAddressForm(Address addr)
		{
			InitializeComponent();
			
			_addr = addr;
			base.WorkingThread = new Thread(new ThreadStart(AddToServer));
			this.Info = "Uploading address to server...";
		}

		void AddToServer()
		{
			if (null == _addr)
				return;

			DateTime dt = DateTime.Now;
			string url = string.Format(Common.URL_DATA_CENTER, "newaddr");
			url += string.Format("&tp={0}&id={1}&pvn={2}&c1={3}&c2={4}&d={5}&sa={6}&r={7}&mb={8}&ph={9}&pc={10}&cm={11}", 
				_addr.Type, _addr.Id,  
				_addr.Province, _addr.City1, _addr.City2, _addr.District, _addr.StreetAddress, 
				_addr.Recipient, _addr.Mobile, _addr.Phone, _addr.PostCode, _addr.Comment);
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
			request.Method = "GET";
			request.ContentType = "text/xml";
			WebResponse response = request.GetResponse();
			StreamReader reader = new StreamReader(response.GetResponseStream());
			string result = reader.ReadToEnd();
			reader.Close();
			
		    if (!string.IsNullOrEmpty(result) && result.StartsWith("ok"))
		        base.Succeed();
		    else
		        base.Fail();
			//return result;
		}
	}
}