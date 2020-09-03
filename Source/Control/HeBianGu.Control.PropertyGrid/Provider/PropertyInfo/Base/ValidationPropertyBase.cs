using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{
    /// <summary> 验证Model属性的基类 </summary>
    public abstract class ValidationPropertyBase : NotifyPropertyChanged, IValidationProperty
    {
        public ValidationPropertyBase()
        {
            LoadDefaultCommand = new RelayCommand(l=>this.LoadDefault());
        }
        #region - 属性 -


        private string _name;
        /// <summary> 实体字段名称  </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }


        private string _displayName;
        /// <summary> 说明  </summary>
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                RaisePropertyChanged("DisplayName");
            }
        }

        private string _flag;
        /// <summary> 必须字段标识  </summary>
        public string Flag
        {
            get { return _flag; }
            set
            {
                _flag = value;
                RaisePropertyChanged("Flag");
            }
        }


        private string _message;
        /// <summary> 验证消息  </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }


        private string _description;
        /// <summary> 描述信息  </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }


        private string _index;
        /// <summary> 索引  </summary>
        public string Index
        {
            get { return _index; }
            set
            {
                _index = value;
                RaisePropertyChanged("Index");
            }
        }



        private string _group;
        /// <summary> 分组  </summary>
        public string Group
        {
            get { return _group; }
            set
            {
                _group = value;
                RaisePropertyChanged("Group");
            }
        }

        #endregion

        /// <summary> 加载单位 </summary>
        public virtual void LoadUnit(UnitGroup unit)
        {
            _unit = unit;
        }

        protected Property _config;

        protected UnitGroup _unit;

        /// <summary> 加载属性参数 </summary>
        public virtual void LoadProperty(Property config, string defaultName = null)
        {
            _config = config;

            this.DisplayName = config?.Name ?? defaultName;

            this.Description = config?.Desc;

            this.Index = config?.Index;

            this.Group = config?.Group;
        }
      
        public virtual bool IsValidation(Property source)
        {
            return true;
        }

        /// <summary> 加载属性参数 </summary>
        public virtual void LoadDefault()
        {

        }

        public virtual void LoadValue(object obj)
        {
            
        }

        public RelayCommand LoadDefaultCommand { get; set; }
    }
}
