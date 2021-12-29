namespace HeBianGu.Base.WpfBase
{
    public interface ISettingPathService
    {
        string GetSetting();

        string GetConfigExtention();
    }

    public class SettingPath : ServiceInstance<ISettingPathService>
    {

    }
}