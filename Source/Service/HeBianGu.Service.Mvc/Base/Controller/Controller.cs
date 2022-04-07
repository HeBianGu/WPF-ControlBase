// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace HeBianGu.Service.Mvc
{
    /// <summary> 带有ViewMode泛型的控制器 </summary>
    public abstract class Controller<M> : Controller where M : MvcViewModelBase
    {
        public M ViewModel { get; set; } = ServiceRegistry.Instance.GetInstance<M>();

        ///// <summary> 验证实体模型是否可用 </summary>
        //public bool ModelState(object obj, out string message)
        //{
        //    var result = ObjectPropertyFactory.ModelState(obj, out List<string> errors);

        //    message = errors.FirstOrDefault();

        //    return result;
        //}

        ///// <summary> 验证实体模型是否可用 </summary>
        //public bool ModelState(object obj)
        //{
        //    string message;

        //    return this.ModelState(obj, out message);
        //}
    }

    /// <summary> 带有Dispatcher的控制器 </summary>
    public abstract class Controller : ControllerBase
    {
        public Dispatcher Dispatcher
        {
            get
            {
                return Application.Current.Dispatcher;
            }
        }

        /// <summary> 应用主线程运行 </summary>
        public T Invoke<T>(Func<T> func)
        {
            return this.Dispatcher.Invoke(func);
        }

        /// <summary> 应用主线程运行 </summary>
        public void Invoke(Action action)
        {
            this.Dispatcher.Invoke(action);
        }

        /// <summary> 系统空闲时运行 </summary>
        public void BeginInvoke(Action action)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, action);
        }

        /// <summary> 异步运行 </summary>
        public void RunAsync(Action action)
        {
            Task.Run(action);
        }

        private Dictionary<string, bool> _isInitailized = new Dictionary<string, bool>();
        /// <summary> 只运行一次的方法 </summary>
        public void RunOnlyInitailizing(Action action, [CallerMemberName] string name = "")
        {
            if (!_isInitailized.ContainsKey(name))
            {
                _isInitailized.Add(name, false);
            }

            bool isInitailized = _isInitailized[name];

            if (!isInitailized)
            {
                action?.Invoke();

                _isInitailized[name] = true;
            }
        }
    }








}
