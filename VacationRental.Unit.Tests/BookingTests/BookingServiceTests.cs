using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
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

        [Fact]
        public void GetById_Should_Success_When_Booking_Id_Is_Exist()
        {
            //arrange
            var booking = new Dictionary<int, BookingViewModel>();
            var rental = Substitute.For<IDictionary<int, RentalViewModel>>();

            var book = new BookingViewModel()
            {
                Id = 1,
                Nights = 1,
                Start = Convert.ToDateTime("2021-01-20"),
                Unit = 1,
                RentalId = 1
            };

            booking.Add(1,book);
            
            var bookingService = new BookingService(booking, rental);

            //act
            var result = bookingService.GetById(1);

            //assert
            result.Should().Be(book);
        }
        
        [Fact]
        public void Create_Should_Throw_When_Not_Available_Rental()
        {
            //arrange
            var booking = new Dictionary<int, BookingViewModel>();
            var rental = new Dictionary<int, RentalViewModel>();

            var rent = new RentalViewModel()
            {
                Id = 1,
                Units = 1,
                PreparationTimeInDays = 0
            };
            
            var book = new BookingViewModel()
            {
                Id = 1,
                Nights = 1,
                Start = Convert.ToDateTime("2021-01-20"),
                Unit = 1,
                RentalId = 1
            };
            
            var book2= new BookingBindingModel()
            {
                Nights = 1,
                Start = Convert.ToDateTime("2021-01-20"),
                RentalId = 1
            };

            rental.Add(1,rent);
            booking.Add(1,book);
            
            var bookingService = new BookingService(booking, rental);

            //act
            Func<ResourceIdViewModel> create = ()=> bookingService.Create(book2);

            //assert
            create.Should().Throw<ApplicationException>();
        }
        
        [Fact]
        public void Create_Should_Success_Book_When_Available_Rental()
        {
            //arrange
            var booking = new Dictionary<int, BookingViewModel>();
            var rental = new Dictionary<int, RentalViewModel>();
            var expected = new ResourceIdViewModel()
            {
                Id = 2
            };

            var rent = new RentalViewModel()
            {
                Id = 1,
                Units = 2,
                PreparationTimeInDays = 0
            };
            
            var book1= new BookingBindingModel()
            {
                Nights = 1,
                Start = Convert.ToDateTime("2021-01-20"),
                RentalId = 1
            };
            
            var book2= new BookingBindingModel()
            {
                Nights = 1,
                Start = Convert.ToDateTime("2021-01-20"),
                RentalId = 1
            };

            rental.Add(1,rent);

            var bookingService = new BookingService(booking, rental);
            bookingService.Create(book1);

            //act
            var result =  bookingService.Create(book2);

            //assert
            result.Should().Equals(expected);
        }
        
    }
}