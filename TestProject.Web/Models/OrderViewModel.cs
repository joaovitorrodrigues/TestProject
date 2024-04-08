using MongoDB.Bson;
using TestProject.Domain.Enums;

namespace TestProject.Web.Models
{
    public class OrderViewModel
    {
        public ObjectId Id { get; set; }
        public string LicensePlate { get; set; }
        public ObjectId MotorcycleId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Price { get; set; }
        public StatusType Status { get; set; }
    }
}
