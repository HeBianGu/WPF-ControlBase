using HeBianGu.Control.Dock.Controls;
using System;

namespace HeBianGu.Control.Dock
{
	public sealed class LayoutFloatingWindowControlClosedEventArgs : EventArgs
	{
		public LayoutFloatingWindowControlClosedEventArgs(LayoutFloatingWindowControl layoutFloatingWindowControl)
		{
			LayoutFloatingWindowControl = layoutFloatingWindowControl;
		}

		public LayoutFloatingWindowControl LayoutFloatingWindowControl { get; }
	}

}
