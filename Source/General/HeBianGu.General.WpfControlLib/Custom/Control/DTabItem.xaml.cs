using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{
    public class DTabItem : TabItem
    {

        public SolidColorBrush SelectedColor
        {
            get { return (SolidColorBrush)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(SolidColorBrush), typeof(DTabItem), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 124, 125, 133))));


        public SolidColorBrush SelectForeground
        {
            get { return (SolidColorBrush)GetValue(SelectForegroundProperty); }
            set { SetValue(SelectForegroundProperty, value); }
        }
        public static readonly DependencyProperty SelectForegroundProperty =
            DependencyProperty.Register("SelectForeground", typeof(SolidColorBrush), typeof(DTabItem), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 229, 229, 231))));


        public TabItemType TabItemType
        {
            get { return (TabItemType)GetValue(TabItemTypeProperty); }
            set
            {
                SetValue(TabItemTypeProperty, value);
            }
        }
        public static readonly DependencyProperty TabItemTypeProperty =
            DependencyProperty.Register("TabItemType", typeof(TabItemType), typeof(DTabItem), new PropertyMetadata(TabItemType.Middle));
    }

    public enum TabItemType
    {
        Left, Middle, Right
    }
}
