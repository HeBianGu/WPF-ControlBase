// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.MessageListBox;

namespace System
{
    public static class MessageListBoxExtention
    {


        /// <summary>
        /// 设置系统路径
        /// </summary>  
        public static IApplicationBuilder UseMessageListBox(this IApplicationBuilder builder, Action<MessageListBoxConfig> systemPath = null)
        {
            systemPath?.Invoke(MessageListBoxConfig.Instance);

            return builder;
        }


    }


}
