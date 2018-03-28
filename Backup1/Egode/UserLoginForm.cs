using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Security;
using System.Security.Cryptography;

namespace Egode
{
	public partial class UserLoginForm : Form
	{
		public UserLoginForm()
		{
			InitializeComponent();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;

			if (string.IsNullOrEmpty(txtUsername.Text))
			{
				MessageBox.Show(this, "请输入用户名.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (string.IsNullOrEmpty(txtPassword.Text))
			{
				MessageBox.Show(this, "请输入登录口令.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			
			try
			{
				WebClient wc = new WebClient();
				byte[] buf = wc.DownloadData(Common.URL_USERS);
				string xml = Encoding.UTF8.GetString(buf);
				//xml = xml.Replace("color=\"#", "color=\"");
				ParseUsers(xml);
				
				User u = User.GetUser(txtUsername.Text.Trim());
				if (null == u)
				{
					MessageBox.Show(this, "用户名错误.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (!CalcMd5(txtPassword.Text).Equals(u.Password))
				{
					MessageBox.Show(this, "口令错误.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				Settings.Operator = u.Username;
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch (WebException)
			{
				MessageBox.Show(this, "无法连接服务器.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, "发生未知错误. 请联系KK...\n" + ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			Cursor.Current = Cursors.Default;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
		
		private void ParseUsers(string xml)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			
			XmlNodeList nlUsers = doc.SelectNodes(".//user");
			if (null == nlUsers || nlUsers.Count <= 0)
				return;
			
			foreach (XmlNode nodeUser in nlUsers)
			{
				string username = nodeUser.Attributes.GetNamedItem("name").Value;
				string pw = nodeUser.Attributes.GetNamedItem("pw").Value;
				string ww = (null == nodeUser.Attributes.GetNamedItem("ww") ? string.Empty : nodeUser.Attributes.GetNamedItem("ww").Value);
				string wwpw = (null == nodeUser.Attributes.GetNamedItem("wwpw") ? string.Empty : nodeUser.Attributes.GetNamedItem("wwpw").Value);
				string displayName = nodeUser.Attributes.GetNamedItem("display_name").Value;
				string color = nodeUser.Attributes.GetNamedItem("color").Value;
				string permission = nodeUser.Attributes.GetNamedItem("permission").Value;
				User u = new User(username, pw, ww, wwpw, displayName, color, permission);
				User.Users.Add(u);
			}
		}

		public static string CalcMd5(string s)
		{
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			byte[] buf = Encoding.Default.GetBytes(s);
			byte[] hash = md5.ComputeHash(buf);
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hash.Length; i++)
				sb.AppendFormat("{0:x2}", hash[i]);
			return sb.ToString();
		}
	}
}