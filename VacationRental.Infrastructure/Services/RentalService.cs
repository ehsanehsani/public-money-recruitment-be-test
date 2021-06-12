using System;
using System.Collections.Generic;
using VacationRental.Domain.Models;
using VacationRental.Domain.Services;

namespace VacationRental.Infrastructure.Services
{
    public class RentalService : IRentalService
    {
        private readonly IDictionary<int, BookingViewModel> _bookings;
        private readonly IDictionary<int, RentalViewModel> _rentals;

        public RentalService(IDictionary<int, BookingViewModel> bookings, IDictionary<int, RentalViewModel> rentals)
        {
            _bookings = bookings;
            _rentals = rentals;
        }

        public RentalViewModel Get(int rentalId)
        {
            if (!_rentals.ContainsKey(rentalId))
                throw new ApplicationException("Rental not found");

            return _rentals[rentalId];
        }

        public ResourceIdViewModel Create(RentalBindingModel model)
        {
            var key = new ResourceIdViewModel { Id = _rentals.Keys.Count + 1 };

            _rentals.Add(key.Id, new RentalViewModel
            {
                Id = key.Id,
                Units = model.Units,
                PreparationTimeInDays = model.PreparationTimeInDays
            });

            return key;
        }
    }
}