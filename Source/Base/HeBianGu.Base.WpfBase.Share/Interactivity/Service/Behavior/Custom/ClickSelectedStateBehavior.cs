// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls.Primitives;


namespace HeBianGu.Base.WpfBase
{
    /// <summary> 设置按钮就有选中和未选中效果</summary>
    public class ClickSelectedStateBehavior : Behavior<ButtonBase>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Click += AssociatedObject_Click;
        }

        private void AssociatedObject_Click(object sender, RoutedEventArgs e)
        {
            bool s = Cattach.GetIsSelected(AssociatedObject);

            Cattach.SetIsSelected(AssociatedObject, !s);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= AssociatedObject_Click;
        }
    }
}