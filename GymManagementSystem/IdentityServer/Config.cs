// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("administrator")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    ClientId = "console.client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = { "administrator" }
                },
                new Client()
                {
                    ClientId = "angular.spa.ui",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = false,
                    RequireClientSecret = false,
                    AccessTokenLifetime = 7200,

                    RedirectUris = {"https://localhost:4200" },
                    PostLogoutRedirectUris = {"https://localhost:4200" },
                    AllowedCorsOrigins = {"https://localhost:4200" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "administrator"
                    },
                    RequireRequestObject = false,
                    RequireConsent = false,
                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}