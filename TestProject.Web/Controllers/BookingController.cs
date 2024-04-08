using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TestProject.Domain.Entities;
using TestProject.Domain.Handlers;
using TestProject.Web.Models;

namespace TestProject.Web.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingHandler _bookingHandler;
        private readonly IMapper _mapper;
        public BookingController(IBookingHandler bookingHandler, IMapper mapper)
        {
            _bookingHandler = bookingHandler;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var bookings = _bookingHandler.GetBooking().ToList();
            var bookingsViewModel = new List<BookingQueryViewModel>();

            bookings.ForEach(x =>
            {
                var newItem = new BookingQueryViewModel()
                {
                    Days = x.Days,
                    EndDate = x.EndDate.ToShortDateString(),
                    EndExpectedDate = x.EndExpectedDate.ToShortDateString(),
                    LicensePlate = _bookingHandler.GetLicensePlateById(x.MotorcycleId),
                    StartDate = x.StartDate.ToShortDateString(),
                    Value = x.Value,
                    Username = _bookingHandler.GetUserNameById(x.UserId)
                };
                bookingsViewModel.Add(newItem);
            });
            return View(bookingsViewModel);
        }

        public IActionResult Create(ObjectId motorcycleId)
        {
            var model = new BookingViewModel()
            {
                MotorcycleId = motorcycleId,
                StartDate = DateTime.Today.AddDays(1),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Confirm(ConfirmBookingViewModel model)
        {
            try
            {
                if (model.EndDate <= model.StartDate)
                    throw new Exception("End date can't be lower than start date!");

                if (model.Days == 0)
                    throw new Exception("You need to select days");

                Booking booking = new Booking()
                {
                    Days = model.Days,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    MotorcycleId = model.MotorcycleId,
                };
                model.Value = _bookingHandler.CalculeBookingValue(booking);
                model.EndExpectedDate = model.StartDate.AddDays(model.Days);
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ResultErrorMessage"] = "error: " + ex.Message;
                return RedirectToAction("Create", new { model.MotorcycleId });

            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ConfirmBookingViewModel model)
        {
            try
            {
                var user = _bookingHandler.GetUserByUsername(User.Identity.Name);

                if (user.CnhType == Domain.Enums.CnhType.Car)
                    throw new Exception("You need a CNH type A or AB to rent a motorcycle!");


                Booking booking = new Booking()
                {
                    UserId = user.Id,
                    Days = model.Days,
                    EndDate = model.EndDate,
                    EndExpectedDate = model.EndExpectedDate,
                    MotorcycleId = model.MotorcycleId,
                    StartDate = model.StartDate,
                    Value = model.Value
                };

                _bookingHandler.CreateBooking(booking);
                TempData["ResultSuccessMessage"] = "Booking confirmed!";
                return RedirectToAction("Index", "Motorcycle");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

        }
    }
}
