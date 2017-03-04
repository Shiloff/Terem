using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.DataAccess.Public.Entities
{
    public class ApartmentPhotoLink
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ApartmentPhotoLinkId { get; set; }

        public string Link { get; set; }
        public int TypeId { get; set; }
        [ForeignKey("ApartmentPhoto")]
        public long? ApartmentPhotoId { get; set; }
        [ForeignKey("ApartmentPhotoId")]
        public ApartmentPhoto ApartmentPhoto { get; set; }

    }
}
