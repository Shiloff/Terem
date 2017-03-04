using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Business.DataAccess.Private.DatabaseContext;
using Business.DataAccess.Private.DatabaseContext.Factory;
using Business.DataAccess.Private.Directory;
using Business.DataAccess.Private.Repository;
using Business.DataAccess.Private.Repository.Specific;
using Business.DataAccess.Private.Repository.Specific.Helpers;
using Business.DataAccess.Private.Services.Profile;
using Business.DataAccess.Private.UnitOfWork;
using Business.DataAccess.Public.DatabaseContext.Factory;
using Business.DataAccess.Public.Directory;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository;
using Business.DataAccess.Public.Repository.Specific;
using Business.DataAccess.Public.Repository.Specific.ProfileHelpers;
using Business.DataAccess.Public.Services.Profile;
using Business.DataAccess.Public.UnitOfWork.Factory;
using DataAccess.Private.Repository;
using DataAccess.Public.Repository;
using Ninject;
using Project.WebUI.Infrastructure.ApplicationUser;
using IProfileRepository = Business.DataAccess.Public.Repository.Specific.IProfileRepository;

namespace Project.WebUI.Infrastructure.Ninject
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IApartmentRepository>().To<EFApartmentsRepository>();

            kernel.Bind<IProfileRepository>().To<EFProfileRepository>();
            kernel.Bind<IMessageRepository>().To<EFMessageRepository>();

            kernel.Bind<IChatRepository>().To<EFChatRepository>();

            kernel.Bind<IProfileFinderHelper>().To<EFProfileFinder>();
            kernel.Bind<IProfileContactsHelper>().To<EFProfileContacts>();

            kernel.Bind<IApplicationManager>().To<OwinApplicationManager>();
            kernel.Bind<IDbContextFactory<EFDBContext>>().To<EfDbContextFactory>();
            kernel.Bind<IUnitOfWorkFactory>().To<UnitOfWorkFactory>();

            kernel.Bind<IProfileService>().To<ProfileService>();


            //Directory
            kernel.Bind<IDirectoryRepository>().To<DirectoryRepository>();
            kernel.Bind<IDirectoryStorage>().To<DirectoryStorage>();
            kernel.Bind<ISexDirectory>().To<SexDirectory>().InSingletonScope();
            kernel.Bind<IAlcoholDirectory>().To<AlcoholDirectory>().InSingletonScope();
            kernel.Bind<ISmokeDirectory>().To<SmokeDirectory>().InSingletonScope();
            kernel.Bind<IAnimalDirectory>().To<AnimalDirectory>().InSingletonScope();
            kernel.Bind<IInteresDirectory>().To<InteresDirectory>().InSingletonScope();
            kernel.Bind<IActivityDirectory>().To<ActivityDirectory>().InSingletonScope();
        }
    }
}