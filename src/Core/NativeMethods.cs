using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Logo
{
	static class NativeMethods
	{
		public static int SW_SHOWNORMAL = 1;
		public static int SW_SHOWNOACTIVATE = 4;

		[DllImport("user32.dll")]
		public static extern bool MoveWindow(IntPtr hWnd, double x, double y, int nWidth, int nHeight, bool bRepaint);
		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, double x);
		[DllImport("user32.dll")]
		public static extern bool SetFocus(IntPtr hWnd);
	}

	static class SendKeys
	{
		[DllImport("user32.dll")]
		private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

		private const int KEYEVENTF_EXTENDEDKEY = 1;
		private const int KEYEVENTF_KEYUP = 2;

		public static void KeyDown(Keys vKey)
		{
			keybd_event((byte)vKey, 0, KEYEVENTF_EXTENDEDKEY, 0);
		}

		public static void KeyUp(Keys vKey)
		{
			keybd_event((byte)vKey, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
		}
	}
}
