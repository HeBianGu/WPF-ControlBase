// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace HeBianGu.Service.Mvp
{
    /// <summary>
    /// 官方网站
    /// </summary>
    public interface IWebsiteViewPresenter : IInvokePresenter
    {

    }

    public interface IWebsiteViewPresenterOption
    {
        string WebSite { get; set; }
    }

    [Displayer(Name = "官方网站", GroupName = SystemSetting.GroupSystem, Icon = Icons.IE, Description = "查看当前应用程序的官方网站")]
    public class WebsiteViewPresenter : ServiceMvpSettingBase<WebsiteViewPresenter, IWebsiteViewPresenter>, IWebsiteViewPresenter, IWebsiteViewPresenterOption
    {
        private string _webSite;
        [DefaultValue("https://github.com/HeBianGu/WPF-ControlBase")]
        [Display(Name = "官方网站")]
        public string WebSite
        {
            get { return _webSite; }
            set
            {
                _webSite = value;
                RaisePropertyChanged();
            }
        }
        public override bool Invoke(out string message)
        {

            Process.Start(new ProcessStartInfo("explorer.exe", this.WebSite) { UseShellExecute = true });
            message = null;
            return true;
        }
    }
}
