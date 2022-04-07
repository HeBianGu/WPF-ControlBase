// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.Generic;

namespace HeBianGu.Service.Mvp
{
    public interface IPresenterService
    {
        List<IRepositoryPresenter> GetRepositoryPresenters();
    }
}