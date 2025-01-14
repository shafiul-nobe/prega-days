﻿using Microsoft.AspNetCore.Identity;

namespace PregaDays.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
