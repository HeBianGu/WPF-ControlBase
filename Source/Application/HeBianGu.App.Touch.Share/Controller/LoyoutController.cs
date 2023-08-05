
using HeBianGu.App.Touch.View.Share;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace HeBianGu.App.Touch
{
    [Controller("Loyout")]
    internal class LoyoutController : LoyoutControllerBase
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }

        [GuideMessage(DisplayName = "身高体重", ImagePath = "/HeBianGu.App.Touch;component/Resources/身高测量.png", Message = "现在开始身高体重测量，请站稳站直目视前方。")]
        public async Task<IActionResult> Bmi()
        {
            ShowGuideMessageAsync();
            return await ViewAsync();
        }

        [GuideMessage(DisplayName = "脂肪", ImagePath = "/HeBianGu.App.Touch;component/Resources/脂肪测量示意图19.12.30.png", Message = "现在开始脂肪测量，请双手伸直握住脂肪仪手柄。")]
        public async Task<IActionResult> Fat()
        {
            bool result;
            result = await System.Windows.Application.Current.Dispatcher.Invoke(async () =>
              {
                  SexControl sex = new SexControl();
                  return await Messager.Instance.ShowDialog<bool>(sex);
              });

            if (!result)
            {
                //  Do ：跳过测量
                ViewModel.GoNext();
                return await ViewAsync("Oxygen");
            }

            result = await System.Windows.Application.Current.Dispatcher.Invoke(async () =>
             {
                 AgrControl age = new AgrControl();
                 return await Messager.Instance.ShowDialog<bool>(age);
             });
            if (!result)
            {
                //  Do ：跳过测量
                ViewModel.GoNext();
                return await ViewAsync("Oxygen");
            }
            ShowGuideMessageAsync();
            return await ViewAsync();
        }

        [GuideMessage(DisplayName = "血氧", ImagePath = "/HeBianGu.App.Touch;component/Resources/血氧测量示意图.png", Message = "现在开始血氧测量，请将血氧仪探头夹住手指进行测量。")]

        public async Task<IActionResult> Oxygen()
        {
            ShowGuideMessageAsync();
            return await ViewAsync();
        }

        [GuideMessage(DisplayName = "血压", ImagePath = "/HeBianGu.App.Touch;component/Resources/血压测量示意图19.12.30.png", Message = "现在开始血压测量，请将手臂放置到肘垫处，保持安静松，点击“开始”按钮测量。")]

        public async Task<IActionResult> Pressure()
        {
            ShowGuideMessageAsync();
            return await ViewAsync();
        }

        [GuideMessage(DisplayName = "体温", ImagePath = "/HeBianGu.App.Touch;component/Resources/体温测量示意图.png", Message = "请拿起体温枪对准额头前方0-5cm处，按下测量按钮测量。")]
        public async Task<IActionResult> Temperature()
        {
            ShowGuideMessageAsync();
            return await ViewAsync();
        }
    }


    internal class LoyoutControllerBase : Controller<LoyoutViewModel>
    {
        private Random random = new Random();

        public async Task<bool> ShowGuideMessageAsync([CallerMemberName] string name = "")
        {
            bool result = false;
            string displayName = null;
            await System.Windows.Application.Current.Dispatcher.Invoke(async () =>
             {
                 if (GetType().GetMethod(name).GetCustomAttributes(typeof(GuideMessageAttribute), true).FirstOrDefault() is GuideMessageAttribute route)
                 {
                     GuideMessageControl control = new GuideMessageControl
                     {
                         ImageSource = new BitmapImage(new Uri(route.ImagePath, UriKind.RelativeOrAbsolute)),
                         Message = route.Message
                     };

                     displayName = route.DisplayName;
                     result = await Messager.Instance.ShowDialog<bool>(control);
                 }
             });
            LinkActionEntity current = ViewModel.SelectLink;
            await ViewModel.CheckAndRunMeasure(current);
            return result;
        }
    }



    /// <summary> 引导提示配置 </summary>
    public sealed class GuideMessageAttribute : Attribute
    {
        public string DisplayName { get; set; }
        public string ImagePath { get; set; }
        public string Message { get; set; }
    }
}
