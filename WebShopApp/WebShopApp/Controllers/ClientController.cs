using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using WebShopApp.Infrastructure.Data.Domain;
using WebShopApp.Models.Client;

namespace WebShopApp.Controllers
{
    public class ClientController :Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ClientController (UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        
        public async Task<IActionResult> Index()
        {
            var allUsers = this._userManager.Users
                .Select(u => new ClientIndexVM
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Adress = u.Address,
                    Email = u.Email,
                })
                .ToList();

            
            var adminIds = (await _userManager.GetUsersInRoleAsync("Administrator"))
                .Select(a => a.Id).ToList();

            foreach (var user in allUsers)
            {
                user.IsAdmin = adminIds.Contains(user.Id);
            }

            var users = allUsers.Where(x => x.IsAdmin == false)
                .OrderBy(x => x.UserName).ToList();

            return this.View(users);
        }


    }
}
