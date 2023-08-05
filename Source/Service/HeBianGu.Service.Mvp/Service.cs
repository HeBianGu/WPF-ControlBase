// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.Generic;
using System.Linq;

namespace HeBianGu.Service.Mvp
{
    public class PresenterService : IPresenterService
    {
        public List<IRepositoryPresenter> GetRepositoryPresenters()
        {
            IEnumerable<IRepositoryPresenter> find = ServiceRegistry.Instance.GetAllAssignableFrom<IRepositoryPresenter>();

            return find?.ToList();
        }
    }

    public class PresenterPresenter : ServiceInstance<IPresenterService>
    {

    }

    public interface IPresenterService
    {
        List<IRepositoryPresenter> GetRepositoryPresenters();
    }
}
