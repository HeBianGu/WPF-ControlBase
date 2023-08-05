
using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Message;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Survey
{
    public interface ISurveyViewPresenterOption : IMvpSettingOption
    {
        DialogMode DialogMode { get; set; }
        Survey Survey { get; set; }
        string Welcome { get; set; }
    }

    /// <summary>
    /// 调查报告
    /// </summary>
    public interface ISurveyViewPresenter : IInvokePresenter
    {

    }
    [Displayer(Name = "调查问卷", GroupName = SystemSetting.GroupSystem, Icon = IconAll.NoteBook2, Description = "使用调查文件帮助优化本软件")]
    public class SurveyViewPresenter : ServiceMvpSettingBase<SurveyViewPresenter, ISurveyViewPresenter>, ISurveyViewPresenter, ISurveyViewPresenterOption
    {
        private Survey _survey = new Survey();
        [XmlIgnore]
        [Browsable(false)]
        public Survey Survey
        {
            get { return _survey; }
            set
            {
                _survey = value;
                RaisePropertyChanged();
            }
        }

        private DialogMode _dialogMode;
        [Browsable(false)]
        [Display(Name = "显示方式")]
        public DialogMode DialogMode
        {
            get { return _dialogMode; }
            set
            {
                _dialogMode = value;
                RaisePropertyChanged();
            }
        }

        private string _welcome = "欢迎使用本软件";
        [Display(Name = "欢迎显示")]
        public string Welcome
        {
            get { return _welcome; }
            set
            {
                _welcome = value;
                RaisePropertyChanged();
            }
        }
        public override bool Invoke(out string message)
        {
            message = null;
            if (this.Survey == null)
            {
                message = "没有配置调查问卷模型";
                return false;
            }
            if (this.DialogMode == DialogMode.Window)
            {
                MessageProxy.WindowPresenter.ShowClearly(this.Survey, this.Name, x =>
                 {
                     x.SizeToContent = SizeToContent.Manual;
                     x.Height = 600;
                 });
            }
            else
            {
                MessageProxy.Presenter.Show(this.Survey, null, this.Name, x =>
                   {
                       //x.Width = 700;
                       //x.Height = 500;
                   }, ObjectContentDialog.CloseKey);
            }

            return true;
        }
    }

    //[Display(Name = "调查问卷", GroupName = SystemSetting.GroupSystem)]
    //public class SurveySetting : LazySettingInstance<SurveySetting>
    //{

    //}
}
