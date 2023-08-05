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

namespace HeBianGu.Systems.Operation
{
    public interface ILoginOparationViewPresenter : IInvokePresenter
    {

    }

    public interface ILoginOparationViewPresenterOption : IMvpSettingOption
    {

    }

    [Displayer(Name = "登录记录报表", GroupName = SystemSetting.GroupData, Icon = "\xe6e7", Description = "应用此功能可以查看最近登录记录信息")]
    public class LoginOparationViewPresenter : ServiceMvpSettingBase<LoginOparationViewPresenter, ILoginOparationViewPresenter>, ILoginOparationViewPresenter, ILoginOparationViewPresenterOption
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

        public override void Load()
        {
            base.Load();

            if (this.Repository == null) return;

            {
                var data = this.Repository.GetList().Where(x => x.Type == "Login")
               .GroupBy(x => x.Time.Date).Select(x => Tuple.Create(x.Key, (double)x.Count()));
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                {
                    PointData seriesData = new PointData();
                    for (int i = 0; i <= this.Days; i++)
                    {
                        var current = DateTime.Now.AddDays(i - this.Days).Date;
                        var find = data.FirstOrDefault(x => x.Item1.Date == current);
                        this.LoginData.xAxis.Add(i - this.Days);
                        this.LoginData.xDisplay.Add(current.ToString("dd"));
                        seriesData.xDatas.Add(i - this.Days);
                        seriesData.yDatas.Add(find == null ? 0 : find.Item2);
                    }
                    this.LoginData.SeriesDatas.Add(seriesData);
                    if (data.Count() > 0)
                    {
                        this.LoginData.BuildyAxis(0, data.Max(x => x.Item2));
                    }
                }));
            }

            {
                var data = this.Repository.GetList().Where(x => x.Type == "Login").Where(x => x.Time > DateTime.Now.AddDays(-this.Days).Date)
               .GroupBy(x => x.UserName).Select(x => Tuple.Create((double)x.Count(), x.Key));
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                {
                    DoubleData seriesData = new DoubleData();
                    seriesData.Build(data);
                    this.UserData.SeriesDatas.Add(seriesData);
                }));
            }

        }


        [Browsable(false)]
        [XmlIgnore]
        public IStringRepository<hi_dd_operation> Repository => ServiceRegistry.Instance.GetInstance<IStringRepository<hi_dd_operation>>();

        private ChartData _loginData = new ChartData();
        [Browsable(false)]
        [XmlIgnore]
        public ChartData LoginData
        {
            get { return _loginData; }
            set
            {
                _loginData = value;
                RaisePropertyChanged();
            }
        }

        private ChartData _userData = new ChartData();
        [Browsable(false)]
        [XmlIgnore]
        public ChartData UserData
        {
            get { return _userData; }
            set
            {
                _userData = value;
                RaisePropertyChanged();
            }
        }


        //private DoubleCollection _x;
        ///// <summary> 说明  </summary>
        //public DoubleCollection X
        //{
        //    get { return _x; }
        //    set
        //    {
        //        _x = value;
        //        RaisePropertyChanged();
        //    }
        //}


    }
}
