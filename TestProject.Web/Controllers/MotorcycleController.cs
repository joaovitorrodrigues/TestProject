using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using TestProject.Domain.Entities;
using TestProject.Domain.Handlers;
using TestProject.Web.Models;

namespace TestProject.Web.Controllers
{
    public class MotorcycleController : Controller
    {
        private readonly IMotorcycleHandler _motorcycleHandler;
        private readonly IMapper _mapper;
        public MotorcycleController(IMotorcycleHandler motorcycleHandler, IMapper mapper)
        {
            _motorcycleHandler = motorcycleHandler;
            _mapper = mapper;
        }
        [Authorize]
        public IActionResult Index(MotorcycleViewModel model)
        {
            var motorcycles = _motorcycleHandler.GetAll(model.LicensePlate?.Trim()).Result;
            MotorcycleViewModel viewModel = new MotorcycleViewModel();

            var motorcycleViewList = new List<MotorcycleQueryViewModel>();

            foreach (var motorcycle in motorcycles)
            {
                var mappedItem = _mapper.Map<MotorcycleQueryViewModel>(motorcycle);
                motorcycleViewList.Add(mappedItem);
            }
            viewModel.Motorcycles = motorcycleViewList.AsEnumerable();
            return View(viewModel);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MotorcycleViewModel motorcycleModel)
        {
            try
            {
                Motorcycle motorcycle = new Motorcycle()
                {
                    Model = motorcycleModel.Model?.Trim(),
                    Year = motorcycleModel.Year,
                    LicensePlate = motorcycleModel.LicensePlate?.Trim()
                };
                await _motorcycleHandler.CreateMotorcycle(motorcycle);


                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(motorcycleModel);
            }
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string plate)
        {
            var motorcycle = _motorcycleHandler.GetByLicensePlate(plate).Result;
            MotorcycleQueryViewModel viewModel = new MotorcycleQueryViewModel()
            {
                Year = motorcycle.Year,
                Model = motorcycle.Model,
                LicensePlate = motorcycle.LicensePlate,
                Id = motorcycle.Id
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ObjectId id,string model, string licensePlate, int year)
        {
            try
            {
                Motorcycle motorcycle = new Motorcycle()
                {
                    Model = model?.Trim(),
                    Year = year,
                    LicensePlate = licensePlate?.Trim(),
                    Id = id
                };
                await _motorcycleHandler.UpdateMotorcycle(motorcycle);

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
        public IActionResult Delete(ObjectId id)
        {
            try
            {
                _motorcycleHandler.Delete(id);

                TempData["ResultSuccessMessage"] = "Item deleted successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ResultErrorMessage"] = "An error occurred while deleting the item: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

    }
}
