using System;
using System.Windows;
using System.Windows.Input;

namespace HeBianGu.Base.WpfBase
{
    public class CollapsedOfOpacityCommand : ICommand
    {

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            if(parameter is UIElement element)
            {
                var engine = DoubleStoryboardEngine.Create(1, 0, 1, "Opacity");

                engine.Start(element);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}