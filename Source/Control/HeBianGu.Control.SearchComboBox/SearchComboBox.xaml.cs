// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Control.SearchComboBox
{
    public class SearchComboBox : ComboBox
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(SearchComboBox), "S.SearchComboBox.Default");
        public static ComponentResourceKey LogoKey => new ComponentResourceKey(typeof(SearchComboBox), "S.SearchComboBox.WithLogo");

        static SearchComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchComboBox), new FrameworkPropertyMetadata(typeof(SearchComboBox)));
        }

        private bool t = true;

        private ObservableCollection<object> bindingList = new ObservableCollection<object>();

        private string editText = "";

        /// <summary>
        /// 注册依赖事件
        /// </summary>
        public static readonly DependencyProperty ItemsSourcePropertyNew = DependencyProperty.Register("MyItemsSource", typeof(IEnumerable), typeof(SearchComboBox), new FrameworkPropertyMetadata(new PropertyChangedCallback(ValueChanged)));

        /// <summary>
        /// 数据源改变，添加数据源到绑定数据源
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SearchComboBox ecb = d as SearchComboBox;
            ecb.bindingList.Clear();

            foreach (object item in ecb.MyItemsSource)
            {
                ecb.bindingList.Add(item);
            }
        }
        /// <summary>
        /// 设置或获取ComboBox的数据源
        /// </summary>
        public IEnumerable MyItemsSource
        {
            get
            {
                return (IEnumerable)GetValue(ItemsSourcePropertyNew);
            }

            set
            {
                if (value == null)
                    ClearValue(ItemsSourcePropertyNew);
                else
                    SetValue(ItemsSourcePropertyNew, value);
            }
        }
        /// <summary>
        /// 重写初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.IsEditable = true;
            this.IsTextSearchEnabled = false;
            this.ItemsSource = bindingList;

            this.StaysOpenOnEdit = true;
        }
        /// <summary>
        /// 下拉框获取焦点，首次搜索文本编辑框
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(RoutedEventArgs e)
        {

            //if (this.IsDropDownOpen == false)
            //    this.IsDropDownOpen = true;

            if (t) FindTextBox(this);
            else
                t = false;

        }
        /// <summary>
        /// 搜索编辑文本框，添加文本改变事件
        /// </summary>
        /// <param name="obj"></param>
        private void FindTextBox(DependencyObject obj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is TextBox)
                {
                    //注册文本改变事件
                    (child as TextBox).TextChanged += EditComboBox_TextChanged;

                    (child as TextBox).Focus();

                    (child as TextBox).Select((child as TextBox).Text.Length, 0);
                }
                else
                {
                    FindTextBox(child);
                }
            }
        }
        /// <summary>
        /// 文本改变，动态控制下拉条数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb.IsFocused)
            {
                if (editText == this.Text)
                    return;

                editText = this.Text;

                tb.Focus();

                if (this.IsDropDownOpen == false)
                {
                    this.IsDropDownOpen = true;
                }


                Task.Run(() =>
                {
                    SetList(editText);
                });

            }
        }

        private Semaphore _semaphoreWait = new Semaphore(1, 1);
        /// <summary>
        /// 组合框关闭，数据源恢复
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDropDownClosed(EventArgs e)
        {
            base.OnDropDownClosed(e);
            if (MyItemsSource == null)
                return;
            foreach (object item in MyItemsSource)
            {
                if (!bindingList.Contains(item))
                    bindingList.Add(item);
            }
        }



        /// <summary>
        /// 过滤符合条件的数据项，添加到数据源项中
        /// </summary>
        /// <param name="txt"></param>
        private void SetList(string txt)
        {
            string temp1 = "";
            string temp2 = "";
            IEnumerable enumerable = null;
            string displayPath = null;
            string selectPath = null;
            Func<object, string, bool> filter = null;

            Application.Current.Dispatcher.Invoke(() =>
            {
                enumerable = MyItemsSource;
                displayPath = this.DisplayMemberPath;
                selectPath = this.SelectedValuePath;
                filter = this.FilterMatch;
            });


            if (enumerable == null) return;

            foreach (object item in enumerable)
            {

                temp1 = item.GetType().GetProperty(displayPath).GetValue(item, null).ToString();

                if (string.IsNullOrEmpty(selectPath))
                {
                    temp2 = "";
                }
                else
                {
                    temp2 = item.GetType().GetProperty(selectPath).GetValue(item, null).ToString();
                }

                if (filter == null)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (temp1.Contains(txt) || temp2.StartsWith(txt))
                        {
                            if (!this.bindingList.Contains(item))
                                this.bindingList.Add(item);
                        }
                        else if (this.bindingList.Contains(item))
                        {
                            this.bindingList.Remove(item);
                        }
                    });

                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (filter(item, txt))
                        {
                            if (!this.bindingList.Contains(item))
                                this.bindingList.Add(item);
                        }
                        else
                        {
                            this.bindingList.Remove(item);
                        }
                    });

                }

            }

            //Application.Current.Dispatcher.Invoke(() =>
            //{
            //    this.bindingList.Clear();

            //    foreach (var item in temp)
            //    {
            //        this.bindingList.Add(item);
            //    }
            //});

        }

        public Func<object, string, bool> FilterMatch
        {
            get { return (Func<object, string, bool>)GetValue(FilterMatchProperty); }
            set { SetValue(FilterMatchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterMatchProperty =
            DependencyProperty.Register("FilterMatch", typeof(Func<object, string, bool>), typeof(SearchComboBox), new PropertyMetadata());

    }
}
