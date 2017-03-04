using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Business.DataAccess.Public.Entities
{
    public class ProfileAnimals
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfileAnimalsId { get; set; }
        [AllowHtml]
        public string Icon { get; set; }
        public string Name { get; set; }
        [ForeignKey("ProfileAnimalsId")]
        public ICollection<Profile> Profiles { get; set; }
    }
}
