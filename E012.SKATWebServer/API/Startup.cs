using A120.Common.Services;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Configuration;
using E012.SKAT.WebServer.Infrastructure;
using A120.UserAuthentication.Services;
using E012.SKAT.WebServer.Services;

//тут создается TokenController - к нему из клиента происходит запрос на получение токена
[assembly: OwinStartup(typeof(E012.SKAT.WebServer.API.Startup))]

namespace E012.SKAT.WebServer.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            TaskConfig taskConfig = (TaskConfig)ConfigurationManager.GetSection("taskConfig");
            IAuthenticationService authService = taskConfig.UseServerSecurity
                ? new AuthenticationService(taskConfig.ServerSecurityUri, taskConfig.TaskCode) as IAuthenticationService 
                : new TestAuthenticationService() as IAuthenticationService; 

            //тут заполняется конфигурация
            A120.UserAuthentication.WebAPI.Infrastructure.OwinStartup.Configuration(app, config, authService);
        }
    }
}
