using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace TestProject.Domain.Entities
{
    [Collection("motorcycle")]
    public class Motorcycle
    {
        public ObjectId Id { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public DateTime AllocatedUntil { get; set; } = new DateTime(1000, 1, 1, 1, 1, 1);
    }
}
