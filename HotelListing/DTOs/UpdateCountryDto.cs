using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.DTOs
{
    public class UpdateCountryDto
    {
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Country Name is too Long")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 4, ErrorMessage = "Short Name is too Long")]
        public string ShortName { get; set; }

        //The hotels in a country can be updated simultaneously while updating country using the collection
        public IList<CreateHotelDto> Hotels { get; set; }
    }
}
