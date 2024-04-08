using MongoDB.Bson;
using TestProject.Domain.Entities;

namespace TestProject.Domain.Repositories
{
    public interface IBookingRepository
    {
        bool ActiveBookingByUserId(Guid userId);
        void Add(Booking booking);
        IEnumerable<Booking> GetAll();
        Booking GetBookingByUserId(Guid id);
        void Update(Booking booking);
    }
}
