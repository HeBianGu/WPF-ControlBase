// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.MessageListBox
{
    public class MessageListBox : ContentControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(MessageListBox), "S.MessageListBox.Default");

        static MessageListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageListBox), new FrameworkPropertyMetadata(typeof(MessageListBox)));
        }

        public ObservableCollection<InfoMessage> Infos
        {
            get { return (ObservableCollection<InfoMessage>)GetValue(InfosProperty); }
            set { SetValue(InfosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InfosProperty =
            DependencyProperty.Register("Infos", typeof(ObservableCollection<InfoMessage>), typeof(MessageListBox), new PropertyMetadata(default(ObservableCollection<InfoMessage>), (d, e) =>
             {
                 MessageListBox control = d as MessageListBox;

                 if (control == null) return;

                 ObservableCollection<InfoMessage> config = e.NewValue as ObservableCollection<InfoMessage>;

             }));


        public ObservableCollection<ErrorMessage> Errors
        {
            get { return (ObservableCollection<ErrorMessage>)GetValue(ErrorsProperty); }
            set { SetValue(ErrorsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorsProperty =
            DependencyProperty.Register("Errors", typeof(ObservableCollection<ErrorMessage>), typeof(MessageListBox), new PropertyMetadata(default(ObservableCollection<ErrorMessage>), (d, e) =>
           {
               MessageListBox control = d as MessageListBox;

               if (control == null) return;

               ObservableCollection<ErrorMessage> config = e.NewValue as ObservableCollection<ErrorMessage>;

           }));


        public ObservableCollection<TraceMessage> Traces
        {
            get { return (ObservableCollection<TraceMessage>)GetValue(TracesProperty); }
            set { SetValue(TracesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TracesProperty =
            DependencyProperty.Register("Traces", typeof(ObservableCollection<TraceMessage>), typeof(MessageListBox), new FrameworkPropertyMetadata(new ObservableCollection<TraceMessage>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 MessageListBox control = d as MessageListBox;

                 if (control == null) return;

                 if (e.OldValue is ObservableCollection<TraceMessage> o)
                 {

                 }

                 if (e.NewValue is ObservableCollection<TraceMessage> n)
                 {

                 }

             }));


        public ObservableCollection<WarnMessage> Warns
        {
            get { return (ObservableCollection<WarnMessage>)GetValue(WarnsProperty); }
            set { SetValue(WarnsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WarnsProperty =
            DependencyProperty.Register("Warns", typeof(ObservableCollection<WarnMessage>), typeof(MessageListBox), new FrameworkPropertyMetadata(new ObservableCollection<WarnMessage>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 MessageListBox control = d as MessageListBox;

                 if (control == null) return;

                 if (e.OldValue is ObservableCollection<WarnMessage> o)
                 {

                 }

                 if (e.NewValue is ObservableCollection<WarnMessage> n)
                 {

                 }

             }));


        public ObservableCollection<FatalMessage> Fatals
        {
            get { return (ObservableCollection<FatalMessage>)GetValue(FatalsProperty); }
            set { SetValue(FatalsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FatalsProperty =
            DependencyProperty.Register("Fatals", typeof(ObservableCollection<FatalMessage>), typeof(MessageListBox), new FrameworkPropertyMetadata(new ObservableCollection<FatalMessage>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 MessageListBox control = d as MessageListBox;

                 if (control == null) return;

                 if (e.OldValue is ObservableCollection<FatalMessage> o)
                 {

                 }

                 if (e.NewValue is ObservableCollection<FatalMessage> n)
                 {

                 }

             }));



    }
}
