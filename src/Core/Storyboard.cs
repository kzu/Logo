using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Logo
{
	internal static class Storyboard
	{
		public static Task BeginAnimationAsync<T>(this T target, DependencyProperty property, Timeline animation)
			where T : DependencyObject, IAnimatable
		{
			var storyboard = new System.Windows.Media.Animation.Storyboard();
			storyboard.Children.Add(animation);
			storyboard.Duration = animation.Duration;
			System.Windows.Media.Animation.Storyboard.SetTarget(animation, target);
			System.Windows.Media.Animation.Storyboard.SetTargetProperty(animation, 
				new PropertyPath("(" + property.OwnerType.Name + "." + property.Name + ")"));
			
			var tcs = new TaskCompletionSource<bool>();
			EventHandler onCompleted = null;
			onCompleted = (s, e) =>
			{
				storyboard.Completed -= onCompleted;
				tcs.SetResult(true);
			};

			storyboard.Completed += onCompleted;

			target.Dispatcher.Invoke(() => storyboard.Begin());

			return tcs.Task;
		}

		public static Task BeginAsync(params Timeline[] animations)
		{
			var storyboard = new System.Windows.Media.Animation.Storyboard();
			foreach (var animation in animations)
			{
				storyboard.Children.Add(animation);
			}

			var tcs = new TaskCompletionSource<bool>();
			EventHandler onCompleted = null;
			onCompleted = (s, e) =>
			{
				storyboard.Completed -= onCompleted;
				tcs.SetResult(true);
			};

			storyboard.Completed += onCompleted;
			storyboard.Begin();

			return tcs.Task;
		}
	}
}
