
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Systems.Feedback
{
    public interface IFeedbackViewPresenterOption : IMvpSettingOption
    {
        string Contact { get; set; }
        DialogMode DialogMode { get; set; }
        string ImageData { get; set; }
        string Text { get; set; }
        string Title { get; set; }
        string YourName { get; set; }
    }

    /// <summary>
    /// 意见反馈
    /// </summary>
    public interface IFeedbackViewPresenter : IInvokePresenter
    {

    }

    [Displayer(Name = "意见反馈", GroupName = SystemSetting.GroupSystem, Icon = IconAll.NoteBook, Description = "点击显示意见反馈")]
    public class FeedbackViewPresenter : ServiceMvpSettingBase<FeedbackViewPresenter, IFeedbackViewPresenter>, IFeedbackViewPresenter, IFeedbackViewPresenterOption
    {
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


        private string _title = "您的意见是我们不断进步的动力，请留下您在使用中遇到的问题或提出宝贵的建议。";
        [Display(Name = "显示标题")]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }


        private string _yourName;
        [Browsable(false)]
        [Display(Name = "您的称呼")]
        public string YourName
        {
            get { return _yourName; }
            set
            {
                _yourName = value;
                RaisePropertyChanged();
            }
        }


        private string _contact;
        [Browsable(false)]
        [Display(Name = "联系方式")]
        public string Contact
        {
            get { return _contact; }
            set
            {
                _contact = value;
                RaisePropertyChanged();
            }
        }

        private string _imageData;
        [Browsable(false)]
        [Display(Name = "显示标题")]
        public string ImageData
        {
            get { return _imageData; }
            set
            {
                _imageData = value;
                RaisePropertyChanged();
            }
        }

        private string _text;
        [Browsable(false)]
        [Display(Name = "问题描述")]
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged();
            }
        }


        public override bool Invoke(out string message)
        {
            message = null;
            if (this.DialogMode == DialogMode.Window)
            {
                MessageProxy.WindowPresenter.ShowClearly(this, this.Name);
            }
            else
            {
                MessageProxy.Presenter.Show(this, null, this.Name, x =>
                {
                    //x.Width = 700;
                    //x.Height = 500;
                });
            }

            return true;
        }
    }


    //[Display(Name = "意见反馈", GroupName = SystemSetting.GroupSystem)]
    //public class FeedbackSetting : LazySettingInstance<FeedbackSetting>
    //{

    //}
}
