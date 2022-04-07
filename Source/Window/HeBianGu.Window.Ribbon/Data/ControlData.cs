// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Windows.Controls.Ribbon;
using System.Windows.Input;
using System.Windows.Media;

namespace HeBianGu.Window.Ribbon
{
    public class ControlData : NotifyPropertyChangedBase
    {
        public string Label
        {
            get { return _label; }
            set
            {
                _label = value;
                RaisePropertyChanged();
            }
        }
        private string _label;

        public string FormalLabel
        {
            get { return _formalLabel; }
            set
            {
                if (_formalLabel != value)
                {
                    _formalLabel = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _formalLabel;

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible == value)
                    return;
                _isVisible = value;
                RaisePropertyChanged();
            }
        }
        private bool _isVisible = true;

        public ImageSource LargeImage
        {
            get { return _largeImage; }
            set
            {
                if (_largeImage != value)
                {
                    _largeImage = value;
                    RaisePropertyChanged();
                }
            }
        }
        private ImageSource _largeImage;

        public ImageSource SmallImage
        {
            get { return _smallImage; }
            set
            {
                if (_smallImage != value)
                {
                    _smallImage = value;
                    RaisePropertyChanged();
                }
            }
        }
        private ImageSource _smallImage;

        public string ToolTipTitle
        {
            get { return _toolTipTitle; }
            set
            {
                if (_toolTipTitle != value)
                {
                    _toolTipTitle = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _toolTipTitle;

        public string ToolTipDescription
        {
            get { return _toolTipDescription; }
            set
            {
                if (_toolTipDescription != value)
                {
                    _toolTipDescription = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _toolTipDescription;

        public object OriginalToolTipDescription
        {
            get;
            set;
        }

        public string HotkeysDescription
        {
            get { return _hotkeysDescription; }
            set
            {
                if (_hotkeysDescription != value)
                {
                    _hotkeysDescription = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _hotkeysDescription;

        public string ToolTipTitleConcatHotKeys
        {
            get
            {
                if (string.IsNullOrEmpty(_hotkeysDescription))
                    return _toolTipTitle;
                else
                    return string.Format("{0} ({1})", _toolTipTitle, _hotkeysDescription);
            }
        }

        public Uri ToolTipImage
        {
            get
            {
                return _toolTipImage;
            }
            set
            {
                _toolTipImage = value;
                RaisePropertyChanged();
            }
        }
        private Uri _toolTipImage;

        public string ToolTipFooterTitle
        {
            get { return _toolTipFooterTitle; }
            set
            {
                if (_toolTipFooterTitle != value)
                {
                    _toolTipFooterTitle = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _toolTipFooterTitle;

        public string ToolTipFooterDescription
        {
            get { return _toolTipFooterDescription; }
            set
            {
                if (_toolTipFooterDescription != value)
                {
                    _toolTipFooterDescription = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _toolTipFooterDescription;

        public Uri ToolTipFooterImage
        {
            get { return _toolTipFooterImage; }
            set
            {
                if (_toolTipFooterImage != value)
                {
                    _toolTipFooterImage = value;
                    RaisePropertyChanged();
                }
            }
        }
        private Uri _toolTipFooterImage;

        public ICommand Command
        {
            get { return _command; }
            set
            {
                _command = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _command;

        public string KeyTip
        {
            get { return _keyTip; }
            set
            {
                if (_keyTip != value)
                {
                    _keyTip = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _keyTip;

        public string HelpToken
        {
            get { return _helpToken; }
            set
            {
                if (_helpToken != value)
                {
                    _helpToken = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _helpToken;

        public object Data { get; set; }


        public ControlData()
        {
            this.RefreshSizeDefinitions(this.ImageSize, this.IsLabelVisible);
        }


        public RibbonImageSize ImageSize
        {
            get { return this.SizeDefinition.ImageSize; }
            set
            {
                this.RefreshSizeDefinitions(value, this.IsLabelVisible);
            }
        }

        public bool IsLabelVisible
        {
            get { return this.SizeDefinition.IsLabelVisible; }
            set
            {
                this.RefreshSizeDefinitions(this.ImageSize, value);
            }
        }
        private RibbonControlSizeDefinition _sizeDefinition = new RibbonControlSizeDefinition() { ImageSize = RibbonImageSize.Small, IsLabelVisible = true };

        public RibbonControlSizeDefinition SizeDefinition
        {
            get { return _sizeDefinition; }
            set
            {
                if (_sizeDefinition != value)
                {
                    _sizeDefinition = value;

                    RaisePropertyChanged(nameof(SizeDefinition));
                    RaisePropertyChanged(nameof(IsLabelVisible));
                    RaisePropertyChanged(nameof(ImageSize));
                }
            }
        }

        protected virtual void RefreshSizeDefinitions(RibbonImageSize imageSize, bool isLabelVisible)
        {
            RibbonControlSizeDefinition sizeDefinition = new RibbonControlSizeDefinition();
            sizeDefinition.ImageSize = imageSize;
            sizeDefinition.IsLabelVisible = isLabelVisible;
            sizeDefinition.Freeze();
            this.SizeDefinition = sizeDefinition;
        }
    }
}
