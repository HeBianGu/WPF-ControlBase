// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;

namespace HeBianGu.Base.WpfBase
{
    public class InvokeViewMethodBehavior<TObject, TDelegate> : Behavior<TObject>
        where TDelegate : Delegate
        where TObject : DependencyObject
    {

        public TDelegate MethodDelegate
        {
            get { return (TDelegate)GetValue(MethodDelegateProperty); }
            set { SetValue(MethodDelegateProperty, value); }
        }

        public static readonly DependencyProperty MethodDelegateProperty =
            DependencyProperty.Register(nameof(MethodDelegate), typeof(TDelegate), typeof(InvokeViewMethodBehavior<TObject, TDelegate>), new FrameworkPropertyMetadata(default(TDelegate)) { BindsTwoWayByDefault = true });

        private Func<TObject, TDelegate> _getMethodDelegateFunc;

        public Func<TObject, TDelegate> GetMethodDelegateFunc
        {
            get => _getMethodDelegateFunc;
            set
            {
                _getMethodDelegateFunc = value;
                RefreshMethodDelegate();
            }
        }

        protected override void OnAttached()
        {
            RefreshMethodDelegate();
            base.OnAttached();
        }
        protected override void OnDetaching()
        {
            RefreshMethodDelegate();
            base.OnDetaching();
        }

        private void RefreshMethodDelegate()
        {
            if (AssociatedObject == null || GetMethodDelegateFunc == null)
            {
                MethodDelegate = null;
            }

            MethodDelegate = GetMethodDelegateFunc(AssociatedObject);

        }
    }
}
