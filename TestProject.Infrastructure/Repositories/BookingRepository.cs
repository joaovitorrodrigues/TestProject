
using MongoDB.Bson;
using TestProject.Domain.Entities;
using TestProject.Domain.Repositories;
using TestProject.Infrastructure.Context;

namespace TestProject.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly TestProjectDbContext _testProjectDbContext;

        public BookingRepository(TestProjectDbContext testProjectDbContext)
        {
            _testProjectDbContext = testProjectDbContext;
        }

        public IEnumerable<Booking> GetAll()
        {
            return _testProjectDbContext.Bookings;
        }

        public void Add(Booking booking)
        {
            _testProjectDbContext.Bookings.Add(booking);

            _testProjectDbContext.ChangeTracker.DetectChanges();
            _testProjectDbContext.SaveChanges();
        }

        public void Update(Booking booking)
        {
            _testProjectDbContext.Bookings.Update(booking);

            _testProjectDbContext.ChangeTracker.DetectChanges();
            _testProjectDbContext.SaveChanges();
        }

        public bool ActiveBookingByUserId(Guid userId)
        {
            return _testProjectDbContext.Bookings.Any(x => x.UserId == userId && x.EndDate >= DateTime.Today);
        }

        public Booking GetBookingByUserId(Guid id)
        {
            return _testProjectDbContext.Bookings.FirstOrDefault(x => x.UserId == id && x.EndDate >= DateTime.Today);
        }
    }

}
