﻿using System.Security.Claims;

namespace BusinessServiceTemplate.Auth.Services.Interfaces
{
    public interface ITokenService
    {
        public string CreateAccessToken(IEnumerable<Claim> claims);
        public bool ValidateAccesstoken(string token);
        public string CreateRefreshToken();
    }
}
