// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace HeBianGu.Service.Converter
{
    public abstract class Convert<T> : ConvertBase where T : new()
    {
        private static readonly Lazy<T> InstanceConstructor = new Lazy<T>(() => new T(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        public static T Instance => InstanceConstructor.Value;
    }
}
