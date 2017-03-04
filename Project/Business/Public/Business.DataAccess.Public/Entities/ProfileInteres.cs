using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.DataAccess.Public.Entities
{
    public class ProfileInteres
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfileInteresId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        [ForeignKey("ParentProfileInteres")]
        public int? ParentProfileInteresId { get; set; }
        [ForeignKey("ProfileInteresId")]
        public ProfileInteres ParentProfileInteres { get; set; }
        [ForeignKey("ParentProfileInteresId")]
        public ICollection<ProfileInteres> ChildProfiles { get; set; }
        public ICollection<Profile> Profiles { get; set; }
    }
}
