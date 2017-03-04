using System.Collections.Generic;
using Business.DataAccess.Public.Directory;
using Business.DataAccess.Public.Directory.DirectoryItems;
using Business.DataAccess.Public.Repository;

namespace Business.DataAccess.Private.Directory
{
    public sealed class AlcoholDirectory : Directory<AlcoholItem>, IAlcoholDirectory
    {
        private readonly IDirectoryRepository _repository;

        public AlcoholDirectory(IDirectoryRepository repository)
        {
            _repository = repository;
        }

        protected override IEnumerable<AlcoholItem> GetData()
        {
            return _repository.GetAlcohol();
        }
    }
}
