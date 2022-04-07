// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.Generic;

namespace System
{
    public interface IInterop
    {
        bool Add(object t);
        bool Delete(Array ts);
        IEnumerable<object> GetAll(Type type, Predicate<object> predicate = null);
        bool Save(object t);
    }

    public class Interop : IInterop
    {
        public bool Add(object o)
        {
            Diagnostics.Debug.WriteLine(o.GetType().Name);
            return true;
        }

        public bool Save(object o)
        {
            Diagnostics.Debug.WriteLine(o.GetType().Name);
            return true;
        }

        public bool Delete(Array ts)
        {
            foreach (object o in ts)
            {
                Diagnostics.Debug.WriteLine(o.GetType().Name);
            }

            return true;
        }

        public IEnumerable<object> GetAll(Type type, Predicate<object> predicate = null)
        {
            Diagnostics.Debug.WriteLine(type.Name);
            return null;
        }
    }

}
