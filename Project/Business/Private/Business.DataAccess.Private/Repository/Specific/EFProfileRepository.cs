using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Business.DataAccess.Private.DatabaseContext;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository.Specific;
using DataAccess.Public.Repository;

namespace Business.DataAccess.Private.Repository.Specific
{
    public class EFProfileRepository : IProfileRepository
    {
        EFDBContext context = new EFDBContext();

        public IQueryable<Profile> Profiles
        {
            get
            {
                return context.Profiles
                    .Include(m => m.Sex)
                    .Include(m => m.SexWho)
                    .Include(m => m.Alcohol)
                    .Include(m => m.Smoking)
                    .Include(m => m.Animals)
                    .Include(m => m.Intereses)
                    .Include(m => m.Activity);
            }
        }

        public Profile GetProfile(long? profileId)
        {
            return context.Profiles.Where(m => m.ProfileId == profileId)
                .Include(m => m.Sex)
                .Include(m => m.SexWho)
                .Include(m => m.Alcohol)
                .Include(m => m.Smoking)
                .Include(m => m.Animals)
                .Include(m => m.Intereses)
                .Include(m => m.Activity)
                .FirstOrDefault();
        }

       public bool IsProfileExists(long? profileId)
        {
            bool result = false;
            if (context.Profiles.Find(profileId) != null)
            {
                result = true;
            }
            return result;
        }
    }
}
