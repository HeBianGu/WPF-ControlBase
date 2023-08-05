// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace HeBianGu.Base.WpfBase
{
    public class AuthorityTransactionCommand : TransactionCommand, IAuthority
    {
        public AuthorityTransactionCommand(Action<object> action, string id, string name) : base(action)
        {
            this.ID = id;
            this.Name = name;
        }

        public AuthorityTransactionCommand(Action<AuthorityTransactionCommand, object> action, string id, string name)
            : base((s, e) => action(s as AuthorityTransactionCommand, e))
        {
            this.ID = id;
            this.Name = name;
        }

        public AuthorityTransactionCommand(Action<object> execute, Predicate<object> canExecute, string id, string name) : base(execute, canExecute)
        {
            this.ID = id;
            this.Name = name;
        }

        public AuthorityTransactionCommand(Action<AuthorityTransactionCommand, object> execute, Func<AuthorityTransactionCommand, object, bool> canExecute, string id, string name)
            : base((s, e) => execute(s as AuthorityTransactionCommand, e), (s, e) => canExecute(s as AuthorityTransactionCommand, e))
        {
            this.ID = id;
            this.Name = name;
        }

        public string ID { get; }
        public string Name { get; }
        public string GroupName { get; set; }
        public bool IsAuthority => Loginner.Instance?.User?.IsValid(this.ID) != false;
    }
}
