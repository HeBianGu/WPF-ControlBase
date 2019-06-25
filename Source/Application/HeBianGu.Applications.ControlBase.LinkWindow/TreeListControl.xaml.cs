using HeBianGu.Base.WpfBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
    /// <summary>
    /// TreeListControl.xaml 的交互逻辑
    /// </summary>
    public partial class TreeListControl : UserControl
    {
        public TreeListControl()
        {
            InitializeComponent();
        }

        private void Txt_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShellViewModel.Instance.TreeListViewModel.RefreshFilter(((TextBox)sender).Text);
        }
    }

    partial class TreeListViewModel :NotifyPropertyChanged
    {
        public static TreeListViewModel _instance = new TreeListViewModel();

        /// <summary> 获取单例模式 </summary>
        public static TreeListViewModel CreateInstance()
        {
            return _instance;
        }

        private int _count;
        /// <summary> 说明  </summary>
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                RaisePropertyChanged("Count");
            }
        }


        /// <summary>
        /// 初始化树形缺陷列表，只需要初始化一遍即可
        /// </summary>
        /// <param name="uses"></param>
        public void InitTyeEncodeDevice(List<TreeNodeEntity> uses)
        {
            Task.Run(() =>
            {

                List<TreeNodeEntityViewModel> no = new List<TreeNodeEntityViewModel>();

                foreach (var item in uses)
                {
                    no.Add(new TreeNodeEntityViewModel(item));
                }

                //no.AddRange(uses.Select(l => new TyeEncodeDeviceEntityNode(l)));

                this.Nodes = this.Bind(no);

                this.RefreshCount();

                this.FilterText = this.FilterText?.Trim();


                //  Message：触发筛选
                this.RefreshNodes(this.FilterText, this.SelectTyeEncodeDeviceEntityNode);
            });

        }


        private List<TreeNodeEntityViewModel> _nodes = new List<TreeNodeEntityViewModel>();
        /// <summary> 说明  </summary>
        public List<TreeNodeEntityViewModel> Nodes
        {
            get { return _nodes; }
            set
            {
                _nodes = value;
                RaisePropertyChanged("Nodes");
            }
        }

        /// <summary>
        /// 绑定树
        /// </summary>
        List<TreeNodeEntityViewModel> Bind(List<TreeNodeEntityViewModel> nodes)
        {
            List<TreeNodeEntityViewModel> outputList = new List<TreeNodeEntityViewModel>();

            this.Count = 0;

            for (int i = 0; i < nodes.Count; i++)
            {
                if (string.IsNullOrEmpty(nodes[i].ParentID))
                {
                    outputList.Add(nodes[i]);

                    this.Count++;
                }
                else
                {
                    var result = FindDownward(nodes, nodes[i].ParentID);

                    if (result != null)
                    {
                        nodes[i].Parent = result;

                        result.Nodes.Add(nodes[i]);

                        this.Count++;
                    }
                }
            }


            return outputList;
        }

        /// <summary>
        /// 递归向下查找
        /// </summary>
        TreeNodeEntityViewModel FindDownward(List<TreeNodeEntityViewModel> nodes, string id)
        {
            if (nodes == null) return null;

            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].ID == id)
                {
                    return nodes[i];
                }
                TreeNodeEntityViewModel node = FindDownward(nodes[i].Nodes, id);

                if (node != null)
                {
                    return node;
                }
            }
            return null;
        }



        private string _filterText;
        /// <summary> 过滤信息  </summary>
        public string FilterText
        {
            get { return _filterText; }
            set
            {
                _filterText = value;

                RaisePropertyChanged("FilterText");
            }
        }


        private ObservableCollection<TreeNodeEntityViewModel> _tyeEncodeDeviceEntityCheck = new ObservableCollection<TreeNodeEntityViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TreeNodeEntityViewModel> TyeEncodeDeviceEntityCheck
        {
            get { return _tyeEncodeDeviceEntityCheck; }
            set
            {
                _tyeEncodeDeviceEntityCheck = value;
                RaisePropertyChanged("TyeEncodeDeviceEntityCheck");
            }
        }


        private TreeNodeEntityViewModel _selectTyeEncodeDeviceEntityNode;
        /// <summary> 设备过滤选择项  </summary>
        public TreeNodeEntityViewModel SelectTyeEncodeDeviceEntityNode
        {
            get { return _selectTyeEncodeDeviceEntityNode; }
            set
            {
                _selectTyeEncodeDeviceEntityNode = value;

                RaisePropertyChanged("SelectTyeEncodeDeviceEntityNode");

                this.FilterText = this.FilterText?.Trim();
                //  Message：触发筛选
                this.RefreshNodes(this.FilterText, value);

                //task.ContinueWith(l =>
                //{
                //    if (this.Nodes.Count == 1)
                //    {
                //        this.Nodes.First().IsExpanded = true;
                //    }
                //});


            }
        }


        private TreeNodeEntityViewModel _selectTreeTyeEncodeDeviceEntityNode;
        /// <summary> 设备过滤选择项  </summary>
        public TreeNodeEntityViewModel SelectTreeTyeEncodeDeviceEntityNode
        {
            get { return _selectTreeTyeEncodeDeviceEntityNode; }
            set
            {
                _selectTreeTyeEncodeDeviceEntityNode = value;

                RaisePropertyChanged("SelectTreeTyeEncodeDeviceEntityNode");
            }
        }


        public void LoadTyeEncodeCheckDevice(List<TreeNodeEntity> uses)
        {
            ObservableCollection<TreeNodeEntityViewModel> result = new ObservableCollection<TreeNodeEntityViewModel>();

            var all = new TreeNodeEntityViewModel(new TreeNodeEntity() { Name = "全部" });
            all.IsSelected = true;

            result.Add(all);


            foreach (var item in uses)
            {
                result.Add(new TreeNodeEntityViewModel(item));
            }

            this.TyeEncodeDeviceEntityCheck = result;

            this.SelectTyeEncodeDeviceEntityNode = all;
        }

        void RefreshCount()
        {
            Action<List<TreeNodeEntityViewModel>> action = null;

            int c = 0;

            action = k =>
            {
                foreach (var item in k)
                {
                    //  Do：匹配则显示
                    if (item.Visibility == Visibility.Visible)
                    {
                        //  Do：前序遍历
                        if (item.Nodes.Count > 0)
                        {
                            action(item.Nodes);
                        }

                        c++;
                    }
                }
            };

            action(this.Nodes);

            this.Count = c;
        }

        public void RefreshFilter(string text)
        {
            this.RefreshNodes(text, this.SelectTyeEncodeDeviceEntityNode);
        }

        List<TreeNodeEntityViewModel> matchNodes = new List<TreeNodeEntityViewModel>();

        public void RefreshNodes(string text, TreeNodeEntityViewModel selectNode)
        {
            if (selectNode == null) return;

            matchNodes.Clear();

            int code;

            bool isCode = text != null && text.StartsWith("0") && int.TryParse(text.Trim('0'), out code) || text == "0";

            Predicate<string> strMatch = l =>
            {
                if (l == null) return false;

                if (string.IsNullOrEmpty(text)) return true;

                return l.Contains(text);
            };

            Predicate<string> codeMatch = l =>
            {
                if (l == null) return false;

                if (string.IsNullOrEmpty(text)) return true;

                return l.StartsWith(text.TrimEnd('0'));
            };

            Predicate<TreeNodeEntityViewModel> match = l =>
            {
                bool result = isCode ? codeMatch(l.Code) : strMatch(l.Name) || strMatch(l.NamePY);

                if (result)
                {
                    matchNodes.Add(l);
                }

                return result;
            };

            Action<List<TreeNodeEntityViewModel>> action = null;

            action = k =>
            {
                foreach (var item in k)
                {
                    //item.Visibility = Visibility.Visible;

                    //  Do：前序遍历
                    if (item.Nodes.Count > 0)
                    {
                        action(item.Nodes);
                    }

                    item.Visibility = match(item) ? Visibility.Visible : Visibility.Collapsed;

                    //  Do：检查子项是否有显示的
                    if (item.Nodes.Count > 0)
                    {
                        item.Visibility = item.Nodes.Exists(l => l.Visibility == Visibility.Visible) ? Visibility.Visible : Visibility.Collapsed;
                    }
                }
            };



            foreach (var item in this.Nodes)
            {
                //  Do：先检查第一级别过滤设备节点
                item.Visibility = selectNode.Name == "全部" || selectNode.Name == item.Name ?
                    item.Visibility = Visibility.Visible : item.Visibility = Visibility.Collapsed;

                if (item.Visibility == Visibility.Collapsed) continue;

                item.IsExpanded = selectNode.Name == item.Name;

                //  Do：递归检查下面子节点，只检查是否匹配
                action(item.Nodes);

                //  Do：当前项匹配或者有匹配的子项时显示
                item.Visibility = match(item) || item.Nodes.Exists(l => l.Visibility == Visibility.Visible) ? Visibility.Visible : Visibility.Collapsed;

            }

            ////  Do：只有一项匹配
            //if (matchNodes.Count == 1)
            //{

            //    Action<TyeEncodeDeviceEntityNode> ExpandParent = null;

            //    ExpandParent = l =>
            //     {
            //         l.IsExpanded = true;

            //         if (l.Parent != null)
            //         {
            //             ExpandParent(l.Parent);
            //         }
            //     };

            //    ExpandParent(matchNodes.First());

            //    matchNodes.First().IsSelected = true;
            //}

            //  Message：目前需求匹配项都展开 2019-03-23

            if (!string.IsNullOrEmpty(text) && matchNodes.Count > 0)
            {

                Action<TreeNodeEntityViewModel> ExpandParent = null;

                ExpandParent = l =>
                {
                    l.IsExpanded = true;

                    if (l.Parent != null)
                    {
                        ExpandParent(l.Parent);
                    }
                };


                Action<TreeNodeEntityViewModel> CloseChildChild = l =>
                {
                    if (l.Nodes == null) return;

                    foreach (var item in l.Nodes)
                    {
                        if (item.Nodes == null) return;

                        foreach (var item1 in item.Nodes)
                        {
                            item1.IsExpanded = false;
                        }
                    }
                };

                if (isCode)
                {

                    var find = matchNodes.Find(l => l.Code == text.TrimEnd('0'));

                    if (find != null)
                    {
                        ExpandParent(find);
                        find.IsSelected = true;
                        find.IsExpanded = true;

                        CloseChildChild(find);
                    }
                }
                else
                {



                    var collection = matchNodes.Take(500);

                    foreach (var item in matchNodes)
                    {
                        ExpandParent(item);
                    }

                    //  Message：只有一项匹配展开
                    if (matchNodes.Count == 1)
                    {
                        matchNodes.First().IsSelected = true;
                    }
                }
            }



            this.RefreshCount();

        }
  

    }

   
    public partial class TreeNodeEntityViewModel: NotifyPropertyChanged
    {
        public TreeNodeEntityViewModel(TreeNodeEntity tyeEncodeDeviceEntity)
        {
            this.TyeEncodeDeviceEntity = tyeEncodeDeviceEntity;

            this.ID = tyeEncodeDeviceEntity.ID;

            this.ParentID = tyeEncodeDeviceEntity.ParentID;

            this.Code = tyeEncodeDeviceEntity.Code;

            this.Name = tyeEncodeDeviceEntity.Name;

            this.NamePY = tyeEncodeDeviceEntity.NamePY;
        }

        public TreeNodeEntity TyeEncodeDeviceEntity { get; set; }

        public string ID { get; set; }

        public string Name { get; set; }

        public string NamePY { get; set; }

        public string Code { get; set; }

        public string ParentID { get; set; }


        private bool _isSelected = false;
        /// <summary> 是否选中  </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }


        private Visibility _visibility;
        /// <summary> 说明  </summary>
        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                RaisePropertyChanged("Visibility");
            }
        }



        private bool _isExpanded;
        /// <summary> 是否展开  </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                RaisePropertyChanged("IsExpanded");
            }
        }

        public TreeNodeEntityViewModel Parent { get; set; }

        public List<TreeNodeEntityViewModel> Nodes { get; set; } = new List<TreeNodeEntityViewModel>();

        /// <summary> 绑定命令 </summary>
        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：保存
            if (command == "btn_sumit")
            {



            }
            //  Do：取消
            else if (command == "btn_cancel")
            {


            }
            //  Do：初始化
            else if (command == "init")
            {

            }

            //  Do：Reset
            else if (command == "btn_clear")
            {
                //this.Reset();
            }


        }

    }

    public class TreeNodeEntity
    {
        public string Code { get; set; }
        public string RootCode { get; set; }
        public string NamePY { get; set; }
        public string Name { get; set; }
        public string ParentID { get; set; }
        public string ID { get; set; }
    }
}
