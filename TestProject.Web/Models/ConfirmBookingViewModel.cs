using MongoDB.Bson;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestProject.Web.Models
{
    public class ConfirmBookingViewModel
    {        
        public ObjectId MotorcycleId { get; set; }
        public Guid UserId { get; set; }
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }
        [DisplayName("Expected end date")]
        public DateTime EndExpectedDate { get; set; }
        [DisplayName("Real end date")]
        [Required]
        public DateTime EndDate { get; set; }
        [DisplayName("Total R$")]
        public decimal Value { get; set; }
        [Required]
        public int Days { get; set; }
    }
}
