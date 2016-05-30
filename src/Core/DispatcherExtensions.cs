using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Threading;

namespace Logo
{
    static class DispatcherExtensions
    {
		public static DispatcherOperation BeginInvoke(this Dispatcher dispatcher, Action action)
		{
			return dispatcher.BeginInvoke((Delegate)action);
		}

		public static DispatcherOperation BeginInvoke(this Dispatcher dispatcher, DispatcherPriority priority, Action action)
		{
			return dispatcher.BeginInvoke(priority, (Delegate)action);
		}
	}
}
