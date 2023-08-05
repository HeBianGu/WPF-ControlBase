using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HeBianGu.App.Currency
{
    internal class ShellViewModel : NotifyPropertyChanged
    {
        HomePage _homePage = new HomePage();
        public ShellViewModel()
        {
            this.Pages.Add(new HomePage());
            this.SelectedPage = _homePage;
        }


        private ObservableCollection<TabPageBase> _pages = new ObservableCollection<TabPageBase>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TabPageBase> Pages
        {
            get { return _pages; }
            set
            {
                _pages = value;
                RaisePropertyChanged();
            }
        }

        private TabPageBase _selectedPage;
        /// <summary> 说明  </summary>
        public TabPageBase SelectedPage
        {
            get { return _selectedPage; }
            set
            {
                _selectedPage = value;
                RaisePropertyChanged();
            }
        }

        protected override void Loaded(object obj)
        {
            base.Loaded(obj);

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                SelectInfo info = new SelectInfo();
                MessageProxy.Presenter.Show(info, null, "选择门店");
            }));
        }

        public RelayCommand AddPageCommand => new RelayCommand(l =>
        {
            if (l is string name)
            {
                var find = this.Pages.FirstOrDefault(x => x.Name == name);
                if (find != null)
                {
                    this.SelectedPage = find;
                    return;
                }

                var test = new TestPage() { Name = name };
                this.Pages.Add(test);
                this.SelectedPage = test;
            }
        });

        public RelayCommand ViewCommand => new RelayCommand(l =>
        {
            if (l is string name)
            {
                MessageProxy.Presenter.Show(name, null, name, x =>
                {
                    x.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                    x.VerticalContentAlignment = VerticalAlignment.Stretch;
                });
            }
        });

    }

    public abstract class TabPageBase : DisplayerViewModelBase
    {

    }

    [Displayer(Name = "首页")]
    public class HomePage : TabPageBase
    {

    }

    public class TestPage : TabPageBase
    {

        public TestPage()
        {
            for (int i = 0; i < 100; i++)
            {
                this.Collection.Add(new Student());
            }

        }

        private ObservableCollection<Student> _collection = new ObservableCollection<Student>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Student> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

    }

    public class SelectInfo : NotifyPropertyChanged
    {
        private string _md;
        /// <summary> 说明  </summary>
        public string Md
        {
            get { return _md; }
            set
            {
                _md = value;
                RaisePropertyChanged();
            }
        }


        private string _gt;
        /// <summary> 说明  </summary>
        public string Gt
        {
            get { return _gt; }
            set
            {
                _gt = value;
                RaisePropertyChanged();
            }
        }


    }

}
