using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Linq;

namespace Logo
{
	static class AeroSnap
	{
		public static void SnapRight()
		{
			Microsoft.VisualBasic.Interaction.AppActivate("Small Basic Graphics Window");

			KeyboardSend.KeyDown(Keys.LWin);
			KeyboardSend.KeyDown(Keys.Right);
			KeyboardSend.KeyUp(Keys.LWin);
			KeyboardSend.KeyUp(Keys.Right);

			KeyboardSend.KeyDown(Keys.Escape);
			KeyboardSend.KeyUp(Keys.Escape);

			var screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
			var screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

			var handle = Process.GetProcesses()
				.Where(p => p.MainWindowTitle.Contains("Workbook") && !p.MainWindowTitle.Contains("(WPF)"))
				.Select(p => p.MainWindowHandle)
				.First();

			ShowWindow(handle, SW_SHOWNORMAL);
			MoveWindow(handle, 0, 0, screenWidth / 2, screenHeight, true);
			SetFocus(handle);
		}

		static int SW_SHOWNORMAL = 1;

		[DllImport("user32.dll")]
		static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
		[DllImport("user32.dll")]
		static extern bool ShowWindow(IntPtr hWnd, int X);
		[DllImport("user32.dll")]
		static extern bool SetFocus(IntPtr hWnd);

		static class KeyboardSend
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
}
