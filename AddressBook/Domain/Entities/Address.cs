using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Address
    {
        //Variables:
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; init; }
        
        [BsonElement]
        [BsonRepresentation(BsonType.String)]
        public string City { get; set; }
        
        [BsonElement]
        [BsonRepresentation(BsonType.String)]
        public string Street { get; set; }
        
        [BsonElement]
        [BsonRepresentation(BsonType.Int64)]
        public int HouseNumber { get; set; }
        
        [BsonElement]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Created { get; init; }

        //Constructors:
        public Address(string city, string street, int houseNumber)
        {
            (City, Street, HouseNumber) = (city, street, houseNumber);
            Id = Guid.NewGuid();
            Created = DateTime.UtcNow;
        }

        //Methods:
        public override bool Equals(object? obj)
        {
            if (obj is not Address address) return false;
            return City == address.City && Street == address.Street && HouseNumber == address.HouseNumber;
        }

        public override int GetHashCode() => HashCode.Combine(City, Street, HouseNumber);
    }
}