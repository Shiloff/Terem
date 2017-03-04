using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.DataAccess.Public.Entities
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MessageId { get; set; }

        [ForeignKey("ProfileFrom")]
        public long? ProfileIdFrom { get; set; }
        [ForeignKey("MyMessage")]
        public Profile ProfileFrom { get; set; }
        [ForeignKey("ProfileTo")]
        public long? ProfileIdTo { get; set; }
        [ForeignKey("MessageForMe")]
        public Profile ProfileTo { get; set; }

        public DateTime? DT { get; set; }
        public string MessageText { get; set; }

        public bool? Read { get; set; }
    }
}
