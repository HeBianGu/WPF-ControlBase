// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HeBianGu.Window.Ribbon
{
    public class MenuButtonData : ControlData
    {
        public MenuButtonData()
            : this(false)
        {
        }

        public MenuButtonData(bool isApplicationMenu)
        {
            _isApplicationMenu = isApplicationMenu;
        }

        public bool IsVerticallyResizable
        {
            get
            {
                return _isVerticallyResizable;
            }

            set
            {
                if (_isVerticallyResizable != value)
                {
                    _isVerticallyResizable = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool IsHorizontallyResizable
        {
            get
            {
                return _isHorizontallyResizable;
            }

            set
            {
                if (_isHorizontallyResizable != value)
                {
                    _isHorizontallyResizable = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool IsDropDownOpen
        {
            get
            {
                return _isDropDownOpen;
            }

            set
            {
                if (_isDropDownOpen != value)
                {
                    _isDropDownOpen = value;
                    RaisePropertyChanged();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<ControlData> ControlDataCollection
        {
            get
            {
                if (_controlDataCollection == null)
                {
                    _controlDataCollection = new ObservableCollection<ControlData>();
                }
                return _controlDataCollection;
            }
        }
        private ObservableCollection<ControlData> _controlDataCollection;

        private bool _isVerticallyResizable, _isHorizontallyResizable;
        private bool _isApplicationMenu;
        private bool _isDropDownOpen;
    }
}
