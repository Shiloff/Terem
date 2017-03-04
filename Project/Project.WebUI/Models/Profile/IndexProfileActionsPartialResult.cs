
using System.Collections.Generic;
using Business.DataAccess.Public.Entities;

namespace Project.WebUI.Models
{
    public class IndexProfileActionsPartialResult
    {
        public List<ProfileAction> Actions { get; set; }
        public Profile Profile { get; set; }
        public int ActionsCount { get; set; }
        public int CommentsCount { get; set; }
    }
}