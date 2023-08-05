// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Windows.Input;

namespace HeBianGu.General.WpfControlLib
{
    public static class Commander
    {
        /// <summary>
        ///     搜索
        /// </summary>
        public static RoutedCommand Search { get; } = new RoutedCommand(nameof(Search), typeof(Commander));

        /// <summary>
        ///     设置
        /// </summary>
        public static RoutedCommand Setting { get; } = new RoutedCommand(nameof(Setting), typeof(Commander));

        /// <summary>
        ///     清除
        /// </summary>
        public static RoutedCommand Clear { get; } = new RoutedCommand(nameof(Clear), typeof(Commander));

        /// <summary>
        ///     切换
        /// </summary>
        public static RoutedCommand Switch { get; } = new RoutedCommand(nameof(Switch), typeof(Commander));

        /// <summary>
        ///     右转
        /// </summary>
        public static RoutedCommand RotateRight { get; } = new RoutedCommand(nameof(RotateRight), typeof(Commander));

        /// <summary>
        ///     左转
        /// </summary>
        public static RoutedCommand RotateLeft { get; } = new RoutedCommand(nameof(RotateLeft), typeof(Commander));

        /// <summary>
        ///     小
        /// </summary>
        public static RoutedCommand Reduce { get; } = new RoutedCommand(nameof(Reduce), typeof(Commander));

        /// <summary>
        ///     大
        /// </summary>
        public static RoutedCommand Enlarge { get; } = new RoutedCommand(nameof(Enlarge), typeof(Commander));

        /// <summary>
        ///     还原
        /// </summary>
        public static RoutedCommand Restore { get; } = new RoutedCommand(nameof(Restore), typeof(Commander));

        /// <summary>
        ///     打开
        /// </summary>
        public static RoutedCommand Open { get; } = new RoutedCommand(nameof(Open), typeof(Commander));

        /// <summary>
        ///     保存
        /// </summary>
        public static RoutedCommand Save { get; } = new RoutedCommand(nameof(Save), typeof(Commander));

        /// <summary>
        ///     选中
        /// </summary>
        public static RoutedCommand Selected { get; } = new RoutedCommand(nameof(Selected), typeof(Commander));

        /// <summary>
        ///     关闭
        /// </summary>
        public static RoutedCommand Close { get; } = new RoutedCommand(nameof(Close), typeof(Commander));

        /// <summary>
        ///     取消
        /// </summary>
        public static RoutedCommand Cancel { get; } = new RoutedCommand(nameof(Cancel), typeof(Commander));

        /// <summary>
        ///     确定
        /// </summary>
        public static RoutedCommand Confirm { get; } = new RoutedCommand(nameof(Confirm), typeof(Commander));

        /// <summary>
        ///     是
        /// </summary>
        public static RoutedCommand Yes { get; } = new RoutedCommand(nameof(Yes), typeof(Commander));

        /// <summary>
        ///     否
        /// </summary>
        public static RoutedCommand No { get; } = new RoutedCommand(nameof(No), typeof(Commander));

        /// <summary>
        ///     关闭所有
        /// </summary>
        public static RoutedCommand CloseAll { get; } = new RoutedCommand(nameof(CloseAll), typeof(Commander));

        /// <summary>
        ///     关闭其他
        /// </summary>
        public static RoutedCommand CloseOther { get; } = new RoutedCommand(nameof(CloseOther), typeof(Commander));

        /// <summary>
        ///     上一个
        /// </summary>
        public static RoutedCommand Prev { get; } = new RoutedCommand(nameof(Prev), typeof(Commander));

        /// <summary>
        ///     下一个
        /// </summary>
        public static RoutedCommand Next { get; } = new RoutedCommand(nameof(Next), typeof(Commander));



        /// <summary>
        ///     第一个
        /// </summary>
        public static RoutedCommand First { get; } = new RoutedCommand(nameof(First), typeof(Commander));

        /// <summary>
        ///     最后一个
        /// </summary>
        public static RoutedCommand Last { get; } = new RoutedCommand(nameof(Last), typeof(Commander));

        /// <summary>
        ///     上午
        /// </summary>
        public static RoutedCommand Am { get; } = new RoutedCommand(nameof(Am), typeof(Commander));

        /// <summary>
        ///     下午
        /// </summary>
        public static RoutedCommand Pm { get; } = new RoutedCommand(nameof(Pm), typeof(Commander));

        /// <summary>
        ///     确认
        /// </summary>
        public static RoutedCommand Sure { get; } = new RoutedCommand(nameof(Sure), typeof(Commander));

        /// <summary>
        ///     小时改变
        /// </summary>
        public static RoutedCommand HourChange { get; } = new RoutedCommand(nameof(HourChange), typeof(Commander));

        /// <summary>
        ///     分钟改变
        /// </summary>
        public static RoutedCommand MinuteChange { get; } = new RoutedCommand(nameof(MinuteChange), typeof(Commander));

        /// <summary>
        ///     秒改变
        /// </summary>
        public static RoutedCommand SecondChange { get; } = new RoutedCommand(nameof(SecondChange), typeof(Commander));

        /// <summary>
        ///     鼠标移动
        /// </summary>
        public static RoutedCommand MouseMove { get; } = new RoutedCommand(nameof(MouseMove), typeof(Commander));

        /// <summary>
        /// 粘贴
        /// </summary>
        public static RoutedCommand Paste { get; } = new RoutedCommand(nameof(Paste), typeof(Commander));

        /// <summary>
        /// 全选
        /// </summary>
        public static RoutedCommand CheckAll { get; } = new RoutedCommand(nameof(CheckAll), typeof(Commander));


        /// <summary>
        /// 删除
        /// </summary>
        public static RoutedCommand Delete { get; } = new RoutedCommand(nameof(Delete), typeof(Commander));

        /// <summary>
        /// 全部删除
        /// </summary>
        public static RoutedCommand DeleteAll { get; } = new RoutedCommand(nameof(DeleteAll), typeof(Commander));


        /// <summary>
        /// 删除选中
        /// </summary>
        public static RoutedCommand DeleteAllChecked { get; } = new RoutedCommand(nameof(DeleteAllChecked), typeof(Commander));


        /// <summary>
        /// 编辑
        /// </summary>
        public static RoutedCommand Edit { get; } = new RoutedCommand(nameof(Edit), typeof(Commander));


        /// <summary>
        /// 查看
        /// </summary>
        public static RoutedCommand View { get; } = new RoutedCommand(nameof(View), typeof(Commander));


        /// <summary> 渐隐藏 </summary>
        public static CollapsedOfOpacityCommand CollapsedOfOpacityCommand { get; } = new CollapsedOfOpacityCommand();

        /// <summary> 渐显示 </summary>
        public static VisibleOfOpacityCommand VisibleOfOpacityCommand { get; } = new VisibleOfOpacityCommand();

        public static CollapsedOfMarginLeftCommand CollapsedOfMarginLeftCommand { get; } = new CollapsedOfMarginLeftCommand();

        public static VisibleOfMarginLeftCommand VisibleOfMarginLeftCommand { get; } = new VisibleOfMarginLeftCommand();

        public static CollapsedOfMarginBottomCommand CollapsedOfMarginBottomCommand { get; } = new CollapsedOfMarginBottomCommand();

        //public static VisibleOfMarginBottomCommand VisibleOfMarginBottomCommand { get; } = new VisibleOfMarginBottomCommand();


        /// <summary> 打开应用程序 </summary>
        public static ProcessCommand ProcessCommand { get; } = new ProcessCommand();

        /// <summary> 关闭消息层 </summary>
        public static MessageLayerCloseCommand MessageLayerCloseCommand { get; } = new MessageLayerCloseCommand();

        //public static CollapsedSplitAnimationCommand CollapsedSplitAnimationCommand { get; } = new CollapsedSplitAnimationCommand();

        //public static VisibleSplitAnimationCommand VisibleSplitAnimationCommand { get; } = new VisibleSplitAnimationCommand();

        ///// <summary> 应用动画效果显示控件 </summary>
        //public static ActionVisibleCommand ActionVisibleCommand { get; } = new ActionVisibleCommand();

        /// <summary> 应用动画隐藏效果 </summary>
        //public static ActionHiddenCommand ActionHiddenCommand { get; } = new ActionHiddenCommand();

        //public static ShowMessageWindowCommand ShowMessageWindowCommand { get; } = new ShowMessageWindowCommand();

        public static ShowCopyWindowCommand ShowCopyWindowCommand { get; } = new ShowCopyWindowCommand();

        public static void InvalidateRequerySuggested()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
