using AdvertAPI.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Entities
{
    public class CampaignDbContext : DbContext
    {
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Client> Clients { get; set; }

        public CampaignDbContext()
        {

        }

        public CampaignDbContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder
            optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Data Source=db-mssql;Initial Catalog=s18899;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BannerEfConfig());
            modelBuilder.ApplyConfiguration(new BuildingEfConfig());
            modelBuilder.ApplyConfiguration(new CampaignEfConfig());
            modelBuilder.ApplyConfiguration(new ClientEfConfig());
        }
    }
}
