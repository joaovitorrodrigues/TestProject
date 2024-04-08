using MongoDB.Bson;
using TestProject.Domain.Entities;

namespace TestProject.Domain.Handlers
{
    public interface IMotorcycleHandler
    {
        Task CreateMotorcycle(Motorcycle motorcycle);
        Task Delete(ObjectId id);
        Task<IEnumerable<Motorcycle>> GetAll(string plate);
        Task<Motorcycle> GetByLicensePlate(string plate);
        Task UpdateMotorcycle(Motorcycle motorcycle);
    }
}
