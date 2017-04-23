using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Business.DataAccess.Public.Entities
{
    public class RegionCity
    {
        [Key]
        public int Id { get; set; }
        public string Value { get;set; }
        public int Sort { get; set; }

        public ICollection<Profile> Profiles { get; set; }

        public int? StateId { get; set; }
        [ForeignKey("StateId")]
        public RegionState State { get; set; }

        public int? CountryId { get; set; }
        [ForeignKey("CountryId")]
        public RegionCountry Country { get; set; }
    }
}
