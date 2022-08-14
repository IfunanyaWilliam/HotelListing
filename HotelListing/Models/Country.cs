using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelListing.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string ShortName { get; set; }

        [JsonIgnore]
        public virtual IList<Hotel> Hotels { get; set; }
    }
}
