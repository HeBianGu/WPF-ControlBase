// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace HeBianGu.Service.Mvc
{
    public abstract class SourceMvcViewModelBase<T> : MvcEntityViewModelBase<T> where T : new()
    {
        public RelayCommand AddCommand => new RelayCommand(async l => await AddMethod(l));

        public RelayCommand EditCommand => new RelayCommand(async l => await EditMethod(l), l => this.SelectedItem != null);

        public RelayCommand DeleteCommand => new RelayCommand(async l => await DeleteMethod(l), l => this.SelectedItem != null);

        public RelayCommand InfoCommand => new RelayCommand(async l => await InfoMethod(l), l => this.SelectedItem != null);

        public RelayCommand ClearCommand => new RelayCommand(async l => await ClearMethod(l), l => this.Collection != null && this.Collection.Count > 0);

        public RelayCommand SaveCommand => new RelayCommand(async l =>
        {
            Tuple<bool, string> r = await this.Save();
            string error = r.Item2;
            if (r.Item1)
            {
                MessageProxy.Snacker.ShowTime("保存成功");
            }
            else
            {
                MessageProxy.Snacker.ShowTime("保存失败," + error);
            }

        });

        public RelayCommand ExportCommand => new RelayCommand(async l =>
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "文本文件(*.xml)|*.xml|所有文件|*.*";
            saveFileDialog.FileName = typeof(T).Name + "s";
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.AddExtension = true;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "保存文件";
            if (saveFileDialog.ShowDialog() != true)
                return;

            await this.Export(saveFileDialog.FileName);


        }, l => this.Collection != null && this.Collection.Count > 0);

        public RelayCommand ImportCommand => new RelayCommand(async l =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            openFileDialog.Filter = "文本文件(*.xml)|*.xml|所有文件|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.Title = "打开文件";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() != true)
                return;

            await this.Import(openFileDialog.FileName);
        });

        protected virtual async Task Export(string path)
        {
            await Task.Run(() => XmlSerialize.Instance.Save(path, this.Collection));
        }

        protected virtual async Task Import(string path)
        {
            await MessageProxy.Messager.ShowWaitter(() =>
             {
                 var models = XmlSerialize.Instance.Load<ObservableCollection<T>>(path);

                 this.Collection.AddRange(models);
             });
        }

        protected virtual T GetAddModel()
        {
            return new T();
        }

        protected virtual T GetEditModel()
        {
            return this.SelectedItem;
        }
        protected virtual object GetInfoModel()
        {
            return this.SelectedItem;
        }

        protected virtual async Task AddMethod(object obj)
        {
            T vm = this.GetAddModel();

            Predicate<T> match = k =>
            {
                if (!k.ModelState(out List<string> errors))
                {
                    MessageProxy.Snacker.ShowTime(errors?.FirstOrDefault());
                    return false;
                }

                return true;
            };

            bool dialog = await MessageProxy.Presenter.Show(vm, match, "新增", l =>
            {
                l.DataContext = this;
            });

            if (!dialog)
            {
                MessageProxy.Snacker.ShowTime("用户取消");
                return;
            }

            Tuple<bool, string> r = await this.Add(vm);
            string error = r.Item2;

            if (r.Item1)
            {
                MessageProxy.Snacker.ShowTime("新增成功");
                this.Collection.Add(vm);
            }
            else
            {
                MessageProxy.Snacker.ShowTime("新增失败," + error);
            }

            this.OnCollectionChanged(obj);
        }

        protected virtual async Task EditMethod(object obj)
        {
            if (this.SelectedItem == null)
            {
                MessageProxy.Snacker.ShowTime("没有选择数据");
                return;
            }

            if (this.SelectedItem == null)
            {
                MessageProxy.Snacker.ShowTime("没有选择数据");
                return;
            }

            Predicate<T> match = k =>
            {
                if (!k.ModelState(out List<string> errors))
                {
                    MessageProxy.Snacker.ShowTime(errors?.FirstOrDefault());
                    return false;
                }

                return true;
            };

            T t = this.GetEditModel();

            bool dialog = await MessageProxy.Presenter.Show(t, match, "编辑", l => l.DataContext = this);

            if (!dialog)
            {
                MessageProxy.Snacker.ShowTime("用户取消");
                return;
            }

            Tuple<bool, string> r = await this.Edit();
            string error = r.Item2;
            if (!r.Item1)
            {
                MessageProxy.Snacker.ShowTime("保存失败," + error);
            }
        }

        protected virtual async Task DeleteMethod(object obj)
        {
            if (this.SelectedItem == null)
            {
                MessageProxy.Snacker.ShowTime("没有选择数据");
                return;
            }

            var result = await MessageProxy.Messager.ShowResult("确定删除数据？");
            if (!result)
                return;

            Tuple<bool, string> r = await this.Delete();
            string error = r.Item2;
            if (r.Item1)
            {
                MessageProxy.Snacker.ShowTime("删除成功");
                int index = this.Collection.IndexOf(this.SelectedItem);
                this.Collection.Remove(this.SelectedItem);

                if (index == 0)
                {
                    this.SelectedItem = this.Collection.FirstOrDefault();
                    return;
                }

                this.SelectedItem = this.Collection.Count < index - 1 ? this.Collection.FirstOrDefault() : this.Collection[index - 1];
            }
            else
            {
                MessageProxy.Snacker.ShowTime("删除失败," + error);
            }

            this.OnCollectionChanged(obj);
        }

        protected virtual async Task ClearMethod(object obj)
        {
            var result = await MessageProxy.Messager.ShowResult("确定清空数据？");
            if (!result)
                return;

            Tuple<bool, string> r = await this.Clear();
            string error = r.Item2;
            if (r.Item1)
            {
                MessageProxy.Snacker.ShowTime("清空成功");
                this.Collection.Clear();
            }
            else
            {
                MessageProxy.Snacker.ShowTime("清空失败," + error);
            }

            this.OnCollectionChanged(obj);
        }


        protected virtual async Task InfoMethod(object obj)
        {
            if (this.SelectedItem == null)
            {
                MessageProxy.Snacker.ShowTime("没有选择数据");
                return;
            }


            object t = this.GetInfoModel();

            await MessageProxy.Presenter.Show(t, null, "详情", l => l.DataContext = this);

            this.OnCollectionChanged(obj);
        }

        protected virtual void OnCollectionChanged(object obj)
        {

        }


        /// <summary> 添加 </summary>
        protected abstract Task<Tuple<bool, string>> Add(T t);

        /// <summary> 删除 </summary>
        protected abstract Task<Tuple<bool, string>> Delete();

        /// <summary> 删除选中项 </summary>
        protected abstract Task<Tuple<bool, string>> DeleteChecked(List<T> checks);

        /// <summary> 清空 </summary>
        protected abstract Task<Tuple<bool, string>> Clear();

        /// <summary> 编辑 </summary>
        protected abstract Task<Tuple<bool, string>> Edit();

        /// <summary> 保存 </summary>
        protected abstract Task<Tuple<bool, string>> Save();

    }

    public abstract class SelectedSourceMvcViewModelBase<M> : SourceMvcViewModelBase<M> where M : ISelectViewModel, new()
    {
        private bool _isCheckedAll;
        /// <summary> 说明  </summary>
        public bool IsCheckedAll
        {
            get { return _isCheckedAll; }
            set
            {
                _isCheckedAll = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand CheckedAllCommand => new RelayCommand(l =>
        {
            if (l is Boolean b)
            {
                this.Collection.Foreach(K => K.IsSelected = b);

                this.IsCheckedAll = b;
            }
        }, l => this.Collection != null && this.Collection.Count > 0);

        public RelayCommand DeleteCheckedCommand => new RelayCommand(async l => await DeleteCheckedMethod(l), l =>
        {
            bool r = this.Collection.Any(k => k.IsSelected);
            return r;
        });


        protected virtual async Task DeleteCheckedMethod(object obj)
        {
            var result = await MessageProxy.Messager.ShowResult("确定删除数据？");
            if (!result)
                return;

            List<M> checks = this.Collection.Where(k => k.IsSelected)?.ToList();

            Tuple<bool, string> r = await this.DeleteChecked(checks);

            string error = r.Item2;

            if (r.Item1)
            {
                MessageProxy.Snacker.ShowTime($"删除成功,共计{checks.Count()}条");
                this.Collection.Clear();
            }
            else
            {
                MessageProxy.Snacker.ShowTime("删除失败," + r.Item2);
            }

            this.OnCollectionChanged(obj);
        }
    }
}
