﻿using System.Collections.Generic;
using Business.DataAccess.Public.Directory;
using Business.DataAccess.Public.Directory.DirectoryItems;
using Business.DataAccess.Public.Repository;

namespace Business.DataAccess.Private.Directory
{
    public sealed class AnimalDirectory : Directory<AnimalItem>, IAnimalDirectory
    {
        private readonly IDirectoryRepository _repository;
        public AnimalDirectory(IDirectoryRepository repository)
        {
            _repository = repository;
        }

        protected override IEnumerable<AnimalItem> GetData()
        {
            return _repository.GetAnimal();
        }
    }
}
