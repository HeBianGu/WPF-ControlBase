using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace HeBianGu.Systems.WinTool
{
    public interface IComputerViewPresenterOption
    {
        ObservableCollection<IComputerInfo> Collection { get; }
    }

    public interface IComputerViewPresenter : IViewPresenter, IStartWindowLoad
    {

    }

    [Displayer(Name = "电脑信息", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "电脑信息")]
    public class ComputerViewPresenter : ServiceMvpSettingBase<ComputerViewPresenter, IComputerViewPresenter>, IComputerViewPresenter, IComputerViewPresenterOption
    {
        public ComputerViewPresenter()
        {
            var types = this.GetType().Assembly.GetTypes().Where(x => typeof(IComputerInfo).IsAssignableFrom(x)).Where(x => x.GetCustomAttribute<DisplayerAttribute>() != null);
            this.Collection = types.Where(x => x.IsClass && !x.IsAbstract).Select(x => Activator.CreateInstance(x)).OfType<IComputerInfo>().OrderBy(x => x.Order).ToObservable();
        }

        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<IComputerInfo> Collection { get; } = new ObservableCollection<IComputerInfo>();


        private IComputerInfo _selectedItem;
        /// <summary> 说明  </summary>
        public IComputerInfo SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }

#if NET
        /// <summary>
        /// 操作系统版本
        /// </summary>
        public string OSDescription { get; } = System.Runtime.InteropServices.RuntimeInformation.OSDescription; 

        /// <summary>
        /// 操作系统架构（<see cref="Architecture">）
        /// </summary>
        public string OSArchitecture { get; } = System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString();

        /// <summary>
        /// 是否为Windows操作系统
        /// </summary>
        public bool IsOSPlatform { get; } = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows);

#else
        /// <summary>
        /// 操作系统版本
        /// </summary>
        public string OSDescription { get; } = Environment.OSVersion.ToString();

        /// <summary>
        /// 操作系统架构（<see cref="Architecture">）
        /// </summary>
        public string OSArchitecture { get; } = Environment.OSVersion.VersionString;

        /// <summary>
        /// 是否为Windows操作系统
        /// </summary>
        public bool IsOSPlatform { get; } = Environment.OSVersion.Platform.ToString().StartsWith("win");

#endif


        /// <summary>
        /// 计算机名称
        /// </summary>
        public string ComputerName { get; } = System.Environment.GetEnvironmentVariable("ComputerName");

        /// <summary>
        /// 计算机用户
        /// </summary>
        public string UserName { get; set; } = System.Environment.UserName;

        public bool Load(IStartWindow window, out string message)
        {
            message = null;
            foreach (var item in this.Collection)
            {
                window?.SetMessage("正在加载" + item.Name);
                //item.IsBuzy = true;
                //var collecltion = item.CreateData();
                //item.Load(collecltion);
                Thread.Sleep(100);
            }
            window?.SetMessage("加载完成" + this.Name);
            return true;
        }

        protected override void Loaded(object obj)
        {
            Task.Run(() =>
            {
                foreach (var item in this.Collection.ToList())
                {
                    item.IsBuzy = true;
                    var collecltion = item.CreateData();
                    item.Load(collecltion);
                }
            });
        }


        public RelayCommand MoreCommand => new RelayCommand((s, e) =>
        {
            if (e is IComputerInfo project)
            {
                this.SelectedItem = project;
            }
        });

        public RelayCommand AddInfoCommand => new RelayCommand(async (s, e) =>
        {
            ComputerInfo info = new ComputerInfo();
            var r = await MessageProxy.PropertyGrid.ShowEdit(info, null, "新增自定义参数信息");
            if (!r) return;
            await MessageProxy.Messager.ShowWaitter(() =>
            {
                var collecltion = info.CreateData();
                info.Load(collecltion);
            });
            this.Collection.Add(info);
            MessageProxy.Snacker.ShowTime("添加成功");
        });
    }
}
