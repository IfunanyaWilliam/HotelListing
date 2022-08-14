using System.Text.Json.Serialization;

namespace HotelListing.DTOs
{
    public class HotelDto : CreateHotelDto
    {
        public int Id { get; set; }

        [JsonIgnore]
        public CountryDto Country { get; set; }
    }
}
