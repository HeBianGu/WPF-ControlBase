// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection;

namespace HeBianGu.Service.Mvp
{
    public interface ICopyRightViewPresenter : IInvokePresenter
    {

    }

    public interface ICopyRightViewPresenterOption
    {
        string CopyRight { get; set; }
        string WebSite { get; set; }
    }

    [Displayer(Name = "许可协议", GroupName = SystemSetting.GroupSystem, Icon = Icons.Eye_slash, Description = "查看当前程序的许可协议")]
    public class CopyRightViewPresenter : ServiceMvpSettingBase<CopyRightViewPresenter, ICopyRightViewPresenter>, ICopyRightViewPresenter, ICopyRightViewPresenterOption
    {
        public override void LoadDefault()
        {
            base.LoadDefault();
            this._copyRight = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;

        }
        private string _webSite;
        [DefaultValue("https://github.com/HeBianGu/WPF-ControlBase")]
        [Display(Name = "跳转地址")]
        public string WebSite
        {
            get { return _webSite; }
            set
            {
                _webSite = value;
                RaisePropertyChanged();
            }
        }

        private string _copyRight;
        [DefaultValue(true)]
        [Display(Name = "许可协议")]
        public string CopyRight
        {
            get { return _copyRight; }
            set
            {
                _copyRight = value;
                RaisePropertyChanged();
            }
        }

        public override bool Invoke(out string message)
        {

            Process.Start(new ProcessStartInfo("explorer.exe", this.WebSite) { UseShellExecute = true });
            //Process.Start("explorer.exe", this.WebSite);
            message = null;
            return true;
        }
    }
}
