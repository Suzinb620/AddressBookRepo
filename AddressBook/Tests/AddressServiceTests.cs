// using System.Collections.Generic;
// using Application.DataTransferObjects;
// using Application.Services;
// using DeepEqual.Syntax;
// using Domain.Entities;
// using Infrastructure.Repositories;
// using Xunit;
//
// namespace Tests
// {
//     public class AddressServiceTests
//     {
//         [Fact]
//         public void GetLastAdded_Returns_The_Correct_Element_If_Data_Not_Empty()
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
//
//             //Act & Assert
//             address2.WithDeepEqual(addressService.GetLastAdded()).SkipDefault<AddressDto>().IgnoreSourceProperty(x => x.Created).Assert();
//         }
//
//         [Fact]
//         public void GetLastAdded_Returns_The_Correct_Element_If_Data_Empty()
//         {
//             //Arrange
//             var addressRepository = new AddressRepository(new HashSet<Address>());
//             var addressService = new AddressService(addressRepository);
//
//             //Act & Assert
//             Assert.Null(addressService.GetLastAdded());
//         }
//
//         [Fact]
//         public void GetByCity_Returns_The_Correct_Elements_If_Entered_City_Exists()
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
//             var addressesService = new AddressService(addressRepository);
//
//             //Act & Assert
//             Assert.Contains(new AddressDto(address2.Id, address2.City, address2.Street, address2.HouseNumber), addressesService.GetByCity("Bulowice")!);
//             Assert.Contains(new AddressDto(address3.Id, address3.City, address3.Street, address3.HouseNumber), addressesService.GetByCity("Bulowice")!);
//         }
//
//         [Fact]
//         public void GetByCity_Returns_The_Correct_Elements_If_Entered_City_Not_Exists()
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
//             var addressesService = new AddressService(addressRepository);
//
//             //Act & Assert
//             Assert.Null(addressesService.GetByCity("Bielsko-Biala"));
//         }
//
//         [Fact]
//         public void Add_Returns_The_Actual_Added_Value()
//         {
//             //Arrange
//             var address1 = new Address("Gliwice", "Stara Dorga", 218);
//             var addresses = new HashSet<Address>
//             {
//                 address1
//             };
//             var addressRepository = new AddressRepository(addresses);
//             var addressesService = new AddressService(addressRepository);
//             var nonExistedAddress = new CreateAddressDto { City = "Bulowice", Street = "Nowa Dorga", HouseNumber = 600 };
//
//             //Act
//             var result = addressesService.Add(nonExistedAddress);
//             var expected = addressesService.GetLastAdded();
//
//             //Assert
//             result.ShouldDeepEqual(expected);
//         }
//
//         [Fact]
//         public void Add_Returns_Null_Because_Address_Already_In_Database()
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
//             var addressesService = new AddressService(addressRepository);
//             var existedAddress = new CreateAddressDto { City = "Bulowice", Street = "Nowa Dorga", HouseNumber = 600 };
//
//             //Act & Assert
//             Assert.Null(addressesService.Add(existedAddress));
//         }
//     }
// }