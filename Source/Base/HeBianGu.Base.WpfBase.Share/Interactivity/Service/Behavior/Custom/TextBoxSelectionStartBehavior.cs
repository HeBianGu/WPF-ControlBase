// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;


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
            TextBox parent = sender as TextBox;

            if (parent != null)
            {
                parent.Focus();

                parent.SelectionStart = 200;

            }


        }
    }
}