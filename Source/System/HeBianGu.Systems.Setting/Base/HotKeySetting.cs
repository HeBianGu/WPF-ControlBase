// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Setting
{
    /// <summary> 热键</summary>
    [Displayer(Name = "热键", GroupName = SystemSetting.GroupBase)]
    public class HotKeySetting : Setting<HotKeySetting>
    {
        private ObservableCollection<HotKeyItem> _hotKeys = new ObservableCollection<HotKeyItem>();
        /// <summary> 说明  </summary>
        public ObservableCollection<HotKeyItem> HotKeys
        {
            get { return _hotKeys; }
            set
            {
                _hotKeys = value;
                RaisePropertyChanged("HotKeys");
            }
        }

        public override void Load()
        {
            base.Load();
        }
    }

    public class HotKeyItem
    {
        public ModifierKeys ModifierKeys { get; set; }

        public Key Key { get; set; }

        public string DisplayName { get; set; }

        [XmlIgnore]
        public ICommand Command { get; set; }
    }



    public class HotKeyService : LazyInstance<HotKeyService>
    {
        public HotKeySetting GetHotKey(object obj)
        {
            HotKeySetting result = new HotKeySetting();

            System.Collections.Generic.IEnumerable<PropertyInfo> cmds = obj.GetType().GetProperties().Where(x => typeof(ICommand).IsAssignableFrom(x.PropertyType));

            foreach (PropertyInfo cmd in cmds)
            {
                BrowsableAttribute browsable = cmd.GetCustomAttribute<BrowsableAttribute>();

                if (browsable?.Browsable != true) continue;

                DisplayAttribute display = cmd.GetCustomAttribute<DisplayAttribute>();

                HotKeyAttribute hotKey = cmd.GetCustomAttribute<HotKeyAttribute>();

                if (hotKey == null) continue;

                HotKeyItem hotKeyItem = new HotKeyItem();

                hotKeyItem.Command = cmd.GetValue(obj) as ICommand;

                hotKeyItem.Key = hotKey.Key;

                hotKeyItem.ModifierKeys = hotKey.ModifierKeys;

                hotKeyItem.DisplayName = hotKey.DisplayName ?? display?.Name;

                result.HotKeys.Add(hotKeyItem);
            }

            return result;
        }

    }
}
