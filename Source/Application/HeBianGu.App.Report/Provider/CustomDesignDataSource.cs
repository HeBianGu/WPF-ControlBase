using HeBianGu.Base.WpfBase;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HeBianGu.Systems.Design;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HeBianGu.Control.PropertyGrid;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Linq;
using System.ComponentModel;
using System.Xml.Serialization;
using HeBianGu.Systems.Operation;
using HeBianGu.Systems.Identity;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Windows;
using System.Linq.Dynamic;

namespace HeBianGu.App.Report
{
    [Displayer(Name = "自定义实体数据源")]
    public class CustomDesignDataSource : TestDesignDataSource
    {
        public CustomDesignDataSource()
        {
            this.AddDataContext(new RepositoryDoublesDataContext<hi_dd_operation>());
            this.AddDataContext(new RepositoryDoublesDataContext<hi_dd_user>());
            this.AddDataContext(new RepositoryDoublesDataContext<hi_dd_role>());
            this.AddDataContext(new RepositoryDoublesDataContext<hi_dd_author>());
            this.AddDataContext(new RepositoryTableDataContext<hi_dd_operation>());
            this.AddDataContext(new RepositoryTableDataContext<hi_dd_user>());
            this.AddDataContext(new RepositoryTableDataContext<hi_dd_role>());
            this.AddDataContext(new RepositoryTableDataContext<hi_dd_author>());
        }
    }
}
