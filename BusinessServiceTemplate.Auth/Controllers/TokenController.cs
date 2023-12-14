using BusinessServiceTemplate.Auth.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessServiceTemplate.Auth.Controllers
{
    public class TokenController : Controller
    {
        private ITokenService _tokenService;
        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        
        //[HttpPost]
        //[Route("refresh")]
        //public IActionResult Refresh(TokenModel tokenApiModel)
        //{
        //    if (tokenApiModel is null)
        //        return BadRequest("Invalid client request");

        //    string? accessToken = tokenApiModel.AccessToken;
        //    string? refreshToken = tokenApiModel.RefreshToken;

        //    var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        //    var username = principal?.Identity?.Name; //this is mapped to the Name claim by default

        //    var user = _storeDbContext?.Logins?.SingleOrDefault(u => u.UserName == username);

        //    if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        //        return BadRequest("Invalid client request");

        //    var newAccessToken = _tokenService.GenerateAccessToken(principal?.Claims);
        //    var newRefreshToken = _tokenService.GenerateRefreshToken();

        //    user.RefreshToken = newRefreshToken;
        //    _storeDbContext?.SaveChanges();

        //    return Ok(new AuthenticatedResponse()
        //    {
        //        Token = newAccessToken,
        //        RefreshToken = newRefreshToken
        //    });
        //}

        [HttpPost]
        [Route("token")]
        public IActionResult Create() 
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Guest"),
                new Claim(ClaimTypes.Role, "Guest")
            };

            var accessToken = _tokenService.CreateAccessToken(claims);
            return Ok(accessToken);
        }

        [HttpPost("validate")]
        public IActionResult ValidateToken([FromBody] string requestToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters();

                tokenHandler.ValidateToken(requestToken, validationParameters, out SecurityToken validatedToken);

                return Ok(new { Message = "Token is valid" });
            }
            catch (SecurityTokenValidationException)
            {
                return Unauthorized(new { Message = "Invalid token" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "YourIssuer",
                ValidateAudience = true,
                ValidAudience = "YourAudience",
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKey")),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero // Optional: reduce or remove clock skew allowance
            };
        }
    }
}
