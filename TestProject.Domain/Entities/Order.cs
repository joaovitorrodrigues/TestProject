using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;
using TestProject.Domain.Enums;

namespace TestProject.Domain.Entities
{
    [Collection("order")]
    public class Order
    {
        public ObjectId Id { get; set; }
        public ObjectId MotorcycleId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Price { get; set; }
        public StatusType Status { get; set; }

    }
}
