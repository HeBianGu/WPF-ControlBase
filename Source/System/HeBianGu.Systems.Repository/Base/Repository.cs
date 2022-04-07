// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace HeBianGu.Systems.Repository
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private ObservableCollection<T> list = new ObservableCollection<T>();

        public Repository()
        {
            for (int i = 0; i < 20; i++)
            {
                T t = new T();
                list.Add(t);
            }
        }
        public virtual bool Add(T t)
        {
            return true;
        }

        public virtual bool Delete(params T[] ts)
        {
            return true;
        }

        public virtual bool DeleteAll(Predicate<T> predicate = null)
        {
            return true;
        }

        public virtual IEnumerable<T> GetAll(Predicate<T> predicate = null)
        {
            Thread.Sleep(1000);
            return list;
        }

        public virtual bool Save(T t)
        {
            return true;
        }
    }
}
