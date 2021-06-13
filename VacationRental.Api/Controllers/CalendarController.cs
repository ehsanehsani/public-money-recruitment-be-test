using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VacationRental.Domain.Enums;
using VacationRental.Domain.Models;
using VacationRental.Domain.Services;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {

        private readonly ICalendarService _calendarService;
        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get Specific Calendar By Id")]
        public CalendarViewModel Get(int rentalId, DateTime start, int nights)
        {
            return _calendarService.Get(rentalId, start, nights);
        }
    }
}
