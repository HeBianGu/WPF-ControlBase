
using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;

namespace HeBianGu.Systems.Encryption
{
    public class DESCryptService : ICryptService
    {
        public string Decrypt(string value)
        {
            return DESExtension.DecryptDES(value);
        }

        public string Encrypt(string value)
        {
            return DESExtension.EncryptDES(value);
        }
    }



    //[SettingConfig(Name = "参数设置", Group = "基本设置")]
    //public class Setting : LazySettingInstance<Setting>
    //{

    //}


}
