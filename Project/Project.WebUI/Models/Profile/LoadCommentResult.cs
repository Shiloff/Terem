using Business.DataAccess.Public.Entities;

namespace Project.WebUI.Models
{
    public class LoadCommentResult
    {
        public ProfileActionComment ProfileActionComment { get; set; }
        public Profile Profile { get; set; }
        public ProfileAction Action { get; set; }
    }
}