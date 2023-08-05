using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.IO;

namespace HeBianGu.App.Above
{
    [ViewModel("Custom")]
    internal class CustomViewModel : MvcViewModelBase
    {
        private Student _student = new Student();
        /// <summary> 说明  </summary>
        public Student Student
        {
            get { return _student; }
            set
            {
                _student = value;
                RaisePropertyChanged("Student");
            }
        }


        private Config _config;
        /// <summary> 说明  </summary>
        public Config Config
        {
            get { return _config; }
            set
            {
                _config = value;
                RaisePropertyChanged("Config");
            }
        }

        private PropertyModel _propertyModel = new PropertyModel();
        /// <summary> 说明  </summary>
        public PropertyModel PropertyModel
        {
            get { return _propertyModel; }
            set
            {
                _propertyModel = value;
                RaisePropertyChanged("PropertyModel");
            }
        }

        private PropertyListModel _propertyListModel = new PropertyListModel();
        /// <summary> 说明  </summary>
        public PropertyListModel PropertyListModel
        {
            get { return _propertyListModel; }
            set
            {
                _propertyListModel = value;
                RaisePropertyChanged("PropertyListModel");
            }
        }



        protected override void Init()
        {
            Config testConfig = new Config();

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "PropertyConfig.xml");


            testConfig = testConfig.LoadFromFile(filePath);

            Config = testConfig;
        }

        protected override void Loaded(string args)
        {

        }

    }
}
