using MongoDB.Bson;
using TestProject.Domain.Entities;

namespace TestProject.Domain.Handlers
{
    public interface IBookingHandler
    {
        decimal CalculeBookingValue(Booking booking);
        void CreateBooking(Booking booking);
        IEnumerable<Booking> GetBooking();
        string GetLicensePlateById(ObjectId id);
        User GetUserByUsername(string username);
        string GetUserNameById(Guid id);
    }
}
