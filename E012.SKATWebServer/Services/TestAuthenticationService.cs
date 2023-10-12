using A120.Common.Services;
using A120.Common.Types;
using A120.UserAuthentication.Services;
using E012.SKAT.WebServer.Infrastructure;
using System;
using System.Configuration;
using System.Web.Security;

namespace E012.SKAT.WebServer.Services
{
    /// <summary>
    /// Тестовый класс аутентификации 
    /// </summary>
    public class TestAuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// получить информацию о пользователе по логину и паролю
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IUserInfo Login(string login, string password)
        {
            IUserInfo userInfo = null;
            string serverSecurityURI, taskCode;

            try
            {
                // Если в логин передали зашифрованную информацию о пользователе
                // расшифруем и получим логин и пароль
                if (login.Length > 9)
                {
                    var ticket = FormsAuthentication.Decrypt(login);
                   
                    if (ticket != null)
                    {
                        login = ticket.Name;
                        password = ticket.UserData;
                    }
                }

                //из настроек получим адрес сервера безопасности и код задачи
                TaskConfig taskConfig = (TaskConfig)ConfigurationManager.GetSection("taskConfig");

                serverSecurityURI = taskConfig.ServerSecurityUri;
                taskCode = taskConfig.TaskCode;

                AuthenticationService authService = new AuthenticationService(serverSecurityURI, taskCode);

                userInfo = authService.Login(login, password);

                if (userInfo == null)
                    throw new NullReferenceException("Информация о пользователе не определена");
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }

            return new UserInfo(userInfo.Login, userInfo.Name, userInfo.Roles, "", userInfo.SessionID, userInfo.SessionKey);
        }

        /// <summary>
        /// метод получения информации о пользователе по логину и идентификатору и ключу сессии
        /// </summary>
        /// <param name="login"></param>
        /// <param name="sessionId"></param>
        /// <param name="sessionKey"></param>
        /// <returns></returns>
        public IUserInfo LoginBySession(string login, string sessionId, string sessionKey)
        {
            IUserInfo userInfo = null;
            string serverSecurityURI, taskCode;

            try
            {
                // Если в логин передали зашифрованную информацию о пользователе
                if (login.Length > 9)
                {
                    var ticket = FormsAuthentication.Decrypt(login);

                    if (ticket != null)
                    {
                        login = ticket.Name;

                    }
                }

                TaskConfig taskConfig = (TaskConfig)ConfigurationManager.GetSection("taskConfig");

                serverSecurityURI = taskConfig.ServerSecurityUri;
                taskCode = taskConfig.TaskCode;

                AuthenticationService authService = new AuthenticationService(serverSecurityURI, taskCode);

                userInfo = authService.LoginBySession(login, sessionId, sessionKey);

                if (userInfo == null)
                    throw new NullReferenceException("Информация о пользователе не определена");
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
            
            return new UserInfo(userInfo.Login, userInfo.Name, userInfo.Roles, "", userInfo.SessionID, userInfo.SessionKey);
        }
    }
}