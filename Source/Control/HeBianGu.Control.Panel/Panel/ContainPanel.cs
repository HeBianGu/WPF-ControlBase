// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Animation;
using System;
using System.Windows;
using System.Windows.Input;

namespace HeBianGu.Control.Panel
{
    public partial class ContainPanel : AnimationPanel
    {
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Source == this && ContainPanelSetting.Instance.UseClickClose)
                this.Remove();
        }
        public ITransition AnimationAction
        {
            get { return (ITransition)GetValue(AnimationActionProperty); }
            set { SetValue(AnimationActionProperty, value); }
        }

        public static readonly DependencyProperty AnimationActionProperty =
            DependencyProperty.Register("AnimationAction", typeof(ITransition), typeof(ContainPanel), new PropertyMetadata(new NoneTransition(), (d, e) =>
            {
                ContainPanel control = d as ContainPanel;
                if (control == null) 
                    return;
                ITransition config = e.NewValue as ITransition;
            }));

        protected override Size ArrangeOverride(Size finalSize)
        {
            System.Windows.Controls.UIElementCollection children = this.Children;
            if (children == null || children.Count == 0) return finalSize;
            Point center = new Point(finalSize.Width / 2, finalSize.Height / 2);
            foreach (FrameworkElement elment in children)
            {
                elment.Measure(finalSize);
                elment.Arrange(new Rect(finalSize));
            }
            return finalSize;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return base.MeasureOverride(availableSize);
        }

        protected override void RefreshAnimation()
        {

        }

        public void Add(UIElement element)
        {
            foreach (UIElement item in this.Children)
            {
                if (ContainPanelSetting.Instance.CanAddMatch?.Invoke(item, element) == false)
                    return;
            }
            this.Children.Add(element);

            var transition = ContainPanel.GetTransition(element);
            if (transition != null)
            {
                transition.BeginCurrent(element);
            }
            else
            {
                this.AnimationAction?.BeginCurrent(element);
            }

            if (this.Children.Count == 1)
            {
                this.Visibility = Visibility.Visible;
                DoubleStoryboardEngine.Create(0, 1, 0.3, "Opacity").Start(this);
            }
        }

        public void Remove()
        {
            if (this.Children == null) return;

            if (this.Children.Count == 0) return;

            UIElement element = this.Children[this.Children.Count - 1];
            Action compate = () =>
              {
                  this.Children.Remove(element);

                  if (this.Children.Count == 0)
                  {
                      DoubleStoryboardEngine.Create(1, 0, 0.3, "Opacity").Start(this, l => l.Visibility = Visibility.Collapsed);
                  }
              };

            var transition = ContainPanel.GetTransition(element);

            if (transition == null)
            {
                this.AnimationAction?.BeginPrevious(element, compate);
            }
            else
            {
                transition?.BeginPrevious(element, compate);
            }
        }

        public void Remove(UIElement element)
        {
            this.AnimationAction?.BeginHidden(element, () =>
             {
                 this.Children.Remove(element);
             });
        }
    }

    partial class ContainPanel
    {

        public static ITransition GetTransition(DependencyObject obj)
        {
            return (ITransition)obj.GetValue(TransitionProperty);
        }

        public static void SetTransition(DependencyObject obj, ITransition value)
        {
            obj.SetValue(TransitionProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty TransitionProperty =
            DependencyProperty.RegisterAttached("Transition", typeof(ITransition), typeof(ContainPanel), new PropertyMetadata(default(ITransition), OnTransitionChanged));

        static public void OnTransitionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            ITransition n = (ITransition)e.NewValue;

            ITransition o = (ITransition)e.OldValue;
        }

    }
}
