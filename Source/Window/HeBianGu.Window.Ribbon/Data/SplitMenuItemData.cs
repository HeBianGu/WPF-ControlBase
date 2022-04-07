// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Window.Ribbon
{
    public class SplitMenuItemData : MenuItemData
    {
        public SplitMenuItemData()
            : this(false)
        {
        }

        public SplitMenuItemData(bool isApplicationMenu)
            : base(isApplicationMenu)
        {
        }
    }
}
