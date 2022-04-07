namespace HeBianGu.App.Touch
{
    public interface IAssemblyDomain
    {
        void GoToLinkAction(string controller, string action, object[] parameter);

        void GoToLinkAction(string controller, string action);

        /// <summary> 判断是否是身份证号 </summary>
        bool IsCardID(string id);

        void StartMonitor();

    }
}