using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.DTOs
{
    public class CountryDto : CreateCountryDto
    {
        public int Id { get; set; }
        public IList<HotelDto> Hotels { get; set; }
    }
}
