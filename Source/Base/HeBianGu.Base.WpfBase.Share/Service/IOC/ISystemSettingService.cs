// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows.Input;

namespace HeBianGu.Base.WpfBase
{
    public interface ISystemSettingService
    {
        void Load();

        bool Save(out string message);

        void SetDefault();

        void Cancel();

        void Add(params ISetting[] settings);
    }

    public class SystemSetting : ServiceInstance<ISystemSettingService>
    {

    }

    public class HotKeyAttribute : Attribute
    {
        public ModifierKeys ModifierKeys { get; set; }

        public Key Key { get; set; }

        public string DisplayName { get; set; }

        public override string ToString()
        {
            if (this.ModifierKeys == ModifierKeys.None)
                return this.Key.ToString();

            return $"{this.ModifierKeys}+{this.Key}";
        }

    }


}