// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Systems.Repository
{
    public class RepositoryAction : ActionBase
    {

        public Type ModelType { get; set; }

        public override async Task<IActionResult> GetActionResult()
        {
            if (RepositorySetting.Instance.ViewType == ViewType.RepositoryView)
            {
                return await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    ActionResult result = new ActionResult();

                    Type rpType = typeof(IRepositoryViewModel<>).MakeGenericType(this.ModelType);
                    Type vmType = typeof(RepositoryViewModel<>).MakeGenericType(this.ModelType);
                    ServiceRegistry.Instance.Register(rpType, vmType);
                    object vm = ServiceRegistry.Instance.GetInstance(rpType);
                    result.ViewModel = vm;
                    result.View = new RepositoryView() { DataContext = vm };
                    return result;
                });
            }
            else
            {
                return await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    ActionResult result = new ActionResult();
                    result.View = new RespositoryBox() { ModelType = this.ModelType };

                    return result;
                });
            }
        }
    }
}
