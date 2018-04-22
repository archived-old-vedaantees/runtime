using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4;
using IdentityModel;

namespace Vedaantees.Hosts.SingleSignOn
{
    internal class IdentityServerConfiguration
    {
        internal static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        internal static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "axelius-api",
                    ApiSecrets = { new Secret("guess-thi$-not-di55icult-resource".Sha256()) },
                    Scopes =
                    {
                        new Scope()
                        {
                            Name = "axelius-api.full_access",
                            DisplayName = "Full access to API",
                        }
                    }
                }
            };
        }

        internal static IEnumerable<Client> GetClients(List<string> clients)
        {
            List<Client> list = new List<Client>();


            foreach(var client in clients)
                list.Add(new Client
                        {
                            ClientId = "browser-client",
                            RequireConsent = false,
                            AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                            ClientSecrets = { new Secret("guess-thi$-not-di55icult-client".Sha256()) },
                            RedirectUris = { $"{client}/signin-oidc" },
                            PostLogoutRedirectUris = { $"{client}/signout-callback-oidc" },
                            AllowOfflineAccess = true,
                            AllowedScopes = {
                                                "axelius-api.full_access",
                                                JwtClaimTypes.Email,
                                                IdentityServerConstants.StandardScopes.OpenId,
                                                IdentityServerConstants.StandardScopes.Profile,
                                            }
                        });
            
            return list;
        }
    }
}