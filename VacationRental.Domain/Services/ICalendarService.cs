using System;
using VacationRental.Domain.Models;

namespace VacationRental.Domain.Services
{
    public interface ICalendarService
    {
        CalendarViewModel Get(int rentalId, DateTime start, int nights);
    }
}