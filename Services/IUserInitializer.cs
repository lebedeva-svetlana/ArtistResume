using Microsoft.AspNetCore.Identity;
using Resume.Models;

namespace Resume.Services
{
    public interface IUserInitializer
    {
        public Task InitializeAsync(UserManager<User> userManager, string email, string password);
    }
}