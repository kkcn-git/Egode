using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Egode
{
	public partial class PacketInfoListForm : Form
	{
		public delegate void OnSearchEventHandler(string keyword);
		public event OnSearchEventHandler OnSearch;
		
		// Added by KK on 2015/12/24.
		private static PdfPacketInfoEx _selectedPacketInfo;
		public static PdfPacketInfoEx SelectedPacketInfo
		{
			get { return _selectedPacketInfo; }
		}
		
		private class ListViewItemSorter : IComparer
		{
			#region IComparer Members

			public int Compare(object x, object y)
			{
				return Compare((PdfPacketInfoListViewItem)x, (PdfPacketInfoListViewItem)y);
			}
			
			private int Compare(PdfPacketInfoListViewItem x, PdfPacketInfoListViewItem y)
			{
				if (null == x && null == y)
					return 0;
				if (null == x)
					return -1;
				if (null == y)
					return 1;

				if (x.SubItems[1].Text.Equals(y.SubItems[1].Text))
					return string.Compare(x.SubItems[2].Text, y.SubItems[2].Text);
				return string.Compare(x.Text, y.Text);
			}

			#endregion
		}

		private class PdfPacketInfoListViewItem : ListViewItem
		{
			private PdfPacketInfoEx _p;
			
			public PdfPacketInfoEx PacketInfo
			{
				get { return _p; }
			}

			public PdfPacketInfoListViewItem(PdfPacketInfoEx p)
			{
				_p = p;
				_p.OnDataChanged += new EventHandler(_p_OnDataChanged);

				this.UseItemStyleForSubItems = false;

				//this.Text = index.ToString("000");
				this.SubItems.Add(Packet.GetPacketTypeDesc(_p.Type));
				this.SubItems.Add(_p.MatchedRecipientName);
				this.SubItems.Add(_p.RecipientName);
				this.SubItems.Add(_p.ReceiverPhone);
				this.SubItems.Add(_p.ShipmentNumber);
				this.SubItems.Add(_p.Weight.ToString("0.0kg"));
				this.SubItems.Add(_p.Status);
				this.SubItems.Add(_p.Address);
				this.SubItems.Add(Path.GetFileName(_p.Filename));
				this.SubItems[2].BackColor = Color.LightBlue;

				if (string.IsNullOrEmpty(_p.ShipmentNumber))
				{
					this.SubItems[5].Text = "未找到国际单号";
					this.SubItems[5].ForeColor = Color.Red;
				}
			}

			void _p_OnDataChanged(object sender, EventArgs e)
			{
				this.SubItems[2].Text = _p.MatchedRecipientName;
				//this.SubItems[2].Text = _p.RecipientName;
				//this.SubItems[3].Text = _p.ReceiverPhone;
				//this.SubItems[4].Text = _p.ShipmentNumber;
				//this.SubItems[5].Text = _p.Weight.ToString("0.0kg");
				//this.SubItems[6].Text = Path.GetFileName(_p.Filename);
			}
		}

		private List<PdfPacketInfoEx> _packetInfos = new List<PdfPacketInfoEx>();

		public PacketInfoListForm()
		{
			InitializeComponent();
			
			lvwPdfPacketInfos.ListViewItemSorter = new ListViewItemSorter();
		}

		private void tsbtnGetLocalPdfPackets_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;

			FolderBrowserDialog fbd = new FolderBrowserDialog();
			fbd.Description = "选择1个目录. 此目录中包含包裹单文件(pdf).";
			//fbd.SelectedPath = Path.GetDirectoryName(Application.ExecutablePath);
			if (Directory.Exists(@"J:\=egode=\=出单="))
				fbd.SelectedPath = @"J:\=egode=\=出单=";

			if (DialogResult.OK == fbd.ShowDialog(this))
			{
				List<PdfPacketInfoEx> pdfPackets = PdfPacketInfo.GetPdfPacketInfos(fbd.SelectedPath, false);
				if (null == pdfPackets)
				{
					MessageBox.Show(this, "No PDF file found in selected folder.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				foreach (PdfPacketInfoEx p in pdfPackets)
				{
					if (IsPacketExist(p, _packetInfos))
						continue;
					_packetInfos.Add(p);
					lvwPdfPacketInfos.Items.Add(new PdfPacketInfoListViewItem(p));
				}
				
				if (_packetInfos.Count > 0)
					tsbtnPackingList.Enabled = true;

				RefreshIndex();
				CheckDuplication();
			}

			Cursor.Current = Cursors.Default;
		}

		private void tsbtnGetOnlinPacket_Click(object sender, EventArgs e)
		{
			GetOnlinePacketInfoForm opif = new GetOnlinePacketInfoForm();
			if (DialogResult.OK == opif.ShowDialog(this))
			{
				if (null == opif.PacketInfos)
				{
					MessageBox.Show(this, "No packet info found.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				
				this.Text = string.Format("包裹单信息匹配 - {0}", opif.Time24Code);

				foreach (PdfPacketInfoEx p in opif.PacketInfos)
				{
					if (IsPacketExist(p, _packetInfos))
						continue;
					_packetInfos.Add(p);
					lvwPdfPacketInfos.Items.Add(new PdfPacketInfoListViewItem(p));
				}
				
				if (_packetInfos.Count > 0)
					tsbtnPackingList.Enabled = true;
				
				RefreshIndex();
				CheckDuplication();
			}
		}
		
		private void RefreshIndex()
		{
			for (int i = 0; i < lvwPdfPacketInfos.Items.Count; i++)
				lvwPdfPacketInfos.Items[i].Text = (i+1).ToString("000");
		}
		
		private void CheckDuplication()
		{
			for (int i = 0; i < lvwPdfPacketInfos.Items.Count-1; i++)
			{
				for (int j = i+1; j < lvwPdfPacketInfos.Items.Count; j++)
				{
					if (!string.IsNullOrEmpty(lvwPdfPacketInfos.Items[i].SubItems[2].Text))
					{
						if (lvwPdfPacketInfos.Items[i].SubItems[2].Text.Equals(lvwPdfPacketInfos.Items[j].SubItems[2].Text))
						{
							lvwPdfPacketInfos.Items[i].SubItems[2].BackColor = Color.OrangeRed;
							lvwPdfPacketInfos.Items[j].SubItems[2].BackColor = Color.OrangeRed;
						}
					}
					if (!string.IsNullOrEmpty(lvwPdfPacketInfos.Items[i].SubItems[3].Text))
					{
						if (lvwPdfPacketInfos.Items[i].SubItems[3].Text.Equals(lvwPdfPacketInfos.Items[j].SubItems[3].Text))
						{
							lvwPdfPacketInfos.Items[i].SubItems[3].BackColor = Color.OrangeRed;
							lvwPdfPacketInfos.Items[j].SubItems[3].BackColor = Color.OrangeRed;
						}
					}
				}
			}
		}
		
		private bool IsPacketExist(PdfPacketInfo p, List<PdfPacketInfoEx> packetInfos)
		{
			if (null == packetInfos)
				return false;

			foreach (PdfPacketInfo pi in packetInfos)
			{
				if (p.ShipmentNumber.Equals(pi.ShipmentNumber))
					return true;
			}
			return false;
		}

		private void UpdateShipmentNumberInPackingList(string packingListExcel)
		{
			Cursor.Current = Cursors.WaitCursor;

			Excel excel = null;

			try
			{
				excel = new Excel(packingListExcel, true, Excel.OledbVersions.OLEDB40);
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

				for (int i = 1; i <= ds.Tables[0].Rows.Count; i++)
					excel.Insert("Sheet1", string.Format("G{0}:G{0}", i), string.Format("'x{0}'", Guid.NewGuid().ToString()));
				excel.Close();
				System.Threading.Thread.Sleep(500);
				Application.DoEvents();
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
			}

			try
			{
				excel = new Excel(packingListExcel, true, Excel.OledbVersions.OLEDB40);

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
					PdfPacketInfoEx ppi = PdfPacketInfoEx.GetItemByRecipientName(recipientNameCn, _packetInfos, true);
					if (null == ppi)
					{
						PdfPacketInfoEx ppi1 = new PdfPacketInfoEx(string.Empty, PacketTypes.Unknown, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty);
						ppi1.MatchedRecipientName = recipientNameCn;
						_packetInfos.Add(ppi1);
						lvwPdfPacketInfos.Items.Add(new PdfPacketInfoListViewItem(ppi1));
						continue;
					}

					ppi.MatchedRecipientName = recipientNameCn;

					try
					{
						excel.Update(
							"Sheet1",
							"运单号", string.Format("{0}:{1}", ppi.RecipientName, ppi.ShipmentNumber),
							//"序号", dr.ItemArray[0].ToString());
							//"收货人", recipientNameCn);
							//"序号", dr.ItemArray[0].ToString());
							"reserved", dr.ItemArray[6].ToString());
						ppi.Updated = true;
						//Trace.WriteLine(dr.ItemArray[0].ToString());
						//if (dr.ItemArray[0].ToString().Equals("28"))
						//    Trace.WriteLine("");
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
				MessageBox.Show(this, "Error occured during udpate shipment number into excel file.\n" + ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			finally
			{
				excel.Close();
				Cursor.Current = Cursors.Default;
			}
		}

		private void tsbtnPackingList_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;

			DialogResult dr = MessageBox.Show(
				this,
				"导入发货清单后不能再追加导入包裹单.\n(但是可以继续导入发货清单)\n是否已经导入所有需要处理的包裹单?", this.Text,
				MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (DialogResult.No == dr)
				return;
			
			tsbtnGetLocalPdfPackets.Enabled = true;
			tsbtnGetOnlinPacket.Enabled = true;

			OpenFileDialog ofd = new OpenFileDialog();
			ofd.FileName = "发货清单*.xls";
			ofd.Multiselect = true;

			if (DialogResult.OK == ofd.ShowDialog(this))
			{
				foreach (string filename in ofd.FileNames)
					UpdateShipmentNumberInPackingList(filename);
			}
			
			Cursor.Current = Cursors.Default;
		}

		private void lvwPdfPacketInfos_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtRecipientNameInPackingList.Text = string.Empty;
			txtRecipientName.Text = string.Empty;
			txtReceiverPhone.Text = string.Empty;
			txtShipmentNumber.Text = string.Empty;
			lnklblFilename.Text = "<null>";
			
			if (lvwPdfPacketInfos.SelectedItems.Count <= 0)
				return;
			
			PdfPacketInfoListViewItem item = lvwPdfPacketInfos.SelectedItems[0] as PdfPacketInfoListViewItem;
			txtRecipientNameInPackingList.Text = item.PacketInfo.MatchedRecipientName;
			txtRecipientName.Text = item.PacketInfo.RecipientName;
			txtReceiverPhone.Text = item.PacketInfo.ReceiverPhone;
			txtShipmentNumber.Text = item.PacketInfo.ShipmentNumber;
			txtAddress.Text = item.PacketInfo.Address;
			lnklblFilename.Text = (string.IsNullOrEmpty(item.PacketInfo.Filename) ? "Unavailable" : item.PacketInfo.Filename);
			lnklblFilename.Enabled = !string.IsNullOrEmpty(item.PacketInfo.Filename);
		}

		private void btnSearchRecipientNameFromPackingList_Click(object sender, EventArgs e)
		{
			CopyShipmentNumberInfoToClipboard();
			if (null != this.OnSearch)
				this.OnSearch(txtRecipientNameInPackingList.Text);
		}

		private void btnSearchRecipientName_Click(object sender, EventArgs e)
		{
			CopyShipmentNumberInfoToClipboard();
			if (null != this.OnSearch)
				this.OnSearch(txtRecipientName.Text);
		}

		private void btnSearchReceiverPhone_Click(object sender, EventArgs e)
		{
			CopyShipmentNumberInfoToClipboard();
			if (null != this.OnSearch)
				this.OnSearch(txtReceiverPhone.Text);
		}

		private void btnSearchShipmentNumber_Click(object sender, EventArgs e)
		{
			CopyShipmentNumberInfoToClipboard();
			if (null != this.OnSearch)
				this.OnSearch(txtShipmentNumber.Text);
		}

		private void btnSearchAddress_Click(object sender, EventArgs e)
		{
			CopyShipmentNumberInfoToClipboard();
			if (null != this.OnSearch)
				this.OnSearch(string.IsNullOrEmpty(txtAddress.SelectedText)?txtAddress.Text:txtAddress.SelectedText);
		}

		private void lnklblFilename_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(lnklblFilename.Text);
		}

		// Modified by KK on 2015/12/24.
		// 不再使用系统Clipboard来传递单号信息.
		// 在PacketInfoListForm中保存全局静态包裹单信息, 总是记录当前选中的包裹单.
		// 为兼容旧代码, 函数名沿用CopyShipmentNumberInfoToClipboard()
		//private void CopyShipmentNumberInfoToClipboard()
		//{
		//    if (lvwPdfPacketInfos.SelectedItems.Count <= 0)
		//        return;

		//    PdfPacketInfoEx ppi = ((PdfPacketInfoListViewItem)lvwPdfPacketInfos.SelectedItems[0]).PacketInfo;

		//    string shipmentNumberInfo = string.Format("[{0}] {1}", Packet.GetPacketTypePrefix(ppi.Type), ppi.ShipmentNumber);
		//    SetCliboardText(shipmentNumberInfo);
		//    Application.DoEvents();
		//    ClipboardPromptForm cpf = new ClipboardPromptForm("单号", shipmentNumberInfo);
		//    cpf.Show(this);
		//    cpf.Location = new Point(this.Location.X + (this.Width - cpf.Width) / 2, this.Location.Y + (this.Height - cpf.Height) / 2);
		//    lblClipboardPrompt.Text = string.Format("单号<{0}>已复制到剪贴板", shipmentNumberInfo);
		//    lblClipboardPrompt.Visible = true;
		//    Application.DoEvents();
		//}
		private void CopyShipmentNumberInfoToClipboard()
		{
			if (lvwPdfPacketInfos.SelectedItems.Count <= 0)
				return;

			_selectedPacketInfo = ((PdfPacketInfoListViewItem)lvwPdfPacketInfos.SelectedItems[0]).PacketInfo;

			//string shipmentNumberInfo = string.Format("[{0}] {1}", Packet.GetPacketTypePrefix(ppi.Type), ppi.ShipmentNumber);
			//SetCliboardText(shipmentNumberInfo);
			//Application.DoEvents();
			//ClipboardPromptForm cpf = new ClipboardPromptForm("单号", shipmentNumberInfo);
			//cpf.Show(this);
			//cpf.Location = new Point(this.Location.X + (this.Width - cpf.Width) / 2, this.Location.Y + (this.Height - cpf.Height) / 2);
			lblClipboardPrompt.Text = string.Format("包裹单信息已记录: [{0}]{1}", Packet.GetPacketTypeDesc(_selectedPacketInfo.Type), _selectedPacketInfo.ShipmentNumber);
			lblClipboardPrompt.Visible = true;
			Application.DoEvents();
		}

		void SetCliboardText(string s)
		{
			bool ok = false;
			while (!ok)
			{
				try
				{
					Clipboard.SetText(s);
					ok = true;
				}
				catch
				{
					Application.DoEvents();
				}
			}
		}

		private void lblClipboardPrompt_SizeChanged(object sender, EventArgs e)
		{
			Point p = new Point(txtShipmentNumber.Right - lblClipboardPrompt.Width, txtAddress.Bottom + 2);
			p.Offset(pnlDetails.Left, pnlDetails.Top);
			lblClipboardPrompt.Location = p;
		}

		private void lvwPdfPacketInfos_DoubleClick(object sender, EventArgs e)
		{
			if (lvwPdfPacketInfos.SelectedItems.Count <= 0)
				return;
			btnSearchReceiverPhone_Click(btnSearchReceiverPhone, EventArgs.Empty);
		}
	}
}