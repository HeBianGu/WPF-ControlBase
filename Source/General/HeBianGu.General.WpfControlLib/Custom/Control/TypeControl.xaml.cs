// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
    public class TypeControl : ContentControl
    {

        public Type Type
        {
            get { return (Type)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(Type), typeof(TypeControl), new FrameworkPropertyMetadata(default(Type), (d, e) =>
             {
                 TypeControl control = d as TypeControl;

                 if (control == null) return;

                 if (e.OldValue is Type o)
                 {

                 }

                 if (e.NewValue is Type n)
                 {
                     control.RefreshData();
                 }

             }));


        public Style TypeStyle
        {
            get { return (Style)GetValue(TypeStyleProperty); }
            set { SetValue(TypeStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeStyleProperty =
            DependencyProperty.Register("TypeStyle", typeof(Style), typeof(TypeControl), new FrameworkPropertyMetadata(default(Style), (d, e) =>
             {
                 TypeControl control = d as TypeControl;

                 if (control == null) return;

                 if (e.OldValue is Style o)
                 {

                 }

                 if (e.NewValue is Style n)
                 {
                     control.RefreshData();
                 }
             }));

        private void RefreshData()
        {
            this.Content = Activator.CreateInstance(this.Type);

            if (this.Content is FrameworkElement framework)
            {
                framework.Style = this.TypeStyle;
            }
        }

    }

}
