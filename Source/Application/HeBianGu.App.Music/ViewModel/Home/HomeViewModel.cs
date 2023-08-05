// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.App.Music.ViewModel.Home
{
    [ViewModel("Home")]
    internal class HomeViewModel : MvcViewModelBase
    {
        private ILinkAction _selectLink1;
        /// <summary> 说明  </summary>
        public ILinkAction SelectLink1
        {
            get { return _selectLink1; }
            set
            {
                _selectLink1 = value;
                RaisePropertyChanged();
            }
        }

        private ILinkAction _selectLink2;
        /// <summary> 说明  </summary>
        public ILinkAction SelectLink2
        {
            get { return _selectLink2; }
            set
            {
                _selectLink2 = value;
                RaisePropertyChanged();
            }
        }

        private ILinkAction _selectLink3;
        /// <summary> 说明  </summary>
        public ILinkAction SelectLink3
        {
            get { return _selectLink3; }
            set
            {
                _selectLink3 = value;
                RaisePropertyChanged();
            }
        }

        private ILinkAction _selectLink4;
        /// <summary> 说明  </summary>
        public ILinkAction SelectLink4
        {
            get { return _selectLink4; }
            set
            {
                _selectLink4 = value;
                RaisePropertyChanged();
            }
        }

        Stack<ILinkAction> _goBackStack = new Stack<ILinkAction>();

        void Select(ILinkAction link)
        {
            this.SelectLink1 = this.SelectLink1 == link ? link : null;
            this.SelectLink2 = this.SelectLink2 == link ? link : null;
            this.SelectLink3 = this.SelectLink3 == link ? link : null;
            this.SelectLink4 = this.SelectLink4 == link ? link : null;
            this.SelectLink = link;
            _goBackStack.Push(link);
        }

        public RelayCommand SelectLinkChangedCommand => new RelayCommand(l =>
        {
            if (l is SelectionChangedEventArgs e)
            {
                if (e.AddedItems.Count > 0 && e.AddedItems[0] is ILinkAction link)
                {
                    this.Select(link);
                }
            }
        });

        public RelayCommand LoadedDefaultCommand => new RelayCommand(l =>
        {
            if (l is RoutedEventArgs e)
            {
                if (e.Source is ListBox listBox)
                {
                    if (listBox.Items is IList list && list.Count > 0)
                    {
                        this.Select(list[0] as ILinkAction);
                        this.SelectLink1 = list[0] as ILinkAction;
                    }
                }
            }
        });

        public RelayCommand GoBackCommand => new RelayCommand(l =>
        {
            var link = _goBackStack.Pop();
            if (link == this.SelectLink)
                link = _goBackStack.Pop();
            this.GoBack(link);
        }, x => _goBackStack.Count > 0 && !(_goBackStack.Count == 1 && _goBackStack.Peek() == this.SelectLink));

        void GoBack(ILinkAction link)
        {
            this.SelectLink1 = link;
            this.SelectLink2 = link;
            this.SelectLink3 = link;
            this.SelectLink4 = link;
            this.SelectLink = link;
        }
    }

}
