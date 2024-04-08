using MongoDB.Bson;
using TestProject.Domain.Entities;
using TestProject.Domain.Enums;
using TestProject.Domain.Repositories;
using TestProject.Infrastructure.Context;

namespace TestProject.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TestProjectDbContext _testProjectDbContext;

        public OrderRepository(TestProjectDbContext testProjectDbContext)
        {
            _testProjectDbContext = testProjectDbContext;
        }
        
        public bool IsAcceptedOrderByMotorcycleId(ObjectId motorcycleId)
        {
            return _testProjectDbContext.Orders.Any(x => x.MotorcycleId == motorcycleId && x.Status != StatusType.Delivered);
        }

        public IEnumerable<Order> GetOrders()
        {
            return _testProjectDbContext.Orders;
        }

        public void Add(Order order)
        {
            _testProjectDbContext.Orders.Add(order);
            _testProjectDbContext.ChangeTracker.DetectChanges();
            _testProjectDbContext.SaveChanges();
        }

        public Order GetById(ObjectId id)
        {
            return _testProjectDbContext.Orders.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Order order)
        {
            _testProjectDbContext.Orders.Update(order);

            _testProjectDbContext.ChangeTracker.DetectChanges();
            _testProjectDbContext.SaveChanges();
        }
    }
}
