using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.DataAccess.Public.Entities
{
    public class ProfileActionCommentLike
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProfileActionCommentLikeId { get; set; }       
        public DateTime? Date { get; set; }
        [ForeignKey("Profile")]
        public long? ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
        [ForeignKey("ProfileActionComment")]
        public long? ProfileActionCommentId { get; set; }
        [ForeignKey("ProfileActionCommentId")]
        public ProfileActionComment ProfileActionComment { get; set; }
    }
}
