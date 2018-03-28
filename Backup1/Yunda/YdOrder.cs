using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace Yunda
{
	// 韵达订单.
	// 1个韵达订单对应导入韵达打印系统表格的1行.
	public class YdOrder
	{
		private string _senderName = "顾双双";
		private string _senderMobile = "18964913317";
		private string _senderAddr = "迎春路678号";
		private string _senderCompany = "bu***洲";
		private string _recipientName;
		private string _recipientPhone;
		private string _recipientMobile;
		private string _recipientFullAddr;
		private string _orderId; // 订单编号, 可以自行任意定义.
		private string _productsInfo; // 这里用了表格中的"订单编号"列, 订单编号在面单上占了很大1块空白, 因此按照韵达的建议, 使用这1区域来打印产品信息.
		
		// 运单号.
		// 按照标准流程, 应该是导入订单到韵达系统后才能知道每个订单的运单号.
		// 为了我们自己的系统操作方便, 每次处理订单前, 从韵达运单号库存中获得第1个可用的运单号, 然后自动递增.
		// 这个运单号不会填入将来导入到韵达系统的excel文件中.
		private string _billNumber;

		public YdOrder(string recipientName, string recipientPhone, string recipientMobile, string recipientFullAddr, 
		               string orderId, string productsInfo, 
		               string senderName, string senderMobile, string senderAddr, string senderCompany)
		{
			_recipientName = recipientName;
			_recipientPhone = recipientPhone;
			_recipientMobile = recipientMobile;
			_recipientFullAddr = recipientFullAddr;
			_orderId = orderId;
			_productsInfo = productsInfo;
			
			if (!string.IsNullOrEmpty(senderName)) // required and cannot be empty.
				_senderName = senderName;
			if (!string.IsNullOrEmpty(senderMobile)) // required and cannot be empty.
				_senderMobile = senderMobile;
			if (!string.IsNullOrEmpty(senderAddr)) // required and cannot be empty.
				_senderAddr = senderAddr;
			_senderCompany = senderCompany;
		}

		public string RecipientName
		{
			get { return _recipientName; }
		}

		public string RecipientPhone
		{
			get { return _recipientPhone; }
		}

		public string RecipientMobile
		{
			get { return _recipientMobile; }
		}

		public string RecipientFullAddress
		{
			get { return _recipientFullAddr; }
		}

		public string OrderId
		{
			get { return _orderId; }
		}

		public string ProductsInfo
		{
			get { return _productsInfo; }
		}
		
		public string SenderName
		{
			get { return _senderName; }
		}
		
		public string SenderMobile
		{
			get { return _senderMobile; }
		}
		
		public string SenderAddress
		{
			get { return _senderAddr; }
		}
		
		public string SenderCompany
		{
			get { return _senderCompany; }
		}
		
		public static int ExportYundaOrders(List<YdOrder> ydOrders, string templateFilename, string outputFilename)
		{
			if (null == ydOrders)
				return -1;
			
			System.IO.File.Copy(templateFilename, outputFilename);
			Excel excel = null;
			try
			{
				excel = new Excel(outputFilename, Excel.OledbVersions.OLEDB40);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.ToString());
				System.IO.File.Delete(outputFilename);
			}

			if (null == excel)
				return ExportYundaOrdersCsv(ydOrders, outputFilename);

			try
			{
				foreach (YdOrder o in ydOrders)
				{
					string values = string.Format(
						"'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', '{22}'",
						o.OrderId, string.Empty, string.Empty, string.Empty, 
						o.SenderName, string.Empty, o.SenderMobile, string.Empty, o.SenderAddress, string.Empty, o.SenderCompany, 
						o.ProductsInfo, // Environment.NewLine + o.ProductsInfo,
						o.RecipientName, o.RecipientPhone, o.RecipientMobile, string.Empty, o.RecipientFullAddress, 
						string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
					excel.Insert("Sheet1", string.Empty, values);
				}
				
				return ydOrders.Count;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.ToString());
			}
			finally
			{
				excel.Close();
			}
			return -1;
		}
		
		// Added by KK on 2017/11/08.
		// Generate .xls file using Office.Interop.
		// The template file is not required any more.
		public static int ExportYundaOrders(List<YdOrder> ydOrders, string outputFilename)
		{
			if (null == ydOrders)
				return -1;
			
			Microsoft.Office.Interop.Excel.ApplicationClass excelApp = new ApplicationClass();
			
			try
			{
				Workbook wk = excelApp.Workbooks.Add(Type.Missing);
				Worksheet ws = (Worksheet)wk.ActiveSheet;
				
				// head.
				ws.Cells[1, 1] = "订单号 *";
				ws.Cells[1, 2] = "重量";
				ws.Cells[1, 3] = "商品信息";
				ws.Cells[1, 4] = "备注";
				ws.Cells[1, 5] = "发件人姓名 *";
				ws.Cells[1, 6] = "发件人电话";
				ws.Cells[1, 7] = "发件人手机 *";
				ws.Cells[1, 8] = "发件人省市区 ";
				ws.Cells[1, 9] = "发件人详细地址 *";
				ws.Cells[1, 10] = "发件人邮编";
				ws.Cells[1, 11] = "发件人公司";
				ws.Cells[1, 12] = "自定义区域1";
				ws.Cells[1, 13] = "收件人姓名 *";
				ws.Cells[1, 14] = "收件人电话";
				ws.Cells[1, 15] = "收件人手机 *";
				ws.Cells[1, 16] = "收件人省市区 ";
				ws.Cells[1, 17] = "收件人详细地址 *";
				ws.Cells[1, 18] = "收件人邮编";
				ws.Cells[1, 19] = "收件人公司";
				ws.Cells[1, 20] = "自定义区域2";
				ws.Cells[1, 21] = "波次号";
				ws.Cells[1, 22] = "订单类型";
				ws.Cells[1, 23] = "到付金额";
				
				((Range)ws.Columns[1, Type.Missing]).ColumnWidth = 20;
				((Range)ws.Columns[5, Type.Missing]).ColumnWidth = 12;
				((Range)ws.Columns[7, Type.Missing]).ColumnWidth = 12;
				((Range)ws.Columns[9, Type.Missing]).ColumnWidth = 14;
				((Range)ws.Columns[11, Type.Missing]).ColumnWidth = 10;
				((Range)ws.Columns[12, Type.Missing]).ColumnWidth = 32;
				((Range)ws.Columns[13, Type.Missing]).ColumnWidth = 12;
				((Range)ws.Columns[15, Type.Missing]).ColumnWidth = 12;
				((Range)ws.Columns[17, Type.Missing]).ColumnWidth = 80;
				
				// fill data.
				for (int i = 0; i < ydOrders.Count; i++)
				{
					ws.Cells[i + 2, 1] = ydOrders[i].OrderId;
					ws.Cells[i + 2, 5] = ydOrders[i].SenderName;
					ws.Cells[i + 2, 7] = ydOrders[i].SenderMobile;
					ws.Cells[i + 2, 9] = ydOrders[i].SenderAddress;
					ws.Cells[i + 2, 11] = ydOrders[i].SenderCompany;
					ws.Cells[i + 2, 12] = ydOrders[i].ProductsInfo;
					ws.Cells[i + 2, 13] = ydOrders[i].RecipientName;
					ws.Cells[i + 2, 14] = ydOrders[i].RecipientPhone;
					ws.Cells[i + 2, 15] = ydOrders[i].RecipientMobile;
					ws.Cells[i + 2, 17] = ydOrders[i].RecipientFullAddress;
				}
				
				wk.Close(true, outputFilename, Type.Missing);
				return ydOrders.Count;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.ToString());
			}
			finally
			{
				excelApp.Quit();
			}
			return -1;
		}
		
		public static int ExportYundaOrdersCsv(List<YdOrder> ydOrders, string outputFilename)
		{
			if (null == ydOrders)
				return -1;
			
			if (!outputFilename.EndsWith(".csv"))
				outputFilename += ".csv";
			
			using (System.IO.StreamWriter writer = new System.IO.StreamWriter(outputFilename, false, Encoding.Default))
			{
				// head.
				string head = string.Format(
					"{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22}",
					"订单号 *", "重量", "商品信息", "备注", 
					"发件人姓名 *", "发件人电话", "发件人手机 *", "发件人省市区 ", "发件人详细地址 *", "发件人邮编", "发件人公司",
					"自定义区域1", 
					"收件人姓名 *", "收件人电话", "收件人手机 *", "收件人省市区", "收件人详细地址 *", "收件人邮编", "收件人公司",
					"自定义区域2", "波次号", "订单类型", "到付金额");
				writer.WriteLine(head);

				// rows.
				foreach (YdOrder o in ydOrders)
				{
					string values = string.Format(
						"{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},\"{11}\",{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22}",
						o.OrderId, string.Empty, string.Empty, string.Empty, 
						o.SenderName, string.Empty, o.SenderMobile, string.Empty, o.SenderAddress, string.Empty, o.SenderCompany, 
						o.ProductsInfo, // Environment.NewLine + o.ProductsInfo,
						o.RecipientName, o.RecipientPhone, o.RecipientMobile, string.Empty, o.RecipientFullAddress, 
						string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
					writer.WriteLine(values);
				}
				
				return ydOrders.Count;
			}
			
			return -1;
		}
	}
}