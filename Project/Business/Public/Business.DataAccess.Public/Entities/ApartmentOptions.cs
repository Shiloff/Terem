using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.DataAccess.Public.Entities
{
    public class ApartmentOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApartmentOptionId { get; set; }

        public string Name { get; set; }        
        public ICollection<Apartment> Apartments { get; set; }
    }
}
