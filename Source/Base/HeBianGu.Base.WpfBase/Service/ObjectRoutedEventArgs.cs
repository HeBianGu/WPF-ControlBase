// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Base.WpfBase
{

    public class ObjectRoutedEventArgs<T> : RoutedEventArgs
    {
        public ObjectRoutedEventArgs(T entity)
        {
            Entity = entity;
        }

        public ObjectRoutedEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }
        public ObjectRoutedEventArgs(RoutedEvent routedEvent, object source, T entity) : base(routedEvent, source)
        {
            Entity = entity;
        }


        public T Entity { get; set; }
    }
}
