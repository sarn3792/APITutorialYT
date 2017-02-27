using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APITutorialYT
{
    public class UserSecurity
    {
        public static bool Login(string userName, string password)
        {
            FINANCIERAAPP_testEntities userContext = new FINANCIERAAPP_testEntities();

            return userContext.PaymentsUsers.Any(user => user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) && user.Password == password);
        }
    }
}