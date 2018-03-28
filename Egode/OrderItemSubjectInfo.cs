using System;
using System.Collections.Generic;
using System.Text;

namespace Egode
{
	public class OrderItemSubjectInfo
	{
		private readonly string _id;
		private readonly string _subject;
		private static List<OrderItemSubjectInfo> _subjectInfos;

		public OrderItemSubjectInfo(string id, string subject)
		{
			_id = id;
			_subject = subject;
		}

		public string Id
		{
			get { return _id; }
		}

		public string Subject
		{
			get { return _subject; }
		}

		public static List<OrderItemSubjectInfo> SubjectInfos
		{
			get
			{
				if (null == _subjectInfos)
					_subjectInfos = new List<OrderItemSubjectInfo>();
				return _subjectInfos;
			}
		}

		private static OrderItemSubjectInfo GetSubjectById(string id)
		{
			foreach (OrderItemSubjectInfo si in _subjectInfos)
			{
				if (si.Id.Equals(id))
					return si;
			}
			return null;
		}
	}
}
