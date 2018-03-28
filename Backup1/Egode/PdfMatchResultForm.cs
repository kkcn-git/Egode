//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Text;
//using System.Windows.Forms;

//namespace Egode
//{
//    public partial class PdfMatchResultForm : Form
//    {
//        public delegate void OnSearchEventHandler(string keyword);
//        public event OnSearchEventHandler OnSearch;
	
//        private List<PdfPacketInfoEx> _pdfPackets;
	
//        public PdfMatchResultForm(List<PdfPacketInfoEx> pdfPackets)
//        {
//            _pdfPackets = pdfPackets;
//            InitializeComponent();
//        }

//        private void PdfMatchResultForm_Load(object sender, EventArgs e)
//        {
//            if (null == _pdfPackets || _pdfPackets.Count <= 0)
//                return;
			
//            foreach (PdfPacketInfoEx ppi in _pdfPackets)
//            {
//                FlowLayoutPanel pnl = new FlowLayoutPanel();
//                pnl.AutoSize = true;
//                pnl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
//                pnl.FlowDirection = FlowDirection.LeftToRight;
//                pnl.WrapContents = false;
//                pnl.Margin = new Padding(0);
//                pnl.Click += new EventHandler(OnPdfItemClick);
				
//                Label lblIndex = new Label();
//                lblIndex.AutoSize = false;
//                lblIndex.Size = new Size(26, 21);
//                lblIndex.TextAlign = ContentAlignment.MiddleCenter;
//                lblIndex.Font = this.Font;
//                lblIndex.ForeColor = Color.FromArgb(0xff, 0x40, 0x40, 0x40);
//                lblIndex.Margin = new Padding(0, 2, 0, 0);
//                lblIndex.Text = (_pdfPackets.IndexOf(ppi) + 1).ToString("000");
//                pnl.Controls.Add(lblIndex);

//                LinkLabel lblPdfFilename = new LinkLabel();
//                lblPdfFilename.BorderStyle = BorderStyle.Fixed3D;
//                lblPdfFilename.Font = this.Font;
//                //lblPdfFilename.ForeColor = this.ForeColor;
//                lblPdfFilename.Tag = ppi;
//                lblPdfFilename.Text = System.IO.Path.GetFileName(ppi.Filename);
//                //lblPdfFilename.Cursor = Cursors.Hand;
//                lblPdfFilename.AutoSize = false;
//                lblPdfFilename.Size = new Size(130, 21);//txtShipmentInfo.Height);
//                lblPdfFilename.TextAlign = ContentAlignment.MiddleLeft;
//                lblPdfFilename.Margin = new Padding(2);
//                lblPdfFilename.LinkClicked += new LinkLabelLinkClickedEventHandler(lblPdfFilename_LinkClicked);
//                lblPdfFilename.Click += new EventHandler(OnPdfItemClick);
//                pnl.Controls.Add(lblPdfFilename);

//                TextBox txtShipmentInfo = new TextBox();
//                txtShipmentInfo.Font = this.Font;
//                txtShipmentInfo.ForeColor = this.ForeColor;
//                txtShipmentInfo.BackColor = ((ppi.Type == PacketTypes.Express) ? Color.Plum : Color.FromKnownColor(KnownColor.Window));
//                txtShipmentInfo.Text = string.IsNullOrEmpty(ppi.ShipmentNumber) ? string.Empty : string.Format("[{0}] {1}: {2}, {3}", Packet.GetPacketTypePrefix(ppi.Type), ppi.ShipmentNumber, ppi.RecipientName, ppi.Weight);
//                txtShipmentInfo.Width = 190;
//                txtShipmentInfo.Margin = new Padding(2, 2, 0, 2);
//                txtShipmentInfo.Click += new EventHandler(OnPdfItemClick);
//                if (GetPdfNameCount(_pdfPackets, ppi.RecipientName) > 1)
//                    txtShipmentInfo.BackColor = Color.Tomato;
//                pnl.Controls.Add(txtShipmentInfo);

//                Button btnSearchByRecipientPinyinName = new Button();
//                btnSearchByRecipientPinyinName.Tag = ppi;
//                btnSearchByRecipientPinyinName.Size = new Size(txtShipmentInfo.Height, txtShipmentInfo.Height);
//                btnSearchByRecipientPinyinName.Margin = new Padding(0, 2, 2, 2);
//                btnSearchByRecipientPinyinName.Click += new EventHandler(OnPdfItemClick);
//                btnSearchByRecipientPinyinName.Click += new EventHandler(btnSearchByRecipientPinyinName_Click);
//                btnSearchByRecipientPinyinName.Enabled = !string.IsNullOrEmpty(ppi.ShipmentNumber);
//                pnl.Controls.Add(btnSearchByRecipientPinyinName);

//                TextBox txtRecipientNameInPackingList = new TextBox();
//                txtRecipientNameInPackingList.Font = this.Font;
//                txtRecipientNameInPackingList.ForeColor = this.ForeColor;
//                txtRecipientNameInPackingList.Text = ppi.MatchedRecipientName;
//                txtRecipientNameInPackingList.Width = 60;
//                txtRecipientNameInPackingList.Margin = new Padding(2, 2, 0, 2);
//                txtRecipientNameInPackingList.Click += new EventHandler(OnPdfItemClick);
//                if (GetNameCount(_pdfPackets, ppi.MatchedRecipientName) > 1)
//                    txtRecipientNameInPackingList.BackColor = Color.Tomato;
//                pnl.Controls.Add(txtRecipientNameInPackingList);

//                Button btnSearchByRecipientName = new Button();
//                btnSearchByRecipientName.Tag = ppi;
//                btnSearchByRecipientName.Size = new Size(txtRecipientNameInPackingList.Height, txtRecipientNameInPackingList.Height);
//                btnSearchByRecipientName.Margin = new Padding(0, 2, 2, 2);
//                btnSearchByRecipientName.Click += new EventHandler(OnPdfItemClick);
//                btnSearchByRecipientName.Click += new EventHandler(btnSearchByRecipientName_Click);
//                btnSearchByRecipientName.Enabled = (!string.IsNullOrEmpty(ppi.MatchedRecipientName));
//                pnl.Controls.Add(btnSearchByRecipientName);

//                Label lblUpdated = new Label();
//                lblUpdated.Font = this.Font;
//                lblUpdated.AutoSize = true;
//                if (string.IsNullOrEmpty(ppi.MatchedRecipientName))
//                {
//                    lblUpdated.Text = "Mismatched";
//                    lblUpdated.ForeColor = Color.Gray;
//                }
//                else
//                {
//                    lblUpdated.Text = ppi.Updated ? "Updated" : "Failed";
//                    lblUpdated.ForeColor = ppi.Updated ? Color.Green : Color.Red;
//                }
//                lblUpdated.Margin = new Padding(2, 6, 2, 0);
//                lblUpdated.Click += new EventHandler(OnPdfItemClick);
//                pnl.Controls.Add(lblUpdated);
				
//                PictureBox picSelectedMark = new PictureBox();
//                picSelectedMark.Size = new Size(4, txtRecipientNameInPackingList.Height-2);
//                picSelectedMark.BackColor = Color.Tomato;
//                picSelectedMark.Visible = false;
//                pnl.Controls.Add(picSelectedMark);

//                pnlMain.Controls.Add(pnl);
//            }
//        }

//        // name: chinese name.
//        private int GetNameCount(List<PdfPacketInfoEx> pdfPackets, string name)
//        {
//            if (null == pdfPackets || pdfPackets.Count <= 0)
//                return 0;

//            int c = 0;
//            foreach (PdfPacketInfoEx p in pdfPackets)
//            {
//                if (!string.IsNullOrEmpty(p.MatchedRecipientName) && p.MatchedRecipientName.Equals(name))
//                    c++;
//            }
//            return c;
//        }

//        // name: pinyin name from pdf.
//        private int GetPdfNameCount(List<PdfPacketInfoEx> pdfPackets, string name)
//        {
//            if (null == pdfPackets || pdfPackets.Count <= 0)
//                return 0;

//            int c = 0;
//            foreach (PdfPacketInfoEx p in pdfPackets)
//            {
//                if (!string.IsNullOrEmpty(p.RecipientName) && p.RecipientName.Equals(name))
//                    c++;
//            }
//            return c;
//        }

//        void OnPdfItemClick(object sender, EventArgs e)
//        {
//            Panel selectedPanel = null;
			
//            if (sender.GetType().Equals(typeof(FlowLayoutPanel)))
//                selectedPanel = sender as Panel;
//            else
//                selectedPanel = (Panel)((Control)sender).Parent;

//            foreach (Panel pnl in pnlMain.Controls)
//            {
//                if (pnl.Equals(selectedPanel))
//                    continue;
//                foreach (Control c in pnl.Controls)
//                {
//                    if (c.GetType().Equals(typeof(PictureBox)))
//                    {
//                        c.Visible = false;
//                        break;
//                    }
//                }
//            }

//            foreach (Control c in selectedPanel.Controls)
//            {
//                if (c.GetType().Equals(typeof(PictureBox)))
//                {
//                    c.Visible = true;
//                    break;
//                }
//            }
//        }

//        void lblPdfFilename_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
//        {
//            System.Diagnostics.Process.Start(((PdfPacketInfoEx)((LinkLabel)sender).Tag).Filename);
//        }

//        void btnSearchByRecipientName_Click(object sender, EventArgs e)
//        {
//            if (null == this.OnSearch)
//                return;
			
//            PdfPacketInfoEx ppi = (PdfPacketInfoEx)((Button)sender).Tag;
//            if (!string.IsNullOrEmpty(ppi.ShipmentNumber))
//            {
//                string shipmentNumberInfo = string.Format("[{0}] {1}", Packet.GetPacketTypePrefix(ppi.Type), ppi.ShipmentNumber);
//                SetCliboardText(shipmentNumberInfo);
//                Application.DoEvents();
//                ClipboardPromptForm cpf = new ClipboardPromptForm("µ¥ºÅ", shipmentNumberInfo);
//                cpf.Show(this);
//                cpf.Location = new Point(this.Location.X + (this.Width - cpf.Width)/2, this.Location.Y + (this.Height-cpf.Height)/2);
//                Application.DoEvents();
//            }
			
//            this.OnSearch(ppi.MatchedRecipientName.Replace(" ", string.Empty).Trim());
//        }

//        void btnSearchByRecipientPinyinName_Click(object sender, EventArgs e)
//        {
//            if (null == this.OnSearch)
//                return;
				
//            PdfPacketInfoEx ppi = (PdfPacketInfoEx)((Button)sender).Tag;
//            if (!string.IsNullOrEmpty(ppi.ShipmentNumber))
//            {
//                string shipmentNumberInfo = string.Format("[{0}] {1}", Packet.GetPacketTypePrefix(ppi.Type), ppi.ShipmentNumber);
//                SetCliboardText(shipmentNumberInfo);
//                Application.DoEvents();
//                ClipboardPromptForm cpf = new ClipboardPromptForm("µ¥ºÅ", shipmentNumberInfo);
//                cpf.Show(this);
//                cpf.Location = new Point(this.Location.X + (this.Width - cpf.Width)/2, this.Location.Y + (this.Height-cpf.Height)/2);
//                Application.DoEvents();
//            }
			
//            this.OnSearch(ppi.RecipientName.Replace(" ", string.Empty).Trim());
//        }
		
//        void SetCliboardText(string s)
//        {
//            bool ok = false;
//            while (!ok)
//            {
//                try
//                {
//                    Clipboard.SetText(s);
//                    ok = true;
//                }
//                catch
//                {
//                    Application.DoEvents();
//                }
//            }
//        }
//    }
//}