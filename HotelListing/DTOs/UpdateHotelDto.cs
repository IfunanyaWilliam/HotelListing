using System.ComponentModel.DataAnnotations;

namespace HotelListing.DTOs
{
    public class UpdateHotelDto
    {
        [Required]
        [StringLength(maximumLength: 150, ErrorMessage = "Hotel Name is too Long")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 450, ErrorMessage = "Address is too Long")]
        public string Address { get; set; }

        [Required]
        [Range(1, 5)]
        public double Rating { get; set; }

        [Required]
        public int CountryId { get; set; }
    }
}
