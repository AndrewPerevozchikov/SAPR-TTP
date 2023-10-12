using A120.Common.Types;
using E012.DomainModel.Services.Infrastructure;
using System.Collections.Concurrent;

namespace E012.SKAT.WebServer.Services
{
    /// <summary>
    /// сессия пользователя
    /// </summary>
    public class UserSessionService : IUserSessionService
    {
        #region Private Members

        private static ConcurrentDictionary<string, ConcurrentDictionary<string, object>> _usersData = new ConcurrentDictionary<string, ConcurrentDictionary<string, object>>();
        private IUserInfo _userInfo = null;

        #endregion

        #region Constructors

        public UserSessionService()
        {

        }

        #endregion

        #region Public

        /// <summary>
        /// Устангавливает информацию о пользователе
        /// </summary>
        /// <param name="userInfo"></param>
        public void SetUserInfo(IUserInfo userInfo)
        {
            _userInfo = userInfo;
        }

        #endregion

        #region Protected

        /// <summary>
        /// Информатия о пользователе
        /// </summary>
        protected IUserInfo UserInfo
        {
            get
            {
                return _userInfo;
            }
        }

        /// <summary>
        /// Данные пользователя (глобальные)
        /// </summary>
        protected ConcurrentDictionary<string, object> UserDataList
        {
            get
            {
                string userLogin = UserInfo.Login;
                return _usersData.GetOrAdd(userLogin, new ConcurrentDictionary<string, object>());
            }
        }


        #endregion

        #region IUserSessionService

        public T UserData<T>(string key)
        {
            object value = null;

            if (UserDataList.TryGetValue(key, out value))
            {
                return (T)value;
            }

            return default(T);
        }

        public T SetUserData<T>(string key, T value)
        {
            T result = (T)UserDataList.AddOrUpdate(key, value, (k, existingVal) => { return value; });
            return result;
        }

        public void DeleteUserData<T>()
        {
            UserDataList.Clear();
        }

        #endregion
    }
}