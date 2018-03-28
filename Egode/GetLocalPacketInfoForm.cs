using System;
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
	public partial class GetLocalPacketInfoForm : Form
	{
		private class PdfPacketInfoListViewItem : ListViewItem
		{
			private PdfPacketInfoEx _p;

			public PdfPacketInfoListViewItem(PdfPacketInfoEx p)
			{
				_p = p;
				_p.OnDataChanged += new EventHandler(_p_OnDataChanged);

				this.Text = _p.MatchedRecipientName;
				this.SubItems.Add(_p.RecipientName);
				this.SubItems.Add(_p.ReceiverPhone);
				this.SubItems.Add(_p.ShipmentNumber);
				this.SubItems.Add(_p.Weight.ToString("0.0kg"));
				this.SubItems.Add(Path.GetFileName(_p.Filename));
				this.SubItems[0].BackColor = Color.LightBlue;
				this.UseItemStyleForSubItems = false;
			}

			void _p_OnDataChanged(object sender, EventArgs e)
			{
				this.Text = _p.MatchedRecipientName;
				this.SubItems[1].Text = _p.RecipientName;
				this.SubItems[2].Text = _p.ReceiverPhone;
				this.SubItems[3].Text = _p.ShipmentNumber;
				this.SubItems[4].Text = _p.Weight.ToString("0.0kg");
				this.SubItems[5].Text = Path.GetFileName(_p.Filename);
			}
		}

		private List<PdfPacketInfoEx> _packetInfos = new List<PdfPacketInfoEx>();

		public GetLocalPacketInfoForm()
		{
			InitializeComponent();
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
					_packetInfos.Add(p);
					lvwPdfPacketInfos.Items.Add(new PdfPacketInfoListViewItem(p));
				}
				
				if (_packetInfos.Count > 0)
					tsbtnPackingList.Enabled = true;
			}

			Cursor.Current = Cursors.Default;
		}

		private void UpdateShipmentNumberInPackingList(string packingListExcel)
		{
			Cursor.Current = Cursors.WaitCursor;

			Excel excel = null;

			try
			{
				excel = new Excel(packingListExcel, true);
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
				excel = new Excel(packingListExcel, true);

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
					string recipientNamePinyin = HanZiToPinYin.Convert(recipientNameCn);
					PdfPacketInfoEx ppi = PdfPacketInfoEx.GetItem(recipientNamePinyin, _packetInfos, true);
					if (null == ppi)
					{
						PdfPacketInfoEx ppi1 = new PdfPacketInfoEx(string.Empty, PacketTypes.Unknown, string.Empty, string.Empty, string.Empty, 0);
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
				"导入发货清单后不能再追加导入包裹单.\n是否已经导入所有需要处理的包裹单?", this.Text,
				MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (DialogResult.No == dr)
				return;

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
	}
}