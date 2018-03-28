using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class StockActionForm : Form
	{
		private bool _stockout=true;
		private static int _selectedBrand=0;
		
		private bool _loading = true;
		private string _defaultSelectedProductId;

		public StockActionForm(bool stockout, string selectedBrandId, string selectedProductId)
		{
			_stockout = stockout;
			InitializeComponent();

			this.Text += _stockout ? " - stockout" : " - stockin";
			nudCount.BackColor = _stockout ? Color.LightCoral : Color.LightGreen;

			foreach (BrandInfo b in BrandInfo.Brands)
			{
				if (GetProductCount(b.Id) <= 0)
					continue;
				cboBrands.Items.Add(b);
			}
			
			_defaultSelectedProductId = selectedProductId;

			if (string.IsNullOrEmpty(selectedBrandId))
			{
				cboBrands.SelectedIndex = _selectedBrand;
			}
			else
			{
				foreach (BrandInfo b in cboBrands.Items)
				{
					if (b.Id.Equals(selectedBrandId))
					{
						cboBrands.SelectedIndex = cboBrands.Items.IndexOf(b);
						break;
					}
				}
			}

			_loading = false;
		}

		public StockActionForm(bool stockout) : this(stockout, string.Empty, string.Empty)
		{
			_stockout = stockout;
			InitializeComponent();
		}

		private void StockActionForm_Load(object sender, EventArgs e)
		{
		}
		
		private int GetProductCount(string brandId)
		{
			int c = 0;
			foreach (ProductInfo p in ProductInfo.Products)
			{
				if (p.BrandId.Equals(brandId))
					c++;
			}
			return c;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void cboBrands_SelectedIndexChanged(object sender, EventArgs e)
		{
			_selectedBrand = cboBrands.SelectedIndex;
			cboProducts.Items.Clear();
			foreach (ProductInfo p in ProductInfo.Products)
			{
				if (p.BrandId.Equals(((BrandInfo)cboBrands.SelectedItem).Id))
					cboProducts.Items.Add(p);
			}
			
			if (_loading && !string.IsNullOrEmpty(_defaultSelectedProductId))
			{
				foreach (ProductInfo p in cboProducts.Items)
				{
					if (p.Id.Equals(_defaultSelectedProductId))
					{
						cboProducts.SelectedIndex = cboProducts.Items.IndexOf(p);
						break;
					}
				}
			}
			else
			{
				cboProducts.SelectedIndex = 0;
			}
		}
		
		public ProductInfo Product
		{ 
			get { return (ProductInfo)cboProducts.SelectedItem; }
		}
		
		public int Count
		{
			get { return (int)nudCount.Value; }
		}
		
		public string FromTo
		{
			get { return txtFromTo.Text; }
			set { txtFromTo.Text = value; }
		}

		public string Comment
		{
			get { return txtComment.Text; }
			set { txtComment.Text = value; }
		}
	}
}