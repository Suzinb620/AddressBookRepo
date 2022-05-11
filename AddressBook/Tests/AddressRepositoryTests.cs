using System.Collections.Generic;
using Domain.Entities;
using Infrastructure.Repositories;
using Xunit;

namespace Tests
{
    public class AddressRepositoryTests
    {
        [Fact]
        public void GetLastAdded_Returns_The_Correct_Element_If_Data_Not_Empty()
        {
            //Arrange
            var address1 = new Address("Bulowice", "Stara Dorga", 218);
            var address2 = new Address("Bulowice", "Nowa Dorga", 600);
            var addresses = new HashSet<Address>
            {
                address1,
                address2
            };

            //Act & Assert
            var addressBook = new AddressRepository(addresses);
            Assert.Same(address2, addressBook.GetLastAdded());
        }

        [Fact]
        public void GetLastAdded_Returns_The_Correct_Element_If_Data_Empty()
        {
            var addressBook = new AddressRepository(new HashSet<Address>());
            Assert.Null(addressBook.GetLastAdded());
        }

        [Fact]
        public void GetByCity_Returns_The_Correct_Elements_If_Entered_City_Exists()
        {
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
            var addressBook = new AddressRepository(addresses);
            Assert.Contains(address2, addressBook.GetByCity("Bulowice")!);
            Assert.Contains(address3, addressBook.GetByCity("Bulowice")!);
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
            var addressBook = new AddressRepository(addresses);
            Assert.Null(addressBook.GetByCity("Bielsko-Biala"));
        }

        [Fact]
        public void Add_Returns_The_Actual_Added_Value()
        {
            var address1 = new Address("Gliwice", "Stara Dorga", 218);
            var address2 = new Address("Bulowice", "Nowa Dorga", 600);
            var addresses = new HashSet<Address>
            {
                address1
            };
            var addressBook = new AddressRepository(addresses);
            Assert.Same(address2, addressBook.Add(address2));
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
            var addressBook = new AddressRepository(addresses);
            Assert.Null( addressBook.Add(address2));
        }
    }
}