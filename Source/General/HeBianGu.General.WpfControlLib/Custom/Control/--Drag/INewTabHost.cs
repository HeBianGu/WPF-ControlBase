using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public interface INewTabHost<out TElement> where TElement : UIElement
    {
        TElement Container { get; }
        TabablzControl TabablzControl { get; }
    }
}