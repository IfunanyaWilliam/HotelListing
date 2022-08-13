namespace HotelListing.DTOs
{
    public class HotelDto : CreateHotelDto
    {
        public int Id { get; set; }
        public CountryDto Country { get; set; }
    }
}
