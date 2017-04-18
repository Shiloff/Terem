using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Business.DataAccess.Private.DatabaseContext;
using Business.DataAccess.Private.DatabaseContext.Factory;
using Business.DataAccess.Private.Directory;
using Business.DataAccess.Private.Repository;
using Business.DataAccess.Private.Repository.Specific;
using Business.DataAccess.Private.Repository.Specific.Helpers;
using Business.DataAccess.Private.Services.Contact;
using Business.DataAccess.Private.Services.Profile;
using Business.DataAccess.Private.UnitOfWork;
using Business.DataAccess.Public.DatabaseContext.Factory;
using Business.DataAccess.Public.Directory;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Repository;
using Business.DataAccess.Public.Repository.Specific;
using Business.DataAccess.Public.Repository.Specific.ProfileHelpers;
using Business.DataAccess.Public.Services.Contact;
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
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IApartmentRepository>().To<EFApartmentsRepository>();

            _kernel.Bind<IProfileRepository>().To<EFProfileRepository>();
            _kernel.Bind<IMessageRepository>().To<EFMessageRepository>();

            _kernel.Bind<IChatRepository>().To<EFChatRepository>();

            _kernel.Bind<IProfileContactsHelper>().To<EFProfileContacts>();

            _kernel.Bind<IApplicationManager>().To<OwinApplicationManager>();
            _kernel.Bind<IDbContextFactory<EFDBContext>>().To<EfDbContextFactory>();
            _kernel.Bind<IUnitOfWorkFactory>().To<UnitOfWorkFactory>();

            //Services
            _kernel.Bind<IProfileService>().To<ProfileService>();
            _kernel.Bind<IContactService>().To<ContactService>();

            //Directory
            _kernel.Bind<IDirectoryRepository>().To<DirectoryRepository>();
            _kernel.Bind<IDirectoryStorage>().To<DirectoryStorage>();
            _kernel.Bind<ISexDirectory>().To<SexDirectory>().InSingletonScope();
            _kernel.Bind<IAlcoholDirectory>().To<AlcoholDirectory>().InSingletonScope();
            _kernel.Bind<ISmokeDirectory>().To<SmokeDirectory>().InSingletonScope();
            _kernel.Bind<IAnimalDirectory>().To<AnimalDirectory>().InSingletonScope();
            _kernel.Bind<IInteresDirectory>().To<InteresDirectory>().InSingletonScope();
            _kernel.Bind<IActivityDirectory>().To<ActivityDirectory>().InSingletonScope();
        }
    }
}