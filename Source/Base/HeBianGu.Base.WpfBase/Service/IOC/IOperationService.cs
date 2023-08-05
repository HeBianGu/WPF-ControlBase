// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.ObjectModel;

namespace HeBianGu.Base.WpfBase
{
    //public interface IOperationService
    //{
    //    ObservableCollection<IOperation> Operations { get; }

    //    void Log(string result, string message, [System.Runtime.CompilerServices.CallerMemberName] string Operation = null);
    //}

    //public class Operationner : ServiceInstance<IOperationService>
    //{

    //}

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class OperationAttribute : Attribute
    {
        public string ID { get; }

        public OperationAttribute(string guid)
        {
            this.ID = guid;
        }

        public string Name { get; set; }
    }

    //public interface IOperation
    //{
    //    bool IsChecked { get; set; }
    //}

}