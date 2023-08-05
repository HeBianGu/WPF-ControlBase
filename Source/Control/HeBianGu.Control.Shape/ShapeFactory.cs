// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq;
using System;
using HeBianGu.Base.WpfBase;

namespace HeBianGu.Control.Shape
{
    public static class ShapeFactory
    { 
        public static ObservableCollection<ShapeBase> AllShape
        {
            get
            {
                var types = Assembly.GetExecutingAssembly().GetTypes().Where(l => typeof(ShapeBase).IsAssignableFrom(l) && !l.IsAbstract);

                return types.Select(x => Activator.CreateInstance(x)).OfType<ShapeBase>().ToObservable();
            }
        }

    }
}
