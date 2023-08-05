using HeBianGu.Base.WpfBase;
using HeBianGu.Service.AppConfig;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Design
{
    [Displayer(Name = "测试实体数据源")]
    public class TestDesignDataSource : DesignDataSourceBase
    {
        public TestDesignDataSource()
        {
            this.AddDataContext(new PropertyTextDataContext(this, "Value"));
            this.AddDataContext(new SelectPropertysTextDataContext(this));

            this.AddDataContext(new DoublesDataContext(this.Stuents.Take(20).Select(x => x.Score)) { Name = "学生成绩" });
            this.AddDataContext(new PropertyDoublesDataContext<Student>(this.Stuents) { Name = "学生模型" });

            this.AddDataContext(new TableDataContext<Student>(this.Stuents) { Name = "学生模型" });

            this.AddDataContext(new SqliteDoublesDataContext() { ConnectionString = "Data Source=" + Path.Combine(SystemPathSetting.Instance.Data, "data.db"), QueryString = "SELECT COUNT(*) 数量,message 消息名称 FROM hl_dm_infos group by message;" });
            this.AddDataContext(new SqliteTableDataContext() { ConnectionString = "Data Source=" + Path.Combine(SystemPathSetting.Instance.Data, "data.db"), QueryString = "SELECT name,title,value,message,CDATE FROM hl_dm_infos limit 20;" });
            this.AddDataContext(new SqliteTextDataContext() { ConnectionString = "Data Source=" + Path.Combine(SystemPathSetting.Instance.Data, "data.db"), QueryString = "SELECT COUNT(*) FROM hl_dm_infos" });

            this.AddDataContext(new SqlServerDoublesDataContext() { ConnectionString = "Server=DESKTOP-UQVBO72\\SQLEXPRESS; Database=master; Trusted_Connection=False; uid=sa; pwd=123456;Initial Catalog=Cell; MultipleActiveResultSets=true;Connect Timeout=1", QueryString = "SELECT COUNT(*) 数量,Type 消息名称 FROM hi_dd_operations group by Type;" });
            this.AddDataContext(new SqlServerTableDataContext() { ConnectionString = "Server=DESKTOP-UQVBO72\\SQLEXPRESS; Database=master; Trusted_Connection=False; uid=sa; pwd=123456;Initial Catalog=Performance3; MultipleActiveResultSets=true;Connect Timeout=1", QueryString = "SELECT top(20) UserName,Type,Title,Message,CDATE FROM hi_dd_operations;" });
            this.AddDataContext(new SqlServerTextDataContext() { ConnectionString = "Server=DESKTOP-UQVBO72\\SQLEXPRESS; Database=master; Trusted_Connection=False; uid=sa; pwd=123456;Initial Catalog=Performance3; MultipleActiveResultSets=true;Connect Timeout=1", QueryString = "SELECT COUNT(*) FROM hi_dd_operations" });
        }

        private List<Student> _stuents = Student.Randoms();
        [XmlIgnore]
        [Browsable(false)]
        public List<Student> Stuents
        {
            get { return _stuents; }
            set
            {
                _stuents = value;
                RaisePropertyChanged();
            }
        }

        private string _text = "Text";
        [XmlIgnore]
        [Browsable(false)]
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged();
            }
        }

        private string _value = "Value";
        [XmlIgnore]
        [Browsable(false)]
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }

    }
}
