namespace HeBianGu.Base.WpfBase
{
    public interface IUpgradeInitService
    {
        bool Init(VersionData data, out string error);
    }
}