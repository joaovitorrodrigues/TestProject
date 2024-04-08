using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TestProject.Domain.Entities;

namespace TestProject.Web.Controllers
{
    public class NotificationController : Controller
    {

        public NotificationController()
        {

        }

        //public async Task<IActionResult> Teste()
        //{
        //    await _hubContext.SendMessage(new TextMessage() { Text = "oi" });

        //    return RedirectToAction("Index", "Order");
        //}
    }
}
