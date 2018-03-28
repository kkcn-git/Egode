using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class PdfMatchResultForm : Form
	{
		public delegate void OnSearchEventHandler(string keyword);
		public event OnSearchEventHandler OnSearch;
	
		private List<PdfPacketInfoEx> _pdfPackets;
	
		public PdfMatchResultForm(List<PdfPacketInfoEx> pdfPackets)
		{
			_pdfPackets = pdfPackets;
			InitializeComponent();
		}

		private void PdfMatchResultForm_Load(object sender, EventArgs e)
		{
			if (null == _pdfPackets || _pdfPackets.Count <= 0)
				return;
			
			foreach (PdfPacketInfoEx ppi in _pdfPackets)
			{
				FlowLayoutPanel pnl = new FlowLayoutPanel();
				pnl.AutoSize = true;
				pnl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
				pnl.FlowDirection = FlowDirection.LeftToRight;
				pnl.WrapContents = false;
				pnl.Margin = new Padding(0);

				TextBox txtShipmentInfo = new TextBox();
				txtShipmentInfo.Font = this.Font;
				txtShipmentInfo.ForeColor = this.ForeColor;
				txtShipmentInfo.Text = string.IsNullOrEmpty(ppi.ShipmentNumber) ? string.Empty : string.Format("{0}: {1}, {2}", ppi.ShipmentNumber, ppi.RecipientName, ppi.Weight);
				txtShipmentInfo.Width = 200;
				txtShipmentInfo.Margin = new Padding(2, 2, 0, 2);
				pnl.Controls.Add(txtShipmentInfo);
				
				Button btnSearchByRecipientPinyinName = new Button();
				btnSearchByRecipientPinyinName.Tag = ppi;
				btnSearchByRecipientPinyinName.Size = new Size(txtShipmentInfo.Height, txtShipmentInfo.Height);
				btnSearchByRecipientPinyinName.Margin = new Padding(0, 2, 2, 2);
				btnSearchByRecipientPinyinName.Click += new EventHandler(btnSearchByRecipientPinyinName_Click);
				btnSearchByRecipientPinyinName.Enabled = !string.IsNullOrEmpty(ppi.ShipmentNumber);
				pnl.Controls.Add(btnSearchByRecipientPinyinName);

				LinkLabel lblPdfFilename = new LinkLabel();
				lblPdfFilename.BorderStyle = BorderStyle.Fixed3D;
				lblPdfFilename.Font = this.Font;
				//lblPdfFilename.ForeColor = this.ForeColor;
				lblPdfFilename.Tag = ppi;
				lblPdfFilename.Text = System.IO.Path.GetFileName(ppi.Filename);
				//lblPdfFilename.Cursor = Cursors.Hand;
				lblPdfFilename.AutoSize = false;
				lblPdfFilename.Size = new Size(100, txtShipmentInfo.Height);
				lblPdfFilename.TextAlign = ContentAlignment.MiddleLeft;
				lblPdfFilename.Margin = new Padding(2);
				lblPdfFilename.LinkClicked += new LinkLabelLinkClickedEventHandler(lblPdfFilename_LinkClicked);
				pnl.Controls.Add(lblPdfFilename);

				TextBox txtRecipientNameInPackingList = new TextBox();
				txtRecipientNameInPackingList.Font = this.Font;
				txtRecipientNameInPackingList.ForeColor = this.ForeColor;
				txtRecipientNameInPackingList.Text = ppi.MatchedRecipientName;
				txtRecipientNameInPackingList.Width = 60;
				txtRecipientNameInPackingList.Margin = new Padding(2, 2, 0, 2);
				pnl.Controls.Add(txtRecipientNameInPackingList);

				Button btnSearchByRecipientName = new Button();
				btnSearchByRecipientName.Tag = ppi;
				btnSearchByRecipientName.Size = new Size(txtRecipientNameInPackingList.Height, txtRecipientNameInPackingList.Height);
				btnSearchByRecipientName.Margin = new Padding(0, 2, 2, 2);
				btnSearchByRecipientName.Click += new EventHandler(btnSearchByRecipientName_Click);
				btnSearchByRecipientName.Enabled = (!string.IsNullOrEmpty(ppi.MatchedRecipientName));
				pnl.Controls.Add(btnSearchByRecipientName);

				Label lblUpdated = new Label();
				lblUpdated.Font = this.Font;
				lblUpdated.AutoSize = true;
				if (string.IsNullOrEmpty(ppi.MatchedRecipientName))
				{
					lblUpdated.Text = "Mismatched";
					lblUpdated.ForeColor = Color.Gray;
				}
				else
				{
					lblUpdated.Text = ppi.Updated ? "Updated" : "Failed";
					lblUpdated.ForeColor = ppi.Updated ? Color.Green : Color.Red;
				}
				lblUpdated.Margin = new Padding(2, 6, 2, 0);
				pnl.Controls.Add(lblUpdated);

				pnlMain.Controls.Add(pnl);
			}
		}

		void lblPdfFilename_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(((PdfPacketInfoEx)((LinkLabel)sender).Tag).Filename);
		}

		void btnSearchByRecipientName_Click(object sender, EventArgs e)
		{
			if (null == this.OnSearch)
				return;
			
			PdfPacketInfoEx ppi = (PdfPacketInfoEx)((Button)sender).Tag;
			if (!string.IsNullOrEmpty(ppi.ShipmentNumber))
			{
				Clipboard.SetText(ppi.ShipmentNumber);
				ClipboardPromptForm cpf = new ClipboardPromptForm("µ¥ºÅ", ppi.ShipmentNumber);
				cpf.Show(this);
				cpf.Location = new Point(this.Location.X + (this.Width - cpf.Width)/2, this.Location.Y + (this.Height-cpf.Height)/2);
				Application.DoEvents();
			}
			
			this.OnSearch(ppi.MatchedRecipientName.Replace(" ", string.Empty).Trim());
		}

		void btnSearchByRecipientPinyinName_Click(object sender, EventArgs e)
		{
			if (null == this.OnSearch)
				return;
				
			PdfPacketInfoEx ppi = (PdfPacketInfoEx)((Button)sender).Tag;
			if (!string.IsNullOrEmpty(ppi.ShipmentNumber))
			{
				Clipboard.SetText(ppi.ShipmentNumber);
				ClipboardPromptForm cpf = new ClipboardPromptForm("µ¥ºÅ", ppi.ShipmentNumber);
				cpf.Show(this);
				cpf.Location = new Point(this.Location.X + (this.Width - cpf.Width)/2, this.Location.Y + (this.Height-cpf.Height)/2);
				Application.DoEvents();
			}
			
			this.OnSearch(ppi.RecipientName.Replace(" ", string.Empty).Trim());
		}
	}
}