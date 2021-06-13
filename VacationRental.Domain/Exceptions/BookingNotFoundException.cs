using System;

namespace VacationRental.Domain.Exceptions
{
    public class BookingNotFoundException : ApplicationException
    {
        public BookingNotFoundException(string message) :base(message)
        {
            
        }
    }
}