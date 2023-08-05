// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HeBianGu.Control.TextEditor
{
    public partial class TextEditorBox : ItemsControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(TextEditorBox), "S.TextEditorBox.Default");

        static TextEditorBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextEditorBox), new FrameworkPropertyMetadata(typeof(TextEditorBox)));
        }

        public EditActiveMode EditActiveMode
        {
            get { return (EditActiveMode)GetValue(EditActiveModeProperty); }
            set { SetValue(EditActiveModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditActiveModeProperty =
            DependencyProperty.Register("EditActiveMode", typeof(EditActiveMode), typeof(TextEditorBox), new FrameworkPropertyMetadata(EditActiveMode.DoubleMouseDown, (d, e) =>
             {
                 TextEditor control = d as TextEditor;

                 if (control == null) return;

                 if (e.OldValue is EditActiveMode o)
                 {

                 }

                 if (e.NewValue is EditActiveMode n)
                 {

                 }

             }));


        public CancelMode CancelMode
        {
            get { return (CancelMode)GetValue(CancelModeProperty); }
            set { SetValue(CancelModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CancelModeProperty =
            DependencyProperty.Register("CancelMode", typeof(CancelMode), typeof(TextEditorBox), new FrameworkPropertyMetadata(default(CancelMode), (d, e) =>
             {
                 TextEditor control = d as TextEditor;

                 if (control == null) return;

                 if (e.OldValue is CancelMode o)
                 {

                 }

                 if (e.NewValue is CancelMode n)
                 {

                 }

             }));

        [Bindable(true)]
        public bool UseCancel
        {
            get { return (bool)GetValue(UseCancelProperty); }
            set { SetValue(UseCancelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseCancelProperty =
            DependencyProperty.Register("UseCancel", typeof(bool), typeof(TextEditorBox), new FrameworkPropertyMetadata(true, (d, e) =>
             {
                 TextEditor control = d as TextEditor;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }
             }));

        [Bindable(true)]
        public bool UseSure
        {
            get { return (bool)GetValue(UseSureProperty); }
            set { SetValue(UseSureProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseSureProperty =
            DependencyProperty.Register("UseSure", typeof(bool), typeof(TextEditorBox), new FrameworkPropertyMetadata(true, (d, e) =>
             {
                 TextEditor control = d as TextEditor;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }
             }));


        public Type DefaultType
        {
            get { return (Type)GetValue(DefaultTypeProperty); }
            set { SetValue(DefaultTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultTypeProperty =
            DependencyProperty.Register("DefaultType", typeof(Type), typeof(TextEditorBox), new FrameworkPropertyMetadata(default(Type), (d, e) =>
             {
                 TextEditorBox control = d as TextEditorBox;

                 if (control == null) return;

                 if (e.OldValue is Type o)
                 {

                 }

                 if (e.NewValue is Type n)
                 {

                 }

             }));


        public IEnumerable DataSource
        {
            get { return (IEnumerable)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(IEnumerable), typeof(TextEditorBox), new FrameworkPropertyMetadata(default(IEnumerable), (d, e) =>
             {
                 TextEditorBox control = d as TextEditorBox;

                 if (control == null) return;

                 if (e.OldValue is INotifyCollectionChanged o)
                 {

                 }

                 if (e.NewValue is INotifyCollectionChanged n)
                 {

                 }
                 control.RefreshData();

             }));




        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems == null) return;

                foreach (var item in e.NewItems)
                {
                    var find = this.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;

                    if (find == null) continue;

                    find.ApplyTemplate();

                    var editor = find.GetChild<TextEditor>();

                    if (editor == null) continue;

                    //editor.ApplyTemplate();

                    editor.IsEditting = true; 

                    if (item.GetType().IsPrimitive || item.GetType() == typeof(string) || item.GetType() == typeof(DateTime))
                    {
                        editor.SaveDatad += (l, k) =>
                        {
                            if(this.DataSource is IList list)
                            {
                                int index = list.IndexOf(item);
                                if (index < 0) return;
                                list[index] = editor.Content;
                            }
                        };
                    }
                }
            }
        }


        public Style ButtonStyle
        {
            get { return (Style)GetValue(ButtonStyleProperty); }
            set { SetValue(ButtonStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonStyleProperty =
            DependencyProperty.Register("ButtonStyle", typeof(Style), typeof(TextEditorBox), new FrameworkPropertyMetadata(default(Style), (d, e) =>
             {
                 TextEditorBox control = d as TextEditorBox;

                 if (control == null) return;

                 if (e.OldValue is Style o)
                 {

                 }

                 if (e.NewValue is Style n)
                 {
                     control.button.Style = n;
                 }

             }));


        public bool UseAddButton
        {
            get { return (bool)GetValue(UseAddButtonProperty); }
            set { SetValue(UseAddButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseAddButtonProperty =
            DependencyProperty.Register("UseAddButton", typeof(bool), typeof(TextEditorBox), new FrameworkPropertyMetadata(true, (d, e) =>
             {
                 TextEditorBox control = d as TextEditorBox;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));
        private Button button = new Button();

        public TextEditorBox()
        {
            button.Style = this.ButtonStyle;

            button.Click += (l, k) =>
              {
                  this.DataSource?.Addtem();
              };

            this.RefreshData();
        }

        private void RefreshData()
        {
            CompositeCollection compositeCollection = new CompositeCollection();

            CollectionContainer container = new CollectionContainer();

            container.Collection = this.DataSource;

            compositeCollection.Add(container);

            if (this.UseAddButton)
            {
                compositeCollection.Add(button);
            }

            if (this.ToolContent != null)
            {
                compositeCollection.Add(this.ToolContent);
            }

            this.ItemsSource = compositeCollection;

        }

        public object ToolContent
        {
            get { return GetValue(ToolContentProperty); }
            set { SetValue(ToolContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToolContentProperty =
            DependencyProperty.Register("ToolContent", typeof(object), typeof(TextEditorBox), new FrameworkPropertyMetadata(default(object), (d, e) =>
             {
                 TextEditorBox control = d as TextEditorBox;

                 if (control == null) return;

                 if (e.OldValue is object o)
                 {

                 }

                 if (e.NewValue is object n)
                 {

                 }
                 control.RefreshData();

             }));
    }

}
