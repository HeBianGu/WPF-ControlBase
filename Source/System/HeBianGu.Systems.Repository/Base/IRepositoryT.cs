// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;

namespace HeBianGu.Systems.Repository
{
    public interface IRepository<T> : IRepository
    {
        bool Add(T t);

        bool Save(T t);

        bool Delete(params T[] ts);

        IEnumerable<T> GetAll(Predicate<T> predicate = null);

        bool DeleteAll(Predicate<T> predicate = null);
    }
}
