using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Egode
{
	public partial class PacketsForm : Form
	{
		public event EventHandler OnUpdateStatus;

		private const string SUPERMARKET_TEMPLATE_FILENAME = "template-supermarket.xls";
		private const string SUPERMARKET_EXCEL_OUTPUT_FILENAME = "包裹信息单-realkk.xls";

		private const string RAINBOW_TEMPLATE_FILENAME = "template-rainbow.xls";
		private const string RAINBOW_EXCEL_OUTPUT_FILENAME = "(realkk)彩虹国际代购2013表格.xls";

		private const string DEALWORTHIER_TEMPLATE_FILENAME = "template-dealworthier.xls";
		private const string DEALWORTHIER_EXCEL_OUTPUT_FILENAME = "DHL-realkk.xls";

		private const string OUHUA_TEMPLATE_FILENAME = "template-ouhua.xls";
		private const string OUHUA_EXCEL_OUTPUT_FILENAME = "realkk-{0}.xls"; // e.g.: realkk-20130712.xls

		private const string HANSLORD_CSV_OUTPUT_FILENAME = "hanslord-{0}.csv"; // e.g.: hanslord-20130712.csv
		private const string HANSLORD_DATEN_CN_FILENAME = "daten4cn.xls";
		private const string HANSLORD_DATEN_DE_FILENAME = "daten4de.xls";

		private const string PACKING_LIST_TEMPLATE = "template-packing-list.xls";
		private const string PACKING_LIST_OUTPUT_JHT = "发货清单-senderCompany-{0}.xls"; // {0}=yyyyMMdd
		private const string CHINESE_ADDRESSES_DOC = "中文地址-{0}.doc";

		private const string ADRESSE_IMPORT_IERUNG_FILENAME = "Adresseimportierung-{0}.csv"; // only for postnl.

		private List<PacketInfo> _packetInfos;
	
		public PacketsForm(List<PacketInfo> packetInfos)
		{
			_packetInfos = packetInfos;
			InitializeComponent();
		}

		private void PacketsForm_Load(object sender, EventArgs e)
		{
			foreach (PacketInfo pi in _packetInfos)
			{
				PacketDetailsControl pdc = new PacketDetailsControl(pi, pnlPackets.Controls.Count+1);
				pnlPackets.Controls.Add(pdc);
			}
		}

		private void tsbtnGo_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			
			try
			{
				//int zeroWeightCount = 0;
				//foreach (PacketDetailsControl pdc in pnlPackets.Controls)
				//{
				//    pdc.UpdatePacketInfo();
				//    OrderParser.Order o = MainForm.Instance.GetOrder(pdc.PacketInfo.OrderId);
				//    if (null == o)
				//        continue;
						
				//    if (pdc.PacketInfo.Weight <= 0)
				//    {
				//        zeroWeightCount++;
				//        if (null != o)
				//            o.LocalPrepared = false;
				//    }
				//    else
				//    {
				//        o.LocalPrepared = true;
				//    }
				//}
				
				//if (zeroWeightCount > 0)
				//{
				//    DialogResult dr = MessageBox.Show(
				//        this, 
				//        string.Format("有{0}个包裹单被设置为0重量, 将取消出单.\n", zeroWeightCount), this.Text, 
				//        MessageBoxIcon.Exclamation, MessageBoxButtons.YesNo);
				//}
				
				// 检查是否有报关信息没有设置. just for hanslord.
				foreach (PacketDetailsControl pdc in pnlPackets.Controls)
				{
					pdc.UpdatePacketInfo();
					if (pdc.PacketInfo.Type != PacketTypes.Hanslord)
						continue;
						
					if (null == pdc.PacketInfo.ExportItems || pdc.PacketInfo.ExportItems.Count <= 0)
					{
						MessageBox.Show(
							this, 
							"DPEE-EXPORT-ITEM is required for Hanslord packet.", this.Text,
							MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
				}
			
				List<PacketInfo> supermarkets = new List<PacketInfo>();
				List<PacketInfo> rainbows = new List<PacketInfo>();
				List<PacketInfo> dealworthiers = new List<PacketInfo>();
				List<PacketInfo> ouhuas = new List<PacketInfo>();
				List<PacketInfo> hanslords = new List<PacketInfo>();
			
				foreach (PacketDetailsControl pdc in pnlPackets.Controls)
				{
					pdc.UpdatePacketInfo();
					
					if (pdc.PacketInfo.Weight <= 0)
						continue;
											
					switch (pdc.PacketInfo.Type)
					{
						case PacketTypes.Supermarket:
						case PacketTypes.Time24_PostNL:
							supermarkets.Add(pdc.PacketInfo);
							break;
						case PacketTypes.Rainbow:
							rainbows.Add(pdc.PacketInfo);
							break;
						case PacketTypes.Dealworthier:
							dealworthiers.Add(pdc.PacketInfo);
							break;
						case PacketTypes.Ouhua:
							ouhuas.Add(pdc.PacketInfo);
							break;
						case PacketTypes.Hanslord:
							hanslords.Add(pdc.PacketInfo);
							break;
					}
				}

				// Create today folder
				string todayFolder = Path.Combine(
					Directory.GetParent(Application.ExecutablePath).FullName, 
					DateTime.Now.ToString("yyyy-MM-dd"));
					
				if (!Directory.Exists(todayFolder))
				{
					try
					{
						Directory.CreateDirectory(todayFolder);
					}
					catch (Exception ex)
					{
						MessageBox.Show(
							this, 
							"Create folder for today failed. \nThe output files will be saved in the same folder with .exe file.\nError:" + ex.Message, 
							this.Text, 
							MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						todayFolder = Directory.GetParent(Application.ExecutablePath).FullName;
					}
				}
				
				string f1=string.Empty, f2=string.Empty, f3=string.Empty, f4=string.Empty, f5=string.Empty, f6=string.Empty, f7=string.Empty, f8=string.Empty;
				
				if (supermarkets.Count > 0)
				{
					f1 = GenerateSupermarketFiles(supermarkets, todayFolder);
					GenerateOuhuaFiles(supermarkets, todayFolder);
					f8 = GenerateAdresseImportIerungFile(supermarkets, todayFolder);
				}
				if (rainbows.Count > 0)
					f2 = GenerateRainbowFiles(rainbows, todayFolder);
				if (dealworthiers.Count > 0)
					f3 = GenerateDealworthierFiles(dealworthiers, todayFolder);
				if (ouhuas.Count > 0)
					f4 = GenerateOuhuaFiles(ouhuas, todayFolder);
				if (hanslords.Count > 0)
					f5 = GenerateHanslordFiles(hanslords, todayFolder);

				f6 = GeneratePackingListExcelFile(_packetInfos, todayFolder);
				f7 = GenerateChineseAddressDoc(_packetInfos, todayFolder);
				GetPackgingResult(supermarkets, rainbows, dealworthiers, ouhuas, hanslords, f1, f2, f3, f4, f5, f6, f7, f8).ShowDialog(this);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
			}
			
			Cursor.Current = Cursors.Default;
		}
		
		private PacketResultForm GetPackgingResult(
			List<PacketInfo> supermarkets, List<PacketInfo> rainbows, List<PacketInfo> dealworthiers, List<PacketInfo> ouhuas, List<PacketInfo> hanslords,
			string supermarketFilename, string rainbowFilename, string dealworthierFilename, string ouhuaFilename, string hanslordFilename,
			string packingListFilename, string addressFilename, string addresseImportIerungFilename)
		{
			// 统计包裹数量
			int supermarketTotalPrice = 0;
			int[] supermarketWeights = new int[31000]; // 0kg ~ 30kg;
			foreach (PacketInfo pi in supermarkets)
			{
				supermarketWeights[pi.Weight]++; // 包裹单计数.
				supermarketTotalPrice += pi.Price;
			}

			int rainbowTotalPrice = 0;
			int[] rainbowWeights = new int[31000]; // 0kg ~ 30kg;
			foreach (PacketInfo pi in rainbows)
			{
				rainbowWeights[pi.Weight]++; // 包裹单计数.
				rainbowTotalPrice += pi.Price;
			}

			int dealworthierTotalPrice = 0;
			int[] dealworthierWeights = new int[31000]; // 0kg ~ 30kg;
			foreach (PacketInfo pi in dealworthiers)
			{
				dealworthierWeights[pi.Weight]++; // 包裹单计数.
				dealworthierTotalPrice += pi.Price;
			}

			int ouhuaTotalPrice = 0;
			int[] ouhuaWeights = new int[31000]; // 0kg ~ 30kg;
			foreach (PacketInfo pi in ouhuas)
			{
				ouhuaWeights[pi.Weight]++; // 包裹单计数.
				ouhuaTotalPrice += pi.Price;
			}

			int hanslordTotalPrice = 0;
			int[] hanslordWeights = new int[31000]; // 0kg ~ 30kg;
			foreach (PacketInfo pi in hanslords)
			{
				hanslordWeights[pi.Weight]++; // 包裹单计数.
				hanslordTotalPrice += pi.Price;
			}

			StringBuilder sbSupermarket = new StringBuilder();
			if (null != supermarkets && supermarkets.Count > 0)
			{
				sbSupermarket.Append("超市: ");
				for (int i = 0; i < supermarketWeights.Length; i++)
				{
					if (supermarketWeights[i] > 0)
						sbSupermarket.Append(string.Format("{0}kg*{1}+", i/1000, supermarketWeights[i]));
				}
				if (sbSupermarket.ToString().EndsWith("+"))
					sbSupermarket.Remove(sbSupermarket.Length - 1, 1);
				sbSupermarket.Append(string.Format(", 共计{0}个, 总价￥{1:0.00}", supermarkets.Count, supermarketTotalPrice));
			}

			StringBuilder sbRainbow = new StringBuilder();
			if (null != rainbows && rainbows.Count > 0)
			{
				sbRainbow.Append("彩虹: ");
				for (int i = 0; i < rainbowWeights.Length; i++)
				{
					if (rainbowWeights[i] > 0)
						sbRainbow.Append(string.Format("{0}kg*{1}+", i / 1000, rainbowWeights[i]));
				}
				if (sbRainbow.ToString().EndsWith("+"))
					sbRainbow.Remove(sbRainbow.Length - 1, 1);
				sbRainbow.Append(string.Format(", 共计{0}个, 总价￥{1:0.00}", rainbows.Count, rainbowTotalPrice));
			}

			StringBuilder sbDealworthier = new StringBuilder();
			if (null != dealworthiers && dealworthiers.Count > 0)
			{
				sbDealworthier.Append("Dealworthier: ");
				for (int i = 0; i < dealworthierWeights.Length; i++)
				{
					if (dealworthierWeights[i] > 0)
						sbDealworthier.Append(string.Format("{0}kg*{1}+", ((float)i / 1000).ToString("0.0"), dealworthierWeights[i]));
				}
				if (sbDealworthier.ToString().EndsWith("+"))
					sbDealworthier.Remove(sbDealworthier.Length - 1, 1);
				sbDealworthier.Append(string.Format(", 共计{0}个, 总价￥{1:0.00}", dealworthiers.Count, dealworthierTotalPrice));
			}

			StringBuilder sbOuhua = new StringBuilder();
			if (null != ouhuas && ouhuas.Count > 0)
			{
				sbOuhua.Append("欧华: ");
				for (int i = 0; i < ouhuaWeights.Length; i++)
				{
					if (ouhuaWeights[i] > 0)
						sbOuhua.Append(string.Format("{0}kg*{1}+", ((float)i / 1000).ToString("0.0"), ouhuaWeights[i]));
				}
				if (sbOuhua.ToString().EndsWith("+"))
					sbOuhua.Remove(sbOuhua.Length - 1, 1);
				sbOuhua.Append(string.Format(", 共计{0}个, 总价￥{1:0.00}", ouhuas.Count, ouhuaTotalPrice));
			}

			StringBuilder sbHanslord = new StringBuilder();
			if (null != hanslords && hanslords.Count > 0)
			{
				sbHanslord.Append("Hanslord: ");
				for (int i = 0; i < hanslordWeights.Length; i++)
				{
					if (hanslordWeights[i] > 0)
						sbHanslord.Append(string.Format("{0}kg*{1}+", ((float)i / 1000).ToString("0.0"), hanslordWeights[i]));
				}
				if (sbHanslord.ToString().EndsWith("+"))
					sbHanslord.Remove(sbHanslord.Length - 1, 1);
				sbHanslord.Append(string.Format(", 共计{0}个, 总价￥{1:0.00}", hanslords.Count, hanslordTotalPrice));
			}

			int totalCount = supermarkets.Count + rainbows.Count + dealworthiers.Count + ouhuas.Count + hanslords.Count;
			int totalPrice = supermarketTotalPrice + rainbowTotalPrice + dealworthierTotalPrice + ouhuaTotalPrice + hanslordTotalPrice;
			PacketResultForm prf = new PacketResultForm(
				sbSupermarket.ToString(), sbRainbow.ToString(), sbDealworthier.ToString(), sbOuhua.ToString(), sbHanslord.ToString(),
				string.Format("共计{0}个, 总价￥{1:0.00}", totalCount, totalPrice),
				supermarketFilename, rainbowFilename, dealworthierFilename, ouhuaFilename, hanslordFilename,
				packingListFilename, addressFilename, addresseImportIerungFilename);
			prf.OnUpdateStatus += new EventHandler(prf_OnUpdateStatus);
			return prf;
		}

		#region obsoleted code
		private string GenerateSupermarketFiles(List<PacketInfo> packetInfos, string folder)
		{
			try
			{
				string destExcelFile = CreateOutputFile(
					Directory.GetParent(Application.ExecutablePath).FullName, SUPERMARKET_TEMPLATE_FILENAME, 
					folder, SUPERMARKET_EXCEL_OUTPUT_FILENAME);
				#region error message
				if (string.IsNullOrEmpty(destExcelFile))
				{
					MessageBox.Show(
						this,
						"Create excel file for supermarket failed.\nMaybe supermarket template file missed.\nMake sure the template file exists in the same folder with the executable file.",
						this.Text,
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return string.Empty;
				}
				#endregion
			
				// Write data into excel.
				Excel supermarketExcel = new Excel(destExcelFile, Excel.OledbVersions.OLEDB40);

				try
				{
					for (int i = 0; i < packetInfos.Count; i++)
					{
						PacketInfo pi = packetInfos[i];	
						if (!supermarketExcel.Insert("导入模板", string.Empty, CreatePacketInfoSupermarketPostNLValues(i+1, pi)))
						{
							#region error message
							MessageBox.Show(
								this,
								"Error occured during write data into excel file for supermarket:" + pi.RecipientNameCn,
								this.Text,
								MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							#endregion
							return string.Empty;
						}
					}
				}
				catch (Exception ex)
				{
					#region error message
					MessageBox.Show(
						this,
						"Error occured during write data into excel file for supermarket:" + ex.ToString(),
						this.Text,
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					#endregion
					return string.Empty;
				}
				finally
				{
					supermarketExcel.Close();
				}

				return destExcelFile;
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					this,
					"Error occured during generating files for supermarket:" + ex.ToString(),
					this.Text,
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return string.Empty;
			}
		}
		
		private string GenerateRainbowFiles(List<PacketInfo> packetInfos, string folder)
		{
			try
			{
				string destExcelFile = CreateOutputFile(
					Directory.GetParent(Application.ExecutablePath).FullName, RAINBOW_TEMPLATE_FILENAME,
					folder, RAINBOW_EXCEL_OUTPUT_FILENAME);
				#region error message
				if (string.IsNullOrEmpty(destExcelFile))
				{
					MessageBox.Show(
						this,
						"Create excel file for rainbow failed.\nMaybe rainbow template file missed.\nMake sure the template file exists in the same folder with the executable file.",
						this.Text,
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return string.Empty;
				}
				#endregion
				
				Excel rainbowExcel = new Excel(destExcelFile, Excel.OledbVersions.OLEDB40);
				
				try
				{
					string packTitle = string.Empty;
					string pingzhangEmail = string.Empty;
					string realkk = string.Empty;
					string senderCompany = string.Empty;
					string OttoBrennerStr4a = string.Empty;
					string senderPlz = string.Empty;
					string senderCity = string.Empty;
					string senderTel = string.Empty;
					string recipientName = string.Empty;
					string address = string.Empty;
					string postCode = string.Empty;
					string provinceCity = string.Empty;
					string phoneNumber = string.Empty;
					string china = string.Empty;
					string item = string.Empty;
					string count = string.Empty;
					string weight = string.Empty;
					string moneyAmount = string.Empty;
					string fullAddressCn = string.Empty;
									
					for (int i = 0; i < packetInfos.Count; i++)
					{
						PacketInfo pi = packetInfos[i];
						packTitle += string.Format("'包裹单{0}'" + (i >= packetInfos.Count-1 ? string.Empty : ","), i+1);
						pingzhangEmail += "'pingzhang1981@yahoo.de'" + (i >= packetInfos.Count-1 ? string.Empty : ",");
						realkk += "'realkk'" + (i >= packetInfos.Count-1 ? string.Empty : ",");
						senderCompany += "'senderCompany International GmbH'" + (i >= packetInfos.Count-1 ? string.Empty : ",");
						OttoBrennerStr4a += "'Otto Brenner Str.4a'" + (i >= packetInfos.Count-1 ? string.Empty : ",");
						senderPlz += "'47877'" + (i >= packetInfos.Count-1 ? string.Empty : ",");
						senderCity += "'Willich'" + (i >= packetInfos.Count-1 ? string.Empty : ",");
						senderTel += "'02154 8839989'" + (i >= packetInfos.Count-1 ? string.Empty : ",");
						recipientName += string.Format("'{0}'" + (i >= packetInfos.Count-1 ? string.Empty : ","), pi.RecipientNameEn);
						address += string.Format("'{0}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), pi.AddressEn);
						postCode += string.Format("'{0}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), pi.PostCode);
						provinceCity += string.Format("'{0}'" + (i >= packetInfos.Count-1 ? string.Empty : ","), pi.ProvinceCityEn);
						phoneNumber += string.Format("'{0}'" + (i >= packetInfos.Count-1 ? string.Empty : ","), pi.PhoneNumber);
						china += "'China'" + (i >= packetInfos.Count-1 ? string.Empty : ",");
						item += "'Milk powder'" + (i >= packetInfos.Count-1 ? string.Empty : ",");
						count += "'10'" + (i >= packetInfos.Count-1 ? string.Empty : ",");
						weight += string.Format("'{0}'" + (i >= packetInfos.Count-1 ? string.Empty : ","), (pi.Weight/1000).ToString("0"));
						moneyAmount += "'115.90'" + (i >= packetInfos.Count-1 ? string.Empty : ",");
						fullAddressCn += string.Format("'{0}'" + (i >= packetInfos.Count-1 ? string.Empty : ","), pi.FullAddress);
					}
					
					string endColumn = Excel.GetColumnIndex("C", packetInfos.Count - 1);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 1, endColumn), packTitle);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 2, endColumn), pingzhangEmail);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 3, endColumn), realkk);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 5, endColumn), senderCompany);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 6, endColumn), OttoBrennerStr4a);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 7, endColumn), senderPlz);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 8, endColumn), senderCity);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 9, endColumn), senderTel);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 10, endColumn), recipientName);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 11, endColumn), address);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 13, endColumn), postCode);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 14, endColumn), provinceCity);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 15, endColumn), phoneNumber);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 16, endColumn), china);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 17, endColumn), item);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 18, endColumn), count);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 19, endColumn), weight);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 20, endColumn), moneyAmount);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 21, endColumn), fullAddressCn);
				}
				finally
				{
					rainbowExcel.Close();
				}

				return destExcelFile;
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					this,
					"Error occured during generating files for rainbow:" + ex.ToString(),
					this.Text,
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return string.Empty;
			}
		}
		
		private string GenerateDealworthierFiles(List<PacketInfo> packetInfos, string folder)
		{
			try
			{
				string destExcelFile = CreateOutputFile(
					Directory.GetParent(Application.ExecutablePath).FullName, DEALWORTHIER_TEMPLATE_FILENAME,
					folder, DEALWORTHIER_EXCEL_OUTPUT_FILENAME);
				#region error message
				if (string.IsNullOrEmpty(destExcelFile))
				{
					MessageBox.Show(
						this,
						"Create excel file for dealworthier failed.\nMaybe rainbow template file missed.\nMake sure the template file exists in the same folder with the executable file.",
						this.Text,
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return string.Empty;
				}
				#endregion

				Excel excel = new Excel(destExcelFile, Excel.OledbVersions.OLEDB40);

				try
				{
					string packTitle = string.Empty;
					string realkk = string.Empty;
					string pingzhangEmail = string.Empty;
					string weight = string.Empty;
					string senderCompany = string.Empty;
					string senderStr = string.Empty;
					string senderStrNumber = string.Empty;
					string senderPlz = string.Empty;
					string senderCity = string.Empty;
					string senderTel = string.Empty;
					string deutschland = string.Empty;
					string recipientName = string.Empty;
					string address = string.Empty;
					string address2 = string.Empty;
					string address3 = string.Empty;
					string postCode = string.Empty;
					string provinceCity = string.Empty;
					string china = string.Empty;
					string phoneNumber = string.Empty;
					string fullAddressCn = string.Empty;
					string item = string.Empty;
					string count = string.Empty;
					string moneyAmount = string.Empty;

					for (int i = 0; i < packetInfos.Count; i++)
					{
						PacketInfo pi = packetInfos[i];
						packTitle += string.Format("'DHL Premium 运单 {0:00}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), i + 1);
						realkk += "'realkk'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						pingzhangEmail += "'pingzhang1981@yahoo.de'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						weight += string.Format("'{0}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), ((float)pi.Weight / 1000).ToString("0.0"));
						senderCompany += "'senderCompany International GmbH'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						senderStr += "'Otto Brenner Str.'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						senderStrNumber += "'4a'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						senderPlz += "'47877'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						senderCity += "'Willich'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						senderTel += "'02154 8839989'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						deutschland += "'Germany'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						recipientName += string.Format("'{0}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), pi.RecipientNameEn);
						address += string.Format("'{0}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), pi.AddressEn);
						address2 += string.Format("'{0}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), " ");
						address3 += string.Format("'{0}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), " ");
						postCode += string.Format("'{0}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), pi.PostCode);
						provinceCity += string.Format("'{0}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), pi.ProvinceCityEn);
						china += "'China'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						phoneNumber += string.Format("'{0}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), pi.PhoneNumber);
						fullAddressCn += string.Format("'{0}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), pi.FullAddress);
						item += "'Milk powder'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						count += "'10'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						moneyAmount += "'115.90'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
					}

					string endColumn = Excel.GetColumnIndex("D", packetInfos.Count - 1);
					int row = 1;
					//packTitle = "' ',' ',' '," + packTitle;
					//excel.Insert("DHL Premium Sheet", string.Empty, packTitle);
					//excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), packTitle);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), realkk);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), realkk);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), pingzhangEmail);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), weight);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), senderCompany);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), senderStr);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), senderStrNumber);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), senderPlz);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), senderCity);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), senderTel);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), deutschland);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), recipientName);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), address);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), address2);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), address3);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), postCode);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), provinceCity);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), china);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), phoneNumber);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), fullAddressCn);
					row++;
					row++;
					row++;
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), item);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), count);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), weight);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), moneyAmount);

					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), packTitle);
				}
				catch (Exception ex)
				{
					MessageBox.Show(
						this,
						"Error occured during inserting information into excel for dealworthier.\n" + ex.ToString(),
						this.Text,
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				finally
				{
					excel.Close();
				}

				return destExcelFile;
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					this,
					"Error occured during generating files for rainbow:" + ex.ToString(),
					this.Text,
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return string.Empty;
			}
		}
		#endregion

		private string GenerateOuhuaFiles(List<PacketInfo> packetInfos, string folder)
		{
			try
			{
				string destExcelFile = CreateOutputFile(
					Directory.GetParent(Application.ExecutablePath).FullName, OUHUA_TEMPLATE_FILENAME,
					folder,
					string.Format(OUHUA_EXCEL_OUTPUT_FILENAME, DateTime.Now.ToString("yyyyMMdd")));
				#region error message
				if (string.IsNullOrEmpty(destExcelFile))
				{
					MessageBox.Show(
						this,
						"Create excel file for Ouhua failed.\nMaybe Ouhua template file missed.\nMake sure the template file exists in the same folder with the executable file.",
						this.Text,
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return string.Empty;
				}
				#endregion

				Excel excel = new Excel(destExcelFile, Excel.OledbVersions.OLEDB40);

				try
				{
					string packTitle = string.Empty;
					string realkk = string.Empty;
					string pingzhangEmail = string.Empty;
					string weight = string.Empty;
					string senderCompany = string.Empty;
					string senderStr = string.Empty;
					string senderStrNumber = string.Empty;
					string senderPlz = string.Empty;
					string senderCity = string.Empty;
					string senderTel = string.Empty;
					string deutschland = string.Empty;
					string recipientName = string.Empty;
					string address = string.Empty;
					string address2 = string.Empty;
					string address3 = string.Empty;
					string postCode = string.Empty;
					string provinceCity = string.Empty;
					string china = string.Empty;
					string phoneNumber = string.Empty;
					string fullAddressCn = string.Empty;
					string milkPowder = string.Empty;
					string count = string.Empty;
					string moneyAmount = string.Empty;

					for (int i = 0; i < packetInfos.Count; i++)
					{
						PacketInfo pi = packetInfos[i];
						packTitle += string.Format("'包裹单{0:00}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), i + 1);
						//realkk += "'realkk'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						//pingzhangEmail += "'pingzhang1981@yahoo.de'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						weight += string.Format("'{0}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), ((float)pi.Weight / 1000).ToString("0.0"));
						// Modified by KK on 2015/12/22.
						// Hanslord was obsoleted. Replaced by Chinovo
						//senderCompany += "'Hanslord e.K.'" + (i >= packetInfos.Count - 1 ? string.Empty : ","); //"'JHT International GmbH'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						//senderStr += "'Wiesenstr.'" + (i >= packetInfos.Count - 1 ? string.Empty : ","); //"'Otto Brenner Str.'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						//senderStrNumber += "'51'" + (i >= packetInfos.Count - 1 ? string.Empty : ","); //"'4a'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						//senderPlz += "'40549'" + (i >= packetInfos.Count - 1 ? string.Empty : ","); //"'47877'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						//senderCity += "'Duesseldorf'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");//"'Willich'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						//senderTel += "'+49 211 56947980'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");//"'02154 8839989'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						senderCompany += "'Chinovo GmbH'" + (i >= packetInfos.Count - 1 ? string.Empty : ","); //"'JHT International GmbH'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						senderStr += "'Sigsfeldstr.'" + (i >= packetInfos.Count - 1 ? string.Empty : ","); //"'Otto Brenner Str.'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						senderStrNumber += "'10'" + (i >= packetInfos.Count - 1 ? string.Empty : ","); //"'4a'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						senderPlz += "'52078'" + (i >= packetInfos.Count - 1 ? string.Empty : ","); //"'47877'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						senderCity += "'Aachen'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");//"'Willich'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						senderTel += "'017684399889'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");//"'02154 8839989'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						deutschland += "'Germany'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						recipientName += string.Format("'{0}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), pi.RecipientNameEn);
						address += string.Format("'{0}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), pi.AddressEn);
						address2 += string.Format("'{0}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), " ");
						address3 += string.Format("'{0}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), " ");
						postCode += string.Format("'{0}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), pi.PostCode);
						provinceCity += string.Format("'{0}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), pi.ProvinceCityEn);
						china += "'China'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						phoneNumber += string.Format("'{0}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), pi.PhoneNumber);
						fullAddressCn += string.Format("'{0}'" + (i >= packetInfos.Count - 1 ? string.Empty : ","), pi.FullAddress);
						milkPowder += "'Milk powder'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						count += "'10'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						moneyAmount += "'115.90'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
					}

					string endColumn = Excel.GetColumnIndex("D", packetInfos.Count - 1);
					int row = 5;
					//packTitle = "' ',' ',' '," + packTitle;
					//excel.Insert("DHL申请表", string.Empty, packTitle);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), packTitle);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), senderCompany);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), senderStr);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), senderStrNumber);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), senderPlz);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), senderCity);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), senderTel);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), recipientName);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), address2);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), address);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), address2);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), postCode);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), address2);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), provinceCity);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), phoneNumber);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), china);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), weight);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), milkPowder);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), moneyAmount);
					//excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), realkk);
					//excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), pingzhangEmail);
					////excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), deutschland);
					////excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), address);
					////excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), address2);
					////excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), address3);
					////excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), fullAddressCn);
					////row++;
					////row++;
					////row++;
					////excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), count);
					////excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), weight);
				}
				catch (Exception ex)
				{
					MessageBox.Show(
						this,
						"Error occured during inserting information into excel for Ouhua.\n" + ex.ToString(),
						this.Text,
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				finally
				{
					excel.Close();
				}

				return destExcelFile;
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					this,
					"Error occured during generating files for Ouhua:" + ex.ToString(),
					this.Text,
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return string.Empty;
			}
		}
		private string GenerateHanslordFiles(List<PacketInfo> packetInfos, string folder)
		{
			try
			{
				// Daten file for hanslord.
				string daten4cnFilename = CreateOutputFile(
					Directory.GetParent(Application.ExecutablePath).FullName, HANSLORD_DATEN_CN_FILENAME,
					folder, HANSLORD_DATEN_CN_FILENAME);
				#region error message
				if (string.IsNullOrEmpty(daten4cnFilename))
				{
					MessageBox.Show(
						this,
						"Create excel file of daten for Hanslord failed.\nMaybe Hanslord template file missed.\nMake sure the template file exists in the same folder with the executable file.",
						this.Text,
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return string.Empty;
				}
				#endregion
				Excel daten4cnExcel = new Excel(daten4cnFilename, Excel.OledbVersions.OLEDB40);

				string outputFilename = string.Format(HANSLORD_CSV_OUTPUT_FILENAME, DateTime.Now.ToString("yyyyMMdd"));
				string hanslordCsv = Path.Combine(folder, outputFilename);
				int postfix = 0;
				while (File.Exists(hanslordCsv))
				{
					string filename = string.Format(
						"{0}({1}){2}",
						Path.GetFileNameWithoutExtension(outputFilename),
						++postfix,
						Path.GetExtension(outputFilename));
					hanslordCsv = Path.Combine(folder, filename);
				}
				
				StreamWriter writer = new StreamWriter(hanslordCsv);

				try
				{
					for (int i = 0; i < packetInfos.Count; i++)
					{
						PacketInfo pi = packetInfos[i];

						// DPEE-SHIPMENT
						writer.WriteLine(string.Format(
							"{0}|DPEE-SHIPMENT|BPI|{1}|{2}|||||||||||||||||||||||||||||01||||||||||||||||||||1|||||||||||||||||||||||||||||||||||||||",
							i + 1, DateTime.Now.ToString("yyyyMMdd"), ((float)pi.Weight / 1000).ToString("0.0"))); // done

						// DPEE-SENDER
						writer.WriteLine(string.Format(
							"{0}|DPEE-SENDER|6274440893|Hanslord e.K.||Junfeng Qi|Wiesenstr.|51||40549|Duesseldorf|DE||frank.qi@hanslord.de|021156947980|||||||||||||||||4917641615985|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||",
							i + 1)); // done.

						// DPEE-RECEIVER
						string[] addressSegments = pi.GetAddressEnSegments();
						writer.WriteLine(string.Format(
							"{0}|DPEE-RECEIVER|{1}||||{1}|{2}|{3}|{4}|{5}|{6}|CN|||{7}|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||",
							i + 1, 
							pi.RecipientNameEn,
							addressSegments[0], addressSegments[1], addressSegments[2],
							pi.PostCode,
							pi.ProvinceCityEn,
							pi.PhoneNumber));// {2}:40, {3}:10, {4}:30

						// DPEE-ITEM
						writer.WriteLine(string.Format(
							"{0}|DPEE-ITEM|{1}|||||PK|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||",
							i + 1, ((float)pi.Weight / 1000).ToString("0.0"))); // done

						// DPEE-EXPORT-DOC
						writer.WriteLine(string.Format(
							"{0}|DPEE-EXPORT-DOC|{1}||||Milk Powder||DE|{2}|EUR||Proforma Invoice||||||Milk Powder|0|0|0|0|1|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||",
							i + 1, DateTime.Now.ToString("yyyyMMdd"), pi.GetTotalExportMoneyAmount().ToString("0.00"))); // done
						
						int exportItemsTotalCount = 0; // 报关信息中包含的报关产品总该数量.
						foreach (ExportItem ei in pi.ExportItems)
						{
							writer.WriteLine(string.Format(
								"{0}|DPEE-EXPORT-ITEM|{1}|DE|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||",
								i+1, ei.ExportItemString)); // done
							exportItemsTotalCount += int.Parse(ei.ExportItemString.Split('|')[1]);
						}
						
						string daten4cnRow = string.Format(
							"'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},'{9}',{10}, {11}", 
							pi.RecipientNameEn, 
							addressSegments[0], addressSegments[1], addressSegments[2],
							pi.PostCode, pi.ProvinceCityEn,
							string.Empty, // dhl shipment No.
							string.Empty, // invoice No.
							exportItemsTotalCount, // Quantity1
							"Milk Powder", // Description1
							(118.00f / exportItemsTotalCount).ToString("0.00"), // unit price1
							118.00.ToString("0.00")); // total price1
						daten4cnExcel.Insert("Tabelle1", string.Empty, daten4cnRow);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(
						this,
						"Error occured during creating .csv file for Hanslord.\n" + ex.ToString(),
						this.Text,
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				finally
				{
					writer.Close();
					daten4cnExcel.Close();
				}

				return hanslordCsv;
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					this,
					"Error occured during generating files for Ouhua:" + ex.ToString(),
					this.Text,
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return string.Empty;
			}
		}
		
		private string GeneratePackingListExcelFile(List<PacketInfo> packetInfos, string folder)
		{
			// Generate list for senderCompany.
			string destPackingListJHT = CreateOutputFile(
				Directory.GetParent(Application.ExecutablePath).FullName, PACKING_LIST_TEMPLATE,
				folder, string.Format(PACKING_LIST_OUTPUT_JHT, DateTime.Now.ToString("yyyyMMdd")));
			if (string.IsNullOrEmpty(destPackingListJHT))
			{
				#region error message
				MessageBox.Show(
					this,
					"Create excel file for shipping list for senderCompany failed.\nMaybe the template file missed.\nMake sure the template file exists in the same folder with the executable file.",
					this.Text,
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				#endregion
				return string.Empty;
			}

			Excel excelPackingListJHT = new Excel(destPackingListJHT, Excel.OledbVersions.OLEDB40);

			try
			{
				for (int i = 0; i < packetInfos.Count; i++)
				{
					PacketInfo pi = packetInfos[i];
					if (pi.Weight == 0)
						continue;

					string[] products = pi.ProductLongString.Split(new char[] { '\r', '\n' });
					
					for (int j = 0; j < products.Length; j++)
					{
						if (string.IsNullOrEmpty(products[j].Trim()))
							continue;
					
						string[] productDetails = products[j].Split(';');
						object[] values = new object[]{
							j == 0 ? (i+1).ToString():" ",
							j == 0 ? pi.RecipientNameCn:" ",
							j == 0 ? "000000000000":" ",
							productDetails[0].Trim(), productDetails[1].Trim(),
							productDetails.Length >= 3 ? productDetails[2].Trim() : string.Empty,
							j == 0 ? ("A"+(i+1).ToString()):" "};
							
						excelPackingListJHT.Insert("Sheet1", string.Empty, values);
					}
				}
			}
			catch (Exception ex)
			{
				#region error message
				MessageBox.Show(
					this,
					"Error occured during write data into excel file for packing:" + ex.ToString(),
					this.Text,
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				#endregion
			}
			finally
			{
				excelPackingListJHT.Close();
			}

			return destPackingListJHT;
		}

		private string GenerateChineseAddressDoc(List<PacketInfo> packetInfos, string folder)
		{
			try
			{
				RichTextBox rtb = new RichTextBox();
				rtb.Font = new Font("Microsoft Yahei", 21, FontStyle.Bold);
				
				bool firstOnPage = true; // 是否1页中2个地址的第1个.
				int prevAddrLines = 0;
				
				foreach (PacketInfo pi in packetInfos)
				{
					if (pi.Weight == 0)
						continue;
					
					string s =  "寄:\n";
					s += string.Format("邮编: {0}\n", pi.PostCode);
					s += string.Format("地址: {0} {1}\n", pi.ProvinceCityCn, pi.AddressCn);
					s += string.Format("收件人: {0}\n", pi.RecipientNameCn);
					s += string.Format("联系电话: {0}\n", pi.PhoneNumber);

					// 1页显示17行, 1个地址根据地址长短至少5行, 地址每超过24个字符增加1行, 则同1页中的2个地址之间的空行减少1行.
					int addrLines = 4 + (int)Math.Ceiling(((float)string.Format("地址: {0} {1}\n", pi.ProvinceCityCn, pi.AddressCn).Length) / 25.0f);
					if (!firstOnPage)
						s = new string('\n', 17 - addrLines - prevAddrLines) + s;
					
					rtb.Text += s;
					
					prevAddrLines = addrLines;
					firstOnPage = !firstOnPage;
				}
				
				string filename = Path.Combine(folder, string.Format(CHINESE_ADDRESSES_DOC, DateTime.Now.ToString("yyyyMMdd")));
				string outputFilename = filename;
				int i = 0;
				while (File.Exists(outputFilename))
				{
					outputFilename = string.Format(
						"{0}({1}){2}",
						Path.GetFileNameWithoutExtension(filename),
						++i,
						Path.GetExtension(filename));
					outputFilename = Path.Combine(folder, outputFilename);
				}

				this.Controls.Add(rtb);
				rtb.SaveFile(outputFilename);
				this.Controls.Remove(rtb);
				return outputFilename;
			}
			catch (Exception ex)
			{
				#region error message
				MessageBox.Show(
					this,
					"Error occured during generating .doc of chinese addresses." + ex.ToString(),
					this.Text,
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				#endregion
				return string.Empty;
			}
		}
		
		// return: the real output filename.
		private string CreateOutputFile(string templateFolder, string templateFilename, string outputFolder, string outputFilename)
		{
			string templateFile = Path.Combine(templateFolder, templateFilename);
			if (!File.Exists(templateFile))
				return string.Empty;

			string outputFile = Path.Combine(outputFolder, outputFilename);
			int i = 0;
			while (File.Exists(outputFile))
			{
				string filename = string.Format(
					"{0}({1}){2}",
					Path.GetFileNameWithoutExtension(outputFilename),
					++i,
					Path.GetExtension(outputFilename));
				outputFile = Path.Combine(outputFolder, filename);
			}

			File.Copy(templateFile, outputFile);

			while (!File.Exists(outputFile))
				;
			System.Threading.Thread.Sleep(2000);

			return outputFile;
		}

		// 创建1个packet info在包裹单申请表中对应的值列表, 用于插入excel表格.
		private object[] CreatePacketInfoSupermarketValues(int index, PacketInfo pi)
		{
			if (null == pi)
				return null;

			//string sql = "Insert into [Sheet1$] values(1,'realkk','000','','','','','','','','pingzhang1981@yahoo.de','Hu Jun','Otto Brenner Str.','4a','47877','Willich','Deutschland','21177920614','Zhang GuoQiang','China','330000','JiangXiShengNanChangShi','HongGuTangDaShaAZuo13Lou',' ',' ','138000000000','Gift',6,50,'张国亮','江西省南昌市','红谷滩大厦A做13楼','330000','13800000000')";
			return new object[]{
				index,"realkk","000",
				"","","","","","","",
				"pingzhang1981@yahoo.de","Hu Jun","Otto Brenner Str.","4a","47877","Willich","Deutschland","21177920614",
				pi.RecipientNameEn,"China",pi.PostCode,pi.ProvinceCityEn.Replace(" ", string.Empty),pi.AddressEn,
				" "," ",
				pi.PhoneNumber,
				"Gift",(pi.Weight/1000).ToString("0"),50,
				pi.RecipientNameCn,pi.ProvinceCityCn,pi.AddressCn,pi.PostCode,pi.PhoneNumber};
		}

		// Added by KK on 2014/11/08.
		// for postNL.
		// 创建1个packet info在包裹单申请表中对应的值列表, 用于插入excel表格.
		private object[] CreatePacketInfoSupermarketPostNLValues(int index, PacketInfo pi)
		{
			if (null == pi)
				return null;

			//string sql = "Insert into [Sheet1$] values(1,'realkk','000','','','','','','','','pingzhang1981@yahoo.de','Hu Jun','Otto Brenner Str.','4a','47877','Willich','Deutschland','21177920614','Zhang GuoQiang','China','330000','JiangXiShengNanChangShi','HongGuTangDaShaAZuo13Lou',' ',' ','138000000000','Gift',6,50,'张国亮','江西省南昌市','红谷滩大厦A做13楼','330000','13800000000')";
			string[] provinceCity = pi.ProvinceCityCn.Split(' ');
			string province = provinceCity[0];
			string city = provinceCity.Length >= 2 ? pi.ProvinceCityCn.Remove(0, province.Length).Trim() : provinceCity[0];
			
			return new object[]{
				"postNL",
				((float)pi.Weight/1000).ToString("0.0"), // weight
				42, 42, 35, // size of packet
				"Milk Powder",
				//"Hanslord e.K.", "Düsseldorf", "Wiesenstr.", "51", "40549", "+49 211 56947980", "pingzhang1981@yahoo.de", // sender
				"Chinovo", "Aachen", "Sigsfeldstr.", "10", "52078", "017684399889", "pingzhang1981@yahoo.de", // sender
				pi.RecipientNameCn, province, city, pi.AddressCn, pi.PostCode, pi.PhoneNumber, string.Empty, // recipient info
				"Aptamil Milk Powder", // 物品1名称
				string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, // 物品1信息, ems包裹填写
				string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, // 物品2信息, ems包裹填写
				string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, // 物品3信息, ems包裹填写
				"P"};
		}

		void prf_OnUpdateStatus(object sender, EventArgs e)
		{
			if (null != this.OnUpdateStatus)
				this.OnUpdateStatus(this, EventArgs.Empty);
		}
		
		// Only required by postnl.
		private string GenerateAdresseImportIerungFile(List<PacketInfo> packetInfos, string folder)
		{
			if (null == packetInfos || packetInfos.Count <= 0)
				return string.Empty;
			
			if (!Directory.Exists(folder))
				return string.Empty;

			string filename = Path.Combine(folder, string.Format(ADRESSE_IMPORT_IERUNG_FILENAME, DateTime.Now.ToString("yyyyMMdd")));
			string outputFilename = filename;
			int i = 0;
			while (File.Exists(outputFilename))
			{
				outputFilename = string.Format(
					"{0}({1}){2}",
					Path.GetFileNameWithoutExtension(filename),
					++i,
					Path.GetExtension(filename));
				outputFilename = Path.Combine(folder, outputFilename);
			}

			StreamWriter writer = new StreamWriter(outputFilename, false, Encoding.UTF8);
			writer.WriteLine("REC_ID;KUNDENGRUPPE;MATCHCODE;NAME1;STRASSE;PLZ;ORT;LAND;USERFELD_01");
			
			for (int j = 0; j < packetInfos.Count; j++)
			{
				PacketInfo pi = packetInfos[j];	
				writer.WriteLine(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8}", 
					j+1, 1, string.Empty, 
					pi.RecipientNameEn, pi.AddressEn, pi.PostCode, pi.ProvinceCityEn, "CN", 
					string.Empty));
			}
			
			writer.Close();

			return outputFilename;
		}

		private void tsbtnGoBaishi_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "Excel Files(*.xls)|*.xls|All Files(*.*)|*.*";
			sfd.FileName = string.Format("直邮代发{0}.xls", DateTime.Now.ToString("yyyyMMdd"));
			sfd.OverwritePrompt = true;
			if (DialogResult.OK == sfd.ShowDialog(this))
			{
				//string innerFilename = string.Format("{0}对内{1}", 
				//    Path.Combine(Directory.GetParent(sfd.FileName).FullName, Path.GetFileNameWithoutExtension(sfd.FileName)), Path.GetExtension(sfd.FileName));
				//StreamWriter writerInner = new StreamWriter(innerFilename, false, Encoding.UTF8);
				//StreamWriter writerBaishi = new StreamWriter(sfd.FileName, false, Encoding.UTF8);
				//writerBaishi.WriteLine("日期,客户信息,客户信息(拼音),直邮品种,数量,金额,单价,物流公司,单号,特殊要求");
				//writerInner.WriteLine("日期,ID,订单编号,客户信息,直邮品种,数量,付款金额,进价,单价,物流公司,单号,客人QQ号");
				
				if (File.Exists(sfd.FileName))
					File.Delete(sfd.FileName);
				File.Copy(Path.Combine(Directory.GetParent(Application.ExecutablePath).FullName, "直邮代发全信息模板.xls"), sfd.FileName);
				Excel excel = new Excel(sfd.FileName, Excel.OledbVersions.OLEDB40);
				
				foreach (PacketDetailsControl pdc in pnlPackets.Controls)
				{
					string productDesc = string.Empty;
					int productCount = 0;
					if (1 == pdc.PacketInfo.Products.Count)
					{
						productDesc = pdc.PacketInfo.Products[0].ShortName;
						productCount = pdc.PacketInfo.Products[0].Count;
					}
					else
					{
						foreach (SoldProductInfo spi in pdc.PacketInfo.Products)
						{
							productDesc += spi.ShortName+"\n";
							productCount += spi.Count;
						}
					}
					
					OrderLib.Order order = MainForm.Instance.GetOrder(pdc.PacketInfo.OrderId);
					Trace.Assert(null != order);
					
					string values = string.Format(
						"'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}'", 
						DateTime.Now.ToString("yyyyy/MM/dd"), order.BuyerAccount, order.OrderId,
						pdc.PacketInfo.FullAddress, HanZiToPinYin.Convert(pdc.PacketInfo.FullAddress),
						productDesc, productCount, "0.00", "0.00", "0.00",
						"DHL", string.Empty, string.Empty, string.Empty);
					excel.Insert("Sheet1", string.Empty, values);
				
					//writerBaishi.WriteLine(string.Format(
					//    "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
					//    DateTime.Now.ToString("yyyy/MM/dd"),
					//    pdc.PacketInfo.AddressCn, pdc.PacketInfo.AddressEn,
					//    productDesc, productCount, "0.00", "0.00", "DHL", string.Empty, string.Empty));
				}
				
				excel.Close();
				//writerBaishi.Close();
				//writerInner.Close();
				
				DialogResult dr = MessageBox.Show(
					this,
					"请确认所有输出文档正确.\n出单状态同步到服务器后, 订单状态将永久被修改, 此操作不可逆.\n这些订单以后将不会出现在<已付款>订单中.\n是否确定要修改订单状态并同步到服务器?", this.Text, 
					MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (DialogResult.No == dr)
					return;
					
				if (null != this.OnUpdateStatus)
					this.OnUpdateStatus(this, EventArgs.Empty);
			}
			
			Cursor.Current = Cursors.Default;
		}

		/*
		private void tsbtnGoEms_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "Excel Files(*.xls)|*.xls|All Files(*.*)|*.*";
			sfd.FileName = string.Format("ems{0}.xls", DateTime.Now.ToString("yyyyMMdd"));
			sfd.OverwritePrompt = true;
			if (DialogResult.OK == sfd.ShowDialog(this))
			{
				//string innerFilename = string.Format("{0}对内{1}", 
				//    Path.Combine(Directory.GetParent(sfd.FileName).FullName, Path.GetFileNameWithoutExtension(sfd.FileName)), Path.GetExtension(sfd.FileName));
				//StreamWriter writerInner = new StreamWriter(innerFilename, false, Encoding.UTF8);
				//StreamWriter writerBaishi = new StreamWriter(sfd.FileName, false, Encoding.UTF8);
				//writerBaishi.WriteLine("日期,客户信息,客户信息(拼音),直邮品种,数量,金额,单价,物流公司,单号,特殊要求");
				//writerInner.WriteLine("日期,ID,订单编号,客户信息,直邮品种,数量,付款金额,进价,单价,物流公司,单号,客人QQ号");
				
				if (File.Exists(sfd.FileName))
					File.Delete(sfd.FileName);
				File.Copy(Path.Combine(Directory.GetParent(Application.ExecutablePath).FullName, "template-ems.xls"), sfd.FileName);
				Excel excel = new Excel(sfd.FileName, Excel.OledbVersions.OLEDB40);
				
				foreach (PacketDetailsControl pdc in pnlPackets.Controls)
				{
					pdc.UpdatePacketInfo();
					string productDesc = string.Empty;
					string brandDesc = string.Empty;
					int productCount = 0;
					if (1 == pdc.PacketInfo.Products.Count)
					{
						productDesc = pdc.PacketInfo.Products[0].ShortName;
						brandDesc = BrandInfo.GetBrand(pdc.PacketInfo.Products[0].BrandId).Name;
						productCount = pdc.PacketInfo.Products[0].Count;
					}
					else
					{
						foreach (SoldProductInfo spi in pdc.PacketInfo.Products)
						{
							productDesc += spi.ShortName+"\n";
							brandDesc += BrandInfo.GetBrand(spi.BrandId).Name + "\n";
							productCount += spi.Count;
						}
					}
					
					OrderLib.Order order = MainForm.Instance.GetOrder(pdc.PacketInfo.OrderId);
					Trace.Assert(null != order);
					
					string[] provinceCity = pdc.PacketInfo.ProvinceCityCn.Split(' ');
					string province = provinceCity[0];
					string city = provinceCity.Length >= 2 ? pdc.PacketInfo.ProvinceCityCn.Remove(0, province.Length).Trim() : provinceCity[0];

					string values = string.Format(
						"'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}'", 
						"<>", "Rechnergigant", "Meissnerweg 59", "17655443697", "Germany", "Darmstadt", "64289", DateTime.Now.ToString("yyyyy/MM/dd"), 
						order.RecipientName, pdc.PacketInfo.FullAddress, province, city, pdc.PacketInfo.PostCode, pdc.PacketInfo.PhoneNumber, "360000000000000000",
						"<>", "<>", "<>", "<>",
						productDesc, brandDesc, pdc.PacketInfo.Products[0].Specification, productCount, "盒");
					excel.Insert("Sheet0", string.Empty, values);
				
					//writerBaishi.WriteLine(string.Format(
					//    "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
					//    DateTime.Now.ToString("yyyy/MM/dd"),
					//    pdc.PacketInfo.AddressCn, pdc.PacketInfo.AddressEn,
					//    productDesc, productCount, "0.00", "0.00", "DHL", string.Empty, string.Empty));
				}
				
				excel.Close();
				//writerBaishi.Close();
				//writerInner.Close();
				
				DialogResult dr = MessageBox.Show(
					this,
					"请确认所有输出文档正确.\n出单状态同步到服务器后, 订单状态将永久被修改, 此操作不可逆.\n这些订单以后将不会出现在<已付款>订单中.\n是否确定要修改订单状态并同步到服务器?", this.Text, 
					MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (DialogResult.No == dr)
					return;
					
				if (null != this.OnUpdateStatus)
					this.OnUpdateStatus(this, EventArgs.Empty);
			}

			Cursor.Current = Cursors.Default;
		}
		*/

		private void tsbtnGoEms_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			
			foreach (PacketDetailsControl pdc in pnlPackets.Controls)
			{
				pdc.UpdatePacketInfo();
				
				if (pdc.PacketInfo.ExportItems.Count <= 0)
				{
					MessageBox.Show(this, "至少有1个订单中没有输入身份证信息! 无法继续出单!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				
				// ID number.
				System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"\d{17}[\d|x|X]");
				System.Text.RegularExpressions.Match m = r.Match(pdc.PacketInfo.ExportItems[0].ExportItemString);
				if (!m.Success)
				{
					MessageBox.Show(this, "至少有1个订单中没有输入身份证信息! 无法继续出单!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				
				if (pdc.PacketInfo.Products.Count <= 0)
				{
					MessageBox.Show(this, "至少有1个订单中有商品无法识别! 无法继续出单!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}

			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "Excel Files(*.csv)|*.csv|All Files(*.*)|*.*";
			sfd.FileName = string.Format("ems{0}.csv", DateTime.Now.ToString("yyyyMMdd"));
			sfd.OverwritePrompt = true;
			if (DialogResult.OK == sfd.ShowDialog(this))
			{
				//string innerFilename = string.Format("{0}对内{1}", 
				//    Path.Combine(Directory.GetParent(sfd.FileName).FullName, Path.GetFileNameWithoutExtension(sfd.FileName)), Path.GetExtension(sfd.FileName));
				//StreamWriter writerInner = new StreamWriter(innerFilename, false, Encoding.UTF8);
				//StreamWriter writerBaishi = new StreamWriter(sfd.FileName, false, Encoding.UTF8);
				//writerBaishi.WriteLine("日期,客户信息,客户信息(拼音),直邮品种,数量,金额,单价,物流公司,单号,特殊要求");
				//writerInner.WriteLine("日期,ID,订单编号,客户信息,直邮品种,数量,付款金额,进价,单价,物流公司,单号,客人QQ号");
				
				if (File.Exists(sfd.FileName))
					File.Delete(sfd.FileName);

				StreamWriter writer = new StreamWriter(sfd.FileName, false, Encoding.UTF8);
				writer.WriteLine(("包裹编号,发件人姓名,发件人地址,发件人电话,发件人所在国家,发件人所在城市,发件人邮编,收件人姓名,收件人详细地址,省,市,收件人邮编,收件人手机号码,收件人身份证号码,包裹总重含包装,包裹长,包裹宽,包裹高,1.物品描述,1.品牌,1.规格,1.数量"));

				foreach (PacketDetailsControl pdc in pnlPackets.Controls)
				{
					pdc.UpdatePacketInfo();
					string productDesc = string.Empty;
					string brandDesc = string.Empty;
					int productCount = 0;
					if (1 == pdc.PacketInfo.Products.Count)
					{
						productDesc = pdc.PacketInfo.Products[0].ShortName;
						brandDesc = BrandInfo.GetBrand(pdc.PacketInfo.Products[0].BrandId).Name;
						productCount = pdc.PacketInfo.Products[0].Count;
					}
					else
					{
						foreach (SoldProductInfo spi in pdc.PacketInfo.Products)
						{
							productDesc += spi.ShortName+"\n";
							brandDesc += BrandInfo.GetBrand(spi.BrandId).Name + "\n";
							productCount += spi.Count;
						}
					}
					
					OrderLib.Order order = MainForm.Instance.GetOrder(pdc.PacketInfo.OrderId);
					Trace.Assert(null != order);
					
					string[] provinceCity = pdc.PacketInfo.ProvinceCityCn.Split(' ');
					string province = provinceCity[0];
					string city = provinceCity.Length >= 2 ? pdc.PacketInfo.ProvinceCityCn.Remove(0, province.Length).Trim() : provinceCity[0];

					writer.WriteLine(string.Format(
						"{0},{1},{2},'{3},{4},{5},'{6},{7},{8},{9},{10},'{11},'{12},'{13},{14},{15},{16},{17},{18},{19},{20},{21}", 
						string.Empty, "Rechnergigant", "Meissnerweg 59", "17655443697", "Germany", "Darmstadt", "64289",
						order.RecipientName, pdc.PacketInfo.FullAddress.Replace(",", "，"), province, city, pdc.PacketInfo.PostCode, pdc.PacketInfo.PhoneNumber, pdc.PacketInfo.ExportItems[0].ExportItemString,
						string.Empty, string.Empty, string.Empty, string.Empty,
						productDesc, brandDesc, pdc.PacketInfo.Products[0].Specification, productCount));
				}
				
				writer.Close();
				//writerBaishi.Close();
				//writerInner.Close();
				
				DialogResult dr = MessageBox.Show(
					this,
					"请确认所有输出文档正确.\n出单状态同步到服务器后, 订单状态将永久被修改, 此操作不可逆.\n这些订单以后将不会出现在<已付款>订单中.\n是否确定要修改订单状态并同步到服务器?", this.Text, 
					MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (DialogResult.No == dr)
					return;
					
				if (null != this.OnUpdateStatus)
					this.OnUpdateStatus(this, EventArgs.Empty);
			}

			Cursor.Current = Cursors.Default;
		}
	}
}