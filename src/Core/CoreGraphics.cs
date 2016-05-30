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
using System.Threading.Tasks;

namespace Logo
{
	internal class CoreGraphics
	{
		static readonly FieldInfo canvasField = typeof(GraphicsWindow).GetFields(BindingFlags.Static | BindingFlags.NonPublic)
				.FirstOrDefault(f => f.FieldType == typeof(Canvas));

		static readonly Task done = Task.FromResult(true);
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

		public static Task AnimateShape(string shapeName, DependencyProperty property, double value, double duration)
		{
			var element = Instance.canvas.Children.OfType<FrameworkElement>().FirstOrDefault(e => e.Name == shapeName);
			if (element != null)
				return Animate(element, property, value, duration);

			return done;
		}

		public static Task MoveShape(string shapeName, double? x, double? y, double? duration)
		{
			if (duration == null)
			{
				Shapes.Move(shapeName,
					x.GetValueOrDefault(Shapes.GetLeft(shapeName)),
					y.GetValueOrDefault(Shapes.GetTop(shapeName)));

				return done;
			}
			else
			{
				var tasks = new List<Task>();
				if (x != null)
					tasks.Add(AnimateShape(shapeName, Canvas.LeftProperty, x.Value, duration.Value));
				if (y != null)
					tasks.Add(AnimateShape(shapeName, Canvas.TopProperty, y.Value, duration.Value));

				return Task.WhenAll(tasks);
			}
		}

		public static Task HideShape(string shapeName, double? duration)
		{
			if (duration == null)
			{
				Shapes.HideShape(shapeName);
				return done;
			}
			else
			{
				return AnimateShape(shapeName, UIElement.OpacityProperty, 0, duration.Value);
			}
		}

		public static Task RotateShape(string shapeName, double angle, double? duration)
		{
			if (duration == null)
			{
				Shapes.Rotate(shapeName, angle);
			}
			else
			{
				var element = Instance.canvas.Children.OfType<FrameworkElement>().FirstOrDefault(e => e.Name == shapeName);
				if (element != null)
				{
					return ApplyTransform(element, e =>
					{
						var transform = new RotateTransform
						{
							CenterX = element.ActualWidth / 2.0,
							CenterY = element.ActualHeight / 2.0,
						};

						((TransformGroup)element.RenderTransform).Children.Add(transform);
						return Animate(transform, RotateTransform.AngleProperty, angle, duration.Value);
					});
				}
			}

			return done;
		}

		public static Task ShowShape(string shapeName, double? duration)
		{
			if (duration == null)
			{
				Shapes.ShowShape(shapeName);
				return done;
			}
			else
			{
				return AnimateShape(shapeName, UIElement.OpacityProperty, 1, duration.Value);
			}
		}

		public static Task ZoomShape(string shapeName, double scaleX, double scaleY, double? duration)
		{
			if (duration == null)
			{
				Shapes.Zoom(shapeName, scaleX, scaleY);
			}
			else
			{
				scaleX = System.Math.Min(System.Math.Max(scaleX, 0.1), 20.0);
				scaleY = System.Math.Min(System.Math.Max(scaleY, 0.1), 20.0);

				var element = Instance.canvas.Children.OfType<FrameworkElement>().FirstOrDefault(e => e.Name == shapeName);
				if (element != null)
				{
					return ApplyTransform(element, e =>
					{
						var transform = new ScaleTransform
						{
							CenterX = element.ActualWidth / 2.0,
							CenterY = element.ActualHeight / 2.0,
						};

						((TransformGroup)element.RenderTransform).Children.Add(transform);
						return Task.WhenAll(
							Animate(transform, ScaleTransform.ScaleXProperty, scaleX, duration.Value),
							Animate(transform, ScaleTransform.ScaleYProperty, scaleY, duration.Value));
					});
				}
			}

			return done;
		}

		static Task ApplyTransform(FrameworkElement element, Func<FrameworkElement, Task> transform)
		{
			var tsc = new TaskCompletionSource<Task>();

			element.Dispatcher.BeginInvoke(() =>
			{
				if (!(element.RenderTransform is TransformGroup))
					element.RenderTransform = new TransformGroup();

				if (element.ActualHeight == 0 && element.ActualWidth == 0)
				{
					// Layout may not have happened yet, so we need to schedule the transform 
					// operation on the layout event.
					EventHandler handler = null;
					handler = (sender, args) =>
					{
						element.LayoutUpdated -= handler;
						tsc.SetResult(transform(element));
					};

					element.LayoutUpdated += handler;
				}
				else
				{
					tsc.SetResult(transform(element));
				}
			});

			return tsc.Task.Unwrap();
		}

		public void ShowWindow()
		{
			var graphics = System.Windows.Application.Current.Windows.OfType<Window>()
				.Where(w => w.Title == GraphicsWindow.Title)
				.Select(w => new WindowInteropHelper(w).Handle)
				.First();

			NativeMethods.ShowWindow(graphics, NativeMethods.SW_SHOWNOACTIVATE);
		}

		static Task Animate<T>(T element, DependencyProperty property, double newValue, double duration)
			where T : DependencyObject, IAnimatable
		{
			var initialValue = (double)element.GetValue(property);
			if (double.IsNaN(initialValue))
				initialValue = 0.0;

			return element.BeginAnimationAsync(property, 
				new DoubleAnimation(initialValue, newValue, new Duration(TimeSpan.FromMilliseconds(duration)))
				{
					FillBehavior = FillBehavior.HoldEnd,
					DecelerationRatio = 0.2
				});
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