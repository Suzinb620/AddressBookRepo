namespace Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }

        public Address() { }
        public Address(int id, string name, string lastName, string city, string street, int houseNumber)
        {
            (Id, Name, LastName, City, Street, HouseNumber) = (id, name, lastName, city, street, houseNumber);
        }
    }
}
