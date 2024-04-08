using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace TestProject.Web.Models
{
    public class BookingViewModel
    {
        public ObjectId Id { get; set; }
        public ObjectId MotorcycleId { get; set; }
        public ObjectId UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Value { get; set; }
        [Required]
        public int Days { get; set; }
    }
}
