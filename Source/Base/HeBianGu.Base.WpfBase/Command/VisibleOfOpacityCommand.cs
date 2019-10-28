using System;
using System.Windows;
using System.Windows.Input;

namespace HeBianGu.Base.WpfBase
{
    public class VisibleOfOpacityCommand : ICommand
    {  
        public bool CanExecute(object parameter)
        { 
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is UIElement element)
            {
                var engine = DoubleStoryboardEngine.Create(0, 1, 0.4, "Opacity");

                engine.CompletedEvent += (l, k) =>
              {
                  element.Visibility = Visibility.Visible;
              };


                engine.Start(element);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}