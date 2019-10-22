using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace HeBianGu.Base.WpfBase
{
    /// <summary> 但点击当前控件时ListBoxItem项值也选中 </summary>
    public class TextBoxSelectionStartBehavior : Behavior<FrameworkElement>
    {

        protected override void OnAttached()
        {
            AssociatedObject.AddHandler(TextBox.TextChangedEvent,
                new TextChangedEventHandler(OnTextChangedEvent), false);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.RemoveHandler(TextBox.TextChangedEvent
                , new TextChangedEventHandler(OnTextChangedEvent));
        }



        private void OnTextChangedEvent(object sender, TextChangedEventArgs e)
        {
            var parent = sender as TextBox;

            if(parent!=null)
            {
                parent.Focus();

                parent.SelectionStart = 200;

            }


        }
    }
}