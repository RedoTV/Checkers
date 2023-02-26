using Checkers.Models;

namespace Checkers.Services
{
    public interface ILoginService
    {
        public Task Registry(HttpContext context, RegisterViewModel model);
        public Task LogIn(HttpContext context, SignInViewModel model);
    }
}