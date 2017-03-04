using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;

namespace Business.DataAccess.Public.Entities
{
    public class Apartment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ApartmentId { get; set; }
        [ForeignKey("Profile")]
        public long? ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
        [Display(Name="Страна")]
        public string Country { get; set; }
        [Required]
        [Display(Name = "Город")]//+
        public string Town { get; set; }
        [Required]
        [Display(Name = "Адрес")]//+
        public string Adress { get; set; }
        [Display(Name = "Квартира")]//+
        public int? Flat { get; set; }
        [Display(Name = "Этаж")]//+
        public int? Floor { get; set; }
        [Display(Name = "Этажность")]//+
        public int? FloorTotal { get; set; }
        [Display(Name = "Общая площадь")]//+
        public double? AreaTotal { get; set; }
        [Display(Name = "Жилая площадь")]//+
        public double? AreaLiving { get; set; }
        [Display(Name = "Кухня")]//+
        public double? AreaKitchen { get; set; }
        [Display(Name = "Описание")]//+
        public string Description { get; set; }

        [Display(Name = "Широта")]//+
        public double? Latitude { get; set; }
        [Display(Name = "Долгота")]//+
        public double? Longitude { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? RemoveDate { get; set; }

        public bool? Published { get; set; }


        [ForeignKey("Type")]
        public int? ApartmentTypeId { get; set; }
        [ForeignKey("ApartmentTypeId")]
        public ApartmentType Type { get; set; }

        public ICollection<ApartmentOption> ApartmentOptions { get; set; }
        [ForeignKey("ApartmentId")]
        public ICollection<ApartmentPhoto> ApartmentPhotos { get; set; }

        [ForeignKey("DefaultPhoto")]
        public long? DefaultPhotoId { get; set; }
        [ForeignKey("ApartmentPhotoId")]
        public ApartmentPhoto DefaultPhoto { get; set; }

        [ForeignKey("ApartmentId")]
        public ICollection<ApartmentVisitors> ApartmentVisitors { get; set; }
        [ForeignKey("ApartmentId")]
        public ICollection<ProfileAction> ProfileActions { get; set; }
        [ForeignKey("ApartmentId")]
        public ICollection<ApartmentComment> ApartmentComments { get; set; }
        public string GetDefaultPhotoLink
        {
            get
            {
                return DefaultPhoto != null &&
                    DefaultPhoto.Links != null &&
                     DefaultPhoto.Links.Where(m => m.TypeId == 2).FirstOrDefault() != null &&
                    File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(DefaultPhoto.Links.Where(m => m.TypeId == 2).FirstOrDefault().Link)) ? DefaultPhoto.Links.Where(m => m.TypeId == 2).FirstOrDefault().Link : Core.Img.Core.GetStandartApartmentPhoto(Core.Img.Core.StandartApartmentPhoto.Big);
            }
        }
        public List<string> PhotoList
        {
            get
            {
                List<string> result = new List<string>();
                result = ApartmentPhotos.SelectMany(m => m.Links.Where(k => k.TypeId == 2).Select(k => k.Link)).ToList();
                if (result.Count == 0)
                {
                    result.Add(GetDefaultPhotoLink);
                }
                return result;
           }
        }
        public string GetAdress
        {
            get
            {
                return Town + ", " + Adress;
            }
        }
        public string GetDateString
        {
            get
            {
                return Core.Core.GetDateString(UpdateDate);
            }
        }
    }
}
