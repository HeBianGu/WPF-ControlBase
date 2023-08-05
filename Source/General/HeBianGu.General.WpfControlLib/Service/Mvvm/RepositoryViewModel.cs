// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HeBianGu.General.WpfControlLib
{
    public interface IRepositoryViewModel<TEntity> : IRepositoryViewModelBase<TEntity>
     where TEntity : StringEntityBase, new()
    {
        IObservableSource<SelectViewModel<TEntity>> Collection { get; set; }
    }

    /// <summary>
    /// 直接对接模型的仓储基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class RepositoryViewModel<TEntity> : RepositoryViewModelBase<SelectViewModel<TEntity>, TEntity>, IRepositoryViewModel<TEntity> where TEntity : StringEntityBase, new()
    {
        public override void RefreshData()
        {
            //this.IsBusy = true;
            //try
            //{
            var includes = this.GetIncludes();
            var collection = includes == null ? this.Repository.GetList().Select(x => new SelectViewModel<TEntity>(x))
            : this.Repository.GetList(includes.ToArray()).Select(x => new SelectViewModel<TEntity>(x));
            //this.Collection = collection.Select(x => new SelectViewModel<TEntity>(x)).ToObservable();
            this.Collection.Load(collection);
            //}
            //catch (Exception ex)
            //{
            //    Logger.Error(ex);
            //    MessageProxy.Messager.ShowSumit("加载数据错误:" + ex.Message);
            //}
            //finally
            //{
            //    this.IsBusy = false;
            //}
        }

        public override async Task Add(params TEntity[] ms)
        {
            if (this.Repository == null)
            {
                foreach (var m in ms)
                {
                    this.Collection.Add(new SelectViewModel<TEntity>(m));
                }
                MessageProxy.Snacker.ShowTime("新增成功");
                return;
            }
            var r = await this.Repository?.InsertRangeAsync(ms);
            if (r > 0)
            {
                this.Collection.Add(ms.Select(x => new SelectViewModel<TEntity>(x)).ToArray());
                MessageProxy.Snacker.ShowTime("新增成功");
            }
            else
            {
                MessageProxy.Snacker.ShowTime("新增失败,数据库保存错误");
            }
        }
    }

    public class DateRepositoryViewModel<TEntity> : RepositoryViewModel<TEntity> where TEntity : mbc_db_modelbase, new()
    {
        public override void RefreshData()
        {
            var includes = this.GetIncludes();
            var collection = includes == null ? this.Repository.GetList(x => x.CDATE > this.StartTime && x.CDATE < this.EndTime).Select(x => new SelectViewModel<TEntity>(x))
            : this.Repository.GetList(includes.ToArray()).Select(x => new SelectViewModel<TEntity>(x));
            this.Collection.Load(collection);
        }

        private DateTime _startTime = DateTime.Now.AddDays(-7).Date;
        /// <summary> 说明  </summary>
        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _endTime = DateTime.Now.AddDays(1).Date;
        /// <summary> 说明  </summary>
        public DateTime EndTime
        {
            get { return _endTime; }
            set
            {
                _endTime = value;
                RaisePropertyChanged();
            }
        }
    }
}
