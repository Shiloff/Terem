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
        public List<Profile> GetContacts(long profileId, int count = 0)
        {
            var query = this.Profiles
               .Where(m => m.New == false)
               .Where(m => m.ProfileId != profileId)
               .Where(m => m.MyMessage.Where(k => k.ProfileIdTo == profileId).Count() > 0
                   || m.MessageForMe.Where(k => k.ProfileIdFrom == profileId).Count() > 0)
               .OrderBy(m => m.LastName)
               .ThenBy(m => m.ProfileId)
               .AsQueryable();

            if (count > 0)
                query = query.Take(count);
            var result = query
                .ToList();
            return result;
        }

        private IQueryable<Profile> FindProfilesQuery(Profile profile, FindProfilesParams param)
        {
            return this.Profiles
                .Where(m => m.New == false)
                .Where(m => m.ProfileId != profile.ProfileId)
                .Include(m=>m.Intereses)
                .OrderByDescending(m => m.LastName)
                .AsQueryable();
        }
        public List<Profile> FindProfiles(Profile profile, FindProfilesParams param)
        {
            List<Profile> result;
            var query = FindProfilesQuery(profile, param);
            if (param.Take != 0)
            {
                query = query.Skip(param.Skip).Take(param.Take);
            }
            result = query.ToList();
            return result;
        }
        public long GetFindProfilesCount(Profile profile, FindProfilesParams param)
        {
            return FindProfilesQuery(profile, param).Count();
        }


    }
}
