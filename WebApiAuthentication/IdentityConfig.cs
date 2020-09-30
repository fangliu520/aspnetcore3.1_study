using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApiAuthentication
{
    public class IdentityConfig
    {
        public static IEnumerable<ApiResource> GetApiResource =>
           new List<ApiResource>
           {
                new ApiResource("api","my api")
           };
        public static IEnumerable<Client> Clients =>
         new List<Client>
         {
                new Client
                {
                     ClientId="webapicore.authdemo",
                      AllowedGrantTypes=GrantTypes.ClientCredentials,
                      ClientSecrets=
                      {
                      new Secret("secret".Sha256())
                      },
                      AllowedScopes=new []{ "api"}
                      ,
                      Claims=new List<Claim>(){
                      new Claim(IdentityModel.JwtClaimTypes.Role,"Admin"),
                      new Claim(IdentityModel.JwtClaimTypes.NickName,"MJD"),
                      new Claim("Phone","18016227549")
                      }

                }
                //,
                //// interactive ASP.NET Core MVC client
                //new Client
                //{
                //    ClientId = "mvc",
                //    ClientSecrets = { new Secret("secret".Sha256()) },

                //    AllowedGrantTypes = GrantTypes.Code,
                //    RequireConsent = false,
                //    RequirePkce = true,
                
                //    // where to redirect to after login
                //    RedirectUris = { "http://localhost:5002/signin-oidc" },
                
                //    // where to redirect to after logout
                //    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                //    AllowedScopes = new List<string>
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile
                //    }
                //}

         };
    }
}
