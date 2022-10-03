using System.Collections;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        // TODO: add lock
        
        // Variables:
        private readonly IMongoCollection<Address> _addressesCollection;

        // Constructors:
        public AddressRepository(IConfiguration config)
        {
            var mongoDbConfig = config.GetSection("MongoDb");
            var mongoClient = new MongoClient(mongoDbConfig.GetSection("ConnectionString").Value);

            _addressesCollection = mongoClient
                .GetDatabase(mongoDbConfig.GetSection("DatabaseName").Value)
                .GetCollection<Address>(mongoDbConfig.GetSection("CollectionName").Value);
        }
        
        // Methods:
        private IQueryable<Address> GetAddresses()
        {
            return _addressesCollection.AsQueryable();
        }
        
        private async Task<IQueryable<Address>> GetAddressesAsync()
        {
            return await Task.Run(GetAddresses);
        }

        // Interface methods:
        public ObjectId? FindObjectId(string id)
        {
            return GetAddresses()
                .SingleOrDefault(address => address.Id.ToString() == id)
                ?.Id;
        }
        
        public async Task<ObjectId?> FindObjectIdAsync(string id)
        {
            var addresses = await GetAddressesAsync();
            
            return await Task.Run(() =>
            {
                return addresses
                    .AsParallel()
                    .SingleOrDefault(address => address.Id.ToString() == id)
                    ?.Id;
            });
        }

        public Address? FindByObjectId(ObjectId objectId)
        {
            return GetAddresses().SingleOrDefault(address => address.Id == objectId);
        }
        
        public async Task<Address?> FindByObjectIdAsync(ObjectId objectId)
        {
            var addresses = await GetAddressesAsync();

            return await Task.Run(() =>
            {
                return addresses
                    .AsParallel()
                    .SingleOrDefault(address => address.Id == objectId);
            });
        }
        
        public Address? GetLastAdded()
        {
            return GetAddresses().MaxBy(address => address.Created);
        }

        public async Task<Address?> GetLastAddedAsync()
        {
            var addresses = await GetAddressesAsync();

            return await Task.Run(() =>
            {
                return addresses.AsParallel().MaxBy(address => address.Created);
            });
        }

        public IEnumerable<Address>? GetByCity(string city)
        {
            var addresses = GetAddresses();
            if (!addresses.Any())
            {
                return null;
            }

            var output = addresses.Where(address => address.City == city);
            return output.ToArray().Any() ? output : null;
        }
        
        public async Task<IEnumerable<Address>?> GetByCityAsync(string city)
        {
            var addresses = await GetAddressesAsync();
            if (!addresses.Any())
            {
                return null;
            }

            var output =  await Task.Run(() =>
            {
                return addresses
                    .AsParallel()
                    .Where(address => address.City == city)
                    .AsQueryable();
            });
            return output.ToArray().Any() ? output : null;
        }

        public Address? Add(Address newAddress)
        {
            var addresses = GetAddresses();
            if (addresses.AsEnumerable().Contains(newAddress))
            {
                return null;
            }

            _addressesCollection.InsertOne(newAddress);
            return newAddress;
        }
        
        public async Task<Address?> AddAsync(Address newAddress)
        {
            var addresses = await GetAddressesAsync();
            if (addresses.AsEnumerable().Contains(newAddress))
            {
                return null;
            }

            await _addressesCollection.InsertOneAsync(newAddress);
            return newAddress;
        }

        public bool DeleteByObjectId(ObjectId objectId)
        {
            var addresses = GetAddresses();
            var output = addresses.SingleOrDefault(address => address.Id == objectId);

            if (output is null)
            {
                return false;
            }
            
            _addressesCollection.DeleteOne(output.ToBsonDocument());
            return true;
        }

        public async Task<bool> DeleteByObjectIdAsync(ObjectId objectId)
        {
            var addresses = await GetAddressesAsync();
            var output = await Task.Run(() =>
            {
                return addresses
                    .AsParallel()
                    .SingleOrDefault(address => address.Id == objectId);
            });

            if (output is null)
            {
                return false;
            }
            
            await _addressesCollection.DeleteOneAsync(output.ToBsonDocument());
            return true;
        }

        public ReplaceOneResult? Put(Address address)
        {
            var filter = FindByObjectId(address.Id);
            return _addressesCollection.ReplaceOne(filter.ToBsonDocument(), address, new ReplaceOptions
            {
                IsUpsert = true
            });
        }

        public async Task<ReplaceOneResult?> PutAsync(Address address)
        {
            var filter = await GetAddressesAsync();

            return await _addressesCollection.ReplaceOneAsync(filter.ToJson(), address, new ReplaceOptions
            {
                IsUpsert = true
            });
        }

        public UpdateResult? Patch(Address newAddress)
        {
            var oldAddress = FindByObjectId(newAddress.Id);
            return oldAddress is null 
                ? null 
                : _addressesCollection
                    .UpdateOne(oldAddress.ToBsonDocument(), newAddress.ToBsonDocument());
        }

        public async Task<UpdateResult?> PatchAsync(Address address)
        {
            var found = await FindByObjectIdAsync(address.Id);
            
            if (found is null)
            {
                return null;
            }

            return await _addressesCollection
                .UpdateOneAsync(found.ToBsonDocument(), address.ToBsonDocument());
        }
    }
}