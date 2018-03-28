using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Data.OleDb;

namespace Yunda
{
	// 从韵达系统导出的订单数据.
	// 只需要订单编号(导入时提供的, 对应淘宝订单编号)和运单号.
	// 主要用于找到对应的淘宝订单, 点发货.
	public class YdExportedOrder
	{
		private string _orderId;
		private string _trackingNumber;
		
		public YdExportedOrder(string orderId, string trackingNumber)
		{
			_orderId = orderId;
			_trackingNumber = trackingNumber;
		}
		
		public string OrderId
		{
			get { return _orderId; }
		}
		
		public string TrackingNumber
		{
			get { return _trackingNumber; }
		}
		
		public static List<YdExportedOrder> LoadXls(string filename)
		{
			if (string.IsNullOrEmpty(filename))
				return null;
			if (!System.IO.File.Exists(filename))
				return null;
			
			Excel excel = new Excel(filename, Excel.OledbVersions.OLEDB40);
			try
			{
				List<string> tableNames = excel.GetTableNames();
				if (null == tableNames || tableNames.Count <= 0)
					return null;
				
				DataSet ds = excel.Get(tableNames[0], string.Empty);
				
				int orderIdIndex = 0, trackingNumberIndex = 0;
				for (int i = 0; i < ds.Tables[0].Rows[0].ItemArray.Length; i++)
				{
					if (ds.Tables[0].Rows[0][i].ToString().Equals("客户订单号"))
						orderIdIndex = i;
					if (ds.Tables[0].Rows[0][i].ToString().Equals("运单号"))
						trackingNumberIndex = i;
					if (0 != orderIdIndex && 0!= trackingNumberIndex)
						break;
				}
				
				// invalid excel file of yunda exported orders.
				if (0 == orderIdIndex || 0 == trackingNumberIndex)
					return null;
				
				List<YdExportedOrder> ydExportedOrders = new List<YdExportedOrder>();
				for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
				{
					DataRow row = ds.Tables[0].Rows[i];
					ydExportedOrders.Add(new YdExportedOrder(row[orderIdIndex].ToString(), row[trackingNumberIndex].ToString()));
				}
				
				return ydExportedOrders;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.ToString());
			}
			finally
			{
				excel.Close();
			}
			
			return null;
		}

		public static List<YdExportedOrder> LoadCsv(string filename)
		{
			if (string.IsNullOrEmpty(filename))
				return null;
			if (!System.IO.File.Exists(filename))
				return null;
			
			StreamReader reader = new StreamReader(filename, Encoding.Default);

			try
			{
				string[] heads = reader.ReadLine().Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
				int orderIdIndex = 0, trackingNumberIndex = 0;
				for (int i = 0; i < heads.Length; i++)
				{
				    if (heads[i].Equals("客户订单号"))
				        orderIdIndex = i;
				    if (heads[i].ToString().Equals("运单号"))
				        trackingNumberIndex = i;
				    if (0 != orderIdIndex && 0!= trackingNumberIndex)
				        break;
				}
				
				// invalid excel file of yunda exported orders.
				if (0 == orderIdIndex || 0 == trackingNumberIndex)
				    return null;
				
				List<YdExportedOrder> ydExportedOrders = new List<YdExportedOrder>();
				while (!reader.EndOfStream)
				{
					string[] datas = reader.ReadLine().Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
					for (int i = 0; i < datas.Length; i++)
					{
						if (datas[i].StartsWith("=\""))
							datas[i] = datas[i].Substring(1, datas[i].Length - 1);
						if (datas[i].StartsWith("\"") && datas[i].EndsWith("\""))
							datas[i] = datas[i].Substring(1, datas[i].Length - 2);
					}
					
					string[] orderIds = datas[orderIdIndex].Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
					
					foreach (string orderId in orderIds)
						ydExportedOrders.Add(new YdExportedOrder(orderId, datas[trackingNumberIndex]));
				}
				
				return ydExportedOrders;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.WriteLine(ex.ToString());
			}
			finally
			{
				reader.Close();
			}
			
			return null;
		}
	}
}