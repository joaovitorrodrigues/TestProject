using MongoDB.Bson;
using TestProject.Domain.Entities;

namespace TestProject.Domain.Handlers
{
    public interface IOrderHandler
    {
        void CreateOrder(decimal price);
        void DeliverOrder(ObjectId id);
        string GetLicensePlateById(ObjectId id);
        Order GetOrder(ObjectId id, string username);
        IEnumerable<Order> GetOrders();
        string GetUserNameById(Guid id);
        void UpdateOrder(Order order);
        //void Teste(string teste);
    }
}
