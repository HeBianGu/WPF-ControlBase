// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace HeBianGu.Systems.Start
{
    public partial class StartWindowBase : WindowBase, IStartWindow
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(StartWindowBase), "S.Window.Start.Default");
        public static ComponentResourceKey OpacityMaskKey => new ComponentResourceKey(typeof(StartWindowBase), "S.Window.Start.OpacityMask");

        static StartWindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StartWindowBase), new FrameworkPropertyMetadata(typeof(StartWindowBase)));
        }

        public StartWindowBase()
        {
            this.ShowAnimation = l =>
            {
                this.Show(true);

            };

            this.CloseAnimation = l =>
            {
                this.Show(false);

            };

            this.BindCommand(Commander.Close, (l, k) =>
            {
                this.CloseAnimation?.Invoke(this);
            });

            this.MouseDown += DialogWindow_MouseDown;

            if (string.IsNullOrEmpty(StartInitService.Instance.Copyright))
            {
                StartInitService.Instance.Copyright = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;
            }

            if (string.IsNullOrEmpty(StartInitService.Instance.Title))
            {
                StartInitService.Instance.Title = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyTitleAttribute>()?.Title;
            }

            this.Copyright = StartInitService.Instance.Copyright;
            this.Title = StartInitService.Instance.Title;
            this.Version = StartInitService.Instance.Version;

        }

        private void DialogWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public void SetMessage(string message)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.Message = message;
            });

        }

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(StartWindowBase), new PropertyMetadata(default(ImageSource), (d, e) =>
            {
                StartWindowBase control = d as StartWindowBase;

                if (control == null) return;

                ImageSource config = e.NewValue as ImageSource;

            }));


        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(StartWindowBase), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 StartWindowBase control = d as StartWindowBase;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

             }));


        public string Copyright
        {
            get { return (string)GetValue(CopyrightProperty); }
            set { SetValue(CopyrightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CopyrightProperty =
            DependencyProperty.Register("Copyright", typeof(string), typeof(StartWindowBase), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 StartWindowBase control = d as StartWindowBase;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

             }));


        public string Version
        {
            get { return (string)GetValue(VersionProperty); }
            set { SetValue(VersionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VersionProperty =
            DependencyProperty.Register("Version", typeof(string), typeof(StartWindowBase), new FrameworkPropertyMetadata(default(string), (d, e) =>
             {
                 StartWindowBase control = d as StartWindowBase;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

             }));

    }


}
