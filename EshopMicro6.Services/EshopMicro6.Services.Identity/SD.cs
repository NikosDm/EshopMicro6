using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace EshopMicro6.Services.Identity;

public static class SD
{
    public const string Admin = "Admin";
    public const string Customer = "Customer";
    public static IEnumerable<IdentityResource> identityResources => 
        new List<IdentityResource> 
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> apiScopes => 
        new List<ApiScope> 
        { 
            new ApiScope("eshop", "Eshop Server"), 
            new ApiScope(name: "read", displayName: "Read your data"), 
            new ApiScope(name: "write", displayName: "Write your data"), 
            new ApiScope(name: "delete", displayName: "Delete your data")
        };
        
    public static IEnumerable<Client> clients => 
        new List<Client> 
        { 
            new Client 
            {
                ClientId = "eshop",
                ClientSecrets = { new Secret("mysecret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:5001/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:5001/signout-callback-oidc" },
                AllowedScopes =  new List<string> 
                { 
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "eshop"
                }
            }
        };
}
