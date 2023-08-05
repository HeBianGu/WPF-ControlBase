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
using Microsoft.EntityFrameworkCore.SqlServer;
#endif
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using log4net;
using System;

namespace HeBianGu.DataBase.SqlServer
{
    public interface ISqlServerOption
    {
        string InitialCatalog { get; set; }
        string Password { get; set; }
        string Server { get; set; }
        string UserName { get; set; }
        string ConfigPath { get; set; }
    }


    public abstract class SqlServerSetting<TSetting> : DataBaseSetting<TSetting>, ISqlServerOption where TSetting : new()
    {
        protected override string GetDefaultPath()
        {
            return this.ConfigPath;
        }

        public override void LoadDefault()
        {
            base.LoadDefault();
            this.ConfigPath = Path.Combine(SystemPathSetting.Instance.Config, this.GetType().Name + SystemPathSetting.Instance.ConfigExtention);
        }

        private string _configPath;
        [ReadOnly(true)]
        [Browsable(false)]
        [Display(Name = "文件路径")]
        public string ConfigPath
        {
            get { return _configPath; }
            set
            {
                _configPath = value;
                RaisePropertyChanged();
            }
        }

        private string _server;
        [Display(Name = "服务器名称")]
        public string Server
        {
            get { return _server; }
            set
            {
                _server = value;
                RaisePropertyChanged();
            }
        }

        private string _initialCatalog;
        [Display(Name = "数据库")]
        public string InitialCatalog
        {
            get { return _initialCatalog; }
            set
            {
                _initialCatalog = value;
                RaisePropertyChanged();
            }
        }


        private string _userName;
        [DefaultValue("sa")]
        [Display(Name = "用户名")]
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged();
            }
        }


        private string _password;
        [DefaultValue(".")]
        [Display(Name = "密码")]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        public override string GetConnect()
        {
            string connect = $"Server={this.Server}; Trusted_Connection=False; uid={this.UserName}; pwd={this.Password};Initial Catalog={this.InitialCatalog}; MultipleActiveResultSets=true;";
            return connect;
        }

        //        protected override void Connect()
        //        {
        //            string connect = this.GetConnect();
        //#if NETCOREAPP
        //            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
        //            optionsBuilder.UseSqlServer(connect);
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
    public class SqlServerSetting : SqlServerSetting<SqlServerSetting>, ISqlServerOption
    {

    }
}
