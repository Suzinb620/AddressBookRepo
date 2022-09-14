using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        // Variables:
        private readonly IMongoCollection<Address> _addresses;
        // TODO: add lock

        // Constructors:
        public AddressRepository(IConfiguration config)
        {
            var mongoDbConfig = config.GetSection("MongoDb");
            var mongoClient = new MongoClient(mongoDbConfig.GetSection("ConnectionString").Value);

            _addresses = mongoClient
                .GetDatabase(mongoDbConfig.GetSection("DatabaseName").Value)
                .GetCollection<Address>(mongoDbConfig.GetSection("CollectionName").Value);
        }

        // Methods:
        public ObjectId? FindObjectId(string id)
        {
            return _addresses
                .Find(new BsonDocument())
                .ToList()
                .Single(address => address.Id.ToString() == id)
                .Id;
        }
        
        public async Task<ObjectId?> FindObjectIdAsync(string id)
        {
            var addresses = await _addresses.FindAsync(new BsonDocument());
            var addressesList = await addresses.ToListAsync();
            
            return await Task.Run(() =>
            {
                return addressesList
                    .AsParallel()
                    .Single(address => address.Id.ToString() == id)
                    .Id;
            });
        }

        public Address? FindByObjectId(ObjectId objectId)
        {
            return _addresses
                .Find(new BsonDocument())
                .ToList()
                .Single(address => address.Id == objectId);
        }
        
        public async Task<Address?> FindByObjectIdAsync(ObjectId objectId)
        {
            var addresses = await _addresses.FindAsync(new BsonDocument());
            var addressesList = await addresses.ToListAsync();
            
            return await Task.Run(() =>
            {
                return addressesList
                    .AsParallel()
                    .Single(address => address.Id == objectId);
            });
        }
        
        public Address? GetLastAdded()
        {
            return _addresses
                .Find(new BsonDocument())
                .ToList()
                .MaxBy(address => address.Created);
        }

        public async Task<Address?> GetLastAddedAsync()
        {
            var addresses = await _addresses.FindAsync(new BsonDocument());
            var addressesList = await addresses.ToListAsync();
            
            return await Task.Run(() =>
            {
                return addressesList.AsParallel().MaxBy(address => address.Created);
            });
        }

        public IEnumerable<Address>? GetByCity(string city)
        {
            var addressesList = _addresses.Find(new BsonDocument()).ToList();

            if (!addressesList.Any())
            {
                return null;
            }

            var output = addressesList.Where(address => address.City == city).ToHashSet();
            
            return output.Any() ? output : null;
        }
        
        public async Task<IEnumerable<Address>?> GetByCityAsync(string city)
        {
            var addresses = await _addresses.FindAsync(new BsonDocument());
            var addressesList = await addresses.ToListAsync();
            
            if (!addressesList.Any())
            {
                return null;
            }

            var output =  await Task.Run(() =>
            {
                return addressesList
                    .AsParallel()
                    .Where(address => address.City == city)
                    .ToHashSet();
            });
            
            return output.Any() ? output : null;
        }

        public Address? Add(Address address)
        {
            var addressesList = _addresses.Find(new BsonDocument()).ToList();
            
            if (addressesList.Contains(address))
            {
                return null;
            }

            _addresses.InsertOne(address);
            return address;
        }
        
        public async Task<Address?> AddAsync(Address address)
        {
            var addresses = await _addresses.FindAsync(new BsonDocument());
            var addressesList = await addresses.ToListAsync();
            
            if (addressesList.Contains(address))
            {
                return null;
            }

            await _addresses.InsertOneAsync(address);
            return address;
        }

        public bool DeleteByObjectId(ObjectId objectId)
        {
            var addressesList = _addresses.Find(new BsonDocument()).ToList();
            var output = addressesList.Single(address => address.Id == objectId);

            if (output is null)
            {
                return false;
            }
            
            _addresses.DeleteOne(output.ToBsonDocument());
            return true;
        }

        public async Task<bool> DeleteByObjectIdAsync(ObjectId objectId)
        {
            var addresses = await _addresses.FindAsync(new BsonDocument());
            var addressesList = await addresses.ToListAsync();

            var output = await Task.Run(() =>
            {
                return addressesList
                    .AsParallel()
                    .Single(address => address.Id == objectId);
            });

            if (output is null)
            {
                return false;
            }
            
            await _addresses.DeleteOneAsync(output.ToBsonDocument());
            return true;
        }

        public Address? Put(Address address)
        {
            var toUpdate = FindByObjectId(address.Id);
            if (toUpdate is null)
            {
                return Add(address);
            }

            _addresses.UpdateOne(toUpdate.ToBsonDocument(), address.ToBsonDocument());
            return null;
        }

        public async Task<Address?> PutAsync(Address address)
        {
            var toUpdate = await FindByObjectIdAsync(address.Id);
            if (toUpdate is null)
            {
                return await AddAsync(address);
            }

            await _addresses.UpdateOneAsync(toUpdate.ToBsonDocument(), address.ToBsonDocument());
            return null;
        }
    }
}