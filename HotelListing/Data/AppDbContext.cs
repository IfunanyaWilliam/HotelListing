using HotelListing.Models;
using HotelListing.IdentityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data
{
    public class AppDbContext : IdentityDbContext<ApiUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());

            builder.Entity<Country>().HasData(
                    new Country
                    {
                        Id = 1,
                        Name = "Nigeria",
                        ShortName = "NGN"
                    },
                    new Country
                    {
                        Id = 2,
                        Name = "Jamaica",
                        ShortName = "JM"
                    },
                    new Country
                    {
                        Id = 3,
                        Name = "Congo",
                        ShortName = "CGO"
                    },
                    new Country
                    {
                        Id = 4,
                        Name = "Cayman Island",
                        ShortName = "CI"
                    }
                );

            builder.Entity<Hotel>().HasData(
                    new Hotel
                    {
                        Id = 1,
                        Name = "Sheraton",
                        Address = "Shagari Avenue, Abuja",
                        CountryId = 1,
                        Rating = 5
                    },
                    new Hotel
                    {
                        Id = 2,
                        Name = "Sandal Resort and Spa",
                        Address = "Negril, Utah",
                        CountryId = 2,
                        Rating = 4.5
                    },
                    new Hotel
                    {
                        Id = 3,
                        Name = "Grand Royal Suit",
                        Address = "Hillview, Congo",
                        CountryId = 3,
                        Rating = 4
                    },
                   new Hotel
                   {
                       Id = 4,
                       Name = "Nicon Nuga",
                       Address = "Palm Beach road, Cayman County",
                       CountryId = 4,
                       Rating = 5
                   }
                );
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
    }
}
