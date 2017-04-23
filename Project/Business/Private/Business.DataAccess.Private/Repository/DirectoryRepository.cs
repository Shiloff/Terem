using System.Collections.Generic;
using System.Linq;
using Business.DataAccess.Private.DatabaseContext;
using Business.DataAccess.Public.Directory.DirectoryItems;
using Business.DataAccess.Public.Repository;

namespace Business.DataAccess.Private.Repository
{
    public sealed class DirectoryRepository : IDirectoryRepository
    {
        public ICollection<InteresItem> GetInteres()
        {
            using (var ctx = new EFDBContext())
            {
                return ctx.ProfileInteres
                    .Select(m => new InteresItem
                    {
                        Id = m.ProfileInteresId,
                        Name = m.Name,
                        Icon = m.Icon
                    }).ToList();
            }
        }

        public ICollection<ActivityItem> GetActivity()
        {
            using (var ctx = new EFDBContext())
            {
                return ctx.ProfileActivity
                    .Select(m => new ActivityItem
                    {
                        Id = m.ProfileActivityId,
                        Name = m.Name,
                        Icon = m.Icon
                    }).ToList();
            }
        }

        public ICollection<CityItem> GetCities()
        {
            using (var ctx = new EFDBContext())
            {
                return ctx.Cities
                    .Select(m => new CityItem
                    {
                        Id = m.Id,
                        Name = m.Value,
                        Sort = m.Sort
                    }).ToList();
            }
        }

        public ICollection<SexItem> GetSex()
        {
            using (var ctx = new EFDBContext())
            {
                return ctx.ProfileSex
                    .Select(m => new SexItem
                    {
                        Id = m.ProfileSexId,
                        Name = m.Name,
                        Icon = m.Icon
                    }).ToList();
            }
        }

        public ICollection<AlcoholItem> GetAlcohol()
        {
            using (var ctx = new EFDBContext())
            {
                return ctx.ProfileAlcohol
                    .Select(m => new AlcoholItem
                    {
                        Id = m.ProfileAlcoholId,
                        Name = m.Name,
                        Icon = m.Icon
                    }).ToList();
            }
        }

        public ICollection<SmokeItem> GetSmoke()
        {
            using (var ctx = new EFDBContext())
            {
                return ctx.ProfileSmoking
                    .Select(m => new SmokeItem
                    {
                        Id = m.ProfileSmokingId,
                        Name = m.Name,
                        Icon = m.Icon
                    }).ToList();
            }
        }

        public ICollection<AnimalItem> GetAnimal()
        {
            using (var ctx = new EFDBContext())
            {
                return ctx.ProfileAnimals
                    .Select(m => new AnimalItem
                    {
                        Id = m.ProfileAnimalsId,
                        Name = m.Name,
                        Icon = m.Icon
                    }).ToList();
            }
        }
    }
}