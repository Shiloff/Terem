using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Business.DataAccess.Public.Entities
{
    public class ProfileSex
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfileSexId { get; set; }
        public string Name { get; set; }
        [AllowHtml]
        public string Icon { get; set; }
        [ForeignKey("ProfileSexId")]
        public ICollection< Profile>Profiles { get; set; }
        [ForeignKey("ProfileSexWhoId")]
        public ICollection<Profile> ProfilesWho { get; set; }
    }
}
