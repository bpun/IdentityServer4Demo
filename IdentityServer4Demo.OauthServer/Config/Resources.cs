using IdentityModel;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4Demo.OauthServer.Config
{
    public static class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            var customProfile = new IdentityResource(
                name: "custom.profile",
                displayName: "Custom profile",
                claimTypes: new[] { "name", "email", "status" });

            return new List<IdentityResource>
            {
                 new IdentityResources.OpenId(),
                 new IdentityResources.Email(),
                 new IdentityResources.Profile(),
                 new IdentityResources.Address()
            };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
           
            return new[]
           {
                // simple API with a single scope (in this case the scope name is the same as the api name)
                new ApiResource("IdentityServer4Demo.API", "IdentityServer4Demo.API 1"),

                // expanded version if more control is needed
                new ApiResource
                {
                    Name = "api2",

                    // secret for using introspection endpoint
                    ApiSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // include the following using claims in access token (in addition to subject id)
                    UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.Email }
                    },
            };
           }
    }
}
