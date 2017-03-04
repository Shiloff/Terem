using System.Collections.Generic;
using Business.DataAccess.Public.Directory;
using Business.DataAccess.Public.Directory.DirectoryItems;
using Business.DataAccess.Public.Repository;

namespace Business.DataAccess.Private.Directory
{
    public sealed class SexDirectory : Directory<SexItem>, ISexDirectory
    {
        private readonly IDirectoryRepository _repository;
        public SexDirectory(IDirectoryRepository repository)
        {
            _repository = repository;
        }

        protected override IEnumerable<SexItem> GetData()
        {
            return _repository.GetSex();
        }
    }
}
