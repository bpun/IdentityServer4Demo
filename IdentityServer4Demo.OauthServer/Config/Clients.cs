using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer4Demo.OauthServer.Config
{
    public static class Clients
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "IdentityServer4Demo",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "IdentityServer4Demo.API" }
                },

                //Defining browser-based JavaScript client (e.g. SPA) for user authentication and delegated access and API
                new Client
                {

                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    //ClientUri = "http://identityserver.io",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =           { "http://localhost:5003/index.html" },
                    PostLogoutRedirectUris = { "http://localhost:5003/index.html" },
                    AllowedCorsOrigins =     { "http://localhost:5003" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,

                        "api1", "api2.read_only"
                     }
                 },

                //Defining a server-side web application (e.g. MVC) for use authentication and delegated API access
                new Client
                {

                        ClientId = "mvc",
                        ClientName = "MVC Client",
                        //ClientUri = "http://identityserver.io",

                        AllowedGrantTypes = GrantTypes.Hybrid,
                        AllowOfflineAccess = true,
                        ClientSecrets = { new Secret("secret".Sha256()) },

                        RedirectUris =           { "http://localhost:5001/signin-oidc" },
                        PostLogoutRedirectUris = { "http://localhost:5001/" },
                        LogoutUri =                "http://localhost:5001/signout-oidc",

                        AllowedScopes =
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.StandardScopes.Email,

                            "IdentityServer4Demo.API", "api2.read_only"
                        }
                }
            };
        }
    }
}
