using System.Collections.Generic;
using Business.DataAccess.Public.Directory;
using Business.DataAccess.Public.Directory.DirectoryItems;
using Business.DataAccess.Public.Repository;

namespace Business.DataAccess.Private.Directory
{
    public sealed class ActivityDirectory : Directory<ActivityItem>, IActivityDirectory
    {
        private readonly IDirectoryRepository _repository;
        public ActivityDirectory(IDirectoryRepository repository)
        {
            _repository = repository;
        }

        protected override IEnumerable<ActivityItem> GetData()
        {
            return _repository.GetActivity();
        }
    }
}
