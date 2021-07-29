using HeBianGu.Base.WpfBase;
using HeBianGu.Common.LocalConfig;
using HeBianGu.Control.Diagram;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;
using Link = HeBianGu.Control.Diagram.Link;

namespace HeBianGu.Application.DiagramWindow
{
    [ViewModel("Loyout")]
    class LoyoutViewModel : MvcViewModelBase
    {
        Random random = new Random();
        protected override void Init()
        {


        }


        private ObservableCollection<Node> _circleNodes = new ObservableCollection<Node>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Node> CircleNodes
        {
            get { return _circleNodes; }
            set
            {
                _circleNodes = value;
                RaisePropertyChanged("CircleNodes");
            }
        }


        private ObservableCollection<Node> _treeNodes = new ObservableCollection<Node>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Node> TreeNodes
        {
            get { return _treeNodes; }
            set
            {
                _treeNodes = value;
                RaisePropertyChanged("TreeNodes");
            }
        }
        private ObservableCollection<Node> _typeTreeNodes = new ObservableCollection<Node>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Node> TypeTreeNodes
        {
            get { return _typeTreeNodes; }
            set
            {
                _typeTreeNodes = value;
                RaisePropertyChanged("TypeTreeNodes");
            }
        }



        private ObservableCollection<Node> _portNodes = new ObservableCollection<Node>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Node> PortNodes
        {
            get { return _portNodes; }
            set
            {
                _portNodes = value;
                RaisePropertyChanged("PortNodes");
            }
        }



        private ObservableCollection<Node> _portXmlNodes = new ObservableCollection<Node>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Node> PortXmlNodes
        {
            get { return _portXmlNodes; }
            set
            {
                _portXmlNodes = value;
                RaisePropertyChanged("PortXmlNodes");
            }
        }

        private ObservableCollection<Node> _toolNodes = new ObservableCollection<Node>();
        /// <summary> 工具拖动数据源  </summary>
        public ObservableCollection<Node> ToolNodes
        {
            get { return _toolNodes; }
            set
            {
                _toolNodes = value;
                RaisePropertyChanged("ToolNodes");
            }
        }


        protected override void Loaded(string args)
        {
            //  Do ：环型数据
            {
                List<Node> nodes = new List<Node>();

                List<Link> links = new List<Link>();

                for (int i = 0; i < 10; i++)
                {
                    Node node = new Node() { Content = new TestViewModel() { Value = "1" } };

                    nodes.Add(node);
                }

                foreach (var item in nodes)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        var index = random.Next(9);

                        Node to = nodes[index];

                        Link link = new Link();

                        link.FromNode = item;
                        item.LinksInto.Add(link);
                        //item.LinksConnected.Add(link);

                        link.ToNode = to;
                        to.LinksOutOf.Add(link);
                        //to.LinksConnected.Add(link);

                        links.Add(link);
                    }
                }

                CircleNodes = nodes.ToObservable();
            }

            this.RefreshTreeNodes();

            this.RefreshPortNodes();

            this.LoadPortXml();

            this.LoadObjectTree();
        }


        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            //  Do：对话消息
            if (command == "Sumit")
            {

            }

            //  Do：等待消息
            else if (command == "Button.Click.RefreshData")
            {
                this.RefreshTreeNodes();
            }

            //  Do：等待消息
            else if (command == "Button.Click.ToolSaveAs")
            {
                foreach (var item in this.ToolNodes)
                {
                    Debug.WriteLine(item);
                }
            }

            //  Do：等待消息
            else if (command == "Button.Click.ToolLoad")
            {

            }
            //  Do：等待消息
            else if (command == "Button.Click.PortXmlLoad")
            {
                OpenFileDialog dialog = new OpenFileDialog();

                dialog.Title = "文件加载路径";
                dialog.Filter = "配置文件(*.xml)|*.xml";

                dialog.InitialDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory);

                var r = dialog.ShowDialog();

                string filePath =dialog.FileName;

                XmlProvider xmlProvider = new XmlProvider();

                Data data = xmlProvider.Load<Data>(filePath);

                UnitGraphSource source = new UnitGraphSource(data.Units, data.Wires);

                this.PortXmlNodes = source.NodeSource.ToObservable();
            }
            //  Do：保存数据
            else if (command == "Button.Click.PortXmlSaveAs")
            {
                List<Unit> nodes = new List<Unit>();

                foreach (var node in this.PortXmlNodes.Cast<PortNode>())
                {
                    Unit unit = node.Content as Unit;

                    PortNode port = node as PortNode;

                    var sockets = port.Ports.Select(l => l.Content).Cast<Socket>();

                    unit.Sockets = sockets.ToList();

                    var postion= NodeLayer.GetPosition(node);

                    unit.X = postion.X;

                    unit.Y = postion.Y;

                    nodes.Add(unit);
                }

                List<Wire> wires = new List<Wire>();

                foreach (var node in this.PortXmlNodes)
                {
                    foreach (var link in node.LinksOutOf)
                    {
                        Wire wire = new Wire();

                        wire.From = link.FromNode?.Id;
                        wire.To = link.ToNode?.Id;
                        wire.FromPort = link.FromPort?.Id;
                        wire.ToPort = link.ToPort?.Id;

                        wires.Add(wire);
                    }
                }

                Data data = new Data();

                data.Units = nodes;

                data.Wires = wires;

                SaveFileDialog dialog = new SaveFileDialog();

                dialog.Title = "文件保存路径";
                dialog.Filter = "配置文件(*.xml)|*.xml";

                dialog.InitialDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory);

                var r = dialog.ShowDialog();

                if (r != true) return;

                string filePath = dialog.FileName;

                XmlProvider xmlProvider = new XmlProvider();

                xmlProvider.Save(filePath, data);

            }
        }

        void RefreshTreeNodes()
        {
            this.TreeNodes.Clear();

            //  Do ：树型数据
            {
                TreeNode root = new TreeNode() { Content = new TestViewModel() { Value = "1", Value2 = random.Next(1000000).ToString() } };

                this.TreeNodes.Add(root);

                for (int i = 0; i < random.Next(1, 4); i++)
                {
                    TreeNode first = new TreeNode() { Content = new TestViewModel() { Value = "1 - " + i, Value2 = random.Next(1000000).ToString() } };

                    Link link = new Link();

                    link.FromNode = root;
                    root.LinksOutOf.Add(link);

                    link.ToNode = first;
                    first.LinksInto.Add(link);

                    for (int j = 0; j < random.Next(5); j++)
                    {
                        TreeNode second = new TreeNode() { Content = new TestViewModel() { Value = i + " - " + j, Value2 = (random.Next(1000000) * j).ToString() } };

                        link = new Link();

                        link.FromNode = first;
                        first.LinksOutOf.Add(link);

                        link.ToNode = second;
                        second.LinksInto.Add(link);
                        this.TreeNodes.Add(second);

                        for (int k = 0; k < random.Next(6); k++)
                        {
                            TreeNode third = new TreeNode() { Content = new TestViewModel() { Value = i + " - " + j + " - " + k, Value2 = random.Next(1000000).ToString() } };

                            link = new Link();

                            link.FromNode = second;
                            second.LinksOutOf.Add(link);

                            link.ToNode = third;
                            third.LinksInto.Add(link);
                            this.TreeNodes.Add(third);

                            if (k == 0) continue;

                            for (int m = 0; m < random.Next(6); m++)
                            {
                                TreeNode forth = new TreeNode() { Content = new TestViewModel() { Value = i + " - " + j + " - " + k, Value2 = random.Next(1000000).ToString() } };

                                link = new Link();

                                link.FromNode = third;
                                third.LinksOutOf.Add(link);

                                link.ToNode = forth;
                                forth.LinksInto.Add(link);
                                this.TreeNodes.Add(forth);
                            }
                        }
                    }
                    this.TreeNodes.Add(first);
                }
            }
        }

        void RefreshPortNodes()
        {
            this.PortNodes.Clear();

            PortNode from = new PortNode() { Content = new TestViewModel() { Value = "1" } };


            //from.Ports = new List<Port>();

            var from_right_port = new Port() { Dock = Dock.Right, Content = new Socket() };
            from_right_port.ParentNode = from;

            from.Ports.Add(from_right_port);

            PortNode to = new PortNode() { Content = new TestViewModel() { Value = "2" } };

            for (int i = 0; i < 1; i++)
            {
                var to_port = new Port() { Dock = Dock.Left, Content = new Socket() };
                to_port.ParentNode = to;
                to.Ports.Add(to_port);
            }

            for (int i = 0; i < 2; i++)
            {
                var to_port = new Port() { Dock = Dock.Right, Content = new Socket() };
                to_port.ParentNode = to;
                to.Ports.Add(to_port);
            }

            for (int i = 0; i < 3; i++)
            {
                var to_port = new Port() { Dock = Dock.Top, Content = new Socket() };
                to_port.ParentNode = to;
                to.Ports.Add(to_port);
            }

            for (int i = 0; i < 4; i++)
            {
                var to_port = new Port() { Dock = Dock.Bottom, Content = new Socket() };
                to_port.ParentNode = to;
                to.Ports.Add(to_port);
            }


            //Link link = new Link();

            //link.FromNode = from;
            //link.ToNode = to;

            //link.FromPort = from_right_port;
            //link.ToPort = to.Ports?.FirstOrDefault();

            //from.LinksInto.Add(link);
            ////from.LinksConnected.Add(link); 

            //to.LinksOutOf.Add(link);
            ////to.LinksConnected.Add(link);

            Link.Create(from, to, from_right_port, to.Ports?.FirstOrDefault());

            this.PortNodes.Add(from);
            this.PortNodes.Add(to);

        }

        public void LoadPortXml()
        {
            XmlProvider xmlProvider = new XmlProvider();

            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "DynamicPorts.xml");

            Data data = xmlProvider.Load<Data>(filePath);

            UnitGraphSource source = new UnitGraphSource(data.Units, data.Wires);

            this.PortXmlNodes = source.NodeSource.ToObservable();
        }

        public void LoadObjectTree()
        {
            TypeTreeGraphSource source = new TypeTreeGraphSource(typeof(Diagram).Assembly);

            this.TypeTreeNodes = source.NodeSource?.ToObservable(); ;
        }
    }

    public class Data
    {
        public List<Unit> Units { get; set; }

        public List<Wire> Wires { get; set; }
    }






}
