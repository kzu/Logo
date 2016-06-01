using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Threading;

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

	static void Invoke(Action action, TimeSpan wait)
	{
		// This basically copies the behavior of the Turtle in 
		// its Move operations and the WaitForReturn method.
		var evt = new AutoResetEvent(false);
		Instance.canvas.Dispatcher.Invoke(() =>
		{
			var dt = new DispatcherTimer { Interval = wait };
			dt.Tick += (s, e) =>
			{
				evt.Set();
				dt.Stop();
			};
			dt.Start();
		});

		var millisecondsTimeout = 100;
		if (Instance.canvas.Dispatcher.CheckAccess())
			millisecondsTimeout = 10;

		Instance.canvas.Dispatcher.Invoke(action);

		while (!evt.WaitOne(millisecondsTimeout))
		{
			try
			{
				Instance.canvas.Dispatcher.Invoke(() => { }, DispatcherPriority.Background);
			}
			catch { }
		}
	}

	public static string AddEllipse(bool fill, double x, double y, int ancho, int alto, string color = null)
	{
		return Draw(fill, () =>
		{
			var shapeName = Shapes.AddEllipse(ancho, alto);
			Shapes.Move(shapeName, x, y);
			return shapeName;
		}, color);
	}

	public static string AddLine(double x1, double y1, double x2, double y2, string color = null)
	{
		return Draw(false, () => Shapes.AddLine(x1, y1, x2, y2), color);
	}

	public static string AddRectangle(bool fill, double x, double y, int width, int height, string color = null)
	{
		return Draw(fill, () =>
		{
			var shapeName = Shapes.AddRectangle(width, height);
			Shapes.Move(shapeName, x, y);
			return shapeName;
		}, color);
	}
	public static void ShowShape(string shapeName, int? duration)
	{
		if (duration == null)
			Shapes.ShowShape(shapeName);
		else
			AnimateShape(shapeName, UIElement.OpacityProperty, 1, duration.Value);
	}


	public static string AddTriangle(bool fill, double x1, double y1, double x2, double y2, double x3, double y3, string color = null)
	{
		return Draw(fill, () => Shapes.AddTriangle(x1, y1, x2, y2, x3, y3), color);
	}

	public static void AnimateShape(string shapeName, DependencyProperty property, double value, int duration)
	{
		var element = Instance.canvas.Children.OfType<FrameworkElement>().FirstOrDefault(e => e.Name == shapeName);
		if (element != null)
			Animate(element, property, value, duration);
	}

	public static void HideShape(string shapeName, int? duration)
	{
		if (duration == null)
			Shapes.HideShape(shapeName);
		else
			AnimateShape(shapeName, UIElement.OpacityProperty, 0, duration.Value);
	}

	public static void RotateShape(string shapeName, double angle, int? duration)
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
				var transform = AddTransform(element, e => new RotateTransform
				{
					CenterX = element.ActualWidth / 2.0,
					CenterY = element.ActualHeight / 2.0,
				});

				Invoke(() => transform.BeginAnimation(RotateTransform.AngleProperty,
					new DoubleAnimation(angle, new Duration(TimeSpan.FromMilliseconds(duration.Value)))
					{
						FillBehavior = FillBehavior.HoldEnd,
						DecelerationRatio = 0.2
					}), TimeSpan.FromMilliseconds(duration.Value));
			}
		}
	}

	public static void MoveShape(string shapeName, double? x, double? y, int? duration)
	{
		if (duration == null)
		{
			Shapes.Move(shapeName,
				x.GetValueOrDefault(Shapes.GetLeft(shapeName)),
				y.GetValueOrDefault(Shapes.GetTop(shapeName)));
		}
		else
		{
			var element = Instance.canvas.Children.OfType<FrameworkElement>().FirstOrDefault(e => e.Name == shapeName);
			if (element != null)
			{
				var storyboard = new Storyboard();
				var time = new Duration(TimeSpan.FromMilliseconds(duration.Value));
				if (x != null)
				{
					var initialValue = (double)element.GetValue(Canvas.LeftProperty);
					if (double.IsNaN(initialValue))
						initialValue = 0.0;

					var animation = new DoubleAnimation(initialValue, x.Value, time)
					{
						FillBehavior = FillBehavior.HoldEnd,
						DecelerationRatio = 0.2
					};

					Storyboard.SetTarget(animation, element);
					Storyboard.SetTargetProperty(animation, new PropertyPath(Canvas.LeftProperty));
					storyboard.Children.Add(animation);
				}

				if (y != null)
				{
					var initialValue = (double)element.GetValue(Canvas.TopProperty);
					if (double.IsNaN(initialValue))
						initialValue = 0.0;

					var animation = new DoubleAnimation(initialValue, y.Value, time)
					{
						FillBehavior = FillBehavior.HoldEnd,
						DecelerationRatio = 0.2
					};

					Storyboard.SetTarget(animation, element);
					Storyboard.SetTargetProperty(animation, new PropertyPath(Canvas.TopProperty));
					storyboard.Children.Add(animation);
				}

				if (x != null || y != null)
					Invoke(() => storyboard.Begin(element), time.TimeSpan);
			}
		}
	}

	public static void ZoomShape(string shapeName, double? scaleX, double? scaleY, int? duration)
	{
		if (duration == null)
		{
			Shapes.Zoom(shapeName, scaleX ?? 1, scaleY ?? 1);
		}
		else
		{
			scaleX = System.Math.Min(System.Math.Max(scaleX ?? 1, 0.1), 20.0);
			scaleY = System.Math.Min(System.Math.Max(scaleY ?? 1, 0.1), 20.0);

			var element = Instance.canvas.Children.OfType<FrameworkElement>().FirstOrDefault(e => e.Name == shapeName);
			if (element != null)
			{
				var time = TimeSpan.FromMilliseconds(duration.Value);
				var transform = AddTransform(element, e => new ScaleTransform
				{
					CenterX = element.ActualWidth / 2.0,
					CenterY = element.ActualHeight / 2.0,
				});

				Invoke(() =>
				{
					if (scaleX != null)
						transform.BeginAnimation(ScaleTransform.ScaleXProperty, new DoubleAnimation(scaleX.Value, new Duration(time))
						{
							FillBehavior = FillBehavior.HoldEnd,
							DecelerationRatio = 0.2
						});
					if (scaleY != null)
						transform.BeginAnimation(ScaleTransform.ScaleYProperty, new DoubleAnimation(scaleY ?? 1, new Duration(time))
						{
							FillBehavior = FillBehavior.HoldEnd,
							DecelerationRatio = 0.2
						});
				}, time);
			}
		}
	}

	static TTransform AddTransform<TTransform>(FrameworkElement element, Func<FrameworkElement, TTransform> factory)
		where TTransform : Transform
	{
		return element.Dispatcher.Invoke(() =>
		{
			if (!(element.RenderTransform is TransformGroup))
				element.RenderTransform = new TransformGroup();

			if (element.ActualHeight == 0 && element.ActualWidth == 0)
			{
				var evt = new AutoResetEvent(false);

					// Layout may not have happened yet, so we need to schedule the transform 
					// operation on the layout event.
					EventHandler handler = null;
				TTransform transform = default(TTransform);
				handler = (sender, args) =>
				{
					element.LayoutUpdated -= handler;
					transform = factory(element);
					((TransformGroup)element.RenderTransform).Children.Add(transform);
					evt.Set();
				};

				element.LayoutUpdated += handler;

				var millisecondsTimeout = 100;
				if (element.Dispatcher.CheckAccess())
					millisecondsTimeout = 10;

				while (!evt.WaitOne(millisecondsTimeout))
				{
					try
					{
						element.Dispatcher.Invoke(() => { }, DispatcherPriority.Background);
					}
					catch { }
				}

				return transform;
			}
			else
			{
				var transform = factory(element);
				((TransformGroup)element.RenderTransform).Children.Add(transform);
				return transform;
			}
		});
	}

	public void ShowWindow()
	{
		var graphics = System.Windows.Application.Current.Windows.OfType<Window>()
			.Where(w => w.Title == GraphicsWindow.Title)
			.Select(w => new WindowInteropHelper(w).Handle)
			.FirstOrDefault();

		if (graphics != null)
			NativeMethods.ShowWindow(graphics, NativeMethods.ShowWindowCommands.ShowNoActivate);
	}

	static void Animate<T>(T element, DependencyProperty property, double newValue, int duration)
		where T : DependencyObject, IAnimatable
	{
		var initialValue = (double)element.GetValue(property);
		if (double.IsNaN(initialValue))
			initialValue = 0.0;

		var animation = new DoubleAnimation(initialValue, newValue, new Duration(TimeSpan.FromMilliseconds(duration)))
		{
			FillBehavior = FillBehavior.HoldEnd,
			DecelerationRatio = 0.2
		};

		Invoke(() => element.BeginAnimation(property, animation), animation.Duration.TimeSpan);
	}

	static string Draw(bool fill, Func<string> operation, string color)
	{
		var brush = GraphicsWindow.BrushColor;
		var pen = GraphicsWindow.PenColor;
		try
		{
			GraphicsWindow.BrushColor = "Transparent";
			if (color != null)
			{
				GraphicsWindow.PenColor = color;
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

		//var workbook = Process.GetProcesses()
		//	.Where(p => p.MainWindowTitle.Contains("Workbook") && !p.MainWindowTitle.Contains("(WPF)"))
		//	.Select(p => p.MainWindowHandle)
		//	.FirstOrDefault();

		//NativeMethods.ShowWindow(workbook, NativeMethods.SW_SHOWNOACTIVATE);
		// NativeMethods.MoveWindow(workbook, 0, 0, screenWidth / 2, screenHeight, true);

		var graphics = System.Windows.Application.Current.Windows.OfType<Window>()
			.Where(w => w.Title == GraphicsWindow.Title)
			.FirstOrDefault();

		while (graphics == null)
		{
			graphics = System.Windows.Application.Current.Windows.OfType<Window>()
				.Where(w => w.Title == GraphicsWindow.Title)
				.FirstOrDefault();
		}

		graphics.Show();
		graphics.Topmost = true;
		graphics.Activate();
		graphics.Top = 0;
		graphics.Left = screenWidth / 2;
		graphics.Width = screenWidth / 2;
		graphics.Height = screenHeight;

		//NativeMethods.ShowWindow(new WindowInteropHelper(graphics).Handle, NativeMethods.SW_SHOWNORMAL);

		//SendKeys.KeyDown(System.Windows.Forms.Keys.LWin);
		//SendKeys.KeyDown(System.Windows.Forms.Keys.Right);
		//SendKeys.KeyUp(System.Windows.Forms.Keys.Right);
		//SendKeys.KeyUp(System.Windows.Forms.Keys.LWin);

		//var graphics = System.Windows.Application.Current.Windows.OfType<Window>()
		//	.Where(w => w.Title == GraphicsWindow.Title)
		//	.Select(w => new WindowInteropHelper(w).Handle)
		//	.First();

		//NativeMethods.ShowWindow(graphics, NativeMethods.SW_SHOWNOACTIVATE);
		//NativeMethods.MoveWindow(graphics, screenWidth / 2, 0, screenWidth / 2, screenHeight, true);
	}
}