using MongoDB.Bson;
using TestProject.Domain.Entities;

namespace TestProject.Domain.Repositories
{
    public interface IMotorcycleRepository
    {
        bool ActiveBooking(ObjectId id);
        void Add(Motorcycle motorcycle);
        void Delete(Motorcycle motorcycle);
        IEnumerable<Motorcycle> GetAll(string plate = null);
        IEnumerable<Motorcycle> GetAllAvailable();
        Motorcycle GetById(ObjectId id);
        Motorcycle GetByLicensePlate(string licensePlate);
        string GetPlateById(ObjectId id);
        bool IsNewPlateAlreadyRegistered(Motorcycle motorcycle);
        bool IsRented(ObjectId id);
        bool LicensePlateExists(string licensePlate);
        void Update(Motorcycle motorcycle);
        void UpdateAllocatedUntil(ObjectId id, DateTime date);
    }
}
