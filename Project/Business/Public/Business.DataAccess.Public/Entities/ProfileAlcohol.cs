using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Business.DataAccess.Public.Entities
{
    public class ProfileAlcohol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfileAlcoholId { get; set; }
        [AllowHtml]
        public string Icon { get; set; }
        public string Name { get; set; }
        [ForeignKey("ProfileAlcoholId")]
        public ICollection<Profile> Profiles { get; set; }
    }
}
