using System;
using System.Collections.Generic;
using System.Text;

namespace Egode
{
	class User
	{
		private readonly string _username;
		private readonly string _pw;
		private readonly string _ww;
		private readonly string _wwpw;
		private readonly string _displayName;
		private readonly string _color;
		private readonly string _permission;
		
		private static List<User> _users;

		public static List<User> Users
		{
			get
			{
				if (null == _users)
					_users = new List<User>();
				return _users;
			}
		}
		
		public static User GetUser(string username)
		{
			if (string.IsNullOrEmpty(username))
				return null;

			foreach (User u in User.Users)
			{
				if (u.Username.Equals(username))
					return u;
			}
			return null;
		}
		
		public static string GetDisplayName(string username)
		{
			User u = GetUser(username);
			if (null == u)
				return "<Unknown User>";
			return u.DisplayName;
		}
		
		public User(string username, string pw, string ww, string wwpw, string displayName, string color, string permission)
		{
			_username = username;
			_pw = pw;
			_ww = ww;
			_wwpw = wwpw;
			_displayName = displayName;
			_color = color;
			_permission = permission;	
		}
		
		public string Username
		{
			get { return _username; }
		}
		
		public string Password
		{
			get { return _pw; }
		}
		
		public string DisplayName
		{
			get { return _displayName; }
		}
		
		public string Color
		{
			get { return _color; }
		}
		
		public string Permission
		{
			get { return _permission; }
		}
		
		// index: zero-based.
		public string GetWW(int index)
		{
			if (string.IsNullOrEmpty(_ww))
				return string.Empty;
			
			string[] wws = _ww.Split(',');
			if (index > wws.Length-1)
				return string.Empty;
			return wws[index];
		}
		
		// index: zero-based.
		public string GetWWPW(int index)
		{
			if (string.IsNullOrEmpty(_wwpw))
				return string.Empty;
			
			string[] wwpws = _wwpw.Split(',');
			if (index > wwpws.Length-1)
				return string.Empty;
			
			string wwpw = string.Empty;
			for (int i = 1; i < wwpws[index].Length; i+=2)
				wwpw += wwpws[index][i];
			
			return wwpw;
		}
	}
}