using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;

namespace Yunda
{
	internal class Excel
	{
		public enum OledbVersions
		{
			OLEDB40 = 0,
			OLEDB12 = 1
		}
	
		private OleDbConnection _conn;

		public Excel(string filename, OledbVersions oledbVer) : this(filename, false, oledbVer)
		{
		}
		
		public Excel(string filename, bool HDR, OledbVersions oledbVer)
		{
			string connStr = string.Empty;
			
			if (oledbVer == OledbVersions.OLEDB40)
			{
				connStr = string.Format(
					"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1};'",
					filename, HDR ? "YES" : "NO");
			}
			else if (oledbVer == OledbVersions.OLEDB12)
			{
				connStr = string.Format(
					"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR={1};IMEX=1\"",
					filename, HDR ? "YES" : "NO");
			}
			Trace.WriteLine("Excel.ctor: connection string: " + connStr);
			_conn = new OleDbConnection(connStr);
			_conn.Open();
		}
		
		~Excel()
		{
			//this.Close();
		}
		
		public void Close()
		{
			if (null != _conn && _conn.State != ConnectionState.Closed)
				_conn.Close();
			_conn = null;
		}
		
		public bool Opened
		{
			get { return (null != _conn && _conn.State == ConnectionState.Open); }
		}

		// range: like "D2:H5". null for all range.
		public bool Insert(string table, string range, object[] values)
		{
			if (null == values || values.Length <= 0)
				return false;
		
			StringBuilder sb = new StringBuilder();
			foreach (object o in values)
				sb.Append(string.Format("'{0}',", o.ToString()));
			if (sb.ToString().EndsWith(","))
				sb.Remove(sb.Length - 1, 1);

			return Insert(table, range, sb.ToString());
		}

		// range: like "D2:H5". null for all range.
		// values: v1,v2,v3... Do not contain starting "(" and ending ")";
		public bool Insert(string table, string range, string values)
		{
			if (!this.Opened)
				return false;

			try
			{
				string sql = string.Format("insert into [{0}${1}] values({2})", table, range, values);
				OleDbCommand cmd = new OleDbCommand(sql, _conn);
				return cmd.ExecuteNonQuery() > 0;
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
			}

			return false;
		}
		
		public DataSet Get(string table, string range)
		{
			if (!this.Opened)
				return null;

			try
			{
				if (table.StartsWith("'"))
					table = table.Remove(0, 1);
				if (table.EndsWith("'"))
					table = table.Remove(table.Length - 1, 1);
				if (table.EndsWith("$"))
					table = table.Remove(table.Length - 1, 1);
				string sql = string.Format("select * from [{0}${1}]", table, range);
				OleDbCommand cmd = new OleDbCommand(sql, _conn);
				OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
				DataSet ds = new DataSet();
				adapter.Fill(ds);
				return ds;
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
			}
			
			return null;
		}
		
		public bool Update(string table, string field, string value, string conditionField, string conditionValue)
		{
			if (!this.Opened)
				return false;

			string sql = string.Format("update [{0}$] set {1}=\'{2}' where {3}='{4}'", table, field, value, conditionField, conditionValue);
			OleDbCommand cmd = new OleDbCommand(sql, _conn);
			int i = cmd.ExecuteNonQuery();
			return (i > 0);
		}

		public List<string> GetTableNames()
		{
			if (!this.Opened)
				return null;
			
			List<string> tableNames = new List<string>();

			DataTable schemaTable = _conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
			foreach (DataRow dr in schemaTable.Rows)
			{
				string s = dr["TABLE_NAME"].ToString().Trim();
				if (s.EndsWith("$"))
					s = s.Substring(0, s.Length - 1);
				tableNames.Add(s);
			}
			
			return tableNames;
		}

		public bool TableExists(string tableName)
		{
			if (!this.Opened)
				return false;
		
			DataTable schemaTable = _conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
			foreach (DataRow dr in schemaTable.Rows)
			{
				string s = dr["TABLE_NAME"].ToString().Trim();
				if (s.EndsWith("$"))
					s = s.Substring(0, s.Length - 1);
				if (s.Equals(tableName))
					return true;
			}
			
			return false;
		}
		
		// item in fields: "filedname datatype"
		public bool CreateTable(string tableName, string[] fields)
		{
			if (TableExists(tableName))
				return true;
			
			StringBuilder sb = new StringBuilder();
			foreach (string field in fields)
			{
				sb.Append(fields);
				sb.Append(",");
			}
			
			if (sb.ToString().EndsWith(","))
				sb.Remove(sb.Length - 1, 1);

			string sql = string.Format("create table [{0}] ({1})", sb.ToString());
			OleDbCommand cmd = new OleDbCommand(sql, _conn);
			int i = cmd.ExecuteNonQuery();
			return i > 0;
		}
		
		// 获得Excel表格中的列索引.
		// startColumn: 起始column, 用作计算基准.
		// length: 在上述起始column后的第几个column. 如果为0则就是startColumn本身.
		// Excel Column索引规则: A,B,C...Z,AA,AB,AC...AZ,BA,BB,BC...BZ,CA... 
		// 考虑到实际应用, 假定最多2位.即上限为ZZ.
		// 不区分大小写, 返回值为大写.
		public static string GetColumnIndex(string startColumn, int length)
		{
			if (string.IsNullOrEmpty(startColumn))
				return string.Empty;
			startColumn = startColumn.ToUpper();

			//
			// 检测是否全部由A~Z组成. 待完善.
			//

			// 计算对应的10进制值.
			double decimalValue = 0;
			char[] chars = startColumn.ToCharArray();

			for (int i = 0; i < chars.Length; i++)
				decimalValue += (chars[i] - 'A') * Math.Pow(26, chars.Length - i - 1);
			
			decimalValue += length;
			
			char lower = (char)('A' + (decimalValue % 26));
			string result = new string(new char[]{lower});
			
			if (decimalValue > 25)
			{
				char higher = (char)('A' + (int)((decimalValue - 26) / 26));
				result = new string(new char[]{higher}) + result;
			}
			
			return result;
		}
	}
}