using AdvertAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Configurations
{
    public class ClientEfConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.IdClient);
            builder.Property(c => c.IdClient).ValueGeneratedOnAdd();
            builder.Property(c => c.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(c => c.LastName).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Email).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Phone).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Login).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Password).HasMaxLength(255).IsRequired();
            builder.Property(c => c.RefreshToken).IsRequired();
            builder.Property(c => c.Salt).IsRequired();

            builder.ToTable("Client");
            var clients = new List<Client>()
            {
                new Client
                {
                    IdClient = 1,
                    FirstName = "Bobby",
                    LastName = "Broody",
                    Email = "badsa@gmail.com",
                    Login = "dafasf21",
                    Phone = "+43131234",
                    Password = "B7KqEYl86BI7WHWSGM7Vv0tuYwPz1lc7p9egQonkkAQ=", //dsa
                    RefreshToken = "asdfaafasfscsac-fsaf",
                    Salt = "vrCE57covNR7T2gk3E6XLw=="
                }

            };
            builder.HasData(clients);
        }
    }
}
