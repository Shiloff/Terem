using System.Collections.Specialized;
using System.Configuration;

namespace Business.DataAccess.Public.Core.Img
{
    public static partial class Core
    {
        public enum StandartAvatar
        {
            Full,
            Small,
            Big
        }
        public static string GetStandartAvatar(StandartAvatar type)
        {
            var defaultAvatars = ((NameValueCollection)ConfigurationManager
                      .GetSection("defaultAvatar"));
            switch (type)
            {
                case StandartAvatar.Full: return defaultAvatars["Full"];
                case StandartAvatar.Small: return defaultAvatars["Small"];
                case StandartAvatar.Big: return defaultAvatars["Big"];
                default: return defaultAvatars["Full"];
            }
        }
    }
}
