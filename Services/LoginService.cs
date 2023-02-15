using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Checkers.Data;
using Checkers.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Checkers.Services
{
    
    public class LoginService : ILoginService
    {
        //TODO ХРЕНАЧЬ ПОКА НЕ СДЕЛАЕШЬ
        CheckersDbContext CheckersDbContext;
        public LoginService(CheckersDbContext checkersDbContext)
        {
            CheckersDbContext = checkersDbContext;
        }
        public async Task Registry(HttpContext context, string name, string password){
            string hashedPassword = ComputeStringToSha256Hash(password);
            User createdUser = new User
            {
                Name = name,
                Password = hashedPassword
            }; 
            if(name != null && hashedPassword != null)
            {
                await CheckersDbContext.Users.AddAsync(createdUser);
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, createdUser.Name) };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            }
            else return;
        }

        static string ComputeStringToSha256Hash(string plainText)
        { 
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));

                StringBuilder stringbuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringbuilder.Append(bytes[i].ToString("x2"));
                }
                return stringbuilder.ToString();
            }
        }
    }
}