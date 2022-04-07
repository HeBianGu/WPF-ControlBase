// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.Service.Animation;
using System.Linq;
using System.Reflection;

namespace HeBianGu.Systems.Setting
{
    public class TransitionPropertyItem : ItemsSourcePropertyItem<TransitionsHost>
    {
        public TransitionPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
            System.Collections.Generic.List<ITransition> transitons = TransitionService.GetAll();

            foreach (ITransition item in transitons)
            {
                TransitionsHost host = new TransitionsHost();
                host.Transitions.Add(item);
                host.Name = item.GetType().Name;
                this.Collection.Add(host);
            }

            if (this.Value == null)
            {
                this.Value = this.Collection?.FirstOrDefault();
            }
            else
            {
                this.Collection.Replace(this.Value, l => l.Name == this.Value.Name);
            }
        }
    }
}
