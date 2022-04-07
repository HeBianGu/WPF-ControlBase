// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Control.Filter
{
    public interface IFilter
    {
        bool IsMatch(object obj);

        IFilter Copy();
    }
}

