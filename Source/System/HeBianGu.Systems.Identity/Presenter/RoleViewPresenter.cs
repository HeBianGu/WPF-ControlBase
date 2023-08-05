// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Identity
{

    public interface IRoleViewPresenterOption : ITreeViewItemPresenterOption
    {

    }

    [Displayer(Name = "角色管理", GroupName = SystemSetting.GroupAuthority, Icon = "\xe904", Description = "应用此功能进行角色管理")]
    public class RoleViewPresenter : TreeViewItemPresenter<RoleViewPresenter, IRoleViewPresenter>, IRoleViewPresenter, IRoleViewPresenterOption, IAuthority
    {
        [Browsable(false)]
        [XmlIgnore]
        IRoleRespository Repository => ServiceRegistry.Instance.GetInstance<IRoleRespository>();
        public override void Load()
        {
            base.Load();

            if (this.Repository == null) return;
            foreach (var item in this.Repository.GetList("Authors"))
            {
                Role user = new Role(item);
                this.Roles.Add(user);
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public AuthorityCommand AddCommand => new AuthorityCommand(async (s, e) =>
        {
            if (e is IRole project)
            {
                if (this.Roles.Any(x => x.Name == project.Name))
                {
                    MessageProxy.Snacker.Show("添加失败，已存在当前名称角色");
                    return;
                }
                await this.Add(project);
                OparationViewPresenterProxy.Instance?.Log(project.Name, project.ID, s.Name);

            }
        }, "44FC6120-6559-4DE5-B938-DBF218299AB2", "添加角色")
        { GroupName = this.Name };

        protected virtual async Task Add(IRole u)
        {
            Role user = new Role() { Name = u.Name };

            if (this.Repository != null)
                await this.Repository.InsertAsync(user.Model);
            this.Roles.Add(user);
            return;
        }

        protected virtual async Task Delete(Role u)
        {
            if (this.Repository != null)
                await this.Repository.DeleteAsync(u.Model);
            this.Roles.Remove(u);
            return;
        }

        protected virtual async Task Save()
        {
            if (this.Repository != null)
                await this.Repository.SaveAsync();
            return;
        }
        [Browsable(false)]
        [XmlIgnore]
        public AuthorityCommand DeleteCommand => new AuthorityCommand(async (s, e) =>
        {
            if (e is Role project)
            {
                await this.Delete(project);
                OparationViewPresenterProxy.Instance?.Log(project.Name, project.ID, s.Name);
            }
        }, "{71BAB79A-6549-4C18-9B8C-8A67831773F2}", "删除角色")
        { GroupName = this.Name };

        [Browsable(false)]
        [XmlIgnore]
        public RelayCommand SelecttionChangedCommand => new RelayCommand((s, e) =>
        {
            if (this.SelectedItem == null) return;

            foreach (var item in this.Authorities)
            {
                item.Foreach(x =>
                {
                    x.IsChecked = this.SelectedItem.Model.Authors?.Any(c => c.AuthorCode == x.Model.ID) == true;
                });
            }
        });

        [Browsable(false)]
        [XmlIgnore]
        public AuthorityCommand EditCommand => new AuthorityCommand(async (s, e) =>
        {
            this.SelectedItem.Model.Authors.Clear();
            foreach (var item in this.Authorities)
            {
                item.Foreach(x =>
                {
                    if (x.IsChecked == true)
                    {
                        hi_dd_author author = new hi_dd_author();
                        author.AuthorCode = x.Model.ID;
                        author.Name = x.Model.Name;
                        this.SelectedItem.Model.Authors.Add(author);
                    }
                });
            }
            if (this.Repository != null)
                await this.Repository.SaveAsync();
            OparationViewPresenterProxy.Instance?.Log(this.SelectedItem.Name, this.SelectedItem.ID, s.Name);

        }, (s, e) => this.SelectedItem != null, "{E83B95F6-4158-4A19-8B5D-FCB4EBCCEFF3}", "编辑角色")
        { GroupName = this.Name };

        [Browsable(false)]
        [XmlIgnore]
        public AuthorityCommand DeleteAuthorCommand => new AuthorityCommand(async (s, e) =>
        {
            if (this.SelectedItem == null) return;
            if (e is hi_dd_author author)
            {
                this.SelectedItem.Model.Authors.Remove(author);
            }
            foreach (var item in this.Authorities)
            {
                item.Foreach(x =>
                {
                    x.IsChecked = this.SelectedItem.Model.Authors.Any(c => c.AuthorCode == x.Model.ID);
                });
            }
            if (this.Repository != null)
                await this.Repository.SaveAsync();
            OparationViewPresenterProxy.Instance?.Log(this.SelectedItem.Name, this.SelectedItem.ID, s.Name);

        }, (s, e) => this.SelectedItem != null, "{1BD8755D-8217-4221-AFF8-1CA0F2DAD4EA}", "移除角色权限")
        { GroupName = this.Name };

        private ObservableCollection<Role> _roles = new ObservableCollection<Role>();
        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<Role> Roles
        {
            get { return _roles; }
            set
            {
                _roles = value;
                RaisePropertyChanged();
            }
        }

        private Role _selectedItem;
        [Browsable(false)]
        [XmlIgnore]
        public Role SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<TreeNodeBase<IAuthority>> _authorities = new ObservableCollection<TreeNodeBase<IAuthority>>();
        [Browsable(false)]
        [XmlIgnore]
        public ObservableCollection<TreeNodeBase<IAuthority>> Authorities
        {
            get
            {
                if (_authorities == null || _authorities.Count == 0)
                {
                    _authorities = this.GetAuthors().ToObservable();
                }
                return _authorities;
            }
            set
            {
                _authorities = value;
                RaisePropertyChanged();
            }
        }

        IEnumerable<TreeNodeBase<IAuthority>> GetAuthors()
        {
            var groups = SystemDisplay.Instance.Settings.OfType<IAuthority>().Concat(AuthoritySetting.Instance.Authoritys).GroupBy(x => x.GroupName);
            List<TreeNodeBase<IAuthority>> roots = new List<TreeNodeBase<IAuthority>>();
            foreach (var group in groups)
            {
                Authority authorityGroup = new Authority()
                {
                    Name = group.Key,
                };

                var root = new TreeNodeBase<IAuthority>(authorityGroup) { IsExpanded = true };
                foreach (var item in group)
                {
                    var note = new TreeNodeBase<IAuthority>(item) { IsExpanded = true }; ;
                    root.Nodes.Add(note);
                    var ps = item.GetType().GetProperties().Where(x => typeof(IAuthority).IsAssignableFrom(x.PropertyType));
                    foreach (var p in ps)
                    {
                        IAuthority value = p.GetValue(item) as IAuthority;
                        var pNode = new TreeNodeBase<IAuthority>(value) { IsExpanded = true }; ;
                        note.Nodes.Add(pNode);
                    }
                }
                roots.Add(root);
            }
            return roots;
        }

        public IEnumerable<IRole> GetRoles(Predicate<IRole> predicate = null)
        {
            var finds = this.Roles.Where(x => predicate?.Invoke(x) != false);
            foreach (var item in finds)
            {
                yield return item;
            }
        }
        [Browsable(false)]
        public bool IsAuthority => Loginner.Instance?.User?.IsValid(this.ID) != false;

    }
}
