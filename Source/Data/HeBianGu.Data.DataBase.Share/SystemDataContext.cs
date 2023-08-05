
#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.EntityFrameworkCore;
using HeBianGu.Systems.Logger;
using HeBianGu.Systems.Operation;
using HeBianGu.Systems.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
#endif
namespace HeBianGu.Data.DataBase
{
    /// <summary>
    ///public class TagConfiguration : IEntityTypeConfiguration<im_dd_tag>
    //{
    //    public void Configure(EntityTypeBuilder<im_dd_tag> builder)
    //    {
    //        builder.Property(e => e.Start).HasConversion(v => v.TryConvertToString(), v => v.TryChangeType<Point>());
    //        builder.Property(e => e.End).HasConversion(v => v.TryConvertToString(), v => v.TryChangeType<Point>());
    //        builder.Property(e => e.Fill).HasConversion(v => v.TryConvertToString(), v => v.TryChangeType<Brush>());
    //        builder.Property(e => e.Stroke).HasConversion(v => v.TryConvertToString(), v => v.TryChangeType<Brush>());
    //    }
    //}
    /// </summary>
    public class DataContext : DataContext<DataContext>
    {
#if NETCOREAPP
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        //static Random random = new Random();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ////  Do：一对一关系模型
            //modelBuilder.Entity<mbc_dv_movie>().HasOne(l => l.Designation).WithOne(l => l.Movie)
            //    .HasForeignKey<mbc_dv_movie>(l => l.DesignationID);

            //  Do ：字段映射
            //modelBuilder.Entity<hi_dd_author>().Property(l => l.Roles).HasConversion(v => string.Join(",", v.Select(l => l)), v => new ObservableCollection<string>(v.Split(',', StringSplitOptions.RemoveEmptyEntries)));
            //modelBuilder.Entity<mbc_dv_performer>().Property(l => l.Tags).HasConversion(v => string.Join(",", v.Select(l => l)), v => new ObservableCollection<string>(v.Split(',', StringSplitOptions.RemoveEmptyEntries)));
            //modelBuilder.Entity<mbc_dv_designation>().Property(l => l.Tags).HasConversion(v => string.Join(",", v.Select(l => l)), v => new ObservableCollection<string>(v.Split(',', StringSplitOptions.RemoveEmptyEntries)));

            //modelBuilder.Entity<mbc_dv_designationmagnet>().Property(l => l.Tags).HasConversion(v => string.Join(",", v.Select(l => l)), v => new ObservableCollection<string>(v.Split(',', StringSplitOptions.RemoveEmptyEntries)));

            //#if DEBUG
            //            this.InitTestData(modelBuilder);
            //#endif
        }

#endif

        //void InitTestData(ModelBuilder modelBuilder)
        //{
        //    //  Do ：种子数据

        //    string[] areas = { "中国", "日本", "韩国", "欧美" };
        //    string[] articulations = { "480p", "720p", "1080p", "2p", "4k" };
        //    string[] tags = { "番剧", "国创", "综艺", "动画", "鬼畜", "舞蹈", "娱乐", "科技", "美食", "汽车", "运动", "电影", "电视剧", "纪录片", "游戏", "音乐" };
        //    string[] types = { "动作", "冒险", "科幻", "剧情", "奇幻", "惊悚", "喜剧", "动画", "战争", "灾难", "运动", "犯罪", "传记", "历史", "恐怖" };

        //    List<mbc_dv_performer> performers = new List<mbc_dv_performer>();
        //    for (int i = 0; i < random.Next(50, 200); i++)
        //    {
        //        mbc_dv_performer performer = new mbc_dv_performer();
        //        performer.Name = "谢逊" + i;
        //        performer.Sex = random.Next(0, 1);
        //        performer.BirthDay = DateTime.Now.AddYears(-random.Next(0, 100));
        //        performer.Birthplace = areas[random.Next(0, areas.Count())];
        //        performer.Image = ImageService.FileToString(@"C:\Users\LENOVO\Pictures\66.jpg");

        //        var skip2 = random.Next(0, tags.Length - 1);
        //        var take2 = random.Next(1, tags.Length - skip2);
        //        //performer.Tags = tags.Skip(skip2).Take(take2)?.Aggregate((a, b) => a + "," + b);
        //        performer.Tags = tags.Skip(skip2).Take(take2).ToObservable();
        //        //List<mbc_dv_performerimage> images1 = new List<mbc_dv_performerimage>();
        //        //for (int j = 0; j < random.Next(5); j++)
        //        //{
        //        //    mbc_dv_performerimage image = new mbc_dv_performerimage();
        //        //    image.Name = "战争开始";
        //        //    image.Image = ImageService.FileToString(@"C:\Users\LENOVO\Pictures\66.jpg");
        //        //    images1.Add(image);
        //        //}
        //        //performer.Images = images1.ToObservable();
        //        performer.Height = random.NextDouble() * 200;
        //        performer.Hipline = random.NextDouble() * 200;
        //        performer.Bust = random.NextDouble() * 200;
        //        performer.Waist = random.NextDouble() * 200;
        //        performer.Occupation = "演员";
        //        performer.Introduction = "使用区域敏感排序规则、当前区域来比较字符串，同时忽略被比较字符串的大小写";
        //        performers.Add(performer);
        //    }

        //    modelBuilder.Entity<mbc_dv_performer>().HasData(performers);

        //    List<mbc_dv_designation> designations = new List<mbc_dv_designation>();
        //    for (int i = 100; i < random.Next(3, 20); i++)
        //    {
        //        mbc_dv_designation designation = new mbc_dv_designation();
        //        designation.Designation = "PXI" + i;
        //        designation.Name = "名称" + i;
        //        designation.Hot = random.Next(10000, 20000);
        //        designation.Size = random.Next(1000000, 2000000).ToString();
        //        designation.UpdateTime = DateTime.Now;
        //        designation.Image = ImageService.FileToString(@"C:\Users\LENOVO\Pictures\11530000.jpg");

        //        var skip2 = random.Next(0, tags.Length - 1);
        //        var take2 = random.Next(1, tags.Length - skip2);
        //        designation.Tags = tags.Skip(skip2).Take(take2).ToObservable();

        //        var skip3 = random.Next(0, performers.Count - 1);
        //        var take3 = random.Next(1, performers.Count - skip3);
        //        //designation.Performers = performers.Skip(skip3).Take(take3).ToObservable();

        //        designation.Introduction = "使用区域敏感排序规则、当前区域来比较字符串，同时忽略被比较字符串的大小写";
        //        designations.Add(designation);
        //    }
        //    modelBuilder.Entity<mbc_dv_designation>().HasData(designations);
        //}


    }

    public class DataContext<T> : DbContext where T : DbContext
    {
        public DataContext(DbContextOptions<T> options) : base(options)
        {

        }

        public DbSet<hi_dd_author> hi_dd_authors { get; set; }
        public DbSet<hi_dd_role> hi_dd_roles { get; set; }
        public DbSet<hi_dd_user> hi_dd_users { get; set; }

        public DbSet<hi_dd_operation> hi_dd_operations { get; set; }

        public DbSet<hl_dm_debug> hl_dm_debugs { get; set; }
        public DbSet<hl_dm_error> hl_dm_errors { get; set; }
        public DbSet<hl_dm_fatal> hl_dm_fatals { get; set; }
        public DbSet<hl_dm_info> hl_dm_infos { get; set; }
        public DbSet<hl_dm_warn> hl_dm_warns { get; set; }
    }
}
