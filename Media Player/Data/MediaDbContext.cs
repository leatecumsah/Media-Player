//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Media_Player.Models;
//using Microsoft.EntityFrameworkCore;

////Damit die App mit der Datenbank reden kann. Verbindet Modelle mit Datenbank

//namespace Media_Player.Data
//{
//    class MediaDbContext : DbContext
//    {
//        public DbSet<MediaItem> MediaItems { get; set; }
//        protected override void OnConfiguring(DbContextOptionsBuilder options)
//        {
//            var connectionString = "server=localhost;database=media_db;user=root;password=DEINPASSWORT";
//            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
//        }
//    }
//}
