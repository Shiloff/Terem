using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.DataAccess.Public.Entities
{
    public class ProfileActionComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProfileActionCommentId { get; set; }     
        public string Text { get; set; }
        public DateTime? Date { get; set; }
        [ForeignKey("Profile")]
        public long? ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
        [ForeignKey("ProfileAction")]
        public long? ProfileActionId { get; set; }
        [ForeignKey("ProfileActionId")]
        public ProfileAction ProfileAction { get; set; }
        #region Likes
        [ForeignKey("ProfileActionCommentId")]
        public ICollection<ProfileActionCommentLike> ProfileActionsCommentsLikes { get; set; }
        #endregion

        public string DateString
        {
            get
            {
                return Core.Core.GetDateString(Date);
            }
        }
    }
}
