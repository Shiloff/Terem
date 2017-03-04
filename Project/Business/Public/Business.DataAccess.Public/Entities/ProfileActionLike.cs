using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.DataAccess.Public.Entities
{
    public class ProfileActionLike
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProfileActionLikeId { get; set; }
        [ForeignKey("ProfileAction")]
        public long? ProfileActionId { get; set; }
        [ForeignKey("ProfileActionId")]
        public ProfileAction ProfileAction{ get; set; }
        [ForeignKey("Profile")]
        public long? ProfileId { get; set; }
        [ForeignKey("Profile")]
        public Profile Profile { get; set; }
        public DateTime? Date { get; set; }
    }
}
