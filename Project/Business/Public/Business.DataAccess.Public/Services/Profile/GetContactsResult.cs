using System.Collections.Generic;

namespace Business.DataAccess.Public.Services.Profile
{
    public class GetContactsResult
    {
        public List<Entities.Profile> Profiles { get; set; }

        public int Count { get; set; }
    }
}