using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace TestProject.Domain.Entities
{
    [Collection("subscription-plan")]
    public class SubscriptionPlan
    {
        public ObjectId Id { get; set; }
        public int Days { get; set; }
        public decimal Price { get; set; }
        public decimal CompesationPercentage  { get; set; }


    }
}
