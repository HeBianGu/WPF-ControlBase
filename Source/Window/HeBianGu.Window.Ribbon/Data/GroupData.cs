// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls.Ribbon;

namespace HeBianGu.Window.Ribbon
{
    public class GroupData : ControlData
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<ControlData> ControlDataCollection
        {
            get
            {
                if (_controlDataCollection == null)
                    _controlDataCollection = new ObservableCollection<ControlData>();
                return _controlDataCollection;
            }
        }
        private ObservableCollection<ControlData> _controlDataCollection;
        private bool _isCollapsed = false;
        public bool IsCollapsed
        {
            get { return _isCollapsed; }
            set
            {
                if (_isCollapsed == value) return;

                _isCollapsed = value;
                RaisePropertyChanged();
            }
        }


        private RibbonGroupSizeDefinitionBaseCollection _groupSizeDefinitions;

        public RibbonGroupSizeDefinitionBaseCollection GroupSizeDefinitions
        {
            get { return _groupSizeDefinitions; }
            set
            {
                if (_groupSizeDefinitions != value)
                {
                    _groupSizeDefinitions = value;

                    RaisePropertyChanged();
                }
            }
        }
    }
}
