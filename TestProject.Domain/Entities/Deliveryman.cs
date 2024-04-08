using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;
using TestProject.Domain.Enums;

namespace TestProject.Domain.Entities
{
    [Collection("deliveryman")]
    public class Deliveryman 
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public CnhType CnhType { get; set; }
        public DateTime BirthDate { get; set; }
        public int CnhNumber { get; set; }
        public long Cnpj { get; set; }
        public Guid CnhImagePath { get; set; }


    }
}
