// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Systems.Repository
{
    [TemplatePart(Name = PART_ListBox, Type = typeof(ListBox))]
    public partial class RespositoryBox : System.Windows.Controls.Control
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(RespositoryBox), "S.RespositoryBox.Default");

        private const string PART_ListBox = "PART_ListBox";

        static RespositoryBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RespositoryBox), new FrameworkPropertyMetadata(typeof(RespositoryBox)));
        }

        public RespositoryBox()
        {
            this.Loaded += (l, k) =>
               {
                   this.RefreshData();
               };

        }

        private ListBox _listBox;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _listBox = this.Template.FindName(PART_ListBox, this) as ListBox;
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(RespositoryBox), new FrameworkPropertyMetadata(default(DataTemplate), (d, e) =>
             {
                 RespositoryBox control = d as RespositoryBox;

                 if (control == null) return;

                 if (e.OldValue is DataTemplate o)
                 {

                 }

                 if (e.NewValue is DataTemplate n)
                 {

                 }
             }));

        public Type ModelType
        {
            get { return (Type)GetValue(ModelTypeProperty); }
            set { SetValue(ModelTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModelTypeProperty =
            DependencyProperty.Register("ModelType", typeof(Type), typeof(RespositoryBox), new FrameworkPropertyMetadata(default(Type), (d, e) =>
             {
                 RespositoryBox control = d as RespositoryBox;

                 if (control == null) return;

                 if (e.OldValue is Type o)
                 {

                 }

                 if (e.NewValue is Type n)
                 {

                 }

                 control.RefreshData();

             }));

        private async void RefreshData()
        {
            if (this.ModelType == null) return;

            if (this._listBox == null) return;

            if (!this.IsLoaded) return;

            IRepository repository = this.ModelType.GetRepository(out string error);

            if (repository == null)
            {
                await MessageProxy.Messager.ShowSumit(error);
                return;
            }

            System.Reflection.MethodInfo method = repository.GetType().GetMethod("GetAll");

            IEnumerable enumerable = await MessageProxy.Messager.ShowWaitter(() =>
                {
                    return method.Invoke(repository, new object[] { null }) as IEnumerable;
                });

            this._listBox.ItemsSource = enumerable;
        }


        public ItemsPanelTemplateDisplay ItemsPanel
        {
            get { return (ItemsPanelTemplateDisplay)GetValue(ItemsPanelProperty); }
            set { SetValue(ItemsPanelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsPanelProperty =
            DependencyProperty.Register("ItemsPanel", typeof(ItemsPanelTemplateDisplay), typeof(RespositoryBox), new FrameworkPropertyMetadata(default(ItemsPanelTemplateDisplay), (d, e) =>
             {
                 RespositoryBox control = d as RespositoryBox;

                 if (control == null) return;

                 if (e.OldValue is ItemsPanelTemplateDisplay o)
                 {

                 }

                 if (e.NewValue is ItemsPanelTemplateDisplay n)
                 {

                 }

             }));

        [TypeConverter(typeof(ArrayConverter))]
        public ItemsPanelTemplateDisplay[] ItemsPanelSelectSource
        {
            get { return (ItemsPanelTemplateDisplay[])GetValue(ItemsPanelSelectSourceProperty); }
            set { SetValue(ItemsPanelSelectSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsPanelSelectSourceProperty =
            DependencyProperty.Register("ItemsPanelSelectSource", typeof(ItemsPanelTemplateDisplay[]), typeof(RespositoryBox), new FrameworkPropertyMetadata(default(ItemsPanelTemplateDisplay[]), (d, e) =>
             {
                 RespositoryBox control = d as RespositoryBox;

                 if (control == null) return;

                 if (e.OldValue is ItemsPanelTemplateDisplay[] o)
                 {

                 }

                 if (e.NewValue is ItemsPanelTemplateDisplay[] n)
                 {
                     control.ItemsPanel = n?.FirstOrDefault();
                 }

             }));

        public DiplayDataTemplate[] ItemTemplateSelectSource
        {
            get { return (DiplayDataTemplate[])GetValue(ItemTemplateSelectSourceProperty); }
            set { SetValue(ItemTemplateSelectSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateSelectSourceProperty =
            DependencyProperty.Register(" ItemTemplateSelectSource", typeof(DiplayDataTemplate[]), typeof(RespositoryBox), new FrameworkPropertyMetadata(default(DiplayDataTemplate[]), (d, e) =>
             {
                 RespositoryBox control = d as RespositoryBox;

                 if (control == null) return;

                 if (e.OldValue is DiplayDataTemplate[] o)
                 {

                 }

                 if (e.NewValue is DiplayDataTemplate[] n)
                 {
                     control.ItemTemplate = n?.FirstOrDefault();
                 }

             }));


    }

    /// <summary> 说明</summary>
    public class ItemsPanelTemplateDisplay : NotifyPropertyChangedBase
    {
        #region - 属性 - 

        private string _name;
        /// <summary> 说明  </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }


        private ItemsPanelTemplate _itemsPanelTemplate;
        /// <summary> 说明  </summary>
        public ItemsPanelTemplate ItemsPanelTemplate
        {
            get { return _itemsPanelTemplate; }
            set
            {
                _itemsPanelTemplate = value;
                RaisePropertyChanged();
            }
        }

        #endregion 
    }

    /// <summary> 说明</summary>
    public class DiplayDataTemplate : DataTemplate
    {
        public string Name { get; set; }
    }


}
