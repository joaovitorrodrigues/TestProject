using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Bson;
using TestProject.Domain.Entities;
using TestProject.Domain.Handlers;
using TestProject.Domain.Repositories;

namespace TestProject.Application.Handlers
{
    public class BookingHandler : IBookingHandler
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;
        public BookingHandler(IBookingRepository bookingRepository, IMotorcycleRepository motorcycleRepository, IUserRepository userRepository, ISubscriptionPlanRepository subscriptionPlanRepository)
        {
            _bookingRepository = bookingRepository;
            _motorcycleRepository = motorcycleRepository;
            _userRepository = userRepository;
            _subscriptionPlanRepository = subscriptionPlanRepository;
        }

        public IEnumerable<Booking> GetBooking()
        {
            return _bookingRepository.GetAll();
        }

        public User GetUserByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }

        public decimal CalculeBookingValue(Booking booking)
        {
            var totalDays = (booking.EndDate.Date - booking.StartDate.Date).Days;
            var subscriptionPlan = _subscriptionPlanRepository.GetSubscriptionPlanByDays(booking.Days);

            if (totalDays == booking.Days)
            {
                var valuePerDay = subscriptionPlan.Price;
                return valuePerDay * totalDays;
            }

            if (totalDays < booking.Days)
            {
                var totalDaysForCompensation = booking.Days - totalDays;
                var valuePerDay = subscriptionPlan.Price * totalDays;
                return valuePerDay += (totalDaysForCompensation * subscriptionPlan.Price) * (subscriptionPlan.CompesationPercentage / 100);
            }
            else
            {
                var totalDaysForCompensation = totalDays - booking.Days;
                var valuePerDay = booking.Days * subscriptionPlan.Price;
                return valuePerDay += (totalDaysForCompensation * 50);
            }
        }

        public void CreateBooking(Booking booking)
        {
            if (_motorcycleRepository.ActiveBooking(booking.MotorcycleId))
                throw new Exception("The motorcycle has already been reserved");

            if (_bookingRepository.ActiveBookingByUserId(booking.UserId))
                throw new Exception("The user already has an active reservation");

            _bookingRepository.Add(booking);
            _motorcycleRepository.UpdateAllocatedUntil(booking.MotorcycleId, booking.EndDate);
        }

        public string GetLicensePlateById(ObjectId id)
        {
            return _motorcycleRepository.GetPlateById(id);
        }

        public string GetUserNameById(Guid id)
        {
            return _userRepository.GetUsernameById(id);
        }
    }
}
