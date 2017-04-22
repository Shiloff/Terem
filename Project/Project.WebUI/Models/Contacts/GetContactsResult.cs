using System.Collections.Generic;
using System.ComponentModel;
using Business.DataAccess.Public.Directory.DirectoryItems;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Services.Contact;

namespace Project.WebUI.Models

{
    public class GetContactsResult
    {
        public List<Profile> Profiles { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public AvalibleFilters AvalibleFilters { get; set; }

        public ContactFilter SelectedFilters { get; set; }
        public string Action { get; set; }
    }

    public class AvalibleFilters
    {
        [DisplayName("Пол")]
        public IReadOnlyCollection<SexItem> Sex { get; set; }
        [DisplayName("Пол соседа")]
        public IReadOnlyCollection<SexItem> SexWho { get; set; }
        [DisplayName("Алкоголь")]
        public IReadOnlyCollection<AlcoholItem> Alcohols { get; set; }
        [DisplayName("Курение")]
        public IReadOnlyCollection<SmokeItem> Smokes { get; set; }
        [DisplayName("Животные")]
        public IReadOnlyCollection<AnimalItem> Animals { get; set; }
        [DisplayName("Интересы")]
        public IReadOnlyCollection<InteresItem> Intereses { get; set; }
        [DisplayName("Деятельность")]
        public IReadOnlyCollection<ActivityItem> Activity { get; set; }
    }

    public class SelectedFilters
    {
        public SelectedFilters(int? sexId = null, int? alcoholId = null, int? animalId = null, int? smokeId = null,
            int? activityId = null)
        {
            ActivityId = activityId;
            AnimalId = animalId;
            SmokeId = smokeId;
            SexId = sexId;
            AlcoholId = alcoholId;
        }

        public int? SexId { get; }
        public int? AlcoholId { get; }
        public int? AnimalId { get; }
        public int? SmokeId { get; }
        public int? ActivityId { get; }
    }

}