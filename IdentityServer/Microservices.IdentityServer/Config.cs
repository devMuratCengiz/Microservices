// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace Microservices.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("ResourceCatalog"){Scopes={"CatalogFullPermission"}},
            new ApiResource("ResourcePhotoStock"){Scopes={"PhotoStockFullPermission"}},
            new ApiResource("ResourceBasket"){Scopes={"BasketFullPermission"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResource(){Name="roles",DisplayName="Roles",Description="Kullanıcı rolleri",UserClaims=new[]{"role"} }
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("CatalogFullPermission","Catalog API için tam erişim."),
                new ApiScope("PhotoStockFullPermission","Photo Stock API için tam erişim."),
                new ApiScope("BasketFullPermission","Basket API için tam erişim."),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {

                new Client
                {
                    ClientName="Asp.Net Core MVC",
                    ClientId="WebMvcClient",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes={ "CatalogFullPermission", "PhotoStockFullPermission",IdentityServerConstants.LocalApi.ScopeName }
                },
                new Client
                {
                    ClientName="Asp.Net Core MVC",
                    ClientId="WebMvcClientForUser",
                    AllowOfflineAccess = true,
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes={ "BasketFullPermission", IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile,IdentityServerConstants.StandardScopes.OfflineAccess, IdentityServerConstants.LocalApi.ScopeName, "roles" },
                    AccessTokenLifetime=1*60*60,
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime=(int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse
                }
            };
    }
}