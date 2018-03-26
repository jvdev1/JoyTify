using JoyTify.Core.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoyTify.Core
{
    public class DatabaseContext : DbContext
    {
        public DbSet<DBVideo> EVideos { get; set; }

        public DbSet<DBLastSearch> ELastSearchs { get; set; }


        private string _databasePath;

        public DatabaseContext(string databasePath)
        {
            _databasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}
