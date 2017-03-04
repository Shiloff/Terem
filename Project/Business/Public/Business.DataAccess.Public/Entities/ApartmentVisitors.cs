using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.DataAccess.Public.Entities
{
    public class ApartmentVisitors
    {        
       
        [Key, Column(Order = 0)]
        [ForeignKey("Profile")]
        public long ProfileId { get; set; }
        [ForeignKey("Profile")]
        public Profile Profile { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("Apartment")]
        public long ApartmentId { get; set; }
        [ForeignKey("ApartmentId")]
        public Apartment Apartment { get; set; }
        public DateTime? LastDate { get; set; }
        public int? Count { get; set; }
    }
}
