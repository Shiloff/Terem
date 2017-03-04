using System.Collections.Generic;
using Business.DataAccess.Public.Directory;
using Business.DataAccess.Public.Directory.DirectoryItems;
using Business.DataAccess.Public.Repository;

namespace Business.DataAccess.Private.Directory
{
    public sealed class SmokeDirectory : Directory<SmokeItem>, ISmokeDirectory
    {
        private readonly IDirectoryRepository _repository;
        public SmokeDirectory(IDirectoryRepository repository)
        {
            _repository = repository;
        }

        protected override IEnumerable<SmokeItem> GetData()
        {
            return _repository.GetSmoke();
        }
    }
}
