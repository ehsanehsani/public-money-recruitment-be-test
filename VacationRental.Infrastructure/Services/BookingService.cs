using System;
using System.Collections.Generic;
using System.Linq;
using VacationRental.Domain.Enums;
using VacationRental.Domain.Models;
using VacationRental.Domain.Services;

namespace VacationRental.Infrastructure.Services
{
    public class BookingService : IBookingService
    {
        private readonly IDictionary<int, BookingViewModel> _bookings;
        private readonly IDictionary<int, RentalViewModel> _rentals;

        public BookingService(IDictionary<int, BookingViewModel> bookings, IDictionary<int, RentalViewModel> rentals)
        {
            _bookings = bookings;
            _rentals = rentals;
        }

        public BookingViewModel GetById(int bookingId)
        {
            if (!_bookings.ContainsKey(bookingId))
                throw new ApplicationException("Booking not found");

            return _bookings[bookingId];
        }

        public ResourceIdViewModel Create(BookingBindingModel model)
        {
            var reservedUnitNumber = new List<int>();

            if (model.Nights <= 0)
                throw new ApplicationException("Nights must be positive");
            if (!_rentals.ContainsKey(model.RentalId))
                throw new ApplicationException("Rental not found");

            for (var i = 0; i < model.Nights; i++)
            {
                var count = 0;
                foreach (var booking in _bookings.Values)
                {
                    if (booking.RentalId == model.RentalId
                        && (booking.Start <= model.Start.Date &&
                            booking.Start.AddDays(booking.Nights) > model.Start.Date)
                        || (booking.Start < model.Start.AddDays(model.Nights) &&
                            booking.Start.AddDays(booking.Nights) >= model.Start.AddDays(model.Nights))
                        || (booking.Start > model.Start &&
                            booking.Start.AddDays(booking.Nights) < model.Start.AddDays(model.Nights))

                        //check for preparation time
                        || booking.Start > model.Start && booking.Start <
                        model.Start.AddDays(model.Nights + _rentals[model.RentalId].PreparationTimeInDays))
                    {
                        count++;
                        if (reservedUnitNumber.IndexOf(booking.Unit) == -1)
                        {
                            reservedUnitNumber.Add(booking.Unit);
                        }
                    }
                }

                if (count >= _rentals[model.RentalId].Units)
                    throw new ApplicationException("Not available");
            }

            //In each book we should assign the unit number, so we want to find available unit number:
            var unitLists = Enumerable.Range(1, _rentals[model.RentalId].Units).ToList();
            var unitNumber = unitLists.Except(reservedUnitNumber).FirstOrDefault();

            if (unitNumber == 0)
            {
                throw new ApplicationException("Not available");
            }

            var key = new ResourceIdViewModel {Id = _bookings.Keys.Count + 1};

            _bookings.Add(key.Id, new BookingViewModel(bookingType: BookingType.Booking)
            {
                Id = key.Id,
                Nights = model.Nights,
                RentalId = model.RentalId,
                Unit = unitNumber,
                Start = model.Start.Date
            });

            if (_rentals[model.RentalId].PreparationTimeInDays > 0)
            {
                var preparationTimekey = new ResourceIdViewModel {Id = _bookings.Keys.Count + 1};
                _bookings.Add(preparationTimekey.Id, new BookingViewModel(bookingType: BookingType.Preparation)
                {
                    Id = preparationTimekey.Id,
                    Nights = _rentals[model.RentalId].PreparationTimeInDays,
                    RentalId = model.RentalId,
                    Unit = unitNumber,
                    Start = model.Start.AddDays(model.Nights)
                });
            }

            return key;
        }
    }
}