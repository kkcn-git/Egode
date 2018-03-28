using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Egode.Utility
{
	public partial class PacketCountAnalyseForm : Form
	{
		private class KgCount
		{
			private float _kg;
			private int _count;
			
			public KgCount(float kg)
			{
				_kg = kg;
			}
			
			public float Kg
			{
				get { return _kg; }
			}
			
			public int Count
			{
				get { return _count; }
				set { _count = value; }
			}
		}
	
		public PacketCountAnalyseForm()
		{
			InitializeComponent();
		}

		private void btnGo_Click(object sender, EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
		
			List<KgCount> kcs = new List<KgCount>();
		
			foreach (string s in txtSource.Lines)
			{
				string[] kgCountInfos = s.Split('+');
				foreach (string kgCountInfo in kgCountInfos)
				{
					string[] kgCount = kgCountInfo.Split('*');
					float kg = float.Parse(kgCount[0].Replace("kg", string.Empty));
					int count = int.Parse(kgCount[1]);
					KgCount kc = GetKgCount(kcs, kg);
					if (null == kc)
					{
						kc = new KgCount(kg);
						kcs.Add(kc);
					}
					kc.Count += count;
				}
			}
			
			StringBuilder sb = new StringBuilder();
			int totalCount = 0;
			foreach (KgCount k in kcs)
			{
				sb.Append(string.Format("{0}*{1}+", k.Kg.ToString("0.0"), k.Count));
				totalCount += k.Count;
			}
			sb.Remove(sb.Length - 1, 1);
			sb.Append(string.Format(", ({0})", totalCount));
			txtResult.Text = sb.ToString();
			
			Cursor.Current = Cursors.Default;
		}
		
		private KgCount GetKgCount(List<KgCount> kgCounts, float kg)
		{
			foreach (KgCount kc in kgCounts)
			{
				if (kc.Kg == kg)
					return kc;
			}
			return null;
		}
	}
}