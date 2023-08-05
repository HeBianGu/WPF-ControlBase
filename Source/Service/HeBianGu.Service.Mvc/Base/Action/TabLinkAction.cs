// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Service.Mvc
{
    public class TabLinkAction : LinkAction
    {
        public ILinkAction LeftLink { get; set; }
        public ILinkAction RightLink { get; set; }
        public ILinkAction TopLink { get; set; }
        public ILinkAction BottomLink { get; set; }
    }
}
