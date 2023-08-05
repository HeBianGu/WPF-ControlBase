数据库迁移命令
步骤一：添加如下代码，配置设计时 DbContext 创建
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlite("Data Source=Migration.db");

            return new DataContext(optionsBuilder.Options);
        }
    }
步骤二：保证整个解决方案生成成功

步骤三：DbContext必须放在当前程序集下面

步骤二：设置启动项
HeBianGu.DataBase.Sqlite

步骤三：执行迁移命令：生成迁移文件
add-migration init -project HeBianGu.DataBase.Sqlite

步骤四：执行更新数据库命令：同步到数据库中
Update-Database -project HeBianGu.DataBase.Sqlite

生产环境自动执行迁移，替换创建数据库方法
db.Database.Migrate();替换掉 db.Database.EnsureCreated();


