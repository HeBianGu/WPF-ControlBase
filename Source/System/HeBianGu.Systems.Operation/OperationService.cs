// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.ObjectModel;

namespace HeBianGu.Systems.Operation
{
    internal class OperationService : NotifyPropertyChangedBase, IOperationService
    {
        private ObservableCollection<IOperation> _operations = new ObservableCollection<IOperation>();
        /// <summary> 说明  </summary>
        public ObservableCollection<IOperation> Operations
        {
            get { return _operations; }
            set
            {
                _operations = value;
                RaisePropertyChanged();
            }
        }

        private IIdentityService Identity => ServiceRegistry.Instance.GetInstance<IIdentityService>();

        public void Log(string result, string message, [System.Runtime.CompilerServices.CallerMemberName] string Operation = null)
        {
            Operation operation = new Operation();

            operation.User = Identity?.User;
            operation.OperationType = Operation;
            operation.Result = result;
            operation.Message = message;

            this.Operations.Add(operation);
        }

    }

}
