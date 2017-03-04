using System.Collections.Generic;
using Business.DataAccess.Public.Entities;

namespace Project.WebUI.Models
{
    public class DialogMessagesViewResult
    {
        public ICollection<Message> Dialog { get; set; }
        public long MyProfileId { get; set; }
        public long ToProfileId { get; set; }
    }
}