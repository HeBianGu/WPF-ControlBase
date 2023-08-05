// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    public interface IPrintBoxMessage
    {
        Task<bool> Show(params IPrintPagePresenter[] data);
        Task<bool> PrintTable<T>(IEnumerable<T> source, string title = "标题");
    }

    public class Printer : ServiceInstance<IPrintBoxMessage>
    {

    }

    public interface IPrintPagePresenter
    {
        void Refresh();
    }

}
