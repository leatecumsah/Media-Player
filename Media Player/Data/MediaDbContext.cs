using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Media_Player.Models;
using Microsoft.EntityFrameworkCore;

//Damit die App mit der Datenbank reden kann. Verbindet Modelle mit Datenbank

namespace Media_Player.Data
{
    class MediaDbContext : DbContext
    {
        public DbSet<VideoFile> VideoFile { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
           
            options.UseMySQL("Server=localhost;Database=mediplayer_db;Uid=root;Pwd=;");
        }
    }


    //Test um verbindung zur DB zu prüfen

    //public static class DbConnectionTest
    //{
    //    public static void TestConnection()
    //    {
    //        try
    //        {
    //            using var context = new MediaDbContext();
    //            var count = context.VideoFile.Count();
    //            MessageBox.Show($"Verbindung erfolgreich! Anzahl der MediaItems: {count}");
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine($"Verbindung Fehlgeschlagen:");
    //            MessageBox.Show(ex.Message);
    //        }

    //    }
    //}
}
