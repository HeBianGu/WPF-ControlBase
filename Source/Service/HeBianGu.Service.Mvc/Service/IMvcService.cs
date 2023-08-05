// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HeBianGu.Service.Mvc
{
    public interface IMvcService
    {
        Task<IActionResult> CreateActionResult(LinkAction linkAction);
        Task DoActionResult(LinkAction linkAction);
        object GetActionResult(IController controller, string action, object[] args = null);
        IActionResult GetActionResult(string controlName, string ActionName);
        IController GetController(LinkAction linkAction);
        LinkActionGroups GetLinkActionGroups();
        List<IAction> GetLinkActions();
        Type GetOfType(Predicate<Type> match);
        string GetPath(string controlName, string ActionName);
        Uri GetUri(string controlName, string ActionName);
        Control GetView(string controlName, string ActionName);
        object GetViewModel(string controlName);
        void Init();
        void RegisterMvc<TClass>() where TClass : class;
        Stack<ILinkAction> History { get; }
    }

    public class MvcProxy : ServiceInstance<IMvcService>
    {

    }
}