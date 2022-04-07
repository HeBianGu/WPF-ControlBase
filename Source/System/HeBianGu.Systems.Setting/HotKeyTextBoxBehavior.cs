// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.Systems.Setting
{
    public class HotKeyTextBoxBehavior : Behavior<TextBox>
    {
        /// <summary>
        /// This attaches the property handlers - PreviewKeyDown, Clipboard support and our adorner.
        /// </summary>
        protected override void OnAttached()
        {
            AssociatedObject.PreviewKeyDown += OnKeyDown;

        }

        /// <summary>
        /// This removes all our handlers.
        /// </summary>
        protected override void OnDetaching()
        {
            AssociatedObject.PreviewKeyDown -= OnKeyDown;

        }

        private List<Key> _keys = new List<Key>();

        /// <summary>
        /// This checks the PreviewKeyDown on the TextBox and constrains it to a numeric value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
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

            if (e.Key >= Key.A && e.Key <= Key.Z)
            {
                _keys.Add(e.Key);
            }
            else
            {
                _keys.Clear();
            }

            StringBuilder sb = new StringBuilder();

            List<string> vs = new List<string>();

            if (Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                vs.Add("Ctrl");
            }

            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                vs.Add(ModifierKeys.Shift.ToString());
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt))
            {
                vs.Add(ModifierKeys.Alt.ToString());
            }

            IEnumerable<Key> finds = _keys.Skip(_keys.Count - 2);

            vs.AddRange(finds.Select(l => l.ToString()));

            sb.Append(string.Join("+", vs));

            this.AssociatedObject.Text = sb.ToString();
        }
    }
}