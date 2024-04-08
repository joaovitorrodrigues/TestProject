using MongoDB.Bson;
using TestProject.Domain.Entities;

namespace TestProject.Domain.Repositories
{
    public interface IOrderRepository
    {
        void Add(Order order);
        Order GetById(ObjectId id);
        IEnumerable<Order> GetOrders();
        bool IsAcceptedOrderByMotorcycleId(ObjectId motorcycleId);
        void Update(Order order);
    }
}
