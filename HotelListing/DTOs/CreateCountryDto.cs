using System.ComponentModel.DataAnnotations;

namespace HotelListing.DTOs
{
    public class CreateCountryDto
    {
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Country Name is too Long")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 4, ErrorMessage = "Short Name is too Long")]
        public string ShortName { get; set; }
    }
}
