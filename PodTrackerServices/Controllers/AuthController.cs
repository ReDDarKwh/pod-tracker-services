using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace WebApi.Jwt.Controllers
{
    public class AuthController : ApiController
    {

        public class AuthUser {
            public string Username{get;set;}
            public string Password { get; set; }
        }

        // THis is naive endpoint for demo, it should use Basic authentication to provide token or POST request
        [AllowAnonymous]
        [HttpPost]
        public string Post(AuthUser user)
        {
            if (CheckUser(user.Username, user.Password))
            {
                return JwtManager.GenerateToken(user.Username);
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        public bool CheckUser(string username, string password)
        {
            // should check in the database
            return true;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
              
            }
            base.Dispose(disposing);
        }


        //private User CreateUser( registerDetails)
        //{
        //    var passwordSalt = CreateSalt();
          

        //    return user;
        //}


        /// <summary>
        ///     Creates a random salt to be used for encrypting a password
        /// </summary>
        /// <returns></returns>
        public static string CreateSalt()
        {
            var data = new byte[0x10];
            using (var cryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                cryptoServiceProvider.GetBytes(data);
                return Convert.ToBase64String(data);
            }
        }

        /// <summary>
        ///     Encrypts a password using the given salt
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string EncryptPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = string.Format("{0}{1}", salt, password);
                var saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);
                return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
            }
        }

    }
}
