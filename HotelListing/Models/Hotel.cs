using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HotelListing.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        [JsonIgnore] //This is done to avoid cyclic dependency
        public Country Country { get; set; }
    }
}
