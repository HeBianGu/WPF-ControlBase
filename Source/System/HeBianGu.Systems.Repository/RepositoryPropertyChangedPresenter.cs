// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace HeBianGu.Systems.Repository
{
    //public interface IRepositoryPropertyChanged
    //{
    //    //RelayCommand AddCommand { get; }
    //    //bool CheckedAll { get; set; }
    //    //RelayCommand CheckedAllCommand { get; }
    //    //RelayCommand ClearCommand { get; }
    //    //RelayCommand DeleteCheckedCommand { get; }
    //    //RelayCommand DeleteCommand { get; }
    //    //RelayCommand EditCommand { get; }
    //    //RelayCommand ExportCommand { get; }
    //    //bool IsBusy { get; set; }
    //    //RelayCommand SaveCommand { get; }
    //    //RelayCommand ViewCommand { get; }
    //}

    public class RepositoryPropertyChangedPresenterBase : DisplayerViewModelBase

    {

    }
    /// <summary>
    /// 直接对接模型的仓储基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class RepositoryPropertyChangedPresenter<TEntity> : RepositoryPropertyChangedPresenterBase where TEntity : StringEntityBase, new()
    {
        protected override void Loaded(object obj)
        {
            if (this.Repository == null) return;

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
                        {
                            MessageProxy.Messager.ShowWaitter(() =>
                            {
                                this.IsBusy = true;
                                try
                                {
                                    var collection = this.Repository.GetList().Select(x => new SelectViewModel<TEntity>(x));
                                    //this.Collection = collection.Select(x => new SelectViewModel<TEntity>(x)).ToObservable();
                                    this.Collection.Load(collection);
                                }
                                catch (Exception ex)
                                {
                                    Logger.Instance?.Error(ex);
                                    MessageProxy.Messager.ShowSumit("加载数据错误:" + ex.Message);
                                }
                                finally
                                {
                                    this.IsBusy = false;
                                }
                            });
                        }));

        }

        #region - 属性 -
        private bool _checkedAll;
        public bool CheckedAll
        {
            get { return _checkedAll; }
            set
            {
                _checkedAll = value;
                RaisePropertyChanged();
            }
        }

        public Type ModelType => typeof(TEntity);

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        public IStringRepository<TEntity> Repository => ServiceRegistry.Instance.GetAllAssignableFrom<IStringRepository<TEntity>>()?.FirstOrDefault();
        //private ObservableCollection<SelectViewModel<TEntity>> _collection = new ObservableCollection<SelectViewModel<TEntity>>();
        //public ObservableCollection<SelectViewModel<TEntity>> Collection
        //{
        //    get { return _collection; }
        //    set
        //    {
        //        _collection = value;
        //        RaisePropertyChanged();
        //    }
        //}

        private IObservableSource<SelectViewModel<TEntity>> _collection = new ObservableSource<SelectViewModel<TEntity>>();
        /// <summary> 说明  </summary>
        public IObservableSource<SelectViewModel<TEntity>> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }
        private SelectViewModel<TEntity> _selectedItem;
        /// <summary> 说明  </summary>
        public SelectViewModel<TEntity> SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }


        private bool _useCheckAll;
        [DefaultValue(true)]
        [Display(Name = "启用全选")]
        public bool UseCheckAll
        {
            get { return _useCheckAll; }
            set
            {
                _useCheckAll = value;
                RaisePropertyChanged();
            }
        }


        private bool _useDelete;
        [DefaultValue(true)]
        [Display(Name = "启用删除")]
        public bool UseDelete
        {
            get { return _useDelete; }
            set
            {
                _useDelete = value;
                RaisePropertyChanged();
            }
        }


        private bool _useEdit;
        [DefaultValue(true)]
        [Display(Name = "启用编辑")]
        public bool UseEdit
        {
            get { return _useEdit; }
            set
            {
                _useEdit = value;
                RaisePropertyChanged();
            }
        }

        private bool _useView;
        [DefaultValue(true)]
        [Display(Name = "启用查看")]
        public bool UseView
        {
            get { return _useView; }
            set
            {
                _useView = value;
                RaisePropertyChanged();
            }
        }


        private bool _useDeleteChecked;
        [DefaultValue(true)]
        [Display(Name = "启用删除选中")]
        public bool UseDeleteChecked
        {
            get { return _useDeleteChecked; }
            set
            {
                _useDeleteChecked = value;
                RaisePropertyChanged();
            }
        }


        private bool _useExport;
        [DefaultValue(true)]
        [Display(Name = "启用导出")]
        public bool UseExport
        {
            get { return _useExport; }
            set
            {
                _useExport = value;
                RaisePropertyChanged();
            }
        }


        private bool _useClear;
        [DefaultValue(true)]
        [Display(Name = "启用清空")]
        public bool UseClear
        {
            get { return _useClear; }
            set
            {
                _useClear = value;
                RaisePropertyChanged();
            }
        }


        private bool _useAdd;
        [DefaultValue(true)]
        [Display(Name = "启用新增")]
        public bool UseAdd
        {
            get { return _useAdd; }
            set
            {
                _useAdd = value;
                RaisePropertyChanged();
            }
        }


        private bool _useSearch;
        [DefaultValue(true)]
        [Display(Name = "启用搜索")]
        public bool UseSearch
        {
            get { return _useSearch; }
            set
            {
                _useSearch = value;
                RaisePropertyChanged();
            }
        }


        private bool _useLayout;
        [DefaultValue(true)]
        [Display(Name = "启用搜索")]
        public bool UseLayout
        {
            get { return _useLayout; }
            set
            {
                _useLayout = value;
                RaisePropertyChanged();
            }
        }


        private bool _usePageCount;
        [DefaultValue(true)]
        [Display(Name = "启用搜索")]
        public bool UsePageCount
        {
            get { return _usePageCount; }
            set
            {
                _usePageCount = value;
                RaisePropertyChanged();
            }
        }


        private int _pageCount;
        [DefaultValue(15)]
        [Display(Name = "每页显示的数量")]
        public int PageCount
        {
            get { return _pageCount; }
            set
            {
                _pageCount = value;
                RaisePropertyChanged();
            }
        }

        private RepositoryDiplayMode _displayMode;
        [DefaultValue(RepositoryDiplayMode.DataGrid)]
        [Display(Name = "呈现的样式")]
        public RepositoryDiplayMode DisplayMode
        {
            get { return _displayMode; }
            set
            {
                _displayMode = value;
                RaisePropertyChanged();
            }
        }


        private int _layoutSelectedIndex;
        [DefaultValue(0)]
        [Display(Name = "布局方式")]
        public int LayoutSelectedIndex
        {
            get { return _layoutSelectedIndex; }
            set
            {
                _layoutSelectedIndex = value;
                RaisePropertyChanged();
            }
        }


        #endregion

        #region - 命令 -

        public RelayCommand AddCommand => new RelayCommand(async l => await Add(l)) { Name = "新增" };

        public RelayCommand EditCommand => new RelayCommand(async l => await Edit(l), l => l is TEntity) { Name = $"编辑" };

        public TransactionCommand EditTransactionCommand => new TransactionCommand(async (s, e) =>
        {
            if (e is TEntity project)
            {
                TEntity temp;
                var r = await s.BeginEditAsync(() =>
                {
                    if (project.ModelState(out List<string> message) == false)
                    {
                        MessageProxy.Snacker.Show(message?.FirstOrDefault());
                        return false;
                    }
                    return true;
                });
                if (r)
                {
                    if (this.Repository != null)
                        this.Repository.Save();
                }
                else
                {
                    //  Do ：rollback
                }
            }
        });

        public RelayCommand DeleteCommand => new RelayCommand(async l => await Delete(l), l => l is TEntity) { Name = $"删除" };

        public RelayCommand ViewCommand => new RelayCommand(async l => await View(l), l => l is TEntity) { Name = $"查看" };

        public RelayCommand ClearCommand => new RelayCommand(async l => await Clear(l), l => this.Collection != null && this.Collection.Count > 0) { Name = $"清除" };

        public RelayCommand SaveCommand => new RelayCommand(async l => await this.Save());

        public RelayCommand ExportCommand => new RelayCommand(async l =>
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel(*.xlsx)|*.xlsx|所有文件|*.*";
            saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.AddExtension = true;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "保存文件";
            if (saveFileDialog.ShowDialog() != true)
                return;

            await this.Export(saveFileDialog.FileName);


        }, l => this.Collection != null && this.Collection.Count > 0)
        { Name = $"导出" };

        //public RelayCommand ImportCommand => new RelayCommand(async l =>
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    //openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
        //    openFileDialog.Filter = "文本文件(*.xml)|*.xml|所有文件|*.*";
        //    openFileDialog.FilterIndex = 2;
        //    openFileDialog.Title = "打开文件";
        //    openFileDialog.RestoreDirectory = true;
        //    openFileDialog.Multiselect = false;
        //    if (openFileDialog.ShowDialog() != true)
        //        return;

        //    await this.Import(openFileDialog.FileName);
        //}); 

        public RelayCommand CheckedAllCommand => new RelayCommand(l =>
        {
            if (l is Boolean b)
            {
                this.Collection.Foreach(K => K.IsSelected = b);
                this.CheckedAll = b;
            }
        }, l => this.Collection != null && this.Collection.Count > 0)
        { Name = $"全选" };

        public RelayCommand DeleteCheckedCommand => new RelayCommand(async l => await DeleteAllChecked(l), l => this.Collection.Any(k => k.IsSelected)) { Name = $"删除选中" };

        #endregion

        #region - 方法 -

        protected virtual async Task Export(string path)
        {
            var collection = this.Collection.Select(x => x.Model);
            string message = null;
            var r = ExcelServiceProxy.Instance?.Export(collection, path, typeof(TEntity).Name, out message);
            if (r == false)
            {
                await MessageProxy.Messager.ShowSumit("导出错误," + message);
            }
            else
            {
                var rs = await MessageProxy.Messager.ShowResult("导出成功，是否立即查看?");
                if (rs)
                {
                    //Process.Start("explorer.exe", path);
                    Process.Start(new ProcessStartInfo("explorer.exe", path) { UseShellExecute = true });

                }
            }
        }

        //protected virtual async Task Import(string path)
        //{
        //    await MessageProxy.Messager.ShowWaitter(() =>
        //    {
        //        var models = XmlSerialize.Instance.Load<ObservableCollection<T>>(path);

        //        this.Collection.AddRange(models);
        //    });
        //}

        protected virtual object GetAddModel(TEntity entity)
        {
            return entity;
        }

        protected virtual object GetEditModel(TEntity entity)
        {
            return entity;
        }
        protected virtual object GetViewModel(TEntity entity)
        {
            return entity;
        }

        protected virtual async Task Add(object obj)
        {
            var m = new TEntity();
            bool dialog = await MessageProxy.PropertyGrid.ShowEdit(this.GetAddModel(m), null, "新增", x => x.Padding = new Thickness(20));
            if (!dialog)
            {
                MessageProxy.Snacker.ShowTime("取消操作");
                return;
            }

            //var r = await this.Repository?.InsertAsync(m);
            //if (r > 0)
            //{
            //    this.Collection.Add(new SelectViewModel<TEntity>(m));
            //    MessageProxy.Snacker.ShowTime("新增成功");
            //}
            //else
            //{
            //    MessageProxy.Snacker.ShowTime("新增失败,数据库保存错误");
            //}

            await this.Add(m);

            this.OnCollectionChanged(obj);


        }

        public virtual async Task Add(TEntity m)
        {
            if (this.Repository == null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.Collection.Add(new SelectViewModel<TEntity>(m));
                });
                MessageProxy.Snacker.ShowTime("新增成功");
                return;
            }
            var r = await this.Repository?.InsertAsync(m);
            if (r > 0)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.Collection.Add(new SelectViewModel<TEntity>(m));
                });
                MessageProxy.Snacker.ShowTime("新增成功");
            }
            else
            {
                MessageProxy.Snacker.ShowTime("新增失败,数据库保存错误");
            }
        }

        protected virtual async Task Edit(object obj)
        {

            if (obj is TEntity entity)
            {
                bool r = await MessageProxy.PropertyGrid.ShowEdit(this.GetEditModel(entity), null, "编辑");

                if (!r)
                {
                    MessageProxy.Snacker.ShowTime("取消操作");
                    return;
                }

                int rs = await this.Repository.SaveAsync();

                if (rs > 0)
                {
                    MessageProxy.Snacker.ShowTime("保存成功");
                }
                else
                {
                    MessageProxy.Snacker.ShowTime("保存失败，数据库保存错误");
                }
            }
        }

        protected virtual async Task Delete(object obj)
        {
            if (obj is TEntity entity)
            {
                var result = await MessageProxy.Messager.ShowResult("确定删除数据？");
                if (!result)
                    return;

                if (this.Repository == null)
                {
                    MessageProxy.Snacker.ShowTime("删除成功");
                    var vm = this.Collection.FirstOrDefault(x => x.Model == entity);
                    this.Collection.Remove(vm);
                    return;
                }
                var r = await this.Repository.DeleteAsync(entity);

                if (r > 0)
                {
                    MessageProxy.Snacker.ShowTime("删除成功");
                    var vm = this.Collection.FirstOrDefault(x => x.Model == entity);
                    this.Collection.Remove(vm);
                }
                else
                {
                    MessageProxy.Snacker.ShowTime("删除失败,数据库保存错误");
                }
            }

            this.OnCollectionChanged(obj);
        }

        protected virtual async Task Clear(object obj)
        {
            var result = await MessageProxy.Messager.ShowResult("确定清空数据？");
            if (!result)
                return;

            if (this.Repository == null)
            {
                MessageProxy.Snacker.ShowTime("清空成功");
                this.Collection.Clear();
                return;
            }

            var r = await this.Repository.ClearAsync();
            if (r > 0)
            {
                MessageProxy.Snacker.ShowTime("清空成功");
                this.Collection.Clear();
            }
            else
            {
                MessageProxy.Snacker.ShowTime("清空失败,数据库保存错误");
            }

            this.OnCollectionChanged(obj);
        }

        protected virtual async Task View(object obj)
        {
            if (obj is TEntity entity)
            {
                await MessageProxy.PropertyGrid.ShowView(this.GetViewModel(entity), null, "查看");
                this.OnCollectionChanged(obj);
            }
        }

        protected virtual async Task Save()
        {
            var r = await this.Repository.SaveAsync();

            if (r > 0)
            {
                MessageProxy.Snacker.ShowTime("保存成功");
            }
            else
            {
                MessageProxy.Snacker.ShowTime("保存失败,数据库保存错误");
            }
        }


        protected virtual void OnCollectionChanged(object obj)
        {

        }

        protected virtual async Task DeleteAllChecked(object obj)
        {
            var result = await MessageProxy.Messager.ShowResult("确定删除数据？");
            if (!result)
                return;

            var checks = this.Collection.Where(k => k.IsSelected).Select(x => x.Model)?.ToList();

            if (this.Repository == null)
            {
                MessageProxy.Snacker.ShowTime($"删除成功,共计{checks.Count()}条");
                this.Collection.RemoveAll(x => checks.Contains(x.Model));
                return;
            }

            var r = await this.Repository.DeleteAsync(x => checks.Contains(x));
            if (r > 0)
            {
                MessageProxy.Snacker.ShowTime($"删除成功,共计{checks.Count()}条");
                this.Collection.RemoveAll(x => checks.Contains(x.Model));
            }
            else
            {
                MessageProxy.Snacker.ShowTime("删除失败,数据库保存错误");
            }

            this.OnCollectionChanged(obj);
        }

        #endregion 
    }
}
