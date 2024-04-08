using MongoDB.Bson;

namespace TestProject.Web.Models
{
    public class BookingQueryViewModel
    {
        public string LicensePlate { get; set; }
        public string Username { get; set; }
        public decimal Value { get; set; }
        public int Days { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string EndExpectedDate { get; set; }


    }
}
