// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Base.WpfBase
{
    public interface ISystemNotifyMessage
    {
        void Show(string message, string title = null, NotifyBalloonIcon tipIcon = NotifyBalloonIcon.Info, int timeout = 1000);
    }

    /// <summary>
    ///     Defines a set of standardized icons that can be associated with a balloon tip.
    /// </summary>
    public enum NotifyBalloonIcon
    {
        /// <summary>
        ///     No icon.
        /// </summary>
        None,

        /// <summary>
        ///     An information icon.
        /// </summary>
        Info,

        /// <summary>
        ///     A warning icon.
        /// </summary>
        Warning,

        /// <summary>
        ///     An error icon.
        /// </summary>
        Error,
    }
}
