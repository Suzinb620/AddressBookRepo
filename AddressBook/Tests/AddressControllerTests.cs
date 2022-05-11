using System.Collections.Generic;
using Application.DataTransferObjects;
using Application.Services;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using WebAPI.Controllers;
using Xunit;

namespace Tests
{
    public class AddressControllerTests
    {
        [Fact]
        public void GetLastAdded_Returns_Ok_Result_With_Correct_Element_When_AddressBook_Is_Not_Empty()
        {
            //Arrange
            var address1 = new Address("Bulowice", "Stara Dorga", 218);
            var address2 = new Address("Bulowice", "Nowa Dorga", 600);
            var addresses = new HashSet<Address>
            {
                address1,
                address2
            };
            var addressRepository = new AddressRepository(addresses);
            var addressService = new AddressService(addressRepository);
            var addressController = new AddressController(addressService, NullLogger<AddressController>.Instance);

            //Act
            var result = addressController.Get();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(new AddressDto(address2.Id, address2.City, address2.Street, address2.HouseNumber), okResult.Value);
        }

        [Fact]
        public void GetLastAdded_Returns_NotFound_Response_When_AddressBook_Is_Empty()
        {
            //Arrange
            var addressRepository = new AddressRepository(new HashSet<Address>());
            var addressService = new AddressService(addressRepository);
            var addressController = new AddressController(addressService, NullLogger<AddressController>.Instance);

            //Act & Assert
            Assert.IsType<NotFoundResult> (addressController.Get());
        }

        [Fact]
        public void GetByCity_Returns_Ok_Result_With_Correct_Objects_When_AddressBook_Is_Not_Empty()
        {
            //Arrange
            var address1 = new Address("Gliwice", "Stara Dorga", 218);
            var address2 = new Address("Bulowice", "Nowa Dorga", 600);
            var address3 = new Address("Bulowice", "Nowa Dorga", 600);
            var address4 = new Address("Bielsko-Biala", "Nowa Dorga", 600);
            var addresses = new HashSet<Address>
            {
                address1,
                address2,
                address3,
                address4
            };
            var addressRepository = new AddressRepository(addresses);
            var addressService = new AddressService(addressRepository);
            var addressController = new AddressController(addressService, NullLogger<AddressController>.Instance);

            //Act
            var result = addressController.Get("Bulowice");

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            //Assert.Contains(new AddressDto(address2.Id, address2.City, address2.Street, address2.HouseNumber), okResult.Value);
            //Assert.Contains(new AddressDto(address3.Id, address3.City, address3.Street, address3.HouseNumber), okResult.Value);
        }

        [Fact]
        public void GetByCity_Returns_The_Correct_Elements_If_Entered_City_Not_Exists()
        {
            var address1 = new Address("Gliwice", "Stara Dorga", 218);
            var address2 = new Address("Bulowice", "Nowa Dorga", 600);
            var addresses = new HashSet<Address>
            {
                address1,
                address2
            };
            var addressRepository = new AddressRepository(addresses);
            var addressesService = new AddressService(addressRepository);

            Assert.Null(addressesService.GetByCity("Bielsko-Biala"));
        }

        [Fact]
        public void Add_Returns_The_Actual_Added_Value()
        {
            var address1 = new Address("Gliwice", "Stara Dorga", 218);
            var addresses = new HashSet<Address>
            {
                address1
            };
            var addressRepository = new AddressRepository(addresses);
            var addressesService = new AddressService(addressRepository);
            var nonExistedAddress = new CreateAddressDto { City = "Bulowice", Street = "Nowa Dorga", HouseNumber = 600 };

            var result = addressesService.Add(nonExistedAddress);
            var expected = addressesService.GetLastAdded();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Add_Returns_Null_Because_Address_Already_In_Database()
        {
            var address1 = new Address("Gliwice", "Stara Dorga", 218);
            var address2 = new Address("Bulowice", "Nowa Dorga", 600);
            var addresses = new HashSet<Address>
            {
                address1,
                address2
            };
            var addressRepository = new AddressRepository(addresses);
            var addressesService = new AddressService(addressRepository);
            var existedAddress = new CreateAddressDto { City = "Bulowice", Street = "Nowa Dorga", HouseNumber = 600 };

            Assert.Null(addressesService.Add(existedAddress));
        }
    }
}