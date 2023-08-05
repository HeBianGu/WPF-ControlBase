// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
    [TemplatePart(Name = "PART_TransitionerSlide_Old")]
    [TemplatePart(Name = "PART_TransitionerSlide_New")]
    public partial class SwtichTransitioner : ContentControl, IZIndexController
    {
        private Point? _nextTransitionOrigin = null;

        static SwtichTransitioner()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SwtichTransitioner), new FrameworkPropertyMetadata(typeof(SwtichTransitioner)));
        }

        private TransitionerSlide _transitionerSlide_Old = null;
        private TransitionerSlide _transitionerSlide_New = null;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _transitionerSlide_Old = GetTemplateChild("PART_TransitionerSlide_Old") as TransitionerSlide;
            _transitionerSlide_New = GetTemplateChild("PART_TransitionerSlide_New") as TransitionerSlide;
        }


        public static readonly DependencyProperty DefaultTransitionOriginProperty = DependencyProperty.Register(
            "DefaultTransitionOrigin", typeof(Point), typeof(SwtichTransitioner), new PropertyMetadata(new Point(0.5, 0.5)));

        public Point DefaultTransitionOrigin
        {
            get => (Point)GetValue(DefaultTransitionOriginProperty);
            set => SetValue(DefaultTransitionOriginProperty, value);
        }

        private Point? GetNavigationSourcePoint(RoutedEventArgs executedRoutedEventArgs)
        {
            FrameworkElement sourceElement = executedRoutedEventArgs.OriginalSource as FrameworkElement;
            if (sourceElement == null || !IsAncestorOf(sourceElement) || !IsSafePositive(ActualWidth) ||
                !IsSafePositive(ActualHeight) || !IsSafePositive(sourceElement.ActualWidth) ||
                !IsSafePositive(sourceElement.ActualHeight)) return null;

            Point transitionOrigin = sourceElement.TranslatePoint(new Point(sourceElement.ActualWidth / 2, sourceElement.ActualHeight), this);
            transitionOrigin = new Point(transitionOrigin.X / ActualWidth, transitionOrigin.Y / ActualHeight);
            return transitionOrigin;
        }

        private static bool IsSafePositive(double dubz)
        {
            return !double.IsNaN(dubz) && !double.IsInfinity(dubz) && dubz > 0.0;
        }



        private Point GetTransitionOrigin(TransitionerSlide slide)
        {
            if (_nextTransitionOrigin != null)
            {
                return _nextTransitionOrigin.Value;
            }

            if (slide.ReadLocalValue(TransitionerSlide.TransitionOriginProperty) != DependencyProperty.UnsetValue)
            {
                return slide.TransitionOrigin;
            }

            return DefaultTransitionOrigin;
        }

        void IZIndexController.Stack(params TransitionerSlide[] highestToLowest)
        {
            DoStack(highestToLowest);
        }

        private static void DoStack(params TransitionerSlide[] highestToLowest)
        {
            if (highestToLowest == null) return;

            int pos = highestToLowest.Length;

            foreach (TransitionerSlide slide in highestToLowest.Where(s => s != null))
            {
                Panel.SetZIndex(slide, pos--);
            }
        }
    }

    public partial class SwtichTransitioner
    {
        public object OldContent
        {
            get { return GetValue(OldContentProperty); }
            set { SetValue(OldContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OldContentProperty =
            DependencyProperty.Register("OldContent", typeof(object), typeof(SwtichTransitioner), new PropertyMetadata(default(object), (d, e) =>
             {
                 SwtichTransitioner control = d as SwtichTransitioner;

                 if (control == null) return;

                 object config = e.NewValue;

             }));


        public object NewContent
        {
            get { return GetValue(NewContentProperty); }
            set { SetValue(NewContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewContentProperty =
            DependencyProperty.Register("NewContent", typeof(object), typeof(SwtichTransitioner), new PropertyMetadata(default(object), (d, e) =>
             {
                 SwtichTransitioner control = d as SwtichTransitioner;

                 if (control == null) return;

                 object config = e.NewValue;

             }));



        public ITransitionWipe ITransitionWipe
        {
            get { return (ITransitionWipe)GetValue(ITransitionWipeProperty); }
            set { SetValue(ITransitionWipeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ITransitionWipeProperty =
            DependencyProperty.Register("ITransitionWipe", typeof(ITransitionWipe), typeof(SwtichTransitioner), new PropertyMetadata(default(ITransitionWipe), (d, e) =>
             {
                 SwtichTransitioner control = d as SwtichTransitioner;

                 if (control == null) return;

                 ITransitionWipe config = e.NewValue as ITransitionWipe;

             }));



        public object CurrentContent
        {
            get { return GetValue(CurrentContentProperty); }
            set { SetValue(CurrentContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentContentProperty =
            DependencyProperty.Register("CurrentContent", typeof(object), typeof(SwtichTransitioner), new PropertyMetadata(default(object), (d, e) =>
             {
                 SwtichTransitioner control = d as SwtichTransitioner;

                 if (control == null) return;

                 object config = e.NewValue;

                 control.RefreshControls(e.OldValue, e.NewValue);

             }));

        private void RefreshControls(object oldControl, object newControl)
        {
            this.OldContent = oldControl ?? this.OldContent;
            this.NewContent = newControl;

            FrameworkElement control_old = NewContent as FrameworkElement;
            FrameworkElement control_new = NewContent as FrameworkElement;

            if (control_new == null || control_new.IsLoaded)
            {
                this.RefreshSwitch();
                return;
            }

            //  Do ：第一次加载完控件在刷新
            if (control_old != null)
            {
                control_old.Loaded -= Control_Loaded;
            }

            control_new.Loaded += Control_Loaded;


        }

        private void Control_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshSwitch();
        }

        private void RefreshSwitch()
        {
            if (_transitionerSlide_Old == null || _transitionerSlide_New == null) return;

            TransitionerSlide oldSlide = null, newSlide = null;

            List<TransitionerSlide> Items = new List<TransitionerSlide>();

            Items.Add(_transitionerSlide_Old);
            Items.Add(_transitionerSlide_New);

            int selectedIndex = 1;

            int unselectedIndex = 0;

            for (int index = 0; index < Items.Count; index++)
            {
                TransitionerSlide slide = Items[index];

                if (index == selectedIndex)
                {
                    newSlide = slide;
                    slide.SetCurrentValue(TransitionerSlide.StateProperty, TransitionerSlideState.Current);
                }
                else if (index == unselectedIndex)
                {
                    oldSlide = slide;
                    slide.SetCurrentValue(TransitionerSlide.StateProperty, TransitionerSlideState.Previous);
                }
                else
                {
                    slide.SetCurrentValue(TransitionerSlide.StateProperty, TransitionerSlideState.None);
                }
                Panel.SetZIndex(slide, 0);
            }

            if (newSlide != null)
            {
                newSlide.Opacity = 1;
            }

            if (oldSlide != null && newSlide != null)
            {
                //var wipe = selectedIndex > unselectedIndex ? oldSlide.ForwardWipe : oldSlide.BackwardWipe;

                ITransitionWipe wipe = ITransitionWipe;

                if (wipe != null)
                {
                    wipe.Wipe(oldSlide, newSlide, GetTransitionOrigin(newSlide), this);
                }
                else
                {
                    DoStack(newSlide, oldSlide);
                }

                oldSlide.Opacity = 0;
            }
            else if (oldSlide != null || newSlide != null)
            {
                DoStack(oldSlide ?? newSlide);

                if (oldSlide != null)
                {
                    oldSlide.Opacity = 0;
                }
            }
        }
    }

}
