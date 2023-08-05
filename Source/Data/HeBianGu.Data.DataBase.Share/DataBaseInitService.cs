
using HeBianGu.Base.WpfBase;
using System.Linq;
using HeBianGu.General.WpfControlLib;
using System.Windows;
using HeBianGu.Data.DataBase;
using System.Windows.Input;
using System;
#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.EntityFrameworkCore;
#endif
namespace HeBianGu.Data.DataBase
{
    public abstract class MultiDataBaseInitServiceBase<TDbContext> : IDataBaseInitService where TDbContext : DbContext
    {
        public string Name => "数据库";
        protected abstract IDataBaseSetting GetSetting();
        protected virtual bool CanConnect(DbContext db, out string message)
        {
            message = null;
#if NETCOREAPP
            try
            {
                //db.Database.EnsureCreated();
                db.Database.Migrate();
                return db.Database.CanConnect();
            }
            catch (System.Exception ex)
            {
                Logger.Instance?.Error(ex);
                message = ex.Message;
                return false;
            }
#endif

#if NETFRAMEWORK
           return db.Database.Exists();
#endif
        }

        public virtual bool Load(out string message)
        {
            message = null;
            var dbs = ServiceRegistry.Instance.GetAllAssignableFrom<TDbContext>();
            bool r = this.CanConnect(out message);
            if (!r)
            {
                var result = Application.Current.Dispatcher.Invoke(() =>
                  {
                      return MessageProxy.Windower.ShowSumit("数据库连接失败，是否重新配置?");
                  });

                if (!result)
                    return false;

                result = Application.Current.Dispatcher.Invoke(() =>
                  {
                      return MessageProxy.WindowPresenter.ShowClearly(this.GetSetting(), "数据库连接失败，请重新配置数据库");
                  });

                message = "数据库配置已修改，请重新启动";
                Application.Current.Dispatcher.Invoke(() =>
                {
                    return MessageProxy.Windower.ShowSumit("数据库配置已修改，请重新启动");
                });
                return false;
            }


#if NETCOREAPP
            //db.Database.EnsureCreated();
#endif

#if NETFRAMEWORK
            db.Database.CreateIfNotExists();
#endif
            return true;
        }

        bool CanConnect(out string message)
        {
            message = null;
            var dbs = ServiceRegistry.Instance.GetAllAssignableFrom<TDbContext>();
            bool r = true;
            foreach (var db in dbs)
            {
                if (!this.CanConnect(db, out message))
                {
                    r = false;
                    break;
                }
            }
            return r;
        }

        public abstract bool TryConnect(out string message);
    }


    public abstract class SingleDataBaseInitServiceBase<TDbContext> : IDataBaseInitService where TDbContext : DbContext
    {
        public string Name => "数据库";
        protected abstract IDataBaseSetting GetSetting();
        //protected abstract void CreateDbConnext();
        protected virtual bool CanConnect(DbContext db, out string message)
        {
            message = null;
#if NETCOREAPP
            try
            {
                //db.Database.EnsureCreated();
                db.Database.Migrate();
                return db.Database.CanConnect();
            }
            catch (System.Exception ex)
            {
                Logger.Instance?.Error(ex);
                message = ex.Message;
                return false;
            }
#endif

#if NETFRAMEWORK
           return db.Database.Exists();
#endif
        }


        public virtual bool Load(out string message)
        {
            message = null;
            bool r = this.CanConnect(out message);
            if (!r)
            {
                var result = Application.Current.Dispatcher.Invoke(() =>
                {
                    return MessageProxy.Windower.ShowSumit("数据库连接失败，是否重新配置?");
                });

                if (!result)
                    return false;

                result = Application.Current.Dispatcher.Invoke(() =>
                {
                    return MessageProxy.WindowPresenter.ShowClearly(this.GetSetting(), "数据库连接失败，请重新配置数据库");
                });

                message = "数据库配置已修改，请重新启动";
                Application.Current.Dispatcher.Invoke(() =>
                {
                    return MessageProxy.Windower.ShowSumit("数据库配置已修改，请重新启动");
                });
                return false;
            }


#if NETCOREAPP
            //db.Database.EnsureCreated();
#endif

#if NETFRAMEWORK
            db.Database.CreateIfNotExists();
#endif
            return true;
        }

        bool CanConnect(out string message)
        {
            var db = ServiceRegistry.Instance.GetInstance<TDbContext>();
            return this.CanConnect(db, out message);
        }


        protected abstract DbContextOptionsBuilder<TDbContext> GetOptionsBuilder(string connect);

        public bool TryConnect(out string message)
        {
            message = null;
            try
            {
                string connect = this.GetSetting().GetConnect();
#if NETCOREAPP
                var optionsBuilder = this.GetOptionsBuilder(connect);
                //DataContext abcDataContext = new DataContext(optionsBuilder.Options);
                //abcDataContext.Database.EnsureCreated();
                var abcDataContext = Activator.CreateInstance(typeof(TDbContext), new object[] { optionsBuilder.Options }) as TDbContext;

                abcDataContext.Database.Migrate();
                var r = abcDataContext.Database.CanConnect();
                abcDataContext.Dispose();
                message = r ? "连接成功" : "连接失败";
                CommandManager.InvalidateRequerySuggested();
                return r;
#endif
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
                message = "连接失败:" + ex.Message;
                return false;
            }
        }
    }

    public interface IDataBaseSetting
    {
        string GetConnect();
    }

    public class DataBaseSaveService : IDataBaseSaveService
    {
        public string Name => "数据库数据";
        public bool Save(out string message)
        {
            message = null;
            var db = ServiceRegistry.Instance.GetAllAssignableFrom<DbContext>()?.FirstOrDefault();
            db?.SaveChanges();
            //db.Dispose();
            return true;
        }
    }

    public class DataBaseSaveService<TDbContext> : IDataBaseSaveService where TDbContext : DbContext
    {
        public string Name => "数据库数据";
        public bool Save(out string message)
        {
            message = null;
            var db = ServiceRegistry.Instance.GetInstance<TDbContext>();
            db?.SaveChanges();
            //db.Dispose();
            return true;
        }
    }
}
