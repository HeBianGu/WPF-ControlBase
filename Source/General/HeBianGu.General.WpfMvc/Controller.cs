using HeBianGu.Base.WpfBase;
using HeBianGu.Common.DataBase;
using HeBianGu.Common.PublicTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace HeBianGu.General.WpfMvc
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
            await this.Respository.DeleteAsync(this.ViewModel.SeletItem?.Model.ID);

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

            await this.Respository.UpdateAsync(this.ViewModel.SeletItem?.Model);

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
            this.ViewModel.AddItem = new Model();

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
        public Dispatcher Dispatcher
        {
            get
            {
                return Application.Current.Dispatcher;
            }
        }

        public T Invoke<T>(Func<T> func)
        {
            return this.Dispatcher.Invoke(func);
        }

        public void Invoke(Action action)
        {
            this.Dispatcher.Invoke(action);
        }
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
                result.View.Cast<FrameworkElement>().DataContext = result.ViewModel;

            });

            return result;
        }


        protected virtual IActionResult LinkAction([CallerMemberName] string name = "")
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
            result.View = content;

            Type type = Assembly.GetEntryAssembly().GetTypeOfMatch<NotifyPropertyChanged>(l => l.Name == controlName + "ViewModel");

            result.ViewModel = ServiceRegistry.Instance.GetInstance(type);

            Application.Current.Dispatcher.Invoke(() =>
            {
                result.View.Cast<FrameworkElement>().DataContext = result.ViewModel;

            });

            return result;
        }

    }


    /// <summary> 用于记录带有输出日志的Controller类 </summary>
    public class MvcLogControllerBase<T> : Controller<T> where T : MvcViewModelBase
    {
        IMvcLog _mvcLog = null;

        public MvcLogControllerBase(IMvcLog shareViewModel)
        {
            _mvcLog = shareViewModel;
        }

        public void RunLog(string title, string message)
        {
            _mvcLog?.RunLog(title, message);
        }

        public void ErrorLog(string title, string message)
        {
            _mvcLog?.ErrorLog(title, message);
        }

        public void OutPutLog(string title, string message)
        {
            _mvcLog?.OutPutLog(title, message);
        }

        protected override IActionResult View([CallerMemberName] string name = "")
        {
            this.OutPutLog("跳转页面", $"调用控制器{this.GetType().Name},跳转到页面{name}");

            return base.View(name);
        }
    }

    public class MvcNavigationControllerBase<T> : MvcLogControllerBase<T> where T : MvcViewModelBase
    {

        public MvcNavigationControllerBase(IMvcLog imvclog) : base(imvclog)
        {

        }

        protected override IActionResult View([CallerMemberName] string name = "")
        {
            if (this.GetType().GetMethod(name).GetCustomAttributes(typeof(RouteAttribute), true).FirstOrDefault() is RouteAttribute route)
            {
                var routes = route.Name.Split('/');

                List<ILinkActionBase> result = new List<ILinkActionBase>();

 
                foreach (var item in routes)
                {
                    ILinkActionBase link = this.ViewModel.GetLinkAction(item);

                    result.Add(link);  
                }

                this.ViewModel.Navigation = result.ToObservable();
            }

            return base.View(name);
        }
    }






}
