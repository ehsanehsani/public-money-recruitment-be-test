using System;
using VacationRental.Api.Enums;

namespace VacationRental.Api.Models
{
    public class BookingViewModel
    {
        public BookingViewModel()
        {
            BookingType = BookingType.Booking;
        }

        public BookingViewModel(BookingType bookingType)
        {
            BookingType = bookingType;
        }
        
        public int Id { get; set; }

        public BookingType BookingType { get; }
        public int RentalId { get; set; }
        public int Unit { get; set; }
        public DateTime Start { get; set; }
        public int Nights { get; set; }
    }
}
