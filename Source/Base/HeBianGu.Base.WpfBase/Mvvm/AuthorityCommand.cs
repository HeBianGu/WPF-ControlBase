// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace HeBianGu.Base.WpfBase
{
    public class AuthorityCommand : RelayCommand, IAuthority
    {
        public AuthorityCommand(Action<object> action, string id, string name) : base(action)
        {
            this.ID = id;
            this.Name = name;
        }

        public AuthorityCommand(Action<AuthorityCommand, object> action, string id, string name) 
            : base((s, e) => action(s as AuthorityCommand, e))
        {
            this.ID = id;
            this.Name = name;
        }

        public AuthorityCommand(Action<object> execute, Predicate<object> canExecute, string id, string name) : base(execute, canExecute)
        {
            this.ID = id;
            this.Name = name;
        }

        public AuthorityCommand(Action<AuthorityCommand, object> execute, Func<AuthorityCommand, object, bool> canExecute, string id, string name)
                        : base((s, e) => execute(s as AuthorityCommand, e), (s, e) => canExecute(s as AuthorityCommand, e))

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
