using System.Collections.Generic;
using Business.DataAccess.Public.Entities;

namespace Project.BusinessLogic.Services.ProfileService
{
    public class GetContactsResult
    {
        public List<Profile> Profiles { get; set; }

        public int Count { get; set; }
    }
}