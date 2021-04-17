using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// Selects style to apply to a <see cref="DragablzItem"/> according to the tab item content itself.
    /// </summary>
    public class TabablzItemStyleSelector : StyleSelector
    {
        private readonly Style _defaultHeaderItemStyle;
        private readonly Style _customHeaderItemStyle;

        public TabablzItemStyleSelector(Style defaultHeaderItemStyle, Style customHeaderItemStyle)
        {
            _defaultHeaderItemStyle = defaultHeaderItemStyle;
            _customHeaderItemStyle = customHeaderItemStyle;
        }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is FTabItem) return _defaultHeaderItemStyle;

            return _customHeaderItemStyle;
        }
    }
}