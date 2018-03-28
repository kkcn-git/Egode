using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace Yunda
{
	// �ϴﶩ��.
	// 1���ϴﶩ����Ӧ�����ϴ��ӡϵͳ����1��.
	public class YdOrder
	{
		private string _senderName = "��˫˫";
		private string _senderMobile = "18964913317";
		private string _senderAddr = "ӭ��·678��";
		private string _senderCompany = "bu***��";
		private string _recipientName;
		private string _recipientPhone;
		private string _recipientMobile;
		private string _recipientFullAddr;
		private string _orderId; // �������, �����������ⶨ��.
		private string _productsInfo; // �������˱���е�"�������"��, ����������浥��ռ�˺ܴ�1��հ�, ��˰����ϴ�Ľ���, ʹ����1��������ӡ��Ʒ��Ϣ.
		
		// �˵���.
		// ���ձ�׼����, Ӧ���ǵ��붩�����ϴ�ϵͳ�����֪��ÿ���������˵���.
		// Ϊ�������Լ���ϵͳ��������, ÿ�δ�����ǰ, ���ϴ��˵��ſ���л�õ�1�����õ��˵���, Ȼ���Զ�����.
		// ����˵��Ų������뽫�����뵽�ϴ�ϵͳ��excel�ļ���.
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
				ws.Cells[1, 1] = "������ *";
				ws.Cells[1, 2] = "����";
				ws.Cells[1, 3] = "��Ʒ��Ϣ";
				ws.Cells[1, 4] = "��ע";
				ws.Cells[1, 5] = "���������� *";
				ws.Cells[1, 6] = "�����˵绰";
				ws.Cells[1, 7] = "�������ֻ� *";
				ws.Cells[1, 8] = "������ʡ���� ";
				ws.Cells[1, 9] = "��������ϸ��ַ *";
				ws.Cells[1, 10] = "�������ʱ�";
				ws.Cells[1, 11] = "�����˹�˾";
				ws.Cells[1, 12] = "�Զ�������1";
				ws.Cells[1, 13] = "�ռ������� *";
				ws.Cells[1, 14] = "�ռ��˵绰";
				ws.Cells[1, 15] = "�ռ����ֻ� *";
				ws.Cells[1, 16] = "�ռ���ʡ���� ";
				ws.Cells[1, 17] = "�ռ�����ϸ��ַ *";
				ws.Cells[1, 18] = "�ռ����ʱ�";
				ws.Cells[1, 19] = "�ռ��˹�˾";
				ws.Cells[1, 20] = "�Զ�������2";
				ws.Cells[1, 21] = "���κ�";
				ws.Cells[1, 22] = "��������";
				ws.Cells[1, 23] = "�������";
				
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
					"������ *", "����", "��Ʒ��Ϣ", "��ע", 
					"���������� *", "�����˵绰", "�������ֻ� *", "������ʡ���� ", "��������ϸ��ַ *", "�������ʱ�", "�����˹�˾",
					"�Զ�������1", 
					"�ռ������� *", "�ռ��˵绰", "�ռ����ֻ� *", "�ռ���ʡ����", "�ռ�����ϸ��ַ *", "�ռ����ʱ�", "�ռ��˹�˾",
					"�Զ�������2", "���κ�", "��������", "�������");
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