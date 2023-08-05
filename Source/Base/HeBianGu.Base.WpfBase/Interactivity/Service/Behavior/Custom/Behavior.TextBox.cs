// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 显示输入内容 目前测试输入法输入时有问题 </summary>
    public class MaskedTextBoxBehavior : Behavior<TextBox>
    {
        #region MaskProperty
        /// <summary>
        /// This property serves a dual purpose.  First, it allows the behavior to be used as a traditional
        /// attached property behavior - without use of the Blend tool.  Second, it acts as a local property
        /// to store the expression.
        /// </summary>
        public static DependencyProperty MaskProperty =
            DependencyProperty.RegisterAttached("Mask", typeof(string), typeof(MaskedTextBoxBehavior),
                new FrameworkPropertyMetadata(null, OnMaskChanged));

        /// <summary>
        /// Returns whether MaskedTextBoxBehavior is enabled via attached property
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns>Regular Expression Mask</returns>
        public static string GetMask(TextBox textBox)
        {
            return (string)textBox.GetValue(MaskProperty);
        }


        /// <summary>
        /// Adds MaskedTextBoxBehavior to TextBox
        /// </summary>
        /// <param name="textBox">TextBox to apply</param>
        /// <param name="value">True/False</param>
        public static void SetMask(TextBox textBox, string value)
        {
            textBox.SetValue(MaskProperty, value);
        }

        /// <summary>
        /// Mask to use for the regular expression.
        /// </summary>
        public string Mask
        {
            get { return (string)GetValue(MaskProperty); }
            set { SetValue(MaskProperty, value); }
        }

        /// <summary>
        /// This associates a new MaskedTextBoxBehavior onto a TextBox using the applied mask.
        /// </summary>
        /// <param name="dpo"></param>
        /// <param name="e"></param>
        private static void OnMaskChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        {
            // If this is being applied to a TextBox then add it to the behaviors collection.
            TextBox tb = dpo as TextBox;
            if (tb != null)
            {
                BehaviorCollection behColl = Interaction.GetBehaviors(tb);
                MaskedTextBoxBehavior existingBehavior = behColl.FirstOrDefault(b => b.GetType() == typeof(MaskedTextBoxBehavior)) as MaskedTextBoxBehavior;

                string mask = (e.NewValue != null) ? e.NewValue.ToString() : null;
                if (string.IsNullOrEmpty(mask) && existingBehavior != null)
                {
                    behColl.Remove(existingBehavior);
                }
                else if (!string.IsNullOrEmpty(mask) && existingBehavior == null)
                {
                    behColl.Add(new MaskedTextBoxBehavior { Mask = mask });
                }
            }
        }
        #endregion

        /// <summary>
        /// This attaches the property handlers - PreviewKeyDown and Clipboard support.
        /// </summary>
        protected override void OnAttached()
        {
            AssociatedObject.PreviewKeyDown += OnKeyDown;
            AssociatedObject.PreviewTextInput += OnTextInput;
            DataObject.AddPastingHandler(AssociatedObject, OnClipboardPaste);
        }

        /// <summary>
        /// This removes all our handlers.
        /// </summary>
        protected override void OnDetaching()
        {
            AssociatedObject.PreviewKeyDown -= OnKeyDown;
            AssociatedObject.PreviewTextInput -= OnTextInput;
            DataObject.RemovePastingHandler(AssociatedObject, OnClipboardPaste);
        }

        /// <summary>
        /// This is called to process text (character) input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsMatch(GetTextFuture(e.Text));
        }

        /// <summary>
        /// This method handles paste and drag/drop events onto the TextBox.  It restricts the character
        /// set to numerics and ensures we have consistent behavior.
        /// </summary>
        /// <param name="sender">TextBox sender</param>
        /// <param name="e">EventArgs</param>
        private void OnClipboardPaste(object sender, DataObjectPastingEventArgs e)
        {
            string text = e.SourceDataObject.GetData(e.FormatToApply) as string;
            if (!string.IsNullOrEmpty(text))
            {
                if (IsMatch(GetTextFuture(text)))
                    return;
            }
            e.CancelCommand();
        }

        /// <summary>
        /// This checks the PreviewKeyDown on the TextBox to check for the SPACE
        /// character which is not considered a TextInput element.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = !IsMatch(GetTextFuture(" "));
        }

        /// <summary>
        /// This returns the next text value if the pending event is processed.
        /// </summary>
        /// <param name="textToAdd">Text to add to the TextBox</param>
        /// <returns>String</returns>
        private string GetTextFuture(string textToAdd)
        {
            string currentText = AssociatedObject.Text;
            if (AssociatedObject.SelectionStart >= 0)
                currentText = currentText.Remove(AssociatedObject.SelectionStart, AssociatedObject.SelectionLength);
            currentText = currentText.Insert(AssociatedObject.CaretIndex, textToAdd);
            return currentText;
        }

        /// <summary>
        /// This method is used to test the string input.
        /// </summary>
        /// <param name="textToCheck"></param>
        /// <returns></returns>
        private bool IsMatch(string textToCheck)
        {
            return string.IsNullOrEmpty(Mask)
                || Regex.IsMatch(textToCheck, Mask, RegexOptions.IgnorePatternWhitespace);
        }
    }


    /// <summary> TextBox支持数值输入，鼠标滚动 拖动改变值等效果的行为 </summary>
    public class NumericTextBoxBehavior : Behavior<TextBox>
    {
        #region IsEnabledProperty
        /// <summary>
        /// This property allows the behavior to be used as a traditional
        /// attached property behavior.
        /// </summary>
        public static DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(NumericTextBoxBehavior),
                new FrameworkPropertyMetadata(false, OnIsEnabledChanged));

        /// <summary>
        /// Returns whether NumericTextBoxBehavior is enabled via attached property
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns>True/FalseB</returns>
        public static bool GetIsEnabled(TextBox textBox)
        {
            return (bool)textBox.GetValue(IsEnabledProperty);
        }

        /// <summary>
        /// Adds NumericTextBoxBehavior to TextBox
        /// </summary>
        /// <param name="textBox">TextBox to apply</param>
        /// <param name="value">True/False</param>
        public static void SetIsEnabled(TextBox textBox, bool value)
        {
            textBox.SetValue(IsEnabledProperty, value);
        }

        private static void OnIsEnabledChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = dpo as TextBox;
            if (tb != null)
            {
                BehaviorCollection behColl = Interaction.GetBehaviors(tb);
                NumericTextBoxBehavior existingBehavior = behColl.FirstOrDefault(b => b.GetType() == typeof(NumericTextBoxBehavior)) as NumericTextBoxBehavior;
                if ((bool)e.NewValue == false && existingBehavior != null)
                {
                    behColl.Remove(existingBehavior);
                }
                else if ((bool)e.NewValue == true && existingBehavior == null)
                {
                    behColl.Add(new NumericTextBoxBehavior());
                }
            }
        }
        #endregion

        #region AllowMouseDrag

        /// <summary>
        /// AllowMouseDrag Dependency Property
        /// </summary>
        public static readonly DependencyProperty AllowMouseDragProperty = DependencyProperty.Register("AllowMouseDrag", typeof(bool),
                typeof(NumericTextBoxBehavior), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Gets or sets the AllowMouseDrag property. When set to "True", the NumericTextBoxBehavior will allow
        /// Mouse "drags" to adjust the value - similar to the Blend text boxes.
        /// </summary>
        public bool AllowMouseDrag
        {
            get { return (bool)GetValue(AllowMouseDragProperty); }
            set { SetValue(AllowMouseDragProperty, value); }
        }

        #endregion

        /// <summary>
        /// Backing storage for minimum
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(long), typeof(NumericTextBoxBehavior), new PropertyMetadata(Int64.MinValue));

        /// <summary>
        /// Minimum value
        /// </summary>
        public long Minimum
        {
            get { return (long)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// Backing storage for maximum
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(long), typeof(NumericTextBoxBehavior), new PropertyMetadata(Int64.MaxValue));

        /// <summary>
        /// Maximum value
        /// </summary>
        public long Maximum
        {
            get { return (long)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// Minimum amount of distance mouse must be "dragged" before we begin changing
        /// the numeric value.
        /// </summary>
        private const int MIN_DELTA = 10;

        /// <summary>
        /// This visual adorner is used to provide mouse cursor + numeric up/down drag support
        /// on top of an existing TextBox.  It provides scroll wheel + left/right dragging
        /// </summary>
        private class CursorAdorner : Adorner, IDisposable
        {
            private readonly NumericTextBoxBehavior _behaviorOwner;
            private Point _lastPos;
            private bool _isCaptured;
            private bool _changedValue;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="textBox"></param>
            /// <param name="behaviorOwner"></param>
            public CursorAdorner(TextBox textBox, NumericTextBoxBehavior behaviorOwner)
                : base(textBox)
            {
                _behaviorOwner = behaviorOwner;
                AdornerLayer layer = AdornerLayer.GetAdornerLayer(textBox);
                Debug.Assert(layer != null);
                layer.Add(this);
            }

            /// <summary>
            /// This is called as the cursor moves to change the visual representation on the screen.
            /// We display the W/E cursor or the IBeam.
            /// </summary>
            /// <param name="e"></param>
            protected override void OnQueryCursor(QueryCursorEventArgs e)
            {
                TextBox textBox = (TextBox)AdornedElement;
                Debug.Assert(textBox != null);

                if (!textBox.IsKeyboardFocused && !textBox.IsFocused)
                {
                    e.Cursor = Cursors.ScrollWE;
                }
                else
                {
                    e.Cursor = Cursors.IBeam;
                }

                e.Handled = true;
            }

            /// <summary>
            /// Called to render the visual - we place a transparent (hit-testable) rectangle on top
            /// of the text box.
            /// </summary>
            /// <param name="drawingContext"></param>
            protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
            {
                TextBox textBox = (TextBox)AdornedElement;
                drawingContext.DrawRectangle(Brushes.Transparent, null, new Rect(0, 0, textBox.ActualWidth, textBox.ActualHeight));
            }

            /// <summary>
            /// Left button down
            /// </summary>
            /// <param name="e"></param>
            protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
            {
                TextBox textBox = (TextBox)AdornedElement;
                Debug.Assert(textBox != null);

                _isCaptured = true;
                _changedValue = false;
                _lastPos = e.GetPosition(textBox);

                CaptureMouse();
            }

            /// <summary>
            /// Mouse movement
            /// </summary>
            /// <param name="e"></param>
            protected override void OnMouseMove(MouseEventArgs e)
            {
                TextBox textBox = (TextBox)AdornedElement;
                Debug.Assert(textBox != null);

                if (_isCaptured == true)
                {
                    Point ptNew = e.GetPosition(textBox);
                    double dX = ptNew.X - _lastPos.X;

                    if (_changedValue || Math.Abs(dX) > MIN_DELTA)
                    {
                        AdjustValue(textBox, (int)dX);
                        _lastPos = ptNew;
                        _changedValue = true;
                    }
                }
            }

            /// <summary>
            /// Left button up
            /// </summary>
            /// <param name="e"></param>
            protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
            {
                TextBox textBox = (TextBox)AdornedElement;
                Debug.Assert(textBox != null);

                if (_isCaptured)
                {
                    ReleaseMouseCapture();
                    _isCaptured = false;
                    if (!_changedValue)
                        SetTextBoxFocus(textBox);
                }
            }

            /// <summary>
            /// Mouse wheel changes
            /// </summary>
            /// <param name="e"></param>
            protected override void OnMouseWheel(MouseWheelEventArgs e)
            {
                TextBox textBox = (TextBox)AdornedElement;
                Debug.Assert(textBox != null);
                AdjustValue(textBox, (e.Delta > 0) ? -1 : 1);
            }

            /// <summary>
            /// Detection for double-click support
            /// </summary>
            /// <param name="e"></param>
            protected override void OnMouseDown(MouseButtonEventArgs e)
            {
                TextBox textBox = (TextBox)AdornedElement;
                Debug.Assert(textBox != null);

                if (e.ClickCount > 1)
                {
                    SetTextBoxFocus(textBox);
                }
            }

            /// <summary>
            /// This is used to remove the adorner
            /// </summary>
            public void Dispose()
            {
                if (AdornedElement != null)
                {
                    AdornerLayer layer = AdornerLayer.GetAdornerLayer(AdornedElement);
                    if (layer != null)
                    {
                        layer.Remove(this);
                    }
                }
            }

            /// <summary>
            /// This is used to change the focus to the text box and adjust the cursor
            /// </summary>
            /// <param name="textBox"></param>
            private static void SetTextBoxFocus(TextBox textBox)
            {
                textBox.Focus();
                textBox.SelectAll();
                Mouse.UpdateCursor();
            }

            /// <summary>
            /// This is used to adjust the numeric value in the TextBox.
            /// </summary>
            /// <param name="textBox"></param>
            /// <param name="val"></param>
            private void AdjustValue(TextBox textBox, int val)
            {
                string text = textBox.Text;
                long currValue;
                if (long.TryParse(text, out currValue))
                {
                    currValue += val;
                    if (currValue < _behaviorOwner.Minimum)
                        currValue = _behaviorOwner.Minimum;
                    else if (currValue > _behaviorOwner.Maximum)
                        currValue = _behaviorOwner.Maximum;

                    textBox.Text = currValue.ToString();
                }
            }
        }

        private CursorAdorner _adorner;

        /// <summary>
        /// This attaches the property handlers - PreviewKeyDown, Clipboard support and our adorner.
        /// </summary>
        protected override void OnAttached()
        {
            AssociatedObject.PreviewKeyDown += OnKeyDown;
            AssociatedObject.PreviewLostKeyboardFocus += PreviewLostFocus;
            DataObject.AddPastingHandler(AssociatedObject, OnClipboardPaste);

            // Add "Blend" drag support if requested.
            if (AllowMouseDrag)
            {
                if (AssociatedObject.IsLoaded == false)
                {
                    RoutedEventHandler handler = null;
                    handler = (s, e) =>
                    {
                        AssociatedObject.Loaded -= handler;
                        AddAdorner(AssociatedObject);
                    };

                    AssociatedObject.Loaded += handler;
                }
                else
                {
                    AddAdorner(AssociatedObject);
                }
            }
        }

        /// <summary>
        /// Called when we are about to lose keyboard focus - we reformat the number here.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewLostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            string text = AssociatedObject.Text;
            long currValue;
            if (long.TryParse(text, out currValue))
            {
                if (currValue < Minimum)
                    currValue = Minimum;
                else if (currValue > Maximum)
                    currValue = Maximum;

                AssociatedObject.Text = currValue.ToString();
            }
        }

        /// <summary>
        /// This adds the adorner.
        /// </summary>
        private void AddAdorner(TextBox tb)
        {
            _adorner = new CursorAdorner(tb, this);
            Debug.Assert(tb.IsLoaded == true);

        }

        /// <summary>
        /// This removes all our handlers.
        /// </summary>
        protected override void OnDetaching()
        {
            AssociatedObject.PreviewKeyDown -= OnKeyDown;
            DataObject.RemovePastingHandler(AssociatedObject, OnClipboardPaste);

            if (_adorner != null)
            {
                _adorner.Dispose();
                _adorner = null;
            }
        }

        /// <summary>
        /// This method handles paste and drag/drop events onto the TextBox.  It restricts the character
        /// set to numerics and ensures we have consistent behavior.
        /// </summary>
        /// <param name="sender">TextBox sender</param>
        /// <param name="e">EventArgs</param>
        private static void OnClipboardPaste(object sender, DataObjectPastingEventArgs e)
        {
            string text = e.SourceDataObject.GetData(e.FormatToApply) as string;
            if (!string.IsNullOrEmpty(text))
            {
                if (text.Count(ch => !Char.IsNumber(ch)) == 0)
                    return;
            }
            e.CancelCommand();
        }

        /// <summary>
        /// This checks the PreviewKeyDown on the TextBox and constrains it to a numeric value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnKeyDown(object sender, KeyEventArgs e)
        {
            // CTRL key down?
            if (Keyboard.IsKeyDown(Key.RightCtrl) ||
                Keyboard.IsKeyDown(Key.LeftCtrl))
                return;

            // Is a set of known keys?
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) ||
                e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.Escape || e.Key == Key.Enter ||
                e.Key == Key.Home || e.Key == Key.End ||
                e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Tab ||
                (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                (e.Key >= Key.F1 && e.Key <= Key.F24))
                return;

            e.Handled = true;
        }
    }

    public class SearchItemsControlTextBoxBehvior : Behavior<TextBox>
    {
        public ItemsControl ItemsControl
        {
            get { return (ItemsControl)GetValue(ItemsControlProperty); }
            set { SetValue(ItemsControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsControlProperty =
            DependencyProperty.Register("ItemsControl", typeof(ItemsControl), typeof(SearchItemsControlTextBoxBehvior), new FrameworkPropertyMetadata(default(ItemsControl), (d, e) =>
            {
                SearchItemsControlTextBoxBehvior control = d as SearchItemsControlTextBoxBehvior;

                if (control == null) return;

                if (e.OldValue is ItemsControl o)
                {

                }

                if (e.NewValue is ItemsControl n)
                {

                }
            }));



        protected override void OnAttached()
        {
            this.AssociatedObject.TextChanged += AssociatedObject_TextChanged;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
        }

        private void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            //  Do ：目前虚拟化应该会有问题 后面修改
            if (this.ItemsControl == null) return;

            string txt = this.AssociatedObject.Text?.ToUpper();

            Func<object, bool> match = item =>
            {
                if (string.IsNullOrEmpty(txt))
                    return true;

                if (item is ISearchable search)
                {
                    return search.Filter(txt);
                }

                if (typeof(IConvertible).IsAssignableFrom(item.GetType()))
                {
                    return item.ToString().ToUpper().Contains(txt);
                }

                IEnumerable<PropertyInfo> ps = item.GetType().GetProperties().Where(x => x.CanRead).Where(l => l.PropertyType == typeof(string) || l.PropertyType.IsPrimitive || l.PropertyType == typeof(DateTime) && l.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false);
                foreach (PropertyInfo property in ps)
                {
                    if (property.Name == "Item")
                        continue;
                    object v = property.GetValue(item);
                    if (v?.ToString().ToUpper().Contains(txt) == true)
                    {
                        return true;
                    }
                }
                return false;
            };

            Action<ItemsControl> fiter = null;
            fiter = x =>
            {
                foreach (var item in x.Items)
                {
                    if (item is HeaderedContentControl header)
                    {
                        header.Visibility = header.Header?.ToString().ToUpper().Contains(txt) == true ? Visibility.Visible : Visibility.Collapsed;
                    }
                    else if (item is ContentControl listBoxItem)
                    {
                        listBoxItem.Visibility = listBoxItem.Content?.ToString().ToUpper().Contains(txt) == true ? Visibility.Visible : Visibility.Collapsed;
                    }
                    else
                    {
                        var control = x.ItemContainerGenerator.ContainerFromItem(item);

                        if (control is ContentControl presenter)
                        {
                            presenter.Visibility = match(item) ? Visibility.Visible : Visibility.Collapsed;
                        }

                        if (control is HeaderedItemsControl itemsControl)
                        {
                            fiter.Invoke(itemsControl);
                            if (itemsControl.Items.OfType<Control>().All(l => l.Visibility == Visibility.Collapsed))
                            {
                                itemsControl.Visibility = match(itemsControl.Header) ? Visibility.Visible : Visibility.Collapsed;
                            }
                            else
                            {
                                itemsControl.Visibility = Visibility.Visible;
                            }
                        }

                        if (item is ISearchable searchable)
                        {
                            searchable.Filter(txt);
                        }
                    }
                }
            };

            if (this.ItemsControl.ItemsSource == null)
            {
                fiter.Invoke(this.ItemsControl);
            }
            else
            {
                int index = 0;
                foreach (object item in this.ItemsControl.ItemsSource)
                {
                    DependencyObject find = this.ItemsControl.ItemContainerGenerator.ContainerFromIndex(index);
                    if (find is UIElement tabitem)
                    {
                        tabitem.Visibility = match(item) ? Visibility.Visible : Visibility.Collapsed;
                    }

                    if (item is ISearchable searchable)
                    {
                        searchable.Filter(txt);
                    }
                    index++;
                }
            }
        }
    }


    /// <summary>
    /// This behavior selects all text in a TextBox when it gets focus
    /// </summary>
    public class SelectTextOnFocusBehavior : Behavior<TextBox>
    {
        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.GotKeyboardFocus += SelectAllText;
            AssociatedObject.GotMouseCapture += SelectAllText;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        /// Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.GotKeyboardFocus -= SelectAllText;
            AssociatedObject.GotMouseCapture -= SelectAllText;
        }

        /// <summary>
        /// This selects the text in the TextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAllText(object sender, RoutedEventArgs e)
        {
            AssociatedObject.SelectAll();
        }
    }

    /// <summary>
    /// This behavior associates a watermark onto a TextBox indicating what the user should
    /// provide as input.
    /// </summary>
    public class WatermarkTextBoxBehavior : Behavior<TextBox>
    {
        /// <summary>
        /// The watermark text
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(WatermarkTextBoxBehavior),
                                        new FrameworkPropertyMetadata(string.Empty));

        /// <summary>
        /// Readonly property used to style the TextBox when the watermark is present
        /// </summary>
        private static readonly DependencyPropertyKey IsWatermarkedPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly("IsWatermarked", typeof(bool), typeof(WatermarkTextBoxBehavior),
                                        new FrameworkPropertyMetadata(false));

        /// <summary>
        /// This readonly property is applied to the TextBox and indicates whether the watermark
        /// is currently being displayed.  It allows a style to change the visual appearanve of the
        /// TextBox.
        /// </summary>
        public static readonly DependencyProperty IsWatermarkedProperty = IsWatermarkedPropertyKey.DependencyProperty;

        /// <summary>
        /// Retrieves the current watermarked state of the TextBox.
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        public static bool GetIsWatermarked(TextBox tb)
        {
            return (bool)tb.GetValue(IsWatermarkedProperty);
        }

        /// <summary>
        /// Retrieves the current watermarked state of the TextBox.
        /// </summary>
        private bool IsWatermarked
        {
            get { return (bool)AssociatedObject.GetValue(IsWatermarkedProperty); }
            set { AssociatedObject.SetValue(IsWatermarkedPropertyKey, value); }
        }

        /// <summary>
        /// The watermark text
        /// </summary>
        [Category("Appearance")]
        public string Text
        {
            get { return (string)base.GetValue(TextProperty); }
            set { base.SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.GotFocus += OnGotFocus;
            AssociatedObject.LostFocus += OnLostFocus;
            AssociatedObject.TextChanged += OnTextChanged;

            OnLostFocus(null, null);
        }

        /// <summary>
        /// This method is called when the text for the TextBox is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isChangingText && !AssociatedObject.IsFocused)
            {
                OnLostFocus(null, null);
            }
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        /// Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.GotFocus -= OnGotFocus;
            AssociatedObject.LostFocus -= OnLostFocus;
        }

        /// <summary>
        /// This method is called when the textbox gains focus.  It removes the watermark.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (IsWatermarked)
                ChangeText(string.Empty);

            IsWatermarked = false;
        }

        /// <summary>
        /// This method is called when focus is lost from the TextBox.  It puts the watermark
        /// into place if no text is in the textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(AssociatedObject.Text))
            {
                ChangeText(this.Text);
                IsWatermarked = true;
            }
            else
                IsWatermarked = false;
        }

        private bool _isChangingText;

        /// <summary>
        /// This method is used to change the text.
        /// </summary>
        /// <param name="newText">New string to assign to TextBox</param>
        private void ChangeText(string newText)
        {
            _isChangingText = true;
            AssociatedObject.Text = newText;
            _isChangingText = false;
        }
    }


    public class TextBoxChangedSelectAllBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.TextChanged += AssociatedObject_TextChanged;
        }

        private void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.AssociatedObject.SelectAll();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextChanged += AssociatedObject_TextChanged;
        }
    }
}