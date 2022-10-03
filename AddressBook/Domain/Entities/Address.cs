using System.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Address
    {
        //Variables:
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        
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
        public DateTime Updated { get; set; }

        [BsonElement]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Created { get; set; }
        
        [BsonElement]
        [BsonRepresentation(BsonType.String)]
        public string CreatedBy { get; set; }

        //Constructors:
        [BsonConstructor]
        public Address()
        {
            
        }
        
        public Address(string city, string street, int houseNumber)
        {
            (City, Street, HouseNumber) = (city, street, houseNumber);
            Id = new ObjectId();
            Updated = DateTime.UtcNow;
            Created = DateTime.UtcNow;
            CreatedBy = "admin";
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