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
			GraphicsWindow.Show();

			var screenWidth = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
			var screenHeight = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;

			var workbook = Process.GetProcesses()
				.Where(p => p.MainWindowTitle.Contains("Workbook") && !p.MainWindowTitle.Contains("(WPF)"))
				.Select(p => p.MainWindowHandle)
				.First();

			NativeMethods.ShowWindow(workbook, NativeMethods.SW_SHOWNORMAL);
			NativeMethods.MoveWindow(workbook, 0, 0, screenWidth / 2, screenHeight, true);

			var graphics = System.Windows.Application.Current.Windows.OfType<Window>()
				.Where(w => w.Title == GraphicsWindow.Title)
				.Select(w => new WindowInteropHelper(w).Handle)
				.First();

			NativeMethods.ShowWindow(graphics, NativeMethods.SW_SHOWNORMAL);
			NativeMethods.MoveWindow(graphics, screenWidth / 2, 0, screenWidth / 2, screenHeight, true);
			NativeMethods.SetFocus(graphics);
		}
	}
}
