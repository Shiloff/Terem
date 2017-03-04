using System.Collections.Generic;
using Business.DataAccess.Public.Directory;
using Business.DataAccess.Public.Directory.DirectoryItems;
using Business.DataAccess.Public.Repository;

namespace Business.DataAccess.Private.Directory
{
    public sealed class InteresDirectory : Directory<InteresItem>, IInteresDirectory
    {
        private readonly IDirectoryRepository _repository;
        public InteresDirectory(IDirectoryRepository repository)
        {
            _repository = repository;
        }

        protected override IEnumerable<InteresItem> GetData()
        {
            return _repository.GetInteres();
        }
    }
}
