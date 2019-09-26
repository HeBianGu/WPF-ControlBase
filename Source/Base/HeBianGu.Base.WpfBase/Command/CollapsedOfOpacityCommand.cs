using System;
using System.Windows;
using System.Windows.Input;

namespace HeBianGu.Base.WpfBase
{
    public class CollapsedOfOpacityCommand : ICommand
    {  
        public bool CanExecute(object parameter)
        { 
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is UIElement element)
            {
                var engine = DoubleStoryboardEngine.Create(1, 0, 0.4, "Opacity");

                engine.CompletedEvent += (l, k) =>
              {
                  element.Visibility = Visibility.Collapsed;
              };


                engine.Start(element);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}