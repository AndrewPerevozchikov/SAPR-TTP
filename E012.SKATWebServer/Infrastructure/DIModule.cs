using E012.DomainModel.Services;
using E012.DomainModel.Services.Infrastructure;
using E012.SKAT.WebServer.Services;
using Ninject.Modules;
using Ninject.Extensions.Factory;
using Ninject.Web.Common;

namespace E012.SKAT.WebServer.Infrastructure
{
    public class DIModule : NinjectModule
    {
        public override void Load()
        {
            //логгер пока не подключаю
          //  Bind<A119.Logger.Infrastructure.ILogger>().To<Logger.Logger>();
            Bind<IUserSessionService>().To<UserSessionService>().InRequestScope();
           

            Bind<IInfrastructureFactory>().ToFactory();
        }
    }
}