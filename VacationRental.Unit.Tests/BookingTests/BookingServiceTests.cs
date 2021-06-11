using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using VacationRental.Domain.Exceptions;
using VacationRental.Domain.Models;
using VacationRental.Infrastructure.Services;
using Xunit;

namespace VacationRental.Unit.Tests.BookingTests
{
    public class BookingServiceTests
    {
        [Fact]
        public void GetById_Should_Throw_When_Booking_Not_Found()
        {
            //arrange
            var booking = Substitute.For<IDictionary<int, BookingViewModel>>();
            var rental = Substitute.For<IDictionary<int, RentalViewModel>>();

            var bookingService = new BookingService(booking, rental);
            
            //act
            Func<BookingViewModel> get = () => bookingService.GetById(1);

            //assert
            get.Should().Throw<BookingNotFoundException>();
        }
        
    }
}