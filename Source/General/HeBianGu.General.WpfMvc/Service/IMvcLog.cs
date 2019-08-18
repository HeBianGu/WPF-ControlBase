namespace HeBianGu.General.WpfMvc
{
    public interface IMvcLog
    {
        void ErrorLog(string title, string message);
        void RunLog(string title, string message);

        void OutPutLog(string title, string message);
        
    }
}