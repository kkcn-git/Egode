using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Egode
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			//string s = HanZiToPinYin.Convert("Â¬Ïþá¯, µÒº¯ö­, µÔ¾²");
			//string s = UserLoginForm.CalcMd5("xiaomei123");
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

//			Blacklist.Import2();
//			return;

			//Application.Run(new PacketInfoForm());
			//return;
			
			ValidateForm vf = new ValidateForm();
			DialogResult dr = vf.ShowDialog();
			switch (dr)
			{
			    case DialogResult.Yes: // validating succeeded.
			        // do nothing;
			        break;
				
			    case DialogResult.No: // validating failed. Invalidate user.
			        MessageBox.Show(
			            string.Format("Contact KK, pls~\nResponse from server: {0}", vf.ResponseFromServer), 
			            "°¡Å¶~:(", 
			            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			        return;
				
			    case DialogResult.Cancel: // cancel
			        return;
			}

			dr = new UserLoginForm().ShowDialog();
			if (DialogResult.Cancel == dr)
				return;

			if (null != args && args.Length > 0)
			{
				if (args[0].ToLower().Equals("rate"))
					Application.Run(new RateForm(null));
				//else if (args[0].ToLower().Equals("local"))
				//    Application.Run(new MainForm(true));
				else if (args[0].ToLower().Equals("stockstat"))
					Application.Run(new StockStatForm(Common.URL_STOCK_SH));
				else if (args[0].ToLower().Equals("stockstatnb"))
					Application.Run(new StockStatForm(Common.URL_STOCK_NINGBO));
				else if (args[0].ToLower().Equals("stock"))
					Application.Run(new StockForm(OrderLib.ShippingOrigins.Shanghai));
				else if (args[0].ToLower().Equals("stocknb"))
					Application.Run(new StockForm(OrderLib.ShippingOrigins.Ningbo));
				else if (args[0].ToLower().Equals("refund"))
					Application.Run(new RefundForm());
				else if (args[0].ToLower().Equals("dhl"))
					Application.Run(new OrderDhlStateForm());
				else if (args[0].ToLower().Equals("pre"))
					Application.Run(new PreparationQueryForm());
				else if (args[0].ToLower().Equals("dd"))
					Application.Run(new Dangdang.DangDangOrdersForm());
				else if (args[0].ToLower().Equals("outer"))
					Application.Run(new OuterOrder.OuterOrdersForm());
				//else if (args[0].ToLower().Equals("pdf"))
				//{
				//    FolderBrowserDialog fbd = new FolderBrowserDialog();
				//    if (DialogResult.OK == fbd.ShowDialog())
				//    {
				//        List<PdfPacketInfoEx> pdfPackets = PdfPacketInfo.GetPdfPacketInfos(fbd.SelectedPath, false);
				//        PdfMatchResultForm pmrf = new PdfMatchResultForm(pdfPackets);
				//        pmrf.ShowInTaskbar = true;
				//        Application.Run(pmrf);
				//    }
				//}
			}
			else
			{
				string recentLog = string.Empty;
				if (!Common.IsServerDbReady(out recentLog))
				{
					MessageBox.Show(
						null, 
						string.Format("The database is being operated.\nPlease wait for a while and restart.\n\nAction in process: {0}", recentLog), 
						"Egode System", 
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				if (DialogResult.OK != new ShopSelectorForm().ShowDialog())
					return;
				new OrderDownloadModeSelectorForm().ShowDialog();
				if (Settings.Instance.UiType != Settings.UiTypes.Deutschland) // both Shanghai and Full need to set printers.
					new PrinterSelectorForm().ShowDialog();
				Application.Run(MainForm.Instance);
			}
		}
		
		//static void Decompress()
		//{
		//    FileStream r = new FileStream(@"E:\taobao\egode\backup\orders-c.xml", FileMode.Open);
		//    byte[] filebuf = new byte[r.Length];
		//    r.Read(filebuf, 0, filebuf.Length);
		//    MemoryStream ms = new MemoryStream(filebuf);
		//    System.IO.Compression.GZipStream decompress = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress);
		//    byte[] buf = new byte[1024];
		//    int len = 0;
		//    MemoryStream decompressedStream = new MemoryStream();
		//    while ((len = decompress.Read(buf, 0, buf.Length)) > 0)
		//        decompressedStream.Write(buf, 0, len);
		//    string xml = Encoding.UTF8.GetString(decompressedStream.GetBuffer());
		//    StreamWriter w = new StreamWriter(@"E:\taobao\egode\backup\orders.xml");
		//    w.Write(xml);
		//    w.Close();
		//    decompressedStream.Close();
		//    decompress.Close();
		//    ms.Close();
		//}
	}
}