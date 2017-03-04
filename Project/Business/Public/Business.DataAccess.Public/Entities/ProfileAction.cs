using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.DataAccess.Public.Entities
{
    public class ProfileAction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProfileActionId { get; set; }
        public int? ProfileActionTypeId { get; set; }
        [ForeignKey("Profile")]
        public long? ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
        [ForeignKey("ProfileWho")]
        public long? ProfileWhoId { get; set; }
        [ForeignKey("ProfileId")]
        public Profile ProfileWho { get; set; }
        [ForeignKey("ProfileActionId")]
        public ICollection<ProfileActionComment> ProfileActionComments { get; set; }
        public string Text { get; set; }
        public DateTime? Date { get; set; }
        #region Likes
        [ForeignKey("ProfileActionId")]
        public ICollection<ProfileActionLike> ProfileActionsLikes { get; set; }
        #endregion
        [ForeignKey("Apartment")]
        public long? ApartmentId { get; set; }
        [ForeignKey("ApartmentId")]
        public Apartment Apartment { get; set; }


        public string DateString
        {
            get
            {
                return Core.Core.GetDateString(Date);
            }
        }
        private int _commentsCount;
        [NotMapped]
        public int CommentsCount
        {
            get
            {
                return _commentsCount != 0 ? _commentsCount : ProfileActionComments != null ? ProfileActionComments.Count : -1;
            }
            set
            {
                _commentsCount = value;
            }
        }
    }
}
