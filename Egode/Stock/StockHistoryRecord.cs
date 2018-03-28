using System;
using System.Collections.Generic;
using System.Text;

namespace Egode
{
	public class StockHistoryRecord
	{
		private readonly string _op;
		private readonly DateTime _datetime;
		private readonly string _productId;
		private readonly int _count;
		private readonly string _fromto;
		private readonly string _comment;
		
		public StockHistoryRecord(string op, DateTime datetime, string productId, int count, string fromto, string comment)
		{
			_op = op;
			_datetime = datetime;
			_productId = productId;
			_count = count;
			_fromto = fromto;
			_comment = comment;
		}
		
		public string Op
		{
			get { return _op; }
		}
		
		public DateTime DateTime
		{
			get { return _datetime; }
		}
		
		public string ProductId
		{
			get { return _productId; }
		}
		
		public int Count
		{
			get { return _count; }
		}
		
		public string FromTo
		{
			get { return _fromto; }
		}
		
		public string Comment
		{
			get { return _comment; }
		}
		
		public bool Match(string keyword)
		{
			string[] metaKeywords = keyword.Replace(" OR ", " or ").Split(" or ".ToCharArray());
		
			foreach (string k in metaKeywords)
			{
				if (string.IsNullOrEmpty(k))
					continue;
			
				if (_op.Contains(k))
					return true;
				if (_productId.Contains(k))
					return true;
				if (_fromto.Contains(k))
					return true;
				if (_comment.Contains(k))
					return true;
			}
			return false;
		}
	}
}