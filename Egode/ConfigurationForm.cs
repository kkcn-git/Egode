using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class ConfigurationForm : Form
	{
		public class ShipmentCompanyItem
		{
			private OrderLib.ShipmentCompanies _sc;
			public ShipmentCompanyItem(OrderLib.ShipmentCompanies sc)
			{
				_sc = sc;
			}
			
			public OrderLib.ShipmentCompanies ShipmentCompany
			{
				get { return _sc; }
			}

			public override string ToString()
			{
				switch (_sc)
				{
					case OrderLib.ShipmentCompanies.Sf:
						return "˳��";
					case OrderLib.ShipmentCompanies.Zto:
						return "��ͨ";
					case OrderLib.ShipmentCompanies.Yto:
						return "Բͨ";
					case OrderLib.ShipmentCompanies.Yunda:
						return "�ϴ�";
					case OrderLib.ShipmentCompanies.Best:
						return "����";
					case OrderLib.ShipmentCompanies.EMS:
						return "����EMS";
				}
				return string.Empty;
			}
		}
	
		public ConfigurationForm()
		{
			InitializeComponent();
		}

		private void ConfigurationForm_Load(object sender, EventArgs e)
		{
			// Shipment rules.
			rdoAutoSelectShipment.Checked = Settings.Instance.AutoSelectShipment;
			rdoAlwaysSameShipment.Checked = !Settings.Instance.AutoSelectShipment;
			cboShipmentCompanies.Enabled = rdoAlwaysSameShipment.Checked;
			
			// Shipment companies.
			cboShipmentCompanies.Items.Add(new ShipmentCompanyItem(OrderLib.ShipmentCompanies.Sf));
			cboShipmentCompanies.Items.Add(new ShipmentCompanyItem(OrderLib.ShipmentCompanies.EMS));
			cboShipmentCompanies.Items.Add(new ShipmentCompanyItem(OrderLib.ShipmentCompanies.Yunda));
			cboShipmentCompanies.Items.Add(new ShipmentCompanyItem(OrderLib.ShipmentCompanies.Zto));
			cboShipmentCompanies.Items.Add(new ShipmentCompanyItem(OrderLib.ShipmentCompanies.Yto));
			
			int selectedIndex = 0;
			for (int i = 0; i <= cboShipmentCompanies.Items.Count; i++)
			{
				if (((ShipmentCompanyItem)cboShipmentCompanies.Items[i]).ShipmentCompany == Settings.Instance.DefaultShipment)
				{
					selectedIndex = i;
					break;
				}
			}
			cboShipmentCompanies.SelectedIndex = selectedIndex;
		}

		private void rdoAlwaysSameShipment_CheckedChanged(object sender, EventArgs e)
		{
			cboShipmentCompanies.Enabled = rdoAlwaysSameShipment.Checked;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Settings.Instance.AutoSelectShipment = rdoAutoSelectShipment.Checked;
			Settings.Instance.DefaultShipment = ((ShipmentCompanyItem)cboShipmentCompanies.SelectedItem).ShipmentCompany;

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}