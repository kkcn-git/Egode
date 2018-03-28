using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode.Stock
{
	public partial class BrandProductForm : Form
	{
		public BrandProductForm()
		{
			InitializeComponent();
		}

		private void BrandProductForm_Load(object sender, EventArgs e)
		{
			foreach (BrandInfo b in BrandInfo.Brands)
				cboBrands.Items.Add(b);
			cboBrands.SelectedIndex = 0;
		}

		private void cboBrands_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtFreeIds.Text = string.Empty;
			for (int i = 1; i <= 128; i++)
			{
				ProductInfo pi = ProductInfo.GetProductInfo(string.Format("{0}-{1}", ((BrandInfo)cboBrands.SelectedItem).Id, i.ToString("0000")));
				if (null == pi)
					txtFreeIds.Text += string.Format("{0}-{1}\r\n", ((BrandInfo)cboBrands.SelectedItem).Id, i.ToString("0000"));
			}
		}
	}
}