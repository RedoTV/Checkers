namespace Checkers.Services
{
    public interface ILoginService
    {
        public Task Registry(HttpContext context, string name, string password);
    }
}