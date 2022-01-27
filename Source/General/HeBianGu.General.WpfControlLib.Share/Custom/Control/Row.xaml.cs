using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{
    public class Row : Grid
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Row), "S.Row.Default");
        public static ComponentResourceKey Column11Key => new ComponentResourceKey(typeof(Row), "S.Row.Column.11");
        public static ComponentResourceKey Column10Key => new ComponentResourceKey(typeof(Row), "S.Row.Column.10");
        public static ComponentResourceKey Column9Key => new ComponentResourceKey(typeof(Row), "S.Row.Column.9");
        public static ComponentResourceKey Column8Key => new ComponentResourceKey(typeof(Row), "S.Row.Column.8");
        public static ComponentResourceKey Column7Key => new ComponentResourceKey(typeof(Row), "S.Row.Column.7");
        public static ComponentResourceKey Column6Key => new ComponentResourceKey(typeof(Row), "S.Row.Column.6");
        public static ComponentResourceKey Column5Key => new ComponentResourceKey(typeof(Row), "S.Row.Column.5");
        public static ComponentResourceKey Column4Key => new ComponentResourceKey(typeof(Row), "S.Row.Column.4");
        public static ComponentResourceKey Column3Key => new ComponentResourceKey(typeof(Row), "S.Row.Column.3");
        public static ComponentResourceKey Column2Key => new ComponentResourceKey(typeof(Row), "S.Row.Column.2");
        public static ComponentResourceKey Column1Key => new ComponentResourceKey(typeof(Row), "S.Row.Column.1"); 


        static Row()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Row), new FrameworkPropertyMetadata(typeof(Row)));
        }

    }


}
