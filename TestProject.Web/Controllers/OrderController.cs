using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TestProject.Domain.Entities;
using TestProject.Domain.Handlers;
using TestProject.Web.Models;

namespace TestProject.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderHandler _orderHandler;
        public OrderController(IOrderHandler orderHandler)
        {
            _orderHandler = orderHandler;
        }
        public IActionResult Index()

        {
            // _orderHandler.Teste("oi");
            var order = _orderHandler.GetOrders();
            var list = new List<OrderViewModel>();


            foreach (var orderItem in order)
            {
                var newItem = new OrderViewModel()
                {
                    CreationDate = orderItem.CreationDate,
                    Id = orderItem.Id,
                    LicensePlate = _orderHandler.GetLicensePlateById(orderItem.MotorcycleId),
                    Price = orderItem.Price,
                    Status = orderItem.Status,
                    UserName = _orderHandler.GetUserNameById(orderItem.UserId)

                };
                list.Add(newItem);
            }

            return View(list.AsEnumerable());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(decimal price)
        {
            _orderHandler.CreateOrder(price);
            return View();
        }

        [HttpPost]
        public IActionResult Update(OrderViewModel viewModel)
        {
            try
            {
                Order order = new Order()
                {
                    CreationDate = viewModel.CreationDate,
                    Id = viewModel.Id,
                    MotorcycleId = viewModel.MotorcycleId,
                    Price = viewModel.Price,
                    Status = Domain.Enums.StatusType.Accepted,
                    UserId = viewModel.UserId
                };

                _orderHandler.UpdateOrder(order);
                TempData["ResultSuccessMessage"] = "Order Accepted!";
                return RedirectToAction("Index");

            }
            catch(Exception ex)
            {
                TempData["ResultErrorMessage"] = "error: " + ex.Message;
                return RedirectToAction("Index");
            }


        }
        public IActionResult GetOrder(ObjectId id)
        {
            try
            {
                var order = _orderHandler.GetOrder(id, User.Identity.Name);

                var orderViewModel = new OrderViewModel()
                {
                    CreationDate = order.CreationDate,
                    LicensePlate = _orderHandler.GetLicensePlateById(order.MotorcycleId),
                    Price = order.Price,
                    Status = order.Status,
                    UserName = User.Identity.Name,
                    UserId = order.UserId,
                    MotorcycleId = order.MotorcycleId
                };
                TempData["ResultSuccessMessage"] = "Order Accepted!";
                return View(orderViewModel);
            }
            catch (Exception ex)
            {
                TempData["ResultErrorMessage"] = "error: " + ex.Message;
                return RedirectToAction("Index");

            }
        }

        public IActionResult Deliver(ObjectId id)
        {
            try
            {
                _orderHandler.DeliverOrder(id);

                TempData["ResultSuccessMessage"] = "Order Delivered!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ResultErrorMessage"] = "error: " + ex.Message;
                return RedirectToAction("Index");

            }
        }


    }
}
