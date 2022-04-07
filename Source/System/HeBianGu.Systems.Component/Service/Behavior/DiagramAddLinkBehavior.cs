// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Diagram;
using Link = HeBianGu.Control.Diagram.Link;

namespace HeBianGu.Systems.Component
{
    public class DiagramAddLinkBehavior : Behavior<Diagram>
    {
        protected override void OnAttached()
        {
            this.AssociatedObject.AddLinked += AssociatedObject_AddLinked;

        }

        private void AssociatedObject_AddLinked(object sender, ObjectRoutedEventArgs<Link> e)
        {
            if (e.Entity == null) return;

            INodeData from = e.Entity.FromNode.Content as INodeData;

            INodeData to = e.Entity.ToNode.Content as INodeData;

            ComponentLink link = new ComponentLink();
            link.RefreshData(from, to);
            e.Entity.Content = link;

            //  Do ：将端口的功能名称赋值给Link去显示
            if (e.Entity.FromPort.Content is IPortData data)
            {
                link.DisplayName = data.DisplayName;
            }
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.AddLinked -= AssociatedObject_AddLinked;
        }
    }

}
