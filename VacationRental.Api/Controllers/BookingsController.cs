using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VacationRental.Domain.Enums;
using VacationRental.Domain.Models;
using VacationRental.Domain.Services;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        
        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        [SwaggerOperation(Summary = "Get Specific Booking By Id")]
        public BookingViewModel Get(int bookingId)
        {
            return _bookingService.GetById(bookingId);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create New Booking")]
        public ResourceIdViewModel Post(BookingBindingModel model)
        {
            return _bookingService.Create(model);
        }
    }
}
