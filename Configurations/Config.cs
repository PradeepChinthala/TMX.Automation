using System;
using System.Collections.Generic;
using System.Configuration;

namespace Configurations
{
    public class Config
    {
        public static string Browser => ConfigurationManager.AppSettings["Browser"];
        public static string SiteUrl => ConfigurationManager.AppSettings["SiteUrl"];
        public static string Email => ConfigurationManager.AppSettings["Email"];
        public static string Password => ConfigurationManager.AppSettings["Password"];
        public static string AuthenticateUserName => ConfigurationManager.AppSettings["AuthenticateUserName"];
        public static string AuthenticatePassword => ConfigurationManager.AppSettings["AuthenticatePassword"];
        public static int Retries => Convert.ToInt32(ConfigurationManager.AppSettings["RefreshReties"]);

    }
}
