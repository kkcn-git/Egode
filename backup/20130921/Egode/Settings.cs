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
		private static Settings _instance;
	
		private bool _showDeal;
		private bool _showPaid = true;
		private bool _showPrepared;
		private bool _showSent;
		private bool _showSucceeded;
		private bool _showClosed;
		private int _durationFilterIndex = 0;
		private int _shippingOriginFilterIndex = 0;

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
					return (Settings)serializer.Deserialize(reader);
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