using AdvertAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Configurations
{
    public class BannerEfConfig : IEntityTypeConfiguration<Banner>
    {
        public void Configure(EntityTypeBuilder<Banner> builder)
        {
            builder.HasKey(b => b.IdAdvertisement);
            builder.Property(b => b.IdAdvertisement).ValueGeneratedOnAdd();
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.Price).IsRequired().HasColumnType("decimal(6,2)");
            builder.Property(b => b.Area).IsRequired().HasColumnType("decimal(6,2)");
            builder.ToTable("Banner");
            builder.HasOne(b => b.Campaign)
                .WithMany(c => c.Banners)
                .HasForeignKey(b => b.IdCampaign)
                .IsRequired();
            var banners = new List<Banner>()
            {
                new Banner()
                {
                    IdAdvertisement = 1,
                    Name = "Coca-Cola",
                    Area = 1.2m,
                    Price = 2333,
                    IdCampaign = 1,
                },
                new Banner()
                {
                    IdAdvertisement = 2,
                    Name = "Sprite",
                    Area = 1.6m,
                    Price = 1331,
                    IdCampaign = 1,
                },
                new Banner()
                {
                    IdAdvertisement = 3,
                    Name = "Rollin",
                    Area = 1,
                    Price = 413,
                    IdCampaign = 1,
                },
                    new Banner()
                {
                    IdAdvertisement = 4,
                    Name = "Donutik",
                    Area = 2.1m,
                    Price = 311,
                    IdCampaign = 2,
                },
                    new Banner()
                {
                    IdAdvertisement = 5,
                    Name = "McDonalds",
                    Area = 1.9m,
                    Price = 566,
                    IdCampaign = 2,
                }
            };
            builder.HasData(banners);
        }

    }
}
