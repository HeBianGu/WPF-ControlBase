// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace HeBianGu.Base.WpfBase
{
    //public abstract class AuthorityNotifyPropertyChangedBase : NotifyPropertyChangedBase, IAuthority
    //{
    //    public AuthorityNotifyPropertyChangedBase()
    //    {
    //        var authority = this.GetType().GetCustomAttribute<AuthorityAttribute>();
    //        if (authority != null)
    //        {
    //            this.AuthorID = authority.ID ?? this.AuthorID;
    //            this.AuthorName = authority.Name ?? this.AuthorName;
    //            this.AuthorPredefinePath = authority.PredefinePath;
    //        }
    //    }

    //    [XmlIgnore]
    //    [Browsable(false)]
    //    public string AuthorID { get; set; }
    //    [XmlIgnore]
    //    [Browsable(false)]
    //    public string AuthorName { get; set; }

    //    [XmlIgnore]
    //    [Browsable(false)]
    //    public string AuthorGroupName { get; set; }

    //    [XmlIgnore]
    //    [Browsable(false)]
    //    public string AuthorPredefinePath { get; set; }
    //    //private bool _isAuthority = true;
    //    [XmlIgnore]
    //    //[Browsable(false)]
    //    public bool IsAuthority => Loginner.Instance?.User?.IsValid(this.AuthorID) != false;
    //    //{
    //    //    get
    //    //    {
    //    //        return Loginner.Instance?.User?.IsValid(this.AuthorID)!=false;
    //    //    }
    //    //}

    //    //protected virtual void LoadAuthority()
    //    //{
    //    //    if (!AuthoritySetting.Instance.UseAuthority) return;
    //    //    {
    //    //        var authority = this.GetType().GetCustomAttribute<AuthorityAttribute>();
    //    //        if (authority != null)
    //    //        {
    //    //            this.AuthorID = authority.ID ?? this.AuthorID;
    //    //            this.AuthorName = authority.Name ?? this.AuthorName;
    //    //            this.AuthorPredefinePath = authority.PredefinePath;
    //    //        }

    //    //        //var check = Loginner.Instance?.User?.IsValid(this.AuthorID);
    //    //        //if (check == false)
    //    //        //    this.IsAuthority = false;
    //    //    }

    //    //    //{
    //    //    //    var ps = this.GetType().GetProperties().Where(x => typeof(IAuthority).IsAssignableFrom(x.PropertyType));
    //    //    //    foreach (PropertyInfo p in ps)
    //    //    //    {
    //    //    //        var d = p.GetCustomAttribute<AuthorityAttribute>();
    //    //    //        if (d == null) continue;
    //    //    //        var authority = p.GetValue(this) as IAuthority;
    //    //    //        if (authority == null) continue;
    //    //    //        authority.AuthorID = d.ID;
    //    //    //        authority.AuthorName = d.Name;
    //    //    //        authority.AuthorGroupName = d.GroupName;
    //    //    //        authority.AuthorPredefinePath = d.PredefinePath;
    //    //    //    }
    //    //    }
    //    //}
    //}
}
