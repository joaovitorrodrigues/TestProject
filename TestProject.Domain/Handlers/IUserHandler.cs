using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Domain.Entities;

namespace TestProject.Domain.Handlers
{
    public interface IUserHandler
    {
        Task Create(User userModel, string password);
        Task UpdateCnhImagePath(string username, string cnhImagePath);
    }
}
