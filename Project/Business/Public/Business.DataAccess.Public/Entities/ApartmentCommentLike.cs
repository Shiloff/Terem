using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.DataAccess.Public.Entities
{
    public class ApartmentCommentLike
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ApartmentCommentLikeId { get; set; }
        public DateTime? Date { get; set; }
        [ForeignKey("Profile")]
        public long? ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
        [ForeignKey("ApartmentComment")]
        public long? ApartmentCommentId { get; set; }
        [ForeignKey("ApartmentCommentId")]
        public ApartmentComment ApartmentComment { get; set; }
    }
}
