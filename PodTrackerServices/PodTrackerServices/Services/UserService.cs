using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PodTrackerServices.Helpers;
using PodTrackerServices.Models;
using PodTrackerServices.podtrackdb;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static PodTrackerServices.Helpers.PasswordSecurity;

namespace PodTrackerServices.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);

    }

    public class UserService : IUserService
    {
       

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {

            using (var db = new podtrackdbContext())
            {

                var podUser = db.PodUser.SingleOrDefault(x => x.Username == username);

                var passwordMatch = PasswordStorage.VerifyPassword(password, podUser.Password);
                // return null if user not found
                if (podUser == null || !passwordMatch)
                    return null;

                var user = new User { PodUser = podUser };

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.PodUser.Username.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                // remove password before returning
                user.PodUser.Password = null;

                return user;
            }
        }

    }

}
