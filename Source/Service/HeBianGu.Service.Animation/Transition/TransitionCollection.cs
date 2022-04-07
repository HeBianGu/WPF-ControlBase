// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.ObjectModel;
using System.Reflection;

namespace HeBianGu.Service.Animation
{
    [DefaultMember("Item")]
    public class TransitionCollection : ObservableCollection<ITransition>
    {

    }
}
