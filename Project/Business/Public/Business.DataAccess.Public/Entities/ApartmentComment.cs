using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.DataAccess.Public.Entities
{
    public class ApartmentComment
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ApartmentCommentId { get; set; }

        public string Text { get; set; }
        public DateTime? Date { get; set; }
        [ForeignKey("Profile")]
        public long? ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
        [ForeignKey("Apartment")]
        public long? ApartmentId { get; set; }
        [ForeignKey("ApartmentId")]
        public Apartment Apartment { get; set; }
        [ForeignKey("ApartmentCommentId")]
        public ICollection<ApartmentCommentLike> ApartmentCommentsLikes { get; set; }
    }
}
