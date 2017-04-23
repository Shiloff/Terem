using System.Collections.Generic;
using Business.DataAccess.Private.DatabaseContext;
using Business.DataAccess.Public.Entities;

namespace Project.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed partial class Configuration : DbMigrationsConfiguration<EFDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Project.Domain.Concrete.EFDBContext";
        }

        protected override void Seed(EFDBContext context)
        {
            context.ApartmentOptions.AddOrUpdate(
                m => m.ApartmentOptionId,
                new ApartmentOption() {ApartmentOptionId = 1, Name = "Мебель"},
                new ApartmentOption() {ApartmentOptionId = 2, Name = "Телевизор"},
                new ApartmentOption() {ApartmentOptionId = 3, Name = "Балкон"},
                new ApartmentOption() {ApartmentOptionId = 4, Name = "Кухонная мебель"},
                new ApartmentOption() {ApartmentOptionId = 5, Name = "Холодильник"},
                new ApartmentOption() {ApartmentOptionId = 6, Name = "Можно с животными"},
                new ApartmentOption() {ApartmentOptionId = 7, Name = "Телефон"},
                new ApartmentOption() {ApartmentOptionId = 8, Name = "Стиральная машина"},
                new ApartmentOption() {ApartmentOptionId = 9, Name = "Можно с детьми"});
            context.ApartmentTypes.AddOrUpdate(
                m => m.ApartmentTypeId,
                new ApartmentType() {ApartmentTypeId = 1, Name = "1 комнатная"},
                new ApartmentType() {ApartmentTypeId = 2, Name = "2 комнатная"},
                new ApartmentType() {ApartmentTypeId = 3, Name = "3 комнатная"},
                new ApartmentType() {ApartmentTypeId = 4, Name = "4+ комнатная"});
            context.ProfileActivity.AddOrUpdate(
                m => m.ProfileActivityId,
                new ProfileActivity() {ProfileActivityId = 1, Name = "Не выбрано"},
                new ProfileActivity() {ProfileActivityId = 2, Name = "Студент"});
            context.ProfileAlcohol.AddOrUpdate(
                m => m.ProfileAlcoholId,
                new ProfileAlcohol()
                {
                    ProfileAlcoholId = 1,
                    Name = "Нет",
                    Icon = "<i class=\"fa fa-times fa-lg\" style=\"color:#ec4758\"></i>"
                },
                new ProfileAlcohol()
                {
                    ProfileAlcoholId = 2,
                    Name = "Да",
                    Icon = "<i class=\"fa fa-check fa-lg\" style=\"color:#1ab394\"></i>"
                },
                new ProfileAlcohol()
                {
                    ProfileAlcoholId = 3,
                    Name = "Не важно",
                    Icon = "<i class=\"fa fa-check fa-lg\"></i>"
                });
            context.ProfileAnimals.AddOrUpdate(m => m.ProfileAnimalsId,
                new ProfileAnimals()
                {
                    ProfileAnimalsId = 1,
                    Name = "Нет",
                    Icon = "<i class=\"fa fa-times fa-lg\" style=\"color:#ec4758\"></i>"
                },
                new ProfileAnimals()
                {
                    ProfileAnimalsId = 2,
                    Name = "Да",
                    Icon = "<i class=\"fa fa-check fa-lg\" style=\"color:#1ab394\"></i>"
                },
                new ProfileAnimals()
                {
                    ProfileAnimalsId = 3,
                    Name = "Не важно",
                    Icon = "<i class=\"fa fa-check fa-lg\"></i>"
                });
            context.ProfileInteres.AddOrUpdate(m => m.ProfileInteresId,
                new ProfileInteres() {ProfileInteresId = 1, Name = "Женщины"},
                new ProfileInteres() {ProfileInteresId = 2, Name = "Пиво"},
                new ProfileInteres() {ProfileInteresId = 3, Name = "Хрень"},
                new ProfileInteres() {ProfileInteresId = 4, Name = "Секс, наркотики, рок-н-ролл"});
            context.ProfileSex.AddOrUpdate(m => m.ProfileSexId,
                new ProfileSex()
                {
                    ProfileSexId = 1,
                    Name = "Мужской",
                    Icon = "<i class=\"fa fa-mars fa-lg\" style=\"color:#1ab394\"></i>"
                },
                new ProfileSex()
                {
                    ProfileSexId = 2,
                    Name = "Женский",
                    Icon = "<i class=\"fa fa-venus fa-lg\" style=\"color:#ec4758\"></i>"
                },
                new ProfileSex() {ProfileSexId = 3, Name = "Не имеет значения", Icon = ""});
            context.ProfileSmoking.AddOrUpdate(m => m.ProfileSmokingId,
                new ProfileSmoking()
                {
                    ProfileSmokingId = 1,
                    Name = "Нет",
                    Icon = "<i class=\"fa fa-times fa-lg\" style=\"color:#ec4758\"></i>"
                },
                new ProfileSmoking()
                {
                    ProfileSmokingId = 2,
                    Name = "Да",
                    Icon = "<i class=\"fa fa-check fa-lg\" style=\"color:#1ab394\"></i>"
                },
                new ProfileSmoking()
                {
                    ProfileSmokingId = 3,
                    Name = "Не важно",
                    Icon = "<i class=\"fa fa-check fa-lg\"></i>"
                });
            context.Profiles.AddOrUpdate(m => m.ProfileId,
                new Profile()
                {
                    ProfileId = 1,
                    New = false,
                    UserId = "a9b13910-e866-416a-b17c-422c3b6ba8b4",
                    FirstName = "Александр",
                    LastName = "Шилов",
                    CityId = 1,
                    Birfday = new DateTime(1987, 10, 3),
                    ProfileSexId = 1,
                    ProfileActivityId = 1,
                    ProfileSmokingId = 1,
                    ProfileAlcoholId = 1,
                    ProfileAnimalsId = 2,
                    ProfileSexWhoId = 3,
                    Actions =
                        new List<ProfileAction>()
                        {
                            new ProfileAction()
                            {
                                ProfileActionId = 1,
                                ProfileActionTypeId = 2,
                                ProfileId = 1,
                                ProfileWhoId = 1,
                                Text = "Зарегистрировался!",
                                Date = new DateTime(2016, 1, 1)
                            }
                        }
                });

            //SeedRegions(context);
            SeedCities(context);
            SeedAdmin(context);
        }
    }
}
