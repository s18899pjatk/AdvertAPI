    using AdvertAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Configurations
{
    public class BuildingEfConfig : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.HasKey(b => b.IdBuilding);
            builder.Property(b => b.IdBuilding).ValueGeneratedOnAdd();
            builder.Property(b => b.Street).HasMaxLength(100).IsRequired();
            builder.Property(b => b.StreetNumber).IsRequired();
            builder.Property(b => b.City).HasMaxLength(100).IsRequired();
            builder.Property(b => b.Height).IsRequired().HasColumnType("decimal(6,2)"); ;
            builder.ToTable("Building");
            var buildings = new List<Building>()
            {
                new Building()
                {
                    IdBuilding = 1,
                    City = "Warsaw",
                    Street = "Krucza",
                    StreetNumber = 21,
                    Height = 16.5m
                },
                new Building()
                {
                    IdBuilding = 2,
                    City = "Warsaw",
                    Street = "Zlota",
                    StreetNumber = 51,
                    Height = 10m
                },
                new Building()
                {
                    IdBuilding = 3,
                    City = "Krakow",
                    Street = "Stara",
                    StreetNumber = 2,
                    Height = 7m
                },
                new Building()
                {
                    IdBuilding = 4,
                    City = "Poznan",
                    Street = "Lublinska",
                    StreetNumber = 3,
                    Height = 12m
                }
            };
            builder.HasData(buildings);

        }
    }
}
