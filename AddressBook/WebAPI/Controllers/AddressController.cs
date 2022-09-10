using Application.DataTransferObjects;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        //Variables:
        private readonly IAddressService _addressService;
        private readonly ILogger _logger;

        //Constructors:
        public AddressController(IAddressService addressService, ILogger<AddressController> logger)
        {
            _addressService = addressService;
            _logger = logger;
        }

        //Methods:
        [HttpGet]
        public async Task<IActionResult> Get()
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

        [HttpGet("{city}")]
        public async Task<IActionResult> Get(string? city)
        {
            if (city is null)
            {
                _logger.LogError("Attempting to get addresses by entering not empty city name");
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
        public async Task<IActionResult> Post(CreateAddressDto? address)
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
    }
}
