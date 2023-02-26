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
        CheckersDbContext CheckersDbContext;
        public LoginService(CheckersDbContext checkersDbContext)
        {
            CheckersDbContext = checkersDbContext;
        }
        public async Task Registry(HttpContext context, RegisterViewModel model)
        {
            string hashedPassword = ComputeStringToSha256Hash(model.Password);
            User createdUser = new User
            {
                Name = model.Name,
                Password = hashedPassword
            }; 
            if(model.Name != null && hashedPassword != null)
            {
                await CheckersDbContext.Users.AddAsync(createdUser);
                await CheckersDbContext.SaveChangesAsync();
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, createdUser.Name) };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            }
            else return;
        }

        public async Task LogIn(HttpContext context, SignInViewModel model)
        {
            string hashedPassword = ComputeStringToSha256Hash(model.Password);
            User usr = CheckersDbContext.Users.Where(u => u.Name == model.Name && u.Password == hashedPassword).First();
            if(usr != null)
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, usr.Name) };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,"Cookies");
                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            }
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