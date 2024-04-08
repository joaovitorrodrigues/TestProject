using EasyNetQ;
using MongoDB.Bson;
using System.Reflection.Metadata.Ecma335;
using TestProject.Domain.Entities;
using TestProject.Domain.Handlers;
using TestProject.Domain.Repositories;

namespace TestProject.Application.Handlers
{
    public class OrderHandler : IOrderHandler
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookingRepository _bookingRepository;
        public OrderHandler(IOrderRepository orderRepository, IMotorcycleRepository motorcycleRepository, IUserRepository userRepository, IBookingRepository bookingRepository)
        {
            _orderRepository = orderRepository;
            _motorcycleRepository = motorcycleRepository;
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
        }

        public IEnumerable<Order> GetOrders()
        {
            return _orderRepository.GetOrders();
        }

        public string GetLicensePlateById(ObjectId id)
        {
            return _motorcycleRepository.GetPlateById(id);
        }

        public string GetUserNameById(Guid id)
        {
            return _userRepository.GetUsernameById(id);
        }

        public void CreateOrder(decimal price)
        {
            Order order = new Order() { Price = price, CreationDate = DateTime.Now};
            _orderRepository.Add(order);
        }

        //public async void Teste(string teste) 
        //{
        //    using(var bus = RabbitHutch.CreateBus("host=localhost"))
        //    {
        //        var message = new TextMessage() { Text = teste};
        //        await bus.PubSub.PublishAsync(message);
        //    }
        //}

        //void HandleTextMessage(TextMessage textMessage)
        //{
        //    var oi = textMessage.Text;

        //}

        public Order GetOrder(ObjectId id, string username)
        {
            var order = _orderRepository.GetById(id);
            var user = _userRepository.GetByUsername(username);

            if (!_bookingRepository.ActiveBookingByUserId(user.Id))
                throw new Exception("You need an active booking to get an order!");

            if (order.Status != Domain.Enums.StatusType.Available)
                throw new Exception("You cannot accept an already accepted order!");

            var booking = _bookingRepository.GetBookingByUserId(user.Id);

            order.MotorcycleId = booking.MotorcycleId;
            order.UserId = user.Id;
            order.Status = Domain.Enums.StatusType.Accepted;

            return order;
        }

        public void UpdateOrder(Order order)
        {
            _orderRepository.Update(order);
        }

        public void DeliverOrder(ObjectId id)
        {
            var order = _orderRepository.GetById(id);

            order.Status = Domain.Enums.StatusType.Delivered;

            _orderRepository.Update(order);
        }
    }
}
