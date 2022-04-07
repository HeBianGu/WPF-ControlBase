// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HeBianGu.Control.ImagePlayer
{
    [TemplatePart(Name = "PART_IMAGECORE", Type = typeof(ImageCore))]
    public partial class ImagePlayer : ContentControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ImagePlayer), "S.ImagePlayer.Default");

        static ImagePlayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImagePlayer), new FrameworkPropertyMetadata(typeof(ImagePlayer)));
        }

        public static RoutedUICommand PreviousCommand = new RoutedUICommand();
        public static RoutedUICommand NextCommand = new RoutedUICommand();
        private ImageCore _core = null;

        public ImagePlayer()
        {
            {
                //  Do ：上一张
                CommandBinding command = new CommandBinding(ImagePlayer.PreviousCommand, (l, k) =>
                {
                    string commond = k.Parameter?.ToString();


                    if (this.Current.Previous != null)
                    {
                        this.Current = this.Current.Previous;

                        this.Index = this.Index - 1;
                    }
                    else
                    {
                        this.Current = this._cache.Last;

                        this.Index = this._cache.Count;

                        this.OnCoreMessaged("已是最后一项，自动跳转至第一项");

                    }
                }, (l, k) => { k.CanExecute = this.Current != null; });

                this.CommandBindings.Add(command);
            }
            {
                //  Do ：下一张
                CommandBinding command = new CommandBinding(ImagePlayer.NextCommand, (l, k) =>
                {
                    string commond = k.Parameter?.ToString();

                    if (this.Current.Next != null)
                    {
                        this.Current = this.Current.Next;

                        this.Index = this.Index + 1;
                    }
                    else
                    {

                        this.Current = this._cache.First;
                        this.Index = 1;
                        this.OnCoreMessaged("已是最后一项，自动跳转至第一项");
                    }
                }, (l, k) => { k.CanExecute = this.Current != null; });

                this.CommandBindings.Add(command);
            }
        }

        #region - 初始化 -

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _core = Template.FindName("PART_IMAGECORE", this) as ImageCore;

            if (_core != null)
            {
                this._core.NoticeMessaged += (l, k) =>
              {
                  this.OnCoreMessaged(this._core.Message);
              };
            }

            //  Do ：防止模板未加载，先触发Current依赖属性改变
            _core.Loaded += (l, k) =>
              {
                  if (this.Current == null || this.Current.Value == null) return;

                  _core.ImageSource = this.ConvertToImageSource.Invoke(this.Current.Value);
              };

        }

        #endregion


        public Func<object, ImageSource> ConvertToImageSource
        {
            get { return (Func<object, ImageSource>)GetValue(ConvertToImageSourceProperty); }
            set { SetValue(ConvertToImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConvertToImageSourceProperty =
            DependencyProperty.Register("ConvertToImageSource", typeof(Func<object, ImageSource>), typeof(ImagePlayer), new PropertyMetadata(new Func<object, ImageSource>(l => l as ImageSource), (d, e) =>
             {
                 ImagePlayer control = d as ImagePlayer;

                 if (control == null) return;

                 Func<object, ImageSource> config = e.NewValue as Func<object, ImageSource>;

             }));



        public int Index
        {
            get { return (int)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IndexProperty =
            DependencyProperty.Register("Index", typeof(int), typeof(ImagePlayer), new PropertyMetadata(default(int), (d, e) =>
             {
                 ImagePlayer control = d as ImagePlayer;

                 if (control == null) return;

                 //int config = e.NewValue as int;

             }));


        public LinkedListNode<object> Current
        {
            get { return (LinkedListNode<object>)GetValue(CurrentProperty); }
            set { SetValue(CurrentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentProperty =
            DependencyProperty.Register("Current", typeof(LinkedListNode<object>), typeof(ImagePlayer), new PropertyMetadata(default(LinkedListNode<ImageSource>), (d, e) =>
             {
                 ImagePlayer control = d as ImagePlayer;

                 if (control == null) return;

                 LinkedListNode<object> config = e.NewValue as LinkedListNode<object>;

                 if (config == null) return;

                 control.SelectedItem = config.Value;

                 if (control._core == null) return;

                 control._core.ImageSource = control.ConvertToImageSource.Invoke(config.Value);


             }));



        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(ImagePlayer), new PropertyMetadata(default(object), (d, e) =>
             {
                 ImagePlayer control = d as ImagePlayer;

                 if (control == null) return;

                 object config = e.NewValue;

             }));


        public IEnumerable DataSource
        {
            get { return (IEnumerable)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(IEnumerable), typeof(ImagePlayer), new PropertyMetadata(default(IEnumerable), (d, e) =>
            {
                ImagePlayer control = d as ImagePlayer;

                if (control == null) return;

                if (e.OldValue is INotifyCollectionChanged notify)
                {
                    if (notify != null)
                    {
                        notify.CollectionChanged -= control.CollectionChanged;
                    }
                }

                if (e.NewValue is INotifyCollectionChanged notify1)
                {
                    if (notify1 != null)
                    {
                        notify1.CollectionChanged += control.CollectionChanged;
                    }
                }

                control.RefreshData(e.NewValue as IEnumerable);
            }));

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            IEnumerable config = sender as IEnumerable;

            this.RefreshData(config);

        }

        private void RefreshData(IEnumerable source)
        {
            this._cache.Clear();

            if (source == null) return;

            foreach (object item in source)
            {
                _cache.AddLast(item);

            }
            this.Current = this.Current = _cache.First;

            this.Index = 1;
        }

        private LinkedList<object> _cache = new LinkedList<object>();

        //声明和注册路由事件
        public static readonly RoutedEvent NotifyMessagedRoutedEvent =
            EventManager.RegisterRoutedEvent("NotifyMessaged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ImagePlayer));
        //CLR事件包装
        public event RoutedEventHandler NotifyMessaged
        {
            add { this.AddHandler(NotifyMessagedRoutedEvent, value); }
            remove { this.RemoveHandler(NotifyMessagedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnNotifyMessaged(string message)
        {
            this.Message = message;

            RoutedEventArgs args = new RoutedEventArgs(NotifyMessagedRoutedEvent, this);
            this.RaiseEvent(args);
        }

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(ImagePlayer), new PropertyMetadata(default(string), (d, e) =>
            {
                ImagePlayer control = d as ImagePlayer;

                if (control == null) return;

                string config = e.NewValue as string;

            }));


        public string CoreMessage
        {
            get { return (string)GetValue(CoreMessageProperty); }
            set { SetValue(CoreMessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CoreMessageProperty =
            DependencyProperty.Register("CoreMessage", typeof(string), typeof(ImagePlayer), new PropertyMetadata(default(string), (d, e) =>
             {
                 ImagePlayer control = d as ImagePlayer;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        //声明和注册路由事件
        public static readonly RoutedEvent CoreMessagedRoutedEvent =
            EventManager.RegisterRoutedEvent("CoreMessaged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ImagePlayer));
        //CLR事件包装
        public event RoutedEventHandler CoreMessaged
        {
            add { this.AddHandler(CoreMessagedRoutedEvent, value); }
            remove { this.RemoveHandler(CoreMessagedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnCoreMessaged(string message)
        {
            this.CoreMessage = message;

            RoutedEventArgs args = new RoutedEventArgs(CoreMessagedRoutedEvent, this);
            this.RaiseEvent(args);
        }


        public OperateType OperateType
        {
            get { return (OperateType)GetValue(OperateTypeProperty); }
            set { SetValue(OperateTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OperateTypeProperty =
            DependencyProperty.Register("OperateType", typeof(OperateType), typeof(ImagePlayer), new PropertyMetadata(default(OperateType), (d, e) =>
             {
                 ImagePlayer control = d as ImagePlayer;

                 if (control == null) return;

                 //OperateType config = e.NewValue as OperateType;

             }));


        public object ToolContent
        {
            get { return GetValue(ToolContentProperty); }
            set { SetValue(ToolContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToolContentProperty =
            DependencyProperty.Register("ToolContent", typeof(object), typeof(ImagePlayer), new PropertyMetadata(default(object), (d, e) =>
             {
                 ImagePlayer control = d as ImagePlayer;

                 if (control == null) return;

                 object config = e.NewValue;

             }));


    }
}
