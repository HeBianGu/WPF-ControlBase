namespace HeBianGu.Base.WpfBase
{
    public interface IProjectService
    {
        void Load();

        bool Save(out string message);
    }
}