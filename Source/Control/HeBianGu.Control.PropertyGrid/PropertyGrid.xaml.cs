// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.PropertyGrid
{

    /// <summary> 自定义导航框架 </summary> 
    public partial class PropertyGrid : ItemsControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(PropertyGrid), "S.PropertyGrid.Default");
        public static ComponentResourceKey SingleKey => new ComponentResourceKey(typeof(PropertyGrid), "S.PropertyGrid.Single");
        public static ComponentResourceKey SumitKey => new ComponentResourceKey(typeof(PropertyGrid), "S.PropertyGrid.Default.WithSumit");
        public static ComponentResourceKey SumitCloseKey => new ComponentResourceKey(typeof(PropertyGrid), "S.PropertyGrid.Default.WithSumitClose");
        public static ComponentResourceKey SettingKey => new ComponentResourceKey(typeof(PropertyGrid), "S.PropertyGrid.SettingDefault");


        static PropertyGrid()
        {

            StyleLoader.Instance?.LoadDefault(typeof(PropertyGrid));

            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGrid), new FrameworkPropertyMetadata(typeof(PropertyGrid)));
        }

        public PropertyGrid()
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

            //this.Filter = l =>
            //  {
            //      //return l.GetCustomAttribute<DisplayAttribute>() != null; 
            //  };


            this.Mapper = (l, k) => l.Create(k);
        }

        public bool Result { get; set; }


        //声明和注册路由事件
        public static readonly RoutedEvent SumitRoutedEvent =
            EventManager.RegisterRoutedEvent("Sumit", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(PropertyGrid));
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
            EventManager.RegisterRoutedEvent("Close", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(PropertyGrid));
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
            DependencyProperty.Register("Title", typeof(string), typeof(PropertyGrid), new PropertyMetadata(default(string), (d, e) =>
             {
                 PropertyGrid control = d as PropertyGrid;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public object SelectObject
        {
            get { return GetValue(SelectObjectProperty); }
            set { SetValue(SelectObjectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectObjectProperty =
            DependencyProperty.Register("SelectObject", typeof(object), typeof(PropertyGrid), new PropertyMetadata(default(object), (d, e) =>
             {
                 PropertyGrid control = d as PropertyGrid;

                 if (control == null) return;

                 object config = e.NewValue;

                 control.RefreshObject(config);

             }));

        public object BottomContent
        {
            get { return GetValue(BottomContentProperty); }
            set { SetValue(BottomContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomContentProperty =
            DependencyProperty.Register("BottomContent", typeof(object), typeof(PropertyGrid), new PropertyMetadata(default(object), (d, e) =>
             {
                 PropertyGrid control = d as PropertyGrid;

                 if (control == null) return;

                 object config = e.NewValue;

             }));

        public Predicate<PropertyInfo> Filter
        {
            get { return (Predicate<PropertyInfo>)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(Predicate<PropertyInfo>), typeof(PropertyGrid), new PropertyMetadata(default(Predicate<PropertyInfo>), (d, e) =>
             {
                 PropertyGrid control = d as PropertyGrid;

                 if (control == null) return;

                 Predicate<PropertyInfo> config = e.NewValue as Predicate<PropertyInfo>;

             }));

        public bool DeclaredOnly
        {
            get { return (bool)GetValue(DeclaredOnlyProperty); }
            set { SetValue(DeclaredOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeclaredOnlyProperty =
            DependencyProperty.Register("DeclaredOnly", typeof(bool), typeof(PropertyGrid), new PropertyMetadata(default(bool), (d, e) =>
             {
                 PropertyGrid control = d as PropertyGrid;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));



        public Func<PropertyInfo, object, IPropertyItem> Mapper
        {
            get { return (Func<PropertyInfo, object, IPropertyItem>)GetValue(MapperProperty); }
            set { SetValue(MapperProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapperProperty =
            DependencyProperty.Register("Mapper", typeof(Func<PropertyInfo, object, IPropertyItem>), typeof(PropertyGrid), new FrameworkPropertyMetadata(default(Func<PropertyInfo, object, IPropertyItem>), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
                 {
                     PropertyGrid control = d as PropertyGrid;

                     if (control == null) return;

                     if (e.OldValue is Func<PropertyInfo, object, IPropertyItem> o)
                     {

                     }

                     if (e.NewValue is Func<PropertyInfo, object, IPropertyItem> n)
                     {

                     }
                 }));

        protected virtual void RefreshObject(object o)
        {
            if (o == null) return;

            Type type = o.GetType();

            List<PropertyInfo> propertys = new List<PropertyInfo>();

            Action<Type> action = null;

            action = l =>
              {
                  if (l.BaseType != typeof(object))
                  {
                      action(l.BaseType);
                  }

                  PropertyInfo[] ps = l.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                  if (ps != null)
                      propertys.AddRange(ps);


              };

            if (this.DeclaredOnly)
            {
                PropertyInfo[] ps = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if (ps != null)
                    propertys.AddRange(ps);
            }
            else
            {
                action.Invoke(type);
            }


            //var propertys = type.GetProperties().OrderBy(l => l.DeclaringType).ToList();

            List<IPropertyItem> items = new List<IPropertyItem>();


            foreach (PropertyInfo item in propertys)
            {
                //  Do ：筛选条件
                if (this.Filter != null && !this.Filter.Invoke(item))
                {
                    continue;
                }
                BrowsableAttribute browsable = item.GetCustomAttribute<BrowsableAttribute>();

                if (browsable != null && browsable.Browsable == false) continue;

                IPropertyItem from = this.Mapper?.Invoke(item, o);

                if (from is ObjectPropertyItem obj)
                {
                    obj.ValueChanged = l =>
                    {
                        this.OnValueChanged(Tuple.Create(from, l));
                    };
                }



                items.Add(from);
            }

            this.ItemsSource = items;
        }

        public bool ModelState(out List<string> errors)
        {
            return this.SelectObject.ModelState(out errors);
        }


        //声明和注册路由事件
        public static readonly RoutedEvent ValueChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(PropertyGrid));
        //CLR事件包装
        public event RoutedEventHandler ValueChanged
        {
            add { this.AddHandler(ValueChangedRoutedEvent, value); }
            remove { this.RemoveHandler(ValueChangedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnValueChanged(Tuple<IPropertyItem, object> tuple)
        {
            RoutedEventArgs args = new RoutedEventArgs(ValueChangedRoutedEvent, this);

            args.Source = tuple;

            //  Do ：触发通知方法
            CustomValidationAttribute attribute = tuple.Item1.PropertyInfo.GetCustomAttribute<CustomValidationAttribute>();

            if (attribute != null)
            {
                MethodInfo method = tuple.Item1.Obj.GetType().GetMethod(attribute.Method);

                method?.Invoke(tuple.Item1.Obj, null);
            }


            {
                //  Do ：触发绑定属性值刷新
                List<IPropertyItem> binds = this.ItemsSource.Cast<IPropertyItem>().Where(l => l.PropertyInfo.GetCustomAttribute<CompareAttribute>()?.OtherProperty == tuple.Item1.PropertyInfo.Name)?.ToList();

                foreach (IPropertyItem bind in binds)
                {
                    if (bind is ObjectPropertyItem objectProperty)
                    {
                        objectProperty.LoadValue();
                    }
                }
            }

            {
                Func<IPropertyItem, bool> predicate = l =>
                      {
                          BindingAttribute binding = l.PropertyInfo.GetCustomAttribute<BindingAttribute>();

                          if (binding == null) return false;

                          string firstPath = binding.GetPaths()?.FirstOrDefault();

                          return firstPath == tuple.Item1.PropertyInfo.Name;
                      };

                IEnumerable<IPropertyItem> binds = this.ItemsSource.Cast<IPropertyItem>().Where(predicate);

                foreach (IPropertyItem bind in binds)
                {
                    BindingAttribute binding = bind.PropertyInfo.GetCustomAttribute<BindingAttribute>();

                    string[] paths = binding.GetPaths();

                    if (bind is ObjectPropertyItem objectProperty)
                    {
                        object v = binding.GetValue(tuple.Item2);

                        bind.PropertyInfo.SetValue(bind.Obj, v);

                        objectProperty.LoadValue();
                    }

                }
            }

            this.RaiseEvent(args);
        }

        public double MessageWidth
        {
            get { return (double)GetValue(MessageWidthProperty); }
            set { SetValue(MessageWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageWidthProperty =
            DependencyProperty.Register("MessageWidth", typeof(double), typeof(PropertyGrid), new PropertyMetadata(default(double), (d, e) =>
             {
                 PropertyGrid control = d as PropertyGrid;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));


    }

    public partial class PropertyGrid
    {
        private static ManualResetEvent _asyncShowWaitHandle = new ManualResetEvent(false);

        /// <summary> 显示蒙版 </summary>
        public static async Task<bool> ShowObject<T>(T value, Predicate<T> match = null, string title = null, Action<PropertyGrid> builder = null, ComponentResourceKey key = null)
        {
            bool result = false;

            await Application.Current.Dispatcher.Invoke(async () =>
            {
                if (Application.Current.MainWindow is IMainWindow window)
                {
                    PropertyGrid form = new PropertyGrid();

                    form.Title = title;

                    form.Style = Application.Current.FindResource(key ?? PropertyGrid.SumitKey) as Style;

                    form.SelectObject = value;

                    builder?.Invoke(form);

                    form.Close += (l, k) =>
                    {
                        Message.Instance.CloseLayer();
                        _asyncShowWaitHandle.Set();
                        result = false;
                    };

                    form.Sumit += (l, k) =>
                    {
                        bool check = form.ModelState(out List<string> errors);

                        if (!check)
                        {
                            Message.Instance.ShowSnackMessageWithNotice(errors.FirstOrDefault());
                            return;
                        }

                        if (match != null && !match(value))
                        {
                            return;
                        }

                        Message.Instance.CloseLayer();
                        _asyncShowWaitHandle.Set();
                        result = true;

                    };

                    window.ShowLayer(form);

                    _asyncShowWaitHandle.Reset();

                    Task task = new Task(() =>
                    {
                        _asyncShowWaitHandle.WaitOne();
                    });

                    task.Start();

                    await task;
                }
            });

            return result;

        }

        public static async Task<bool> ShowObject<T>(T value, Action<PropertyGrid> builder)
        {
            return await ShowObject(value, null, null, builder);
        }

        public static async Task<bool> ShowObject<T>(T value, string title, Action<PropertyGrid> builder)
        {
            return await ShowObject(value, null, title, builder);
        }
    }


}
