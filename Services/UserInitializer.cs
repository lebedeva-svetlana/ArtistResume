using Microsoft.AspNetCore.Identity;
using Resume.Models;

namespace Resume.Services
{
    public class UserInitializer : IUserInitializer
    {
        public async Task InitializeAsync(UserManager<User> userManager, string email, string password)
        {
            if (await userManager.FindByNameAsync(email) is not null)
            {
                return;
            }

            User admin = new()
            {
                Email = email,
                UserName = email
            };

            await userManager.CreateAsync(admin, password);
        }
    }
}