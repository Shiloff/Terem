using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DataAccess.Private.DatabaseContext;
using Business.DataAccess.Public.Entities;

namespace Project.Domain.Migrations
{
    internal sealed partial class Configuration
    {
        private void SeedCities(EFDBContext context)
        {
            context.Cities.AddOrUpdate(m => m.Value,
                new RegionCity() {Id = 1, Value = "Москва", Sort = 1},
                new RegionCity() { Id = 2, Value = "Санкт-Петербург", Sort = 2 },
                new RegionCity() { Id = 3, Value = "Чебоксары", Sort = 3 });
        }
    }
}
