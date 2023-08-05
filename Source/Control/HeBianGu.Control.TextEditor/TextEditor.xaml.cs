// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.Control.TextEditor
{
    [TemplatePart(Name = Part_TextBlock, Type = typeof(TextBlock))]
    [TemplatePart(Name = PART_TextBox, Type = typeof(TextBox))]
    public partial class TextEditor : System.Windows.Controls.Control
    {

        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(TextEditor), "S.TextEditor.Default");
        public static ComponentResourceKey TextKey => new ComponentResourceKey(typeof(TextEditor), "S.TextEditor.Text");
        public static ComponentResourceKey TextBoxKey => new ComponentResourceKey(typeof(TextEditor), "S.TextEditor.TextBox");

        private const string Part_TextBlock = "PART_TextBlock";
        private const string PART_TextBox = "PART_TextBox";
        static TextEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextEditor), new FrameworkPropertyMetadata(typeof(TextEditor)));
        }

        public static RoutedCommand Sure { get; } = new RoutedCommand(nameof(Sure), typeof(TextEditor));

        public static RoutedCommand Cancel { get; } = new RoutedCommand(nameof(Cancel), typeof(TextEditor));


        public TextEditor()
        {
            {
                CommandBinding binding = new CommandBinding(TextEditor.Sure, (l, k) =>
                {
                    this.SaveData();
                    this.IsEditting = false;
                },
                (l, k) =>
                {
                    k.CanExecute = this.Message == null;
                });

                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(TextEditor.Cancel, (l, k) =>
                {
                    this.IsEditting = false;
                });

                this.CommandBindings.Add(binding);
            }
        }

        private TextBlock _textBlock;
        private TextBox _textBox;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _textBlock = this.Template.FindName(Part_TextBlock, this) as TextBlock;
            _textBox = this.Template.FindName(PART_TextBox, this) as TextBox;

            this.MouseLeftButtonDown += (l, k) =>
              {
                  if (this.EditActiveMode != EditActiveMode.MouseDown) 
                      return;

                  if (this.IsEditting == true)
                      return;

                  this.IsEditting = true;

                  this.SetEditState();
              };


            this.MouseDoubleClick += (l, k) =>
              {
                  if (this.EditActiveMode != EditActiveMode.DoubleMouseDown) return;

                  if (this.IsEditting == true) return;

                  this.IsEditting = true;

                  this.SetEditState();
              };


            _textBox.TextChanged += (l, k) =>
              {
                  this.ValidData();
              };

            _textBox.LostFocus += (l, k) =>
              {
                  if (this.CancelMode != CancelMode.LostFocus) return;

                  this.IsEditting = false;

                  this.SaveData();
              };
        }


        public EditActiveMode EditActiveMode
        {
            get { return (EditActiveMode)GetValue(EditActiveModeProperty); }
            set { SetValue(EditActiveModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditActiveModeProperty =
            DependencyProperty.Register("EditActiveMode", typeof(EditActiveMode), typeof(TextEditor), new FrameworkPropertyMetadata(EditActiveMode.DoubleMouseDown, (d, e) =>
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
            DependencyProperty.Register("CancelMode", typeof(CancelMode), typeof(TextEditor), new FrameworkPropertyMetadata(default(CancelMode), (d, e) =>
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


        public bool IsEditting
        {
            get { return (bool)GetValue(IsEdittingProperty); }
            set { SetValue(IsEdittingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEdittingProperty =
            DependencyProperty.Register("IsEditting", typeof(bool), typeof(TextEditor), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 TextEditor control = d as TextEditor;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {
                     if (n)
                         control.SetEditState();
                 }
             }));

        private void SetEditState()
        {
            this.EditText = this.Text;
            this._textBox?.Focus();
            this._textBox?.SelectAll();
            if (this._textBlock != null)
                this._textBlock.Visibility = Visibility.Hidden;
        }

        public object Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(TextEditor), new FrameworkPropertyMetadata(default(object), (d, e) =>
             {
                 TextEditor control = d as TextEditor;

                 if (control == null) return;

                 if (e.OldValue is object o)
                 {

                 }

                 if (e.NewValue is object n)
                 {
                     control.RefreshData();
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
            DependencyProperty.Register("UseCancel", typeof(bool), typeof(TextEditor), new FrameworkPropertyMetadata(true, (d, e) =>
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
            DependencyProperty.Register("UseSure", typeof(bool), typeof(TextEditor), new FrameworkPropertyMetadata(true, (d, e) =>
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
            DependencyProperty.Register("DefaultType", typeof(Type), typeof(TextEditor), new FrameworkPropertyMetadata(default(Type), (d, e) =>
             {
                 TextEditor control = d as TextEditor;

                 if (control == null) return;

                 if (e.OldValue is Type o)
                 {

                 }

                 if (e.NewValue is Type n)
                 {

                 }

             }));

        private void RefreshData()
        {
            this.Text = this.Content.TryConvertToString(out string error);
            this.Message = error;
        }

        private void SaveData()
        {
            this._textBlock.Visibility = Visibility.Visible;

            if (!string.IsNullOrEmpty(this.Message))
                return;

            Type type = this.Content?.GetType();

            type = type ?? this.DefaultType;

            if (type == null) return;

            this.Content = type.TryConvertFromString(this.EditText, out string error);

            this.Message = error;

            this.OnSaveDatad();
        }


        //声明和注册路由事件
        public static readonly RoutedEvent SaveDatadRoutedEvent =
            EventManager.RegisterRoutedEvent("SaveDatad", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(TextEditor));
        //CLR事件包装
        public event RoutedEventHandler SaveDatad
        {
            add { this.AddHandler(SaveDatadRoutedEvent, value); }
            remove { this.RemoveHandler(SaveDatadRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnSaveDatad()
        {
            RoutedEventArgs args = new RoutedEventArgs(SaveDatadRoutedEvent, this);
            this.RaiseEvent(args);
        }


        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(TextEditor), new FrameworkPropertyMetadata(default(string), (d, e) =>
             {
                 TextEditor control = d as TextEditor;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }
             }));

        private void ValidData()
        {
            this.Message = null;

            if (this.Content == null) return;

            Type type = this.Content.GetType();

            object o = type.TryConvertFromString(this.EditText, out string error);

            if (o == null)
            {
                this.Message = error;
            }
        }


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextEditor), new FrameworkPropertyMetadata(default(string), (d, e) =>
             {
                 TextEditor control = d as TextEditor;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

             }));


        public string EditText
        {
            get { return (string)GetValue(EditTextProperty); }
            set { SetValue(EditTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditTextProperty =
            DependencyProperty.Register("EditText", typeof(string), typeof(TextEditor), new FrameworkPropertyMetadata(default(string), (d, e) =>
             {
                 TextEditor control = d as TextEditor;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

             }));


        public Style TextBoxStyle
        {
            get { return (Style)GetValue(TextBoxStyleProperty); }
            set { SetValue(TextBoxStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxStyleProperty =
            DependencyProperty.Register("TextBoxStyle", typeof(Style), typeof(TextEditor), new FrameworkPropertyMetadata(default(Style), (d, e) =>
             {
                 TextEditor control = d as TextEditor;

                 if (control == null) return;

                 if (e.OldValue is Style o)
                 {

                 }

                 if (e.NewValue is Style n)
                 {

                 }

             }));



        public Style TextBlockStyle
        {
            get { return (Style)GetValue(TextBlockStyleProperty); }
            set { SetValue(TextBlockStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBlockStyleProperty =
            DependencyProperty.Register("TextBlockStyle", typeof(Style), typeof(TextEditor), new FrameworkPropertyMetadata(default(Style), (d, e) =>
             {
                 TextEditor control = d as TextEditor;

                 if (control == null) return;

                 if (e.OldValue is Style o)
                 {

                 }

                 if (e.NewValue is Style n)
                 {

                 }

             }));

    }

    public enum CancelMode
    {
        LostFocus = 0, None
    }

    public enum EditActiveMode
    {
        MouseDown = 0, DoubleMouseDown
    }
}
