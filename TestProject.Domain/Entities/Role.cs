using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace TestProject.Domain.Entities
{
    [Collection("role")]
    public class Role : MongoIdentityRole<Guid>
    {
    }
}
