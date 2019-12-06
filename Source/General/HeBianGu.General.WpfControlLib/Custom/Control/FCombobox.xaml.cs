using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    public class FComboBox : ComboBox
    {

        static FComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FComboBox), new FrameworkPropertyMetadata(typeof(FComboBox)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (SearchMode == SearchModes.TextChanged)
            {
                AddHandler(FTextBox.TextChangedEvent, new RoutedEventHandler(OnSearchTextChanged));
                SearchBoxVisibility = Visibility.Visible;
            }
            else if (SearchMode == SearchModes.Enter)
            {
                AddHandler(FTextBox.PreviewKeyDownEvent, new RoutedEventHandler(OnSearchKeyDown));
                SearchBoxVisibility = Visibility.Visible;
            }
            else
            {
                SearchBoxVisibility = Visibility.Collapsed;
            }
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            if (SelectedValuePath == SelectedValuePaths.Header)
                SelectedValue = SelectedItem == null ? "" : (SelectedItem as FComboBoxItem).Content;
            else
                SelectedValue = SelectedItem == null ? null : (SelectedItem as FComboBoxItem).Value;
            base.OnSelectionChanged(e);
        }

        private void OnSearchKeyDown(object sender, RoutedEventArgs e)
        {
            var eve = e as System.Windows.Input.KeyEventArgs;
            if (eve.Key != System.Windows.Input.Key.Enter)
                return;
            var tbSearch = e.OriginalSource as FTextBox;
            if (tbSearch == null || tbSearch.Tag == null || tbSearch.Tag.ToString() != "Search")
                return;
            var text = tbSearch.Text;
            foreach (var item in Items)
            {
                var comboItem = item as ComboBoxItem;
                if (comboItem.Content.ToString().Contains(text))
                    comboItem.Visibility = Visibility.Visible;
                else
                    comboItem.Visibility = Visibility.Collapsed;
            }
            e.Handled = true;
        }

        private void OnSearchTextChanged(object sender, RoutedEventArgs e)
        {
            var tbSearch = e.OriginalSource as FTextBox;
            if (tbSearch == null || tbSearch.Tag == null || tbSearch.Tag.ToString() != "Search")
                return;

            var text = tbSearch.Text;
            foreach (var item in Items)
            {
                var comboItem = item as ComboBoxItem;
                if (comboItem.Content.ToString().Contains(text))
                    comboItem.Visibility = Visibility.Visible;
                else
                    comboItem.Visibility = Visibility.Collapsed;
            }
        }

        #region RoutedEvent
        /// <summary>
        /// 用户点击删除按钮事件。
        /// </summary>
        public static readonly RoutedEvent DeleteItemEvent = EventManager.RegisterRoutedEvent("DeleteItem", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<FComboBoxItem>), typeof(FComboBox));
        public event RoutedPropertyChangedEventHandler<FComboBoxItem> DeleteItem
        {
            add { AddHandler(DeleteItemEvent, value); }
            remove { RemoveHandler(DeleteItemEvent, value); }
        }
        internal void OnDeleteItem(FComboBoxItem oldItem, FComboBoxItem newItem)
        {
            RoutedPropertyChangedEventArgs<FComboBoxItem> arg = new RoutedPropertyChangedEventArgs<FComboBoxItem>(oldItem, newItem, DeleteItemEvent);
            RaiseEvent(arg);
        }
        #endregion

        #region Property
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //[Obsolete("该属性对此控件无效。请使用BindingItems属性替代。", true)]
        //public new IEnumerable ItemsSource
        //{
        //    get { return base.ItemsSource; }
        //    private set { base.ItemsSource = value; }
        //}

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("该属性对此控件无效。BindingItems属性中的Header属性即为要显示的内容。", true)]
        public new string DisplayMemberPath
        {
            get { return base.DisplayMemberPath; }
            private set { base.DisplayMemberPath = value; }
        }

        /// <summary>
        /// 获取或设置鼠标悬浮时子项的背景颜色。默认值为浅灰色(#EEEEEE)。
        /// </summary>
        public Brush CoverBrush
        {
            get { return (Brush)GetValue(CoverBrushProperty); }
            set { SetValue(CoverBrushProperty, value); }
        }
        public static readonly DependencyProperty CoverBrushProperty =
            DependencyProperty.Register("CoverBrush", typeof(Brush), typeof(FComboBox), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EEEEEE"))));

        /// <summary>
        /// 获取或设置子项被选中时的背景颜色。默认值为浅灰色(#DDDDDD)。
        /// </summary>
        public Brush SelectedBrush
        {
            get { return (Brush)GetValue(SelectedBrushProperty); }
            set { SetValue(SelectedBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBrushProperty =
            DependencyProperty.Register("SelectedBrush", typeof(Brush), typeof(FComboBox), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DDDDDD"))));

        /// <summary>
        /// 获取或设置显示框和下拉框的圆角大小。默认值为0。
        /// </summary>
        public CornerRadius BorderCornerRadius
        {
            get { return (CornerRadius)GetValue(BorderCornerRadiusProperty); }
            set { SetValue(BorderCornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty BorderCornerRadiusProperty =
            DependencyProperty.Register("BorderCornerRadius", typeof(CornerRadius), typeof(FComboBox), new PropertyMetadata(new CornerRadius(0)));

        /// <summary>
        ///  获取或设置下拉框激活时阴影的颜色，默认值为#888888。
        /// </summary>
        public Color ShadowColor
        {
            get { return (Color)GetValue(ShadowColorProperty); }
            set { SetValue(ShadowColorProperty, value); }
        }
        public static readonly DependencyProperty ShadowColorProperty = DependencyProperty.Register("ShadowColor", typeof(Color), typeof(FComboBox), new PropertyMetadata((Color)ColorConverter.ConvertFromString("#888888")));

        /// <summary>
        /// 获取或设置当子项目可删除时，用户点击删除按钮后的操作。默认为删除项目并触发DeleteItem路由事件。
        /// </summary>
        public DeleteModes DeleteMode
        {
            get { return (DeleteModes)GetValue(DeleteModeProperty); }
            set { SetValue(DeleteModeProperty, value); }
        }

        public static readonly DependencyProperty DeleteModeProperty =
            DependencyProperty.Register("DeleteMode", typeof(DeleteModes), typeof(FComboBox), new PropertyMetadata(DeleteModes.Delete));

        /// <summary>
        /// 若使用MVVM绑定，请使用此依赖属性。
        /// </summary>
        public ObservableCollection<PUComboBoxItemModel> BindingItems
        {
            get { return (ObservableCollection<PUComboBoxItemModel>)GetValue(BindingItemsProperty); }
            set { SetValue(BindingItemsProperty, value); }
        }

        public static readonly DependencyProperty BindingItemsProperty =
            DependencyProperty.Register("BindingItems", typeof(ObservableCollection<PUComboBoxItemModel>), typeof(FComboBox), new PropertyMetadata(null, OnBindingItemsChanged));

        private static void OnBindingItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var comboBox = d as FComboBox;
            if (comboBox.BindingItems != null)
            {
                comboBox.BindingItems.CollectionChanged -= comboBox.BindingItemChanged;
                comboBox.BindingItems.CollectionChanged += comboBox.BindingItemChanged;
            }
            comboBox.GenerateBindindItems(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        private void BindingItemChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            GenerateBindindItems(e);
        }

        /// <summary>
        /// 获取或设置当子项目被选中时，SelectedValue应呈现子项目的哪一个值。（在ComboBox中，Header属性表示展现子项的Content属性）
        /// 可选项为Header或Value，默认值为Header。
        /// </summary>
        public new SelectedValuePaths SelectedValuePath
        {
            get { return (SelectedValuePaths)GetValue(SelectedValuePathProperty); }
            set { SetValue(SelectedValuePathProperty, value); }
        }

        public new static readonly DependencyProperty SelectedValuePathProperty =
            DependencyProperty.Register("SelectedValuePath", typeof(SelectedValuePaths), typeof(FComboBox), new PropertyMetadata(SelectedValuePaths.Header));

        /// <summary>
        /// 获取被选中PUComboBoxItem的Header或Value属性（这取决于SelectedValuePath），
        /// 或根据设置的SelectedValue来选中子项目。
        /// </summary>
        public new object SelectedValue
        {
            get { return (object)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        public new static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(FComboBox), new PropertyMetadata("", OnSelectedValueChanged));

        private static void OnSelectedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var comboBox = d as FComboBox;
            if (comboBox.SelectedValue == null)
            {
                return;
            }
            if (e.NewValue.Equals(e.OldValue))
                return;

            var selectedItem = comboBox.SelectedItem as FComboBoxItem;
            foreach (var item in comboBox.Items)
            {

                var comboBoxItem = item as FComboBoxItem;
                if ((comboBox.SelectedValuePath == SelectedValuePaths.Header ?
                    (comboBoxItem.Content == null ? false : comboBoxItem.Content.Equals(comboBox.SelectedValue)) :
                    (comboBoxItem.Value == null ? false : comboBoxItem.Value.Equals(comboBox.SelectedValue))))
                {
                    if (!comboBoxItem.IsSelected)
                        comboBoxItem.IsSelected = true;
                    return;
                }
            }
        }

        /// <summary>
        /// 获取或设置搜索模式。默认为不显示搜索。
        /// </summary>
        public SearchModes SearchMode
        {
            get { return (SearchModes)GetValue(SearchModeProperty); }
            set { SetValue(SearchModeProperty, value); }
        }

        public static readonly DependencyProperty SearchModeProperty =
            DependencyProperty.Register("SearchMode", typeof(SearchModes), typeof(FComboBox), new PropertyMetadata(SearchModes.None, OnSearchModeChanged));

        private static void OnSearchModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var comboBox = d as FComboBox;
            if (comboBox.IsLoaded)
            {
                comboBox.RemoveHandler(FTextBox.TextChangedEvent, new RoutedEventHandler(comboBox.OnSearchTextChanged));
                comboBox.RemoveHandler(FTextBox.KeyDownEvent, new RoutedEventHandler(comboBox.OnSearchKeyDown));

                if (comboBox.SearchMode == SearchModes.TextChanged)
                {
                    comboBox.AddHandler(FTextBox.TextChangedEvent, new RoutedEventHandler(comboBox.OnSearchTextChanged));
                    comboBox.SearchBoxVisibility = Visibility.Visible;
                }
                else if (comboBox.SearchMode == SearchModes.Enter)
                {
                    comboBox.AddHandler(FTextBox.PreviewKeyDownEvent, new RoutedEventHandler(comboBox.OnSearchKeyDown));
                    comboBox.SearchBoxVisibility = Visibility.Visible;
                }
                else
                {
                    comboBox.SearchBoxVisibility = Visibility.Collapsed;
                }
            }
        }
        #endregion

        #region Internal Property

        internal Visibility SearchBoxVisibility
        {
            get { return (Visibility)GetValue(SearchBoxVisibilityProperty); }
            set { SetValue(SearchBoxVisibilityProperty, value); }
        }

        internal static readonly DependencyProperty SearchBoxVisibilityProperty =
            DependencyProperty.Register("SearchBoxVisibility", typeof(Visibility), typeof(FComboBox), new PropertyMetadata(Visibility.Collapsed));
        #endregion

        #region APIs
        /// <summary>
        /// 通过内容选中项目。
        /// <para>若content不是值类型，则将逐一比较其中各个属性的值是否相等。</para>
        /// </summary>
        /// <param name="content">要匹配的内容。</param>
        public void SelectItemByContent(object content)
        {
            var comboItem = GetItemByContent(content);
            if (comboItem != null)
                comboItem.IsSelected = true;
        }

        /// <summary>
        /// 通过Value选中项目。
        /// <para>若value不是值类型，则将逐一比较其中各个属性的值是否相等。</para>
        /// </summary>
        /// <param name="value">要匹配的value。</param>
        public void SelectItemByValue(object value)
        {
            var comboItem = GetItemByValue(value);
            if (comboItem != null)
                comboItem.IsSelected = true;
        }


        #endregion

        #region Function

        private void GenerateBindindItems(NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    var value = SelectedValue;
                    SelectedValue = null;
                    Items.Clear();
                    if (BindingItems == null)
                        break;
                    foreach (var item in BindingItems)
                    {
                        var comboBoxItem = GenerateComboBoxItem(item);
                        Items.Add(comboBoxItem);
                    }
                    SelectedValue = value;
                    break;
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems)
                    {
                        var comboBoxItem = GenerateComboBoxItem(item as PUComboBoxItemModel);
                        Items.Insert(e.NewStartingIndex, comboBoxItem);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                    {
                        Items.RemoveAt(e.OldStartingIndex);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    foreach (var item in e.NewItems)
                    {
                        var comboBoxItem = GenerateComboBoxItem(item as PUComboBoxItemModel);
                        Items[e.OldStartingIndex] = comboBoxItem;
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    {
                        var tabItem = Items[e.OldStartingIndex];
                        Items.RemoveAt(e.OldStartingIndex);
                        Items.Insert(e.NewStartingIndex, tabItem);
                    }
                    break;
            }
            if (SelectedValue != null)
            {
                if (SelectedValuePath == SelectedValuePaths.Header)
                    SelectItemByContent(SelectedValue);
                else
                    SelectItemByValue(SelectedValue);
            }
        }

        private FComboBoxItem GenerateComboBoxItem(PUComboBoxItemModel model)
        {
            var comboBoxItem = new FComboBoxItem()
            {
                Uid = model.Uid,
                Content = model.Header,
                Value = model.Value,
                CanDelete = model.CanDelete,
            };

            model.PropertyChanged += delegate
            {
                comboBoxItem.Content = model.Header;
                comboBoxItem.Value = model.Value;
                comboBoxItem.CanDelete = model.CanDelete;
            };

            return comboBoxItem;
        }

        private FComboBoxItem GetItemByContent(object content)
        {
            foreach (var item in Items)
            {
                var comboItem = item as FComboBoxItem;
                if (comboItem == null)
                    throw new Exception("PUComboBox的子项必须是PUComboBoxItem。");
                //if (comboItem.Content.IsEqual(content))
                //    return comboItem;

                if (comboItem.Content== content)
                    return comboItem;
            }
            return null;
        }

        private FComboBoxItem GetItemByValue(object value)
        {
            foreach (var item in Items)
            {
                var comboItem = item as FComboBoxItem;
                if (comboItem == null)
                    throw new Exception("PUComboBox的子项必须是PUComboBoxItem。");
                //if (comboItem.Value.IsEqual(value))
                //    return comboItem;

                if (comboItem.Value== value)
                    return comboItem;
            }
            return null;
        }


        #endregion


    }


    public class FComboBoxItem : ComboBoxItem
    {

        public FComboBoxItem()
        {

        }

        static FComboBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FComboBoxItem), new FrameworkPropertyMetadata(typeof(FComboBoxItem)));
        }

        #region Property
        /// <summary>
        /// 获取或设置是否显示删除按钮，默认值为False（不显示）。
        /// </summary>
        public bool CanDelete
        {
            get { return (bool)GetValue(CanDeleteProperty); }
            set { SetValue(CanDeleteProperty, value); }
        }

        public static readonly DependencyProperty CanDeleteProperty =
            DependencyProperty.Register("CanDelete", typeof(bool), typeof(FComboBoxItem));


        /// <summary>
        /// 获取或设置用以标记该项目的值。默认值为Null。
        /// </summary>
        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(FComboBoxItem));


        #endregion

        public ICommand DeleteCommand
        {
            get
            { return _deleteCommand; }
        }
        private ICommand _deleteCommand = new PUComboBoxDeleteCommand();
    }

    internal sealed class PUComboBoxDeleteCommand : ICommand
    {
        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var comItem = (parameter as FComboBoxItem);
            var combox = comItem.Parent as FComboBox;
            if (combox.DeleteMode == DeleteModes.Delete)
            {
                if (combox.BindingItems != null && !String.IsNullOrEmpty((comItem.Uid)))
                {
                    var model = combox.BindingItems.FirstOrDefault(x => x.Uid == comItem.Uid);
                    if (model != null)
                        combox.BindingItems.Remove(model);
                    else
                        combox.Items.Remove(comItem);
                }
                else
                {
                    combox.Items.Remove(comItem);
                }
            }
            combox.OnDeleteItem(null, comItem);
        }
    }

    public class PUComboBoxItemModel : INotifyPropertyChanged
    {
        protected internal virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #region Constructor
        public PUComboBoxItemModel()
        {
            Uid = Guid.NewGuid().ToString("N");
        }
        #endregion

        #region Property
        /// <summary>
        /// 要显示的名称。可以作为SelectValuePath的值。
        /// </summary>
        public string Header
        {
            get { return _header; }
            set { _header = value; OnPropertyChanged("Header"); }
        }
        private string _header = "";

        /// <summary>
        /// 该对象的值。可以作为SelectValuePath的值。
        /// <para>若Value不是值类型，使用Value作为匹配时会逐一比较每一个可写属性的值（参见PanuonUI.Utils扩展方法IsEqual）。</para>
        /// </summary>
        public object Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged("Value"); }
        }
        private object _value;

        /// <summary>
        /// 是否显示删除按钮。
        /// </summary>
        public bool CanDelete
        {
            get { return _canDelete; }
            set { _canDelete = value; OnPropertyChanged("CanDelete"); }
        }
        private bool _canDelete = false;

        /// <summary>
        /// 生成该对象时，自动生成的唯一ID。该属性 与该对象生成的PUComboBoxItem的Uid属性值相等。
        /// </summary>
        public string Uid
        {
            get { return _uid; }
            private set { _uid = value; }
        }
        private string _uid;
        #endregion
    }

    /// <summary>
    /// (PanuonUI) SelectedValuePath for PUTabControl , PUComboBox and PUTreeView
    /// </summary>
    public enum SelectedValuePaths
    {
        /// <summary>
        /// SelectedValue应呈现被选中子项的Header属性（在ComboBox中为Content属性）。
        /// </summary>
        Header,
        /// <summary>
        /// SelectedValue应呈现被选中子项的Value属性。
        /// </summary>
        Value
    }

    /// <summary>
    /// (PanuonUI) SearchMode for PUComboBox
    /// </summary>
    public enum SearchModes
    {
        /// <summary>
        /// 不显示搜索框。
        /// </summary>
        None,
        /// <summary>
        /// 在搜索框按下键盘时搜索。
        /// </summary>
        TextChanged,
        /// <summary>
        /// 当按下Enter键时发起搜索。
        /// </summary>
        Enter,
    }

    /// <summary>
    /// (PanuonUI) DeleteModes for PUTabControl and PUComboBox
    /// </summary>
    public enum DeleteModes
    {
        /// <summary>
        /// 当用户点击删除按钮时，应立即删除项目并触发DeleteItem路由事件。
        /// </summary>
        Delete,
        /// <summary>
        /// 当用户点击删除按钮时，不删除项目，只触发DeleteItem路由事件。
        /// </summary>
        EventOnly,
    }


}
