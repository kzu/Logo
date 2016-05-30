using System;
using System.Threading;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Logo
{
	static class Extensions
	{
		public static DispatcherOperation BeginInvoke(this Dispatcher dispatcher, Action action)
		{
			return dispatcher.BeginInvoke((Delegate)action);
		}

		public static DispatcherOperation BeginInvoke(this Dispatcher dispatcher, DispatcherPriority priority, Action action)
		{
			return dispatcher.BeginInvoke(priority, (Delegate)action);
		}

		public static void BeginAnimation<T>(this T target, DependencyProperty property, AnimationTimeline animation, HandoffBehavior handoffBehavior = HandoffBehavior.Compose)
			where T : DispatcherObject, IAnimatable
		{
			var ev = new ManualResetEvent(false);
			var timer = new DispatcherTimer
			{
				Interval = animation.Duration.TimeSpan
			};
			timer.Tick += (s, e) =>
			{
				ev.Set();
				timer.Stop();
			};

			timer.Start();

			//animation.Completed += (s, e) => ev.Set();
			target.BeginAnimation(property, animation, handoffBehavior);

			while (!ev.WaitOne(20))
			{
			}
		}
	}
}
