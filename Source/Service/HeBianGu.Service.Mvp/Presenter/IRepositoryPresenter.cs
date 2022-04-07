// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;

namespace HeBianGu.Service.Mvp
{
    public interface IRepositoryPresenter<T> : IRepositoryPresenter
    {
        RelayCommand AddCommand { get; }
        RelayCommand CheckAllCommand { get; }
        RelayCommand ClearCommand { get; }
        RelayCommand DeleteCommand { get; }
        RelayCommand DeleteSelectedCommand { get; }
        RelayCommand DetialCommand { get; }
        RelayCommand EditCommand { get; }
        RelayCommand ExportXmlCommand { get; }
        RelayCommand GridSetCommand { get; }
        IObservableSource<SelectViewModel<T>> ItemsSource { get; set; }
        Type ModelType { get; set; }
        RelayCommand RefreshCommand { get; }
        SelectViewModel<T> SelectedItem { get; set; }
    }

    public interface IRepositoryPresenter
    {

    }
}