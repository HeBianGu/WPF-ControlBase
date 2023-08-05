//// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

//using HeBianGu.Base.WpfBase;
//using HeBianGu.Control.PagedDataGrid;
//using HeBianGu.Control.PropertyGrid;
//using HeBianGu.General.WpfControlLib;
//using HeBianGu.Service.Mvp;
//using Microsoft.Win32;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Windows.Input;

//namespace HeBianGu.Systems.Repository
//{
//    /// <summary> 说明</summary>
//    public class RepositoryViewModel<T> : NotifyPropertyChanged, IRepositoryPresenter<T>
//    {
//        protected override void Init()
//        {
//            base.Init();

//            this.ModelType = typeof(T);

//        }

//        protected override void Loaded(object obj)
//        {
//            base.Loaded(obj);

//            this.Refresh(null);
//        }

//        #region - 属性 - 


//        private Type _modelType;
//        /// <summary> 说明  </summary>
//        public Type ModelType
//        {
//            get { return _modelType; }
//            set
//            {
//                _modelType = value;
//                RaisePropertyChanged();
//            }
//        }

//        private IObservableSource<SelectViewModel<T>> _itemsSource = new ObservableSource<SelectViewModel<T>>();
//        /// <summary> 说明  </summary>
//        public IObservableSource<SelectViewModel<T>> ItemsSource
//        {
//            get { return _itemsSource; }
//            set
//            {
//                _itemsSource = value;
//                RaisePropertyChanged();
//            }
//        }

//        private SelectViewModel<T> _selectedItem;
//        /// <summary> 说明  </summary>
//        public SelectViewModel<T> SelectedItem
//        {
//            get { return _selectedItem; }
//            set
//            {
//                _selectedItem = value;
//                RaisePropertyChanged();
//            }
//        }


//        private string _searchText;
//        /// <summary> 说明  </summary>
//        public string SearchText
//        {
//            get { return _searchText; }
//            set
//            {
//                _searchText = value;
//                RaisePropertyChanged();
//            }
//        }

//        #endregion

//        #region - 命令 -

//        [HotKey(Key = Key.A, ModifierKeys = ModifierKeys.Control)]
//        [Displayer(Name = "添加", GroupName = "操作", Order = 5, TabName = "操作", Icon = "\xe8a5")]
//        public RelayCommand AddCommand => new RelayCommand(Add);

//        [HotKey(Key = Key.E, ModifierKeys = ModifierKeys.Control)]
//        [Displayer(Name = "编辑", GroupName = "操作", Order = 5, TabName = "操作", Icon = "\xe8a5)")]
//        public RelayCommand EditCommand => new RelayCommand(Edit);

//        [HotKey(Key = Key.E, ModifierKeys = ModifierKeys.Control)]
//        [Displayer(Name = "详情", GroupName = "操作", Order = 5, TabName = "操作", Icon = "\xe8a5")]
//        public RelayCommand DetialCommand => new RelayCommand(Detial);


//        [HotKey(Key = Key.E, ModifierKeys = ModifierKeys.Control)]
//        [Style()]
//        [Displayer(Name = "删除", GroupName = "操作", Order = 5, TabName = "操作", Icon = "\xe8a5")]
//        public RelayCommand DeleteCommand => new RelayCommand(Delete);

//        [HotKey(Key = Key.E, ModifierKeys = ModifierKeys.Control)]
//        [Displayer(Name = "清空", GroupName = "操作", Order = 5, TabName = "操作", Icon = "\xe8a5")]
//        public RelayCommand ClearCommand => new RelayCommand(Clear);

//        [HotKey(Key = Key.E, ModifierKeys = ModifierKeys.Control)]
//        [Displayer(Name = "删除选中", GroupName = "操作", Order = 5, TabName = "操作", Icon = "\xe8a5")]
//        public RelayCommand DeleteSelectedCommand => new RelayCommand(DeleteSelected);

//        [HotKey(Key = Key.E, ModifierKeys = ModifierKeys.Control)]
//        [Displayer(Name = "刷新数据", GroupName = "操作", Order = 5, TabName = "操作", Icon = "\xe8a5")]
//        public RelayCommand RefreshCommand => new RelayCommand(Refresh);

//        [HotKey(Key = Key.E, ModifierKeys = ModifierKeys.Control)]
//        [Displayer(Name = "全选", GroupName = "操作", Order = 5, TabName = "操作", Icon = "\xe8a5")]
//        public RelayCommand CheckAllCommand => new RelayCommand(CheckAll);

//        [HotKey(Key = Key.E, ModifierKeys = ModifierKeys.Control)]
//        [Displayer(Name = "表格设置", GroupName = "操作", Order = 5, TabName = "操作", Icon = "\xe8a5")]
//        public RelayCommand GridSetCommand => new RelayCommand(GridSet);

//        [HotKey(Key = Key.E, ModifierKeys = ModifierKeys.Control)]
//        [Displayer(Name = "导出Xml", GroupName = "操作", Order = 5, TabName = "操作", Icon = "\xe8a5")]
//        public RelayCommand ExportXmlCommand => new RelayCommand(ExportXml);

//        public RelayCommand SearchTextCommand => new RelayCommand(Search);
//        #endregion

//        #region - 方法 -

//        protected async void Refresh(object obj)
//        {
//            IEnumerable<T> result = await this.InvokeMethod<IEnumerable<T>>("GetAll", new object[] { null });

//            if (result == null)
//            {
//                this.ItemsSource.Clear();
//                return;
//            }

//            IEnumerable<SelectViewModel<T>> select = result.Select(l => new SelectViewModel<T>(l));

//            this.ItemsSource.PageCount = RepositorySetting.Instance.UsePagination ?
//                RepositorySetting.Instance.PageCount : select.Count();

//            this.ItemsSource.Load(select);
//        }

//        protected void CheckAll(object obj)
//        {
//            if (obj is bool b)
//            {
//                this.ItemsSource.Foreach(l => l.IsSelected = b);
//            }
//        }

//        protected async void GridSet(object obj)
//        {
//            if (obj is IList source)
//            {
//                await AutoColumnPagedDataGrid.ShowSource(source);
//            }
//        }

//        protected void Search(object obj)
//        {
//            if (RepositorySetting.Instance.ItemsControlType == ItemsControlType.ListBox)
//            {
//                this.ItemsSource.Fileter = l => l.Model.IsMacth(this.SearchText?.ToString());

//                this.ItemsSource.RefreshSource();
//            }

//        }


//        protected void ExportXml(object obj)
//        {
//            ISerializerService XmlSerializer = ServiceRegistry.Instance.GetInstance<ISerializerService>();

//            if (XmlSerializer == null) return;

//            List<T> models = this.ItemsSource?.Where(l => l.IsSelected)?.Select(l => l.Model)?.ToList();

//            if (models == null || models.Count == 0)
//            {
//                MessageProxy.Snacker.ShowTime("选择的数据不能为空");
//                return;
//            }

//            SaveFileDialog saveFileDialog = new SaveFileDialog();
//            saveFileDialog.Filter = "文本文件(*.xml)|*.xml|所有文件|*.*";//设置文件类型
//            saveFileDialog.FileName = "设置默认文件名";//设置默认文件名
//            saveFileDialog.DefaultExt = "xml";//设置默认格式（可以不设）
//            saveFileDialog.AddExtension = true;//设置自动在文件名中添加扩展名
//            saveFileDialog.RestoreDirectory = true; //设置对话框是否记忆上次打开的目
//            saveFileDialog.Title = "保存文件"; //获取或设置文件对话框标题 
//            if (saveFileDialog.ShowDialog() != true) return;

//            try
//            {

//                XmlSerializer.Save(saveFileDialog.FileName, models);
//                MessageProxy.Snacker.ShowTime("导出成功!");

//                bool b = MessageProxy.Windower.ShowDialog("导出成功是否立即查看?");

//                if (b)
//                {

//                    Process.Start(new ProcessStartInfo(saveFileDialog.FileName) { UseShellExecute = true });
//                    //Process.Start(saveFileDialog.FileName);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageProxy.Windower.ShowSumit("保存错误,详情请查看日志");
//                Logger?.Error(ex);
//            }
//        }



//        protected virtual async void Add(object obj)
//        {
//            if (this.ItemsSource == null) return;

//            object instance = Activator.CreateInstance(typeof(T));

//            bool r = await PropertyGrid.Show(instance, null, "添加");

//            if (r == false) return;

//            bool result = false;

//            if (RepositorySetting.Instance.InteropMode == InteropMode.IRepository)
//            {
//                result = await this.InvokeMethod<bool>("Add", instance);
//            }
//            else if (RepositorySetting.Instance.InteropMode == InteropMode.CustomAction)
//            {
//                if (RepositorySetting.Instance.Action.Add == null)
//                {
//                    Logger.Error("没有注册自定义操作Add");
//                }
//                else
//                {
//                    result = await Task.Run(() =>
//                    {
//                        return RepositorySetting.Instance.Action.Add.Invoke(instance);
//                    });
//                }
//            }
//            else if (RepositorySetting.Instance.InteropMode == InteropMode.IInterop)
//            {
//                if (RepositorySetting.Instance.Interop == null)
//                {
//                    Logger.Error("没有注册交互接口Interop");
//                }
//                else
//                {
//                    result = await Task.Run(() =>
//                    {
//                        return RepositorySetting.Instance.Interop.Add(instance);
//                    });
//                }
//            }
//            else
//            {
//                Logger.Error("未识别InteropMode" + RepositorySetting.Instance.InteropMode);
//            }

//            if (result == false)
//            {
//                MessageProxy.Snacker.ShowTime("添加失败,详情请看错误日志");
//                return;
//            }

//            this.ItemsSource.Add(new SelectViewModel<T>((T)instance));
//        }

//        protected virtual async void Edit(object obj)
//        {
//            if (this.SelectedItem == null)
//            {
//                MessageProxy.Snacker.ShowTime("请先选择一个项目");
//                return;
//            }
//            bool r = await PropertyGrid.Show(this.SelectedItem.Model, null, "编辑");

//            if (r == false) return;

//            T instance = this.SelectedItem.Model;

//            bool result = false;

//            if (RepositorySetting.Instance.InteropMode == InteropMode.IRepository)
//            {
//                result = await this.InvokeMethod<bool>("Save", instance);
//            }
//            else if (RepositorySetting.Instance.InteropMode == InteropMode.CustomAction)
//            {
//                if (RepositorySetting.Instance.Action.Save == null)
//                {
//                    Logger.Error("没有注册自定义操作Save");
//                }
//                else
//                {
//                    result = await Task.Run(() =>
//                    {
//                        return RepositorySetting.Instance.Action.Save.Invoke(instance);
//                    });
//                }
//            }
//            else if (RepositorySetting.Instance.InteropMode == InteropMode.IInterop)
//            {
//                if (RepositorySetting.Instance.Interop == null)
//                {
//                    Logger.Error("没有注册交互接口Interop");
//                }
//                else
//                {
//                    result = await Task.Run(() =>
//                    {
//                        return RepositorySetting.Instance.Interop.Save(instance);
//                    });
//                }
//            }
//            else
//            {
//                Logger.Error("未识别InteropMode" + RepositorySetting.Instance.InteropMode);
//            }

//            if (result == false)
//            {
//                MessageProxy.Snacker.ShowTime("保存失败");
//                return;
//            }
//        }

//        protected virtual async void Detial(object obj)
//        {
//            if (this.SelectedItem == null)
//            {
//                MessageProxy.Snacker.ShowTime("请先选择一个项目");
//                return;
//            }

//            await PropertyGrid.Show(this.SelectedItem.Model, null, "详情", x => x.UsePropertyView = true);
//        }
//        protected virtual async void Delete(object obj)
//        {
//            if (this.SelectedItem == null)
//            {
//                MessageProxy.Snacker.ShowTime("请先选择一个项目");
//                return;
//            }

//            Array array = Array.CreateInstance(typeof(T), 1);

//            array.SetValue(this.SelectedItem.Model, 0);

//            bool result = false;

//            T instance = this.SelectedItem.Model;

//            if (RepositorySetting.Instance.InteropMode == InteropMode.IRepository)
//            {
//                result = await this.InvokeMethod<bool>("Delete", array);
//            }
//            else if (RepositorySetting.Instance.InteropMode == InteropMode.CustomAction)
//            {
//                if (RepositorySetting.Instance.Action.Delete == null)
//                {
//                    Logger.Error("没有注册自定义操作Save");
//                }
//                else
//                {
//                    result = await Task.Run(() =>
//                    {
//                        return RepositorySetting.Instance.Action.Delete.Invoke(array);
//                    });
//                }
//            }
//            else if (RepositorySetting.Instance.InteropMode == InteropMode.IInterop)
//            {
//                if (RepositorySetting.Instance.Interop == null)
//                {
//                    Logger.Error("没有注册交互接口Interop");
//                }
//                else
//                {
//                    result = await Task.Run(() =>
//                    {
//                        return RepositorySetting.Instance.Interop.Delete(array);
//                    });
//                }
//            }
//            else
//            {
//                Logger.Error("未识别InteropMode" + RepositorySetting.Instance.InteropMode);
//            }


//            if (result == false)
//            {
//                MessageProxy.Snacker.ShowTime("删除失败,详情请查看日志");
//                return;
//            }

//            this.ItemsSource.Remove(this.SelectedItem);
//        }

//        protected virtual async void Clear(object obj)
//        {
//            if (this.ItemsSource == null) return;

//            IEnumerable<SelectViewModel<T>> finds = this.ItemsSource.Where(l => true);

//            T[] array = finds.Select(l => l.Model).ToArray();

//            bool result = false;
//            if (RepositorySetting.Instance.InteropMode == InteropMode.IRepository)
//            {
//                result = await this.InvokeMethod<bool>("Delete", array);
//            }
//            else if (RepositorySetting.Instance.InteropMode == InteropMode.CustomAction)
//            {
//                if (RepositorySetting.Instance.Action.Delete == null)
//                {
//                    Logger.Error("没有注册自定义操作Delete");
//                }
//                else
//                {
//                    result = await Task.Run(() =>
//                    {
//                        return RepositorySetting.Instance.Action.Delete.Invoke(array);
//                    });
//                }
//            }
//            else if (RepositorySetting.Instance.InteropMode == InteropMode.IInterop)
//            {
//                if (RepositorySetting.Instance.Interop == null)
//                {
//                    Logger.Error("没有注册交互接口Interop");
//                }
//                else
//                {
//                    result = await Task.Run(() =>
//                    {
//                        return RepositorySetting.Instance.Interop.Delete(array);
//                    });
//                }
//            }
//            else
//            {
//                Logger.Error("未识别InteropMode" + RepositorySetting.Instance.InteropMode);
//            }

//            if (result == false)
//            {
//                MessageProxy.Snacker.ShowTime("清空失败,请查看日志");
//                return;
//            }

//            this.ItemsSource.Clear();
//        }

//        protected virtual async void DeleteSelected(object obj)
//        {
//            IEnumerable<SelectViewModel<T>> finds = this.ItemsSource.Where(l => l.IsSelected);

//            T[] array = finds.Select(l => l.Model).ToArray();

//            bool result = false;

//            if (RepositorySetting.Instance.InteropMode == InteropMode.IRepository)
//            {
//                result = await this.InvokeMethod<bool>("Delete", array);
//            }
//            else if (RepositorySetting.Instance.InteropMode == InteropMode.CustomAction)
//            {
//                if (RepositorySetting.Instance.Action.Delete == null)
//                {
//                    Logger.Error("没有注册自定义操作Delete");
//                }
//                else
//                {
//                    result = await Task.Run(() =>
//                    {
//                        return RepositorySetting.Instance.Action.Delete.Invoke(array);
//                    });
//                }
//            }
//            else if (RepositorySetting.Instance.InteropMode == InteropMode.IInterop)
//            {
//                if (RepositorySetting.Instance.Interop == null)
//                {
//                    Logger.Error("没有注册交互接口Interop");
//                }
//                else
//                {
//                    result = await Task.Run(() =>
//                    {
//                        return RepositorySetting.Instance.Interop.Delete(array);
//                    });
//                }
//            }
//            else
//            {
//                Logger.Error("未识别InteropMode" + RepositorySetting.Instance.InteropMode);
//            }

//            if (result == false)
//            {
//                MessageProxy.Snacker.ShowTime("删除失败,请查看日志");
//                return;
//            }

//            this.ItemsSource.RemoveAll(l => l.IsSelected);
//        }

//        public virtual async Task<TResult> InvokeMethod<TResult>(string methodName, params object[] parameters)
//        {
//            Type type = typeof(T);

//            return await type.InvokeMethod<TResult>(methodName, parameters);

//        }

//        protected override void RelayMethod(object obj)
//        {
//            string command = obj.ToString();

//            //  Do：应用
//            if (command == "Sumit")
//            {


//            }
//            //  Do：取消
//            else if (command == "Cancel")
//            {


//            }
//        }

//        #endregion
//    }

//}
