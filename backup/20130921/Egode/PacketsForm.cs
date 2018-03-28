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
		private const string SUPERMARKET_EXCEL_OUTPUT_FILENAME = "DHL包裹信息单.xls";

		private const string RAINBOW_TEMPLATE_FILENAME = "template-rainbow.xls";
		private const string RAINBOW_EXCEL_OUTPUT_FILENAME = "(realkk)彩虹国际代购2013表格.xls";

		private const string DEALWORTHIER_TEMPLATE_FILENAME = "template-dealworthier.xls";
		private const string DEALWORTHIER_EXCEL_OUTPUT_FILENAME = "DHL-realkk.xls";

		private const string OUHUA_TEMPLATE_FILENAME = "template-ouhua.xls";
		private const string OUHUA_EXCEL_OUTPUT_FILENAME = "realkk-{0}.xls"; // e.g.: realkk-20130712.xls

		private const string PACKING_LIST_TEMPLATE = "template-packing-list.xls";
		private const string PACKING_LIST_OUTPUT_JHT = "发货清单-JHT-{0}.xls"; // {0}=yyyyMMdd
		private const string CHINESE_ADDRESSES_DOC = "中文地址-{0}.doc";

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
				List<PacketInfo> supermarkets = new List<PacketInfo>();
				List<PacketInfo> rainbows = new List<PacketInfo>();
				List<PacketInfo> dealworthiers = new List<PacketInfo>();
				List<PacketInfo> ouhuas = new List<PacketInfo>();
			
				foreach (PacketDetailsControl pdc in pnlPackets.Controls)
				{
					pdc.UpdatePacketInfo();
					
					switch (pdc.PacketInfo.Type)
					{
						case PacketTypes.Supermarket:
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
				
				string f1=string.Empty, f2=string.Empty, f3=string.Empty, f4=string.Empty, f5=string.Empty, f6=string.Empty;
				
				if (supermarkets.Count > 0)
					f1 = GenerateSupermarketFiles(supermarkets, todayFolder);
				if (rainbows.Count > 0)
					f2 = GenerateRainbowFiles(rainbows, todayFolder);
				if (dealworthiers.Count > 0)
					f3 = GenerateDealworthierFiles(dealworthiers, todayFolder);
				if (ouhuas.Count > 0)
					f4 = GenerateOuhuaFiles(ouhuas, todayFolder);

				f5 = GeneratePackingListExcelFile(_packetInfos, todayFolder);
				f6 = GenerateChineseAddressDoc(_packetInfos, todayFolder);
				GetPackgingResult(supermarkets, rainbows, dealworthiers, ouhuas, f1, f2, f3, f4, f5, f6).ShowDialog(this);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
			}
			
			Cursor.Current = Cursors.Default;
		}
		
		private PacketResultForm GetPackgingResult(
			List<PacketInfo> supermarkets, List<PacketInfo> rainbows, List<PacketInfo> dealworthiers, List<PacketInfo> ouhuas,
			string supermarketFilename, string rainbowFilename, string dealworthierFilename, string ouhuaFilename,
			string packingListFilename, string addressFilename)
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

			int totalCount = supermarkets.Count + rainbows.Count + dealworthiers.Count + ouhuas.Count;
			int totalPrice = supermarketTotalPrice + rainbowTotalPrice + dealworthierTotalPrice + ouhuaTotalPrice;
			PacketResultForm prf = new PacketResultForm(
				sbSupermarket.ToString(), sbRainbow.ToString(), sbDealworthier.ToString(), sbOuhua.ToString(),
				string.Format("共计{0}个, 总价￥{1:0.00}", totalCount, totalPrice),
				supermarketFilename, rainbowFilename, dealworthierFilename, ouhuaFilename,
				packingListFilename, addressFilename);
			prf.OnUpdateStatus += new EventHandler(prf_OnUpdateStatus);
			return prf;
		}

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
				Excel supermarketExcel = new Excel(destExcelFile);

				try
				{
					for (int i = 0; i < packetInfos.Count; i++)
					{
						PacketInfo pi = packetInfos[i];	
						if (!supermarketExcel.Insert("Sheet1", string.Empty, CreatePacketInfoSupermarketValues(i+1, pi)))
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
				
				Excel rainbowExcel = new Excel(destExcelFile);
				
				try
				{
					string packTitle = string.Empty;
					string pingzhangEmail = string.Empty;
					string realkk = string.Empty;
					string JHT = string.Empty;
					string OttoBrennerStr4a = string.Empty;
					string PLZ47877 = string.Empty;
					string OrtWillich = string.Empty;
					string TelNr021548839989 = string.Empty;
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
						JHT += "'JHT International GmbH'" + (i >= packetInfos.Count-1 ? string.Empty : ",");
						OttoBrennerStr4a += "'Otto Brenner Str.4a'" + (i >= packetInfos.Count-1 ? string.Empty : ",");
						PLZ47877 += "'47877'" + (i >= packetInfos.Count-1 ? string.Empty : ",");
						OrtWillich += "'Willich'" + (i >= packetInfos.Count-1 ? string.Empty : ",");
						TelNr021548839989 += "'02154 8839989'" + (i >= packetInfos.Count-1 ? string.Empty : ",");
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
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 5, endColumn), JHT);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 6, endColumn), OttoBrennerStr4a);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 7, endColumn), PLZ47877);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 8, endColumn), OrtWillich);
					rainbowExcel.Insert("Sheet1", string.Format("C{0}:{1}{0}", 9, endColumn), TelNr021548839989);
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

				Excel excel = new Excel(destExcelFile);

				try
				{
					string packTitle = string.Empty;
					string realkk = string.Empty;
					string pingzhangEmail = string.Empty;
					string weight = string.Empty;
					string JHT = string.Empty;
					string OttoBrennerStr = string.Empty;
					string str4a = string.Empty;
					string PLZ47877 = string.Empty;
					string OrtWillich = string.Empty;
					string TelNr021548839989 = string.Empty;
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
						JHT += "'JHT International GmbH'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						OttoBrennerStr += "'Otto Brenner Str.'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						str4a += "'4a'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						PLZ47877 += "'47877'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						OrtWillich += "'Willich'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						TelNr021548839989 += "'02154 8839989'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
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
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), JHT);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), OttoBrennerStr);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), str4a);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), PLZ47877);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), OrtWillich);
					excel.Insert("DHL Premium Sheet", string.Format("D{0}:{1}{0}", row++, endColumn), TelNr021548839989);
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
						"Create excel file for Ohua failed.\nMaybe rainbow template file missed.\nMake sure the template file exists in the same folder with the executable file.",
						this.Text,
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return string.Empty;
				}
				#endregion

				Excel excel = new Excel(destExcelFile);

				try
				{
					string packTitle = string.Empty;
					string realkk = string.Empty;
					string pingzhangEmail = string.Empty;
					string weight = string.Empty;
					string JHT = string.Empty;
					string OttoBrennerStr = string.Empty;
					string str4a = string.Empty;
					string PLZ47877 = string.Empty;
					string OrtWillich = string.Empty;
					string TelNr021548839989 = string.Empty;
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
						JHT += "'JHT International GmbH'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						OttoBrennerStr += "'Otto Brenner Str.'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						str4a += "'4a'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						PLZ47877 += "'47877'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						OrtWillich += "'Willich'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
						TelNr021548839989 += "'02154 8839989'" + (i >= packetInfos.Count - 1 ? string.Empty : ",");
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
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), JHT);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), OttoBrennerStr);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), str4a);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), PLZ47877);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), OrtWillich);
					excel.Insert("DHL申请表", string.Format("D{0}:{1}{0}", row++, endColumn), TelNr021548839989);
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
		
		private string GeneratePackingListExcelFile(List<PacketInfo> packetInfos, string folder)
		{
			// Generate list for JHT.
			string destPackingListJHT = CreateOutputFile(
				Directory.GetParent(Application.ExecutablePath).FullName, PACKING_LIST_TEMPLATE,
				folder, string.Format(PACKING_LIST_OUTPUT_JHT, DateTime.Now.ToString("yyyyMMdd")));
			if (string.IsNullOrEmpty(destPackingListJHT))
			{
				#region error message
				MessageBox.Show(
					this,
					"Create excel file for shipping list for JHT failed.\nMaybe the template file missed.\nMake sure the template file exists in the same folder with the executable file.",
					this.Text,
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				#endregion
				return string.Empty;
			}

			Excel excelPackingListJHT = new Excel(destPackingListJHT);

			try
			{
				for (int i = 0; i < packetInfos.Count; i++)
				{
					PacketInfo pi = packetInfos[i];
					string[] products = pi.ProductInfo.Split(new char[]{'\r','\n'});
					
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
							productDetails.Length >= 3 ? productDetails[2].Trim() : string.Empty};
							
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
				
				foreach (PacketInfo pi in packetInfos)
				{
					rtb.Text += "寄:\n";
					rtb.Text += string.Format("邮编: {0}\n", pi.PostCode);
					rtb.Text += string.Format("地址: {0} {1}\n", pi.ProvinceCityCn, pi.AddressCn);
					rtb.Text += string.Format("收件人: {0}\n", pi.RecipientNameCn);
					rtb.Text += string.Format("联系电话: {0}\n", pi.PhoneNumber);
					rtb.Text += "\n\n\n";
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
		
		void prf_OnUpdateStatus(object sender, EventArgs e)
		{
			if (null != this.OnUpdateStatus)
				this.OnUpdateStatus(this, EventArgs.Empty);
		}
	}
}