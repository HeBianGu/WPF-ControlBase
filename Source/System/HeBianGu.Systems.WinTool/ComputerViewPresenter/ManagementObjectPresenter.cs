using HeBianGu.Base.WpfBase;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.Text;

namespace HeBianGu.Systems.WinTool
{
    [Displayer(Name = "配置参数")]
    public class ManagementObjectPresenter : DisplayerViewModelBase
    {
        public ManagementObject Model { get; set; }
        public ManagementObjectPresenter(ManagementObject model)
        {
            try
            {
                this.Model = model;
                this.Caption = model.Properties["Caption"]?.Value?.ToString();
                this.Description = model.Properties["Description"]?.Value?.ToString();
                //this.Version = model.Properties["Version"]?.Value?.ToString();
                //this.Manufacturer = model.Properties["Manufacturer"]?.Value?.ToString();
                //this.Name = this.Caption;
                //ICollectionView view = CollectionViewSource.GetDefaultView(model.Properties);
                //view.GroupDescriptions.Add(new PropertyGroupDescription("Origin"));

                foreach (var item in this.Model.Properties)
                {
                    //if (item.Value == null)
                    //    continue;
                    //if (item.Type == CimType.Object)
                    //    continue;
                    this.Collection.Add(new PropertyDataPresenter(item));
                }

                var version = this.Collection.FirstOrDefault(x => x.Model.Name == "Version")?.Model?.Value?.ToString();
                var manufacturer = this.Collection.FirstOrDefault(x => x.Model.Name == "Manufacturer")?.Model?.Value?.ToString();
                var vendor = this.Collection.FirstOrDefault(x => x.Model.Name == "Vendor")?.Model?.Value?.ToString();
                var installdate = this.Collection.FirstOrDefault(x => x.Model.Name == "InstallDate")?.Model?.Value?.ToString();
                var driveVersion = this.Collection.FirstOrDefault(x => x.Model.Name == "DriverVersion")?.Model?.Value?.ToString();
                var serialNumber = this.Collection.FirstOrDefault(x => x.Model.Name == "SerialNumber")?.Model?.Value?.ToString();
                var depdendent = this.Collection.FirstOrDefault(x => x.Model.Name == "Dependent")?.Model?.Value?.ToString();
                var antecedent = this.Collection.FirstOrDefault(x => x.Model.Name == "Antecedent")?.Model?.Value?.ToString();

                StringBuilder sb = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(serialNumber))
                    sb.Append(" 序列号 - " + serialNumber);
                if (!string.IsNullOrWhiteSpace(version))
                    sb.Append(" 版本号 - " + version);
                if (!string.IsNullOrWhiteSpace(driveVersion))
                    sb.Append(" 驱动版本 - " + driveVersion);
                if (!string.IsNullOrWhiteSpace(manufacturer))
                    sb.Append(" 制造商 - " + manufacturer);
                if (!string.IsNullOrWhiteSpace(vendor))
                    sb.Append(" 供应商 - " + vendor);
                if (!string.IsNullOrWhiteSpace(installdate))
                    sb.Append(" 安装日期 - " + installdate);
                if (string.IsNullOrWhiteSpace(this.Caption) && !string.IsNullOrWhiteSpace(depdendent))
                    this.Caption = depdendent;
                if (string.IsNullOrWhiteSpace(this.Description) && !string.IsNullOrWhiteSpace(antecedent))
                    this.Description = antecedent;
                this.Tag = sb.ToString();
            }
            catch
            {

            }
        }

        private string _caption;
        /// <summary> 说明  </summary>
        public string Caption
        {
            get { return _caption; }
            set
            {
                _caption = value;
                RaisePropertyChanged();
            }
        }


        private string _version;
        /// <summary> 说明  </summary>
        public string Version
        {
            get { return _version; }
            set
            {
                _version = value;
                RaisePropertyChanged();
            }
        }


        private string _manufacturer;
        /// <summary> 说明  </summary>
        public string Manufacturer
        {
            get { return _manufacturer; }
            set
            {
                _manufacturer = value;
                RaisePropertyChanged();
            }
        }


        private string _tag;
        /// <summary> 说明  </summary>
        public string Tag
        {
            get { return _tag; }
            set
            {
                _tag = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<PropertyDataPresenter> _collection = new ObservableCollection<PropertyDataPresenter>();
        /// <summary> 说明  </summary>
        public ObservableCollection<PropertyDataPresenter> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }

    }

    public class PropertyDataPresenter : ModelViewModel<PropertyData>
    {
        public PropertyDataPresenter(PropertyData model) : base(model)
        {

        }
    }

}
