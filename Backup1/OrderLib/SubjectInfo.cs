// Removed by KK on 2016/06/05.
// Subject information is not necessary any more.
// The SKU code will be used to identity item uniquly.
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace OrderLib
//{
//    public class SubjectInfo
//    {
//        private readonly string _id;
//        private readonly string _subject;
//        private static List<SubjectInfo> _subjectInfos;
//        public static event EventHandler OnNewSubject;

//        public SubjectInfo(string id, string subject)
//        {
//            _id = id;
//            _subject = subject;
//        }

//        public string Id
//        {
//            get { return _id; }
//        }

//        public string Subject
//        {
//            get { return _subject; }
//        }
		
//        public static List<SubjectInfo> SubjectInfos
//        {
//            get { return _subjectInfos; }
//        }
		
//        // the stream contains data of subjects.
//        // Each line represents a subject. The format is:
//        // id,subject
//        public static int InitializeSubjectInfos(string buf)
//        {
//            if (null == buf || buf.Length <= 0)
//                return 0;
			
//            string[] lines = buf.Trim().Split(new char[]{'\r','\n'});
//            if (null == lines || lines.Length <= 0)
//                return 0;
			
//            _subjectInfos = new List<SubjectInfo>();
			
//            foreach (string line in lines)
//            {
//                if (string.IsNullOrEmpty(line.Trim()))
//                    continue;
				
//                string[] infos = line.Trim().Split(',');
//                if (null == infos || infos.Length < 2)
//                    continue;
				
//                _subjectInfos.Add(new SubjectInfo(infos[0].Trim(), infos[1].Trim()));
//            }
			
//            return _subjectInfos.Count;
//        }

//        private static SubjectInfo GetSubjectInfo(string subject)
//        {
//            foreach (SubjectInfo si in _subjectInfos)
//            {
//                if (si.Subject.Equals(subject))
//                    return si;
//            }
//            return null;
//        }

//        public static SubjectInfo GetSubjectInfoById(string id)
//        {
//            foreach (SubjectInfo si in _subjectInfos)
//            {
//                if (si.Id.Equals(id))
//                    return si;
//            }
//            return null;
//        }

//        // return id for new subject.
//        private static SubjectInfo NewSubject(string subject)
//        {
//            SubjectInfo newsi = GetSubjectInfo(subject);
//            if (null != newsi)
//                return newsi;

//            int id = 0;
//            while (null != GetSubjectInfoById((++id).ToString("0000")))
//                ;

//            newsi = new SubjectInfo(id.ToString("0000"), subject);
//            _subjectInfos.Add(newsi);
			
//            if (null != OnNewSubject)
//                OnNewSubject(null, EventArgs.Empty);

//            return newsi;
//        }

//        // Returns the id for specified subject.
//        // If the subject does not exist it will create a new one.
//        public static string GetSubjectId(string subject)
//        {
//            SubjectInfo si = GetSubjectInfo(subject.Trim());
//            if (null == si)
//                si = NewSubject(subject.Trim());
//            return si.Id;
//        }
//    }
//}