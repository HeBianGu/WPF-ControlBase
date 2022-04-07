// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeBianGu.Service.Mvc
{
    public abstract class SourceMvcViewModelBase<T> : MvcEntityViewModelBase<T> where T : new()
    {
        public RelayCommand AddCommand => new RelayCommand(async l => await AddMethod(l));

        public RelayCommand EditCommand => new RelayCommand(async l => await EditMethod(l), l => this.SelectedItem != null);

        public RelayCommand DeleteCommand => new RelayCommand(async l =>
        {
            if (this.SelectedItem == null)
            {
                Message.Instance.ShowSnackMessageWithNotice("没有选择数据");
                return;
            }

            Tuple<bool, string> r = await this.Delete();
            string error = r.Item2;
            if (r.Item1)
            {
                Message.Instance.ShowSnackMessageWithNotice("删除成功");
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
                Message.Instance.ShowSnackMessageWithNotice("删除失败," + error);
            }
        }, l => this.SelectedItem != null);

        public RelayCommand InfoCommand => new RelayCommand(async l => await InfoMethod(l), l => this.SelectedItem != null);

        public RelayCommand ClearCommand => new RelayCommand(async l =>
        {
            Tuple<bool, string> r = await this.Clear();
            string error = r.Item2;
            if (r.Item1)
            {
                Message.Instance.ShowSnackMessageWithNotice("清空成功");
                this.Collection.Clear();
            }
            else
            {
                Message.Instance.ShowSnackMessageWithNotice("清空失败," + error);
            }

        }, l => this.Collection != null && this.Collection.Count > 0);

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
                    Message.Instance.ShowSnackMessageWithNotice(errors?.FirstOrDefault());
                    return false;
                }

                return true;
            };

            bool dialog = await Message.Instance.ShowObjectWithContent(vm, match, "新增数据", l =>
            {
                l.DataContext = this;
            });

            if (!dialog)
            {
                Message.Instance.ShowSnackMessageWithNotice("用户取消");
                return;
            }

            Tuple<bool, string> r = await this.Add(vm);
            string error = r.Item2;

            if (r.Item1)
            {
                Message.Instance.ShowSnackMessageWithNotice("新增成功");
                this.Collection.Add(vm);
            }
            else
            {
                Message.Instance.ShowSnackMessageWithNotice("新增失败," + error);
            }
        }

        protected virtual async Task EditMethod(object obj)
        {
            if (this.SelectedItem == null)
            {
                Message.Instance.ShowSnackMessageWithNotice("没有选择数据");
                return;
            }

            if (this.SelectedItem == null)
            {
                Message.Instance.ShowSnackMessageWithNotice("没有选择数据");
                return;
            }

            Predicate<T> match = k =>
            {
                if (!k.ModelState(out List<string> errors))
                {
                    Message.Instance.ShowSnackMessageWithNotice(errors?.FirstOrDefault());
                    return false;
                }

                return true;
            };

            T t = this.GetEditModel();

            bool dialog = await Message.Instance.ShowObjectWithContent(t, match, "新增数据", l => l.DataContext = this);

            if (!dialog)
            {
                Message.Instance.ShowSnackMessageWithNotice("用户取消");
                return;
            }

            Tuple<bool, string> r = await this.Edit();
            string error = r.Item2;
            if (!r.Item1)
            {
                Message.Instance.ShowSnackMessageWithNotice("保存失败," + error);
            }
        }

        protected virtual async Task InfoMethod(object obj)
        {
            if (this.SelectedItem == null)
            {
                Message.Instance.ShowSnackMessageWithNotice("没有选择数据");
                return;
            }


            object t = this.GetInfoModel();

            await Message.Instance.ShowObjectWithContent(t, null, "详情", l => l.DataContext = this);

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
                this.Collection.Foreach(K => K.Selected = b);

                this.IsCheckedAll = b;
            }
        }, l => this.Collection != null && this.Collection.Count > 0);

        public RelayCommand DeleteCheckedCommand => new RelayCommand(async l =>
        {
            List<M> checks = this.Collection.Where(k => k.Selected)?.ToList();

            Tuple<bool, string> r = await this.DeleteChecked(checks);

            string error = r.Item2;

            if (r.Item1)
            {
                Message.Instance.ShowSnackMessageWithNotice($"删除成功,共计{checks.Count()}条");
                this.Collection.Clear();
            }
            else
            {
                Message.Instance.ShowSnackMessageWithNotice("删除失败," + r.Item2);
            }

        }, l =>
        {
            bool r = this.Collection.Any(k => k.Selected);
            return r;
        });
    }
}
