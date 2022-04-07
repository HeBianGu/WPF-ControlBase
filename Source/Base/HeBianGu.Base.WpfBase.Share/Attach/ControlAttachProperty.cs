using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 公共附加属性 </summary>
    public static partial class Cattach
    {
        /************************************ Attach Property **************************************/

        #region FocusBackground 获得焦点背景色，

        public static readonly DependencyProperty FocusBackgroundProperty = DependencyProperty.RegisterAttached(
            "FocusBackground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(null));

        public static void SetFocusBackground(DependencyObject element, Brush value)
        {
            element.SetValue(FocusBackgroundProperty, value);
        }

        public static Brush GetFocusBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(FocusBackgroundProperty);
        }

        #endregion

        #region FocusForeground 获得焦点前景色，

        public static readonly DependencyProperty FocusForegroundProperty = DependencyProperty.RegisterAttached(
            "FocusForeground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(null));

        public static void SetFocusForeground(DependencyObject element, Brush value)
        {
            element.SetValue(FocusForegroundProperty, value);
        }

        public static Brush GetFocusForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(FocusForegroundProperty);
        }

        #endregion

        #region MouseOverBackgroundProperty 鼠标悬浮背景色

        public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.RegisterAttached(
            "MouseOverBackground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(null));

        public static void SetMouseOverBackground(DependencyObject element, Brush value)
        {
            element.SetValue(MouseOverBackgroundProperty, value);
        }

        public static Brush MouseOverBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(MouseOverBackgroundProperty);
        }

        #endregion

        #region MouseOverForegroundProperty 鼠标悬浮前景色

        public static readonly DependencyProperty MouseOverForegroundProperty = DependencyProperty.RegisterAttached(
            "MouseOverForeground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(null));

        public static void SetMouseOverForeground(DependencyObject element, Brush value)
        {
            element.SetValue(MouseOverForegroundProperty, value);
        }

        public static Brush MouseOverForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(MouseOverForegroundProperty);
        }

        #endregion

        #region MouseOverBackgroundProperty 选中背景色

        public static readonly DependencyProperty SelectBackgroundProperty = DependencyProperty.RegisterAttached(
            "SelectBackground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(null));

        public static void SetSelectBackground(DependencyObject element, Brush value)
        {
            element.SetValue(SelectBackgroundProperty, value);
        }

        public static Brush SelectBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(SelectBackgroundProperty);
        }

        #endregion

        #region MouseOverForegroundProperty 选中前景色

        public static readonly DependencyProperty SelectForegroundProperty = DependencyProperty.RegisterAttached(
            "SelectForeground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(null));

        public static void SetSelectForeground(DependencyObject element, Brush value)
        {
            element.SetValue(MouseOverForegroundProperty, value);
        }

        public static Brush SelectForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(SelectForegroundProperty);
        }

        #endregion

        #region PressBackgroundProperty 按下背景色

        public static readonly DependencyProperty PressBackgroundProperty = DependencyProperty.RegisterAttached(
            "PressBackground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(null));

        public static void SetPressBackground(DependencyObject element, Brush value)
        {
            element.SetValue(PressBackgroundProperty, value);
        }

        public static Brush PressBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(PressBackgroundProperty);
        }

        #endregion

        #region  PressForegroundProperty 按下前景色

        public static readonly DependencyProperty PressForegroundProperty = DependencyProperty.RegisterAttached(
            "PressForeground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(null));

        public static void SetPressForeground(DependencyObject element, Brush value)
        {
            element.SetValue(PressForegroundProperty, value);
        }

        public static Brush PressForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(PressForegroundProperty);
        }

        #endregion


        #region - PressBorderBrushProperty 按下边框颜色 -

        /// <summary>
        /// 按下边框颜色
        /// </summary>
        public static readonly DependencyProperty PressBorderBrushProperty = DependencyProperty.RegisterAttached(
            "PressBorderBrush", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPressBorderBrushChanged));

        public static Brush GetPressBorderBrush(DependencyObject d)
        {
            return (Brush)d.GetValue(PressBorderBrushProperty);
        }

        public static void SetPressBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(PressBorderBrushProperty, value);
        }

        static void OnPressBorderBrushChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        #endregion


        #region FocusBorderBrush 焦点边框色，输入控件

        public static readonly DependencyProperty FocusBorderBrushProperty = DependencyProperty.RegisterAttached(
            "FocusBorderBrush", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(null));
        public static void SetFocusBorderBrush(DependencyObject element, Brush value)
        {
            element.SetValue(FocusBorderBrushProperty, value);
        }
        public static Brush GetFocusBorderBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(FocusBorderBrushProperty);
        }

        #endregion

        #region SelectedForegroundBrush 选中前景色

        public static readonly DependencyProperty SelectedForegroundBrushProperty = DependencyProperty.RegisterAttached(
            "SelectedForegroundBrush", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(Brushes.Transparent));
        public static void SetSelectedForegroundBrush(DependencyObject element, Brush value)
        {
            element.SetValue(SelectedForegroundBrushProperty, value);
        }
        public static Brush GetSelectedForegroundBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(SelectedForegroundBrushProperty);
        }

        #endregion

        #region SelectedBackgroundBrush 选中背景色

        public static readonly DependencyProperty SelectedBackgroundBrushProperty = DependencyProperty.RegisterAttached(
            "SelectedBackgroundBrush", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(Brushes.Transparent));
        public static void SetSelectedBackgroundBrush(DependencyObject element, Brush value)
        {
            element.SetValue(SelectedBackgroundBrushProperty, value);
        }
        public static Brush GetSelectedBackgroundBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(SelectedBackgroundBrushProperty);
        }

        #endregion

        #region CheckedForegroundBrush 选中前景色

        public static readonly DependencyProperty CheckedForegroundBrushProperty = DependencyProperty.RegisterAttached(
            "CheckedForegroundBrush", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(Brushes.Transparent));

        public static void SetCheckedForegroundBrush(DependencyObject element, Brush value)
        {
            element.SetValue(CheckedForegroundBrushProperty, value);
        }

        public static Brush GetCheckedForegroundBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(CheckedForegroundBrushProperty);
        }

        #endregion

        #region CheckedBackgroundBrush 选中背景色

        public static readonly DependencyProperty CheckedBackgroundBrushProperty = DependencyProperty.RegisterAttached(
            "CheckedBackgroundBrush", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(Brushes.Transparent));

        public static void SetCheckedBackgroundBrush(DependencyObject element, Brush value)
        {
            element.SetValue(CheckedBackgroundBrushProperty, value);
        }

        public static Brush GetCheckedBackgroundBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(CheckedBackgroundBrushProperty);
        }

        #endregion

        #region MouseOverBorderBrush 鼠标进入边框色，输入控件

        public static readonly DependencyProperty MouseOverBorderBrushProperty =
            DependencyProperty.RegisterAttached("MouseOverBorderBrush", typeof(Brush), typeof(Cattach),
                new FrameworkPropertyMetadata(Brushes.Transparent,
                    FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Sets the brush used to draw the mouse over brush.
        /// </summary>
        public static void SetMouseOverBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseOverBorderBrushProperty, value);
        }

        /// <summary>
        /// Gets the brush used to draw the mouse over brush.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        [AttachedPropertyBrowsableForType(typeof(CheckBox))]
        [AttachedPropertyBrowsableForType(typeof(RadioButton))]
        [AttachedPropertyBrowsableForType(typeof(DatePicker))]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        [AttachedPropertyBrowsableForType(typeof(RichTextBox))]
        public static Brush GetMouseOverBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseOverBorderBrushProperty);
        }

        #endregion

        #region AttachContentProperty 附加组件模板
        /// <summary>
        /// 附加组件模板
        /// </summary>
        public static readonly DependencyProperty AttachContentProperty = DependencyProperty.RegisterAttached(
            "AttachContent", typeof(ControlTemplate), typeof(Cattach), new FrameworkPropertyMetadata(null));

        public static ControlTemplate GetAttachContent(DependencyObject d)
        {
            return (ControlTemplate)d.GetValue(AttachContentProperty);
        }

        public static void SetAttachContent(DependencyObject obj, ControlTemplate value)
        {
            obj.SetValue(AttachContentProperty, value);
        }
        #endregion

        #region WatermarkProperty 水印
        /// <summary>
        /// 水印
        /// </summary>
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached(
            "Watermark", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(""));

        public static string GetWatermark(DependencyObject d)
        {
            return (string)d.GetValue(WatermarkProperty);
        }

        public static void SetWatermark(DependencyObject obj, string value)
        {
            obj.SetValue(WatermarkProperty, value);
        }
        #endregion

        #region FIconProperty 字体图标

        /// <summary>
        /// 字体图标
        /// </summary>
        public static readonly DependencyProperty FIconProperty = DependencyProperty.RegisterAttached(
            "FIcon", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(""));

        public static string GetFIcon(DependencyObject d)
        {
            return (string)d.GetValue(FIconProperty);
        }

        public static void SetFIcon(DependencyObject obj, string value)
        {
            obj.SetValue(FIconProperty, value);
        }

        /// <summary>
        /// 字体图标
        /// </summary>
        public static readonly DependencyProperty FIconChangedProperty = DependencyProperty.RegisterAttached(
            "FIconChanged", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(""));

        public static string GetFIconChanged(DependencyObject d)
        {
            return (string)d.GetValue(FIconChangedProperty);
        }

        public static void SetFIconChanged(DependencyObject obj, string value)
        {
            obj.SetValue(FIconChangedProperty, value);
        }

        #endregion

        #region FIconSizeProperty 字体图标大小
        /// <summary>
        /// 字体图标
        /// </summary>
        public static readonly DependencyProperty FIconSizeProperty = DependencyProperty.RegisterAttached(
            "FIconSize", typeof(double), typeof(Cattach), new FrameworkPropertyMetadata(12D));

        public static double GetFIconSize(DependencyObject d)
        {
            return (double)d.GetValue(FIconSizeProperty);
        }

        public static void SetFIconSize(DependencyObject obj, double value)
        {
            obj.SetValue(FIconSizeProperty, value);
        }
        #endregion

        #region FIconMarginProperty 字体图标边距
        /// <summary>
        /// 字体图标
        /// </summary>
        public static readonly DependencyProperty FIconMarginProperty = DependencyProperty.RegisterAttached(
            "FIconMargin", typeof(Thickness), typeof(Cattach), new FrameworkPropertyMetadata(null));

        public static Thickness GetFIconMargin(DependencyObject d)
        {
            return (Thickness)d.GetValue(FIconMarginProperty);
        }

        public static void SetFIconMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(FIconMarginProperty, value);
        }
        #endregion

        #region AllowsAnimationProperty 启用旋转动画
        /// <summary>
        /// 启用旋转动画
        /// </summary>
        public static readonly DependencyProperty AllowsAnimationProperty = DependencyProperty.RegisterAttached("AllowsAnimation"
            , typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false, AllowsAnimationChanged));

        public static bool GetAllowsAnimation(DependencyObject d)
        {
            return (bool)d.GetValue(AllowsAnimationProperty);
        }

        public static void SetAllowsAnimation(DependencyObject obj, bool value)
        {
            obj.SetValue(AllowsAnimationProperty, value);
        }

        /// <summary>
        /// 旋转动画刻度
        /// </summary>
        private static DoubleAnimation RotateAnimation = new DoubleAnimation(0, new Duration(TimeSpan.FromMilliseconds(200)));

        /// <summary>
        /// 绑定动画事件
        /// </summary>
        private static void AllowsAnimationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var uc = d as FrameworkElement;
            //if (uc == null) return;
            //if (uc.RenderTransformOrigin == new Point(0, 0))
            //{
            //    uc.RenderTransformOrigin = new Point(0.5, 0.5);
            //    RotateTransform trans = new RotateTransform(0);
            //    uc.RenderTransform = trans;
            //}
            //var value = (bool)e.NewValue;
            //if (value)
            //{
            //    RotateAnimation.To = 180;
            //    uc.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, RotateAnimation);
            //}
            //else
            //{
            //    RotateAnimation.To = 0;
            //    uc.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, RotateAnimation);
            //}
        }
        #endregion

        #region CornerRadiusProperty Border圆角
        /// <summary>
        /// Border圆角
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached(
            "CornerRadius", typeof(CornerRadius), typeof(Cattach), new FrameworkPropertyMetadata(null));

        public static CornerRadius GetCornerRadius(DependencyObject d)
        {
            return (CornerRadius)d.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }
        #endregion

        #region double 类型的附加属性 Tag
        /// <summary>
        /// Border圆角
        /// </summary>
        public static readonly DependencyProperty DoubleAttachProperty = DependencyProperty.RegisterAttached(
            "DoubleAttach", typeof(double), typeof(Cattach), new FrameworkPropertyMetadata(0.0));

        public static double GetDoubleAttach(DependencyObject d)
        {
            return (double)d.GetValue(DoubleAttachProperty);
        }

        public static void SetDoubleAttach(DependencyObject obj, double value)
        {
            obj.SetValue(DoubleAttachProperty, value);
        }
        #endregion

        #region LabelProperty TextBox的头部Label
        /// <summary>
        /// TextBox的头部Label
        /// </summary>
        public static readonly DependencyProperty LabelProperty = DependencyProperty.RegisterAttached(
            "Label", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(null));

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static string GetLabel(DependencyObject d)
        {
            return (string)d.GetValue(LabelProperty);
        }

        public static void SetLabel(DependencyObject obj, string value)
        {
            obj.SetValue(LabelProperty, value);
        }
        #endregion

        #region LabelTemplateProperty TextBox的头部Label模板
        /// <summary>
        /// TextBox的头部Label模板
        /// </summary>
        public static readonly DependencyProperty LabelTemplateProperty = DependencyProperty.RegisterAttached(
            "LabelTemplate", typeof(ControlTemplate), typeof(Cattach), new FrameworkPropertyMetadata(null));

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static ControlTemplate GetLabelTemplate(DependencyObject d)
        {
            return (ControlTemplate)d.GetValue(LabelTemplateProperty);
        }

        public static void SetLabelTemplate(DependencyObject obj, ControlTemplate value)
        {
            obj.SetValue(LabelTemplateProperty, value);


        }
        #endregion

        /************************************ RoutedUICommand Behavior enable **************************************/

        #region IsClearTextButtonBehaviorEnabledProperty 清除输入框Text值按钮行为开关（设为ture时才会绑定事件）
        /// <summary>
        /// 清除输入框Text值按钮行为开关
        /// </summary>
        public static readonly DependencyProperty IsClearTextButtonBehaviorEnabledProperty = DependencyProperty.RegisterAttached("IsClearTextButtonBehaviorEnabled"
            , typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false, IsClearTextButtonBehaviorEnabledChanged));

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static bool GetIsClearTextButtonBehaviorEnabled(DependencyObject d)
        {
            return (bool)d.GetValue(IsClearTextButtonBehaviorEnabledProperty);
        }

        public static void SetIsClearTextButtonBehaviorEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsClearTextButtonBehaviorEnabledProperty, value);
        }

        /// <summary>
        /// 绑定清除Text操作的按钮事件
        /// </summary>
        private static void IsClearTextButtonBehaviorEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var button = d as Button;
            if (e.OldValue != e.NewValue && button != null)
            {
                button.CommandBindings.Add(ClearTextCommandBinding);
            }
        }

        #endregion

        #region 设置后面的描述信息
        /// <summary>
        /// 详细信息
        /// </summary>
        public static readonly DependencyProperty DetailProperty = DependencyProperty.RegisterAttached(
            "Detail", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(""));

        public static string GetDetail(DependencyObject d)
        {
            return (string)d.GetValue(DetailProperty);
        }

        public static void SetDetail(DependencyObject obj, string value)
        {
            obj.SetValue(DetailProperty, value);
        }

        #endregion

        #region IsOpenFileButtonBehaviorEnabledProperty 选择文件命令行为开关
        /// <summary>
        /// 选择文件命令行为开关
        /// </summary>
        public static readonly DependencyProperty IsOpenFileButtonBehaviorEnabledProperty = DependencyProperty.RegisterAttached("IsOpenFileButtonBehaviorEnabled"
            , typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false, IsOpenFileButtonBehaviorEnabledChanged));

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static bool GetIsOpenFileButtonBehaviorEnabled(DependencyObject d)
        {
            return (bool)d.GetValue(IsOpenFileButtonBehaviorEnabledProperty);
        }

        public static void SetIsOpenFileButtonBehaviorEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsOpenFileButtonBehaviorEnabledProperty, value);
        }

        private static void IsOpenFileButtonBehaviorEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var button = d as Button;
            if (e.OldValue != e.NewValue && button != null)
            {
                button.CommandBindings.Add(OpenFileCommandBinding);
            }
        }

        #endregion

        #region IsOpenFolderButtonBehaviorEnabledProperty 选择文件夹命令行为开关
        /// <summary>
        /// 选择文件夹命令行为开关
        /// </summary>
        public static readonly DependencyProperty IsOpenFolderButtonBehaviorEnabledProperty = DependencyProperty.RegisterAttached("IsOpenFolderButtonBehaviorEnabled"
            , typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false, IsOpenFolderButtonBehaviorEnabledChanged));

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static bool GetIsOpenFolderButtonBehaviorEnabled(DependencyObject d)
        {
            return (bool)d.GetValue(IsOpenFolderButtonBehaviorEnabledProperty);
        }

        public static void SetIsOpenFolderButtonBehaviorEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsOpenFolderButtonBehaviorEnabledProperty, value);
        }

        private static void IsOpenFolderButtonBehaviorEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var button = d as Button;
            if (e.OldValue != e.NewValue && button != null)
            {
                button.CommandBindings.Add(OpenFolderCommandBinding);
            }
        }

        #endregion

        #region IsSaveFileButtonBehaviorEnabledProperty 选择文件保存路径及名称
        /// <summary>
        /// 选择文件保存路径及名称
        /// </summary>
        public static readonly DependencyProperty IsSaveFileButtonBehaviorEnabledProperty = DependencyProperty.RegisterAttached("IsSaveFileButtonBehaviorEnabled"
            , typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false, IsSaveFileButtonBehaviorEnabledChanged));

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static bool GetIsSaveFileButtonBehaviorEnabled(DependencyObject d)
        {
            return (bool)d.GetValue(IsSaveFileButtonBehaviorEnabledProperty);
        }

        public static void SetIsSaveFileButtonBehaviorEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsSaveFileButtonBehaviorEnabledProperty, value);
        }

        private static void IsSaveFileButtonBehaviorEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var button = d as Button;
            if (e.OldValue != e.NewValue && button != null)
            {
                button.CommandBindings.Add(SaveFileCommandBinding);
            }
        }

        #endregion

        /************************************ RoutedUICommand **************************************/

        #region ClearTextCommand 清除输入框Text事件命令

        /// <summary>
        /// 清除输入框Text事件命令，需要使用IsClearTextButtonBehaviorEnabledChanged绑定命令
        /// </summary>
        public static RoutedUICommand ClearTextCommand { get; private set; }

        /// <summary>
        /// ClearTextCommand绑定事件
        /// </summary>
        private static readonly CommandBinding ClearTextCommandBinding;

        /// <summary>
        /// 清除输入框文本值
        /// </summary>
        private static void ClearButtonClick(object sender, ExecutedRoutedEventArgs e)
        {
            var tbox = e.Parameter as FrameworkElement;
            if (tbox == null) return;
            if (tbox is TextBox)
            {
                ((TextBox)tbox).Clear();
            }
            if (tbox is PasswordBox)
            {
                ((PasswordBox)tbox).Clear();
            }
            if (tbox is ComboBox)
            {
                var cb = tbox as ComboBox;
                cb.SelectedItem = null;
                cb.Text = string.Empty;
            }

            if (tbox is DatePicker)
            {
                var dp = tbox as DatePicker;
                dp.SelectedDate = null;
                dp.Text = string.Empty;
            }
            tbox.Focus();
        }

        #endregion

        #region OpenFileCommand 选择文件命令

        /// <summary>
        /// 选择文件命令，需要使用IsClearTextButtonBehaviorEnabledChanged绑定命令
        /// </summary>
        public static RoutedUICommand OpenFileCommand { get; private set; }

        /// <summary>
        /// OpenFileCommand绑定事件
        /// </summary>
        private static readonly CommandBinding OpenFileCommandBinding;

        /// <summary>
        /// 执行OpenFileCommand
        /// </summary>
        private static void OpenFileButtonClick(object sender, ExecutedRoutedEventArgs e)
        {
            var tbox = e.Parameter as FrameworkElement;
            var txt = tbox as TextBox;
            string filter = txt.Tag == null ? "所有文件(*.*)|*.*" : txt.Tag.ToString();
            if (filter.Contains(".bin"))
            {
                filter += "|所有文件(*.*)|*.*";
            }
            if (txt == null) return;
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "请选择文件";
            //“图像文件(*.bmp, *.jpg)|*.bmp;*.jpg|所有文件(*.*)|*.*”
            fd.Filter = filter;
            fd.FileName = txt.Text.Trim();
            if (fd.ShowDialog() == true)
            {
                txt.Text = fd.FileName;
            }
            tbox.Focus();
        }

        #endregion

        #region OpenFolderCommand 选择文件夹命令

        /// <summary>
        /// 选择文件夹命令
        /// </summary>
        public static RoutedUICommand OpenFolderCommand { get; private set; }

        /// <summary>
        /// OpenFolderCommand绑定事件
        /// </summary>
        private static readonly CommandBinding OpenFolderCommandBinding;

        /// <summary>
        /// 执行OpenFolderCommand
        /// </summary>
        private static void OpenFolderButtonClick(object sender, ExecutedRoutedEventArgs e)
        {
            //var tbox = e.Parameter as FrameworkElement;
            //var txt = tbox as TextBox;
            //if (txt == null) return;
            //System.Windows.Forms.FolderBrowserDialog fd = new System.Windows.Forms.FolderBrowserDialog();
            //fd.Description = "请选择文件路径";
            //fd.SelectedPath = txt.Text.Trim();
            //if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    txt.Text = fd.SelectedPath;
            //}
            //tbox.Focus();


        }

        #endregion

        #region SaveFileCommand 选择文件保存路径及名称

        /// <summary>
        /// 选择文件保存路径及名称
        /// </summary>
        public static RoutedUICommand SaveFileCommand { get; private set; }

        /// <summary>
        /// SaveFileCommand绑定事件
        /// </summary>
        private static readonly CommandBinding SaveFileCommandBinding;

        /// <summary>
        /// 执行OpenFileCommand
        /// </summary>
        private static void SaveFileButtonClick(object sender, ExecutedRoutedEventArgs e)
        {
            //var tbox = e.Parameter as FrameworkElement;
            //var txt = tbox as TextBox;
            //if (txt == null) return;
            //SaveFileDialog fd = new SaveFileDialog();
            //fd.Title = "文件保存路径";
            //fd.Filter = "所有文件(*.*)|*.*";
            //fd.FileName = txt.Text.Trim();
            //if (fd.ShowDialog() == DialogResult.OK)
            //{
            //    txt.Text = fd.FileName;
            //}
            //tbox.Focus();
        }

        #endregion

        #region - PassWordBox -

        public static readonly DependencyProperty PasswordProperty =
          DependencyProperty.RegisterAttached("Password",
          typeof(string), typeof(Cattach),
          new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));
        public static readonly DependencyProperty AttachProperty = DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(Cattach), new PropertyMetadata(false, Attach));

        private static readonly DependencyProperty IsUpdatingProperty = DependencyProperty.RegisterAttached("IsUpdating", typeof(bool), typeof(Cattach));

        public static void SetAttach(DependencyObject dp, bool value)
        {
            dp.SetValue(AttachProperty, value);
        }
        public static bool GetAttach(DependencyObject dp)
        {
            return (bool)dp.GetValue(AttachProperty);
        }
        public static string GetPassword(DependencyObject dp)
        {
            return (string)dp.GetValue(PasswordProperty);
        }
        public static void SetPassword(DependencyObject dp, string value)
        {
            dp.SetValue(PasswordProperty, value);
        }
        private static bool GetIsUpdating(DependencyObject dp)
        {
            return (bool)dp.GetValue(IsUpdatingProperty);
        }
        private static void SetIsUpdating(DependencyObject dp, bool value)
        {
            dp.SetValue(IsUpdatingProperty, value);
        }
        private static void OnPasswordPropertyChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            passwordBox.PasswordChanged -= PasswordChanged;
            if (!(bool)GetIsUpdating(passwordBox))
            {
                passwordBox.Password = (string)e.NewValue;
            }
            passwordBox.PasswordChanged += PasswordChanged;
        }
        private static void Attach(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox == null)
                return;
            if ((bool)e.OldValue)
            {
                passwordBox.PasswordChanged -= PasswordChanged;
            }
            if ((bool)e.NewValue)
            {
                passwordBox.PasswordChanged += PasswordChanged;
            }
        }
        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            SetIsUpdating(passwordBox, true);
            SetPassword(passwordBox, passwordBox.Password);
            SetIsUpdating(passwordBox, false);
        }

        #endregion

        #region - ListBox - 

        public static IList GetSelectedItems(DependencyObject obj)
        {
            return (IList)obj.GetValue(SelectedItemsProperty);
        }

        public static void SetSelectedItems(DependencyObject obj, IList value)
        {
            obj.SetValue(SelectedItemsProperty, value);
        }

        //Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...

        public static readonly DependencyProperty SelectedItemsProperty =

            DependencyProperty.RegisterAttached("SelectedItems", typeof(IList), typeof(Cattach), new PropertyMetadata(OnSelectedItemsChanged));

        static public void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var listBox = d as ListBox;

            if ((listBox != null) && (listBox.SelectionMode == SelectionMode.Multiple))
            {
                if (e.OldValue != null)
                {
                    listBox.SelectionChanged -= OnlistBoxSelectionChanged;
                }

                IList collection = e.NewValue as IList;

                listBox.SelectedItems.Clear();

                if (collection != null)
                {
                    foreach (var item in collection)
                    {
                        listBox.SelectedItems.Add(item);
                    }
                    listBox.SelectionChanged += OnlistBoxSelectionChanged;
                }
            }
        }

        static void OnlistBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IList dataSource = GetSelectedItems(sender as DependencyObject);

            //添加用户选中的当前项.
            foreach (var item in e.AddedItems)
            {
                dataSource.Add(item);
            }

            //删除用户取消选中的当前项
            foreach (var item in e.RemovedItems)
            {
                dataSource.Remove(item);
            }
        }

        #endregion

        #region bool 类型的附加属性 Tag
        /// <summary>
        /// bool类型附加属性
        /// </summary>
        public static readonly DependencyProperty BoolProperty = DependencyProperty.RegisterAttached(
            "Bool", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false));

        public static bool GetBool(DependencyObject d)
        {
            return (bool)d.GetValue(BoolProperty);
        }

        public static void SetBool(DependencyObject obj, bool value)
        {
            obj.SetValue(BoolProperty, value);
        }
        #endregion

        #region - 将焦点设置在控件上 -
        /// <summary>
        /// 将焦点设置在控件上
        /// </summary>
        public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.RegisterAttached(
            "IsFocused", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false, (l, k) =>
             {
                 if (k.NewValue == k.NewValue) return;

                 if (l is UIElement element)
                 {
                     element?.Focus();
                 }
             }
            ));

        public static bool GetIsFocused(DependencyObject d)
        {
            return (bool)d.GetValue(DoubleAttachProperty);
        }

        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(DoubleAttachProperty, value);
        }
        #endregion

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static Cattach()
        {
            //ClearTextCommand
            ClearTextCommand = new RoutedUICommand();
            ClearTextCommandBinding = new CommandBinding(ClearTextCommand);
            ClearTextCommandBinding.Executed += ClearButtonClick;
            //OpenFileCommand
            OpenFileCommand = new RoutedUICommand();
            OpenFileCommandBinding = new CommandBinding(OpenFileCommand);
            OpenFileCommandBinding.Executed += OpenFileButtonClick;
            //OpenFolderCommand
            OpenFolderCommand = new RoutedUICommand();
            OpenFolderCommandBinding = new CommandBinding(OpenFolderCommand);
            OpenFolderCommandBinding.Executed += OpenFolderButtonClick;

            SaveFileCommand = new RoutedUICommand();
            SaveFileCommandBinding = new CommandBinding(SaveFileCommand);
            SaveFileCommandBinding.Executed += SaveFileButtonClick;
        }
    }

    static partial class Cattach
    {

        #region - 等待效果 -

        public static bool GetIsBuzy(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsBuzyProperty);
        }

        public static void SetIsBuzy(DependencyObject obj, bool value)
        {
            obj.SetValue(IsBuzyProperty, value);
        }

        /// <summary> 是否等待 </summary>
        public static readonly DependencyProperty IsBuzyProperty =
            DependencyProperty.RegisterAttached("IsBuzy", typeof(bool), typeof(Cattach), new PropertyMetadata(false));

        public static string GetBuzyText(DependencyObject obj)
        {
            return (string)obj.GetValue(BuzyTextProperty);
        }

        public static void SetBuzyText(DependencyObject obj, string value)
        {
            obj.SetValue(BuzyTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for BuzyText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BuzyTextProperty =
            DependencyProperty.RegisterAttached("BuzyText", typeof(string), typeof(Cattach), new PropertyMetadata("请等待"));

        #endregion

        #region - Path -

        public static Geometry GetPath(DependencyObject obj)
        {
            return (Geometry)obj.GetValue(PathProperty);
        }

        public static void SetPath(DependencyObject obj, Geometry value)
        {
            obj.SetValue(PathProperty, value);
        }

        /// <summary> 是否等待 </summary>
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.RegisterAttached("Path", typeof(Geometry), typeof(Cattach), new PropertyMetadata(default(Geometry)));

        #endregion

    }

    static partial class Cattach
    {

        #region - 应用动画效果的显示、隐藏 -


        public static bool GetIsClose(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCloseProperty);
        }

        public static void SetIsClose(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCloseProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty IsCloseProperty =
            DependencyProperty.RegisterAttached("IsClose", typeof(bool), typeof(Cattach), new PropertyMetadata(false, OnIsCloseChanged));

        static public void OnIsCloseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as UIElement;

            var actions = Cattach.GetVisibleActions(element);

            if (actions == null) return;

            bool v = (bool)e.NewValue;

            if (v)
            {
                //  Do ：显示动画
                foreach (var item in actions)
                {
                    item.BeginVisible(element);
                }
            }
            else
            {
                //  Do ：隐藏动画
                foreach (var item in actions)
                {
                    item.BeginHidden(element, () =>
                     {
                         if (element is Window window)
                         {
                             window.Close();
                         }

                         return;
                     });
                }
            }
        }

        public static bool GetIsVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsVisibleProperty);
        }

        public static void SetIsVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsVisibleProperty, value);
        }

        /// <summary> 应用控件显示和隐藏 </summary>
        public static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.RegisterAttached("IsVisible", typeof(bool), typeof(Cattach), new PropertyMetadata(true, OnIsVisibleChanged));

        static public void OnIsVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as UIElement;

            var actions = Cattach.GetVisibleActions(element);

            if (actions == null) return;

            bool v = (bool)e.NewValue;

            if (v)
            {

                //  Do ：显示动画
                foreach (var item in actions)
                {
                    item.BeginVisible(element);
                }
            }
            else
            {
                //  Do ：隐藏动画
                foreach (var item in actions)
                {
                    item.BeginHidden(element);
                }

            }
        }


        public static ActionCollection GetVisibleActions(DependencyObject obj)
        {
            return (ActionCollection)obj.GetValue(VisibleActionsProperty);
        }

        public static void SetVisibleActions(DependencyObject obj, ActionCollection value)
        {
            obj.SetValue(VisibleActionsProperty, value);
        }

        /// <summary> 显示隐藏过度效果 </summary>
        public static readonly DependencyProperty VisibleActionsProperty =
            DependencyProperty.RegisterAttached("VisibleActions", typeof(ActionCollection), typeof(Cattach), new PropertyMetadata(new ActionCollection()));

        #endregion



    }


    static partial class Cattach
    {
        /// <summary>
        /// 是否启用关闭按钮
        /// </summary>
        public static readonly DependencyProperty IsUseCloseProperty = DependencyProperty.RegisterAttached(
            "IsUseClose", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsUseCloseChanged));

        public static bool GetIsUseClose(DependencyObject d)
        {
            return (bool)d.GetValue(IsUseCloseProperty);
        }

        public static void SetIsUseClose(DependencyObject obj, bool value)
        {
            obj.SetValue(IsUseCloseProperty, value);
        }

        static void OnIsUseCloseChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        /// <summary>
        /// 是否正在拖动 DragEnter
        /// </summary>
        public static readonly DependencyProperty IsDragEnterProperty = DependencyProperty.RegisterAttached(
            "IsDragEnter", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsDragEnterChanged));

        public static bool GetIsDragEnter(DependencyObject d)
        {
            return (bool)d.GetValue(IsDragEnterProperty);
        }

        public static void SetIsDragEnter(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDragEnterProperty, value);
        }

        static void OnIsDragEnterChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }

    }

    static partial class Cattach
    {
        /// <summary>
        /// 子内容中的间距
        /// </summary>
        public static readonly DependencyProperty CellMarginProperty = DependencyProperty.RegisterAttached(
            "CellMargin", typeof(Thickness), typeof(Cattach), new FrameworkPropertyMetadata(default(Thickness), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnCellMarginChanged));

        public static Thickness GetCellMargin(DependencyObject d)
        {
            return (Thickness)d.GetValue(CellMarginProperty);
        }

        public static void SetCellMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(CellMarginProperty, value);
        }

        static void OnCellMarginChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        /// <summary>
        /// 布局方式
        /// </summary>
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.RegisterAttached(
            "Orientation", typeof(Orientation), typeof(Cattach), new FrameworkPropertyMetadata(default(Orientation), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnOrientationChanged));

        public static Orientation GetOrientation(DependencyObject d)
        {
            return (Orientation)d.GetValue(OrientationProperty);
        }

        public static void SetOrientation(DependencyObject obj, Orientation value)
        {
            obj.SetValue(OrientationProperty, value);
        }

        static void OnOrientationChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        /// <summary>
        /// 是否选中
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.RegisterAttached(
            "IsSelected", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsSelectedChanged));

        public static bool GetIsSelected(DependencyObject d)
        {
            return (bool)d.GetValue(IsSelectedProperty);
        }

        public static void SetIsSelected(DependencyObject obj, bool value)
        {
            obj.SetValue(IsSelectedProperty, value);
        }

        static void OnIsSelectedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }

        /// <summary>
        /// 值
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
            "Value", typeof(Double), typeof(Cattach), new FrameworkPropertyMetadata(default(Double), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged));

        public static Double GetValue(DependencyObject d)
        {
            return (Double)d.GetValue(ValueProperty);
        }

        public static void SetValue(DependencyObject obj, Double value)
        {
            obj.SetValue(ValueProperty, value);
        }

        static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        /// <summary>
        /// 文本
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached(
            "Text", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextChanged));

        public static string GetText(DependencyObject d)
        {
            return (string)d.GetValue(TextProperty);
        }

        public static void SetText(DependencyObject obj, string value)
        {
            obj.SetValue(TextProperty, value);
        }

        static void OnTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        /// <summary>
        /// 是否显示等待
        /// </summary>
        public static readonly DependencyProperty IsIndeterminateProperty = DependencyProperty.RegisterAttached(
            "IsIndeterminate", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsIndeterminateChanged));

        public static bool GetIsIndeterminate(DependencyObject d)
        {
            return (bool)d.GetValue(IsIndeterminateProperty);
        }

        public static void SetIsIndeterminate(DependencyObject obj, bool value)
        {
            obj.SetValue(IsIndeterminateProperty, value);
        }

        static void OnIsIndeterminateChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        /// <summary>
        /// 是否始终显示
        /// </summary>
        public static readonly DependencyProperty IsStayOpenProperty = DependencyProperty.RegisterAttached(
            "IsStayOpen", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsStayOpenChanged));

        public static string GetIsStayOpen(DependencyObject d)
        {
            return (string)d.GetValue(IsStayOpenProperty);
        }

        public static void SetIsStayOpen(DependencyObject obj, string value)
        {
            obj.SetValue(IsStayOpenProperty, value);
        }

        static void OnIsStayOpenChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        /// <summary>
        /// 选中文本
        /// </summary>
        public static readonly DependencyProperty SelectedTextProperty = DependencyProperty.RegisterAttached(
            "SelectedText", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedTextChanged));

        public static string GetSelectedText(DependencyObject d)
        {
            return (string)d.GetValue(SelectedTextProperty);
        }

        public static void SetSelectedText(DependencyObject obj, string value)
        {
            obj.SetValue(SelectedTextProperty, value);
        }

        static void OnSelectedTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        /// <summary>
        /// 未选中文本
        /// </summary>
        public static readonly DependencyProperty UnSelectedTextProperty = DependencyProperty.RegisterAttached(
            "UnSelectedText", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnUnSelectedTextChanged));

        public static string GetUnSelectedText(DependencyObject d)
        {
            return (string)d.GetValue(UnSelectedTextProperty);
        }

        public static void SetUnSelectedText(DependencyObject obj, string value)
        {
            obj.SetValue(UnSelectedTextProperty, value);
        }

        static void OnUnSelectedTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }

        #region - 标题相关 - 


        /// <summary>
        /// 标题
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.RegisterAttached(
            "Title", typeof(object), typeof(Cattach), new FrameworkPropertyMetadata(default(object), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTitleChanged));

        public static object GetTitle(DependencyObject d)
        {
            return (object)d.GetValue(TitleProperty);
        }

        public static void SetTitle(DependencyObject obj, object value)
        {
            obj.SetValue(TitleProperty, value);
        }

        static void OnTitleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        /// <summary>
        /// 标题模板
        /// </summary>
        public static readonly DependencyProperty TitleTemplateProperty = DependencyProperty.RegisterAttached(
            "TitleTemplate", typeof(ControlTemplate), typeof(Cattach), new FrameworkPropertyMetadata(default(ControlTemplate), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTitleTemplateChanged));

        public static ControlTemplate GetTitleTemplate(DependencyObject d)
        {
            return (ControlTemplate)d.GetValue(TitleTemplateProperty);
        }

        public static void SetTitleTemplate(DependencyObject obj, ControlTemplate value)
        {
            obj.SetValue(TitleTemplateProperty, value);
        }

        static void OnTitleTemplateChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        /// <summary>
        /// 标题背景色
        /// </summary>
        public static readonly DependencyProperty TitleBackgroundProperty = DependencyProperty.RegisterAttached(
            "TitleBackground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTitleBackgroundChanged));

        public static Brush GetTitleBackground(DependencyObject d)
        {
            return (Brush)d.GetValue(TitleBackgroundProperty);
        }

        public static void SetTitleBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(TitleBackgroundProperty, value);
        }

        static void OnTitleBackgroundChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        /// <summary>
        /// 标题前景色
        /// </summary>
        public static readonly DependencyProperty TitleForegroundProperty = DependencyProperty.RegisterAttached(
            "TitleForeground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTitleForegroundChanged));

        public static Brush GetTitleForeground(DependencyObject d)
        {
            return (Brush)d.GetValue(TitleForegroundProperty);
        }

        public static void SetTitleForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(TitleForegroundProperty, value);
        }

        static void OnTitleForegroundChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        /// <summary>
        /// 标题边框线颜色
        /// </summary>
        public static readonly DependencyProperty TitleBorderBrushProperty = DependencyProperty.RegisterAttached(
            "TitleBorderBrush", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTitleBorderBrushChanged));

        public static Brush GetTitleBorderBrush(DependencyObject d)
        {
            return (Brush)d.GetValue(TitleBorderBrushProperty);
        }

        public static void SetTitleBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(TitleBorderBrushProperty, value);
        }

        static void OnTitleBorderBrushChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        /// <summary>
        /// 标题边框
        /// </summary>
        public static readonly DependencyProperty TitleBorderThicknessProperty = DependencyProperty.RegisterAttached(
            "TitleBorderThickness", typeof(Thickness), typeof(Cattach), new FrameworkPropertyMetadata(default(Thickness), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTitleBorderThicknessChanged));

        public static Thickness GetTitleBorderThickness(DependencyObject d)
        {
            return (Thickness)d.GetValue(TitleBorderThicknessProperty);
        }

        public static void SetTitleBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(TitleBorderThicknessProperty, value);
        }

        static void OnTitleBorderThicknessChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        #endregion

        #region - 历史记录相关 -

        /// <summary>
        /// 是否启用历史数据
        /// </summary>
        public static readonly DependencyProperty IsUseHistoryProperty = DependencyProperty.RegisterAttached(
            "IsUseHistory", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsUseHistoryChanged));

        public static bool GetIsUseHistory(DependencyObject d)
        {
            return (bool)d.GetValue(IsUseHistoryProperty);
        }

        public static void SetIsUseHistory(DependencyObject obj, bool value)
        {
            obj.SetValue(IsUseHistoryProperty, value);
        }

        static void OnIsUseHistoryChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var textBox = sender as TextBox;

            if (textBox == null) return;

            if (args.NewValue is bool b == true)
            {
                textBox.LostFocus -= TextBox_LostFocus;
                textBox.LostFocus += TextBox_LostFocus;
            }
            else
            {
                textBox.LostFocus -= TextBox_LostFocus;
            }
        }

        private static void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;

            var datas = Cattach.GetHistoryData(textBox);

            var capacity = Cattach.GetCapacity(textBox);

            var regex = Cattach.GetRegex(textBox);

            if (datas.Count > capacity)
            {
                datas = datas.Take(capacity)?.ToObservable();
            }

            if (!Regex.IsMatch(textBox.Text, regex)) return;

            datas.Remove(textBox.Text);
            datas.Insert(0, textBox.Text);

            Cattach.SetHistoryData(textBox, datas);
        }


        /// <summary>
        /// 历史数据
        /// </summary>
        public static readonly DependencyProperty HistoryDataProperty = DependencyProperty.RegisterAttached(
            "HistoryData", typeof(ObservableCollection<string>), typeof(Cattach), new FrameworkPropertyMetadata(new ObservableCollection<string>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnHistoryDataChanged));

        public static ObservableCollection<string> GetHistoryData(DependencyObject d)
        {
            return (ObservableCollection<string>)d.GetValue(HistoryDataProperty);
        }

        public static void SetHistoryData(DependencyObject obj, ObservableCollection<string> value)
        {
            obj.SetValue(HistoryDataProperty, value);
        }

        static void OnHistoryDataChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        /// <summary>
        /// 数据容量
        /// </summary>
        public static readonly DependencyProperty CapacityProperty = DependencyProperty.RegisterAttached(
            "Capacity", typeof(int), typeof(Cattach), new FrameworkPropertyMetadata(10, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnCapacityChanged));

        public static int GetCapacity(DependencyObject d)
        {
            return (int)d.GetValue(CapacityProperty);
        }

        public static void SetCapacity(DependencyObject obj, int value)
        {
            obj.SetValue(CapacityProperty, value);
        }

        static void OnCapacityChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        /// <summary>
        /// 验证数据的正则表达式 默认不等于空
        /// </summary>
        public static readonly DependencyProperty RegexProperty = DependencyProperty.RegisterAttached(
            "Regex", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(@"\S", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnRegexChanged));

        public static string GetRegex(DependencyObject d)
        {
            return (string)d.GetValue(RegexProperty);
        }

        public static void SetRegex(DependencyObject obj, string value)
        {
            obj.SetValue(RegexProperty, value);
        }

        static void OnRegexChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        /// <summary>
        /// 选中历史数据改变
        /// </summary>
        public static readonly DependencyProperty SelectedHistroyItemProperty = DependencyProperty.RegisterAttached(
            "SelectedHistroyItem", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedHistroyItemChanged));

        public static string GetSelectedHistroyItem(DependencyObject d)
        {
            return (string)d.GetValue(SelectedHistroyItemProperty);
        }

        public static void SetSelectedHistroyItem(DependencyObject obj, string value)
        {
            obj.SetValue(SelectedHistroyItemProperty, value);
        }

        static void OnSelectedHistroyItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var textBox = sender as TextBox;

            string config = args.NewValue as string;

            if (config == null) return;

            var regex = Cattach.GetRegex(textBox);

            if (!Regex.IsMatch(config, regex)) return;

            textBox.Text = config;
        }

        #endregion
    }

    static partial class Cattach
    {
        /// <summary>
        /// 要注册的行为
        /// </summary>
        public static readonly DependencyProperty BehaviorsProperty = DependencyProperty.RegisterAttached(
            "Behaviors", typeof(Behaviors), typeof(Cattach), new FrameworkPropertyMetadata(new Behaviors(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBehaviorsChanged));

        public static Behaviors GetBehaviors(DependencyObject d)
        {
            return (Behaviors)d.GetValue(BehaviorsProperty);
        }

        public static void SetBehaviors(DependencyObject obj, Behaviors value)
        {
            obj.SetValue(BehaviorsProperty, value);
        }

        static void OnBehaviorsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            Behaviors o = args.OldValue as Behaviors;

            Behaviors n = args.NewValue as Behaviors;

            BehaviorCollection bc = Interaction.GetBehaviors(sender);

            //  Do ：先删除
            if (o != null)
            {
                foreach (var b in o)
                {
                    var behavior = bc.FirstOrDefault(beh => beh.GetType() == b.GetType());

                    if (behavior != null)
                        bc.Remove(behavior);
                }
            }

            //  Do ：再更新
            if (n != null)
            {
                foreach (var b in n)
                {
                    var behavior = bc.FirstOrDefault(beh => beh.GetType() == b.GetType());

                    if (behavior != null)
                    {
                        bc.Remove(behavior);
                    }

                    var instance = Activator.CreateInstance(b.GetType()) as Behavior;

                    bc.Add(instance);
                }
            }

        }
    }

    public class Behaviors : ObservableCollection<Behavior>
    {

    }
}
