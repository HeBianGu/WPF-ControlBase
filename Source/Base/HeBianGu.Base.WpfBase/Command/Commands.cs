using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace HeBianGu.Base.WpfBase
{
    public static class Commands
    {
        public static ICommand WriteLine => new RelayCommand((s, e) => System.Diagnostics.Debug.WriteLine(e?.ToString()));

        #region - Logger -
        public static ICommand LogInfo => new RelayCommand((s, e) => Logger.Instance?.Info(e?.ToString()));
        public static ICommand LogError => new RelayCommand((s, e) => Logger.Instance?.Error(e?.ToString()));
        public static ICommand LogDebug => new RelayCommand((s, e) => Logger.Instance?.Debug(e?.ToString()));
        public static ICommand LogWarn => new RelayCommand((s, e) => Logger.Instance?.Warn(e?.ToString()));
        public static ICommand LogTrace => new RelayCommand((s, e) => Logger.Instance?.Trace(e?.ToString()));
        public static ICommand LogFatal => new RelayCommand((s, e) => Logger.Instance?.Fatal(e?.ToString()));
        #endregion

        public static ICommand CheckUpgrade => new RelayCommand((s, e) =>
        {
            if (UpgradeIniter.Instance?.CheckUpgrade(out string message) == false)
            {
                MessageProxy.Snacker.ShowTime(message);
            }
        });

        #region - MessageProxy -

        public static ICommand SnackerShowTime => new RelayCommand((s, e) =>
        {
            MessageProxy.Snacker.ShowTime(e?.ToString());
        });

        public static ICommand ShowEdit => new RelayCommand((s, e) =>
        {
            MessageProxy.PropertyGrid.ShowEdit(e);
        });


        public static ICommand ShowView => new RelayCommand((s, e) =>
        {
            MessageProxy.PropertyGrid.ShowView(e);
        });

        public static ICommand ShowDataGrid => new RelayCommand((s, e) =>
        {
            if (e is IList list)
                MessageProxy.AutoColumnPagedDataGrid.Show(list);
        });

        public static ICommand ShowNotifyError => new RelayCommand((s, e) =>
        {
            MessageProxy.Notify.ShowError(e?.ToString());
        });

        public static ICommand ShowNotifyInfo => new RelayCommand((s, e) =>
         {
             MessageProxy.Notify.ShowInfo(e?.ToString());

         });

        public static ICommand CloseContainer => new RelayCommand((s, e) =>
        {
            MessageProxy.Container.Close();
        });

        public static ICommand ShowContainer => new RelayCommand((s, e) =>
        {
            if (e is FrameworkElement element)
                MessageProxy.Container.Show(element);
        });

        public static ICommand ShowPresenterClose => new RelayCommand((s, e) =>
        {
            MessageProxy.Presenter.ShowClose(e);
        });

        public static ICommand ShowPresenterRightClose => new RelayCommand((s, e) =>
        {
            MessageProxy.Presenter.ShowRightClose(e);
        });

        public static ICommand ShowWindowPresenterClearly => new RelayCommand((s, e) =>
        {
            MessageProxy.WindowPresenter.ShowClearly(e);
        });


        public static ICommand ShowWindowPresenterSumit => new RelayCommand((s, e) =>
        {
            MessageProxy.WindowPresenter.ShowSumit(e);
        });

        public static ICommand ShowWindowerSumit => new RelayCommand((s, e) =>
        {
            MessageProxy.Windower.ShowSumit(e?.ToString());
        });

        public static ICommand ShowSumit => new RelayCommand((s, e) =>
        {
            MessageProxy.Messager.ShowSumit(e?.ToString());
        });

        public static ICommand ShowWaitter => new RelayCommand((s, e) =>
        {
            MessageProxy.Messager.ShowWaitter(() => Thread.Sleep(2000));
        });

        public static ICommand ShowPercentProgress => new RelayCommand((s, e) =>
        {
            MessageProxy.Messager.ShowPercentProgress(x =>
            {
                for (int i = 0; i < 100; i++)
                {
                    x.Value = i + 1;
                    Thread.Sleep(20);
                }
            });
        });


        public static ICommand ShowStringProgress => new RelayCommand((s, e) =>
        {
            MessageProxy.Messager.ShowStringProgress(x =>
            {
                for (int i = 0; i < 100; i++)
                {
                    x.MessageStr = $"正在加载第{i + 1}条数据，共计100条数据";
                    Thread.Sleep(20);
                }
            });
        });


        public static ICommand ShowTaskBarWaitting => new RelayCommand((s, e) =>
        {
            MessageProxy.TaskBar.ShowWaitting(() =>
            {
                Thread.Sleep(2000);
                return true;
            });
        });

        public static ICommand ShowTaskBarPercent => new RelayCommand((s, e) =>
        {
            MessageProxy.TaskBar.ShowPercent(x =>
            {
                for (int i = 0; i < 100; i++)
                {
                    x.ProgressValue = i + 1;
                    Thread.Sleep(20);
                }
            });
        });

        #endregion


        public static ICommand ExcelExport => new RelayCommand((s, e) =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory; //设置初始路径
            openFileDialog.Filter = "Excel文件(*.xls)|*.xls|Csv文件(*.csv)|*.csv|所有文件(*.*)|*.*"; //设置“另存为文件类型”或“文件类型”框中出现的选择内容
            openFileDialog.FilterIndex = 2; //设置默认显示文件类型为Csv文件(*.csv)|*.csv
            openFileDialog.Title = "打开文件"; //获取或设置文件对话框标题
            openFileDialog.RestoreDirectory = true; //设置对话框是否记忆上次打开的目录
            openFileDialog.Multiselect = false;//设置多选
            if (openFileDialog.ShowDialog() != true) return;
            if (e is IEnumerable<object> source)
            {
                if (ExcelServiceProxy.Instance?.Export(source, openFileDialog.FileName, "name", out string message) == false)
                {
                    MessageProxy.Snacker.ShowTime(message);
                }
            }
        });

        #region - Project -

        public static ICommand SaveProject => new RelayCommand((s, e) =>
        {
            if (ProjectProxy.Instance?.Save(out string message) == false)
            {
                MessageProxy.Snacker.ShowTime(message);
            }
        });


        public static ICommand LoadProject => new RelayCommand((s, e) =>
        {
            if (ProjectProxy.Instance?.Load(out string message) == false)
            {
                MessageProxy.Snacker.ShowTime(message);
            }
        });
        #endregion 

        #region - XmlSerialize -

        public static ICommand SaveObject => new RelayCommand((s, e) =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory; //设置初始路径
            openFileDialog.Filter = "Excel文件(*.xml)|*.xml|所有文件(*.*)|*.*"; //设置“另存为文件类型”或“文件类型”框中出现的选择内容
            openFileDialog.FilterIndex = 2; //设置默认显示文件类型为Csv文件(*.csv)|*.csv
            openFileDialog.Title = "打开文件"; //获取或设置文件对话框标题
            openFileDialog.RestoreDirectory = true; //设置对话框是否记忆上次打开的目录
            openFileDialog.Multiselect = false;//设置多选
            if (openFileDialog.ShowDialog() != true) return;
            XmlSerialize.Instance?.Save(openFileDialog.FileName, e);
        });

        public static ICommand LoadObject => new RelayCommand((s, e) =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory; //设置初始路径
            openFileDialog.Filter = "Excel文件(*.xml)|*.xml|所有文件(*.*)|*.*"; //设置“另存为文件类型”或“文件类型”框中出现的选择内容
            openFileDialog.FilterIndex = 2; //设置默认显示文件类型为Csv文件(*.csv)|*.csv
            openFileDialog.Title = "打开文件"; //获取或设置文件对话框标题
            openFileDialog.RestoreDirectory = true; //设置对话框是否记忆上次打开的目录
            openFileDialog.Multiselect = false;//设置多选
            if (openFileDialog.ShowDialog() != true) return;
            e = XmlSerialize.Instance?.Load(openFileDialog.FileName, e.GetType());
        });

        #endregion
    }
}
