// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;

namespace HeBianGu.Systems.Setting
{
    /// <summary> 文件管理 </summary>
    [Displayer(Name = "文件管理", GroupName = SystemSetting.GroupBase)]
    public class FileSetting : Setting<FileSetting>
    {
    }

    [Displayer(Name = "密码", GroupName = SystemSetting.GroupSecurity)]
    public class PasswordSetting : Setting<PasswordSetting>
    {

    }

    [Displayer(Name = "消息记录", GroupName = SystemSetting.GroupMessage)]
    public class MessageSetting : Setting<MessageSetting>
    {

    }


    [Displayer(Name = "个人资料", GroupName =SystemSetting.GroupSecurity)]
    public class PersonalSetting : Setting<PersonalSetting>
    {

    }
}
