using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace Egode
{
	public partial class WaitingFormBase : Form
	{
		private Thread _workingThread;
		private bool _completed = false;
		private string _info;
		private System.Windows.Forms.Timer _tmrDelayClose;
		private System.Windows.Forms.Timer _tmrIAmWorking;
		
		private string[] _status = new string[]{"¡ô¡ó¡ó¡ó¡ó¡ó", "¡ó¡ô¡ó¡ó¡ó¡ó", "¡ó¡ó¡ô¡ó¡ó¡ó", "¡ó¡ó¡ó¡ô¡ó¡ó", "¡ó¡ó¡ó¡ó¡ô¡ó", "¡ó¡ó¡ó¡ó¡ó¡ô"};
		private int _currentStatus = 0;

		public WaitingFormBase()
		{
			InitializeComponent();
			
			_tmrIAmWorking = new System.Windows.Forms.Timer();
			_tmrIAmWorking.Interval = 500;
			_tmrIAmWorking.Tick += new EventHandler(_tmrIAmWorking_Tick);
			_tmrIAmWorking.Start();

			_tmrDelayClose = new System.Windows.Forms.Timer();
			_tmrDelayClose.Interval = 100;
			_tmrDelayClose.Tick += new EventHandler(_tmr_Tick);
			_tmrDelayClose.Start();
		}

		void _tmrIAmWorking_Tick(object sender, EventArgs e)
		{
			if (_status.Length == ++_currentStatus)
				_currentStatus = 0;
			lblInfo.Text = string.Format("{0}\n{1}", _info, _status[_currentStatus]);
		}
		
		void _tmr_Tick(object sender, EventArgs e)
		{
			if (_completed)
				this.Close();
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			
			if (null != _workingThread)
			{
				_workingThread.IsBackground = true;
				_workingThread.Start();
			}
		}
		
		protected string Info
		{
			get { return _info; }
			set 
			{ 
				_info = value;
				lblInfo.Text = string.Format("{0}\n{1}", _info, _status[_currentStatus]);
			}
		}

		protected Thread WorkingThread
		{
			get { return _workingThread; }
			set { _workingThread = value; }
		}
		
		protected void Succeed()
		{
			this.DialogResult = DialogResult.OK;
			_completed = true;
		}
		
		protected void Fail()
		{
			this.DialogResult = DialogResult.No;
			_completed = true;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}