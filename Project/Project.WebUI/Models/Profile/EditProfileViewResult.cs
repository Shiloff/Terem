using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Business.DataAccess.Public.Directory.DirectoryItems;
using Business.DataAccess.Public.Entities;

namespace Project.WebUI.Models
{
    public class ProfileEditResult
    {
        public Profile Profile { get; set; }
        [Display(Name="Выберите пол")]
        public IReadOnlyCollection<SexItem> ProfileSex { get; set; }
        public IReadOnlyCollection<AlcoholItem> ProfileAlcohol { get; set; }
        public IReadOnlyCollection<SmokeItem> ProfileSmoking { get; set; }
        public IReadOnlyCollection<AnimalItem> ProfileAnimals { get; set; }
        public IReadOnlyCollection<InteresItem> Interests { get; set; }
        [Display(Name = "Деятельность")]
        public IReadOnlyCollection<ActivityItem> ProfileActivity { get; set; }
        [Display(Name = "Город")]
        public IReadOnlyCollection<CityItem> Cities { get; set; }
        public int[] SelectedInterests { get; set; }
        public HttpPostedFileBase inputImage { get; set; } 
    }
    public class Interset
    {
        public int id { get; set; }
        public string Name { get; set; }
    }
}