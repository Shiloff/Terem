using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Business.DataAccess.Public.Entities;

namespace Project.WebUI.Models
{
    public class ProfileNewProfileViewResult
    {
        public Profile Profile { get; set; }
        [Display(Name="Выберите пол")]
        public ICollection<ProfileSex> ProfileSex { get; set; }
    }
}