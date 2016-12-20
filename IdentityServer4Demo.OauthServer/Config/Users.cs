using IdentityServer4.Services.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4Demo.OauthServer.Config
{
    public static class Users
    {
        public static List<InMemoryUser> GetUsers()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject = "strong_death@outlook.com",
                    Username = "strong_death@outlook.com",
                    Password = "password"
                }
            };
        }

    }
}
