// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace HeBianGu.Control.Filter
{
    public class SearchBox : TextBox
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(SearchBox), "S.SearchBox.Default");
        public static ComponentResourceKey LabelKey => new ComponentResourceKey(typeof(SearchBox), "S.SearchBox.Label");

        static SearchBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchBox), new FrameworkPropertyMetadata(typeof(SearchBox)));
        }

        public SearchBox()
        {
            this.Match = (p, o) =>
              {
                  if (string.IsNullOrEmpty(this.Text)) return true;

                 if (p.GetMethod.Name == "get_Item") return false;

                  object v = p.GetValue(o);

                  if (v == null) return false;

                  if (this.IsAaZz)
                  {
                      return v.ToString().ToUpper().Contains(this.Text.ToUpper());
                  }


                  return v.ToString().Contains(this.Text);
              };
        }


        public IEnumerable InSource
        {
            get { return (IEnumerable)GetValue(InSourceProperty); }
            set { SetValue(InSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InSourceProperty =
            DependencyProperty.Register("InSource", typeof(IEnumerable), typeof(SearchBox), new PropertyMetadata(default(IEnumerable), (d, e) =>
             {
                 SearchBox control = d as SearchBox;

                 if (control == null) return;

                 IEnumerable config = e.NewValue as IEnumerable;

                 control.RefreshData();

             }));


        public IEnumerable OutSource
        {
            get { return (IEnumerable)GetValue(OutSourceProperty); }
            set { SetValue(OutSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutSourceProperty =
            DependencyProperty.Register("OutSource", typeof(IEnumerable), typeof(SearchBox), new PropertyMetadata(default(IEnumerable), (d, e) =>
             {
                 SearchBox control = d as SearchBox;

                 if (control == null) return;

                 IEnumerable config = e.NewValue as IEnumerable;


             }));

        public Func<PropertyInfo, object, bool> Match
        {
            get { return (Func<PropertyInfo, object, bool>)GetValue(MatchProperty); }
            set { SetValue(MatchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MatchProperty =
            DependencyProperty.Register("Match", typeof(Func<PropertyInfo, object, bool>), typeof(SearchBox), new PropertyMetadata(default(Func<PropertyInfo, object, bool>), (d, e) =>
                 {
                     SearchBox control = d as SearchBox;

                     if (control == null) return;

                     Func<PropertyInfo, object, bool> config = e.NewValue as Func<PropertyInfo, object, bool>;

                 }));

        /// <summary> 区分大小写 </summary>

        public bool IsAaZz
        {
            get { return (bool)GetValue(IsAaZzProperty); }
            set { SetValue(IsAaZzProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAaZzProperty =
            DependencyProperty.Register("IsAaZz", typeof(bool), typeof(SearchBox), new PropertyMetadata(default(bool), (d, e) =>
             {
                 SearchBox control = d as SearchBox;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

                 control.RefreshData();

             }));


        /// <summary> 是否异步通知输出数据 </summary>
        public bool IsAsync
        {
            get { return (bool)GetValue(IsAsyncProperty); }
            set { SetValue(IsAsyncProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAsyncProperty =
            DependencyProperty.Register("IsAsync", typeof(bool), typeof(SearchBox), new PropertyMetadata(true, (d, e) =>
             {
                 SearchBox control = d as SearchBox;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));




        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            this.RefreshData();
        }

        private void RefreshData()
        {
            if (this.InSource == null) return;

            IList list = new ObservableCollection<object>();

            if (this.IsAsync)
            {
                this.OutSource = list;

                foreach (object item in this.InSource)
                {
                    if (!this.IsMacth(item)) continue;

                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                    {
                        list.Add(item);
                    }));
                }
            }
            else
            {
                foreach (object item in this.InSource)
                {
                    if (!this.IsMacth(item)) continue;

                    list.Add(item);
                }

                this.OutSource = list;
            }

        }

        private bool IsMacth(object obj)
        {
            PropertyInfo[] ps = obj.GetType().GetProperties();

            foreach (PropertyInfo p in ps)
            {
                if (!p.CanRead) continue;

                if (this.Match?.Invoke(p, obj) == true) return true;
            }

            return false;
        }
    }

}
