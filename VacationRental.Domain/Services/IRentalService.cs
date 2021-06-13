using VacationRental.Domain.Models;

namespace VacationRental.Domain.Services
{
    public interface IRentalService
    {
        RentalViewModel Get(int rentalId);
        ResourceIdViewModel Create(RentalBindingModel model);
    }
}