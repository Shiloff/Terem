﻿using Business.DataAccess.Public.Directory;

namespace Business.DataAccess.Private.Directory
{
    public sealed class DirectoryStorage : IDirectoryStorage
    {
        public DirectoryStorage(
            ISexDirectory sexDirectory,
            IAlcoholDirectory alcohol,
            ISmokeDirectory smoke,
            IAnimalDirectory animal,
            IInteresDirectory interes,
            IActivityDirectory activity, 
            ICityDirectory city)
        {
            Sex = sexDirectory;
            Alcohol = alcohol;
            Smoke = smoke;
            Animal = animal;
            Interes = interes;
            Activity = activity;
            City = city;
        }

        public ISexDirectory Sex { get; }
        public IAlcoholDirectory Alcohol { get; }
        public ISmokeDirectory Smoke { get; }
        public IAnimalDirectory Animal { get; }
        public IInteresDirectory Interes { get; }
        public IActivityDirectory Activity { get; }

        public ICityDirectory City { get; }
    }
}