using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Movies.Contracts.Requests;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Identity : ControllerBase
    {
        private const string TokenSecret = "SecureThisDevSessions";
        private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(8);
        [HttpPost("token")]
        public IActionResult GenerateToken([FromBody] GenerateTokenRequest request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(TokenSecret);

            var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, request.Email),
            new(JwtRegisteredClaimNames.Email, request.Email),
        };
            string valueType=string.Empty;

            foreach (var claimPair in request.CustomClaims)
            {
                var jsonElement = (JsonElement)claimPair.Value;
               
            switch (jsonElement.ValueKind)
                {
                    case JsonValueKind.True:
                        valueType = ClaimValueTypes.Boolean.ToString();
                        break;
                    case JsonValueKind.False:
                        valueType = ClaimValueTypes.Boolean.ToString();
                        break;
                    case JsonValueKind.Number:
                        valueType = ClaimValueTypes.Double.ToString();
                        break;
                }
                var claim = new Claim(claimPair.Key, claimPair.Value.ToString()!, valueType);
                claims.Add(claim);
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TokenLifetime),
                Issuer = "https://id.devsessions.com",
                Audience = "https://movies.devsessions.com",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var jwt = tokenHandler.WriteToken(token);
            return Ok(jwt);
        }
    }
}
