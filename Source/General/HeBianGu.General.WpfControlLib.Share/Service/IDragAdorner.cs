using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public interface IDragAdorner
    {
        Point Offset { get; set; }

        void UpdatePosition(Point location);
    }
}