// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;

namespace HeBianGu.Systems.Setting
{
    /// <summary> 文件管理 </summary>
    [SettingConfig(Name = "文件管理", Group = "基本设置")]
    public class FileSetting : Setting<FileSetting>
    {
    }

    [SettingConfig(Name = "密码", Group = "安全设置")]
    public class PasswordSetting : Setting<PasswordSetting>
    {

    }

    [SettingConfig(Name = "消息记录", Group = "安全设置")]
    public class MessageSetting : Setting<MessageSetting>
    {

    }


    [SettingConfig(Name = "个人资料", Group = "权限设置")]
    public class PersonalSetting : Setting<PersonalSetting>
    {

    }
}
