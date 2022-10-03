using Application.DataTransferObjects;
using Application.Interfaces;
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
        
        // Methods:
        [HttpGet]
        public async Task<IActionResult> GetLastAdded()
        {
            try
            {
                var address = await _addressService.GetLastAddedAsync();
                if (address is null)
                {
                    _logger.LogError("Attempting to get the last added address that not exist");
                    return NotFound();
                }

                _logger.LogInformation("Got the last added address with id: {Address}", address.Id);
                return Ok(address);
            }
            catch (UnauthorizedAccessException e)
            {
                _logger.LogCritical("Exception: {Message}", e.Message);
                return Unauthorized();
            }
            catch (Exception e)
            {
                _logger.LogCritical("Exception: {Message}", e.Message);
                return StatusCode(500);
            }
        }
        
        [HttpGet("id")]
        public async Task<IActionResult> GetAddressById(string? id)
        {
            try
            {
                if (id is null)
                {
                    _logger.LogError("Attempting to get address by entering empty id");
                    return NotFound();
                } 
            
                var objectId = await _addressService.FindObjectIdAsync(id);
                if (objectId is null)
                {
                    _logger.LogError("Attempting to get the address by non-existent id: {Id}", id);
                    return NotFound();
                }
            
                var address = await _addressService.FindByObjectIdAsync((ObjectId)objectId);

                _logger.LogInformation("Got the address with id: {Address}", address?.Id);
                return Ok(address);
            }
            catch (UnauthorizedAccessException e)
            {
                _logger.LogCritical("Exception: {Message}", e.Message);
                return Unauthorized();
            }
            catch (Exception e)
            {
                _logger.LogCritical("Exception: {Message}", e.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetByCity(string? city)
        {
            try
            {
                if (city is null)
                {
                    _logger.LogError("Attempting to get addresses by entering empty city name");
                    return NotFound();
                }
            
                var addresses = await _addressService.GetByCityAsync(city);
                if (addresses is null)
                {
                    _logger.LogError("Attempting to get addresses by entering non-existent city name");
                    return NotFound();
                }
            
                _logger.LogInformation("Got the addresses by entering city name: {City}", city);
                return Ok(addresses);
            }
            catch (UnauthorizedAccessException e)
            {
                _logger.LogCritical("Exception: {Message}", e.Message);
                return Unauthorized();
            }
            catch (Exception e)
            {
                _logger.LogCritical("Exception: {Message}", e.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAddress(CreateAddressDto? address)
        {
            try
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

                _logger.LogInformation("Added new address with id: {Id}", created.Id);
                return Created($"api/posts/{created.Id}", created);
            }
            catch (UnauthorizedAccessException e)
            {
                _logger.LogCritical("Exception: {Message}", e.Message);
                return Unauthorized();
            }
            catch (Exception e)
            {
                _logger.LogCritical("Exception: {Message}", e.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteByObjectId(string? id)
        {
            try
            {
                if (id is null)
                {
                    _logger.LogError("Attempting to delete empty id");
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
            catch (UnauthorizedAccessException e)
            {
                _logger.LogCritical("Exception: {Message}", e.Message);
                return Unauthorized();
            }
            catch (Exception e)
            {
                _logger.LogCritical("Exception: {Message}", e.Message);
                return StatusCode(500);
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> PutAddress(AddressDto? address)
        {
            try
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
                    _logger.LogError("Attempting to put address with empty id");
                    return BadRequest(ModelState);
                }
                
                if (response.UpsertedId is null)
                {
                    if (response.ModifiedCount == 0)
                    {
                        var message = $"Updated 0 rows in address with id: {address.Id}";
                        _logger.LogInformation(message);
                        return Content(message);
                    }
                    else
                    {
                        var message = $"Updated {response.ModifiedCount} rows in address with id: {address.Id}";
                        _logger.LogInformation(message);
                        return Content(message);
                    }
                }

                _logger.LogInformation("Added new address with id: {Id}", response.UpsertedId.ToString());
                return Created($"api/posts/{response.UpsertedId}", response);
            }
            catch (UnauthorizedAccessException e)
            { 
                _logger.LogCritical("Exception: {Message}", e.Message);
                return Unauthorized();
            }
            catch (Exception e)
            {
                _logger.LogCritical("Exception: {Message}", e.Message);
                return StatusCode(500);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> PatchAddress(AddressDto? address)
        {
            try
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
            
                var response = await _addressService.PatchAsync(address);
                if (response is null)
                {
                    _logger.LogError("Attempting to put address with empty id");
                    return BadRequest(ModelState);
                }
                
                if (response.ModifiedCount == 0)
                {
                    var message = $"Updated 0 rows in address with id: {address.Id}";
                    _logger.LogInformation(message);
                    return Content(message);
                }
                
                var msg = $"Updated {response.ModifiedCount} rows in address with id: {address.Id}";
                _logger.LogInformation(msg);
                return Content(msg);
            }
            catch (UnauthorizedAccessException e)
            {
                _logger.LogCritical("Exception: {Message}", e.Message);
                return Unauthorized();
            }
            catch (Exception e)
            {
                _logger.LogCritical("Exception: {Message}", e.Message);
                return StatusCode(500);
            }
        }
    }
}
