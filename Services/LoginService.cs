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
        private readonly CheckersDbContext CheckersDbContext;
        public LoginService(CheckersDbContext checkersDbContext)
        {
            CheckersDbContext = checkersDbContext;
        }
        public async Task Registry(HttpContext context, RegisterViewModel model)
        {
            if(string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Password))
            {
                throw new Exception("Incorrect input login or password");
            }

            var findedUser = CheckersDbContext.Users.Where(u => u.Name == model.Name).Count();
            if(findedUser > 0)
            {
                throw new Exception("User with this login already exist");
            }
            
            string hashedPassword = ComputeStringToSha256Hash(model.Password);
            User createdUser = new User
            {
                Name = model.Name,
                Password = hashedPassword
            }; 
            
            //adding user to database
            await CheckersDbContext.Users.AddAsync(createdUser);
            await CheckersDbContext.SaveChangesAsync();            
            //user authorization
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, createdUser.Name) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        public async Task LogIn(HttpContext context, SignInViewModel model)
        {
            string hashedPassword = ComputeStringToSha256Hash(model.Password);
            int usrFromModel = CheckersDbContext.Users.Where(u => u.Name == model.Name && u.Password == hashedPassword).Count();
            if(usrFromModel == 1)
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, model.Name) };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,"Cookies");
                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            }
            else if(usrFromModel == 0)
            {
                throw new Exception("User with this login and password not exist in database");
            }
            else if(usrFromModel > 1)
            {
                throw new Exception("More then 1 user exist in database!");
            }
        }

        public static string ComputeStringToSha256Hash(string plainText)
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