using System.Collections.Generic;
using Business.DataAccess.Public.Directory.DirectoryItems;

namespace Business.DataAccess.Public.Repository
{
    public interface IDirectoryRepository
    {
        ICollection<SexItem> GetSex();
        ICollection<AlcoholItem> GetAlcohol();
        ICollection<SmokeItem> GetSmoke();
        ICollection<AnimalItem> GetAnimal();
        ICollection<InteresItem> GetInteres();
        ICollection<ActivityItem> GetActivity();
    }
}
