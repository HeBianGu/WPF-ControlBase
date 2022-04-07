// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.AnimatedTabControl;
using HeBianGu.Control.AnimationBox;
using HeBianGu.Control.Chart2D;
using HeBianGu.Control.Diagram;
using HeBianGu.Control.Drawer;
using HeBianGu.Control.Explorer;
using HeBianGu.Control.Filter;
using HeBianGu.Control.GridSplitter;
using HeBianGu.Control.ImagePlayer;
using HeBianGu.Control.LeftMenu;
using HeBianGu.Control.MessageContainer;
using HeBianGu.Control.MessageListBox;
using HeBianGu.Control.MultiComboBox;
using HeBianGu.Control.PagedDataGrid;
using HeBianGu.Control.Panel;
using HeBianGu.Control.PasswordBox;
using HeBianGu.Control.Ping;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.Control.ScrollInto;
using HeBianGu.Control.ScrollVewerLocator;
using HeBianGu.Control.SearchComboBox;
using HeBianGu.Control.Shuttle;
using HeBianGu.Control.Step;
using HeBianGu.Control.TextEditor;
using HeBianGu.Control.ToggleExpander;
using HeBianGu.Control.TopContainer;
using HeBianGu.Control.TreeListView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace HeBianGu.App.ControlResourceKey
{
    internal class ShellViewModel : NotifyPropertyChanged
    {

        private ObservableCollection<Type> _types = new ObservableCollection<Type>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Type> Types
        {
            get { return _types; }
            set
            {
                _types = value;
                RaisePropertyChanged("Types");
            }
        }

        protected override void Init()
        {

        }

        protected override void Loaded(object obj)
        {
            base.Loaded(obj);

            List<Type> types = new List<Type>();

            IEnumerable<Assembly> asses = AppDomain.CurrentDomain.GetAssemblies().Where(l => l.FullName.StartsWith("HeBianGu.Control."));
            foreach (Assembly ass in asses)
            {
                System.Diagnostics.Debug.WriteLine(ass.FullName);
                IEnumerable<Type> cs = ass.GetTypes().Where(l => typeof(FrameworkElement).IsAssignableFrom(l) && !l.IsAbstract);

                foreach (Type item in types)
                {
                    PropertyInfo[] find = item.GetProperties(BindingFlags.Static | BindingFlags.Public);

                    if (find.Count() == 0) continue;

                    types.Add(item);
                }

            }

            types.Add(typeof(PagedDataGrid));
            types.Add(typeof(TreeListView));
            types.Add(typeof(GitTopContainer));
            types.Add(typeof(ToggleExpander));
            types.Add(typeof(TextEditor));
            types.Add(typeof(TextEditorBox));
            types.Add(typeof(Step));
            types.Add(typeof(Shuttle));
            types.Add(typeof(SearchComboBox));
            types.Add(typeof(ScrollBarLocator));
            types.Add(typeof(ScrollViewerLocator));
            types.Add(typeof(ScrollViewerTransfor));
            types.Add(typeof(ScrollIntoView));
            types.Add(typeof(Ping));
            types.Add(typeof(PasswordBox));
            types.Add(typeof(EffectBox));
            types.Add(typeof(Pagination));
            types.Add(typeof(AutoColumnPagedDataGrid));
            types.Add(typeof(MultiComboBox));
            types.Add(typeof(MessageListBox));
            types.Add(typeof(MessageContainer));
            types.Add(typeof(LeftMenu));
            types.Add(typeof(GroupExpander));
            types.Add(typeof(LinkGroupExpander));
            types.Add(typeof(ImagePlayer));
            types.Add(typeof(GridSplitterContainer));
            types.Add(typeof(FilterBox));
            types.Add(typeof(FilterColumn));
            types.Add(typeof(SearchBox));
            types.Add(typeof(SelectionBox));
            types.Add(typeof(Explorer));
            types.Add(typeof(ExplorerTree));
            types.Add(typeof(Drawer));
            types.Add(typeof(Diagram));
            types.Add(typeof(Chart));
            types.Add(typeof(AnimationBox));
            types.Add(typeof(AnimatedTabControl));
            types.Add(typeof(PropertyControl));
            types.Add(typeof(PropertyGrid));
            types.Add(typeof(PropertyTabControl));
            types.Add(typeof(PropertyView));
            this.Types = types.Distinct().OrderBy(l => l.Name).ToObservable();
        }

        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {

        }
    }
}
