using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KmRobot
{
	class Win32
	{
		public struct RECT  //Win32汇编数据结构不用自己定义
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
		}

		[Flags]
		public enum MouseEventFlag : uint
		{
			Move = 0x0001,
			LeftDown = 0x0002,
			LeftUp = 0x0004,
			RightDown = 0x0008,
			RightUp = 0x0010,
			MiddleDown = 0x0020,
			MiddleUp = 0x0040,
			XDown = 0x0080,
			XUp = 0x0100,
			Wheel = 0x0800,
			VirtualDesk = 0x4000,
			Absolute = 0x8000
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			public Int32 x;
			public Int32 y;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct CURSORINFO
		{
			public Int32 cbSize;        // Specifies the size, in bytes, of the structure.
			// The caller must set this to Marshal.SizeOf(typeof(CURSORINFO)).
			public Int32 flags;         // Specifies the cursor state. This parameter can be one of the following values:
			//    0             The cursor is hidden.
			//    CURSOR_SHOWING    The cursor is showing.
			public IntPtr hCursor;          // Handle to the cursor.
			public POINT ptScreenPos;       // A POINT structure that receives the screen coordinates of the cursor.
		}

		public delegate bool CallBack(int hwnd, int lParam);
		
		public const int WM_CLOSE	= 0x10;
		public const int KEYEVENTF_KEYUP = 0X2;
		public const int VK_BACK = 0X8; // backspace
		public const int VK_LSHIFT = 0XA0;
		public const int VK_LCONTROL = 0XA2;
		public const int VK_LMENU = 0xA4;
		public const int VK_F4 = 0X73;

		[DllImport("user32.dll")]
		public static extern bool GetCursorInfo(out CURSORINFO pci);

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetDesktopWindow();
		
		[DllImport("user32.dll", EntryPoint = "GetDCEx", CharSet = CharSet.Auto, ExactSpelling = true)]
		public  static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, int flags);
		
		[DllImport("user32.dll", EntryPoint = "GetDC", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetDC(IntPtr hWnd);
		
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

		[DllImport("user32.dll")]
		public static extern int EnumWindows(CallBack lpfn, int lParam);

		[DllImport("user32.dll")]
		public static extern int EnumChildWindows(int hWndParent, CallBack lpfn, int lParam);

		[DllImport("user32.dll")]
		public static extern int GetWindowText(int hWnd, StringBuilder lpString, int nMaxCount);

		[DllImport("user32.dll")]
		public static extern int IsZoomed(int hWnd);

		[DllImport("user32.dll")]
		public static extern int GetParent(int hWnd);

		[DllImport("user32.dll")]
		public static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo);

		[DllImport("user32.dll")]
		public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		public static extern bool MessageBeep(uint uType);

		[DllImport("User32.dll", EntryPoint = "SendMessage")]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

		[DllImport("User32.dll", EntryPoint = "SetForegroundWindow")]
		public static extern int SetForegroundWindow(IntPtr hWnd);

		[DllImport("User32.dll", EntryPoint = "GetForegroundWindow")]
		public static extern int GetForegroundWindow();
		
		[DllImport("User32.dll", EntryPoint = "VkKeyScanExA")]
		public static extern short VkKeyScanEx(byte ch, int dwhkl);

		public static void LocateCursor(System.Drawing.Point p)
		{
			mouse_event(
				MouseEventFlag.Absolute | MouseEventFlag.Move,
				p.X * 65536 / Screen.PrimaryScreen.Bounds.Width,
				p.Y * 65536 / Screen.PrimaryScreen.Bounds.Height,
				0, UIntPtr.Zero);
		}
		
		public static void ClickLeftMouse()
		{
			Win32.mouse_event(Win32.MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
			System.Threading.Thread.Sleep(50);
			Win32.mouse_event(Win32.MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
			System.Threading.Thread.Sleep(50);
		}

		public static void ClickRightMouse()
		{
			Win32.mouse_event(Win32.MouseEventFlag.RightDown, 0, 0, 0, UIntPtr.Zero);
			System.Threading.Thread.Sleep(50);
			Win32.mouse_event(Win32.MouseEventFlag.RightUp, 0, 0, 0, UIntPtr.Zero);
			System.Threading.Thread.Sleep(50);
		}

		public static List<int> GetChildWindows(int hwnd)
		{
			List<int> childHwnds = new List<int>();
			GCHandle listHandle = GCHandle.Alloc(childHwnds);
			Win32.EnumChildWindows(hwnd, new Win32.CallBack(ChildWindowProcess), GCHandle.ToIntPtr(listHandle).ToInt32());
			//Trace.WriteLine(childHwnds.Count);
			return childHwnds;
		}

		private static bool ChildWindowProcess(int hwnd, int lParam)
		{
			List<int> childWindows = GCHandle.FromIntPtr(new IntPtr(lParam)).Target as List<int>;
			childWindows.Add(hwnd);
			//Trace.WriteLine("    --------");
			//Trace.WriteLine(string.Format("    ChildwindowProcess: {0}, {1}", hwnd, lParam));
			//Win32.RECT rc = new Win32.RECT();
			//Win32.GetWindowRect(new IntPtr(hwnd), ref rc);
			//Trace.WriteLine(string.Format("    left={0}, top={1}, right={2}, bottom={3}", rc.left, rc.top, rc.right, rc.bottom));

			//StringBuilder sb = new StringBuilder(1024);
			//Win32.GetWindowText(hwnd, sb, 1024);
			//Trace.WriteLine("    " + sb.ToString());
			//Trace.WriteLine("    --------");
			return true;
		}
		
		public static void PressKey(byte ch)
		{
			short vk = VkKeyScanEx(ch, 0);
			byte code = (byte)(vk & 0x00ff);

			if ((vk & 0x100) != 0)
				keybd_event(VK_LSHIFT, 0, 0, 0);

			keybd_event(code, 0, 0, 0);
			keybd_event(code, 0, KEYEVENTF_KEYUP, 0);

			if ((vk & 0x100) != 0)
				keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
		}
		
		public static void PressKey(int vk)
		{
			keybd_event((byte)vk, 0, 0, 0);
			keybd_event((byte)vk, 0, KEYEVENTF_KEYUP, 0);
		}
		
		public static void PressCtrlF4()
		{
			Win32.keybd_event(Win32.VK_LCONTROL, 0, 0, 0);
			System.Threading.Thread.Sleep(20);
			Application.DoEvents();
			Win32.keybd_event(Win32.VK_F4, 0, 0, 0);
			System.Threading.Thread.Sleep(20);
			Application.DoEvents();
			Win32.keybd_event(Win32.VK_F4, 0, Win32.KEYEVENTF_KEYUP, 0);
			System.Threading.Thread.Sleep(20);
			Application.DoEvents();
			Win32.keybd_event(Win32.VK_LCONTROL, 0, Win32.KEYEVENTF_KEYUP, 0);
			System.Threading.Thread.Sleep(20);
			Application.DoEvents();
		}

		public static void PressCtrlV()
		{
			Win32.keybd_event(Win32.VK_LCONTROL, 0, 0, 0);
			System.Threading.Thread.Sleep(20);
			Application.DoEvents();
			Win32.keybd_event((byte)'V', 0, 0, 0);
			System.Threading.Thread.Sleep(20);
			Application.DoEvents();
			Win32.keybd_event((byte)'V', 0, Win32.KEYEVENTF_KEYUP, 0);
			System.Threading.Thread.Sleep(20);
			Application.DoEvents();
			Win32.keybd_event(Win32.VK_LCONTROL, 0, Win32.KEYEVENTF_KEYUP, 0);
			System.Threading.Thread.Sleep(20);
			Application.DoEvents();
		}
	}
}