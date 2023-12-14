using BusinessServiceTemplate.Auth.Services.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessServiceTemplate.Auth.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration config)
        {
            _configuration = config;
        }
        public string CreateAccessToken(IEnumerable<Claim> claims)
        {
            //var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );
            
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }

        public string CreateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public bool ValidateAccesstoken(string token)
        {
            string validationMessage = null;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JwtSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["JwtSettings:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                // Cast the validated token to JwtSecurityToken to access its Claims
                var jwtToken = validatedToken as JwtSecurityToken;
                if (jwtToken != null)
                {
                    var claims = jwtToken.Claims;
                    // You can now work with the claims
                    // Example: var userName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                    return true; // Or modify the return value as per your needs
                }

                validationMessage = "Token is valid";
                return false;
            }
            catch (SecurityTokenValidationException)
            {
                validationMessage = "Invalid token";
                return false;
            }
            catch (Exception ex)
            {
                validationMessage = $"An error occurred while validating the token: {ex.Message}";
                throw; // Rethrow any other unexpected exception
            }
        }
    }
}
