using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace Egode
{
	[Serializable()]
	public class Settings
	{
		public enum UiTypes
		{
			Shanghai = 1,
			Deutschland = 2,
			Full = 3
		}

		public enum OrderDownloadModes
		{
			Speed = 0,
			Full = 1
		}

		private static Settings _instance;
		
		private static string _operator = "kk";
	
		private bool _showDeal;
		private bool _showPaid = true;
		private bool _showPrepared;
		private bool _showSent;
		private bool _showSucceeded;
		private bool _showClosed;
		private int _durationFilterIndex = 0;
		private int _shippingOriginFilterIndex = 0;
		private UiTypes _uiType = UiTypes.Full;// UiTypes.Deutschland;
		private string _logisticsDelay = "10,3";
		private int _logisticsDays = 26; // 直邮点发货后, 自动延长到最少该天数.
		private bool _showBooking;
		private string _ytoPrinter;
		private string _sfPrinter;
		private string _sfNewPrinter;
		private bool _autoCloseAfterSignin = false;
		private OrderDownloadModes _orderDownloadMode = OrderDownloadModes.Speed;
		private string _recentYtoBillNumber;
		private string _recentZtoBillNumber;
		private string _recentYundaBillNumber;
		private string _itemNameOnBill = "生活用品";
		
		private bool _autoSelectShipment = true; // 自动根据地区选择快递公司.
		private OrderLib.ShipmentCompanies _defaultShipment = OrderLib.ShipmentCompanies.Yunda; // 默认首选快递公司. 仅在_autoSelectShipment为false时有效.

		public static Settings Instance
		{
			get 
			{ 
				if (null == _instance)
					_instance = Settings.Load();
				return _instance; 
			}
		}

		~Settings()
		{
			_instance.Save();
		}
		
		public static string Operator
		{
			get { return _operator; }
			set { _operator = value; }
		}

		public bool ShowDeal
		{
			get { return _showDeal; }
			set { _showDeal = value; }
		}

		public bool ShowPaid
		{
			get { return _showPaid; }
			set { _showPaid = value; }
		}

		public bool ShowPrepared
		{
			get { return _showPrepared; }
			set { _showPrepared = value; }
		}

		public bool ShowSent
		{
			get { return _showSent; }
			set { _showSent = value; }
		}

		public bool ShowSucceeded
		{
			get { return _showSucceeded; }
			set { _showSucceeded = value; }
		}

		public bool ShowClosed
		{
			get { return _showClosed; }
			set { _showClosed = value; }
		}
		
		public int DurationFilterIndex
		{
			get { return _durationFilterIndex; }
			set { _durationFilterIndex = value; }
		}
		
		public int ShippingOriginFilterIndex
		{
			get { return _shippingOriginFilterIndex; }
			set { _shippingOriginFilterIndex = value; }
		}
		
		public UiTypes UiType
		{
			get { return _uiType; }
			set { _uiType = value; }
		}
		
		public string LogisticsDelay
		{
			get { return _logisticsDelay; }
			set { _logisticsDelay = value; }
		}
		
		public int LogisticsDays
		{
			get { return _logisticsDays; }
			set { _logisticsDays = value; }
		}
		
		public bool ShowBooking
		{
			get { return _showBooking; }
			set { _showBooking = value; }
		}
		
		public string YtoPrinter
		{
			get { return _ytoPrinter; }
			set { _ytoPrinter = value; }
		}
		
		public string SfPrinter
		{
			get { return _sfPrinter; }
			set { _sfPrinter = value; }
		}
		
		public string SfNewPrinter
		{
			get { return _sfNewPrinter; }
			set { _sfNewPrinter = value; }
		}
		
		public bool AutoCloseAfterSignin
		{
			get { return _autoCloseAfterSignin; }
			set { _autoCloseAfterSignin = value; }
		}
		
		public OrderDownloadModes OrderDownloadMode
		{
			get { return _orderDownloadMode; }	
			set { _orderDownloadMode = value; }
		}
		
		public string RecentYtoBillNumber
		{
			get { return _recentYtoBillNumber; }
			set { _recentYtoBillNumber = value; }
		}

		public string RecentZtoBillNumber
		{
			get { return _recentZtoBillNumber; }
			set { _recentZtoBillNumber = value; }
		}

		public string RecentYundaBillNumber
		{
			get { return _recentYundaBillNumber; }
			set { _recentYundaBillNumber = value; }
		}
		
		public string ItemNameOnBill
		{
			get { return _itemNameOnBill; }
			set { _itemNameOnBill = value; }
		}
		
		public bool AutoSelectShipment
		{
			get { return _autoSelectShipment; }
			set { _autoSelectShipment = value; }
		}
		
		public OrderLib.ShipmentCompanies DefaultShipment
		{
			get { return _defaultShipment; }
			set { _defaultShipment = value; }
		}

		private static string Filename
		{
			get { return Path.Combine(Directory.GetParent(Application.ExecutablePath).FullName, "settings.xml"); }
		}
	
		public void Save()
		{
			try
			{
				XmlTextWriter writer = new XmlTextWriter(Settings.Filename, Encoding.Unicode);
				XmlSerializer serializer = new XmlSerializer(this.GetType());
				serializer.Serialize(writer, this);
				writer.Close();
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
			}
		}

		public static Settings Load()
		{
			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(Settings));

				if (File.Exists(Settings.Filename))
				{
					XmlTextReader reader = new XmlTextReader(Settings.Filename);
					Settings s = (Settings)serializer.Deserialize(reader);
					reader.Close();
					return s;
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
			}

			return new Settings();
		}
	}
}