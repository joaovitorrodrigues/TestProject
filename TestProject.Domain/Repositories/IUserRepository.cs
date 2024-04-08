using MongoDB.Bson;
using TestProject.Domain.Entities;

namespace TestProject.Domain.Repositories
{
    public interface IUserRepository
    {
        bool CnhNumberExists(int cnhNumber);
        bool CnpjExists(long cnpj);
        bool EmailExists(string email);
        User GetById(Guid id);
        User GetByUsername(string username);
        string GetUsernameById(Guid id);
        void UpdateCnhImagePath(string username, string imagepath);
    }
}
