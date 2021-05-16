using AccountManager.Models;
using AccountManager.Models.ViewModel;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using AccountManager.Helpers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace AccountManager.Api
{
    public class JWTService
    {
        private readonly AppSettings _appSettings;
        public JWTService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GenerateSecurityToken(UserSession userSession)
        {
            var subject = new ClaimsIdentity(new[]
            {
                new Claim("userId", userSession.UserID.ToString()),
                new Claim("emailAddress", userSession.EmailAddress.ToString(), ClaimValueTypes.Email),
                new Claim("fullname", userSession.FullName.ToString()),
            });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(subject),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

       
    }
}
