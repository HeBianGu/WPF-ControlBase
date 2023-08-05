// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Windows.Controls.Primitives;

namespace HeBianGu.App.Track
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShellWindow
    {
        public ShellWindow()
        {
            InitializeComponent();
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            TestViewModel model = (sender as Thumb).DataContext as TestViewModel;

            model.Double1 += e.HorizontalChange;
        }
    }
}
