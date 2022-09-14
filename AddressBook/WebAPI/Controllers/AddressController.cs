using Application.DataTransferObjects;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        // Variables:
        private readonly IAddressService _addressService;
        private readonly ILogger _logger;

        // Constructors:
        public AddressController(IAddressService addressService, ILogger<AddressController> logger)
        {
            _addressService = addressService;
            _logger = logger;
        }

        // TODO: Add try catch
        // Methods:
        [HttpGet]
        public async Task<IActionResult> GetLastAdded()
        {
            var address = await _addressService.GetLastAddedAsync();
            if (address is null)
            {
                _logger.LogError("Attempting to get the last added address that not exist");
                return NotFound();
            }
            
            _logger.LogInformation("Got the last added address");
            return Ok(address);
        }
        
        [HttpGet("id")]
        public async Task<IActionResult> GetAddressById(string? id)
        {
            if (id is null)
            {
                _logger.LogError("Attempting to get address by entering empty id");
                return NotFound();
            } 
            
            var objectId = await _addressService.FindObjectIdAsync(id);
            if (objectId is null)
            {
                _logger.LogError("Attempting to get the address by not exist id");
                return NotFound();
            }
            
            var address = await _addressService.FindByObjectIdAsync((ObjectId)objectId);

            _logger.LogInformation("Got the address by id");
            return Ok(address);
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetByCity(string? city)
        {
            if (city is null)
            {
                _logger.LogError("Attempting to get addresses by entering empty city name");
                return NotFound();
            }
            
            var addresses = await _addressService.GetByCityAsync(city);
            if (addresses is null)
            {
                _logger.LogError("Attempting to get addresses by entering not existing city name");
                return NotFound();
            }
            
            _logger.LogInformation("Got the addresses by entering city name");
            return Ok(addresses);
        }

        [HttpPost]
        public async Task<IActionResult> PostNewAddress(CreateAddressDto? address)
        {
            if (address is null)
            {
                _logger.LogError("Attempting to add empty address");
                return BadRequest();
            }
            
            if (!ModelState.IsValid)
            {
                _logger.LogError("Attempting to add address with invalid data");
                return BadRequest(ModelState);
            }

            var created = await _addressService.AddAsync(address);
            if (created is null)
            {
                _logger.LogError("Attempting to add currently existing address");
                return Conflict();
            }

            _logger.LogInformation("Added new address");
            return Created($"api/posts/{created.Id}", created);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteByObjectId(string? id)
        {
            if (id is null)
            {
                _logger.LogError("Empty id");
                return BadRequest();
            }

            var objectId = await _addressService.FindObjectIdAsync(id);
            if (objectId is null)
            {
                _logger.LogError("Attempting to delete not existing address");
                return NotFound();
            }

            await _addressService.DeleteByObjectIdAsync((ObjectId)objectId);

            _logger.LogInformation("Deleted address with id: {Id}", id);
            return Accepted();
        }
        
        [HttpPut]
        public async Task<IActionResult> PutByObjectId(AddressDto? address)
        {
             if (address is null)
             {
                 _logger.LogError("Attempting to put empty address");
                 return BadRequest();
             }
            
             if (!ModelState.IsValid)
             {
                 _logger.LogError("Attempting to put address with invalid data");
                 return BadRequest(ModelState);
             }
            
             var response = await _addressService.PutAsync(address);
             if (response is null)
             {
                 return Content("Updated already existed address");
             }

             return Created($"api/posts/{response.Id}", response);
        }

        // [HttpPatch]
        // public async Task<IActionResult> PatchByObjectId(string? id)
        // {
        //     
        // }
    }
}
