using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;


namespace HeBianGu.General.WpfControlLib
{
    public class MvcNavigationBar : ListBox
    {
        //protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        //{
        //    base.OnSelectionChanged(e);

        //    this.RefreshData();
        //}


        //void RefreshData()
        //{
        //    ObservableCollection<ILinkActionBase> points = this.ItemsSource as ObservableCollection<ILinkActionBase>;

        //    if (points == null) return;

        //    var collection = points.ToList();

        //    collection.RemoveRange(this.SelectedIndex + 1, points.Count - this.SelectedIndex-1);

        //    this.ItemsSource = collection.ToObservable();


        //}
    }
}