using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.General.WpfControlLib
{
    public class ShuttleSelectControl : ContentControl
    {

        public ObservableCollection<ShuttleItem> Source
        {
            get { return (ObservableCollection<ShuttleItem>)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ObservableCollection<ShuttleItem>), typeof(ShuttleSelectControl), new PropertyMetadata(default(ObservableCollection<ShuttleItem>), (d, e) =>
             {
                 ShuttleSelectControl control = d as ShuttleSelectControl;

                 if (control == null) return;

                 ObservableCollection<ShuttleItem> config = e.NewValue as ObservableCollection<ShuttleItem>;

             }));


    }

    public class ShuttleItem
    {
        public string Header { get; set; }

        public IEnumerable ItemSource { get; set; }
    }
}
