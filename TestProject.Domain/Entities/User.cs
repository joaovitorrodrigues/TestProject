using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;
using TestProject.Domain.Enums;

namespace TestProject.Domain.Entities
{
    [Collection("users")]
    public class User : MongoIdentityUser<Guid>
    {
        public string Name { get; set; }
        public CnhType CnhType { get; set; }
        public DateTime BirthDate { get; set; }
        public int CnhNumber { get; set; }
        public long Cnpj { get; set; }
        public string CnhImagePath { get; set; }
    }
}
