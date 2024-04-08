using MongoDB.Bson;
using System.Security.Cryptography.X509Certificates;
using TestProject.Domain.Entities;
using TestProject.Domain.Repositories;
using TestProject.Infrastructure.Context;

namespace TestProject.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TestProjectDbContext _testProjectDbContext;

        public UserRepository(TestProjectDbContext testProjectDbContext)
        {
            _testProjectDbContext = testProjectDbContext;
        }

        public bool CnpjExists(long cnpj)
        {
            return _testProjectDbContext.Users.Any(x => x.Cnpj == cnpj);
        }

        public bool CnhNumberExists(int cnhNumber)
        {
            return _testProjectDbContext.Users.Any(x => x.CnhNumber == cnhNumber);
        }

        public bool EmailExists(string email)
        {
            return _testProjectDbContext.Users.Any(x => x.Email == email);
        }

        public void UpdateCnhImagePath(string username, string imagepath)
        {
            var user = _testProjectDbContext.Users.FirstOrDefault(x => x.UserName == username);
            user.CnhImagePath = imagepath;
            user.Roles = null;
            _testProjectDbContext.Update(user);
            _testProjectDbContext.ChangeTracker.DetectChanges();
            _testProjectDbContext.SaveChanges();

        }

        public User GetByUsername(string username)
        {
            return _testProjectDbContext.Users.FirstOrDefault(x => x.UserName == username);            
        }

        public User GetById(Guid id)
        {
            return _testProjectDbContext.Users.FirstOrDefault(x => x.Id == id);
        }
        public string GetUsernameById(Guid id)
        {
            return _testProjectDbContext.Users.FirstOrDefault(x => x.Id == id)?.UserName;
        }
    }
}
