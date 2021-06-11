using System;

namespace VacationRental.Domain.Exceptions
{
    public class RentalNotFoundException : ApplicationException
    {
        public RentalNotFoundException(string message) : base(message)
        {
            
        }
    }
}