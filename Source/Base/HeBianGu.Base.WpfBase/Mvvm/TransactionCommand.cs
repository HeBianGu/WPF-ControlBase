// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Threading;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    public interface ITransactionCommand
    {
        Task<bool> BeginEditAsync(Func<bool> canCommit = null, int millisecondsTimeout = 60000);
    }


    public class TransactionCommand : RelayCommand, ITransactionCommand
    {

        protected Func<bool> _canCommit;

        public TransactionCommand(Action<object> action) : base(action)
        {

        }

        public TransactionCommand(Action<ITransactionCommand, object> action) : base((s, e) => action(s as ITransactionCommand, e))
        {

        }
        public TransactionCommand(Action<object> execute, Predicate<object> canExecute) : base(execute, canExecute)
        {

        }

        public TransactionCommand(Action<ITransactionCommand, object> action, Func<ITransactionCommand, object, bool> canAction) 
            : base((s, e) => action(s as ITransactionCommand, e), (s, e) => canAction(s as ITransactionCommand, e))
        {

        }

        private bool _isEditting;
        /// <summary> 说明  </summary>
        public bool IsEditting
        {
            get { return _isEditting; }
            set
            {
                _isEditting = value;
                RaisePropertyChanged();
            }
        }


        //public async Task<bool> BeginEdit(Action action = null)
        //{
        //    return await Task.Run(() =>
        //    {
        //        this.IsEditing = true;
        //        action?.Invoke();
        //        waitHandle.WaitOne();
        //        return _result;
        //    });
        //}


        public async Task<bool> BeginEditAsync(Func<bool> canCommit = null, int millisecondsTimeout = 60 * 1000)
        {
            this._canCommit = canCommit;
            return await Task.Run(() =>
            {
                this.IsEditting = true;
                bool r = waitHandle.WaitOne(millisecondsTimeout);
                this.IsEditting = false;
                return _result && r;
            });
        }

        bool _result;
        AutoResetEvent waitHandle = new AutoResetEvent(false);
        public InvokeCommand CommitCommand => new InvokeCommand(e =>
         {
             if (_canCommit?.Invoke() == false) return;
             this._result = true;
             waitHandle.Set();
         }, e => this.IsEditting);
        public InvokeCommand RollbackCommand => new InvokeCommand(e =>
        {
            this._result = false;
            waitHandle.Set();
        }, e => this.IsEditting);
    }
}
