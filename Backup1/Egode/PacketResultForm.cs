using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class PacketResultForm : Form
	{
		public event EventHandler OnUpdateStatus;
	
		public PacketResultForm(
			string supermarketInfo, string rainbowInfo, string dealworthierInfo, string ouhuaInfo, string hanslordInfo, string totalInfo,
			string supermarketFilename, string rainbowFilename, string dealworthierFilename, string ouhuaFilename, string hanslordFilename,
			string packingListFilename, string addressFilename, string addresseImportIerungFilename)
		{
			InitializeComponent();
			txtResult.Text += string.IsNullOrEmpty(supermarketInfo) ? string.Empty : (supermarketInfo + "\r\n");
			txtResult.Text += string.IsNullOrEmpty(rainbowInfo) ? string.Empty : (rainbowInfo + "\r\n");
			txtResult.Text += string.IsNullOrEmpty(dealworthierInfo) ? string.Empty : (dealworthierInfo + "\r\n");
			txtResult.Text += string.IsNullOrEmpty(ouhuaInfo) ? string.Empty : (ouhuaInfo + "\r\n");
			txtResult.Text += string.IsNullOrEmpty(hanslordInfo) ? string.Empty : (hanslordInfo + "\r\n");
			txtResult.Text += totalInfo + "\r\n";
			txtResult.Text += "\r\n";
			
			txtResult.Text += "生成以下文件:\r\n";

			if (!string.IsNullOrEmpty(supermarketFilename))
				txtResult.Text += string.Format("超市表格: {0}\r\n", supermarketFilename);
			if (!string.IsNullOrEmpty(addresseImportIerungFilename))
				txtResult.Text += string.Format("AddresseImportIerung(for PostNL): {0}\r\n", addresseImportIerungFilename);
			if (!string.IsNullOrEmpty(rainbowFilename))
				txtResult.Text += string.Format("彩虹表格: {0}\r\n", rainbowFilename);
			if (!string.IsNullOrEmpty(dealworthierFilename))
				txtResult.Text += string.Format("Dealworthier表格: {0}\r\n", dealworthierFilename);
			if (!string.IsNullOrEmpty(ouhuaFilename))
				txtResult.Text += string.Format("欧华表格: {0}\r\n", ouhuaFilename);
			if (!string.IsNullOrEmpty(hanslordFilename))
				txtResult.Text += string.Format("Hanslord表格: {0}\r\n", hanslordFilename);
			if (!string.IsNullOrEmpty(packingListFilename))
				txtResult.Text += string.Format("仓库发货清单: {0}\r\n", packingListFilename);
			if (!string.IsNullOrEmpty(addressFilename))
				txtResult.Text += string.Format("中文地址: {0}\r\n", addressFilename);
			
			//txtResult.Height = txtResult.PreferredSize.Height;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnUpdateStatus_Click(object sender, EventArgs e)
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

				string recentLog = string.Empty;
				if (!Common.IsServerDbReady(out recentLog))
				{
					MessageBox.Show(
						this,
						string.Format("The database is being operated.\nPlease STAY in this window and wait for a while to try again.\nDO NOT close this window before synchronizing succeeded.\n\nAction in process: {0}", recentLog),
						this.Text,
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				DialogResult dr = MessageBox.Show(
					this,
					"请确认所有输出文档正确.\n出单状态同步到服务器后, 订单状态将永久被修改, 此操作不可逆.\n这些订单以后将不会出现在<已付款>订单中.\n是否确定要修改订单状态并同步到服务器?", this.Text, 
					MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (DialogResult.No == dr)
					return;
					
				if (null != this.OnUpdateStatus)
					this.OnUpdateStatus(this, EventArgs.Empty);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}
	}
}