using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Dock.Layout;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using HeBianGu.Systems.Design;
using System.Linq;
using System;
using HeBianGu.Control.PrintBox;
using HeBianGu.Systems.Print;
using HeBianGu.Systems.Project;
using System.IO;
using HeBianGu.Service.AppConfig;
using HeBianGu.Systems.XmlSerialize;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Windows.Threading;

namespace HeBianGu.App.Report
{
    internal class ShellViewModel : NotifyPropertyChanged
    {

        protected override void Loaded(object obj)
        {
            base.Loaded(obj);
        }
        protected override void Init()
        {
            this.Documents.Add(new Document() { Name = "新建文档1" });
            ProjectProxy.Instance.CurrentChanged = (l, k) =>
            {
                if (!File.Exists(k.Path))
                    return;
                MessageProxy.Messager.ShowWaitter(() =>
                {
                    var rpt = XmlableSerializor.Instance.Load<Rpt>(k.Path);
                    this.Documents = rpt.Documents.ToObservable();
                });
                MessageProxy.Snacker.ShowTime("加载完成");
            };

            DesginViewPresenter.Instance.AddSource(new CustomDesignDataSource());
            DesginViewPresenter.Instance.DataContextChanged = x =>
            {
                if (this.SelectedDocument == null)
                    return;
                x.Refresh(this.SelectedDocument.SelectedDesignPresenter);
            };
        }

        private ObservableCollection<IPrintPagePresenter> _pages = new ObservableCollection<IPrintPagePresenter>();
        /// <summary> 说明  </summary>
        public ObservableCollection<IPrintPagePresenter> Pages
        {
            get { return _pages; }
            set
            {
                _pages = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Document> _documents = new ObservableCollection<Document>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Document> Documents
        {
            get { return _documents; }
            set
            {
                _documents = value;
                RaisePropertyChanged();
            }
        }


        private Document _selectedDocument;
        /// <summary> 说明  </summary>
        public Document SelectedDocument
        {
            get { return _selectedDocument; }
            set
            {
                _selectedDocument = value;
                RaisePropertyChanged();
            }
        }

        private int _selectedDocumentIndex;
        /// <summary> 说明  </summary>
        public int selectedDocumentIndex
        {
            get { return _selectedDocumentIndex; }
            set
            {
                _selectedDocumentIndex = value;
                RaisePropertyChanged();

                this.SelectedDocument = this.Documents[value];
            }
        }

        private object _activeContent;
        /// <summary> 说明  </summary>
        public object ActiveContent
        {
            get { return _activeContent; }
            set
            {
                _activeContent = value;
                RaisePropertyChanged();
            }
        }

        private object _dataSources;
        /// <summary> 说明  </summary>
        public object DataSources
        {
            get { return _dataSources; }
            set
            {
                _dataSources = value;
                RaisePropertyChanged();
            }
        }


        public RelayCommand SaveCommand => new RelayCommand(async (s, e) =>
        {
            Rpt rpt = new Rpt();
            rpt.Documents = this.Documents.ToList();
            if (ProjectProxy.Instance.Current == null)
            {
                var project = ProjectProxy.Instance.Create();
                bool r = await MessageProxy.PropertyGrid.ShowEdit(project, null, "新建工程");
                if (!r) return;
                string message = null;
                string filePath = Path.Combine(SystemPathSetting.Instance.Project, project.Title + ".rpt");
                project.Path = filePath;
                r = await MessageProxy.Messager.ShowWaitter(() =>
                  {
                      XmlableSerializor.Instance.Save(filePath, rpt);
                      return ProjectProxy.Instance.Save(out message);
                  });
                if (r)
                {
                    ProjectProxy.Instance.Add(project);
                    ProjectProxy.Instance.Current = project;
                    MessageProxy.Snacker.ShowTime("保存完成");
                }
                else
                    MessageProxy.Notify.ShowError("保存失败：" + message);
            }
            else
            {
                if (!File.Exists(ProjectProxy.Instance.Current.Path))
                {
                    string filePath = Path.Combine(SystemPathSetting.Instance.Project, ProjectProxy.Instance.Current.Title + ".rpt");
                    ProjectProxy.Instance.Current.Path = filePath;
                    ProjectProxy.Instance.Save(out string message);
                }
                await MessageProxy.Messager.ShowWaitter(() =>
                 {
                     XmlableSerializor.Instance.Save(ProjectProxy.Instance.Current.Path, rpt);
                 });
                MessageProxy.Snacker.ShowTime("保存完成");
            }
        });

        public RelayCommand ClearCommand => new RelayCommand((s, e) =>
        {
            this.Documents.Clear();
        });

        public RelayCommand PrintCommand => new RelayCommand((s, e) =>
        {
            if (e is FrameworkElement control)
            {
                var printBox = control.GetChild<PrintBox>();
                PrintCommands.Print.Execute(null, printBox);
            }
        });

        public RelayCommand PrintPreViewCommand => new RelayCommand((s, e) =>
        {
            if (e is FrameworkElement control)
            {
                var printBox = control.GetChild<PrintBox>();
                PrintCommands.PrintView.Execute(null, printBox);
            }
        });

        public RelayCommand PrintSettingCommand => new RelayCommand((s, e) =>
        {
            if (e is FrameworkElement control)
            {
                var printBox = control.GetChild<PrintBox>();
                PrintCommands.PrintSetting.Execute(null, printBox);
            }
        });

        public RelayCommand ActiveContentChangedCommand => new RelayCommand((s, e) =>
        {
            if (e is string project)
            {

            }

            if (this.ActiveContent is Document doc)
            {
                this.SelectedDocument = doc;
            }
        });


        public RelayCommand SelectedDesignPresenterChangedCommand => new RelayCommand((s, e) =>
        {
            if (e is RoutedPropertyChangedEventArgs<object> project)
            {
                var n = project.NewValue;
                this.SelectedDocument.SelectedDesignPresenter = n as IDesignPresenter;
                DesginViewPresenter.Instance.Filter(this.SelectedDocument.SelectedDesignPresenter);
                this.DataSources = null;
                this.DataSources = DesginViewPresenter.Instance;
            }
        });

        public RelayCommand AddDocumentCommand => new RelayCommand((s, e) =>
        {
            this.Documents.Add(new Document() { Name = "新建文档" + this.Documents.Count });
        });
    }

    public class Rpt
    {
        public List<Document> Documents { get; set; }
    }

    /// <summary> 说明</summary>
    public class Document : DisplayerViewModelBase
    {
        public Document()
        {
            this.Pages.Add(new ListPrintPagePresenter());
        }
        private ObservableCollection<IPrintPagePresenter> _pages = new ObservableCollection<IPrintPagePresenter>();
        /// <summary> 说明  </summary>
        public ObservableCollection<IPrintPagePresenter> Pages
        {
            get { return _pages; }
            set
            {
                _pages = value;
                RaisePropertyChanged();
            }
        }

        private IDesignPresenter _selectedDesignPresenter;
        [XmlIgnore]
        public IDesignPresenter SelectedDesignPresenter
        {
            get { return _selectedDesignPresenter; }
            set
            {
                _selectedDesignPresenter = value;
                RaisePropertyChanged();
            }
        }

    }

    class PanesTemplateSelector : DataTemplateSelector
    {
        public PanesTemplateSelector()
        {

        }


        public DataTemplate Documentemplate
        {
            get;
            set;
        }

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var itemAsLayoutContent = item as LayoutContent;
            if (item is Document)
                return Documentemplate;

            return base.SelectTemplate(item, container);
        }
    }
}
