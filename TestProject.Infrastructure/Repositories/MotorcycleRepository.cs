using MongoDB.Bson;
using MongoDB.Driver.Linq;
using TestProject.Domain.Entities;
using TestProject.Domain.Repositories;
using TestProject.Infrastructure.Context;

namespace TestProject.Infrastructure.Repositories
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly TestProjectDbContext _testProjectDbContext;

        public MotorcycleRepository(TestProjectDbContext testProjectDbContext)
        {
            _testProjectDbContext = testProjectDbContext;
        }

        public void Add(Motorcycle motorcycle)
        {
            _testProjectDbContext.Motorcycles.Add(motorcycle);

            _testProjectDbContext.ChangeTracker.DetectChanges();
            _testProjectDbContext.SaveChanges();
        }

        public void Update(Motorcycle motorcycle)
        {
            _testProjectDbContext.Motorcycles.Update(motorcycle);

            _testProjectDbContext.ChangeTracker.DetectChanges();
            _testProjectDbContext.SaveChanges();
        }

        public void Delete(Motorcycle motorcycle)
        {
            _testProjectDbContext.Motorcycles.Remove(motorcycle);

            _testProjectDbContext.ChangeTracker.DetectChanges();
            _testProjectDbContext.SaveChanges();
        }

        public Motorcycle GetById(ObjectId id)
        {
            return _testProjectDbContext.Motorcycles.FirstOrDefault(x => x.Id == id);
        }

        public Motorcycle GetByLicensePlate(string licensePlate)
        {
            return _testProjectDbContext.Motorcycles.FirstOrDefault(x => x.LicensePlate == licensePlate);
        }

        public bool LicensePlateExists(string licensePlate)
        {
            return _testProjectDbContext.Motorcycles.Any(x => x.LicensePlate == licensePlate);
        }

        public IEnumerable<Motorcycle> GetAll(string plate = null)
        {
            return _testProjectDbContext.Motorcycles.Where(x => x.LicensePlate == plate || plate == null);
        }

        public bool IsNewPlateAlreadyRegistered(Motorcycle motorcycle)
        {
            return _testProjectDbContext.Motorcycles.Any(x => x.LicensePlate.Equals(motorcycle.LicensePlate) && x.Id != motorcycle.Id);
        }

        public IEnumerable<Motorcycle> GetAllAvailable()
        {
            return _testProjectDbContext.Motorcycles.Where(x => x.AllocatedUntil < DateTime.Today);
        }

        public bool ActiveBooking(ObjectId id)
        {
            return _testProjectDbContext.Motorcycles.Any(_x => _x.Id == id && _x.AllocatedUntil > DateTime.Today);
        }

        public string GetPlateById(ObjectId id)
        {
            return _testProjectDbContext.Motorcycles.FirstOrDefault(x => x.Id == id)?.LicensePlate;
        }

        public void UpdateAllocatedUntil(ObjectId id, DateTime date)
        {
            var motorcycle = _testProjectDbContext.Motorcycles.FirstOrDefault(x => x.Id == id);
            if (motorcycle != null)
            {
                motorcycle.AllocatedUntil = date;
            }

            _testProjectDbContext.Motorcycles.Update(motorcycle);
            _testProjectDbContext.ChangeTracker.DetectChanges();
            _testProjectDbContext.SaveChanges();
        }

        public bool IsRented(ObjectId id)
        {
            return _testProjectDbContext.Motorcycles.Any(x => x.Id == id);
        }


    }
}
