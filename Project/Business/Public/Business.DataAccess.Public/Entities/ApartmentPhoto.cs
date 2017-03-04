using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.DataAccess.Public.Entities
{
    public class ApartmentPhoto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ApartmentPhotoId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
         [ForeignKey("Apartment")]
        public long? ApartmentId { get; set; }
        [ForeignKey("ApartmentId")]
        public Apartment Apartment { get; set; }
        [ForeignKey("ApartmentPhotoId")]
        public ICollection<ApartmentPhotoLink> Links { get; set; }
        [ForeignKey("DefaultPhotoId")]
        public List<Apartment> DefaultApartment { get; set; }
    }
}
