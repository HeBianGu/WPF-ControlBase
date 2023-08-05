using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
#if NET || NETCOREAPP
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
#else
using System.Data.SqlClient;
#endif

namespace HeBianGu.Systems.Design
{
    [Displayer(Name = "SqlServer查询模型", GroupName = "[表格]", Icon = "\xe6e7", Description = "仓储模型表格数据")]
    public class SqlServerTableDataContext : SqlServerDataContextBase<DataGridDesignPresenter>
    {
        public override void RefreshPresenter(DataGridDesignPresenter presenter)
        {
#if NET || NETCOREAPP
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(QueryString, connection);
                    command.Connection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    DataTable table = new DataTable();
                    table.Load(dr);
                    presenter.ColumnPropertyInfos = null;
                    presenter.ItemsSource = table.DefaultView;
                }
            }
            catch (System.Exception ex)
            {
                Trace.Assert(false, ex.Message);
                Logger.Instance?.Error(ex);
            }
#else
           try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(QueryString, connection);
                    command.Connection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    DataTable table = new DataTable();
                    table.Load(dr);
                    presenter.ColumnPropertyInfos = null;
                    presenter.ItemsSource = table.DefaultView;
                }
            }
            catch (System.Exception ex)
            {
                Trace.Assert(false, ex.Message);
                Logger.Instance?.Error(ex);
            }
#endif

        }
    }


    [Displayer(Name = "SqlServer查询模型", GroupName = "[浮点]", Icon = "\xe6e7", Description = "浮点数据")]
    public class SqlServerDoublesDataContext : SqlServerDataContextBase<IDisplayDoublesData>
    {
        private int _valueIndex = 0;
        /// <summary> 说明  </summary>
        public int ValueIndex
        {
            get { return _valueIndex; }
            set
            {
                _valueIndex = value;
                RaisePropertyChanged();
            }
        }

        private int _displayIndex = 1;
        /// <summary> 说明  </summary>
        public int DisplayIndex
        {
            get { return _displayIndex; }
            set
            {
                _displayIndex = value;
                RaisePropertyChanged();
            }
        }


        public override void RefreshPresenter(IDisplayDoublesData presenter)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(QueryString, connection);
                    command.Connection.Open();
                    var dr = command.ExecuteReader();
                    List<double> ds = new List<double>();
                    List<string> dsplay = new List<string>();
                    while (dr.Read())
                    {
                        if (dr.VisibleFieldCount > ValueIndex)
                        {
                            var obj = dr.GetValue(ValueIndex);
                            ds.Add(Convert.ToDouble(obj.ToString()));
                        }
                        if (dr.VisibleFieldCount > DisplayIndex)
                            dsplay.Add(dr.GetString(DisplayIndex));
                    }
                    presenter.RefreshData(ds.Skip(Skip).Take(Take));
                    presenter.xDisplay = dsplay.ToObservable();
                }
            }
            catch (System.Exception ex)
            {
                Trace.Assert(false, ex.Message);
                Logger.Instance?.Error(ex);
            }
        }
    }


    [Displayer(Name = "SqlServer查询模型", GroupName = "[文本]", Icon = "\xe6e7", Description = "浮点数据")]
    public class SqlServerTextDataContext : SqlServerDataContextBase<ITextData>
    {
        public override void RefreshPresenter(ITextData presenter)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(QueryString, connection);
                    command.Connection.Open();
                    var dr = command.ExecuteReader();
                    List<double> ds = new List<double>();
                    List<string> dsplay = new List<string>();
                    dr.Read();
                    presenter.Text = dr.GetString(0);
                }
            }
            catch (System.Exception ex)
            {
                Trace.Assert(false, ex.Message);
                Logger.Instance?.Error(ex);
            }
        }
    }

    /// <summary>
    /// 用用数据库sql语句查询
    /// </summary>
    public abstract class SqlServerDataContextBase<T> : RangeDesignDataContextBase<T> where T : class
    {
        private string _connectionString;
        [Display(Name = "连接字符串")]
        [SqlServerConnection]
        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                _connectionString = value;
                RaisePropertyChanged();
            }
        }

        private string _queryString;
        [Display(Name = "查询语句")]
        public string QueryString
        {
            get { return _queryString; }
            set
            {
                _queryString = value;
                DispatcherRaisePropertyChanged();
            }
        }
    }

    [System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class SqlServerConnectionAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(value?.ToString()))
                {
                    connection.Open();
                }
                return true;
            }
            catch (System.Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
    }
}
