using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Web.Mvc;
namespace Business.DataAccess.Public.Entities
{
    public class Profile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProfileId { get; set; }
        public bool? New { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "День рождения")]
        public DateTime? Birfday { get; set; }
        [MaxLength(8000)]
        [AllowHtml]
        public string AboutMe { get; set; }
        [Display(Name = "Контактный телефон")]
        public string ContactPhone { get; set; }

        [ForeignKey("Sex")]
        public int? ProfileSexId { get; set; }
        [ForeignKey("ProfileSexId")]
        [Display(Name = "Пол")]
        public ProfileSex Sex { get; set; }
        [ForeignKey("Activity")]
        public int? ProfileActivityId { get; set; }
        [ForeignKey("ProfileActivityId")]
        [Display(Name = "Деятельность")]
        public ProfileActivity Activity { get; set; }
        [ForeignKey("Smoking")]
        public int? ProfileSmokingId { get; set; }
        [ForeignKey("ProfileSmokingId")]
        [Display(Name = "Курение")]
        public ProfileSmoking Smoking { get; set; }
        [ForeignKey("Alcohol")]
        public int? ProfileAlcoholId { get; set; }
        [ForeignKey("ProfileAlcoholId")]
        [Display(Name = "Алкоголь")]
        public ProfileAlcohol Alcohol { get; set; }
        [ForeignKey("Animals")]
        public int? ProfileAnimalsId { get; set; }
        [ForeignKey("ProfileAnimalsId")]
        [Display(Name = "Животные")]
        public ProfileAnimals Animals { get; set; }
        [ForeignKey("SexWho")]
        public int? ProfileSexWhoId { get; set; }
        [ForeignKey("ProfileSexId")]
        [Display(Name = "Пол соседа")]
        public ProfileSex SexWho { get; set; }

        public ICollection<ProfileInteres> Intereses { get; set; }
        [ForeignKey("ProfileId")]
        public ICollection<Apartment> Apartments { get; set; }

        [ForeignKey("ProfileIdFrom")]
        public ICollection<Message> MyMessage { get; set; }
        [ForeignKey("ProfileIdTo")]
        public ICollection<Message> MessageForMe { get; set; }
        [ForeignKey("ProfileId")]
        public ICollection<ApartmentVisitors> Visits { get; set; }

        [ForeignKey("ProfileId")]
        public ICollection<ProfileAction> Actions { get; set; }
        [ForeignKey("ProfileWhoId")]
        public ICollection<ProfileAction> ActionsWho { get; set; }
        [ForeignKey("ProfileId")]
        public ICollection<ProfileActionComment> ProfileActionComments { get; set; }
        [ForeignKey("ProfileId")]
        public ICollection<ApartmentComment> ApartmentComments { get; set; }
        [ForeignKey("ProfileId")]
        public ICollection<ApartmentCommentLike> ApartmentCommentsLikes { get; set; }

        [ForeignKey("State")]
        public int? StateId { get; set; }
        [ForeignKey("StateId")]
        public RegionState State { get; set; }

        [ForeignKey("Country")]
        public int? CountryId { get; set; }
        [ForeignKey("CountryId")]
        public RegionCountry Country { get; set; }

        [ForeignKey("City")]
        public int? CityId { get; set; }
        [ForeignKey("CityId")]
        public RegionCity City { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public string ImageLink { get; set; }
        public string GetImageLink
        {
            get
            {
                return ImageLink != null && File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(ImageLink)) ? ImageLink : Core.Img.Core.GetStandartAvatar(Core.Img.Core.StandartAvatar.Full);
            }
        }
        
        public string ImageType { get; set; }

        public string ImageAvatarLink { get; set; }
        public string GetImageAvatarLink
        {
            get
            {
                return ImageAvatarLink != null && File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(ImageAvatarLink)) ? ImageAvatarLink : Core.Img.Core.GetStandartAvatar(Core.Img.Core.StandartAvatar.Small);
            }
        }
        public string ImageAvatarType { get; set; }
        public string ImageAvatarBigLink { get; set; }
        public string GetImageAvatarBigLink
        {
            get
            {
                return ImageAvatarBigLink != null && File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(ImageAvatarBigLink)) ? ImageAvatarBigLink : Core.Img.Core.GetStandartAvatar(Core.Img.Core.StandartAvatar.Big);
            }
        }
        public string ImageAvatarBigType { get; set; }

        #region Likes
        [ForeignKey("ProfileId")]
        public ICollection<ProfileActionLike> ProfileActionsLikes { get; set; }
        [ForeignKey("ProfileId")]
        public ICollection<ProfileActionCommentLike> ProfileActionsCommentsLikes { get; set; }
        #endregion
    }
}
