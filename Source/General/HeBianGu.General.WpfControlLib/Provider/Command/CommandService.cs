using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    ///     控件库使用的所有命令（为了统一，不使用wpf自带的命令）
    /// </summary>
    public static class CommandService
    {
        /// <summary>
        ///     搜索
        /// </summary>
        public static RoutedCommand Search { get; } = new RoutedCommand(nameof(Search), typeof(CommandService));

        /// <summary>
        ///     清除
        /// </summary>
        public static RoutedCommand Clear { get; } = new RoutedCommand(nameof(Clear), typeof(CommandService));

        /// <summary>
        ///     切换
        /// </summary>
        public static RoutedCommand Switch { get; } = new RoutedCommand(nameof(Switch), typeof(CommandService));

        /// <summary>
        ///     右转
        /// </summary>
        public static RoutedCommand RotateRight { get; } = new RoutedCommand(nameof(RotateRight), typeof(CommandService));

        /// <summary>
        ///     左转
        /// </summary>
        public static RoutedCommand RotateLeft { get; } = new RoutedCommand(nameof(RotateLeft), typeof(CommandService));

        /// <summary>
        ///     小
        /// </summary>
        public static RoutedCommand Reduce { get; } = new RoutedCommand(nameof(Reduce), typeof(CommandService));

        /// <summary>
        ///     大
        /// </summary>
        public static RoutedCommand Enlarge { get; } = new RoutedCommand(nameof(Enlarge), typeof(CommandService));

        /// <summary>
        ///     还原
        /// </summary>
        public static RoutedCommand Restore { get; } = new RoutedCommand(nameof(Restore), typeof(CommandService));

        /// <summary>
        ///     打开
        /// </summary>
        public static RoutedCommand Open { get; } = new RoutedCommand(nameof(Open), typeof(CommandService));

        /// <summary>
        ///     保存
        /// </summary>
        public static RoutedCommand Save { get; } = new RoutedCommand(nameof(Save), typeof(CommandService));

        /// <summary>
        ///     选中
        /// </summary>
        public static RoutedCommand Selected { get; } = new RoutedCommand(nameof(Selected), typeof(CommandService));

        /// <summary>
        ///     关闭
        /// </summary>
        public static RoutedCommand Close { get; } = new RoutedCommand(nameof(Close), typeof(CommandService));

        /// <summary>
        ///     取消
        /// </summary>
        public static RoutedCommand Cancel { get; } = new RoutedCommand(nameof(Cancel), typeof(CommandService));

        /// <summary>
        ///     确定
        /// </summary>
        public static RoutedCommand Confirm { get; } = new RoutedCommand(nameof(Confirm), typeof(CommandService));

        /// <summary>
        ///     是
        /// </summary>
        public static RoutedCommand Yes { get; } = new RoutedCommand(nameof(Yes), typeof(CommandService));

        /// <summary>
        ///     否
        /// </summary>
        public static RoutedCommand No { get; } = new RoutedCommand(nameof(No), typeof(CommandService));

        /// <summary>
        ///     关闭所有
        /// </summary>
        public static RoutedCommand CloseAll { get; } = new RoutedCommand(nameof(CloseAll), typeof(CommandService));

        /// <summary>
        ///     关闭其他
        /// </summary>
        public static RoutedCommand CloseOther { get; } = new RoutedCommand(nameof(CloseOther), typeof(CommandService));

        /// <summary>
        ///     上一个
        /// </summary>
        public static RoutedCommand Prev { get; } = new RoutedCommand(nameof(Prev), typeof(CommandService));

        /// <summary>
        ///     下一个
        /// </summary>
        public static RoutedCommand Next { get; } = new RoutedCommand(nameof(Next), typeof(CommandService));



        /// <summary>
        ///     第一个
        /// </summary>
        public static RoutedCommand First { get; } = new RoutedCommand(nameof(First), typeof(CommandService));

        /// <summary>
        ///     最后一个
        /// </summary>
        public static RoutedCommand Last { get; } = new RoutedCommand(nameof(Last), typeof(CommandService));

        /// <summary>
        ///     上午
        /// </summary>
        public static RoutedCommand Am { get; } = new RoutedCommand(nameof(Am), typeof(CommandService));

        /// <summary>
        ///     下午
        /// </summary>
        public static RoutedCommand Pm { get; } = new RoutedCommand(nameof(Pm), typeof(CommandService));

        /// <summary>
        ///     确认
        /// </summary>
        public static RoutedCommand Sure { get; } = new RoutedCommand(nameof(Sure), typeof(CommandService));

        /// <summary>
        ///     小时改变
        /// </summary>
        public static RoutedCommand HourChange { get; } = new RoutedCommand(nameof(HourChange), typeof(CommandService));

        /// <summary>
        ///     分钟改变
        /// </summary>
        public static RoutedCommand MinuteChange { get; } = new RoutedCommand(nameof(MinuteChange), typeof(CommandService));

        /// <summary>
        ///     秒改变
        /// </summary>
        public static RoutedCommand SecondChange { get; } = new RoutedCommand(nameof(SecondChange), typeof(CommandService));

        /// <summary>
        ///     鼠标移动
        /// </summary>
        public static RoutedCommand MouseMove { get; } = new RoutedCommand(nameof(MouseMove), typeof(CommandService));


        /// <summary> 渐隐藏 </summary>
        public static CollapsedOfOpacityCommand CollapsedOfOpacityCommand { get; } = new CollapsedOfOpacityCommand();

        /// <summary> 渐显示 </summary>
        public static VisibleOfOpacityCommand VisibleOfOpacityCommand { get; } = new VisibleOfOpacityCommand();
        

        /// <summary> 打开应用程序 </summary>
        public static ProcessCommand ProcessCommand { get; } = new ProcessCommand();

        /// <summary> 关闭消息层 </summary>
        public static MessageLayerCloseCommand MessageLayerCloseCommand { get; } = new MessageLayerCloseCommand();
        

    }
}
