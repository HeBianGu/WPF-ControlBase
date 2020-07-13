using HeBianGu.Base.WpfBase;
using HeBianGu.Common.DataBase;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Domain.MvcRespository
{
    /// <summary> 带有具体列表的Controler基类 T=实体类型 M = ViewModel R = Resistory E=实体类型的ViewModel封装 </summary>
    public abstract class ViewModelEntityController<Model, ViewModel, Repository, EntityViewModel> : Controller<ViewModel, Repository> where Repository : IStringRepository<Model>
         where ViewModel : MvcEntityViewModelBase<EntityViewModel>
         where Model : StringEntityBase, new()
         where EntityViewModel : ModelViewModel<Model>, new()
    {
        /// <summary> 跳转列表页面 </summary>
        public virtual async Task<IActionResult> List()
        {
            var models = await this.Respository.GetListAsync();

            if (models == null)
            {
                return View();
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                this.ViewModel.Collection.Clear();

                foreach (var item in models)
                {
                    EntityViewModel viewModel = Activator.CreateInstance(typeof(EntityViewModel), new object[] { item }) as EntityViewModel;

                    this.ViewModel.Collection.Add(viewModel);
                }
            });

            return View();
        }

        /// <summary> 跳转到中心 </summary>
        public virtual async Task<IActionResult> Center()
        {
            return View();
        }

        public virtual async Task<IActionResult> Insert()
        {
            string message;

            if (!this.ModelState(this.ViewModel.AddItem, out message))
            {
                return await Add();
            }

            await this.Respository.InsertAsync(this.ViewModel.AddItem.Model);

            return await List();
        }


        public virtual async Task<IActionResult> Add()
        {
            this.ViewModel.AddItem = new EntityViewModel();

            return View();
        }

        public virtual async Task<IActionResult> Edit()
        {
            return View();
        }

        public virtual async Task<IActionResult> Delete()
        {
            await this.Respository.DeleteAsync(this.ViewModel.SelectedItem?.Model.ID);

            Application.Current.Dispatcher.Invoke(() =>
            {
                this.ViewModel.Collection.Remove(this.ViewModel.SelectedItem);
            });

            return await List();
        }

        public virtual async Task<IActionResult> Update()
        {
            string message;

            if (!this.ModelState(this.ViewModel.SelectedItem, out message))
            {
                return await Edit();
            }

            await this.Respository.UpdateAsync(this.ViewModel.SelectedItem?.Model);

            return await List();
        }

        public virtual async Task<IActionResult> Detial()
        {
            return View();
        }
    }

    /// <summary> 带有具体列表的Controler基类 T=实体类型 M = ViewModel R = Resistory </summary>
    public abstract class EntityBaseController<Model, ViewModel, Repository> : Controller<ViewModel, Repository> where Repository : IStringRepository<Model>
         where ViewModel : MvcEntityViewModelBase<Model>
         where Model : StringEntityBase, new()
    {
        /// <summary> 跳转列表页面 </summary>
        public virtual async Task<IActionResult> List()
        {
            ////  Do ：异步加载数据
            // this.ViewModel.RunAsync(() =>
            // {
            //     var source = this.Respository.GetListAsync();

            //     this.ViewModel.Collection = source?.Result?.ToObservable();
            // });

            await Task.Run(async () =>
             {
                 var source = await this.Respository.GetListAsync();

                 var sss = source?.ToObservable();

                 this.ViewModel.Collection = sss;
             });

            //this.ViewModel.RunAsync(()=>
            //{
            //    this.ViewModel.Collection.Invoke(l => l.Clear());

            //    foreach (var item in source)
            //    {
            //        this.ViewModel.Collection.Invoke(l => l.Add(item));

            //        Thread.Sleep(10);
            //    }
            //});

            return await ViewAsync();
        }

        /// <summary> 跳转到中心 </summary>
        public virtual async Task<IActionResult> Center()
        {
            return View();
        }

        public virtual async Task<IActionResult> Insert()
        {
            string message;

            if (!this.ModelState(this.ViewModel.AddItem, out message))
            {
                return await Add();
            }

            await this.Respository.InsertAsync(this.ViewModel.AddItem);

            return await List();
        }


        public virtual async Task<IActionResult> Add()
        {
            this.ViewModel.AddItem = new Model();

            return View();
        }

        public virtual async Task<IActionResult> Edit()
        {
            return View();
        }

        public virtual async Task<IActionResult> Delete()
        {
            await this.Respository.DeleteAsync(this.ViewModel.SelectedItem?.ID);

            Application.Current.Dispatcher.Invoke(() =>
            {
                this.ViewModel.Collection.Remove(this.ViewModel.SelectedItem);
            });

            return await List();
        }

        public virtual async Task<IActionResult> Update()
        {
            string message;

            if (!this.ModelState(this.ViewModel.SelectedItem, out message))
            {
                return await Edit();
            }

            await this.Respository.UpdateAsync(this.ViewModel.SelectedItem);

            return await List();
        }

        public virtual async Task<IActionResult> Detial()
        {
            return View();
        }
    }

    public abstract class Controller<M, R> : Controller<M> where M : MvcViewModelBase where R : IRepository
    {
        public R Respository { get; set; } = ServiceRegistry.Instance.GetInstance<R>();

    }
}
