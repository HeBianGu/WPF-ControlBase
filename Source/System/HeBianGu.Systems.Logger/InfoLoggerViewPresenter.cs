//Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Systems.Repository;

namespace HeBianGu.Systems.Logger
{
    public interface IDebugRepositoryViewPresenter : IRepositoryViewPresenter
    {

    }

    //public interface IDebugLoggerViewPresenterOption : IRepositoryPresenterOption
    //{

    //}
    [Displayer(Name = "操作日志", GroupName = "日志管理", Icon = "\xe6e7", Description = "应用此功能关系系统操作日志")]
    public class DebugRepositoryViewPresenter : RepositoryViewPresenter<DebugRepositoryViewPresenter, IDebugRepositoryViewPresenter>, IDebugRepositoryViewPresenter
    {
        public DebugRepositoryViewPresenter()
        {
            this.ViewModel = new RepositoryViewModel<hl_dm_debug>();
        }
    }
}
