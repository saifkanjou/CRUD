using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));//we use encoding to turn it into bytes 
        }
        public string CreateToken(AppUser user)
        {
            //put claims within our Token
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            };

            //creating signing for (what type of encryption we want)
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //now we actullay create the Token as an object
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),//ClaimsIdentity is loike a wallet for our claims(like our id, password..)
                Expires = DateTime.Now.AddDays(7),//expiration date
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            // (tokenHandler) method that will create the actual token for us
            var tokenHandler = new JwtSecurityTokenHandler();

            //we save  our token we utilize our token handler to create the token
            var token = tokenHandler.CreateToken(tokenDescriptor);


            return tokenHandler.WriteToken(token);//WriteToken() to return the Token in type of a string
        }
    }
}