// using System.Collections.Generic;
// using Application.DataTransferObjects;
// using Application.Services;
// using DeepEqual.Syntax;
// using Domain.Entities;
// using Infrastructure.Repositories;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging.Abstractions;
// using WebAPI.Controllers;
// using Xunit;
//
// namespace Tests
// {
//     public class AddressControllerTests
//     {
//         [Fact]
//         public void GetLastAdded_Returns_Ok_Result_With_Correct_Element_When_AddressBook_Is_Not_Empty()
//         {
//             //Arrange
//             var address1 = new Address("Bulowice", "Stara Dorga", 218);
//             var address2 = new Address("Bulowice", "Nowa Dorga", 600);
//             var addresses = new HashSet<Address>
//             {
//                 address1,
//                 address2
//             };
//             var addressRepository = new AddressRepository(addresses);
//             var addressService = new AddressService(addressRepository);
//             var addressController = new AddressController(addressService, NullLogger<AddressController>.Instance);
//
//             //Act
//             var result = addressController.Get();
//             var expected = new AddressDto(address1.Id, address2.City, address2.Street, address2.HouseNumber);
//
//             //Assert
//             var okResult = Assert.IsType<OkObjectResult>(result);
//             expected.ShouldDeepEqual(okResult.Value);
//         }
//
//         [Fact]
//         public void GetLastAdded_Returns_NotFound_Response_When_AddressBook_Is_Empty()
//         {
//             //Arrange
//             var addressRepository = new AddressRepository(new HashSet<Address>());
//             var addressService = new AddressService(addressRepository);
//             var addressController = new AddressController(addressService, NullLogger<AddressController>.Instance);
//
//             //Act & Assert
//             Assert.IsType<NotFoundResult> (addressController.Get());
//         }
//
//         [Fact]
//         public void GetByCity_Returns_Ok_Result_With_Correct_Objects_When_AddressBook_Is_Not_Empty()
//         {
//             //Arrange
//             var address1 = new Address("Gliwice", "Stara Dorga", 218);
//             var address2 = new Address("Bulowice", "Nowa Dorga", 600);
//             var address3 = new Address("Bulowice", "Nowa Dorga", 600);
//             var address4 = new Address("Bielsko-Biala", "Nowa Dorga", 600);
//             var addresses = new HashSet<Address>
//             {
//                 address1,
//                 address2,
//                 address3,
//                 address4
//             };
//             var addressRepository = new AddressRepository(addresses);
//             var addressService = new AddressService(addressRepository);
//             var addressController = new AddressController(addressService, NullLogger<AddressController>.Instance);
//
//             //Act
//             var result = addressController.Get("Bulowice");
//
//             //Assert
//             var okResult = Assert.IsType<OkObjectResult>(result);
//             Assert.Contains(new AddressDto(address2.Id, address2.City, address2.Street, address2.HouseNumber), (IEnumerable<AddressDto>)okResult.Value!);
//             Assert.Contains(new AddressDto(address3.Id, address3.City, address3.Street, address3.HouseNumber), (IEnumerable<AddressDto>)okResult.Value!);
//         }
//
//         [Fact]
//         public void GetByCity_Returns_NotFound_Result_If_Entered_City_Not_Exists()
//         {
//             //Arrange
//             var address1 = new Address("Gliwice", "Stara Dorga", 218);
//             var address2 = new Address("Bulowice", "Nowa Dorga", 600);
//             var addresses = new HashSet<Address>
//             {
//                 address1,
//                 address2
//             };
//             var addressRepository = new AddressRepository(addresses);
//             var addressService = new AddressService(addressRepository);
//             var addressController = new AddressController(addressService, NullLogger<AddressController>.Instance);
//
//             //Act & Assert
//             Assert.IsType<NotFoundResult>(addressController.Get("Bielsko-Biala"));
//         }
//
//         [Fact]
//         public void Add_Returns_Returns_CreatedResult()
//         {
//             //Arrange
//             var address1 = new Address("Gliwice", "Stara Dorga", 218);
//             var addresses = new HashSet<Address>
//             {
//                 address1
//             };
//             var addressRepository = new AddressRepository(addresses);
//             var addressService = new AddressService(addressRepository);
//             var addressController = new AddressController(addressService, NullLogger<AddressController>.Instance);
//             var nonExistedAddress = new CreateAddressDto { City = "Bulowice", Street = "Nowa Dorga", HouseNumber = 600 };
//
//             //Act
//             var result = addressController.Post(nonExistedAddress);
//             var expected = addressService.GetLastAdded();
//
//             //Assert
//             var okResult = Assert.IsType<CreatedResult>(result);
//             expected.ShouldDeepEqual(okResult.Value);
//         }
//
//         [Fact]
//         public void Add_Returns_ConflictResult()
//         {
//             //Arrange
//             var address1 = new Address("Gliwice", "Stara Dorga", 218);
//             var address2 = new Address("Bulowice", "Nowa Dorga", 600);
//             var addresses = new HashSet<Address>
//             {
//                 address1,
//                 address2
//             };
//             var addressRepository = new AddressRepository(addresses);
//             var addressService = new AddressService(addressRepository);
//             var addressController = new AddressController(addressService, NullLogger<AddressController>.Instance);
//             var existedAddress = new CreateAddressDto { City = "Bulowice", Street = "Nowa Dorga", HouseNumber = 600 };
//
//             //Act & Assert
//             Assert.IsType<ConflictResult>(addressController.Post(existedAddress));
//         }
//     }
// }