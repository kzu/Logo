using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Logo
{
	internal static class StoryboardExtensions
	{
		public static Task BeginAsync(this Storyboard timeline)
		{
			var source = new TaskCompletionSource<object>();
			timeline.Dispatcher.BeginInvoke(() =>
			{
				timeline.Completed += (s, e) => source.SetResult(null);
				timeline.Begin();
			});

			return source.Task;
		}
	}
}
