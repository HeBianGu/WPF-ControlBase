// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase
using HeBianGu.Base.WpfBase;
using System.Runtime.CompilerServices;

namespace HeBianGu.Service.Mvp
{
    public interface IOparationViewPresenter : ITreeViewItemPresenter
    {
        void Log(string title, string message = null, [CallerMemberName] string type = null, bool result = true, OperationType operationType = OperationType.Default);

    }

    public class OparationViewPresenterProxy : ServiceInstance<IOparationViewPresenter>
    {

    }

    public enum OperationType
    {
        Default = 0, Add, Update, Delete, Search
    }
}
