namespace HeBianGu.Application.MapWindow
{
    public interface IAssemblyDomain
    {
        LoginViewModel GetAccountConfig();

        bool Login(string userName, string psd, bool IsSavePSD, out string err);
    }
}