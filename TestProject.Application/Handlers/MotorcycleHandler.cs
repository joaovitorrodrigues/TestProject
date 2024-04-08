using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Domain.Entities;
using TestProject.Domain.Handlers;
using TestProject.Domain.Repositories;

namespace TestProject.Application.Handlers
{
    public class MotorcycleHandler : IMotorcycleHandler
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IOrderRepository _orderRepository;
        public MotorcycleHandler(IMotorcycleRepository motorcycleRepository, IOrderRepository orderRepository)
        {
            _motorcycleRepository = motorcycleRepository;
            _orderRepository = orderRepository;
        }

        public async Task CreateMotorcycle(Motorcycle motorcycle)
        {
            if (_motorcycleRepository.LicensePlateExists(motorcycle.LicensePlate))
                throw new Exception("This license plate has already been registered");

            _motorcycleRepository.Add(motorcycle);
        }

        public async Task<IEnumerable<Motorcycle>> GetAll(string plate)
        {

            return _motorcycleRepository.GetAll(plate);

        }

        public async Task<Motorcycle> GetByLicensePlate(string plate)
        {
            return _motorcycleRepository.GetByLicensePlate(plate);
        }

        public async Task UpdateMotorcycle(Motorcycle motorcycle)
        {
            if(_motorcycleRepository.IsNewPlateAlreadyRegistered(motorcycle))
                throw new Exception("This license plate has already been registered");

            _motorcycleRepository.Update(motorcycle);
        }

        public async Task Delete(ObjectId id)
        {
            if(_motorcycleRepository.IsRented(id))
                throw new Exception("This license plate has an active rent");

            if (_orderRepository.IsAcceptedOrderByMotorcycleId(id))
                throw new Exception("This license plate has an active order");

            var motorcycle = _motorcycleRepository.GetById(id);
            _motorcycleRepository.Delete(motorcycle);
            
        }
    }
}
