using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace HeBianGu.Control.OutlookBar
{
    public class OverflowMenuCreatedEventArgs : EventArgs
    {
        public OverflowMenuCreatedEventArgs(Collection<object> menuItems)
            : base()
        {
            MenuItems = menuItems;
        }

        public Collection<object> MenuItems { get; private set; }
    }
}
