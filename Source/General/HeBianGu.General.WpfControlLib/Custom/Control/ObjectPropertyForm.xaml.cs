using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{

    /// <summary> 自定义导航框架 </summary> 
    public class ObjectPropertyForm : ItemsControl
    {
        static ObjectPropertyForm()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ObjectPropertyForm), new FrameworkPropertyMetadata(typeof(ObjectPropertyForm)));
        }

        public ObjectPropertyForm()
        {
            this.BindCommand(CommandService.Sure, (l, k) =>
             {
                 this.Result = true;

                 this.OnSumit();
             });

            this.BindCommand(CommandService.Close, (l, k) =>
            {
                this.OnClose();
            });
        }

        public bool Result { get; set; }


        //声明和注册路由事件
        public static readonly RoutedEvent SumitRoutedEvent =
            EventManager.RegisterRoutedEvent("Sumit", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ObjectPropertyForm));
        //CLR事件包装
        public event RoutedEventHandler Sumit
        {
            add { this.AddHandler(SumitRoutedEvent, value); }
            remove { this.RemoveHandler(SumitRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnSumit()
        {
            RoutedEventArgs args = new RoutedEventArgs(SumitRoutedEvent, this);
            this.RaiseEvent(args);
        }


        //声明和注册路由事件
        public static readonly RoutedEvent CloseRoutedEvent =
            EventManager.RegisterRoutedEvent("Close", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ObjectPropertyForm));
        //CLR事件包装
        public event RoutedEventHandler Close
        {
            add { this.AddHandler(CloseRoutedEvent, value); }
            remove { this.RemoveHandler(CloseRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnClose()
        {
            RoutedEventArgs args = new RoutedEventArgs(CloseRoutedEvent, this);
            this.RaiseEvent(args);
        }



        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ObjectPropertyForm), new PropertyMetadata(default(string), (d, e) =>
             {
                 ObjectPropertyForm control = d as ObjectPropertyForm;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public object SelectObject
        {
            get { return (object)GetValue(SelectObjectProperty); }
            set { SetValue(SelectObjectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectObjectProperty =
            DependencyProperty.Register("SelectObject", typeof(object), typeof(ObjectPropertyForm), new PropertyMetadata(default(object), (d, e) =>
             {
                 ObjectPropertyForm control = d as ObjectPropertyForm;

                 if (control == null) return;

                 object config = e.NewValue as object;

                 control.RefreshObject(config);

             }));




        public object BottomContent
        {
            get { return (object)GetValue(BottomContentProperty); }
            set { SetValue(BottomContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomContentProperty =
            DependencyProperty.Register("BottomContent", typeof(object), typeof(ObjectPropertyForm), new PropertyMetadata(default(object), (d, e) =>
             {
                 ObjectPropertyForm control = d as ObjectPropertyForm;

                 if (control == null) return;

                 object config = e.NewValue as object;

             }));


        ObservableCollection<ObjectPropertyItem> PropertyItemSource
        {
            get { return (ObservableCollection<ObjectPropertyItem>)GetValue(PropertyItemSourceProperty); }
            set { SetValue(PropertyItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropertyItemSourceProperty =
            DependencyProperty.Register("PropertyItemSource", typeof(ObservableCollection<ObjectPropertyItem>), typeof(ObjectPropertyForm), new PropertyMetadata(new ObservableCollection<ObjectPropertyItem>(), (d, e) =>
             {
                 ObjectPropertyForm control = d as ObjectPropertyForm;

                 if (control == null) return;

                 ObservableCollection<ObjectPropertyItem> config = e.NewValue as ObservableCollection<ObjectPropertyItem>;

             }));


        void RefreshObject(object o)
        {
            if (o == null) return;

            Type type = o.GetType();

            var propertys = type.GetProperties();

            this.PropertyItemSource.Clear();

            foreach (var item in propertys)
            {
                var from = ObjectPropertyFactory.Create(item, o);

                this.PropertyItemSource.Add(from);
            }
            //this.Items.Refresh();

            //this.ItemsSource = this.PropertyItemSource;

            this.Items.Clear();

            foreach (var item in this.PropertyItemSource)
            {
                this.Items.Add(item);
            }
        }

    }



}
