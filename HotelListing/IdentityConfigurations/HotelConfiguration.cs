using HotelListing.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.IdentityConfigurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
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
    }
}
