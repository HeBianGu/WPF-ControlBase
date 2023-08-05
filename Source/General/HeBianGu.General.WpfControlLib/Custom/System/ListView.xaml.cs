using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class ListViewKeys
    {
        public static ComponentResourceKey Dynamic => new ComponentResourceKey(typeof(ListViewKeys), "S.ListView.Dynamic");
        public static ComponentResourceKey GridViewColumnHeader => new ComponentResourceKey(typeof(ListViewKeys), "S.ListView.GridViewColumnHeader");

    }

    public class ListViewItemKeys
    {
        public static ComponentResourceKey Dynamic => new ComponentResourceKey(typeof(ListViewItemKeys), "S.ListViewItem.Dynamic");
    }
}
