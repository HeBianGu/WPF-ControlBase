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
using Microsoft.EntityFrameworkCore.Sqlite;
#endif
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using System;

namespace HeBianGu.DataBase.Sqlite
{
    public interface ISqliteOption
    {
        string InitialCatalog { get; set; }
        string FilePath { get; set; }
        string ConfigPath { get; set; }
    }

    public abstract class SqlServerSetting<TSetting, TDbContext> : DataBaseSetting<TSetting>, ISqliteOption where TSetting : new() where TDbContext : DbContext
    {
        protected override string GetDefaultPath()
        {
            return this.ConfigPath;
        }

        public override void LoadDefault()
        {
            base.LoadDefault();
            this.ConfigPath = Path.Combine(SystemPathSetting.Instance.Config, this.GetType().Name + SystemPathSetting.Instance.ConfigExtention);
            this.FilePath = SystemPathSetting.Instance.Data;
        }

        private string _configPath;
        [ReadOnly(true)]
        [Browsable(false)]
        [Display(Name = "数据库配置文件路径", Description = "表示数据库连接字符串的配置文件存放的位置")]
        public string ConfigPath
        {
            get { return _configPath; }
            set
            {
                _configPath = value;
                RaisePropertyChanged();
            }
        }

        private string _filePath;
        [Display(Name = "数据库文件夹路径", Description = "数据库保存的文件夹路径")]
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                RaisePropertyChanged();
            }
        }

        private string _initialCatalog;
        [DefaultValue("data.db")]
        [Display(Name = "数据库名称", Description = "数据库文件的名称")]
        public string InitialCatalog
        {
            get { return _initialCatalog; }
            set
            {
                _initialCatalog = value;
                RaisePropertyChanged();
            }
        }

        public override string GetConnect()
        {
            if (string.IsNullOrEmpty(this.FilePath))
                return $"Data Source={this.InitialCatalog}";
            return $"Data Source={Path.Combine(this.FilePath, this.InitialCatalog)}";
        }

//        protected override void Connect()
//        {
//            //            string connect = this.GetConnect();
//            //#if NETCOREAPP
//            //            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
//            //            optionsBuilder.UseSqlite(connect);
//            //            DataContext abcDataContext = new DataContext(optionsBuilder.Options);
//            //            //abcDataContext.Database.EnsureCreated();
//            //            abcDataContext.Database.Migrate();
//            //            var r = abcDataContext.Database.CanConnect();
//            //            abcDataContext.Dispose();
//            //            this.Message = r ? "连接成功" : "连接失败";
//            //            CommandManager.InvalidateRequerySuggested();
//            //#endif

//            string connect = this.GetConnect();
//#if NETCOREAPP
//            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
//            optionsBuilder.UseSqlite(connect);
//            //DataContext abcDataContext = new DataContext(optionsBuilder.Options);
//            //abcDataContext.Database.EnsureCreated();
//            var abcDataContext = Activator.CreateInstance(typeof(TDbContext), new object[] { optionsBuilder.Options }) as TDbContext;

//            abcDataContext.Database.Migrate();
//            var r = abcDataContext.Database.CanConnect();
//            abcDataContext.Dispose();
//            this.Message = r ? "连接成功" : "连接失败";
//            CommandManager.InvalidateRequerySuggested();
//#endif
//        }
    }


    [Displayer(Name = "数据库配置", GroupName = SystemSetting.GroupApp)]
    public class SqliteSetting : SqlServerSetting<SqliteSetting, DataContext>, ISqliteOption, IDataBaseSetting
    {

        //        [XmlIgnore]
        //        [Browsable(false)]
        //        [Display(Name = "测试连接")]
        //        public RelayCommand ConnectCommand => new RelayCommand((s, e) =>
        //        {
        //            Task.Run(() =>
        //            {
        //                this.Message = "正在连接...";
        //                s.IsBusy = true;
        //                s.Message = this.Message;
        //                try
        //                {
        //                    string connect = this.GetConnect();
        //#if NETCOREAPP
        //                    var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        //                    optionsBuilder.UseSqlite(connect);
        //                    DataContext abcDataContext = new DataContext(optionsBuilder.Options);
        //                    //abcDataContext.Database.EnsureCreated();
        //                    abcDataContext.Database.Migrate();
        //                    var r = abcDataContext.Database.CanConnect();
        //                    abcDataContext.Dispose();
        //                    this.Message = r ? "连接成功" : "连接失败";
        //                    CommandManager.InvalidateRequerySuggested();
        //#endif


        //                }
        //                catch (System.Exception ex)
        //                {
        //                    this.Message = "连接失败：" + ex.Message;
        //                    Logger.Instance?.Error(ex);
        //                }
        //                finally
        //                {
        //                    s.IsBusy = false;
        //                }
        //            });
        //        });

        //        [XmlIgnore]
        //        [Browsable(false)]
        //        [Display(Name = "保存配置")]
        //        public RelayCommand SaveCommand => new RelayCommand((s, e) =>
        //        {
        //            var b = this.Save(out string message);
        //            this.Message = b ? "保存成功" : "保存失败" + message;

        //        });
    }
}
