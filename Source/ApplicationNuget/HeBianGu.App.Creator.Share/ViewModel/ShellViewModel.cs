using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Diagram;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;
using HeBianGu.Systems.Component;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using ComponentNode = HeBianGu.Systems.Component.ComponentNode;
using ErrorMessage = HeBianGu.Control.MessageListBox.ErrorMessage;
using InfoMessage = HeBianGu.Control.MessageListBox.InfoMessage;

namespace HeBianGu.App.Creator
{
    internal class ShellViewModel : MvcShellViewModel
    {
        private ObservableCollection<IComponentNode> _component = new ObservableCollection<IComponentNode>();
        /// <summary> 说明  </summary>
        public ObservableCollection<IComponentNode> Components
        {
            get { return _component; }
            set
            {
                _component = value;
                RaisePropertyChanged("Components");
            }
        }

        private ObservableCollection<Node> _nodesSource = new ObservableCollection<Node>();
        /// <summary> 工具拖动数据源  </summary>
        public ObservableCollection<Node> NodesSource
        {
            get { return _nodesSource; }
            set
            {
                _nodesSource = value;
                RaisePropertyChanged("NodesSource");
            }
        }

        private ProjectData _projectData = new ProjectData();
        /// <summary> 说明  </summary>
        public ProjectData ProjectData
        {
            get { return _projectData; }
            set
            {
                _projectData = value;
                RaisePropertyChanged("ProjectData");
            }
        }

        private ObservableCollection<InfoMessage> _infos = new ObservableCollection<InfoMessage>();
        /// <summary> 说明  </summary>
        public ObservableCollection<InfoMessage> Infos
        {
            get { return _infos; }
            set
            {
                _infos = value;
                RaisePropertyChanged("Infos");
            }
        }


        private ObservableCollection<ErrorMessage> _errors = new ObservableCollection<ErrorMessage>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ErrorMessage> Errors
        {
            get { return _errors; }
            set
            {
                _errors = value;
                RaisePropertyChanged("Errors");
            }
        }

        private ProjectState _state = ProjectState.None;
        /// <summary> 说明  </summary>
        public ProjectState State
        {
            get { return _state; }
            set
            {
                _state = value;
                RaisePropertyChanged("State");
            }
        }


        private Part _selectedPart;
        /// <summary> 说明  </summary>
        public Part SelectedPart
        {
            get { return _selectedPart; }
            set
            {
                _selectedPart = value;
                RaisePropertyChanged("SelectedPart");
            }
        }


        public RelayCommand StartCommand => new RelayCommand(l => Start(), l =>
          {
              return State != ProjectState.Running;
          });

        public RelayCommand StopCommand => new RelayCommand(l =>
        {
            State = ProjectState.Stopped;
        });

        public RelayCommand NewProjectCommand => new RelayCommand(l =>
         {
             if (l is ComponentAttribute componentGroup)
             {
                 WorkflowProject project = new WorkflowProject
                 {
                     Title = componentGroup.DisplayName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"),

                     TemplateID = componentGroup.DisplayName
                 };

                 WorkflowProjectService.Instance.Current = project;

                 IsShowLink = false;

                 RefreshComponents(project);

                 RefreshNodes(project);

                 Message.Instance.ShowSnackMessageWithNotice(project.Title);
             }
         });


        private bool _isShowLink;
        /// <summary> 说明  </summary>
        public bool IsShowLink
        {
            get { return _isShowLink; }
            set
            {
                _isShowLink = value;
                RaisePropertyChanged();
            }
        }

        private void RefreshComponents(WorkflowProject project)
        {
            Components = ComponentService.GetComponents(l => l.DisplayName == project?.TemplateID)?.ToObservable();
        }

        public RelayCommand SelectedProjectChangedCommand => new RelayCommand(l =>
        {
            if (l is WorkflowProject project)
            {
                WorkflowProjectService.Instance.Current = project;

                IsShowLink = false;

                RefreshComponents(project);

                RefreshNodes(project);
            }
        });


        public RelayCommand SettingCommand { get; set; } = new RelayCommand(l =>
        {
            View.SettingDialog setting = new View.SettingDialog();

            Message.Instance.ShowLayer(setting);
        });

        private void RefreshNodes(WorkflowProject project)
        {
            if (!File.Exists(project.Path))
            {
                NodesSource = new ObservableCollection<Node>();
                return;
            }
            string filePath = project.Path;

            //XmlProvider xmlProvider = new XmlProvider(); 

            ProjectData = XmlSerializer.Load<ProjectData>(filePath);

            Tuple<IEnumerable<ComponentNode>, IEnumerable<ComponentLink>> tuble = ProjectData.DiagramData.GetDatas();

            IEnumerable<ComponentNode> nodes = tuble.Item1;

            IEnumerable<ComponentLink> links = tuble.Item2;

            IGraphSource source = new ComponentGraphSource(nodes, links);

            NodesSource = source.NodeSource.ToObservable();
        }

        private async void Start()
        {
            IEnumerable<ComponentNode> actions = NodesSource.Select(l => l.Content).Cast<ComponentNode>();

            foreach (ComponentNode action in actions)
            {
                action.State = ActionState.Ready;
            }

            Node start = NodesSource.FirstOrDefault(l => l.Content is StartComponentNode);

            if (start == null)
            {
                Message.Instance.ShowSnackMessageWithNotice("请先添加一个StartAction");
                return;
            }


            State = ProjectState.Running;

            ProjectData.SystemSet.IsLogVisible = true;

            bool b = await ComponentService.StartAllAction(start);

            State = b ? ProjectState.Success : ProjectState.Error;

            CommandService.InvalidateRequerySuggested();
        }

        protected override void Init()
        {
            base.Init();

            WorkflowProject current = WorkflowProjectService.Instance.Current;

            if (current == null)
            {
                IsShowLink = true;

                //this.GoToLink("Loyout","Open");

                CurrentLink = LinkActions.FirstOrDefault(l => l.Action == "New");
                return;
            }

            RefreshComponents(current);

            RefreshNodes(current);
        }

        private ISerializerService XmlSerializer => ServiceRegistry.Instance.GetInstance<ISerializerService>();

        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            if (command == "1")
            {

            }
            else if (command == "2")
            {

            }

            //  Do：等待消息
            else if (command == "Button.Click.Load")
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Title = "文件加载路径",
                    Filter = "配置文件(*.wf)|*.wf",

                    InitialDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory)
                };

                bool? r = dialog.ShowDialog();

                if (r == false) return;

                string filePath = dialog.FileName;

                //XmlProvider xmlProvider = new XmlProvider(); 

                ProjectData = XmlSerializer.Load<ProjectData>(filePath);

                Tuple<IEnumerable<ComponentNode>, IEnumerable<ComponentLink>> tuble = ProjectData.DiagramData.GetDatas();

                IEnumerable<ComponentNode> nodes = tuble.Item1;

                IEnumerable<ComponentLink> links = tuble.Item2;

                IGraphSource source = new ComponentGraphSource(nodes, links);

                NodesSource = source.NodeSource.ToObservable();
            }
            //  Do：保存数据
            else if (command == "Button.Click.Save")
            {
                if (WorkflowProjectService.Instance.Current == null)
                {
                    Message.Instance.ShowSnackMessageWithNotice("当前工程不能为空,请新建工程");
                    return;
                }
                string extention = WorkflowProjectService.Instance.Extenstion;

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = $"文本文件(*.{extention})|*.{extention}",//设置文件类型
                    FileName = WorkflowProjectService.Instance.Current.Title,//设置默认文件名
                    DefaultExt = extention,//设置默认格式（可以不设）
                    AddExtension = true,//设置自动在文件名中添加扩展名
                    RestoreDirectory = true, //设置对话框是否记忆上次打开的目
                    Title = "保存工程", //获取或设置文件对话框标题 
                    InitialDirectory = SystemPathSetting.Instance.Project
                };
                if (saveFileDialog.ShowDialog() != true) return;

                IDataSource<ComponentNode, ComponentLink> source = new ComponentGraphSource(NodesSource.ToList());

                ComponentData data = new ComponentData
                {
                    Nodes = source.GetNodeType().Select(l => l.Serialize()).ToList(),

                    Links = source.GetLinkType().Select(l => l.Serialize()).ToList(),

                    Ports = source.GetNodeType().SelectMany(l => l.PortDatas).Cast<ISerialize>().Select(l => l.Serialize()).ToList()
                };

                ProjectData.DiagramData = data;

                string filePath = saveFileDialog.FileName;


                XmlSerializer.Save(filePath, ProjectData);

                //  Do ：保存到列表
                WorkflowProjectService.Instance.Current.Path = filePath;
                WorkflowProjectService.Instance.Add(WorkflowProjectService.Instance.Current);

            }
            else if (command == "Button.Click.Clear")
            {
                NodesSource = new ObservableCollection<Node>();
            }
            else if (command == "Button.Click.Delete")
            {
                SelectedPart?.Delete();
            }

        }

        public static string Serializer(Type type, object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(type);
            //序列化对象
            xml.Serialize(Stream, obj);
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            return str;
        }
    }

    public enum ProjectState
    {
        None = 0, Running, Success, Error, Stopped
    }
}
