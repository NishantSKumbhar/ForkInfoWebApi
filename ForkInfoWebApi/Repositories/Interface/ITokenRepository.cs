﻿using Microsoft.AspNetCore.Identity;

namespace ForkInfoWebApi.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
