using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.DataAccess.Public.Entities
{
    public class ProfileActivity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfileActivityId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        [ForeignKey("ProfileActivityId")]
        public ICollection<Profile>Profiles { get; set; }
      
    }
}
