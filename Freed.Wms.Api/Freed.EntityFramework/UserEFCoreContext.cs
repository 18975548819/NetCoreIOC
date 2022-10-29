using DataEntities.InterfaceModel;
using Microsoft.EntityFrameworkCore;
using System;

namespace Freed.EntityFramework
{
    public class UserEFCoreContext:DbContext
    {
        //public string _sqlConn = "Source=127.0.0.1;User ID=sa;Password=sa123456;Database=CustomBase;Max Pool Size = 512;";

        //protected UserEFCoreContext(string sqlconn)
        //{
        //    _sqlConn = sqlconn;
        //}

        public DbSet<Users> usersInfo { get; set; }

        //这里是配置
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //DbContextOptionsBuilder注入
            base.OnConfiguring(optionsBuilder);
            //设置连接字符串
            optionsBuilder.UseSqlServer("server=localhost;uid=sa;pwd=sa123456;database=CustomBase;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 映射
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Users>().HasKey(u => u.Id);

            //注入ModelBuilder
            base.OnModelCreating(modelBuilder);
            //获取当前程序集默认的是查找所有继承了IEntityTypeConfiguration的类
            //modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
