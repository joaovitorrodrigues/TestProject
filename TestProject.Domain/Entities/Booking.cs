using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace TestProject.Domain.Entities
{
    [Collection("bookings")]
    public class Booking
    {
        public ObjectId Id { get; set; }
        public ObjectId MotorcycleId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EndExpectedDate { get; set; }   
        public decimal Value { get; set; }
        public int Days { get; set; }

    }
}
