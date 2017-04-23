using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Business.DataAccess.Public.Entities
{

    public class RegionCountry
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
        public int Sort { get; set; }

        public ICollection<Profile> Profiles { get; set; }

        public ICollection<RegionState> States { get; set; }
        public ICollection<RegionCity> Cities { get; set; }
    }
}
