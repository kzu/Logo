using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using Microsoft.SmallBasic.Library;
using System.Linq;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Reflection;
using System.Windows.Threading;
using System.Windows.Media;

namespace Logo
{
	internal class CoreGraphics
	{
		static readonly FieldInfo canvasField = typeof(GraphicsWindow).GetFields(BindingFlags.Static | BindingFlags.NonPublic)
				.FirstOrDefault(f => f.FieldType == typeof(Canvas));

		static CoreGraphics instance;
		Canvas canvas;

		public CoreGraphics()
		{
			instance = this;
			GraphicsWindow.Show();
			Snap();

			if (canvasField != null)
				canvas = (Canvas)canvasField.GetValue(null);
			else
				canvas = new Canvas();

			GridLines = new GridLines(canvas);
		}

		static CoreGraphics Instance { get { return instance; } }

		public GridLines GridLines { get; }

		public static void AnimateShape(string shapeName, DependencyProperty property, double value, double duration)
		{
			var element = Instance.canvas.Children.OfType<FrameworkElement>().FirstOrDefault(e => e.Name == shapeName);
			if (element != null)
				Animate(element, property, value, duration);
		}

		static void Animate<T>(T element, DependencyProperty property, double newValue, double duration)
			where T : DependencyObject, IAnimatable
		{
			Action<T, DependencyProperty, double, double> animate = (e, p, v, d) =>
			{
				var initialValue = (double)e.GetValue(p);
				if (double.IsNaN(initialValue))
					initialValue = 0.0;
				
				e.BeginAnimation(p,
					new DoubleAnimation(initialValue, v, new Duration(TimeSpan.FromMilliseconds(d)))
					{
						FillBehavior = FillBehavior.HoldEnd,
						DecelerationRatio = 0.2
					});
			};

			element.Dispatcher.BeginInvoke(animate, DispatcherPriority.Render, element, property, newValue, duration);
		}

		public static string AddEllipse(bool fill, double x, double y, int ancho, int alto, Color? color)
		{
			return Draw(fill, () =>
			{
				var shapeName = Shapes.AddEllipse(ancho, alto);
				Shapes.Move(shapeName, x, y);
				return shapeName;
			}, color);
		}

		public static string AddLine(double x1, double y1, double x2, double y2, Color? color)
		{
			return Draw(false, () => Shapes.AddLine(x1, y1, x2, y2), color);
		}

		public static string AddRectangle(bool fill, double x, double y, int ancho, int alto, Color? color)
		{
			return Draw(fill, () =>
			{
				var shapeName = Shapes.AddRectangle(ancho, alto);
				Shapes.Move(shapeName, x, y);
				return shapeName;
			}, color);
		}

		public static string AddTriangle(bool fill, double x1, double y1, double x2, double y2, double x3, double y3, Color? color)
		{
			return Draw(fill, () => Shapes.AddTriangle(x1, y1, x2, y2, x3, y3), color);
		}

		public static void MoveShape(string shapeName, double? x, double? y, double? duration)
		{
			if (duration == null)
			{
				Shapes.Move(shapeName,
					x.GetValueOrDefault(Shapes.GetLeft(shapeName)),
					y.GetValueOrDefault(Shapes.GetTop(shapeName)));
			}
			else
			{
				if (x != null)
					AnimateShape(shapeName, Canvas.LeftProperty, x.Value, duration.Value);
				if (y != null)
					AnimateShape(shapeName, Canvas.TopProperty, y.Value, duration.Value);
			}
		}

		internal static void HideShape(string shapeName, double? duration)
		{
			if (duration == null)
			{
				Shapes.HideShape(shapeName);
			}
			else
			{
				AnimateShape(shapeName, UIElement.OpacityProperty, 0, duration.Value);
			}
		}

		internal static void RotateShape(string shapeName, double angle, double? duration)
		{
			if (duration == null)
			{
				Shapes.Rotate(shapeName, angle);
			}
			else
			{
				Instance.canvas.Dispatcher.Invoke(() =>
				{
					var element = Instance.canvas.Children.OfType<FrameworkElement>().FirstOrDefault(e => e.Name == shapeName);
					if (element != null)
					{
						if (!(element.RenderTransform is TransformGroup))
							element.RenderTransform = new TransformGroup();

						EventHandler rotate = null;
						rotate = (sender, args) =>
						{
							var transform = new RotateTransform
							{
								CenterX = element.ActualWidth / 2.0,
								CenterY = element.ActualHeight / 2.0,
							};

							((TransformGroup)element.RenderTransform).Children.Add(transform);
							Animate(transform, RotateTransform.AngleProperty, angle, duration.Value);
							element.LayoutUpdated -= rotate;
						};

						if (element.ActualHeight == 0 && element.ActualWidth == 0)
							element.LayoutUpdated += rotate;
						else
							rotate(element, EventArgs.Empty);
					}
				});
			}
		}

		internal static void ShowShape(string shapeName, double? duration)
		{
			if (duration == null)
			{
				Shapes.ShowShape(shapeName);
			}
			else
			{
				AnimateShape(shapeName, UIElement.OpacityProperty, 1, duration.Value);
			}
		}

		static string Draw(bool fill, Func<string> operation, Color? color)
		{
			var brush = GraphicsWindow.BrushColor;
			var pen = GraphicsWindow.PenColor;
			try
			{
				GraphicsWindow.BrushColor = "Transparent";
				if (color != null)
				{
					GraphicsWindow.PenColor = ColorConverter.ToString(color.Value);
					if (fill)
						GraphicsWindow.BrushColor = GraphicsWindow.PenColor;
				}

				return operation();
			}
			finally
			{
				GraphicsWindow.BrushColor = brush;
				if (color != null)
				{
					GraphicsWindow.PenColor = pen;
					if (fill)
						GraphicsWindow.BrushColor = brush;
				}
			}

		}

		public void ShowWindow()
		{
			var graphics = System.Windows.Application.Current.Windows.OfType<Window>()
				.Where(w => w.Title == GraphicsWindow.Title)
				.Select(w => new WindowInteropHelper(w).Handle)
				.First();

			NativeMethods.ShowWindow(graphics, NativeMethods.SW_SHOWNOACTIVATE);
		}

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
	}
}
