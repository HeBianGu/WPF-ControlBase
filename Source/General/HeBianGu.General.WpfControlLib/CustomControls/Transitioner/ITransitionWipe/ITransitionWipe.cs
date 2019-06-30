using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary> 过度效果 </summary>
    public interface ITransitionWipe
    {
        void Wipe(TransitionerSlide fromSlide, TransitionerSlide toSlide, Point origin, IZIndexController zIndexController);
    }
}