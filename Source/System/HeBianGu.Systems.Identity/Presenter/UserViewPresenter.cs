using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows.Controls;
using System.Xml.Serialization;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Systems.Identity
{
    public interface IUserViewPresenterOption : IWindowComponentViewPresenterOption
    {

    }

    [Displayer(Name = "用户管理", GroupName = SystemSetting.GroupAuthority, Icon = "\xe84a", Description = "应用此功能进行用户管理")]
    public class UserViewPresenter : WindowComponentViewPresenter<UserViewPresenter, IUserViewPresenter>, IUserViewPresenter, IUserViewPresenterOption, IAuthority
    {
        PropertyGridViewPresenter _property = new PropertyGridViewPresenter();
        public UserViewPresenter()
        {
            //this.Toolbars.Add(new AddUserViewPresenter());
            this.SideEidts.Add(_property);
        }

        public override void Load()
        {
            base.Load();
            if (this.Repository == null) return;
            foreach (var item in this.Repository.GetList("Role"))
            {
                IUser user = new User(item);
                this.Collection.Add(user);
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public IUserRespository Repository => ServiceRegistry.Instance.GetInstance<IUserRespository>();

        [Browsable(false)]
        [XmlIgnore]
        public AuthorityCommand AddCommand => new AuthorityCommand(async (s, e) =>
        {
            if (e is User project)
            {
                if (this.Collection.Any(x => x.Account == project.Account))
                {
                    MessageProxy.Snacker.Show("添加失败，已存在当前名称用户");
                    return;
                }
                await this.Add(project);
                OparationViewPresenterProxy.Instance?.Log(project.Account, project.ID, s.Name);
            }
        }, "{9A515858-9E16-434B-8304-851AF5858EEE}", "新增用户")
        { GroupName = this.Name };

        protected virtual async Task Add(User u)
        {
            User user = new User() { Account = u.Account, Password = u.Password, Role = u.Role };
            if (this.Repository != null)
                await this.Repository.InsertAsync(user.Model);
            this.Collection.Add(user);
            return;
        }

        protected virtual async Task Delete(IUser u)
        {
            if (this.Repository != null)
                await this.Repository.DeleteByIDAsync(u.ID);
            this.Collection.Remove(u);
            return;
        }
        [Browsable(false)]
        [XmlIgnore]
        public AuthorityTransactionCommand EditCommand => new AuthorityTransactionCommand(async (s, e) =>
        {
            if (e is IUser project)
            {
                string name = project.Name;
                string password = project.Password;
                string account = project.Account;
                var r = await s.BeginEditAsync(() =>
                {
                    if (this.Collection.Where(x => x != project).Any(x => x.Account == project.Account))
                    {
                        MessageProxy.Snacker.Show("保存失败，已存在当前登录账号用户");
                        return false;
                    }
                    if (project.ModelState(out List<string> message) == false)
                    {
                        MessageProxy.Snacker.Show(message?.FirstOrDefault());
                        return false;
                    }
                    OparationViewPresenterProxy.Instance?.Log(project.Account, project.ID, s.Name);
                    return true;
                });
                if (r)
                {
                    if (this.Repository != null)
                        this.Repository.Save();
                }
                else
                {
                    project.Name = name;
                    project.Account = account;
                    project.Password = password;
                }
            }
        }, "{CC6DA5D1-3174-4EB9-8643-89AE1B7DED35}", "修改用户")
        { GroupName = this.Name };

        [Browsable(false)]
        [XmlIgnore]
        public AuthorityCommand DeleteCommand => new AuthorityCommand(async (s, e) =>
        {
            if (e is IUser project)
            {
                await this.Delete(project);
                OparationViewPresenterProxy.Instance?.Log(project.Account, project.ID, s.Name);
            }
        }, "{55596A7E-E62D-4732-9592-FAE70AC91AC5}", "删除用户")
        { GroupName = this.Name };

        private ObservableCollection<IUser> _collection = new ObservableCollection<IUser>();
        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<IUser> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }

        //private IUser _selectedItem;
        //[XmlIgnore]
        //[Browsable(false)]
        //public IUser SelectedItem
        //{
        //    get { return _selectedItem; }
        //    set
        //    {
        //        _selectedItem = value;
        //        RaisePropertyChanged();
        //        _property.Data = value;
        //    }
        //}

        public IEnumerable<IUser> GetUsers(Predicate<IUser> predicate = null)
        {
            if (predicate?.Invoke(AdminUser.Instance) == true)
                yield return AdminUser.Instance;

            var finds = this.Collection.Where(x => predicate?.Invoke(x) != false);
            if (finds != null)
            {
                foreach (var find in finds)
                {
                    yield return find;
                }
            }
        }

        [Browsable(false)]
        public bool IsAuthority => Loginner.Instance?.User?.IsValid(this.ID) != false;

    }

    //[Displayer(Name = "添加用户", GroupName = SystemSetting.GroupSystem, Icon = Icons.Add, Description = "这是一个关于页面的信息")]
    //public class AddUserViewPresenter : InvokePresenterBase
    //{
    //    public override bool Invoke(out string message)
    //    {
    //        message = null;
    //        return true;
    //    }
    //}


    public class UserViewPresenterProxy : ServiceInstance<IUserViewPresenter>
    {

    }
}


