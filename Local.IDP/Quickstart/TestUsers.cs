// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using IdentityServer4;

namespace IdentityServerHost.Quickstart.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users
        {
            get
            {
                return new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "7b80daf0-3cca-446a-bb21-f8036761d115",
                        Username = "Oscar", //Username should never be the same as name to avoid predictable usernames that can be later bruteforced
                        Password = "password", //Passwords should always be salted and saved encrypted (Sha256 recommended) this is just a way to get them fast from in Memory. Also, a strong password policy with at least numbers aqnd special char must be implemented.
                        Claims =
                        {
                            new Claim(JwtClaimTypes.GivenName, "Oscar"),
                            new Claim(JwtClaimTypes.FamilyName, "Medina"),
                            new Claim(JwtClaimTypes.Email, "oscar@email.com"),
                            new Claim(JwtClaimTypes.Address, "Fake street 213"),
                            new Claim("telephone", "1242312"),
                            new Claim("role", "admin")
                        }
                    },
                    new TestUser
                    {
                        SubjectId = "0e2118b1-ba06-4145-a42b-51dd710c44d0",
                        Username = "Maj",
                        Password = "password1",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.GivenName, "Maj"),
                            new Claim(JwtClaimTypes.FamilyName, "Abildgren"),
                            new Claim(JwtClaimTypes.Address, "Fake avenue 213"),
                            new Claim(JwtClaimTypes.Email, "maj@email.com"),
                            new Claim("telephone", "1242312"),
                            new Claim("role", "user")
                        }
                    },
                    new TestUser
                    {
                        SubjectId = "a8d73961-2432-43b0-96a1-88e46c206c3d",
                        Username = "Jose", 
                        Password = "pass123", 
                        Claims =
                        {
                            new Claim(JwtClaimTypes.GivenName, "Jose"),
                            new Claim(JwtClaimTypes.FamilyName, "Arredondo"),
                            new Claim(JwtClaimTypes.Email, "jose@email.com"),
                            new Claim(JwtClaimTypes.Address, "New street 213"),
                            new Claim("telephone", "4215653123"),
                            new Claim("role", "user")
                        }
                    },
                    new TestUser
                    {
                        SubjectId = "231gf340-23ca-f32a-34r1-32fs43122345",
                        Username = "Jimmy", 
                        Password = "password4",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.GivenName, "Jimmy"),
                            new Claim(JwtClaimTypes.FamilyName, "Carlsen"),
                            new Claim(JwtClaimTypes.Email, "jimmy@email.com"),
                            new Claim(JwtClaimTypes.Address, "Street ever green 213"),
                            new Claim("telephone", "241321321"),
                            new Claim("role", "user")
                        }
                    },
                    new TestUser
                    {
                        SubjectId = "74155be9-fd2a-4d34-8cc1-b82169d7d97b",
                        Username = "Raul", 
                        Password = "password2", 
                        Claims =
                        {
                            new Claim(JwtClaimTypes.GivenName, "Raul"),
                            new Claim(JwtClaimTypes.FamilyName, "Pineda"),
                            new Claim(JwtClaimTypes.Email, "raul@email.com"),
                            new Claim("telephone", "213214"),
                            new Claim(JwtClaimTypes.Address, "Avenue ever green 213"),
                            new Claim("role", "admin")
                        }
                    }
                };
            }
        }
    }
}
