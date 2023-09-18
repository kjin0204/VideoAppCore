//Install-Package Microsoft.EntityFrameworkCore.SqlServer
//Install-Package System.Configuration.ConfigurationManager

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace VideoAppCoreModels
{
    public class VideoDbContext : DbContext
    {
        public VideoDbContext(DbContextOptions<VideoDbContext> options)
            : base(options)
        {
            //공식과 같은코드
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 닷넷 프레임워크 기반에서 호출되는 코드 영역:
            // App.Config 또는 Web.Config의 연결 문자열 사용
            if(!optionsBuilder.IsConfigured)
            {
                string connectionString = ConfigurationManager.ConnectionStrings[
                    "DefaultConnection"].ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        /// <summary>
        /// 비디오앱
        /// </summary>
        public DbSet<Video> Videos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //[!] vidoes 테이블의 created,modified 열을 자동으로 GetDate() 제약 조건을 부여 함.
            modelBuilder.Entity<Video>().Property(x => x.Created).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<Video>().Property(x => x.Modified).HasDefaultValueSql("GetDate()");
        }
    }
}
