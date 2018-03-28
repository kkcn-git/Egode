using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Egode
{
	class PrepareHistory
	{
		private static List<PrepareHistory> _prepareHistoryList;
		private static List<PrepareHistory> _ningboPrepareHistoryList;
	
		private readonly DateTime _date;
		private readonly string _op;
		private readonly string _orderId;
		private readonly string _shop;
		
		public PrepareHistory(DateTime date, string op, string orderId, string shop)
		{
			_date = date;
			_op = op;
			_orderId = orderId;
			_shop = shop;
		}

		public static List<PrepareHistory> PrepareHistoryList
		{
			get
			{
				if (null == _prepareHistoryList)
					_prepareHistoryList = new List<PrepareHistory>();
				return _prepareHistoryList;
			}
		}

		public static List<PrepareHistory> NingboPrepareHistoryList
		{
			get
			{
				if (null == _ningboPrepareHistoryList)
					_ningboPrepareHistoryList = new List<PrepareHistory>();
				return _ningboPrepareHistoryList;
			}
		}

		public DateTime Date
		{
			get { return _date; }
		}
		
		public string Operator
		{
			get { return _op; }
		}
		
		public string OrderId
		{
			get { return _orderId; }
		}
		
		public string Shop
		{
			get { return _shop; }
		}

		public static int Load(string xml)
		{
			XmlDocument xmldoc = new XmlDocument();
			xmldoc.LoadXml(xml);

			XmlNode nodeHistory = xmldoc.SelectSingleNode(".//history");
			if (null == nodeHistory)
				return -1;

			XmlNodeList nlHistory = nodeHistory.SelectNodes(".//h");
			if (null == nlHistory || nlHistory.Count <= 0)
				return -1;

			foreach (XmlNode nodeH in nlHistory)
			{
				DateTime date = DateTime.Parse(nodeH.Attributes.GetNamedItem("date").InnerText);
				string op = nodeH.Attributes.GetNamedItem("op").InnerText;
				string orderId = nodeH.Attributes.GetNamedItem("order_id").InnerText;
				string shop = nodeH.Attributes.GetNamedItem("shop").InnerText;

				PrepareHistoryList.Add(new PrepareHistory(date, op, orderId, shop));
			}

			return nlHistory.Count;
		}

		public static bool Exists(string orderId)
		{
			return null != Get(orderId);
		}

		public static PrepareHistory Get(string orderId)
		{
			if (string.IsNullOrEmpty(orderId))
				return null;
			if (null == _prepareHistoryList)
				return null;

			foreach (PrepareHistory h in _prepareHistoryList)
			{
				if (h.OrderId.Equals(orderId))
					return h;
			}
			return null;
		}
		
		public static int LoadNingbo(string xml)
		{
			XmlDocument xmldoc = new XmlDocument();
			xmldoc.LoadXml(xml);

			XmlNode nodeHistory = xmldoc.SelectSingleNode(".//history");
			if (null == nodeHistory)
				return -1;

			XmlNodeList nlHistory = nodeHistory.SelectNodes(".//h");
			if (null == nlHistory || nlHistory.Count <= 0)
				return -1;

			foreach (XmlNode nodeH in nlHistory)
			{
				DateTime date = DateTime.Parse(nodeH.Attributes.GetNamedItem("date").InnerText);
				string op = nodeH.Attributes.GetNamedItem("op").InnerText;
				string orderId = nodeH.Attributes.GetNamedItem("order_id").InnerText;
				string shop = nodeH.Attributes.GetNamedItem("shop").InnerText;

				NingboPrepareHistoryList.Add(new PrepareHistory(date, op, orderId, shop));
			}

			return nlHistory.Count;
		}

		public static bool ExistsNingbo(string orderId)
		{
			return null != GetNingbo(orderId);
		}

		public static PrepareHistory GetNingbo(string orderId)
		{
			if (string.IsNullOrEmpty(orderId))
				return null;
			if (null == _ningboPrepareHistoryList)
				return null;

			foreach (PrepareHistory h in _ningboPrepareHistoryList)
			{
				if (h.OrderId.Equals(orderId))
					return h;
			}
			return null;
		}
	}
}
