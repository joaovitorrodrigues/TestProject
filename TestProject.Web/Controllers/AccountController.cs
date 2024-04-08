using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TestProject.Domain.Entities;
using TestProject.Domain.Handlers;
using TestProject.Web.Models;

namespace TestProject.Web.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private RoleManager<Role> _roleManager;
        private IUserHandler _userHandler;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, IUserHandler userHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userHandler = userHandler;
            _roleManager = roleManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Required][EmailAddress] string email, [Required] string password, string returnurl)
        {
            User user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                user.Roles = new List<Guid>();
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (result.Succeeded)
                {

                    return Redirect(returnurl ?? "/");
                }

                ModelState.AddModelError(nameof(email), "Incorrect password or email!");

            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel userModel)
        {
            try
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
                        CnhImagePath = ""
                    };
                    string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");


                    await _userHandler.Create(user, userModel.Password);

                    var savedImagePath = await SaveImageAsync(userModel.CnhImage, folderPath);
                    await _userHandler.UpdateCnhImagePath(user.UserName, savedImagePath);

                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, userModel.Password, false, false);
                    return RedirectToAction("Index", "Home");
                }
                return View(userModel);
            }
            catch (Exception e)
            {
                e.Message.Split(";").ToList().ForEach(x =>
                {
                    ModelState.AddModelError("", x);
                });

                

                return View(userModel);
            }


        }

        private async Task<string> SaveImageAsync(IFormFile imageFile, string folderPath)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string folderName = Guid.NewGuid().ToString();
                string subFolderPath = Path.Combine(folderPath, folderName);

                if (!Directory.Exists(subFolderPath))
                {
                    Directory.CreateDirectory(subFolderPath);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string filePath = Path.Combine(subFolderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                 
                    await imageFile.CopyToAsync(stream);
                }
                return fileName; 
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return null;
            }
        }
    }
}
