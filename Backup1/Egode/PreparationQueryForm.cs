using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Egode
{
	public partial class PreparationQueryForm : Form
	{
		public PreparationQueryForm()
		{
			InitializeComponent();
		}

		private void PreparationQueryForm_Load(object sender, EventArgs e)
		{
			PromptForm prompt = new PromptForm();
			prompt.MaxLine = 2;
			prompt.Owner = this;
			prompt.Show(this);
			StartDownloadPrepareHistory(prompt);
		}

		private void PreparationQueryForm_Shown(object sender, EventArgs e)
		{
			txtOrderId.Focus();
			
			string s = Clipboard.GetText().Trim();
			if (IsOrderIdFormat(s))
				txtOrderId.Text = s;
		}

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
			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("正在下载出单记录...{0}%", e.ProgressPercentage);
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
			prompt.Messages[prompt.Messages.Count - 1].Content = string.Format("成功下载{0}条出单记录.", c);
			prompt.RefreshDisplay();
			prompt.OKEnabled = true;
		}

		private void btnQuery_Click(object sender, EventArgs e)
		{
			if (!IsOrderIdFormat(txtOrderId.Text.Trim()))
			{
				MessageBox.Show(this, "订单编号是15位或16位数字!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			PrepareHistory h = PrepareHistory.Get(txtOrderId.Text);
			if (null == h)
			{
				MessageBox.Show(this, "无出单记录", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show(
					this,
					string.Format("已出单\n出单时间:{0}\n出单人:{1}\n店铺:{2}", h.Date.ToString("yyyy/MM/dd HH:mm:ss"), h.Operator, h.Shop), 
					this.Text, 
					MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
		
		private static bool IsOrderIdFormat(string s)
		{
			if (string.IsNullOrEmpty(s))
				return false;
			System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^\d{15,16}$");
			System.Text.RegularExpressions.Match m = r.Match(s);
			return m.Success;
		}

		private void PreparationQueryForm_Activated(object sender, EventArgs e)
		{
			txtOrderId.Focus();
			txtOrderId.SelectionStart = 0;
			txtOrderId.SelectionLength = txtOrderId.Text.Length;
		}

		private void txtOrderId_Enter(object sender, EventArgs e)
		{
			txtOrderId.SelectionStart = 0;
			txtOrderId.SelectionLength = txtOrderId.Text.Length;
		}

		private void txtOrderId_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((int)e.KeyChar == 13)
			{
				btnQuery_Click(btnQuery, EventArgs.Empty);
			}
		}
	}
}