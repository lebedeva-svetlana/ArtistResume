using Microsoft.AspNetCore.Identity;
using Resume.Models;

namespace Resume.Services
{
    public class UserInitializerService : IUserInitializerService
    {
        private UserManager<User> _userManager;
        private IUserInitializer _userInitializer;

        private string _email = "test@mail.com";
        private string _password = "Test1234!";

        public UserInitializerService(UserManager<User> userManager, IUserInitializer userInitializer)
        {
            _userManager = userManager;
            _userInitializer = userInitializer;
        }

        public async Task InitializeAsync()
        {
            await _userInitializer.InitializeAsync(_userManager, _email, _password);
        }
    }
}