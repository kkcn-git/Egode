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
			string supermarketInfo, string rainbowInfo, string dealworthierInfo, string ouhuaInfo, string totalInfo,
			string supermarketFilename, string rainbowFilename, string dealworthierFilename, string ouhuaFilename,
			string packingListFilename, string addressFilename)
		{
			InitializeComponent();
			txtResult.Text += string.IsNullOrEmpty(supermarketInfo) ? string.Empty : (supermarketInfo + "\r\n");
			txtResult.Text += string.IsNullOrEmpty(rainbowInfo) ? string.Empty : (rainbowInfo + "\r\n");
			txtResult.Text += string.IsNullOrEmpty(dealworthierInfo) ? string.Empty : (dealworthierInfo + "\r\n");
			txtResult.Text += string.IsNullOrEmpty(ouhuaInfo) ? string.Empty : (ouhuaInfo + "\r\n");
			txtResult.Text += totalInfo + "\r\n";
			txtResult.Text += "\r\n";
			
			txtResult.Text += "���������ļ�:\r\n";

			if (!string.IsNullOrEmpty(supermarketFilename))
				txtResult.Text += string.Format("���б��: {0}\r\n", supermarketFilename);
			if (!string.IsNullOrEmpty(rainbowFilename))
				txtResult.Text += string.Format("�ʺ���: {0}\r\n", rainbowFilename);
			if (!string.IsNullOrEmpty(dealworthierFilename))
				txtResult.Text += string.Format("Dealworthier���: {0}\r\n", dealworthierFilename);
			if (!string.IsNullOrEmpty(ouhuaFilename))
				txtResult.Text += string.Format("ŷ�����: {0}\r\n", ouhuaFilename);
			if (!string.IsNullOrEmpty(packingListFilename))
				txtResult.Text += string.Format("�ֿⷢ���嵥: {0}\r\n", packingListFilename);
			if (!string.IsNullOrEmpty(addressFilename))
				txtResult.Text += string.Format("���ĵ�ַ: {0}\r\n", addressFilename);
			
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
						string.Format("��Ŷ...����ʱ��: {0}", DateTime.Now.ToString("HH:mm:ss")),
						this.Text,
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				DialogResult dr = MessageBox.Show(
					this,
					"��ȷ����������ĵ���ȷ.\n����״̬ͬ������������, ����״̬�����ñ��޸�, �˲���������.\n��Щ�����Ժ󽫲��������<�Ѹ���>������.\n�Ƿ�ȷ��Ҫ�޸Ķ���״̬��ͬ����������?", this.Text, 
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