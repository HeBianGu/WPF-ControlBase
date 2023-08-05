// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows.Input;

namespace HeBianGu.Base.WpfBase
{
    public interface ISystemSettingServiceOpation
    {
        void Add(params ISetting[] settings);
    }

    public interface ISystemSettingService : ISave, ILoad, ISystemSettingServiceOpation
    {
        void SetDefault();
        void Cancel();
    }

    public class SystemSetting : ServiceInstance<ISystemSettingService>
    {
        public const string GroupBase = "基本设置";
        public const string GroupApp = "应用设置";
        public const string GroupStyle = "样式设置";
        public const string GroupSecurity = "安全设置";
        public const string GroupAuthority = "权限设置";
        public const string GroupSystem = "系统设置";
        public const string GroupControl = "控件设置";
        public const string GroupMessage = "消息设置";
        public const string GroupData = "数据设置";
        public const string GroupOther = "其他设置";
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