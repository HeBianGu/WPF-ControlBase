using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Application.ConfigWindow
{
    public class TestConfigViewModel : ModelViewModel<TestConfig>
    { 
        public TestConfigViewModel(TestConfig model) : base(model)
        {
            foreach (var item in model.TestCategories)
            {
                this.TestCategories.Add(new TestCategoryViewModel(item));
            }
        }

        private ObservableCollection<TestCategoryViewModel> _testCategories = new ObservableCollection<TestCategoryViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TestCategoryViewModel> TestCategories
        {
            get { return _testCategories; }
            set
            {
                _testCategories = value;
                RaisePropertyChanged("TestCategories");
            }
        }

        public void Save(string path)
        {
            return;
        }

    }

    public class TestCategoryViewModel : ModelViewModel<TestCategory>
    {

        public TestCategoryViewModel(TestCategory model) : base(model)
        {
            foreach (var item in model.Items)
            {
                this.Items.Add(new ItemViewModel(item));
            }
        }

        private ObservableCollection<ItemViewModel> _items = new ObservableCollection<ItemViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged("Items");
            }
        }

    }

    public class ItemViewModel : SelectViewModel<Item>
    {
        public ItemViewModel(Item model) : base(model)
        {
            foreach (var item in model.Parameters)
            {
                this.Parameters.Add(new ParameterViewModel(item));
            }

            foreach (var item in model.Limits)
            {
                this.Limits.Add(new LimitViewModel(item));
            }
        }
        private ObservableCollection<ParameterViewModel> _parameters = new ObservableCollection<ParameterViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ParameterViewModel> Parameters
        {
            get { return _parameters; }
            set
            {
                _parameters = value;
                RaisePropertyChanged("Parameters");
            }
        }


        private ObservableCollection<LimitViewModel> _limits = new ObservableCollection<LimitViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<LimitViewModel> Limits
        {
            get { return _limits; }
            set
            {
                _limits = value;
                RaisePropertyChanged("Limits");
            }
        } 
    }

    public class ParameterViewModel : ModelViewModel<Parameter>
    {
        public ParameterViewModel(Parameter model) : base(model)
        {

        } 
    }
    public class LimitViewModel : ModelViewModel<Limit>
    {
        public LimitViewModel(Limit model) : base(model)
        {
            foreach (var item in model.CompareConditions)
            {
                this.CompareConditions.Add(new CompareConditionViewModel(item));
            }
        }


        private ObservableCollection<CompareConditionViewModel> _compareConditions = new ObservableCollection<CompareConditionViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<CompareConditionViewModel> CompareConditions
        {
            get { return _compareConditions; }
            set
            {
                _compareConditions = value;
                RaisePropertyChanged("CompareConditions");
            }
        } 
    }

    public class CompareConditionViewModel : ModelViewModel<CompareCondition>
    {
        public CompareConditionViewModel(CompareCondition model) : base(model)
        {

        }
    }
}
