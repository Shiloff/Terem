using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Business.DataAccess.Public.Entities
{
    public class RegionState
    {
        [Key]
        public int Id { get; set; }

        public string Value { get; set; }
        public int Sort { get; set; }

        [JsonIgnore]
        public ICollection<RegionCity> Cities { get; set; }

        [JsonIgnore]
        public RegionCountry Country { get; set; }
    }
}
