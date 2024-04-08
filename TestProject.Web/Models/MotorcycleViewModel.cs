using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace TestProject.Web.Models
{
    public class MotorcycleViewModel
    {
        [Required]
        public int Year { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string LicensePlate { get; set; }
        public IEnumerable<MotorcycleQueryViewModel> Motorcycles { get; set; }
    }

    public class MotorcycleQueryViewModel
    {
        public ObjectId Id { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string LicensePlate { get; set; }
        public DateTime AllocatedUntil { get; set; }

    }
}
