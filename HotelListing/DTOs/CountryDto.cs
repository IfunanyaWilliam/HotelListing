using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HotelListing.DTOs
{
    public class CountryDto : CreateCountryDto
    {
        public int Id { get; set; }

        [JsonIgnore]
        public IList<HotelDto> Hotels { get; set; }
    }
}
