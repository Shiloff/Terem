using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.DataAccess.Public.Entities
{
    public class ApartmentType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApartmentTypeId { get; set; }

        public string Name { get; set; }
         [ForeignKey("ApartmentTypeId")]
        public ICollection<Apartment> Apartments { get; set; }
    }
}
