using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

		public static Task BeginAnimationAsync<T>(this T target, DependencyProperty property, AnimationTimeline animation, HandoffBehavior handoffBehavior = HandoffBehavior.Compose)
			where T : DispatcherObject, IAnimatable
		{
			if (target.Dispatcher.Thread != Thread.CurrentThread)
			{
				return target.Dispatcher.Invoke(() =>
				{
					var source = new TaskCompletionSource<object>();
					animation.Completed += (s, e) => source.SetResult(null);
					return source.Task;
				});
			}
			else
			{
				var source = new TaskCompletionSource<object>();
				animation.Completed += (s, e) => source.SetResult(null);
				return source.Task;
			}
		}
	}
}
