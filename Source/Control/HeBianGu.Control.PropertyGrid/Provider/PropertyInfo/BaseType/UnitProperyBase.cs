using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{
    /// <summary> 带有单位的属性 </summary>
    public class UnitProperyBase : DoubleProperty
    {
        List<Unit> _units = new List<Unit>();

        public List<Unit> Units
        {
            get => _units;
            set
            {
                _units = value;
                RaisePropertyChanged();
            }
        }

        Unit _selectItem;

        public Unit SelectItem
        {
            get => _selectItem;
            set
            {
                // 切换单位计算
                if (_selectItem != null && value != null)
                {
                    double convert = this.ConvertToValue(this.Text) / _selectItem.Param;

                    this.Text = (convert * value.Param).ToString();
                }

                _selectItem = value;

                RaisePropertyChanged();
            }
        }

        public override void LoadUnit(UnitGroup u)
        {
            if (u == null) return;

            this.Units.Clear();

            foreach (var item in u.Units)
            {
                Unit unit = new Unit();
                unit.Name = item.Name;
                unit.Param = item.Param;
                this.Units.Add(unit);
            }
            // 暂时设置单位第一个 后面根据配置设置
            this.SelectItem = this.Units?.FirstOrDefault();
        }
    }
}
