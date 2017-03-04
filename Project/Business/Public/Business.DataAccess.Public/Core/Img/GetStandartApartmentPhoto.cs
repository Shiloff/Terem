using System.Collections.Specialized;
using System.Configuration;

namespace Business.DataAccess.Public.Core.Img
{
    public static partial class Core
    {
        public enum StandartApartmentPhoto
        {
            Full,
            Small,
            Big
        }
        public static string GetStandartApartmentPhoto(StandartApartmentPhoto type)
        {
            var defaultAvatars = ((NameValueCollection)ConfigurationManager
                      .GetSection("defaultApartmentPhoto"));
            switch (type)
            {
                case StandartApartmentPhoto.Full: return defaultAvatars["Full"];
                case StandartApartmentPhoto.Small: return defaultAvatars["Small"];
                case StandartApartmentPhoto.Big: return defaultAvatars["Big"];
                default: return defaultAvatars["Full"];
            }
        }
    }
}
