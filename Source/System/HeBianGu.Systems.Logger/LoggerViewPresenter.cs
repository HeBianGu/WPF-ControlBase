// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Linq;
using HeBianGu.Control.Chart2D;
using System.ComponentModel.DataAnnotations;
using System;
using System.Windows.Threading;
using System.Windows;
using System.Collections.Generic;
using HeBianGu.Systems.Logger;
using HeBianGu.General.WpfControlLib;

namespace HeBianGu.Systems.Logger
{
    public interface ILoggerViewPresenter : IInvokePresenter
    {

    }

    public interface ILoggerViewPresenterOption : IMvpSettingOption
    {

    }

    [Displayer(Name = "系统日志", GroupName = SystemSetting.GroupMessage, Icon = "\xe6e7", Description = "应用功能管理和配置系统日志")]
    public class LoggerViewPresenter : ServiceMvpSettingBase<LoggerViewPresenter, ILoggerViewPresenter>, ILoggerViewPresenter, ILoggerViewPresenterOption
    {
        private int _days;
        [DefaultValue(30)]
        [Display(Name = "显示天数")]
        public int Days
        {
            get { return _days; }
            set
            {
                _days = value;
                RaisePropertyChanged();
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public IStringRepository<hl_dm_debug> DebugRepository => ServiceRegistry.Instance.GetInstance<IStringRepository<hl_dm_debug>>();
        [Browsable(false)]
        [XmlIgnore]
        public IStringRepository<hl_dm_error> ErrorRepository => ServiceRegistry.Instance.GetInstance<IStringRepository<hl_dm_error>>();
        [Browsable(false)]
        [XmlIgnore]
        public IStringRepository<hl_dm_info> InfoRepository => ServiceRegistry.Instance.GetInstance<IStringRepository<hl_dm_info>>();
        [Browsable(false)]
        [XmlIgnore]
        public IStringRepository<hl_dm_fatal> FatalRepository => ServiceRegistry.Instance.GetInstance<IStringRepository<hl_dm_fatal>>();
        [Browsable(false)]
        [XmlIgnore]
        public IStringRepository<hl_dm_warn> WarnRepository => ServiceRegistry.Instance.GetInstance<IStringRepository<hl_dm_warn>>();

        public override void Load()
        {
            base.Load();

            double min = 0;
            double max = 0;
            Action<IEnumerable<Tuple<DateTime, double>>> action = d =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                {
                    PointData seriesData = new PointData();
                    for (int i = 0; i <= this.Days; i++)
                    {
                        var current = DateTime.Now.AddDays(i - this.Days).Date;
                        var find = d.FirstOrDefault(x => x.Item1.Date == current);
                        seriesData.xDatas.Add(i - this.Days);
                        seriesData.yDatas.Add(find == null ? 0 : find.Item2);
                    }
                    this.Data.SeriesDatas.Add(seriesData);
                    if (d.Count() > 0)
                    {
                        max = Math.Max(max, d.Max(x => x.Item2));
                    }
                }));
            };
            if (this.DebugRepository != null)
            {
                var data = this.DebugRepository.GetList().GroupBy(x => x.CDATE.Date).Select(x => Tuple.Create(x.Key, (double)x.Count()));
                action.Invoke(data);
            }

            if (this.ErrorRepository != null)
            {
                var data = this.ErrorRepository.GetList().GroupBy(x => x.CDATE.Date).Select(x => Tuple.Create(x.Key, (double)x.Count()));
                action.Invoke(data);
            }

            if (this.InfoRepository != null)
            {
                var data = this.InfoRepository.GetList().GroupBy(x => x.CDATE.Date).Select(x => Tuple.Create(x.Key, (double)x.Count()));
                action.Invoke(data);
            }

            if (this.WarnRepository != null)
            {
                var data = this.WarnRepository.GetList().GroupBy(x => x.CDATE.Date).Select(x => Tuple.Create(x.Key, (double)x.Count()));
                action.Invoke(data);
            }

            if (this.FatalRepository != null)
            {
                var data = this.FatalRepository.GetList().GroupBy(x => x.CDATE.Date).Select(x => Tuple.Create(x.Key, (double)x.Count()));
                action.Invoke(data);
            }

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
            {
                for (int i = 0; i <= this.Days; i++)
                {
                    var current = DateTime.Now.AddDays(i - this.Days).Date;
                    this.Data.xAxis.Add(i - this.Days);
                    this.Data.xDisplay.Add(current.ToString("dd"));
                }
                this.Data.BuildyAxis(min, max);
            }));

        }

        private ChartData _data = new ChartData();
        [Browsable(false)]
        [XmlIgnore]
        public ChartData Data
        {
            get { return _data; }
            set
            {
                _data = value;
                RaisePropertyChanged();
            }
        }
    }
}
