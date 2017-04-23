using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.DataAccess.Public.Entities
{
    public class RegionState
    {
        [Key]
        public int Id { get; set; }

        public string Value { get; set; }
        public int Sort { get; set; }

        public ICollection<Profile> Profiles { get; set; }

        public ICollection<RegionCity> Cities { get; set; }

        public int? CountryId { get; set; }
        [ForeignKey("CountryId")]
        public RegionCountry Country { get; set; }
    }
}
