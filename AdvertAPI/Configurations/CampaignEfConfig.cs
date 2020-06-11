using AdvertAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Configurations
{
    public class CampaignEfConfig : IEntityTypeConfiguration<Campaign>
    {
        public void Configure(EntityTypeBuilder<Campaign> builder)
        {
            builder.HasKey(c => c.IdCampaign);
            builder.Property(c => c.IdCampaign).ValueGeneratedOnAdd();
            builder.Property(c => c.StartDate).IsRequired();
            builder.Property(c => c.EndDate).IsRequired();
            builder.Property(c => c.PricePerSquareMeter).IsRequired().HasColumnType("decimal(6,2)");
            builder.ToTable("Campaign");
            builder.HasOne(c => c.Client)
                .WithMany(c => c.Campaigns)
                .HasForeignKey(c => c.IdClient).IsRequired();

            builder.HasOne(c => c.FromBuilding)
                .WithMany(b => b.FromCampaigns)
                .HasForeignKey(c => c.FromIdBuilding)
                .OnDelete(DeleteBehavior.NoAction).IsRequired();

            builder.HasOne(c => c.ToBuilding)
               .WithMany(b => b.ToCampaigns)
               .HasForeignKey(c => c.ToIdBuilding)
               .OnDelete(DeleteBehavior.NoAction).IsRequired();

            var campaigns = new List<Campaign>()
           {
              new Campaign()
              {
                  IdCampaign = 1,
                  IdClient = 1,
                  StartDate = new DateTime(2020,2,1).Date,
                  EndDate = new DateTime(2020,2,2).Date,
                  PricePerSquareMeter = 3344,
                  FromIdBuilding = 1,
                  ToIdBuilding = 3
              },
               new Campaign()
              {
                  IdCampaign = 2,
                  IdClient = 2,
                  StartDate = new DateTime(2020,2,3).Date,
                  EndDate = new DateTime(2020,2,9).Date,
                  PricePerSquareMeter = 3344,
                  FromIdBuilding = 1,
                  ToIdBuilding = 3
              },
                  new Campaign()
              {
                  IdCampaign = 3,
                  IdClient = 2,
                  StartDate = new DateTime(2020,2,9).Date,
                  EndDate = new DateTime(2020,2,15).Date,
                  PricePerSquareMeter = 3344,
                  FromIdBuilding = 4,
                  ToIdBuilding = 6
              }
           };
            builder.HasData(campaigns);
        }
    }
}
