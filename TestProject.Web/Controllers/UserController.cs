using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestProject.Domain.Entities;
using TestProject.Web.Models;

namespace TestProject.Web.Controllers
{
    public class UserController : Controller
    {
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;
        public UserController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel userModel)
        {
            if (userModel != null)
            {
                User user = new User
                {
                    UserName = userModel.Username,
                    Email = userModel.Email,
                    BirthDate = userModel.BirthDate,
                    CnhNumber = userModel.CnhNumber,
                    Name = userModel.Name,
                    Cnpj = userModel.Cnpj,
                    CnhType = userModel.CnhType,
                    CnhImagePath = "",                    
                };

                //await _roleManager.CreateAsync(new Role() { Name = "Admin" });
                //await _roleManager.CreateAsync(new Role() { Name = "Deliveryman" });
                _userManager.AddToRoleAsync(user, "Admin");
                IdentityResult result = await _userManager.CreateAsync(user, userModel.Password);

                if (result.Succeeded)
                {
                    ViewBag.Message = "user created!";
                }
                else
                {
                    result.Errors.ToList().ForEach(error =>
                    {
                        ModelState.AddModelError("", error.Description);
                    });
                }
            }

            return View(userModel);
        }
    }
}
