using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using YazLab1.Models.Data.Models;

namespace YazLab1.Models.Data.Context
{
    public class KouDatabaseContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public KouDatabaseContext()
        {
            Database.Connection.ConnectionString = "server=ONURPC;database=KouDatabase;Trusted_Connection=true;";
        }
        public DbSet<User> User { get; set; }
        public DbSet<Announcement> Duyuru { get; set; }
        public DbSet<SliderItem> SliderItem { get; set; }
    }


}