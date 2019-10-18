namespace HeBianGu.General.WpfMvc
{
    /// <summary> 用于注入日志的接口 </summary>
    public interface IMvcLog
    {
        void ErrorLog(string title, string message);

        void RunLog(string title, string message);

        void OutPutLog(string title, string message);
        
    }
}