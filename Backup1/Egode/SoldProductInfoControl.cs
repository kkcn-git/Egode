using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Egode
{
	public partial class SoldProductInfoControl : UserControl
	{
		private class ProductInfoItem
		{
			private ProductInfo _pi;
			public ProductInfoItem(ProductInfo pi)
			{
				_pi = pi;
			}
			
			public ProductInfo ProductInfo
			{
				get { return _pi; }
			}

			public override string ToString()
			{
				string brand = string.Empty;
				BrandInfo bi = BrandInfo.GetBrand(_pi.BrandId);
				if (null != bi)
					brand = bi.Name;
				
				string productFullName = string.IsNullOrEmpty(_pi.Name) ? _pi.ShortName : _pi.Name;
				if (!string.IsNullOrEmpty(brand) && !brand.Equals("Misc") && !productFullName.Trim().StartsWith(brand))
					productFullName = string.Format("{0} {1}", brand, productFullName);
				
				return productFullName;
			}
		}
	
		public event EventHandler OnRemove;
		public event EventHandler OnProductChanged;
		public event EventHandler OnCountChanged;
		
		// 仅显示宁波保税区支持的产品. 即NingboCode不为空.
		private bool _showOnlyNingboProducts; 
	
		public SoldProductInfoControl(SoldProductInfo initialSoldProductInfo, bool showOnlyNingboProducts)
		{
			InitializeComponent();

			ProductInfo piEmpty = new ProductInfo("0", "0", string.Empty, "0", "0", "选择1个产品", 0, string.Empty, "选择1个产品", string.Empty, false);
			cboProducts.Items.Add(new ProductInfoItem(piEmpty));
			cboProducts.SelectedIndex = 0;

			foreach (ProductInfo pi in ProductInfo.Products)
			{
				if (showOnlyNingboProducts && string.IsNullOrEmpty(pi.NingboId))
					continue;
				cboProducts.Items.Add(new ProductInfoItem(pi));
			}
			
			if (null != initialSoldProductInfo)
			{
				foreach (ProductInfoItem pii in cboProducts.Items)
				{
					if (pii.ProductInfo.Id.Equals(initialSoldProductInfo.Id))
					{
						cboProducts.SelectedItem = pii;
						break;
					}
				}

				nudCount.Value = initialSoldProductInfo.Count;
			}

			SoldProductInfoControl_SizeChanged(this, EventArgs.Empty);
		}
		
		public ProductInfo SelectedProductInfo
		{
			get 
			{ 
				if (null == cboProducts.SelectedItem)
					return null;
				return ((ProductInfoItem)cboProducts.SelectedItem).ProductInfo; 
			}
		}
		
		public int Count
		{
			get { return (int)nudCount.Value;}
		}
		
		public void SetSelectedProduct(string productId)
		{
			foreach (ProductInfoItem pii in cboProducts.Items)
			{
				if (pii.ProductInfo.Id.Equals(productId))
				{
					cboProducts.SelectedItem = pii;
					break;
				}
			}
		}

		private void cboProducts_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (null != this.OnProductChanged)
				this.OnProductChanged(this, EventArgs.Empty);
		}

		private void cboProducts_TextChanged(object sender, EventArgs e)
		{
			System.Diagnostics.Trace.WriteLine("");
			cboProducts.ForeColor = (null == cboProducts.SelectedItem ? Color.Red : Color.FromArgb(0xff, 0x40, 0x40, 0x40));
		}

		private void nudCount_ValueChanged(object sender, EventArgs e)
		{
			if (nudCount.Value <= 0)
				nudCount.ForeColor = Color.Red;
			else
				nudCount.ForeColor = Color.FromArgb(0xff, 0x60, 0x60, 0x60);
		
			if (null != this.OnCountChanged)
				this.OnCountChanged(this, EventArgs.Empty);
		}

		private void tsbtnRemove_Click(object sender, EventArgs e)
		{
			if (null != this.OnRemove)
				this.OnRemove(this, EventArgs.Empty);
		}

		private void SoldProductInfoControl_SizeChanged(object sender, EventArgs e)
		{
			cboProducts.Width = this.Width - ts.Margin.Right - ts.Width - ts.Margin.Left - nudCount.Margin.Right - nudCount.Width - nudCount.Margin.Left- lblX.Margin.Right - lblX.Width - lblX.Margin.Left - cboProducts.Margin.Right - cboProducts.Margin.Left;
		}
		
		void SoldProductInfoControl_Enter(object sender, EventArgs e)
		{
			cboProducts.Focus();
		}

	}
}