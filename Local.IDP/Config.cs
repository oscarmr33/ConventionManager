// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Local.IDP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource("roles", "Your roles", new List<string> { "role" }),
                new IdentityResource("email", "Your Email", new List<string> { "email" }),
                new IdentityResource("telephone", "Your telephone", new List<string> { "telephone" })
            };

        public static IEnumerable<ApiResource> Apis =>
           new ApiResource[]
           {
                new ApiResource(
                    "conventionsapi",
                    "Conventions Api",
                    new List<string>() { "role", "profile" })
                {
                    Scopes = { "conventionsapi" }
                }
           };


        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(
                    "conventionsapi",
                    "Conventions Api",
                    new List<string>() { "role", "profile" }),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientName = "Conventions Manager",
                    ClientId = "conventionsclient",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowOfflineAccess = true,
                    //AllowedGrantTypes = new List<string> () { GrantType.AuthorizationCode },
                    RedirectUris = new List<string>()
					{
                        "https://localhost:44389/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>()
					{
                        "https://localhost:44389/signout-callback-oidc"
                    },
                    AllowedScopes =
					{
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        "telephone",
                        "roles",
                        "conventionsapi"
                    },
                    ClientSecrets =
					{
                        new Secret("secret".Sha256())
					},
                    RequirePkce = true,
                    RequireConsent = true
                }
            };
    }
}