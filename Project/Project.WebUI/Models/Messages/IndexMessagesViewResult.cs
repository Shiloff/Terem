using System;
using System.Collections.Generic;
using Business.DataAccess.Public.Entities;

namespace Project.WebUI.Models
{
    public class IndexMessagesViewResult
    {
        public ICollection<DialogInfo> Dialogs { get; set; }
        public Profile Profile { get;set; }
        public long openedChat { get; set; }
        
    }
     public class DialogInfo
     {
         public Profile Profile { get; set; }
         public DateTime? LastUpdate { get; set; }
         public string LastMessage { get; set; }
         public long? NewMessages { get; set; }
     }
}