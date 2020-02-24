using System.Windows;
using System.Windows.Media.Animation;

namespace HeBianGu.General.WpfControlLib
{
    public interface ITransitionEffect
    {
        Timeline Build<TSubject>(TSubject effectSubject) where TSubject : FrameworkElement, ITransitionEffectSubject;
    }
}