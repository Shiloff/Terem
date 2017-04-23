using System.Collections.Generic;
using Business.DataAccess.Public.Directory;
using Business.DataAccess.Public.Directory.DirectoryItems;
using Business.DataAccess.Public.Repository;

namespace Business.DataAccess.Private.Directory
{
    public sealed class CityDirectory : Directory<CityItem>, ICityDirectory
    {
        private readonly IDirectoryRepository _repository;
        public CityDirectory(IDirectoryRepository repository)
        {
            _repository = repository;
        }

        protected override IEnumerable<CityItem> GetData()
        {
            return _repository.GetCities();
        }
    }
}
