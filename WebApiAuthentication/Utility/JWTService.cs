using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApiAuthentication.Utility
{
    public interface IJWTService
    {
        string GetToken(string UserName);
    }
    public class JWTService : IJWTService
    {

        private readonly IConfiguration _iConfiguration;
        public JWTService(IConfiguration configuration)
        {
            _iConfiguration = configuration;
        }
        public string GetToken(string UserName)
        {
            Claim[] claims = new[] {
                  new Claim(ClaimTypes.Name,UserName),
                  new Claim("nickName","MJD"),
                  new Claim("Role","admin")
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iConfiguration["SecurityKey"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                 issuer: _iConfiguration["issuer"],
                 audience: _iConfiguration["audience"],
                 claims: claims,
                 expires: DateTime.Now.AddMinutes(5),
                 signingCredentials: creds

            );
            string rtoken = new JwtSecurityTokenHandler().WriteToken(token);

            return rtoken;


        }
    }
}
