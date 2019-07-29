using HeBianGu.Base.WpfBase;
using HeBianGu.ExplorePlayer.Base.Model;
using HeBianGu.ExplorePlayer.General.SqliteDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfMvc
{

    public abstract class ExtendEntityBaseController<T, M, R1, R2> : EntityBaseController<T, M, R1> where R1 : IStringRepository<T>
         where M : MvcEntityViewModelBase<T>
         where T : StringEntityBase, new()
    {
        public R2 Extend { get; set; } = ServiceRegistry.Instance.GetInstance<R2>();
    }

    /// <summary> 带有具体列表的Controler基类 T=实体类型 M = ViewModel R = Resistory </summary>
    public abstract class EntityBaseController<T, M, R> : Controller<M, R> where R : IStringRepository<T>
         where M : MvcEntityViewModelBase<T>
         where T : StringEntityBase, new()
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
                    this.ViewModel.Collection.Add(item);
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

            await this.Respository.InsertAsync(this.ViewModel.AddItem);

            return await List();
        }


        public virtual async Task<IActionResult> Add()
        {
            this.ViewModel.AddItem = new T();

            return View();
        }

        public virtual async Task<IActionResult> Edit()
        {
            return View();
        }

        public virtual async Task<IActionResult> Delete()
        {
            await this.Respository.DeleteAsync(this.ViewModel.SeletItem?.ID);

            Application.Current.Dispatcher.Invoke(() =>
            {
                this.ViewModel.Collection.Remove(this.ViewModel.SeletItem);
            });

            return await List();
        }

        public virtual async Task<IActionResult> Update()
        {
            string message;

            if (!this.ModelState(this.ViewModel.SeletItem, out message))
            {
                return await Edit();
            }

            await this.Respository.UpdateAsync(this.ViewModel.SeletItem);

            return await List();
        }

        public virtual async Task<IActionResult> Detial()
        {
            return View();
        }
    }

    public abstract class Controller<T, R1, R2> : Controller<T, R1> where T : MvcViewModelBase where R1 : IRepository where R2 : IRepository
    {
        public R2 Extend { get; set; } = ServiceRegistry.Instance.GetInstance<R2>();

    }

    public abstract class Controller<M, R> : Controller<M> where M : MvcViewModelBase where R : IRepository
    {
        public R Respository { get; set; } = ServiceRegistry.Instance.GetInstance<R>();

    }

    public abstract class Controller<M> : Controller where M : MvcViewModelBase
    {
        public M ViewModel { get; set; } = ServiceRegistry.Instance.GetInstance<M>();

        public bool ModelState(object obj, out string message)
        {
            var results = ObjectPropertyFactory.ModelState(obj);

            message = results.FirstOrDefault();

            return results.Count == 0;
        }

        public bool ModelState(object obj)
        {
            string message;

            return this.ModelState(obj, out message);
        }
    }

    public abstract class Controller : ControllerBase
    {

    }

    public abstract class ControllerBase : IController
    {
        protected virtual IActionResult View([CallerMemberName] string name = "")
        {

            var route = this.GetType().GetCustomAttributes(typeof(RouteAttribute), true).Cast<RouteAttribute>();

            string controlName = null;

            if (route.FirstOrDefault() == null)
            {
                controlName = this.GetType().Name;
            }
            else
            {
                controlName = route.FirstOrDefault().Name;
            }

            var ass = Assembly.GetEntryAssembly().GetName();

            string path = $"/{ass.Name};component/View/{controlName}/{name}Control.xaml";

            Uri uri = new Uri(path, UriKind.RelativeOrAbsolute);



            var content = Application.Current.Dispatcher.Invoke(() =>
            {
                return Application.LoadComponent(uri);
            });

            ActionResult result = new ActionResult();

            result.Uri = uri;
            result.View = content as ContentControl;

            Type type = Assembly.GetEntryAssembly().GetTypeOfMatch<NotifyPropertyChanged>(l => l.Name == controlName + "ViewModel");

            result.ViewModel = ServiceRegistry.Instance.GetInstance(type);

            Application.Current.Dispatcher.Invoke(() =>
            {
                result.View.DataContext = result.ViewModel;

            });

            return result;
        }

    }





}
