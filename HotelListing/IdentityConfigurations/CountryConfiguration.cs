using HotelListing.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.IdentityConfigurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
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
                ); ;
        }
    }
}
