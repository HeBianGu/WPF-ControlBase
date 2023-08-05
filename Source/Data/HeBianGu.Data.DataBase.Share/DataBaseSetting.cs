using HeBianGu.Base.WpfBase;
using HeBianGu.Data.DataBase;
using HeBianGu.Service.AppConfig;
#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
#endif
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace HeBianGu.Data.DataBase
{
    //public interface ISqlServerOption
    //{
    //    string InitialCatalog { get; set; }
    //    string Password { get; set; }
    //    string Server { get; set; }
    //    string UserName { get; set; }
    //    string ConfigPath { get; set; }
    //}

    public abstract class DataBaseSetting<T> : LazySettingInstance<T>, IDataBaseSetting where T : new()
    {
        public abstract string GetConnect();

        private string _message;
        [XmlIgnore]
        [Browsable(false)]
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }


        //protected abstract bool Connect();
        [XmlIgnore]
        [Browsable(false)]
        [Display(Name = "测试连接")]
        public RelayCommand ConnectCommand => new RelayCommand((s, e) =>
        {
            Task.Run(() =>
            {
                this.Message = "正在连接...";
                s.IsBusy = true;
                s.Message = this.Message;

                var init = ServiceRegistry.Instance.GetInstance<IDataBaseInitService>();
                var r = init.TryConnect(out string message);
                this.Message = message;
                s.IsBusy = false;
                CommandManager.InvalidateRequerySuggested();
                //try
                //{
                //    this.Connect();
                //}
                //catch (System.Exception ex)
                //{
                //    this.Message = "连接失败：" + ex.Message;
                //    Logger.Instance?.Error(ex);
                //}
                //finally
                //{
                //    s.IsBusy = false;
                //    CommandManager.InvalidateRequerySuggested();
                //}
            });

            //services.AddSingleton(abcDataContext);
        });

        [XmlIgnore]
        [Browsable(false)]
        [Display(Name = "保存配置")]
        public RelayCommand SaveCommand => new RelayCommand((s, e) =>
        {
            var b = this.Save(out string message);
            this.Message = b ? "保存成功" : "保存失败" + message;
        });
    }

}
