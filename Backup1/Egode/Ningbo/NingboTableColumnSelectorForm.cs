using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode.Ningbo
{
	public partial class NingboTableColumnSelectorForm : Form
	{
		private Excel _ningboExcel; // Excel�ĵ�1���Ǳ�ͷ. ��HDR=true
		private Ningbo.NingboTableColumnInfo _colInfo;
	
		public NingboTableColumnSelectorForm(Excel ningboExcel)
		{
			_ningboExcel = ningboExcel;
			InitializeComponent();
		}
		
		public Ningbo.NingboTableColumnInfo ColumnInfo
		{
			get { return _colInfo; }
		}

		private void NingboTableColumnSelectorForm_Load(object sender, EventArgs e)
		{
			foreach (Control c in pnlProperties.Controls)
			{
				if (!c.GetType().Equals(typeof(ComboBox)))
					continue;
				((ComboBox)c).Items.Add("Unknown");
				((ComboBox)c).SelectedIndex = 0;
			}

			if (null == _ningboExcel)
				return;
			
			List<string> tableNames = _ningboExcel.GetTableNames();
			if (null == tableNames || tableNames.Count <= 0)
				return;

			foreach (string tableName in tableNames)
			{
				FlowLayoutPanel pnl = new FlowLayoutPanel();
				pnl.BackColor = Color.White;
				pnl.BorderStyle = BorderStyle.FixedSingle;
				pnl.AutoScroll = true;
				pnl.WrapContents = false;

				TabPage tp = new TabPage(tableName);
				tc.TabPages.Add(tp);
				tp.Controls.Add(pnl);
				pnl.Dock = DockStyle.Fill;

				DataSet ds = _ningboExcel.Get(tableName, string.Empty);
				foreach (DataColumn col in ds.Tables[0].Columns)
				{
					if (col.Caption.Equals("F"+(col.Ordinal+1).ToString()))
						continue;
				
					Label lbl = new Label();
					lbl.AutoSize = true;
					lbl.Margin = new Padding(3, 3, 3, 3);
					lbl.Text = col.ColumnName;
					lbl.BackColor = Color.LightGray;
					pnl.Controls.Add(lbl);
					
					foreach (Control c in pnlProperties.Controls)
					{
						if (!c.GetType().Equals(typeof(ComboBox)))
							continue;
						((ComboBox)c).Items.Add(col.ColumnName);
					}
				}
			}
			
			// try to match.
			for (int i = 1; i < cboOrderId.Items.Count; i++)
			{
				if (cboOrderId.Items[i].ToString().Contains("������"))
					cboOrderId.SelectedIndex = i;
				if (cboOrderId.Items[i].ToString().Contains("��ݹ�˾"))
					cboLogisticsCompany.SelectedIndex = i;
				if (cboOrderId.Items[i].ToString().Contains("��ݵ���"))
					cboMailNumber.SelectedIndex = i;
				if (cboOrderId.Items[i].ToString().Contains("����"))
					cboRecipientName.SelectedIndex = i;
				if (cboOrderId.Items[i].ToString().Contains("�ֻ�"))
					cboMobile.SelectedIndex = i;
				if (cboOrderId.Items[i].ToString().Contains("ʡ"))
					cboProvince.SelectedIndex = i;
				if (cboOrderId.Items[i].ToString().Contains("��"))
					cboCity.SelectedIndex = i;
				if (cboOrderId.Items[i].ToString().Contains("��"))
					cboDistrict.SelectedIndex = i;
				if (cboOrderId.Items[i].ToString().Contains("��ַ"))
					cboStreetAddr.SelectedIndex = i;
				if (cboOrderId.Items[i].ToString().Contains("����"))
					cboProductCode.SelectedIndex = i;
				if (cboOrderId.Items[i].ToString().Contains("����"))
					cboCount.SelectedIndex = i;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			_colInfo = new NingboTableColumnInfo();
			_colInfo.OrderId = cboOrderId.SelectedIndex - 1;
			_colInfo.LogisticsCompany= cboLogisticsCompany.SelectedIndex - 1;
			_colInfo.MailNumber = cboMailNumber.SelectedIndex - 1;
			_colInfo.RecipientName = cboRecipientName.SelectedIndex - 1;
			_colInfo.Mobile = cboMobile.SelectedIndex - 1;
			_colInfo.Province = cboProvince.SelectedIndex - 1;
			_colInfo.City = cboCity.SelectedIndex - 1;
			_colInfo.District = cboDistrict.SelectedIndex - 1;
			_colInfo.StreetAddr = cboStreetAddr.SelectedIndex - 1;
			_colInfo.ProductNingboCode = cboProductCode.SelectedIndex - 1;
			_colInfo.Count = cboCount.SelectedIndex - 1;

			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}