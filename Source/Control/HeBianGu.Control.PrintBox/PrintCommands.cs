using System.Windows.Input;

namespace HeBianGu.Control.PrintBox
{
    public static class PrintCommands
    {
        public static RoutedUICommand PrintView = new RoutedUICommand() { Text = "打印预览" };
        public static RoutedUICommand Print = new RoutedUICommand() { Text = "打印" };
        public static RoutedUICommand PrintSetting = new RoutedUICommand() { Text = "打印设置" };
        public static RoutedUICommand ParamSetting = new RoutedUICommand() { Text = "参数设置" };

    }
}
