using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Business.DataAccess.Public.Entities
{

    public class RegionCountry
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
        public int Sort { get; set; }
        [JsonIgnore]
        public ICollection<RegionState> States { get; set; }
    }
}
