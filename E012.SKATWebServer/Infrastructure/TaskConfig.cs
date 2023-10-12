using System;
using System.Configuration;

namespace E012.SKAT.WebServer.Infrastructure
{
    /// <summary>
    /// конфигурация сервера безопасности
    /// </summary>
    public class TaskConfig : ConfigurationSection
    {
        [ConfigurationProperty("serverSecurityUri", IsRequired = true)]
        public String ServerSecurityUri { get { return (String)this["serverSecurityUri"]; } set { this["serverSecurityUri"] = value; } }

        [ConfigurationProperty("taskCode", IsRequired = true)]
        public String TaskCode { get { return (String)this["taskCode"]; } set { this["taskCode"] = value; } }

        [ConfigurationProperty("useServerSecurity", IsRequired = true)]
        public Boolean UseServerSecurity { get { return (Boolean)this["useServerSecurity"]; } set { this["useServerSecurity"] = value; } }
    }
}