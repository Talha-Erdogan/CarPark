using CarPark.API.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPark.API.Data
{
    public class AppDBContext : DbContext
    {

        private IConfiguration _config;

        public AppDBContext(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config.GetConnectionString("AppDbConnectionString"));
            }
        }

        //entities
        public DbSet<Auth> Auth { get; set; }
        public DbSet<Car> Car { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<LocationMove> LocationMove { get; set; }
        public DbSet<Personnel> Personnel { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<ProfileDetail> ProfileDetail { get; set; }
        public DbSet<ProfilePersonnel> ProfilePersonnel { get; set; }
       
    }
}
