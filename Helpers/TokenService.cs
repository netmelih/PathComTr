using DbConnection;
using DbConnection.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class TokenService : ITokenService
    {
        public static DataContext _dataContext;
        private readonly SymmetricSecurityKey _key;

        public TokenService(DataContext dataContext, IConfiguration config)
        {
            _dataContext = dataContext;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));
        }

        public string CreateToken(Accounts account)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.NameId, account.AccountId.ToString())
                };

                //Creating credentials. Specifying which type of Security Algorithm we are using
                var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

                //Creating Token description
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                //Finally returning the created token
                return tokenHandler.WriteToken(token);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }

    public interface ITokenService
    {
        public string CreateToken(Accounts account);
    }
}
