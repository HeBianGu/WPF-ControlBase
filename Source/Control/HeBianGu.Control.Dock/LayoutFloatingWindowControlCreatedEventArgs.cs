using HeBianGu.Control.Dock.Controls;
using System;

namespace HeBianGu.Control.Dock
{
	public sealed class LayoutFloatingWindowControlCreatedEventArgs : EventArgs
	{
		public LayoutFloatingWindowControlCreatedEventArgs(LayoutFloatingWindowControl layoutFloatingWindowControl)
		{
			LayoutFloatingWindowControl = layoutFloatingWindowControl;
		}

		public LayoutFloatingWindowControl LayoutFloatingWindowControl { get; }
	}

}
