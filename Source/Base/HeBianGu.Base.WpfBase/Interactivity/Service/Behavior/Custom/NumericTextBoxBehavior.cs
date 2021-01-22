using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input; 
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{
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
                var behColl = Interaction.GetBehaviors(tb);
                var existingBehavior = behColl.FirstOrDefault(b => b.GetType() == typeof(NumericTextBoxBehavior)) as NumericTextBoxBehavior;
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
            DependencyProperty.Register("Minimum", typeof (long), typeof (NumericTextBoxBehavior), new PropertyMetadata(Int64.MinValue));

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
            Point _lastPos;
            bool _isCaptured;
            bool _changedValue;

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
        void PreviewLostFocus(object sender, KeyboardFocusChangedEventArgs e)
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
}