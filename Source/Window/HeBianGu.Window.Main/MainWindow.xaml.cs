// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Window.Message;
using System.Windows;

namespace HeBianGu.Window.Main
{
    public partial class MainWindow : MessageWindowBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(MainWindow), "S.MainWindow.Default");
        public static new ComponentResourceKey SingleKey => new ComponentResourceKey(typeof(MainWindow), "S.MainWindow.Single");
        public static new ComponentResourceKey MvpKey => new ComponentResourceKey(typeof(MainWindow), "S.MainWindow.Mvp");
        public static new ComponentResourceKey HwndHostKey => new ComponentResourceKey(typeof(MainWindow), "S.MainWindow.HwndHost");

        static MainWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MainWindow), new FrameworkPropertyMetadata(typeof(MainWindow)));
            StyleLoader.Instance?.LoadDefault(typeof(MainWindow));
        }
    }
}
