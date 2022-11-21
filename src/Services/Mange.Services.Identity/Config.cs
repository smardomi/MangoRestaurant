using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Mange.Services.Identity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("mango", "Mango Server"),
                new ApiScope(name: "read",   displayName: "Read your data."),
                new ApiScope(name: "write",  displayName: "Write your data."),
                new ApiScope(name: "delete", displayName: "Delete your data.")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
            // m2m client credentials flow client
            new Client
            {
                ClientId="client",
                ClientSecrets= { new Secret("secret".Sha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes={ "read", "write","profile"}
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId="mango",
                ClientSecrets= { new Secret("secret".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:7028/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:7028/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:7028/signout-callback-oidc" },
                AllowedScopes=new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "mango"
                    }
            },
            };
    }
}