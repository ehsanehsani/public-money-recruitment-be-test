using VacationRental.Domain.Models;

namespace VacationRental.Domain.Services
{
    public interface IBookingService
    {
        BookingViewModel GetById(int bookingId);
        ResourceIdViewModel Create(BookingBindingModel model);
    }
}