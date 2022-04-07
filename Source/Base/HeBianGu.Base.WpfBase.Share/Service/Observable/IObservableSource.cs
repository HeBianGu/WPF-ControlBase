// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Base.WpfBase
{
    public interface IObservableSource
    {
        int MaxValue { get; set; }
        int MinValue { get; set; }
        int PageCount { get; set; }
        int PageIndex { get; set; }
        int Total { get; set; }
        int TotalPage { get; set; }
    }
}