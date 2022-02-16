using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Diagram;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Systems.Component;
using HeBianGu.Service.Mvc;
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
    class ShellViewModel : MvcShellViewModel
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


        public RelayCommand StartCommand => new RelayCommand(l => this.Start(), l =>
          {
              return this.State != ProjectState.Running;
          });

        public RelayCommand StopCommand => new RelayCommand(l =>
        {
            this.State = ProjectState.Stopped;
        });

        public RelayCommand NewProjectCommand => new RelayCommand(l =>
         {
             if (l is ComponentAttribute componentGroup)
             {
                 WorkflowProject project = new WorkflowProject();

                 project.Title = componentGroup.DisplayName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");

                 project.TemplateID = componentGroup.DisplayName;

                 WorkflowProjectService.Instance.Current = project;

                 this.IsShowLink = false;

                 this.RefreshComponents(project);

                 this.RefreshNodes(project);

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

        void RefreshComponents(WorkflowProject project)
        {
            this.Components = ComponentService.GetComponents(l => l.DisplayName == project?.TemplateID)?.ToObservable();
        }

        public RelayCommand SelectedProjectChangedCommand => new RelayCommand(l =>
        {
            if (l is WorkflowProject project)
            {
                WorkflowProjectService.Instance.Current = project;

                this.IsShowLink = false;

                this.RefreshComponents(project);

                this.RefreshNodes(project);
            }
        });


        public RelayCommand SettingCommand { get; set; } = new RelayCommand(l =>
        {
            var setting = new View.SettingDialog();

            Message.Instance.ShowLayer(setting);
        });


        void RefreshNodes(WorkflowProject project)
        {
            if (!File.Exists(project.Path))
            {
                this.NodesSource = new ObservableCollection<Node>();
                return;
            }
            string filePath = project.Path;

            //XmlProvider xmlProvider = new XmlProvider(); 

            this.ProjectData = XmlSerializer.Load<ProjectData>(filePath);

            var tuble = this.ProjectData.DiagramData.GetDatas();

            IEnumerable<ComponentNode> nodes = tuble.Item1;

            IEnumerable<ComponentLink> links = tuble.Item2;

            IGraphSource source = new ComponentGraphSource(nodes, links);

            this.NodesSource = source.NodeSource.ToObservable();
        }


        async void Start()
        {
            var actions = this.NodesSource.Select(l => l.Content).Cast<ComponentNode>();

            foreach (var action in actions)
            {
                action.State = ActionState.Ready;
            }

            var start = this.NodesSource.FirstOrDefault(l => l.Content is StartComponentNode);

            if (start == null)
            {
                Message.Instance.ShowSnackMessageWithNotice("请先添加一个StartAction");
                return;
            }


            this.State = ProjectState.Running;

            this.ProjectData.SystemSet.IsLogVisible = true;

            bool b = await ComponentService.StartAllAction(start);

            this.State = b ? ProjectState.Success : ProjectState.Error;

            CommandService.InvalidateRequerySuggested();
        }

        protected override void Init()
        {
            base.Init();

            WorkflowProject current = WorkflowProjectService.Instance.Current;

            if (current == null)
            {
                this.IsShowLink = true;

                //this.GoToLink("Loyout","Open");

                this.CurrentLink = this.LinkActions.FirstOrDefault(l => l.Action == "New");
                return;
            }

            this.RefreshComponents(current);

            this.RefreshNodes(current);
        }

        ISerializerService XmlSerializer => ServiceRegistry.Instance.GetInstance<ISerializerService>();

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
                OpenFileDialog dialog = new OpenFileDialog();

                dialog.Title = "文件加载路径";
                dialog.Filter = "配置文件(*.wf)|*.wf";

                dialog.InitialDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory);

                var r = dialog.ShowDialog();

                if (r == false) return;

                string filePath = dialog.FileName;

                //XmlProvider xmlProvider = new XmlProvider(); 

                this.ProjectData = XmlSerializer.Load<ProjectData>(filePath);

                var tuble = this.ProjectData.DiagramData.GetDatas();

                IEnumerable<ComponentNode> nodes = tuble.Item1;

                IEnumerable<ComponentLink> links = tuble.Item2;

                IGraphSource source = new ComponentGraphSource(nodes, links);

                this.NodesSource = source.NodeSource.ToObservable();
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

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = $"文本文件(*.{extention})|*.{extention}";//设置文件类型
                saveFileDialog.FileName = WorkflowProjectService.Instance.Current.Title;//设置默认文件名
                saveFileDialog.DefaultExt = extention;//设置默认格式（可以不设）
                saveFileDialog.AddExtension = true;//设置自动在文件名中添加扩展名
                saveFileDialog.RestoreDirectory = true; //设置对话框是否记忆上次打开的目
                saveFileDialog.Title = "保存工程"; //获取或设置文件对话框标题 
                saveFileDialog.InitialDirectory = SystemPathSetting.Instance.Project;
                if (saveFileDialog.ShowDialog() != true) return;

                IDataSource<ComponentNode, ComponentLink> source = new ComponentGraphSource(this.NodesSource.ToList());

                ComponentData data = new ComponentData();

                data.Nodes = source.GetNodeType().Select(l => l.Serialize()).ToList();

                data.Links = source.GetLinkType().Select(l => l.Serialize()).ToList();

                data.Ports = source.GetNodeType().SelectMany(l => l.PortDatas).Cast<ISerialize>().Select(l => l.Serialize()).ToList();

                this.ProjectData.DiagramData = data;

                string filePath = saveFileDialog.FileName;


                XmlSerializer.Save(filePath, this.ProjectData);

                //  Do ：保存到列表
                WorkflowProjectService.Instance.Current.Path = filePath;
                WorkflowProjectService.Instance.Add(WorkflowProjectService.Instance.Current);

            }
            else if (command == "Button.Click.Clear")
            {
                this.NodesSource = new ObservableCollection<Node>();
            }
            else if (command == "Button.Click.Delete")
            {
                this.SelectedPart?.Delete();
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
