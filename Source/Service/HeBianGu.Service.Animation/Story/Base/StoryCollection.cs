// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Markup;

namespace HeBianGu.Service.Animation
{
    //[DefaultProperty("Item")]
    //[ContentProperty("Item")]
    [DefaultMember("Item")]
    public class StoryCollection : ObservableCollection<IStory>
    {
    }
}
