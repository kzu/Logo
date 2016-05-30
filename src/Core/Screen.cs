using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using Microsoft.SmallBasic.Library;
using System.Linq;
using System.Windows.Interop;

namespace Logo
{
    internal class Screen
    {
		public Screen()
		{
			GraphicsWindow.Show();
			Snap();
			Grid = GridLines.Create();
		}

		public GridLines Grid { get; }

		void Snap()
		{
			var screenWidth = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
			var screenHeight = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;

			var workbook = Process.GetProcesses()
				.Where(p => p.MainWindowTitle.Contains("Workbook") && !p.MainWindowTitle.Contains("(WPF)"))
				.Select(p => p.MainWindowHandle)
				.First();

			var graphics = System.Windows.Application.Current.Windows.OfType<Window>()
				.Where(w => w.Title == GraphicsWindow.Title)
				.First();
			graphics.Show();
			graphics.Topmost = true;
			graphics.Activate();

			//SendKeys.KeyDown(System.Windows.Forms.Keys.LWin);
			//SendKeys.KeyDown(System.Windows.Forms.Keys.Right);
			//SendKeys.KeyUp(System.Windows.Forms.Keys.Right);
			//SendKeys.KeyUp(System.Windows.Forms.Keys.LWin);

			//NativeMethods.ShowWindow(workbook, NativeMethods.SW_SHOWNOACTIVATE);
			//NativeMethods.MoveWindow(workbook, 0, 0, screenWidth / 2, screenHeight, true);

			//var graphics = System.Windows.Application.Current.Windows.OfType<Window>()
			//	.Where(w => w.Title == GraphicsWindow.Title)
			//	.Select(w => new WindowInteropHelper(w).Handle)
			//	.First();

			//NativeMethods.ShowWindow(graphics, NativeMethods.SW_SHOWNOACTIVATE);
			//NativeMethods.MoveWindow(graphics, screenWidth / 2, 0, screenWidth / 2, screenHeight, true);
		}

		internal void Show()
		{
			var graphics = System.Windows.Application.Current.Windows.OfType<Window>()
				.Where(w => w.Title == GraphicsWindow.Title)
				.Select(w => new WindowInteropHelper(w).Handle)
				.First();

			NativeMethods.ShowWindow(graphics, NativeMethods.SW_SHOWNOACTIVATE);
		}
	}
}
