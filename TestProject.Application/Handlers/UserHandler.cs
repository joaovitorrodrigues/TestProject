using Microsoft.AspNetCore.Identity;
using TestProject.Domain.Entities;
using TestProject.Domain.Handlers;
using TestProject.Domain.Repositories;

namespace TestProject.Application.Handlers
{
    public class UserHandler : IUserHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        public UserHandler(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task Create(User userModel, string password)
        {

            string errors = "";

            if (_userRepository.CnpjExists(userModel.Cnpj))
                errors += "This cnpj has already been registered;";

            if (_userRepository.EmailExists(userModel.Email))
                errors += "This email has already been registered;";

            if (_userRepository.CnhNumberExists(userModel.CnhNumber))
                errors += "This CNH number has already been registered;";

            if (!string.IsNullOrEmpty(errors))
                throw new Exception(errors);

            IdentityResult result = await _userManager.CreateAsync(userModel, password);
            _userManager.AddToRoleAsync(userModel, "Deliveryman");

            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach(error =>
                {
                    errors += error.Description + ";";
                });

                throw new Exception(errors);
            }


        }

        public async Task UpdateCnhImagePath(string username, string imagePath)
        {
            _userRepository.UpdateCnhImagePath(username, imagePath);
        }
    }
}
