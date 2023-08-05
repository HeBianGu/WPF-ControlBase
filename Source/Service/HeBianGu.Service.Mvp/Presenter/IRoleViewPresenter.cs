// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;

namespace HeBianGu.Service.Mvp
{
    public interface IRoleViewPresenter : ITreeViewItemPresenter
    {
        IEnumerable<IRole> GetRoles(Predicate<IRole> predicate = null);
    }

    public class RoleViewPresenterProxy : ServiceInstance<IRoleViewPresenter>
    {

    }
}
