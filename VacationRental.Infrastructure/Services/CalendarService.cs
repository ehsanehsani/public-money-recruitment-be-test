using System;
using System.Collections.Generic;
using VacationRental.Domain.Enums;
using VacationRental.Domain.Models;
using VacationRental.Domain.Services;

namespace VacationRental.Infrastructure.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IDictionary<int, BookingViewModel> _bookings;
        private readonly IDictionary<int, RentalViewModel> _rentals;

        public CalendarService(IDictionary<int, BookingViewModel> bookings, IDictionary<int, RentalViewModel> rentals)
        {
            _bookings = bookings;
            _rentals = rentals;
        }

        public CalendarViewModel Get(int rentalId, DateTime start, int nights)
        {
            if (nights < 0)
                throw new ApplicationException("Nights must be positive");
            if (!_rentals.ContainsKey(rentalId))
                throw new ApplicationException("Rental not found");

            var result = new CalendarViewModel 
            {
                RentalId = rentalId,
                Dates = new List<CalendarDateViewModel>() 
            };
            for (var i = 0; i < nights; i++)
            {
                var date = new CalendarDateViewModel
                {
                    Date = start.Date.AddDays(i),
                    Bookings = new List<CalendarBookingViewModel>(),
                    PreparationTimes = new List<PreparationTimeViewModel>()
                };
                
                foreach (var booking in _bookings.Values)
                {
                    if (booking.RentalId == rentalId
                        && booking.Start <= date.Date && booking.Start.AddDays(booking.Nights) > date.Date)
                    {
                        if (booking.BookingType == BookingType.Booking)
                        {
                            date.Bookings.Add(new CalendarBookingViewModel
                            {
                                Id = booking.Id,
                                Unit = booking.Unit
                            });
                        }
                        
                        if (booking.BookingType == BookingType.Preparation)
                        {
                            date.PreparationTimes.Add(new PreparationTimeViewModel()
                            {
                                Unit = booking.Unit
                            });
                        }
                    }
                }

                result.Dates.Add(date);
            }

            return result;
        }
    }
}