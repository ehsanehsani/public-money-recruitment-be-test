using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VacationRental.Domain.Models;
using VacationRental.Domain.Services;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        [SwaggerOperation(Summary = "Get Specific Rental By Id")]
        public RentalViewModel Get(int rentalId)
        {
            return _rentalService.Get(rentalId);
        }

        
        [HttpPost]
        [SwaggerOperation(Summary = "Create New Rental Record")]
        public ResourceIdViewModel Post(RentalBindingModel model)
        {
            return _rentalService.Create(model);
        }
    }
}
